using System;
using System.Data;
using System.Data.SqlClient;
using PubliPayments.Utiles;

namespace PubliPayments.Entidades
{
    public class EntDashboard
    {
        //private static object _lock = new object();

        public DataSet Dashboard(string idUsuario, string rol, int parte, string delegacion, string despacho,
            string supervisor, string gestor, string tipoFormulario, string callcenter, int idRol)
        {
            if (supervisor == null)
            {
                supervisor = "%";
            }
            var guidTiempos = Tiempos.Iniciar();
            var rs = new DataSet();

            var instancia = ConnectionDB.Instancia;

            var parametros = new SqlParameter[3];
            parametros[0] = new SqlParameter("@Parte", SqlDbType.Int) { Value = parte };
            parametros[1] = new SqlParameter("@Rol", SqlDbType.Int, 25) { Value = idRol };
            parametros[2] = new SqlParameter("@Ruta", SqlDbType.VarChar, 10) { Value = tipoFormulario };


            var ds43 = instancia.EjecutarDataSet("SqlDefault", "ObtenerIndicadoresDash", parametros);


            var tabla = new DataTable();
            tabla.Reset();
            tabla.Columns.Add("Descripcion");
            tabla.Columns.Add("Valor");
            tabla.Columns.Add("Porcentaje");
            tabla.Columns.Add("NombreDisplay");
            tabla.Columns.Add("Parte");

            foreach (DataRow fila in ds43.Tables[0].Rows)
            {
                var row = tabla.NewRow();
                row["Descripcion"] = fila["fc_Clave"].ToString();
                row["Valor"] = 0;
                row["Porcentaje"] = 0;
                row["NombreDisplay"] = "";
                row["Parte"] = fila["fi_Parte"].ToString();

                tabla.Rows.Add(row);
            }

            var rowDef = tabla.NewRow();
            rowDef["Descripcion"] = "DASH_CREDASIGPOOL";
            rowDef["Valor"] = 0;
            rowDef["Porcentaje"] = 0;
            rowDef["NombreDisplay"] = "";
            rowDef["Parte"] = "0";

            tabla.Rows.Add(rowDef);

            var cnn = instancia.IniciaConexion("SqlDefault");
            var totalPool = 0;
            try
            {
                var ds1 =
                    ObtenerIndicadorDashboard(cnn, idUsuario, "DASH_CREDASIGPOOL", delegacion, despacho, supervisor,
                        gestor, tipoFormulario, totalPool, callcenter).Split('|');
                var row = tabla.Select("Descripcion='" + "DASH_CREDASIGPOOL" + "'");
                row[0]["Valor"] = ds1[0];
                row[0]["Porcentaje"] = ds1[1] + "%";
                row[0]["NombreDisplay"] = ds1[2];
                totalPool = Convert.ToInt32(ds1[0]);
            }
                // ReSharper disable once UnusedVariable
            catch (Exception ex)
            {
                var row = tabla.Select("Descripcion='" + "DASH_CREDASIGPOOL" + "'");
                row[0]["Valor"] = "0";
                row[0]["Porcentaje"] = "-";
                row[0]["NombreDisplay"] = "ERROR";
            }


            //Parallel.For(0, ds43.Tables[0].Rows.Count, i =>
            //{
            //    var descripcion = ds43.Tables[0].Rows[i]["fc_Clave"].ToString();
            //    try
            //    {
            //        var ds1 = ObtenerIndicadorDashboard(cnn,idUsuario, descripcion, delegacion, despacho, supervisor, gestor, tipoFormulario,totalPool).Split('|');
            //        lock (_lock)
            //        {
            //            var row = tabla.Select("Descripcion='" + descripcion + "'");
            //            row[0]["Valor"] = ds1[0];
            //            row[0]["Porcentaje"] = ds1[1] + "%";
            //            row[0]["NombreDisplay"] = ds1[2];
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        lock (_lock)
            //        {
            //            var row = tabla.Select("Descripcion='" + descripcion + "'");
            //            row[0]["Valor"] = "0";
            //            row[0]["Porcentaje"] = "-";
            //            row[0]["NombreDisplay"] = "ERROR";
            //        }
            //    }
            //});



            for (var i = 0; i < ds43.Tables[0].Rows.Count; i++)
            {
                var descripcion = ds43.Tables[0].Rows[i]["fc_Clave"].ToString();
                try
                {
                    var ds1 =
                        ObtenerIndicadorDashboard(cnn, idUsuario, descripcion, delegacion, despacho, supervisor, gestor,
                            tipoFormulario, totalPool, callcenter).Split('|');
                    var row = tabla.Select("Descripcion='" + descripcion + "'");
                    row[0]["Valor"] = ds1[0];
                    row[0]["Porcentaje"] = ds1[1] + "%";
                    row[0]["NombreDisplay"] = ds1[2];
                }
                    // ReSharper disable once UnusedVariable
                catch (Exception ex)
                {
                    Logger.WriteLine(Logger.TipoTraceLog.Error, Convert.ToInt32(idUsuario), "EntDashboard",
                        "ObtenerIndicadorDashboard - " + descripcion + " - Error: " + ex.Message);
                    var row = tabla.Select("Descripcion='" + descripcion + "'");
                    row[0]["Valor"] = "0";
                    row[0]["Porcentaje"] = "-";
                    row[0]["NombreDisplay"] = "ERROR";
                }
            }
            cnn.Close();

            rs.Tables.Add(tabla);
            Tiempos.Terminar(guidTiempos);
            return rs;
        }

        private string ObtenerIndicadorDashboard(SqlConnection cnn, string idUsuario, string indicador,
            string delegacion, string despacho, string supervisor, string gestor, string tipoFormulario, int totalPool,
            string callcenter)
        {

            var parametros = new SqlParameter[9];
            parametros[0] = new SqlParameter("@indicador", SqlDbType.VarChar, 50) {Value = indicador};
            parametros[1] = new SqlParameter("@idUsuario", SqlDbType.Int) {Value = idUsuario};
            parametros[2] = new SqlParameter("@delegacion", SqlDbType.VarChar, 10) {Value = delegacion};
            parametros[3] = new SqlParameter("@despacho", SqlDbType.VarChar, 10) {Value = despacho};
            parametros[4] = new SqlParameter("@supervisor", SqlDbType.VarChar, 10) {Value = supervisor};
            parametros[5] = new SqlParameter("@gestor", SqlDbType.VarChar, 10) {Value = gestor};
            parametros[6] = new SqlParameter("@tipoFormulario", SqlDbType.VarChar, 10) {Value = tipoFormulario};
            parametros[7] = new SqlParameter("@contraPorcentaje", SqlDbType.Int) {Value = totalPool};
            parametros[8] = new SqlParameter("@callcenter", SqlDbType.VarChar, 6) {Value = callcenter};

            var sc = new SqlCommand("ObtenerIndDashMaster", cnn);
            sc.Parameters.AddRange(parametros);
            sc.CommandType = CommandType.StoredProcedure;
            sc.CommandTimeout = 2;
            var resultado = (string) sc.ExecuteScalar();


            //var sda = new SqlDataAdapter(sc);
            //var ds = new DataSet();
            //sda.Fill(ds);

            return resultado;
        }

        public DataTable ObtenerFiltros(string idUsuario, string delegacion, string despacho, string supervisor, string gestor, string idformulario)
        {
            var instancia = ConnectionDB.Instancia;

            var parametros = new SqlParameter[6];
            parametros[0] = new SqlParameter("@idUsuario", SqlDbType.VarChar, 10) {Value = idUsuario};
            parametros[1] = new SqlParameter("@delegacion", SqlDbType.VarChar, 100) {Value = delegacion};
            parametros[2] = new SqlParameter("@despacho", SqlDbType.VarChar, 100) {Value = despacho};
            parametros[3] = new SqlParameter("@supervisor", SqlDbType.VarChar, 100) {Value = supervisor};
            parametros[4] = new SqlParameter("@gestor", SqlDbType.VarChar, 100) {Value = gestor};
            parametros[5] = new SqlParameter("@idformulario", SqlDbType.VarChar, 3) { Value = idformulario };
            

            var ds = instancia.EjecutarDataSet("SqlDefault", "ObtenerFiltros", parametros);

            return ds.Tables[0];
        }
    }
}
