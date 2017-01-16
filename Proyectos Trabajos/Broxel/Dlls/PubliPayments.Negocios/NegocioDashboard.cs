using System.Data;
using PubliPayments.Entidades;

namespace PubliPayments.Negocios
{
    public class NegocioDashboard
    {

        public DataTable ObtenerIndiciadoresDashboard(string idUsuario, string rol, int parte, string delegacion,
            string despacho, string supervisor, string gestor, string tipoFormulario, string callcenter, int idRol)
        {
            var ent = new EntDashboard();
            DataSet datosInd =
                ent.Dashboard(idUsuario, rol, parte, delegacion, despacho, supervisor, gestor, tipoFormulario,
                    callcenter, idRol);

            if (datosInd != null && datosInd.Tables.Count > 0)
            {
                return datosInd.Tables[0];
            }

            return new DataTable();
        }
    }
}
