namespace TestUnit.Reflection.Mapper
{
    public static class ColumnKeyOptionsExtensions
    {
        public static bool HasOption(this ColumnKeyOptions value, ColumnKeyOptions flag)
        {
            return (value & flag) != 0;
        }
    }
}