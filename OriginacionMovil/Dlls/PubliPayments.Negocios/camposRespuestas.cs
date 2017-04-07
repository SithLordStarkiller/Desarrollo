using System.Collections.Generic;
using System.Data;
using PubliPayments.Entidades;

namespace PubliPayments.Negocios
{
    public class CamposRespuestas
    {
        public List<CamposRespuestaModel> ObtenerDatos()
        {
            var lista = new EntCamposRespuesta().ObtenerCamposRespuestaEtiqueta().Tables[0].AsEnumerable();
            var listaCampos = new List<CamposRespuestaModel>();
            foreach (var campo in lista)
            {
                var cam = new CamposRespuestaModel
                {
                    Nombre = campo[1].ToString(),
                    Etiqueta = campo[2].ToString(),
                    IdCampo = int.Parse(campo[0].ToString())
                };
                listaCampos.Add(cam);
            }

            return listaCampos;
        }

        public bool GuardarRespuestas(CamposRespuestaModel modelo)
        {
            var res = new EntCamposRespuesta().GuardarCamposRespuesta(modelo.Nombre, modelo.Etiqueta, modelo.IdCampo);
            return res;
        }
    }
}
