using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Dapper.Contrib.Extensions;
using NUnit.Framework;
using Spartacus.Database.DBModels.Civilizations;

namespace TestUnit.Reflection
{
    public class SqLiteHelperTest
    {
        [Test]
        public void AttributeTest()
        {
            var props = GetAllProperties(typeof(Civilization));
            foreach (var prop in props)
            {
                Debug.WriteLine($"{prop.Name}, {IsWriteAttribute(prop)}");
            }
        }

        private static bool IsWriteAttribute(PropertyInfo propertyInfo)
        {
            var attributes = propertyInfo.GetCustomAttributes(typeof(WriteAttribute), false).ToList();
            if (attributes.Count != 1) return true;

            var writeAttribute = (WriteAttribute)attributes[0];
            return writeAttribute.Write;
        }

        private static IEnumerable<PropertyInfo> GetAllProperties(Type type)
        {
            //Filter out non-writable properties
            return type?.GetProperties().Where(IsWriteAttribute);
        }
    }
}