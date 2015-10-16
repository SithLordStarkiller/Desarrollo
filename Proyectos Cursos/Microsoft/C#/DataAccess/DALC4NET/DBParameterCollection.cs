using System;
using System.Collections.Generic;

using System.Text;

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
    public class DBParameterCollection
    {
        private List<DBParameter> _parameterCollection = new List<DBParameter>();

        /// <summary>
        /// Adds a DBParameter to the ParameterCollection
        /// </summary>
        /// <param name="parameter">Parameter to be added</param>
        public void Add(DBParameter parameter)
        {
            _parameterCollection.Add(parameter);
        }

        /// <summary>
        /// Removes parameter from the Parameter Collection
        /// </summary>
        /// <param name="parameter">Parameter to be removed</param>
        public void Remove(DBParameter parameter)
        {
            _parameterCollection.Remove(parameter);
        }
        
        /// <summary>
        /// Removes all the parameters from the Parameter Collection
        /// </summary>
        public void RemoveAll()
        {
            _parameterCollection.RemoveRange(0, _parameterCollection.Count - 1);
        }

        /// <summary>
        /// Removes parameter from the specified index.
        /// </summary>
        /// <param name="index">Index from which parameter is supposed to be removed</param>
        public void RemoveAt(int index)
        {
            _parameterCollection.RemoveAt(index);
        }

        /// <summary>
        /// Gets list of parameters
        /// </summary>
        internal List<DBParameter> Parameters
        {
            get
            {
                return _parameterCollection;
            }
        }
    }
}
