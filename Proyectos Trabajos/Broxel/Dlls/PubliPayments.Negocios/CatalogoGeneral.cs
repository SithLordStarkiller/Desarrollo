using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PubliPayments.Entidades;

namespace PubliPayments.Negocios
{
    public class CatalogoGeneral
    {

        /// <summary>
        /// Inserta o actualiza un campo en el catalogo general
        /// </summary>
        /// <param name="modelo">Modelo que contiene la informacion, obligatorios la llave y el valor</param>
        /// <returns>Modelo con el id del registro modificado o creado resultado de la operacion</returns>
        public CatalogoGeneralModel InsUpdCatalogoGeneral(CatalogoGeneralModel modelo)
        {
            var resultado=new EntCatalogoGeneral().InsUpdCatalogoGeneral(modelo.Llave, modelo.Valor, modelo.Descripcion);
            return new CatalogoGeneralModel() { id = Convert.ToInt32(resultado) };
                
        }
        /// <summary>
        /// Obtiene los Datos del registro econtrado en CataloGeneral
        /// </summary>
        /// <param name="modelo">Modelo con infomacion a buscar, llave o id</param>
        /// <returns>Modelo con la infomacion relacionada</returns>
        public CatalogoGeneralModel ObtenerDatosCatalogoGeneral(CatalogoGeneralModel modelo)
        {
            var resultado = new EntCatalogoGeneral().ObtenerDatosCatalogoGeneral(modelo.id, modelo.Llave);

            if (resultado.Tables.Count>0 && resultado.Tables[0].Rows.Count>0)
            {
                modelo.id = Convert.ToInt32(resultado.Tables[0].Rows[0]["id"].ToString());
                modelo.Valor = Convert.ToString(resultado.Tables[0].Rows[0]["Valor"]);
                modelo.Llave = Convert.ToString(resultado.Tables[0].Rows[0]["llave"]);
                modelo.Descripcion = Convert.ToString(resultado.Tables[0].Rows[0]["Descripcion"]);
            }
            return modelo;
        }
    }
}
