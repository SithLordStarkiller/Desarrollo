using System;

namespace GOB.SPF.ConecII.Entities.Attributes
{
    public class ColumnManyAttribute : Attribute
    {
        public ColumnManyAttribute()
        {
        }

        public ColumnManyAttribute(string name)
        {
            this.Name = name;
        }

        public string Name
        {
            get;
            set;
        }
    }
}