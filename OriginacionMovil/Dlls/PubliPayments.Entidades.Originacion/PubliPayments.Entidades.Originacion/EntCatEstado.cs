namespace PubliPayments.Entidades.Originacion
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using OriginacionMovil.Models;

    public class EntCatEstado
    {
        public List<CatEstados> ObtenerEstados()
        {
            try
            {
                var instancia = ConnectionDB.Instancia;
                var ds = instancia.EjecutarDataSet("SqlDefault", "ObtenerEstados", null);
                return BindingDatos(ds);
            }
            catch (Exception)
            {
                return new List<CatEstados>();
            }
        }

        private List<CatEstados> BindingDatos(DataSet ds)
        {
            var lista = new List<CatEstados>();
            
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                var face = new CatEstados();
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
