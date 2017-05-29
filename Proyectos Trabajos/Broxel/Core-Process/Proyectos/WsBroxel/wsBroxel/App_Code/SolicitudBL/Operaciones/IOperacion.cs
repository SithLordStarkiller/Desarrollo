using wsBroxel.App_Code.SolicitudBL.Model;

namespace wsBroxel.App_Code.SolicitudBL.Operaciones
{
    interface IOperacion
    {
        DispResponse Execute(OperArguments oper, MySqlDataAccess mySql);
    }
}
