﻿using Fasetto.Word.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Fasetto.Word
{
    /// <summary>
    /// A converter that takes in a <see cref="MenuItemType"/> and returns a <see cref="Visibility"/>
    /// based on the given parameter being the same as the menu item type
    /// </summary>
    public class MenuItemTypeToConverter : BaseValueConverter<MenuItemTypeToConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // IF we have no parameter return invisible
            if (parameter == null)
              return Visibility.Collapsed;

            // Try to convert parameter string to enum
            if (!Enum.TryParse(parameter as string, out MenuItemType type))
                return Visibility.Collapsed;

            // Return Visible if the parameter matches the type
            return (MenuItemType)value == type ? Visibility.Visible : Visibility.Collapsed;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
