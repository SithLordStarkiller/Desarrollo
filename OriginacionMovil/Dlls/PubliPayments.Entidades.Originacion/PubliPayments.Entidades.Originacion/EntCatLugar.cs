namespace PubliPayments.Entidades.Originacion
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using OriginacionMovil.Models;

    public class EntCatLugar
    {
        public List<CatLugar> ObtenerLugares()
        {
            try
            {
                var instancia = ConnectionDB.Instancia;
                var ds = instancia.EjecutarDataSet("SqlDefault", "ObtenerLugar", null);

                //const string sql = "exec ObtenerLugar ";
                //var conexion = ConexionSql.Instance;
                //var cnn = conexion.IniciaConexion();
                //var sc = new SqlCommand(sql, cnn);
                //var sda = new SqlDataAdapter(sc);
                //var ds = new DataSet();
                //sda.Fill(ds);
                //conexion.CierraConexion(cnn);

                return BindingDatos(ds);
            }
            catch (Exception)
            {
                return new List<CatLugar>();
            }
        }

        private List<CatLugar> BindingDatos(DataSet ds)
        {
            var lista = new List<CatLugar>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                var face = new CatLugar();
                var props = face.GetType().GetProperties().ToDictionary(p => p.Name);

                foreach (DataColumn col in row.Table.Columns)
                {
                    var name = col.ColumnName;
                    if (row[name] == DBNull.Value || !props.ContainsKey(name)) continue;
                    var item = row[name];
                    var p = props[name];
                    if (p.PropertyType != col.DataType)
                        item = Convert.ChangeType(item, p.PropertyType);
                    p.SetValue(face, item, null);
                }

                lista.Add(face);
            }

            return lista;
        }
    }
}
