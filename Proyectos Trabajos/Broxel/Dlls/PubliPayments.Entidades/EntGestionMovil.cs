using System;
using System.Data;
using System.Data.SqlClient;


namespace PubliPayments.Entidades
{
    public class EntGestionMovil
    {
        #region Datos Extra

        public string ObtenerFechaActualizacion()
        {
            const string sql = "select Valor from CatalogoGeneral where id=2";
            var conexion = ConexionSql.Instance;
            var cnn = conexion.IniciaConexion();
            var sc = new SqlCommand(sql, cnn);
            var sda = new SqlDataAdapter(sc);
            var ds = new DataSet();
            sda.Fill(ds);
            conexion.CierraConexion(cnn);
            return ds.Tables[0].Rows[0][0].ToString();
        }
        #endregion


        #region Filtros
        public DataSet ObtenerEstFinalFiltro()
        {
            var ds = new DataSet();

            const string sql = "select distinct rgm.estFinal as Value,isnull(cdr.Valor,'Sin Dictamen') as Description from ReporteGestionMovil rgm left join CatDictamenRespuesta cdr on cdr.idCampo=rgm.estFinal order by 2";
            var conexion = ConexionSql.Instance;
            var cnn = conexion.IniciaConexion();
            var sc = new SqlCommand(sql, cnn);
            var sda = new SqlDataAdapter(sc);
            var ds1 = new DataSet();
            sda.Fill(ds1);
            conexion.CierraConexion(cnn);

            var dt=new DataTable();
            dt.Columns.Add(new DataColumn("Value", typeof(string)));
            dt.Columns.Add(new DataColumn("Description", typeof(string)));

            for(var i=0;i<ds1.Tables[0].Rows.Count;)
            {
                var dr = dt.NewRow();
                var estfinal = "";
                
                var descripActual = ds1.Tables[0].Rows[i][1].ToString();
                estfinal += ds1.Tables[0].Rows[i][0].ToString();
                i++;
                var iActual = i;
                while (i < ds1.Tables[0].Rows.Count && descripActual == ds1.Tables[0].Rows[i][1].ToString())
                {
                    estfinal += (","+ds1.Tables[0].Rows[i][0]);
                    i++;
                }

                dr["Value"] = estfinal;
                dr["Description"] = descripActual;

                dt.Rows.Add(dr);
            }

            ds.Tables.Add(dt);

            return ds;
        }

        public DataSet ObtenerDiaGestionFiltro()
        {
            const string sql = "select distinct(isnull(cast(DiaFinalDate as varchar(20)),'null')) as Value, DiaFinalDate," +
                               " isnull(cast(DiaFinalDate as varchar(20)),'Sin fecha de gestion') as Description from ReporteGestionMovil WITH (NOLOCK)" +
                               " order by DiaFinalDate";
            var conexion = ConexionSql.Instance;
            var cnn = conexion.IniciaConexion();
            var sc = new SqlCommand(sql, cnn);
            var sda = new SqlDataAdapter(sc);
            var ds = new DataSet();
            sda.Fill(ds);
            conexion.CierraConexion(cnn);
            return ds;
        }

        public DataSet ObtenerHoraGestionFiltro()
        {
            const string sql = "select distinct(isnull(cast(horaFinalDate as varchar(20)),'null')) as Value, horaFinalDate," +
                               " isnull(cast(horaFinalDate as varchar(20)),'Sin fecha de gestion') as Description from ReporteGestionMovil WITH (NOLOCK)" +
                               " order by horaFinalDate";
            var conexion = ConexionSql.Instance;
            var cnn = conexion.IniciaConexion();
            var sc = new SqlCommand(sql, cnn);
            var sda = new SqlDataAdapter(sc);
            var ds = new DataSet();
            sda.Fill(ds);
            conexion.CierraConexion(cnn);
            return ds;
        }

        public DataSet ObtenerFechaCargaFiltro()
        {
            const string sql = "select distinct(convert(varchar(10),fechaCarga,103)+' ' +convert(varchar(5), fechaCarga,108)) as Value" +
                               ",convert(varchar(10),fechaCarga,103)+' ' +convert(varchar(5), fechaCarga,108)  as Description" +
                               " from ReporteGestionMovil WITH (NOLOCK) where fechaCarga is not null";
            var conexion = ConexionSql.Instance;
            var cnn = conexion.IniciaConexion();
            var sc = new SqlCommand(sql, cnn);
            var sda = new SqlDataAdapter(sc);
            var ds = new DataSet();
            sda.Fill(ds);
            conexion.CierraConexion(cnn);
            return ds;
        }

        public DataSet ObtenerDelegacionesFiltro()
        {
            const string sql = "select Delegacion as Value,Descripcion as Description from CatDelegaciones WITH (NOLOCK)";
            var conexion = ConexionSql.Instance;
            var cnn = conexion.IniciaConexion();
            var sc = new SqlCommand(sql, cnn);
            var sda = new SqlDataAdapter(sc);
            var ds = new DataSet();
            sda.Fill(ds);
            conexion.CierraConexion(cnn);
            return ds;
        }

        #endregion

        #region Gestion X Hora
        public DataSet GestionXHoraMaster(string delegacion = "", string fechaCarga = "", string resFinal = "", string diaGestion = "", string tipoFormulario="")
        {
            var sql ="exec ReporteGestionMovil_GestionXHora "+
		                "@delegacion = N'"+delegacion+"', "+
		                "@fechaCarga = N'"+fechaCarga+"', "+
		                "@resFinal = N'"+resFinal+"', "+
		                "@diaGestion = N'"+diaGestion+"', "+
		                "@despacho = N'', "+
		                "@supervisor = N'', "+
		                "@tipoFormulario = N'"+tipoFormulario+"', "+
		                "@tipoConsulta =   1";
            var conexion = ConexionSql.Instance;
            var cnn = conexion.IniciaConexion();
            var sc = new SqlCommand(sql, cnn);
            var sda = new SqlDataAdapter(sc);
            var ds = new DataSet();
            sda.Fill(ds);
            conexion.CierraConexion(cnn);
            
            return ds;
        }

        public DataSet GestionXHoraDespacho(string delegacion = "", string fechaCarga = "", string resFinal = "", string diaGestion = "", string despacho = "", string tipoFormulario="")
        {
            var sql = "exec ReporteGestionMovil_GestionXHora " +
                        "@delegacion = N'" + delegacion + "', " +
                        "@fechaCarga = N'" + fechaCarga + "', " +
                        "@resFinal = N'" + resFinal + "', " +
                        "@diaGestion = N'" + diaGestion + "', " +
                        "@despacho = N'"+despacho+"', " +
                        "@supervisor = N'', " +
                        "@tipoFormulario = N'" + tipoFormulario + "', " +
                        "@tipoConsulta =   2";
            var conexion = ConexionSql.Instance;
            var cnn = conexion.IniciaConexion();
            var sc = new SqlCommand(sql, cnn);
            var sda = new SqlDataAdapter(sc);
            var ds = new DataSet();
            sda.Fill(ds);
            conexion.CierraConexion(cnn);
            return ds;
        }

        public DataSet GestionXHoraSupervisor(string delegacion = "", string fechaCarga = "", string resFinal = "", string diaGestion = "", string despacho = "", string supervisor = "", string tipoFormulario="")
        {
            var sql = "exec ReporteGestionMovil_GestionXHora " +
                        "@delegacion = N'" + delegacion + "', " +
                        "@fechaCarga = N'" + fechaCarga + "', " +
                        "@resFinal = N'" + resFinal + "', " +
                        "@diaGestion = N'" + diaGestion + "', " +
                        "@despacho = N'" + despacho + "', " +
                        "@supervisor = N'"+supervisor+"', " +
                        "@tipoFormulario = N'" + tipoFormulario + "', " +
                        "@tipoConsulta =   3";
            var conexion = ConexionSql.Instance;
            var cnn = conexion.IniciaConexion();
            var sc = new SqlCommand(sql, cnn);
            var sda = new SqlDataAdapter(sc);
            var ds = new DataSet();
            sda.Fill(ds);
            conexion.CierraConexion(cnn);
            return ds;
        }

        #endregion

        #region Gestion X Dia
        public DataSet GestionXDiaMaster(string delegacion = "", string fechaCarga = "", string resFinal = "", string horaGestion = "", string tipoFormulario="")
        {
            var sql = "exec ReporteGestionMovil_GestionXDia " +
                        "@delegacion = N'" + delegacion + "', " +
                        "@fechaCarga = N'" + fechaCarga + "', " +
                        "@resFinal = N'" + resFinal + "', " +
                        "@horaFinalDate = N'" + horaGestion + "', " +
                        "@despacho = N'', " +
                        "@supervisor = N'', " +
                        "@tipoFormulario = N'" + tipoFormulario + "', " +
                        "@tipoConsulta =   1";
            var conexion = ConexionSql.Instance;
            var cnn = conexion.IniciaConexion();
            var sc = new SqlCommand(sql, cnn);
            var sda = new SqlDataAdapter(sc);
            var ds = new DataSet();
            sda.Fill(ds);
            conexion.CierraConexion(cnn);
            return ds;
        }

        public DataSet GestionXDiaDespacho(string delegacion = "", string fechaCarga = "", string resFinal = "", string horaGestion = "", string despacho = "", string tipoFormulario="")
        {
            var sql = "exec ReporteGestionMovil_GestionXDia " +
                        "@delegacion = N'" + delegacion + "', " +
                        "@fechaCarga = N'" + fechaCarga + "', " +
                        "@resFinal = N'" + resFinal + "', " +
                        "@horaFinalDate = N'" + horaGestion + "', " +
                        "@despacho = N'"+despacho+"', " +
                        "@supervisor = N'', " +
                        "@tipoFormulario = N'" + tipoFormulario + "', " +
                        "@tipoConsulta =   2";
            var conexion = ConexionSql.Instance;
            var cnn = conexion.IniciaConexion();
            var sc = new SqlCommand(sql, cnn);
            var sda = new SqlDataAdapter(sc);
            var ds = new DataSet();
            sda.Fill(ds);
            conexion.CierraConexion(cnn);
            return ds;
        }

        public DataSet GestionXDiaSupervisor(string delegacion = "", string fechaCarga = "", string resFinal = "", string horaGestion = "", string despacho = "", string supervisor = "", string tipoFormulario="")
        {
            var sql = "exec ReporteGestionMovil_GestionXDia " +
                        "@delegacion = N'" + delegacion + "', " +
                        "@fechaCarga = N'" + fechaCarga + "', " +
                        "@resFinal = N'" + resFinal + "', " +
                        "@horaFinalDate = N'" + horaGestion + "', " +
                        "@despacho = N'"+despacho+"', " +
                        "@supervisor = N'"+supervisor+"', " +
                        "@tipoFormulario = N'" + tipoFormulario + "', " +
                        "@tipoConsulta =   3";
            var conexion = ConexionSql.Instance;
            var cnn = conexion.IniciaConexion();
            var sc = new SqlCommand(sql, cnn);
            var sda = new SqlDataAdapter(sc);
            var ds = new DataSet();
            sda.Fill(ds);
            conexion.CierraConexion(cnn);
            return ds;
        }

        #endregion

        #region Soluciones
        public DataSet Soluciones(string delegacion = "", string fechaCarga = "", string despacho = "", string supervisor = "", string tipoFormulario="")
        {
            var ds = new DataSet();

            var sql = "exec ReporteGestionMovil_Soluciones " +
                      "@delegacion = N'"+delegacion+"', " +
                      "@fechaCarga = N'"+fechaCarga+"',  " +
                      "@despacho = N'"+despacho+"', " +
                      "@supervisor = N'"+supervisor+"', " +
                      "@tipoFormulario = N'"+tipoFormulario+"'"; 
            var conexion = ConexionSql.Instance;
            var cnn = conexion.IniciaConexion();
            var sc = new SqlCommand(sql, cnn);
            var sda = new SqlDataAdapter(sc);
            var ds1 = new DataSet();
            sda.Fill(ds1);
            conexion.CierraConexion(cnn);

            var dt = new DataTable();
            dt.TableName = "Table";
            dt.Columns.Add(new DataColumn("Valor", typeof(string)));
            dt.Columns.Add(new DataColumn("valor32", typeof(string)));
            dt.Columns.Add(new DataColumn("valor0", typeof(string)));
            dt.Columns.Add(new DataColumn("valor1", typeof(string)));
            dt.Columns.Add(new DataColumn("valor2", typeof(string)));
            dt.Columns.Add(new DataColumn("valor3", typeof(string)));
            dt.Columns.Add(new DataColumn("valor4", typeof(string)));
            dt.Columns.Add(new DataColumn("valor5", typeof(string)));
            dt.Columns.Add(new DataColumn("valor6", typeof(string)));
            

            for (var i = 0; i < ds1.Tables[0].Rows.Count; )
            {
                var dr = dt.NewRow();
               
                var descripActual = ds1.Tables[0].Rows[i][1].ToString();
                var val32 =Convert.ToInt32(ds1.Tables[0].Rows[i][2].ToString());
                var val0 = Convert.ToInt32(ds1.Tables[0].Rows[i][3].ToString());
                var val1 = Convert.ToInt32(ds1.Tables[0].Rows[i][4].ToString());
                var val2 = Convert.ToInt32(ds1.Tables[0].Rows[i][5].ToString());
                var val3 = Convert.ToInt32(ds1.Tables[0].Rows[i][6].ToString());
                var val4 = Convert.ToInt32(ds1.Tables[0].Rows[i][7].ToString());
                var val5 = Convert.ToInt32(ds1.Tables[0].Rows[i][8].ToString());
                var val6 = Convert.ToInt32(ds1.Tables[0].Rows[i][9].ToString());

                i++;
                
                while (i < ds1.Tables[0].Rows.Count && descripActual == ds1.Tables[0].Rows[i][1].ToString())
                {
                     val32 += Convert.ToInt32(ds1.Tables[0].Rows[i][2].ToString());
                     val0 += Convert.ToInt32(ds1.Tables[0].Rows[i][3].ToString());
                     val1 += Convert.ToInt32(ds1.Tables[0].Rows[i][4].ToString());
                     val2 += Convert.ToInt32(ds1.Tables[0].Rows[i][5].ToString());
                     val3 += Convert.ToInt32(ds1.Tables[0].Rows[i][6].ToString());
                     val4 += Convert.ToInt32(ds1.Tables[0].Rows[i][7].ToString());
                     val5 += Convert.ToInt32(ds1.Tables[0].Rows[i][8].ToString());
                     val6 += Convert.ToInt32(ds1.Tables[0].Rows[i][9].ToString());
                    i++;
                }

                dr["Valor"] = descripActual;
                dr["valor32"] = val32;
                dr["valor0"] = val0;
                dr["valor1"] = val1;
                dr["valor2"] = val2;
                dr["valor3"] = val3;
                dr["valor4"] = val4;
                dr["valor5"] = val5;
                dr["valor6"] = val6;

                dt.Rows.Add(dr);
            }

            var tabCompute = ds1.Tables[1].Copy();

            ds.Tables.Add(dt);
            ds.Tables.Add(tabCompute);

            return ds;
            
        }

        #endregion

        #region GestionXSolucion
      
        public DataSet ReporteGestionMovilGestionXSolucion(string delegacion, string fechaCarga, string estFinal, string despacho, string supervisor, string tipoFormulario, int tipoConsulta)
        {
            var instancia = ConnectionDB.Instancia;
            var parametros = new SqlParameter[7];
            parametros[0] = new SqlParameter("@delegacion", SqlDbType.VarChar,5) { Value = delegacion };
            parametros[1] = new SqlParameter("@fechaCarga", SqlDbType.VarChar,50) { Value = fechaCarga };
            parametros[2] = new SqlParameter("@estFinal", SqlDbType.VarChar,500) { Value = estFinal };
            parametros[3] = new SqlParameter("@despacho", SqlDbType.VarChar,5) { Value = despacho };
            parametros[4] = new SqlParameter("@supervisor", SqlDbType.VarChar,5) { Value = supervisor };
            parametros[5] = new SqlParameter("@tipoFormulario", SqlDbType.VarChar,10) { Value = tipoFormulario };
            parametros[6] = new SqlParameter("@tipoConsulta", SqlDbType.Int) { Value = tipoConsulta };

            return instancia.EjecutarDataSet("SqlDefault", "ReporteGestionMovil_GestionXSolucion", parametros);
        }

        #endregion

        #region Pagos

        /// <summary>
        /// Obtiene el reporte de los pagos que se han realizado
        /// </summary>
        /// <param name="idDominio">Filtro para dominio op=0</param>
        /// <param name="ruta">Filtro para la ruta op=''</param>
        /// <param name="delegacion">Filtro para delegación op=''</param>
        /// <param name="proceso">Proceso a ejecutar 1=Despachos, 2=Despachos & Delegación </param>
        /// <returns></returns>
        public DataSet ReporteGestionMovil_Pagos(int idDominio, string ruta, string delegacion, int proceso)
        {
            var instancia = ConnectionDB.Instancia;
            var parametros = new SqlParameter[4];
            parametros[0] = new SqlParameter("@idDominio", SqlDbType.Int) { Value = idDominio };
            parametros[1] = new SqlParameter("@Ruta", SqlDbType.VarChar, 10) { Value = ruta };
            parametros[2] = new SqlParameter("@Delegacion", SqlDbType.VarChar, 2) { Value = delegacion };
            parametros[3] = new SqlParameter("@Proceso", SqlDbType.Int) { Value = proceso };

            return instancia.EjecutarDataSet("SqlDefault", "ReporteGestionMovil_Pagos", parametros);
        }
        #endregion

        #region Llamadas sin éxito

        public DataSet ReporteGestionMovil_LlamadasNoExito(int idproceso, int idDominio)
        {
            var instancia = ConnectionDB.Instancia;
            var parametros = new SqlParameter[2];
            parametros[0] = new SqlParameter("@idproceso", SqlDbType.Int) { Value = idproceso };
            parametros[1] = new SqlParameter("@idDominio", SqlDbType.Int) { Value = idDominio };
            return instancia.EjecutarDataSet("SqlDefault", "ReporteLlamadasNoExito",parametros);
        }
        
        #endregion

    }
}
