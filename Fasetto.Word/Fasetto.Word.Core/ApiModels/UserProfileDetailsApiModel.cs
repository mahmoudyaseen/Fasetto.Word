namespace Fasetto.Word.Core
{
    /// <summary>
    /// The result of a login request or get user profile details request via API
    /// </summary>
    public class UserProfileDetailsApiModel
    {

        #region Public Properties

        /// <summary>
        /// The authentication token used to stay authonticated through future requests
        /// </summary>
        /// <remarks>The token is only provided when called from the login method</remarks>
        public string Token { get; set; }

        /// <summary>
        /// The user's first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The user's last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The user's user name 
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// The user's Email
        /// </summary>
        public string Email { get; set; }

        #endregion

        #region Public Healper Methods

        /// <summary>
        /// Creates a new <see cref="LoginCredentialsDataModel"/>
        /// from this model
        /// </summary>
        /// <returns></returns>
        public LoginCredentialsDataModel ToLoginCredentialsDataModel()
        {
            return new LoginCredentialsDataModel
            {
                Email = this.Email,
                FirstName = this.FirstName,
                LastName = this.LastName,
                Username = this.Username,
                Token = this.Token,
            };
        }

        #endregion

    }
}
