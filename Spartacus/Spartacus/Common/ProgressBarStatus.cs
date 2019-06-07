namespace Spartacus.Common
{
    public class ProgressBarStatus
    {
        public ProgressBarStatus(string statusText, double currentValue, double maximumValue)
        {
            StatusText = statusText;
            CurrentValue = currentValue;
            MaximumValue = maximumValue;
        }

        public ProgressBarStatus()
        {
            StatusText = "";
            CurrentValue = 0;
            MaximumValue = 0;
        }

        public string StatusText { get; }
        public double CurrentValue { get; }
        public double MaximumValue { get; }
    }
}