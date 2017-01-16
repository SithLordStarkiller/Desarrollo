using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PubliPayments.Entidades;
using PubliPayments.Negocios.WebServiceFmk;
using PubliPayments.Utiles;

namespace PubliPayments.Negocios
{
    public class MensajesFormiik
    {
        public void EnviarMensaje(MensajeModel mensajeModel)
        {
            try
            {
                if (mensajeModel!=null)
                {
                    var bkclient = new BackEndClient("BasicHttpBinding_IBackEnd");

                    var isGroup = mensajeModel.UserName.Count() > 2;
                    if (isGroup)
                    {
                        bkclient.QueueMessageToUserNames(mensajeModel.ClientId, mensajeModel.ProductId, mensajeModel.UserName, mensajeModel.Sender, mensajeModel.Message, mensajeModel.IsImportant);
                    }
                    else
                    {
                        bkclient.QueueMessageToUserName(mensajeModel.ClientId, mensajeModel.ProductId, mensajeModel.UserName[0], mensajeModel.Sender, mensajeModel.Message, mensajeModel.IsImportant);
                    }

                    Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "MensajesFormiik - EnviarMensaje - ", String.Format(" -EnviarMensaje- UsuarioPadre {0} usuarios {1}  mensaje {2}", mensajeModel.Sender, String.Join(",", mensajeModel.UserName), mensajeModel.Message));            
                }
               
            }
            catch (Exception ex)
            {                
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "MensajesFormiik - EnviarMensaje - ", ex.Message);            
            }
        }
    }
}
