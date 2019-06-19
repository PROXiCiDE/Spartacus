namespace TestUnit.Reflection.Mapper
{
    public static class ColumnKeyAttribExtensions
    {
        public static bool HasFlag(this ColumnKeyAttrib value, ColumnKeyAttrib flag)
        {
            return (value & flag) != 0;
        }
    }
}