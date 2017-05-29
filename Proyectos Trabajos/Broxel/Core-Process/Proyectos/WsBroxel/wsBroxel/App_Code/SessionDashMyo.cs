using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace wsBroxel.App_Code
{
    public class SessionDashMyo
    {
        public string CrearSessionDash(int idUser, int idAplication)
        {
            var db = new broxelco_rdgEntities();
            String session = string.Empty;
          //  var usuarios = db.UsuariosOnlineBroxel.Where(x => x.Usuario.ToLower() == user.ToLower()).ToList();

            //if(usuarios.Count == 1)
            //{
               // var usuario = usuarios[0];

                session_dash sessionData = new session_dash
                {
                    idUser = idUser,
                    vigencia = DateTime.Now.AddMinutes(20),
                    entrada = DateTime.Now,
                    idAplicacion = idAplication //Inter
                };

                db.session_dash.Add(sessionData);

                try
                {
                    db.SaveChanges();
                }
                catch {
                    Trace.WriteLine("error");
                }

                session = GeneraSesion(idUser, sessionData.idsession_dash);
             
           // }

            return session;

        }

        private string GeneraSesion(int idUsuario, int idSesion)
        {
            return Helper.ToBase64(idSesion + "|" + idUsuario + "|" + DateTime.Now.ToString("dd/M/yyyy HH:mm:ss"));
        }

    }
}