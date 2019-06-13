using System;

namespace SpartacusUtils.SQLite.Annotations
{
    /// <summary>
    /// Specifies a property that is unique
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class UniqueKeyAttribute : Attribute
    {
    }
}