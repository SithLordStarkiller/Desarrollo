using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Configuration;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using GOB.SPF.ConecII.Entities.Attributes;

namespace GOB.SPF.ConecII.AccessData
{
    public partial class UnitOfWork 
    {
        public List<T> ExecuteReaderStored<T>(string cmd)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            return ExecuteCmdStored<T>(cmd, properties);
        }

        public List<T> ExecuteReaderStored<T>(string cmd, List<SqlParameter> paramsFilters)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            return ExecuteCmdStored<T>(cmd, properties, paramsFilters);
        }

        public List<T> ExecuteReaderStored<T>(string cmd, List<string> fields)
        {
            PropertyInfo[] properties = typeof(T).GetProperties().Where(p => fields.Contains(p.Name)).ToArray();
            return ExecuteCmdStored<T>(cmd, properties);
        }

        public List<T> ExecuteReaderStored<T>(string cmd, List<string> fields, List<SqlParameter> paramsFilters)
        {
            PropertyInfo[] properties = typeof(T).GetProperties().Where(p => fields.Contains(p.Name)).ToArray();
            return ExecuteCmdStored<T>(cmd, properties, paramsFilters);
        }

        private List<T> ExecuteCmdStored<T>(string cmdString, PropertyInfo[] properties, List<SqlParameter> paramsFilters = null)
        {
            List<T> listout = new List<T>();
            Database database = DatabaseFactory.CreateDatabase();
            SqlCommand command = SetParameters(cmdString, paramsFilters);

            using (IDataReader reader = database.ExecuteReader(command))
            {
                while (reader.Read())
                {
                    var rdrList = Activator.CreateInstance<T>();
                    foreach (var item in properties)
                    {
                        object[] attributes = item.GetCustomAttributes(false);

                        if (attributes.Length > 0)
                        {
                            foreach (var itemattribute in attributes)
                            {
                                if (itemattribute.GetType() == (typeof(IdentityKeyAttribute)) || (itemattribute.GetType() == (typeof(PrimaryKeyAttribute))) || (itemattribute.GetType() == (typeof(ColumnAttribute))))
                                {

                                    if (!reader.IsDBNull(reader.GetOrdinal(item.Name)))
                                    {
                                        Type convertTo = Nullable.GetUnderlyingType(item.PropertyType) ?? item.PropertyType;
                                        item.SetValue(rdrList, Convert.ChangeType(reader[item.Name], convertTo), null);
                                    }
                                    break;
                                }

                            }
                        }
                    }
                    listout.Add(rdrList);
                }
                return listout;
            }
        }


        public List<T> ExecuteReaderStoredMany<T>(string cmdString, List<SqlParameter> paramsFilters, ref int numero, string campoNumero = null,
            List<string> relatedEntity = null, List<string> relatedEntityFields = null,
            List<string> relatedMany = null, List<string> relatedManyFields = null)
        {
            var listout = new List<T>();
            var propertiesRoot = typeof(T).GetProperties();

            Database database = DatabaseFactory.CreateDatabase();

            {
                using (SqlCommand command = SetParameters(cmdString, paramsFilters))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    if (!string.IsNullOrEmpty(campoNumero))
                        command.Parameters.Add(campoNumero, SqlDbType.Int).Direction = ParameterDirection.Output;

                    //{
                    //    SqlParameter campoNumeroParameter = new SqlParameter(campoNumero, SqlDbType.Int);
                    //    campoNumeroParameter.Direction = ParameterDirection.Output;
                    //    command.Parameters.Add(campoNumeroParameter);
                    //}


                    using (IDataReader reader = database.ExecuteReader(command))
                    {
                        if (!string.IsNullOrEmpty(campoNumero))
                            numero = int.Parse("0" + command.Parameters[campoNumero].Value);

                        while (reader.Read())
                        {
                            var rdrListRoot = Activator.CreateInstance<T>();
                            bool addEntity = true;


                            foreach (var itemRoot in propertiesRoot)
                            {
                                object[] attributesRoot = itemRoot.GetCustomAttributes(false);

                                if (attributesRoot.Length > 0)
                                {
                                    foreach (var itemattributeRoot in attributesRoot)
                                    {
                                        if (itemattributeRoot.GetType() == (typeof(PrimaryKeyAttribute)))
                                        {
                                            Type convertTo = Nullable.GetUnderlyingType(itemRoot.PropertyType) ?? itemRoot.PropertyType;
                                            itemRoot.SetValue(rdrListRoot, Convert.ChangeType(reader[itemRoot.Name], convertTo), null);

                                            foreach (var elemento in listout)
                                            {
                                                if (itemRoot.GetValue(rdrListRoot).Equals(itemRoot.GetValue(elemento)))
                                                {
                                                    rdrListRoot = elemento;
                                                    addEntity = false;
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            if (addEntity)
                                listout.Add(rdrListRoot);



                            foreach (var itemRoot in propertiesRoot)
                            {
                                object[] attributesRoot = itemRoot.GetCustomAttributes(false);

                                if (attributesRoot.Length > 0)
                                {
                                    foreach (var itemattributeRoot in attributesRoot)
                                    {
                                        if (itemattributeRoot.GetType() == (typeof(IdentityKeyAttribute)) || (itemattributeRoot.GetType() == (typeof(PrimaryKeyAttribute))) || (itemattributeRoot.GetType() == (typeof(ColumnAttribute))))
                                        {
                                            if (!reader.IsDBNull(reader.GetOrdinal(itemRoot.Name)))
                                            {
                                                Type convertTo = Nullable.GetUnderlyingType(itemRoot.PropertyType) ?? itemRoot.PropertyType;
                                                itemRoot.SetValue(rdrListRoot, Convert.ChangeType(reader[itemRoot.Name], convertTo), null);
                                            }
                                        }
                                        else if (itemattributeRoot.GetType() == typeof(ColumnEntityAttribute))
                                        {

                                            if (relatedEntity != null && relatedEntityFields != null)
                                            {
                                                if (relatedEntity.Contains(itemRoot.Name))
                                                {
                                                    var enity = Activator.CreateInstance(itemRoot.PropertyType.Assembly.FullName.Split(',')[0], itemRoot.PropertyType.FullName).Unwrap();
                                                    Type tipo = enity.GetType();
                                                    var propertiesRelated = tipo.GetProperties();
                                                    bool addObject = false;

                                                    foreach (var item in propertiesRelated)
                                                    {


                                                        if (relatedEntityFields.Contains(item.Name + "Entity" + itemRoot.Name))
                                                        {
                                                            addObject = true;
                                                            object[] attributes = item.GetCustomAttributes(false);

                                                            if (attributes.Length > 0)
                                                            {
                                                                foreach (var itemattribute in attributes)
                                                                {
                                                                    if (itemattribute.GetType() == (typeof(IdentityKeyAttribute)) || (itemattribute.GetType() == (typeof(PrimaryKeyAttribute))) || (itemattribute.GetType() == (typeof(ColumnAttribute))))
                                                                    {

                                                                        if (!reader.IsDBNull(reader.GetOrdinal(item.Name + "Entity" + itemRoot.Name)))
                                                                        {
                                                                            Type convertTo = Nullable.GetUnderlyingType(item.PropertyType) ?? item.PropertyType;
                                                                            item.SetValue(enity, Convert.ChangeType(reader[item.Name + "Entity" + itemRoot.Name], convertTo), null);
                                                                            break;
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }


                                                    }
                                                    if (addObject)
                                                    {
                                                        itemRoot.SetValue(rdrListRoot, enity);
                                                    }
                                                }
                                            }
                                        }
                                        else if (itemattributeRoot.GetType() == typeof(ColumnManyAttribute))
                                        {

                                            if (relatedMany != null && relatedManyFields != null)
                                            {
                                                if (relatedMany.Contains(itemRoot.Name))
                                                {
                                                    var entityMany = Activator.CreateInstance("Nsft.Entity.Proyect", "Nsft.Entity.Proyect." + itemRoot.Name).Unwrap();
                                                    Type tipoMany = entityMany.GetType();
                                                    var propertiesRelatedMany = tipoMany.GetProperties();
                                                    bool addObjectMany = false;

                                                    foreach (var itemMany in propertiesRelatedMany)
                                                    {

                                                        if (relatedManyFields.Contains(itemMany.Name + itemRoot.Name))
                                                        {
                                                            addObjectMany = true;
                                                            object[] attributesMany = itemMany.GetCustomAttributes(false);

                                                            if (attributesMany.Length > 0)
                                                            {
                                                                foreach (var itemattributeMany in attributesMany)
                                                                {
                                                                    if (itemattributeMany.GetType() == (typeof(IdentityKeyAttribute)) || (itemattributeMany.GetType() == (typeof(PrimaryKeyAttribute))) || (itemattributeMany.GetType() == (typeof(ColumnAttribute))))
                                                                    {

                                                                        if (!reader.IsDBNull(reader.GetOrdinal(itemMany.Name + itemRoot.Name)))
                                                                        {
                                                                            Type convertTo = Nullable.GetUnderlyingType(itemMany.PropertyType) ?? itemMany.PropertyType;
                                                                            itemMany.SetValue(entityMany, Convert.ChangeType(reader[itemMany.Name + itemRoot.Name], convertTo), null);
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }


                                                    }
                                                    if (addObjectMany)
                                                    {
                                                        var listMany = (IList)itemRoot.GetValue(rdrListRoot);
                                                        listMany.Add(entityMany);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(campoNumero))
                        numero = int.Parse("0" + command.Parameters[campoNumero].Value);
                }
            }
            return listout;
        }

        public int ExecuteNonqueryStored(string cmdString)
        {
            object resultado = -1;
            Database database = DatabaseFactory.CreateDatabase();
            {
                try
                {


                    resultado = database.ExecuteNonQuery(cmdString);

                }
                catch (Exception)
                {
                }
            }
            return (int)resultado;
        }

        public int ExecuteNonqueryStored(string cmdString, List<SqlParameter> parametros, string pk = null)
        {
            object resultado = -1;
            Database database = DatabaseFactory.CreateDatabase();
            {
                try
                {

                    using (SqlCommand command = SetParameters(cmdString, parametros))
                    {
                        if (!string.IsNullOrEmpty(pk))
                            command.Parameters.Add(pk, SqlDbType.Int).Direction = ParameterDirection.Output;

                        command.CommandType = CommandType.StoredProcedure;
                        resultado = database.ExecuteNonQuery(command);

                        if (!string.IsNullOrEmpty(pk))
                            resultado = int.Parse("0" + command.Parameters[pk].Value);
                    }
                }
                catch (Exception)
                {
                }
            }

            return (int)resultado;
        }


        public int ExecuteScalarStored(string cmdString)
        {
            object resultado = -1;
            Database database = DatabaseFactory.CreateDatabase();
            {
                try
                {

                    using (SqlCommand cmd = new SqlCommand(cmdString))
                    {
                        //to gain access to ROWIDs of the table
                        //cmd.AddRowid = true;
                        //cmd.CommandType = CommandType.Text;
                        resultado = database.ExecuteScalar(cmd);
                    }

                }
                catch (Exception)
                {
                }
            }

            return Convert.ToInt32(resultado);
        }

        public int ExecuteScalarStored(string cmdString, List<SqlParameter> parametros)
        {
            object resultado = -1;
            Database database = DatabaseFactory.CreateDatabase();
            {

                using (SqlCommand command = SetParameters(cmdString, parametros))
                {


                    resultado = database.ExecuteScalar(command);


                }

            }
            return Convert.ToInt32(resultado);
        }

        private SqlCommand SetParameters(String cmd, List<SqlParameter> parametros)
        {
            SqlCommand command = new SqlCommand(cmd);

            if (parametros != null)
                foreach (SqlParameter item in parametros)
                {
                    command.Parameters.Add(new SqlParameter(item.ParameterName, item.SqlValue));
                }
            return command;
        }

        public List<T> ExecuteReader<T>(string cmd, List<SqlParameter> paramsFilters)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            return ExecuteCmd<T>(cmd, properties, paramsFilters);
        }

        private List<T> ExecuteCmd<T>(string cmd, PropertyInfo[] properties, List<SqlParameter> paramsFilters = null)
        {
            List<T> listout = new List<T>();
            Database database = DatabaseFactory.CreateDatabase();
            SqlCommand command = SetParameters(cmd, paramsFilters);

            using (IDataReader reader = database.ExecuteReader(command))
            {
                while (reader.Read())
                {
                    var rdrList = Activator.CreateInstance<T>();
                    foreach (var item in properties)
                    {
                        object[] attributes = item.GetCustomAttributes(false);

                        if (attributes.Length > 0)
                        {
                            foreach (var itemattribute in attributes)
                            {
                                if (itemattribute.GetType() == (typeof(IdentityKeyAttribute)) || (itemattribute.GetType() == (typeof(PrimaryKeyAttribute))) || (itemattribute.GetType() == (typeof(ColumnAttribute))))
                                {

                                    if (!reader.IsDBNull(reader.GetOrdinal(item.Name)))
                                    {
                                        Type convertTo = Nullable.GetUnderlyingType(item.PropertyType) ?? item.PropertyType;
                                        item.SetValue(rdrList, Convert.ChangeType(reader[item.Name], convertTo), null);
                                    }
                                    break;
                                }

                            }
                        }
                    }
                    listout.Add(rdrList);
                }
                return listout;
            }
        }


    }
}
