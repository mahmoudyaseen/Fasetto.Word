using Dna;
using Fasetto.Word.Core;
using System.Security;
using System.Threading.Tasks;
using System.Windows.Input;
using static Fasetto.Word.DI;

namespace Fasetto.Word
{
    /// <summary>
    /// the view model for login screen
    /// </summary>
    public class LoginViewModel : BaseViewModel
    {
        #region Public Members

        /// <summary>
        /// The email of the user
        /// </summary>
        public string Email { get; set; }

        // we don't need a password parameter we will bind it directly from ui to the command parameter

        /// <summary>
        /// A flag indicating if the login command is running
        /// </summary>
        public bool LoginIsRunning { get; set; }


        #endregion

        #region Commands

        /// <summary>
        /// The command to login
        /// </summary>
        public ICommand LoginCommand { get; set; }

        /// <summary>
        /// The command to register for a new account
        /// </summary>
        public ICommand RegisterCommand { get; set; }

        #endregion

        #region Constractor

        /// <summary>
        /// Default Constractor
        /// </summary>
        public LoginViewModel()
        {
            // Create commands
            LoginCommand = new RelayParameterizedCommand(async (parameter) => await LoginAsync(parameter));
            RegisterCommand = new RelayCommand(async () => await RegisterAsync());

        }

        #endregion

        /// <summary>
        /// Attempts to log the user in
        /// </summary>
        /// <param name="parameter">The <see cref="SecureString"/> passed in form the view for the user's password</param>
        /// <returns></returns>
        private async Task LoginAsync(object parameter)
        {
            await RunCommandAsync(() => LoginIsRunning, async () => 
            {
                // Call the serever and attemp to login with credentials
                // TODO: Move all URLs and API routes to static class in core
                var result = await WebRequests.PostAsync<ApiResponse<UserProfileDetailsApiModel>>(
                    // Set Url
                    RouteHelpers.GetAbsoluteRoute(ApiRoutes.Login),
                    new LoginCredentialsApiModel
                    {
                        UsernameOrEmail = Email,
                        Password = (parameter as IHavePassword).securePasssword.Unsecure()
                    });

                // If the response has an error
                if (await result.HandleErrorIfFailedAsync("Login Failed"))
                    // We are done
                    return;

                // Ok successfully logged in... now gets user data
                var loginResult = result.ServerResponse.Response;

                // Let the application view model handle what happens
                // with the successful login
                await ViewModelApplication.HandleSuccessfulLoginAsync(loginResult);
            });
        }

        /// <summary>
        /// Takes the user to the register page
        /// </summary>
        /// <returns></returns>
        private async Task RegisterAsync()
        {
            // Go to register page?
            ViewModelApplication.GoToPage(ApplicationPage.Register);

            await Task.Delay(1);
        }


    }
}
