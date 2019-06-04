using System;
using ProjectCeleste.GameFiles.Tools.Bar;

namespace SpartacusUtils.Bar
{
    public class BarFileLastWriteTime : IBarFileLastWriteTime
    {
        private short _year;

        public BarFileLastWriteTime(DateTime dateTime)
        {
            Hour = (short) dateTime.Hour;
            Minute = (short) dateTime.Minute;
            Second = (short) dateTime.Second;
            Milliseconds = (short) dateTime.Millisecond;
            Year = (short) dateTime.Year;
            Month = (short) dateTime.Month;
            Day = (short) dateTime.Day;
            DayOfWeek = (short) dateTime.DayOfWeek;
        }

        public BarFileLastWriteTime()
        {
        }

        public BarFileLastWriteTime(BarEntryLastWriteTime lastWriteTime)
        {
            Hour = lastWriteTime.Hour;
            Minute = lastWriteTime.Minute;
            Second = lastWriteTime.Second;
            Milliseconds = lastWriteTime.Msecond;
            Year = lastWriteTime.Year;
            Month = lastWriteTime.Month;
            Day = lastWriteTime.Day;
            DayOfWeek = lastWriteTime.DayOfWeek;
        }

        public short Hour { get; }
        public short Minute { get; }
        public short Second { get; }
        public short Milliseconds { get; }

        public short Year
        {
            get => _year;
            set
            {
                if (value < 2002)
                    _year = (short)DateTime.Today.Year;
                else if (value >= 2100)
                    _year = 2100;
                else
                    _year = value;
            }
        }

        public short Month { get; }
        public short Day { get; }
        public short DayOfWeek { get; }

        public override string ToString()
        {
            var dateTime = new DateTime(Year, Month, Day, Hour, Minute, Second, Milliseconds);
            return $"{dateTime.ToShortTimeString()}  {dateTime.ToShortDateString()}";
        }
    }
}