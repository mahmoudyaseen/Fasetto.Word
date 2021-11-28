using Dna;
using Fasetto.Word.Core;
using System.Security;
using System.Threading.Tasks;
using System.Windows.Input;
using static Fasetto.Word.DI;

namespace Fasetto.Word
{
    /// <summary>
    /// the view model for register screen
    /// </summary>
    public class RegisterViewModel : BaseViewModel
    {

        #region Public Members

        /// <summary>
        /// The Username of the user
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// The email of the user
        /// </summary>
        public string Email { get; set; }

        // we don't need a password parameter we will bind it directly from ui to the command parameter

        /// <summary>
        /// A flag indicating if the register command is running
        /// </summary>
        public bool RegisterIsRunning { get; set; }


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
        public RegisterViewModel()
        {
            // Create commands
            RegisterCommand = new RelayParameterizedCommand(async (parameter) => await RegisterAsync(parameter));
            LoginCommand = new RelayCommand(async () => await LoginAsync());

        }

        #endregion

        /// <summary>
        /// Attempts to register a new user
        /// </summary>
        /// <param name="parameter">The <see cref="SecureString"/> passed in form the view for the user's password</param>
        /// <returns></returns>
        private async Task RegisterAsync(object parameter)
        {
            await RunCommandAsync(() => RegisterIsRunning, async () => 
            {
                // Call the serever and attemp to Register with the provided credentials
                // TODO: Move all URLs and API routes to static class in core
                var result = await WebRequests.PostAsync<ApiResponse<RegisterResultApiModel>>(
                    // Set Url
                    RouteHelpers.GetAbsoluteRoute(ApiRoutes.Register),
                    new RegisterCredentialsApiModel
                    {
                        Username = Username,
                        Email = Email,
                        Password = (parameter as IHavePassword).securePasssword.Unsecure()
                    });

                // If the response has an error
                if (await result.HandleErrorIfFailedAsync("Register Failed"))
                    // We are done
                    return;

                // Ok successfully registered (and logged in)... now gets user data
                var loginResult = result.ServerResponse.Response;

                // Let the application view model handle what happens
                // with the successful login
                await ViewModelApplication.HandleSuccessfulLoginAsync(loginResult);
            });
        }

        /// <summary>
        /// Takes the user to the Login page
        /// </summary>
        /// <returns></returns>
        private async Task LoginAsync()
        {
            // Go to register page?
            ViewModelApplication.GoToPage (ApplicationPage.Login);

            await Task.Delay(1);
        }


    }
}
