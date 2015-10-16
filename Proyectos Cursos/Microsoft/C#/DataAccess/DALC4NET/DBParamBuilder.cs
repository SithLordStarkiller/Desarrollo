using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.Common;

/*****************************************************************************
 * DALC4NET IS AN OPEN SOURCE DATA ACCESS LAYER
 * THIS DOES NOT REQUIRE ANY KIND OF LICENSEING
 * USERS ARE FREE TO MODIFY THE SOURCE CODE AS PER REQUIREMENT
 * ANY SUGGESTIONS ARE MOST WELCOME (SEND THE SAME TO AK.TRIPATHI@YAHOO.COM WITH DALC4NET AS SUBJECT LINE 
 * ---------------- AUTHOR DETAILS--------------
 * NAME     : ASHISH TRIPATHI
 * LOCATION : BANGALORE (INDIA)
 * EMAIL    : AK.TRIPATHI@YAHOO.COM
 * MOBILE   : +91 98809 46821
 ******************************************************************************/
namespace DALC4NET
{
    internal class DBParamBuilder
    {

        private string _providerName = string.Empty;
        private AssemblyProvider _assemblyProvider = null;

        internal DBParamBuilder(string providerName)
        {
            _assemblyProvider = new AssemblyProvider(providerName);
            _providerName = providerName;
        }

        internal DbParameter GetParameter(DBParameter parameter)
        {
            DbParameter dbParam = GetParameter();         
            dbParam.ParameterName = parameter.Name;         
            dbParam.Value = parameter.Value;
            dbParam.Direction = parameter.ParamDirection;            
            dbParam.DbType = parameter.Type;

            return dbParam;            
        }

        internal List<DbParameter> GetParameterCollection(DBParameterCollection parameterCollection)
        {
            List<DbParameter> dbParamCollection = new List<DbParameter>();
            DbParameter dbParam = null;
            foreach(DBParameter param in parameterCollection.Parameters)
            {
                dbParam = GetParameter(param);
                dbParamCollection.Add(dbParam);
            }
            
            return dbParamCollection;
        }

        #region Private Methods
        private DbParameter GetParameter()
        {
            //string typeName = AssemblyProvider.GetInstance().GetParameterType();
            //IDbDataParameter dbParam = (IDbDataParameter)AssemblyProvider.GetInstance().DBProviderAssembly.CreateInstance(typeName);
            //return dbParam;

            //DbParameter dbParam = AssemblyProvider.GetInstance(this._providerName).Factory.CreateParameter();
            DbParameter dbParam = _assemblyProvider.Factory.CreateParameter();
            return dbParam;

        }

  

        #endregion
    }
}
