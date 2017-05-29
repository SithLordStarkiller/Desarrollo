using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace wsBroxel
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdateOnlineBroxel
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idUsuarioOnline"></param>
        /// <param name="nombreCompleto"></param>
        /// <param name="genero"></param>
        /// <returns></returns>
        public OnlineBroxelResponse UpdateInfoOnlineBroxel(int idUsuarioOnline, string nombreCompleto, string genero)
        {
            var broxelCo = new broxelco_rdgEntities();
            var res = new OnlineBroxelResponse();
            try
            {
                var registro = broxelCo.UsuariosOnlineBroxel.FirstOrDefault(x => x.Id == idUsuarioOnline);

                if (registro != null)
                {
                    registro.NombreCompleto = nombreCompleto ?? "";
                    registro.Sexo = genero ?? "";
                    broxelCo.Entry(registro).State = EntityState.Modified;
                    broxelCo.SaveChanges();
                    res.Mensaje = "Se actualizo correctamente";
                    res.Respuesta = true;
                }
                else
                {
                    res.Mensaje = "No se encontro informacion del usuario";
                    res.Respuesta = false;
                }

            }
            catch(Exception e)
            {
                res.Mensaje = "Error -> " + e.Message;
                res.Respuesta = false;
            }

            return res;

        }
    }
}