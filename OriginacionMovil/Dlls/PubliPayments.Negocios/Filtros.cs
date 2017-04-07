using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PubliPayments.Entidades;

namespace PubliPayments.Negocios
{
    public class Filtros
    {
        /// <summary>
        /// Inserta el filtro que se esta aplicando para este usuario y elemento que se esta utilizando
        /// </summary>
        /// <param name="idUsuario">usuario del sistema</param>
        /// <param name="filtro">Nombre del filtro que se esta aplicando</param>
        /// <param name="valor">Nuevo valor que se tiene del filtro</param>
        public void InsertaFiltro(int idUsuario, string filtro, string valor)
        {
            var entFiltro = new EntFiltros();
            entFiltro.InsertaFiltro(idUsuario,filtro,valor);
        }

        /// <summary>
        /// Obtiene el valor de un filtro que se tiene relacionado para el usuario
        /// </summary>
        /// <param name="idUsuario">usuario del sistema</param>
        /// <param name="filtro">Nombre del filtro que se esta consultando</param>
        /// <returns>Valor que se tiene guardado para ese filtro</returns>
        public string ObtenerFiltros(int idUsuario, string filtro)
        {
            var entFiltro = new EntFiltros();
            return  entFiltro.ObtenerFiltros(idUsuario, filtro);
        }

    }
}
