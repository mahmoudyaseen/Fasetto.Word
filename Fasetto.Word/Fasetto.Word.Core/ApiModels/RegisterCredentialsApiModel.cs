namespace Fasetto.Word.Core
{
    /// <summary>
    /// The credentials for an API client to Register on the server 
    /// </summary>
    public class RegisterCredentialsApiModel
    {
        #region Public Properties

        /// <summary>
        /// The user's user name
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// The user's user email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The user's user first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The user's user last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The user's password
        /// </summary>
        public string Password { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constractor
        /// </summary>
        public RegisterCredentialsApiModel()
        {

        }

        #endregion
    }
}
