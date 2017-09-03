using System;

namespace GOB.SPF.ConecII.Entities.Attributes
{
    public class ColumnEntityAttribute : Attribute
    {
        public ColumnEntityAttribute()
        {
        }

        public ColumnEntityAttribute(string name)
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