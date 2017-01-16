using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace PubliPayments.Entidades.Originacion
{
    public static class EntDocumentosOrden
    {

        public static CaratulaContratoModel CaratulaContrato(string idOrden)
        {
            string sql = "exec obtenerDatosRegistro " +
                         " @idOrden=" + idOrden + "";
            var conexion = ConexionSql.Instance;
            var cnn = conexion.IniciaConexion();
            var sc = new SqlCommand(sql, cnn);
            var sda = new SqlDataAdapter(sc);
            var ds = new DataSet();
            sda.Fill(ds);
            conexion.CierraConexion(cnn);

            var row = ds.Tables[0].Rows[0];
            var ccm = new CaratulaContratoModel
            {
                AmortizacionMensual = row["AmortizacionMensual"].ToString(),
                CURP = row["CURP"].ToString(),
                Domicilio = row["Domicilio"].ToString(),
                Identificacion = row["Identificacion"].ToString(),
                LugarFecha = row["LugarFecha"].ToString(),
                MontoCredito = row["MontoCredito"].ToString(),
                MontoTotal = row["MontoTotal"].ToString(),
                NSS = row["NSS"].ToString(),
                NombreAcreditado = row["NombreAcreditado"].ToString(),
                NumeroCredito = row["NumeroCredito"].ToString(),
                NumeroTarjeta = row["NumeroTarjeta"].ToString(),
                PlazoCredito = row["PlazoCredito"].ToString(),
                RFC = row["RFC"].ToString(),
                TasaInteres = row["TasaInteres"].ToString()
            };

            return ccm;
        }

        public static ConsultaBuroModel BuroCredito(string idOrden)
        {
            string sql = "exec ObtenerConsultaBuro " +
                         " @idOrden=" + idOrden + "";
            var conexion = ConexionSql.Instance;
            var cnn = conexion.IniciaConexion();
            var sc = new SqlCommand(sql, cnn);
            var sda = new SqlDataAdapter(sc);
            var ds = new DataSet();
            sda.Fill(ds);
            conexion.CierraConexion(cnn);

            var row = ds.Tables[0].Rows[0];
            var ccm = new ConsultaBuroModel
            {
                EntidadFinanciera = row["EntidadFinanciera"].ToString(),
                LugarFecha = row["LugarFecha"].ToString(),
                Nombre = row["Nombre"].ToString(),
                i = ""
            };

            return ccm;
        }


    }
}
