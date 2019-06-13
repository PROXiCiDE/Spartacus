using System;

namespace SpartacusUtils.SQLite
{
    /// <summary>
    /// Specifies a property cannot be a null value
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class NotNullAttribute : Attribute
    {
    }
}