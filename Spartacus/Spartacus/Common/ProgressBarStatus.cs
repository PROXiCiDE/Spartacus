using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spartacus.Common
{
    public class ProgressBarStatus
    {
        public string StatusText { get; }
        public double CurrentValue { get; }
        public double MaximumValue { get; }

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
    }
}
