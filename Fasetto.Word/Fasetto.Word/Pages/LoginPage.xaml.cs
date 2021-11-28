using Fasetto.Word.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Fasetto.Word
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    /// to make it inherit from base u should change it in XAML also <local:BasePage insted of <Page
    public partial class LoginPage : BasePage<LoginViewModel>, IHavePassword
    {
        #region Constractor

        /// <summary>
        /// Default constractor
        /// </summary>
        public LoginPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constractor with specific view model
        /// </summary>
        public LoginPage(LoginViewModel specificViewModel) : base(specificViewModel)
        {
            InitializeComponent();
        }

        #endregion

        /// <summary>
        /// The secure password for the login, implementation of getter that in <see cref="IHavePassword"/>
        /// </summary>
        public SecureString securePasssword => this.PasswordText.SecurePassword;
    }
}
