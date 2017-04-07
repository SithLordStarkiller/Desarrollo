using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Mime;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using PubliPayments.Entidades;
using PubliPayments.Negocios.ServicioOCR;
using PubliPayments.Utiles;

namespace PubliPayments.Negocios
{
    public class ProcesarOcr
    {
        /// <summary>
        /// Obtiene los numeros de ordenes son convenios y que fueron autorizados por el conyuge o el acreditado
        /// </summary>
        /// <param name="familiar">True = indica que va a regresar los creditos gestionados por el conyuge y 
        /// false los creditos gestionados por el acreditado </param>
        /// <returns>Regresa un dataset con los registros encontrados</returns>
        public DataSet ObtenerOrdenesPorAceptacionConvenio(bool familiar)
        {
            var guidTiempos = Tiempos.Iniciar();
            var ent = new EntProcesarOcr();
            var ds = ent.ObtenerOrdenesPorAceptacionConvenio(familiar);
            Tiempos.Terminar(guidTiempos);
            return ds;
        }

        /// <summary>
        /// Inserta la respuesta obtenida para la etiqueta de OCR
        /// </summary>
        /// <param name="idOrden">Id de la orden a agregarle la respuesta</param>
        /// <param name="etiqueta">Enumeración de la etiqueta que le corresponde a la orden dada</param>
        public void InsertarRespuestaOcr(int idOrden, EtiquetaOcr etiqueta)
        {
            string valor;
            switch (etiqueta)
            {
                case EtiquetaOcr.AceptadoFamiliar:
                    valor = "ACEPTADO POR FAMILIAR";
                    break;
                case EtiquetaOcr.CoincideCompletamente:
                    valor = "COINCIDE COMPLETAMENTE NOMBRE DEL ACREDITADO";
                    break;
                case EtiquetaOcr.CoincideParcialmente:
                    valor = "COINCIDE PARCIALMENTE NOMBRE DEL ACREDITADO";
                    break;
                case EtiquetaOcr.NoCoincideNoEsIdentificacion:
                    valor = "NO COINCIDE NOMBRE DEL ACREDITADO O NO ES IDENTIFICACION";
                    break;
                default:
                    throw new ArgumentOutOfRangeException("etiqueta");
            }
            var ent = new EntRespuestas();
            try
            {
                ent.InsertarRespuesta(idOrden,
                    "EtiquetaOCR", //Este nombre de campo tiene que coincidir con el que filtra los valores en los Stored Procedures
                    valor);
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "InsertarRespuestaOcr",
                    "InsertarRespuestaOcr - idOrden: " + idOrden + " - Etiqueta: " + etiqueta + " - Error:" + ex.Message);
            }
        }

        /// <summary>
        /// Obtiene un listado de las imagenes y los nombres de los acreditados para que sean procesados por el OCR
        /// </summary>
        /// <returns>Regresa un dataset con los registros encontrados</returns>
        public List<ProcesoOcrModel> ObtenerImagenesOcr()
        {
            var guidTiempos = Tiempos.Iniciar();
            var ent = new EntProcesarOcr();
            var ds = ent.ObtenerImagenesOcr();
            Tiempos.Terminar(guidTiempos);

            var lista = new List<ProcesoOcrModel>();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    lista.Add(new ProcesoOcrModel
                    {
                        IdOrden = Convert.ToInt32(row["idOrden"]),
                        Url = row["Valor"].ToString(),
                        NombreAcreditado = row["TX_NOMBRE_ACREDITADO"].ToString()
                    });
                }
            }

            return lista;
        }

        public string EnviarImagenOcr(string url)
        {
            var servicio = new ReconocimientoCaracteresClient();
            return servicio.ProcesarImagen(
                new ImagenOcr
                {
                    MejorarResolucion = false,
                    Origen = "CobranzaLondon",
                    PrioridadAlta = false,
                    Tipo = TipoDeExtraccion.Texto,
                    Uri = url
                });
        }

        public string ConsultarImagenOcr(string guid)
        {
            var servicio = new ReconocimientoCaracteresClient();
            var respuesta = servicio.ObtenerRespuestaOcr(guid);
            if (respuesta.Error != 0 || respuesta.EstatusOcr == Estatus.Error)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "ProcesarOcr",
                    "ConsultarImagenOcr - Guid: " + respuesta.Guid + " Error: " + respuesta.MensajeError);
                return null;
            }
            if (respuesta.EstatusOcr == Estatus.Procesada)
            {
                if (respuesta.InformacionExtraida.Trim().Length > 0)
                    return Encoding.UTF8.GetString(Convert.FromBase64String(respuesta.InformacionExtraida));
                //Si no hay texto regresa la palabra vacio
                return "vacio";
            }
            return "";
        }

        public EtiquetaOcr ObtenerEtiquetaOcr(string textoOcr, string nombreAcreditado)
        {
            var porcentajeINE = MaxSimilarInStringPercent(textoOcr, "INSTITUTO NACIONAL ELECTORAL");
            var porcentajeCPV = MaxSimilarInStringPercent(textoOcr, "CREDENCIAL PARA VOTAR");
            var porcentajeIFE = MaxSimilarInStringPercent(textoOcr, "INSTITUTO FEDERAL ELECTORAL");

            //Si no se reconoce como una identificación
            if (porcentajeINE < 50 && porcentajeCPV < 50 && porcentajeIFE < 50)
            {
                return EtiquetaOcr.NoCoincideNoEsIdentificacion;
            }

            //Obtengo el porcentaje de nombre
            //Limpio caracteres raros para mejorar el porcentaje
            textoOcr = textoOcr.Replace('\n', ' ');
            textoOcr = textoOcr.Replace("  ", " ");
            nombreAcreditado = nombreAcreditado.Replace('\n', ' ');
            nombreAcreditado = nombreAcreditado.Replace("  ", " ");
            double porcentaje = MaxSimilarInStringPercent(textoOcr, nombreAcreditado);


            //En caso de que el porcentaje sea menor a 85 se revisa nombre por nombre
            double porcentajeNombre = 0.0;
            bool esParcial = false;
            if (porcentaje < 90.0)
            {
                var nombres = nombreAcreditado.Split(' ');

                foreach (var nombre in nombres)
                {
                    var porcentajeNombreActual = MaxSimilarInStringPercent(textoOcr, nombre);
                    if (porcentajeNombreActual > 50 && porcentajeNombreActual < 90)
                        esParcial = true;
                    porcentajeNombre += porcentajeNombreActual;
                }

                porcentajeNombre = porcentajeNombre / nombres.GetLength(0);
                if (porcentajeNombre >= 90.0)
                    return EtiquetaOcr.CoincideCompletamente;

                if (porcentajeNombre >= 80.0 && esParcial)
                    return EtiquetaOcr.CoincideParcialmente;
            }

            if (porcentaje >= 90.0)
                return EtiquetaOcr.CoincideCompletamente;

            if (porcentaje >= 80.0)
                return EtiquetaOcr.CoincideParcialmente;

            return EtiquetaOcr.NoCoincideNoEsIdentificacion;
        }

        private static readonly object Mutex = new object();

        private double MaxSimilarInStringPercent(string text1, string text2)
        {
            string cadenaLarga;
            string cadenaBuscar;
            int busquedas = 0;
            if (text1.Length > text2.Length)
            {
                cadenaLarga = text1.ToLower();
                cadenaBuscar = text2.ToLower();
                busquedas = text1.Length - text2.Length;
            }
            else
            {
                cadenaLarga = text2.ToLower();
                cadenaBuscar = text1.ToLower();
                busquedas = text2.Length - text1.Length;
            }

            double porcentajeEncontrado = 0.0;

            //for (int i = 0; i < busquedas + 1; i++)
            Parallel.For(0, busquedas + 1, (i, loopState) =>
            {
                var chunk = cadenaLarga.Substring(i, cadenaBuscar.Length);
                var distancia = DamLev(chunk, cadenaBuscar);
                double porcentaje;
                if (distancia < 1)
                    porcentaje = 100.0;
                else if (distancia > cadenaBuscar.Length)
                    porcentaje = 0.0;
                else
                {
                    porcentaje = 100 - ((distancia * 100) / cadenaBuscar.Length);
                }

                lock (Mutex)
                {
                    if (porcentaje > porcentajeEncontrado)
                        porcentajeEncontrado = porcentaje;
                }

                if (Math.Abs(porcentajeEncontrado - 100.0) < 1)
                    loopState.Stop();
            });

            return porcentajeEncontrado;
        }

        /// <summary>
        // Computes and returns the Damerau-Levenshtein edit distance between two strings, 
        /// i.e. the number of insertion, deletion, sustitution, and transposition edits
        /// required to transform one string to the other. This value will be >= 0, where 0
        /// indicates identical strings. Comparisons are case sensitive, so for example, 
        /// "Fred" and "fred" will have a distance of 1. This algorithm is basically the
        /// Levenshtein algorithm with a modification that considers transposition of two
        /// adjacent characters as a single edit.
        /// http://blog.softwx.net/2015/01/optimizing-damerau-levenshtein_15.html
        /// </summary>
        /// <remarks>See http://en.wikipedia.org/wiki/Damerau%E2%80%93Levenshtein_distance
        /// Note that this is based on Sten Hjelmqvist's "Fast, memory efficient" algorithm, described
        /// at http://www.codeproject.com/Articles/13525/Fast-memory-efficient-Levenshtein-algorithm.
        /// This version differs by including some optimizations, and extending it to the Damerau-
        /// Levenshtein algorithm.
        /// Note that this is the simpler and faster optimal string alignment (aka restricted edit) distance
        /// that difers slightly from the classic Damerau-Levenshtein algorithm by imposing the restriction
        /// that no substring is edited more than once. So for example, "CA" to "ABC" has an edit distance
        /// of 2 by a complete application of Damerau-Levenshtein, but a distance of 3 by this method that
        /// uses the optimal string alignment algorithm. See wikipedia article for more detail on this
        /// distinction.
        /// </remarks>
        /// <param name="s">String being compared for distance.</param>
        /// <param name="t">String being compared against other string.</param>
        /// <returns>int edit distance, >= 0 representing the number of edits required
        /// to transform one string to the other.</returns>
        private int DamLev(string s, string t)
        {
            if (String.IsNullOrEmpty(s)) return (t ?? "").Length;
            if (String.IsNullOrEmpty(t)) return s.Length;

            // if strings of different lengths, ensure shorter string is in s. This can result in a little
            // faster speed by spending more time spinning just the inner loop during the main processing.
            if (s.Length > t.Length)
            {
                var temp = s;
                s = t;
                t = temp; // swap s and t
            }
            int sLen = s.Length; // this is also the minimun length of the two strings
            int tLen = t.Length;

            // suffix common to both strings can be ignored
            while ((sLen > 0) && (s[sLen - 1] == t[tLen - 1]))
            {
                sLen--;
                tLen--;
            }

            int start = 0;
            if ((s[0] == t[0]) || (sLen == 0))
            {
                // if there's a shared prefix, or all s matches t's suffix
                // prefix common to both strings can be ignored
                while ((start < sLen) && (s[start] == t[start])) start++;
                sLen -= start; // length of the part excluding common prefix and suffix
                tLen -= start;

                // if all of shorter string matches prefix and/or suffix of longer string, then
                // edit distance is just the delete of additional characters present in longer string
                if (sLen == 0) return tLen;

                t = t.Substring(start, tLen); // faster than t[start+j] in inner loop below
            }

            var v0 = new int[tLen];
            var v2 = new int[tLen]; // stores one level further back (offset by +1 position)
            for (int j = 0; j < tLen; j++) v0[j] = j + 1;

            char sChar = s[0];
            int current = 0;
            for (int i = 0; i < sLen; i++)
            {
                char prevsChar = sChar;
                sChar = s[start + i];
                char tChar = t[0];
                int left = i;
                current = i + 1;
                int nextTransCost = 0;
                for (int j = 0; j < tLen; j++)
                {
                    int above = current;
                    int thisTransCost = nextTransCost;
                    nextTransCost = v2[j];
                    v2[j] = current = left; // cost of diagonal (substitution)
                    left = v0[j]; // left now equals current cost (which will be diagonal at next iteration)
                    char prevtChar = tChar;
                    tChar = t[j];
                    if (sChar != tChar)
                    {
                        if (left < current) current = left; // insertion
                        if (above < current) current = above; // deletion
                        current++;
                        if ((i != 0) && (j != 0)
                            && (sChar == prevtChar)
                            && (prevsChar == tChar))
                        {
                            thisTransCost++;
                            if (thisTransCost < current) current = thisTransCost; // transposition
                        }
                    }
                    v0[j] = current;
                }
            }
            return current;
        }
    }

    public enum EtiquetaOcr
    {
        AceptadoFamiliar,
        CoincideCompletamente,
        CoincideParcialmente,
        NoCoincideNoEsIdentificacion
    }
}

