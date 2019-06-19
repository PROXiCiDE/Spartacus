using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using Dapper;
using NUnit.Framework;
using ProjectCeleste.GameFiles.XMLParser;
using ProjectCeleste.GameFiles.XMLParser.Enum;
using SpartacusUtils.SQLite;
using TestUnit.Database;

namespace TestUnit.Reflection
{
    public class SqLiteHelperTest : SqliteTestBase
    {
       

        [Test]
        public void NonGenerics()
        {
            var obj = new HashSet<UnitFlagEnum>();
            var type = obj.GetType();
            var propertyInfos = SqLiteHelperExtensions.GetAllProperties(type);

            Debug.WriteLineIf(GenericClassifier.IsISet(type), "IsISet");
            Debug.WriteLineIf(GenericClassifier.IsICollection(type), "IsICollection");
            Debug.WriteLineIf(GenericClassifier.IsIEnumerable(type), "IsIEnumerable");
            Debug.WriteLine($"IsGeneric({type.IsGenericType}), IsArray({type.IsArray}), IsClass({type.IsClass})");
            SqLiteHelperExtensions.AddColumnReplace("ProtoAge4Xml", "ProtoUnit");
            foreach (var propertyInfo in propertyInfos)
            {
                var propType = propertyInfo.PropertyType;

                var name = SqLiteHelperExtensions.GetColumnName(propertyInfo);

                if(propType.IsArray)
                    Debug.WriteLine($"{propertyInfo.Name} == Array");

                var prop_Decl = SqLiteHelperExtensions.GetColumnDeclaringType(propType);
                if (!propType.IsGenericType && propType.IsClass && string.IsNullOrEmpty(prop_Decl))
                {
                    Debug.WriteLine(propType.Name);
                }
            }
        }

        public class MyClass
        {
            public ProtoAge4XmlUnitCost UnitCost { get; set; }
            public ProtoAge4XmlUnitDecay UnitDecay { get; set; }
            public ProtoAge4XmlUnitInitialResource InitialResource { get; set; }
            public HashSet<UnitFlagEnum> Flag { get; set; }
        }

        [Test]
        public void CheckClass()
        {
            //SqLiteHelperExtensions.AddColumnRename(typeof(ProtoAge4XmlUnitCost), "UnitCost");
            //SqLiteHelperExtensions.AddColumnRename(typeof(ProtoAge4XmlUnitDecay), "UnitDecay");
            //SqLiteHelperExtensions.AddColumnRename(typeof(ProtoAge4XmlUnitInitialResource), "UnitInitialResource");
            //SqLiteHelperExtensions.AddColumnRename(typeof(ProtoAge4XmlUnitEvent), "UnitEvent");
            //SqLiteHelperExtensions.AddColumnRename(typeof(ProtoAge4XmlUnitCommand), "UnitCommand");
            //SqLiteHelperExtensions.AddColumnRename(typeof(ProtoAge4XmlUnitCarryCapacity), "UnitCarryCapacity");
            //SqLiteHelperExtensions.AddColumnRename(typeof(ProtoAge4XmlRowPageColumn), "UnitRowPageColumn");
            //SqLiteHelperExtensions.AddColumnRename(typeof(ProtoAge4XmlUnitHeightBob), "UnitHeightBob");
            //SqLiteHelperExtensions.AddColumnRename(typeof(ProtoAge4XmlUnitMinimapColor), "UnitMinimapColor");

            SqLiteHelperExtensions.AddColumnReplace("ProtoAge4Xml", "");

            using (IDbConnection conn = new SQLiteConnection(ConnectionString))
            {
                conn.DropAllTablesIfExists();

                var tableSchemaDev = conn.CreateTableSchemaDev<ProtoAge4XmlUnit>("UnitId", tableNameOverride: "ProtoUnit");
                Debug.WriteLine(tableSchemaDev);

                conn.Execute(tableSchemaDev);

            }


            //List<double> maxRange = new List<double>();
            //var type = maxRange.GetType();
            //Debug.WriteLine($"{type.IsGenericType}, {type.IsClass}, {type.IsAbstract}, {type.IsInterface}");
        }


        [Test]
        public void CheckList()
        {
            var maxRange = new List<double>();
            var type = maxRange.GetType();

            if (type.IsGenericType && type.GetGenericTypeDefinition()
                == typeof(List<>))
            {
                var itemType = type.GenericTypeArguments[0];
                Debug.WriteLine($"{itemType.Name}");
            }
        }

        [Test]
        public void AttributeTest()
        {
            using (IDbConnection conn = new SQLiteConnection(ConnectionString))
            {
                conn.DropAllTablesIfExists();
                var sql = conn.CreateTableSchemaDev<ProtoAge4XmlUnit>("UnitId", tableNameOverride: "ProtoUnit");
                conn.Execute(sql);
            }
        }
    }
}