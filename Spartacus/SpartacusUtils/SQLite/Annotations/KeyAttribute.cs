﻿using System;

namespace SpartacusUtils.SQLite
{
    /// <summary>
    /// Specifies a property is a primary key <see cref="PrimaryKeyAttribute"/> && <see cref="KeyAttribute"/> are the same
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class KeyAttribute : Attribute
    {
    }
}