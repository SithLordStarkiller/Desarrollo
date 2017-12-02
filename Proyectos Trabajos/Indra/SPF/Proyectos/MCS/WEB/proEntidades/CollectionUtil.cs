using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Reflection;

namespace proEntidades
{
    public class CollectionUtil
    {
        private static List<DataColumn> ToEnumerable(DataColumnCollection Columns)
        {
            List<DataColumn> dc = new List<DataColumn>();
            if (Columns.Count > 0)
            {
                foreach (DataColumn dcItem in Columns)
                {
                    dc.Add(dcItem);
                }
            }
            return dc;
        }

        public static List<T> ToCollection<T>(DataTable dt)
        {
            List<T> lst = new System.Collections.Generic.List<T>();
            Type tClass = typeof(T);
            PropertyInfo[] pClass = tClass.GetProperties();
            List<DataColumn> dc = ToEnumerable(dt.Columns);
            T cn;
            foreach (DataRow item in dt.Rows)
            {
                cn = (T)Activator.CreateInstance(tClass);
                foreach (PropertyInfo pc in pClass)
                {
                    // Can comment try catch block. 
                    try
                    {
                        DataColumn d = dc.Find(c => c.ColumnName == pc.Name);
                        if (d != null)
                            pc.SetValue(cn, item[pc.Name], null);
                    }
                    catch
                    {
                    }
                }
                lst.Add(cn);
            }
            return lst;
        }
    }
}
