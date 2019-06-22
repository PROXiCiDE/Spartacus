namespace TestUnit.Reflection.Mapper
{
    public static class ColumKeyOptionsExtensions
    {
        public static bool HasOption(this ColumKeyOptions value, ColumKeyOptions flag)
        {
            return (value & flag) != 0;
        }
    }
}