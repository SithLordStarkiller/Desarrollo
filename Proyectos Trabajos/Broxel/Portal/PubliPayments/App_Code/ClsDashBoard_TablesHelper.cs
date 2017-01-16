/****************************************************************************************************************
* Desarrrollador: Israel Salinas Contreras
* Proyecto:	London-PubliPayments-Formiik
* Fecha de Creación: 29/04/2014
* Descripción de Creacion: Clase Generica para controlar enumeradores
* Ultima Fecha de Modificaciòn: 29/04/2014
* Descripciòn de ultima modificacion: ----
****************************************************************************************************************/

using System;
using System.Data;
using PubliPayments.App_Code;
using PubliPayments.Entidades;

namespace PubliPayments
{
    public class ClsDashBoardTablesHelper
    {
        #region "Variables"

        //Cadena de Conexion
        ClsConexionDB ObjConn = new ClsConexionDB();

        private String sStored_Master = "dbo.SpU_DashBoard_Handler";        
        private String StrQry = "";
        //private DataTable objDt = null;
        private int _nUser = -1;
        private int _nRol = -1;
        private int _nDominio = -1;

        //Datos del dashboard
        private String _sDelegacion = "%";
        private String _sEstado = "%";
        private String _sDespacho = "%";
        private String _sSupervisor = "%";
        private String _sGestor = "%";

        private String _sDominio = "";
        private String _sUser = "";  
        private String _sError_Message = "";

        #endregion

        #region "Propiedades"

        public int Usuario_Id { get { return _nUser; } set { _nUser = value; } }
        public int Usuario_Rol { get { return _nRol; } set { _nRol = value; } }
        public String Usuario_Name_Desc { get { return _sUser; } set { _sUser = value; } }
        public int Usuario_Dominio_Id { get { return _nDominio; } set { _nDominio = value; } }        
        public String Usuario_Dominio_Desc { get { return _sDominio; } set { _sDominio = value; } }

        //Propiedades para el dashboard
        public String Dash_Delegacion { get { return _sDelegacion; } set { _sDelegacion = value; } }
        public String Dash_Estado { get { return _sEstado; } set { _sEstado = value; } }
        public String Dash_Despacho { get { return _sDespacho; } set { _sDespacho = value; } }
        public String Dash_Supervisor { get { return _sSupervisor; } set { _sSupervisor = value; } }
        public String Dash_Gestor { get { return _sGestor; } set { _sGestor = value; } }

        public String Error_Message { get { return _sError_Message; } }

        #endregion

        #region "Constructores"

        public ClsDashBoardTablesHelper()
        {}

        public ClsDashBoardTablesHelper(int Usuario, int Rol, int DominioId, String DominioDesc, String UsuarioDesc)
        {
            _nUser = Usuario;
            _sUser = UsuarioDesc;
            _nRol = Rol;
            _nDominio = DominioId;
            _sDominio = DominioDesc;        
        }

        #endregion

        #region "Funciones"

        void bLlenaDashBoard()
        {            
        }

        public string bGetFechadeCorte()
        {
            String sValue = "";
            StrQry = String.Format(sStored_Master + " @Accion = 'Dash_Admin', @SubAccion = 'DiaDeCorte' ");
            sValue = Convert.ToString(ObjConn.ExecuteNonScalar(StrQry));
            return sValue;
        }

        public DataTable Source_GetIndicadores(filtroDashBoard enumDashBoard)
        {
            //DataTable Value = null;

            ConexionSql sql = ConexionSql.Instance;

            var resultado = sql.Source_GetIndicadores(_nUser, _nDominio, _nRol, _sUser, enumDashBoard.ToString(), _sDelegacion, _sEstado,
                _sDespacho, _sSupervisor, _sGestor);
            //StrQry = String.Format(sStored_Master + " @Accion = 'Get_Indicadores', @SubAccion = '',  " +
            //                       " @fi_Usuario = {0}, @fi_Dominio = {1}, @fi_Rol = {2}, @fc_Usuario = '{3}', @fc_DashBoard = '{4}', " +
            //                       " @fc_Delegacion = '{5}', @fc_Estado = '{6}', @fc_Despacho = '{7}',  @fc_idUsuarioPadre = '{8}', @fc_idUsuario = '{9}' ",
            //                       _nUser, _nDominio, _nRol, _sUser, enumDashBoard.ToString(),
            //                       _sDelegacion, _sEstado, _sDespacho, _sSupervisor, _sGestor);
            //Value = ObjConn.ExecuteNonDataTable(StrQry);

            if (resultado != null && resultado.Tables.Count > 0 && resultado.Tables[0].Rows.Count > 0)
                return resultado.Tables[0];
            else
                return null;
        }

        public bool Execute_CalcularIndicadores(filtroDashBoard enumDashBoard)
        {
            bool Value = false;
            StrQry = String.Format(sStored_Master + " @Accion = 'Calcular_Indicadores', @SubAccion = '',  " +
                                   " @fi_Usuario = {0}, @fi_Dominio = {1}, @fi_Rol = {2}, @fc_Usuario = '{3}', @fc_DashBoard = '{4}', " +
                                   " @fc_Delegacion = '{5}', @fc_Estado = '{6}', @fc_Despacho = '{7}',  @fc_idUsuarioPadre = '{8}', @fc_idUsuario = '{9}' ",
                                   _nUser, _nDominio, _nRol, _sUser, enumDashBoard.ToString(),
                                   _sDelegacion, _sEstado, _sDespacho, _sSupervisor, _sGestor);
            try
            {
                Value = true;
                ObjConn.ExecuteNonQuery(StrQry);
            }
            catch(System.Data.SqlClient.SqlException ex)
            {
                _sError_Message = ex.Message;
                Value = false;
            }            
            return Value;
        }
        
        public DataTable Source_Tabla_GestionDomiciliaria(filtroDashBoard enumDashBoard)
        {
            DataTable Value = null;
            StrQry = String.Format(sStored_Master + " @Accion = 'Dash_Admin', @SubAccion = 'StatusGestDom_Tabla',  " +
                                   " @fi_Usuario = {0}, @fi_Dominio = {1}, @fi_Rol = {2}, @fc_Usuario = '{3}', @fc_DashBoard = '{4}' ",
                                   _nUser, _nDominio, _nRol, _sUser, enumDashBoard.ToString());
            Value = ObjConn.ExecuteNonDataTable(StrQry);
            return Value;
        }

        #endregion

        #region "Metodos"


        #endregion
    }
}