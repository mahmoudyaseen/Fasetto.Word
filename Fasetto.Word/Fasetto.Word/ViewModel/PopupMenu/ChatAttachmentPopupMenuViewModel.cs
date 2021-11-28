using Fasetto.Word.Core;
using System.Collections.Generic;

namespace Fasetto.Word
{
    /// <summary>
    /// A view model for any popup menus
    /// </summary>
    public class ChatAttachmentPopupMenuViewModel : BasePopupViewModel
    {

        #region Constractor

        /// <summary>
        /// Default constractor
        /// </summary>
        public ChatAttachmentPopupMenuViewModel()
        {
            Content = new MenuViewModel
            {
                Items = new List<MenuItemViewModel>(new[]
                {
                    new MenuItemViewModel{ Text = "Attach a file...", Type = MenuItemType.Header},
                    new MenuItemViewModel{ Text = "From Computer", Icon = IconType.File },
                    new MenuItemViewModel{ Text = "From Picture", Icon = IconType.Picture },
                })
            };
        }

        #endregion
    }
}
