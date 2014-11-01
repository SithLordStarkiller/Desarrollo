﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Universidad.Entidades;

namespace Universidad.AccesoDatos.ControlUsuarios.LoginA
{
    public class LoginA
    {
        #region Propiedades de la clase
        
        private static readonly LoginA _classInstance = new LoginA();

        public static LoginA ClassInstance
        {
            get { return _classInstance; }
        }

        /// <summary>
        ///  Instancia hacia el contexto de la DB
        /// </summary>
        private readonly UniversidadBDEntities _contexto = new UniversidadBDEntities();

        public LoginA()
        {
        }

        #endregion

        #region Metodos de Insercion

        #endregion

        #region Metodos de Extraccion

        public US_USUARIOS LoginAdministrador(string Nombre, string Contrasena)
        {
            US_USUARIOS Usuario = null; 

            using (var Aux = new Repositorio<US_USUARIOS> ())
            {
                Usuario = Aux.Extraer(r => r.USUARIO == Nombre && r.CONTRASENA == Contrasena);
            }

            return Usuario;
        }

        public US_USUARIOS LoginAdministradoresTSQL(string Nombre, string Contrasena)
        {
            const string executesqlstr = "SELECT TOP 1 * FROM US_USUARIOS WHERE USUARIO = @Usuario AND CONTRASENA = @Contrasena";
            var resultado = new US_USUARIOS();

            try
            {
                var para = new SqlParameter[] { 
                    new SqlParameter("@Usuario",Nombre),
                    new SqlParameter("@Contrasena",Contrasena)                    
                };
                var obj = ControladorSQL.ExecuteDataTable(ParametrosSQL.strCon_DBLsWebApp, CommandType.Text, executesqlstr, para);



                if (obj != null)
                {
                    List<US_USUARIOS> lst = (from DataRow row in obj.Rows
                        select new US_USUARIOS()
                        {
                            NOMBRE_COMPLETO = (string)row["NOMBRE_COMPLETO"],
                            CONTRASENA = (string)row["NOMBRE_COMPLETO"],
                            ID_ESTATUS_USUARIOS = (int)row["ID_ESTATUS_USUARIOS"],
                            ID_USUARIO = (int)row["ID_USUARIO"],
                            ID_HISTORIAL = (int)row["ID_HISTORIAL"],
                            ID_NIVEL_USUARIO = (int)row["ID_NIVEL_USUARIO"],
                            ID_PERSONA = (int)row["ID_PERSONA"],
                            ID_PER_LINKID = (int)row["ID_PER_LINKID"],
                            ID_TIPO_USUARIO = (int)row["ID_TIPO_USUARIO"]
                               
                        }).ToList();

                    resultado = lst.FirstOrDefault();
                }
            }
            catch (SqlException ex)
            {
                
            }
            return resultado;
        }

        #endregion
    }
}
