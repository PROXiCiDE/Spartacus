using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using TestUnit.Reflection.Mapper;

namespace TestUnit.Reflection
{
    public class SqlTableWriter
    {
        public Dictionary<ColumnType, string> ColumnTypeDictionary;

        public SqlTableWriter(IEnumerable<PropertyColumnMap> map)
        {
            ColumnTypeDictionary = new Dictionary<ColumnType, string>
            {
                {ColumnType.Blob, "BLOB"},
                {ColumnType.DateTime, "TEXT"},
                {ColumnType.DateTimeOffset, "TEXT"},
                {ColumnType.Guid, "TEXT"},
                {ColumnType.Integer, "INTEGER"},
                {ColumnType.Real, "REAL"},
                {ColumnType.Text, "TEXT"},
                {ColumnType.TimeSpan, "TEXT"},
                {ColumnType.Unknown, "TEXT"}
            };

            var propertyColumnMaps = map as PropertyColumnMap[] ?? map.ToArray();

            Map = propertyColumnMaps;
            InitMap(propertyColumnMaps);
        }

        public IEnumerable<PropertyColumnMap> Map { get; }
        public List<string> TableList { get; }

        private void InitMap(IEnumerable<PropertyColumnMap> map)
        {
            var maps = map as PropertyColumnMap[] ?? map.ToArray();
            var classMaps = maps.Where(x =>
                    x.ColumnKeyOptions.HasOption(ColumnKeyOptions.Class) && x.ColumnType == ColumnType.Unknown &&
                    x.ChildrenInfos.Any())
                .Select(p => p)
                .Distinct()
                .ToArray();

            var enumerableMaps = maps.Where(x =>
                    x.ColumnKeyOptions.HasOption(ColumnKeyOptions.Enumerable) && x.ColumnType != ColumnType.Unknown)
                .Select(p => p)
                .Distinct()
                .ToArray();

            var uniqueMaps = new HashSet<PropertyColumnMap>();
            GetUniqueTableMaps(classMaps, uniqueMaps);
            var tables = CreateTablesForCollections(uniqueMaps, out var processedMaps);

            var collapsed = CollapseChildColumnMaps(classMaps).ToArray();
            var thisTable = CreateTablesForMap(maps, collapsed, processedMaps);

            //foreach (var columnMap in collapsed) Debug.WriteLine(columnMap);
            Debug.WriteLine(thisTable);
        }

        private void GetUniqueTableMaps(IEnumerable<PropertyColumnMap> itemChildrenInfos,
            HashSet<PropertyColumnMap> classMaps)
        {
            foreach (var item in itemChildrenInfos)
            {
                lock (classMaps)
                {
                    if (!item.ColumnKeyOptions.HasOption(ColumnKeyOptions.Class) ||
                        item.ColumnType != ColumnType.Unknown || classMaps.Contains(item)) continue;

                    classMaps.Add(item);
                }

                GetUniqueTableMaps(item.ChildrenInfos, classMaps);
            }
        }

        /// <summary>
        ///     Collapses children properties of a class into the parent class
        /// </summary>
        /// <param name="maps"></param>
        /// <param name="allowDuplicateColumnName"></param>
        /// <returns></returns>
        private IEnumerable<PropertyColumnMap> CollapseChildColumnMaps(IEnumerable<PropertyColumnMap> maps,
            bool allowDuplicateColumnName = true)
        {
            var noCollection = maps.Where(c => !c.ColumnKeyOptions.HasOption(ColumnKeyOptions.Enumerable))
                .ToArray();
            var results = new HashSet<PropertyColumnMap>();

            foreach (var item in noCollection)
            foreach (var itemChild in item.ChildrenInfos)
            {
                var columnName = $"{item.ColumnName}{itemChild.ColumnName}";
                if (!allowDuplicateColumnName)
                    columnName = $"{itemChild.ParentType.Name}{itemChild.ColumnName}";

                var child = new PropertyColumnMap(columnName, item.ParentType, item.ThisType, item.ColumnType,
                    item.KeyAttrib,
                    item.ColumnKeyOptions, item.ChildrenInfos);

                if (!results.Contains(child))
                    results.Add(child);
            }

            return results;
        }

        /// <summary>
        ///     Create a Sqlite table from a map
        /// </summary>
        /// <param name="map"></param>
        /// <param name="propertyColumnBuilderCallback"></param>
        /// <param name="propertyColumnIterationCallback"></param>
        /// <param name="afterIterationAction"></param>
        /// <returns></returns>
        private string CreateTableForMappedColumn(PropertyColumnMap map,
            PropertyColumnBuilderCallback propertyColumnBuilderCallback = null,
            PropertyColumnIterationCallback propertyColumnIterationCallback = null,
            PropertyColumnBuilderCallback afterIterationAction = null)
        {
            var sb = new StringBuilder();

            var childInfos = map.ChildrenInfos.OrderBy(x => ColumnTypeDictionary[x.ColumnType]).ToArray();
            var parent = map.ThisType;
            var lastChild = childInfos.LastOrDefault();

            sb.AppendLine($"CREATE TABLE IF NOT EXISTS '{parent.Name}' (");

            propertyColumnBuilderCallback?.Invoke(childInfos, sb);

            foreach (var column in childInfos)
            {
                var comaSep = lastChild != null && lastChild.Equals(column) ? "" : ",";
                propertyColumnIterationCallback?.Invoke(column, sb, comaSep);
            }

            afterIterationAction?.Invoke(childInfos, sb);

            sb.AppendLine(")");
            return sb.ToString();
        }

        /// <summary>
        ///     Create a Sqlite table from a map
        /// </summary>
        /// <param name="maps"></param>
        /// <param name="propertyColumnBuilderCallback"></param>
        /// <param name="propertyColumnIterationCallback"></param>
        /// <param name="afterIterationAction"></param>
        /// <returns></returns>
        private string CreateTableForMappedColumn(IEnumerable<PropertyColumnMap> maps,
            Action<PropertyColumnMap[], StringBuilder> propertyColumnBuilderCallback = null,
            Action<PropertyColumnMap, StringBuilder, string> propertyColumnIterationCallback = null,
            PropertyColumnBuilderCallback afterIterationAction = null)
        {
            var sb = new StringBuilder();

            var childInfos = maps.OrderBy(x => ColumnTypeDictionary[x.ColumnType]).ToArray();
            var parent = childInfos.FirstOrDefault()?.ParentType;
            var lastChild = childInfos.LastOrDefault();

            if (parent == null)
                return null;

            sb.AppendLine($"CREATE TABLE IF NOT EXISTS '{parent.Name}' (");

            propertyColumnBuilderCallback?.Invoke(childInfos, sb);

            foreach (var column in childInfos)
            {
                var comaSep = lastChild != null && lastChild.Equals(column) ? "" : ",";
                propertyColumnIterationCallback?.Invoke(column, sb, comaSep);
            }

            afterIterationAction?.Invoke(childInfos, sb);

            sb.AppendLine(")");
            return sb.ToString();
        }

        /// <summary>
        /// </summary>
        /// <param name="maps"></param>
        /// <param name="collapsedColumnMaps"></param>
        /// <param name="processedColumnMaps"></param>
        /// <returns></returns>
        private string CreateTablesForMap(IEnumerable<PropertyColumnMap> maps,
            IEnumerable<PropertyColumnMap> collapsedColumnMaps,
            IEnumerable<PropertyColumnMap> processedColumnMaps)
        {
            //var columnMap = maps.FirstOrDefault(x => x.ChildrenInfos.Any())?.ChildrenInfos.FirstOrDefault();

            return CreateTableForMappedColumn(maps,
                (columnMaps, stringBuilder) =>
                {
                    var primaryKeyMaps = columnMaps.FirstOrDefault(x => x.KeyAttrib.HasKey(ColumnKeyAttrib.Primary));
                    var idName = columnMaps.FirstOrDefault(x =>
                        x.ColumnName.Equals("Id", StringComparison.OrdinalIgnoreCase));
                    var lastChild = columnMaps.LastOrDefault();
                    var primaryKeyName = primaryKeyMaps?.ColumnName;

                    if (string.IsNullOrEmpty(primaryKeyName))
                        primaryKeyName = idName?.ColumnName ?? "Id";
                    stringBuilder.AppendLine($"'{primaryKeyName}' INTEGER{(lastChild != null ? "," : "")}");
                },
                (value, stringBuilder, commaString) =>
                {
                    if (processedColumnMaps.Contains(value, new PropertyColumnMapDistinctComparer()))
                        stringBuilder.AppendLine($"'{value.ColumnName}' INTEGER{commaString}");
                    else
                        stringBuilder.AppendLine(
                            $"'{value.ColumnName}' {ColumnTypeDictionary[value.ColumnType]}{commaString}");
                });
        }

        /// <summary>
        ///     Creates a separate table for enumerated collections
        /// </summary>
        /// <param name="maps"></param>
        /// <param name="processedColumnMaps"></param>
        /// <returns></returns>
        private string CreateTablesForCollections(IEnumerable<PropertyColumnMap> maps,
            out HashSet<PropertyColumnMap> processedColumnMaps)
        {
            var list = new List<string>();
            processedColumnMaps = new HashSet<PropertyColumnMap>();

            foreach (var child in maps.Distinct(new PropertyColumnMapDistinctComparer()))
                if (!child.ColumnKeyOptions.HasOption(ColumnKeyOptions.Enumerable))
                {
                    processedColumnMaps.Add(child);
                    list.Add(CreateTableForMappedColumn(child,
                        (columnMaps, stringBuilder) =>
                        {
                            var primaryKeyMaps =
                                columnMaps.FirstOrDefault(x => x.KeyAttrib.HasKey(ColumnKeyAttrib.Primary));
                            var idName = columnMaps.FirstOrDefault(x =>
                                x.ColumnName.Equals("Id", StringComparison.OrdinalIgnoreCase));
                            var lastChild = columnMaps.LastOrDefault();
                            var primaryKeyName = primaryKeyMaps?.ColumnName;

                            if (string.IsNullOrEmpty(primaryKeyName))
                                primaryKeyName = idName?.ColumnName ?? "Id";
                            stringBuilder.AppendLine($"'{primaryKeyName}' INTEGER{(lastChild != null ? "," : "")}");
                        },
                        (map, stringBuilder, commaString) => stringBuilder.AppendLine(
                            $"'{map.ColumnName}' {ColumnTypeDictionary[map.ColumnType]}{commaString}")
                    ));
                }

            return string.Join(Environment.NewLine, list);
        }

        private delegate void PropertyColumnBuilderCallback(PropertyColumnMap[] maps, StringBuilder stringBuilder);

        private delegate void PropertyColumnIterationCallback(PropertyColumnMap columnMap, StringBuilder stringBuilder,
            string commaString);
    }
}