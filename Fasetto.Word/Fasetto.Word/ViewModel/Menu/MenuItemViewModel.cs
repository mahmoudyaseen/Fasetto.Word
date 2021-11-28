using Fasetto.Word.Core;

namespace Fasetto.Word
{
    /// <summary>
    /// A view model for a menu item
    /// </summary>
    public class MenuItemViewModel : BaseViewModel
    {
        /// <summary>
        /// The text to display for the menu item
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// The icon for this menu item
        /// </summary>
        public IconType Icon { get; set; }

        /// <summary>
        /// The type of the menu item
        /// </summary>
        /// default = 0
        public MenuItemType Type { get; set; }

    }
}
