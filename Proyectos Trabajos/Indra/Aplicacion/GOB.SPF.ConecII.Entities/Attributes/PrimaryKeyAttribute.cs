using System;

namespace GOB.SPF.ConecII.Entities.Attributes
{
     public  class PrimaryKeyAttribute : Attribute
     {
         public PrimaryKeyAttribute(String primaryKey)
         {
             Value = primaryKey;
             AutoIncrement = true;
         }
         public String Value
         {
             get;
             private set;
         }
         public bool AutoIncrement
         {
             get;
             set;
         }
     }
}
