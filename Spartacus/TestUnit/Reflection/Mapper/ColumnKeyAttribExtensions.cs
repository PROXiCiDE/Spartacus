namespace TestUnit.Reflection.Mapper
{
    public static class ColumnKeyAttribExtensions
    {
        public static bool HasKey(this ColumnKeyAttrib value, ColumnKeyAttrib flag)
        {
            return (value & flag) != 0;
        }
    }
}