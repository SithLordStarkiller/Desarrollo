using System;

namespace GOB.SPF.ConecII.Entities.Attributes
{
     public  class TableNameAttribute : Attribute
     {
         public TableNameAttribute(string tableName)
         {
             Value = tableName;
         }
         public string Value
         {
             get;
             private set;
         }
     }
}
