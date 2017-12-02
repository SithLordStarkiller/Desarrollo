using System.Collections.Generic;

namespace Mcs.Core.Common
{
    public class RequestItem: Dictionary<string, object>
    {
        public new object this[string key]
        {
            get
            {
                return ContainsKey(key) ? base[key] : null;
            }
            set
            {
                if (ContainsKey(key))
                    base[key] = value;
                else
                    Add(key, value);
            }
        }
    }
}
