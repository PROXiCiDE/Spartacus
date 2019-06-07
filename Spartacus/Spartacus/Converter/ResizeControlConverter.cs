﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace Spartacus.Converter
{
    public class ResizeControlConverter : IValueConverter
    {
        public double Minimum { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var dvalue = (double) value;
            if (dvalue > Minimum)
                Minimum = dvalue;
            else if (dvalue < Minimum)
                dvalue = Minimum;
            return dvalue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}