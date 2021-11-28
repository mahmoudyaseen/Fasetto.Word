using System.Collections.Generic;

namespace Fasetto.Word
{
    /// <summary>
    /// A view model for a menu
    /// </summary>
    public class MenuViewModel : BaseViewModel
    {
        /// <summary>
        /// The Items in the menu
        /// </summary>
        public List<MenuItemViewModel> Items { get; set; }

    }
}
