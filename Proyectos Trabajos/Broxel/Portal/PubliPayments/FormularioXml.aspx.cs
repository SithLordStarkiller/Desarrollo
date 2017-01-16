using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using PubliPayments.Entidades;
using PubliPayments.Negocios;
using PubliPayments.Utiles;

namespace PubliPayments
{
    public partial class FormularioXml : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (SessionUsuario.ObtenerDato(SessionUsuarioDato.Dominio) == "0" || "234".Contains(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdRol)))
            {
                Response.Redirect("unauthorized.aspx");
            }
        }
        protected void BtnUploadClick(object sender, EventArgs e)
        {
            if (FileUploadControl.HasFile)
            {
                try
                {
                    if (FileUploadControl.PostedFile.ContentType == "text/xml")
                    {
                        var stream = FileUploadControl.PostedFile.InputStream;
                        var reader = new StreamReader(stream);
                        var text = reader.ReadToEnd();
                        var xmlFormDocument = new XmlDocument();
                        xmlFormDocument.LoadXml(text);
                        var resultado = new Formularios().ProcesarFormularioXML(xmlFormDocument,
                            Convert.ToInt32(TipoCaptura.SelectedValue), Descripcion.Text, Ruta.Text);
                        StatusLabel.Text = resultado;
                    }
                    else
                        StatusLabel.Text = "Archivo no valido";
                }
                catch (Exception ex)
                {
                    StatusLabel.Text = "Error de carga-" + ex.Message;
                }
            }
        }

       
    }
}