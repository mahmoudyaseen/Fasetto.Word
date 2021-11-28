using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fasetto.Word
{
    /// <summary>
    /// The design-time data for a <see cref="SettingsViewModel"/>
    /// </summary>
    public class SettingsDesignModel : SettingsViewModel
    {

        #region Singleton

        /// <summary>
        /// A single instance of the design model
        /// </summary>
        /// it just getter in C#7
        public static SettingsDesignModel Instance => new SettingsDesignModel(); 

        #endregion

        #region Constractor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public SettingsDesignModel()
        {
            FirstName = new TextEntryViewModel { Label = "First Name", OriginalText = "Mahmoud" };
            LastName = new TextEntryViewModel { Label = "Last Name", OriginalText = "Yaseen" };
            Username = new TextEntryViewModel { Label = "Username", OriginalText = "Yaseen" };
            Password = new PasswordEntryViewModel { Label = "Password", FakePassword = "********" };
            Email= new TextEntryViewModel { Label = "Email", OriginalText = "Email@gmail.com" };
        }

        #endregion

    }
}
