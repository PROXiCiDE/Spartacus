using System;

namespace SpartacusUtils.SQLite
{
    [AttributeUsage(AttributeTargets.Property)]
    public class WriteAttribute : Attribute
    {
        public bool Writable { get; }

        /// <summary>
        /// Specifies if we should ignore or write a column in the database
        /// </summary>
        /// <param name="writable"></param>
        public WriteAttribute(bool writable)
        {
            Writable = writable;
        }
    }
}