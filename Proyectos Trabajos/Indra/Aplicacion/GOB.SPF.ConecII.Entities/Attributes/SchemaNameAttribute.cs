using System;

namespace GOB.SPF.ConecII.Entities.Attributes
{
     public  class SchemaNameAttribute : Attribute
     {
         public SchemaNameAttribute(string schemaName)
         {
             Value = schemaName;
         }
         public string Value
         {
             get;
             private set;
         }
     }
}
