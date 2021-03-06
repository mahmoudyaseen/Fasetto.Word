using Fasetto.Word.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fasetto.Word
{
    /// <summary>
    /// The design-time data for a <see cref="MenuViewModel"/>
    /// </summary>
    public class MenuDesignModel : MenuViewModel
    {
        #region Singleton

        /// <summary>
        /// A single instance of the design model
        /// </summary>
        /// it just getter in C#7
        public static MenuDesignModel Instance => new MenuDesignModel();

        #endregion



        #region Constrator

        /// <summary>
        /// Default constractor
        /// </summary>
        public MenuDesignModel()
        {
            Items = new List<MenuItemViewModel>(new [] 
            { 
                new MenuItemViewModel{ Type = MenuItemType.Header, Text = "Design time header..."},
                new MenuItemViewModel{ Text = "Menu item 1", Icon = IconType.File },
                new MenuItemViewModel{ Text = "Menu item 2", Icon = IconType.Picture },
            });
        }

        #endregion
    }
}
