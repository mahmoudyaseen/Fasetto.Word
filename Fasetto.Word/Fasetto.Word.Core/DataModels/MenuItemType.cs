using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fasetto.Word.Core
{
    /// <summary>
    /// The Types of items for a menu item
    /// </summary>
    public enum MenuItemType
    {
        /// <summary>
        /// Shows the menu text and icon
        /// </summary>
        TextAndIcon = 0,
        /// <summary>
        /// Shows a simple divider between the menu items
        /// </summary>
        Divider = 1,
        /// <summary>
        /// Shows the menu text as a header
        /// </summary>
        Header = 2
    }
}
