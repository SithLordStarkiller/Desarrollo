using GOB.SPF.ConecII.Entities.Attributes;
using GOB.SPF.ConecII.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.AccessData.Repositories
{
    public partial class Repositorio<T> where T : class
    {
        private readonly UnitOfWork mCurrentUoW;
        private readonly string mTableName;
        private readonly string mSchemaName;
        private readonly string mSchemaTable;

        public UnitOfWork CurrentUoW
        {
            get { return mCurrentUoW; }
        }
        public List<AppError> ListaErrores { get; set; }
        public Boolean IndicadorExito { get; set; }

        public Repositorio()
        {
            mCurrentUoW = new UnitOfWork();
            this.ListaErrores = new List<AppError>();

            SchemaNameAttribute schemaName = (SchemaNameAttribute)Attribute.GetCustomAttribute(typeof(T), typeof(SchemaNameAttribute));
            mSchemaName = schemaName.Value;
            TableNameAttribute tableName = (TableNameAttribute)Attribute.GetCustomAttribute(typeof(T), typeof(TableNameAttribute));
            mTableName = tableName.Value;
            mSchemaTable = mSchemaName + "." + mTableName;
        }

        public List<T> GetAllStored()
        {
            string cmd = mTableName + "Get";
            try
            {

                var entityList = mCurrentUoW.ExecuteReaderStored<T>(cmd);
                this.IndicadorExito = true;
                return entityList;
            }
            catch (Exception ex)
            {
                this.ListaErrores.Add(new AppError() { IdError = 1, Descripcion = ex.Message });
                this.IndicadorExito = false;
                return null;
            }
        }

        public List<DropDto> GetAllDropStored()
        {
            string cmd = mTableName + "Drop";
            try
            {

                var entityList = mCurrentUoW.ExecuteReaderStored<DropDto>(cmd);
                this.IndicadorExito = true;
                return entityList;
            }
            catch (Exception ex)
            {
                this.ListaErrores.Add(new AppError() { IdError = 1, Descripcion = ex.Message });
                this.IndicadorExito = false;
                return null;
            }
        }

        public T GetByIdStored(T entity)
        {
            string cmd = mTableName + "GetById";

            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>();
                string whereSelect = GetWhere(entity, GetPrimaryKey(), parametros, false);

                var entityList = mCurrentUoW.ExecuteReaderStored<T>(cmd, parametros);
                return entityList.FirstOrDefault();

            }
            catch (Exception ex)
            {
                this.ListaErrores.Add(new AppError() { IdError = 2, Descripcion = ex.Message });
                this.IndicadorExito = false;
                return null;
            }
        }

        private void SetParametersStored(T entity, List<SqlParameter> parametros)
        {


            PropertyInfo[] property = typeof(T).GetProperties();

            SetValueParametersStored(entity, property, parametros);


        }

        private void SetValueParametersStored(T entity, PropertyInfo[] property, List<SqlParameter> parametros)
        {


            foreach (PropertyInfo info in property)
            {

                Attribute columnAttribute = info.GetCustomAttribute(typeof(ColumnAttribute));

                if (columnAttribute != null)
                    parametros.Add(new SqlParameter("@p_" + info.Name, info.GetValue(entity)));
            }

        }

        public void AddStored(T entity)
        {
            string cmd = mTableName + "Post";
            try
            {
                mCurrentUoW.ExecuteNonqueryStored(cmd);


                this.IndicadorExito = true;
            }
            catch (Exception ex)
            {
                this.ListaErrores.Add(new AppError() { IdError = 3, Descripcion = ex.Message });
            }
        }

        public int AddIdentityStored(T entity)
        {
            string cmd = mTableName + "Post";
            try
            {

                int identity;
                List<SqlParameter> parametrosInsert = new List<SqlParameter>();
                SetParametersStored(entity, parametrosInsert);

                string pk = GetIdentityKey();
                if (!string.IsNullOrEmpty(pk))
                {
                    SqlParameter parametrosPk = parametrosInsert.Where(p => p.ParameterName == "@p_" + pk).FirstOrDefault();
                    parametrosInsert.Remove(parametrosPk);
                }

                identity = mCurrentUoW.ExecuteNonqueryStored(cmd, parametrosInsert, "p_" + pk + "out");


                PropertyInfo property = typeof(T).GetProperties().Where(p => p.Name == pk).First();
                property.SetValue(entity, identity);

                this.IndicadorExito = true;
                return identity;
            }
            catch (Exception ex)
            {
                this.ListaErrores.Add(new AppError() { IdError = 3, Descripcion = ex.Message });
                return -1;
            }
        }


        public void ModifiyStored(T entity)
        {
            string cmd = mTableName + "Put";
            try
            {

                List<SqlParameter> parametrosInsert = new List<SqlParameter>();
                SetParametersStored(entity, parametrosInsert);
                mCurrentUoW.ExecuteNonqueryStored(cmd, parametrosInsert);
                this.IndicadorExito = true;
            }
            catch (Exception ex)
            {
                this.ListaErrores.Add(new AppError() { IdError = 4, Descripcion = ex.Message });
                this.IndicadorExito = false;
            }
        }

        public void RemoveStored(T entity)
        {
            string cmd = mTableName + "Deletet";
            try
            {
                List<SqlParameter> parametrosWhere = new List<SqlParameter>();
                string whereSelect = GetWhere(entity, GetPrimaryKey(), parametrosWhere, false);



                mCurrentUoW.ExecuteNonqueryStored(cmd, parametrosWhere);

                this.IndicadorExito = true;
            }
            catch (Exception ex)
            {
                this.ListaErrores.Add(new AppError() { IdError = 5, Descripcion = ex.Message });
                this.IndicadorExito = false;
            }
        }

        public void OperationGenericStored(string cmd)
        {
            //try
            //{
            //    NonReadOperations tbrd = new NonReadOperations();
            //    tbrd.ExecuteNonqueryGeneric(cmd);
            //    this.IndicadorExito = true;
            //}
            //catch (Exception ex)
            //{
            //    this.ListaErrores.Add(new AppError() { IdError = 6, Descripcion = ex.Message });
            //    this.IndicadorExito = false;
            //}
        }

        public String GetDataTypeStored(String dataType, String param)
        {
            switch (dataType)
            {
                case "System.Byte": return param;
                case "System.SByte": return param;
                case "System.Int16": return param;
                case "System.Int32": return param;
                case "System.Int64": return param;
                case "System.UInt16": return param;
                case "System.UInt32": return param;
                case "System.UInt64": return param;
                case "System.Single": return param;
                case "System.Double": return param;
                case "System.Decimal": return param;
                case "System.Char": return "'" + param + "'";
                case "System.Boolean": return "'" + param + "'";
                case "System.String": return "'" + param + "'";
                case "System.Guid": return "'" + param + "'";
                case "System.TimeSpan": return "'" + param + "'";
                case "System.DateTime": return "'" + Convert.ToDateTime(param).ToString("yyyyMMdd HH:mm:ss") + "'";
                default:
                    break;
            }
            return param;
        }

        public bool OperationResultStored()
        {
            return this.IndicadorExito;
        }

        public string GetWhere(T entity, List<string> filters, List<SqlParameter> parametros, bool likeString = true, string operatorType = " AND ")
        {
            List<String> filterQuery = new List<string>();

            string whereSelect = " WHERE 1 = 1";
            if (filters != null)
            {


                foreach (var filter in filters)
                {
                    string propertiName = string.Empty;
                    string[] fieldSelecyFilter = filter.Split('.');
                    if (fieldSelecyFilter.Count() > 1)
                        propertiName = fieldSelecyFilter[fieldSelecyFilter.Count() - 1];
                    else
                        propertiName = filter;


                    var info = entity.GetType().GetProperty(propertiName);

                    if (info.PropertyType.FullName.Equals("System.String") && likeString)
                        filterQuery.Add(filter + " LIKE '%' + @" + filter.Replace(".", string.Empty) + " + '%' ");
                    else
                        filterQuery.Add(filter + " = @" + filter.Replace(".", string.Empty));

                    parametros.Add(new SqlParameter("@" + filter.Replace(".", string.Empty), info.GetValue(entity)));
                    //info.PropertyType.FullName;
                }
                if (filterQuery.Count > 0)
                    whereSelect = " WHERE " + string.Join(operatorType, filterQuery);
            }
            return whereSelect;
        }

        private List<string> GetPrimaryKey()
        {
            List<string> primary = new List<string>();

            PropertyInfo[] property = typeof(T).GetProperties();
            foreach (PropertyInfo info in property)
            {
                PrimaryKeyAttribute key = (PrimaryKeyAttribute)info.GetCustomAttribute(typeof(PrimaryKeyAttribute));
                if (key != null)
                    primary.Add(key.Value);
            }

            return (primary);
        }

        private string GetIdentityKey()
        {

            PropertyInfo[] property = typeof(T).GetProperties();
            foreach (PropertyInfo info in property)
            {
                IdentityKeyAttribute key = (IdentityKeyAttribute)info.GetCustomAttribute(typeof(IdentityKeyAttribute));

                if (key != null)
                {
                    PrimaryKeyAttribute primaryKey = (PrimaryKeyAttribute)info.GetCustomAttribute(typeof(PrimaryKeyAttribute));
                    return primaryKey.Value;
                }
            }

            return string.Empty;
        }

        public T GetById(T entity)
        {

            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>();
                string whereSelect = GetWhere(entity, GetPrimaryKey(), parametros, false);

                string cmd = string.Format("SELECT * FROM {0} {1}",
                             mSchemaTable, whereSelect);

                var entityList = mCurrentUoW.ExecuteReader<T>(cmd, parametros);
                return entityList.FirstOrDefault();

            }
            catch (Exception ex)
            {
                this.ListaErrores.Add(new AppError() { IdError = 2, Descripcion = ex.Message });
                this.IndicadorExito = false;
                return null;
            }
        }
    }
}
