/****************************************************************************************************************
* Desarrrollador: Israel Salinas Contreras
* Proyecto:	London-PubliPayments-Formiik
* Fecha de Creación: 29/04/2014
* Descripción de Creacion: Clase para conexion a la base de datos
* Ultima Fecha de Modificaciòn: 29/04/2014
* Descripciòn de ultima modificacion: ----
****************************************************************************************************************/
using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace PubliPayments.App_Code
{
    public class ClsConexionDB
    {
        public String StrConnectionString = ConfigurationManager.ConnectionStrings["SqlDefault"].ToString();
        public SqlConnection ObjConexion = new SqlConnection();
        public SqlDataAdapter ObjAdapter;
        public SqlCommand ObjComando;
        public String sGlobal_Error = "";

        public ClsConexionDB()
        {
            ObjConexion.ConnectionString = StrConnectionString;
        }
        ~ClsConexionDB()
        {
            Dispose(false);
        }

        public ClsConexionDB(String sCadenaConexion)
        {
            ObjConexion.ConnectionString = sCadenaConexion;
        }

        public bool ExecuteNonQuery(String StrQuery)
        {
            sGlobal_Error = "";
            bool bRetorno = false;

            try
            {
                if (ObjConexion.State == ConnectionState.Closed)
                    ObjConexion.Open();

                ObjComando = new SqlCommand();
                ObjComando.Connection = ObjConexion;
                ObjComando.CommandText = StrQuery;
                ObjComando.ExecuteNonQuery();

                if (ObjConexion.State == ConnectionState.Open)
                    ObjConexion.Close();

                bRetorno = true;
            }
            catch (SqlException Exsql)
            {
                sGlobal_Error = Exsql.Message;
                bRetorno = false;

            }
            catch (Exception Ex)
            {
                sGlobal_Error = Ex.Message;
                bRetorno = false;

            }

            return bRetorno;
        }


        public object ExecuteNonScalar(String StrQuery)
        {
            sGlobal_Error = "";
            object bRetorno = null;

            try
            {
                if (ObjConexion.State == ConnectionState.Closed)
                    ObjConexion.Open();

                ObjComando = new SqlCommand();
                ObjComando.Connection = ObjConexion;
                ObjComando.CommandText = StrQuery;
                bRetorno = ObjComando.ExecuteScalar();

                if (ObjConexion.State == ConnectionState.Open)
                    ObjConexion.Close();
            }
            catch (SqlException Exsql)
            {
                sGlobal_Error = Exsql.Message;
                bRetorno = false;

            }
            catch (Exception Ex)
            {
                sGlobal_Error = Ex.Message;
                bRetorno = null;
            }

            return bRetorno;
        }

        public DataTable ExecuteNonDataTable(String StrQuery)
        {
            sGlobal_Error = "";
            DataTable ObjTabla = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                if (ObjConexion.State == ConnectionState.Closed)
                    ObjConexion.Open();

                ObjAdapter = new SqlDataAdapter(StrQuery, ObjConexion);
                ObjAdapter.SelectCommand.CommandText = StrQuery;
                ObjAdapter.Fill(ds);

                if (ObjConexion.State == ConnectionState.Open)
                    ObjConexion.Close();
            }
            catch (SqlException Exsql)
            {
                sGlobal_Error = Exsql.Message;
                ObjTabla = null;
            }
            catch (Exception Ex)
            {
                sGlobal_Error = Ex.Message;
                ObjTabla = null;
            }

            if (ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            else
                return ObjTabla;
        }


        public bool FillDropDownList(String StrQuery, String sDisplayMember, String sValueMember, ref System.Web.UI.WebControls.DropDownList objList, bool limpiar)
        {
            bool bRetorno = false;
            DataTable TablaDb = null;
            try
            {
                if (!limpiar)
                {
                    TablaDb = this.ExecuteNonDataTable(StrQuery);
                    if (TablaDb.Rows.Count == 0)
                    {
                        bRetorno = false;
                    }
                    else
                    {
                        objList.DataSource = TablaDb;
                        objList.DataTextField = sDisplayMember;
                        objList.DataValueField = sValueMember;
                    }   
                }
                objList.Items.Add(new System.Web.UI.WebControls.ListItem("Todas", "%"));

                bRetorno = true;
            }
            catch
            {
                bRetorno = false;
            }

            return bRetorno;
        }

        public bool Template(String StrQuery)
        {
            bool bRetorno = false;

            try
            {
                bRetorno = true;
            }
            catch
            {
                bRetorno = false;
            }

            return bRetorno;
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool bDisposing)
        {
            try
            {
                if (bDisposing)
                {
                    ObjComando.Dispose();
                    ObjAdapter.Dispose();
                    if (ObjConexion.State == ConnectionState.Open)
                        ObjConexion.Close();
                    ObjConexion.Dispose();
                }
            }
            catch
            {
            }
        }

    }
}