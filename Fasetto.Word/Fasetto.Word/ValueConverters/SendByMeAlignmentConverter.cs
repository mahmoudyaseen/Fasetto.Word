﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Fasetto.Word
{
    /// <summary>
    /// A converter that takes in a boolean if a message was sent by me, and aligns to the right
    /// IF not sent by me, aligns to the left
    /// </summary>
    public class SendByMeAlignmentConverter : BaseValueConverter<SendByMeAlignmentConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(parameter == null)
                return (bool)value ? HorizontalAlignment.Right : HorizontalAlignment.Left; 
            else
                return (bool)value ? HorizontalAlignment.Left : HorizontalAlignment.Right;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
