using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Reflection;
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
    /// <summary>
    /// A Singlton class which provides and loads the necessary assembly
    /// </summary>
    internal class AssemblyProvider
    {
        private static AssemblyProvider _assemblyProvider = null;
       
        private string _providerName = string.Empty;
        public AssemblyProvider()
        {
            _providerName = Configuration.GetProviderName(Configuration.DefaultConnection) ;                       
        }

        public AssemblyProvider(string providerName)
        {
            _providerName = providerName;
        }


        #region Refactored Code
        internal DbProviderFactory Factory
        {
            get
            {
                DbProviderFactory factory = DbProviderFactories.GetFactory(_providerName);
                return factory;
            }
        }

        #endregion

        #region "Internal Methods and Properties"

       
      
        //public static AssemblyProvider GetInstance(string providerName)
        //{
        //    if (_assemblyProvider == null)
        //    {
        //        _assemblyProvider = new AssemblyProvider(providerName);
        //    }
        //    return _assemblyProvider;
        //}
       
        #endregion


    }
}
