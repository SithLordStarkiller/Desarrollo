using System;

namespace GOB.SPF.ConecII.Entities.Attributes
{
    public class ColumnRelatedAttribute : Attribute
    {
        public ColumnRelatedAttribute()
        {
        }

        public ColumnRelatedAttribute(string name)
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