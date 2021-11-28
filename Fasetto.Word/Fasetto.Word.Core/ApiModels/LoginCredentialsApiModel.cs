namespace Fasetto.Word.Core
{
    /// <summary>
    /// The credentials for an API client to log into the server and receive the token back
    /// </summary>
    public class LoginCredentialsApiModel
    {
        #region Public Properties

        /// <summary>
        /// The user's user name or email
        /// </summary>
        public string UsernameOrEmail { get; set; }

        /// <summary>
        /// The user's password
        /// </summary>
        public string Password { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constractor
        /// </summary>
        public LoginCredentialsApiModel()
        {

        }

        #endregion
    }
}
