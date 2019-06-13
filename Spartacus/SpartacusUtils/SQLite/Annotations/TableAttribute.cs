using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartacusUtils.SQLite
{
    /// <summary>
    /// Specifies a Class / Interface with a table name
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public class TableAttribute : Attribute
    {
        public string Name { get; }

        public TableAttribute(string name)
        {
            Name = name;
        }
    }
}
