namespace PubliPayments.Entidades.Originacion
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using OriginacionMovil.Models;

    public class EntCatEmpresa
    {
        public List<CatEmpresa> ObtenerEmpresas()
        {
            try
            {
                var instancia = ConnectionDB.Instancia;
                var ds = instancia.EjecutarDataSet("SqlDefault", "ObtenerEmpresas", null);

                //const string sql = "exec ObtenerEmpesas ";
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
                return new List<CatEmpresa>();
            }
        }

        private List<CatEmpresa> BindingDatos(DataSet ds)
        {
            var lista = new List<CatEmpresa>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                var face = new CatEmpresa();
                var props = face.GetType().GetProperties().ToDictionary(p => p.Name);

                foreach (DataColumn col in row.Table.Columns)
                {
                    var name = col.ColumnName;
                    if (row[name] == DBNull.Value || !props.ContainsKey(name)) continue;
                    var item = row[name];
                    var p = props[name];
                    if (p.PropertyType != col.DataType)
                        try
                        {
                            item = Convert.ChangeType(item, p.PropertyType);
                        }
                        catch (Exception)
                        {
                            item = null;
                        }
                    p.SetValue(face, item, null);
                }

                lista.Add(face);
            }

            return lista;
        }
    }
}
