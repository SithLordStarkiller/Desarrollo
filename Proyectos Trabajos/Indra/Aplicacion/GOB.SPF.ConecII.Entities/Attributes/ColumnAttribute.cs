using System;

namespace GOB.SPF.ConecII.Entities.Attributes
{
    public class ColumnAttribute : Attribute
    {
        public ColumnAttribute()
        {
        }
        public ColumnAttribute(string name)
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