using System;
using System.Globalization;
using IBC.buroCredito;

namespace IBC.AppCode
{
    public class ConsultasBc
    {
        public bool ConsultaBuro(string apaterno, string amaterno, string nombres, string rfc, string calle,
            string numeroExt, string numeroInt, string colonia, string delegacion, string estado, string cp, ref string result)
        {
            var ret = false;
            var bl = new BcbLogic();
            int idConsultaBc = 0;
            try
            {
                
                var proxy = new WSConsultaDelegateClient();
                var person = new PersonaBC {Encabezado = bl.GetEncabezadoBc(1, 1)};
                if (person.Encabezado == null)
                {
                    result = "Los datos de configuración de la consulta no existen";
                    return false;
                }
                var nombre = new NombreBC
                {
                    ApellidoPaterno = apaterno.ToUpper(),
                    ApellidoMaterno = amaterno.ToUpper(),
                    PrimerNombre = bl.GetPrimerNombre(nombres).ToUpper(),
                    SegundoNombre = bl.GetSegundoNombre(nombres).ToUpper(),
                    RFC = rfc
                };
                person.Nombre = nombre;

                var direcciones = new Direccion[1];

                var varDir = "";

                direcciones[0] = bl.GetDireccion(calle, numeroExt, numeroInt, colonia, delegacion, estado, cp,
                    ref varDir);

                if (direcciones[0] == null)
                {
                    result = varDir;
                    return false;
                }

                person.Domicilios = direcciones;
                var consulta = new IBC.buroCredito.ConsultaBC
                {
                    Personas = new PersonasBC
                    {
                        Persona = person
                    }
                };

                var validaRet = false;
                if (bl.ValidaConsultaBc(apaterno, amaterno, bl.GetPrimerNombre(nombres).ToUpper(), bl.GetSegundoNombre(nombres).ToUpper(), rfc, calle, numeroExt, numeroInt, colonia, delegacion, estado, cp,ref validaRet))
                {
                    if (!validaRet)
                        result = "Cuenta con atraso de más de 12 meses o deuda sin recuperar";
                    return validaRet;
                }

                idConsultaBc = bl.InsertaConsultaBc(apaterno, amaterno, bl.GetPrimerNombre(nombres).ToUpper(), bl.GetSegundoNombre(nombres).ToUpper(), rfc, calle, numeroExt, numeroInt, colonia, delegacion, estado, cp,consulta.Personas.Persona.Domicilios[0]);
                var sConsec = idConsultaBc.ToString(CultureInfo.InvariantCulture).PadLeft(25, ' ');
                consulta.Personas.Persona.Encabezado.NumeroReferenciaOperador = sConsec;
                var response = proxy.consultaXML(consulta);
                if (response != null)
                {
                    if (idConsultaBc != 0)
                    {
                        bl.PersisteResponse(response,idConsultaBc);    
                    }
                    ret = bl.AnalizaReponse(response, ref result);
                }

            }
            catch(Exception e)
            {
                ret = false;
                if (idConsultaBc != 0)
                {
                    var msg = "Error al consultar buro de crédito en ConsultaBuro:" + e.Message;
                    if (e.InnerException != null)
                        msg = msg + e.InnerException;
                    bl.InsertBcException(msg,idConsultaBc);
                }
            }
            if (!ret)
            {
                //Codigo de bypass
                var byPass = bl.GetParametro(1);
                if (byPass == "1")
                    ret = true;
            }
            return ret;
        }
    }
}
