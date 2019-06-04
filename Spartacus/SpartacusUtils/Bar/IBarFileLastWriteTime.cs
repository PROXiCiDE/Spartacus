namespace SpartacusUtils.Bar
{
    public interface IBarFileLastWriteTime
    {
        short Hour { get; }
        short Minute { get; }
        short Second { get; }
        short Milliseconds { get; }
        short Year { get; }
        short Month { get; }
        short Day { get; }
        short DayOfWeek { get; }
    }
}