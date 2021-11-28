namespace Fasetto.Word.Core
{
    /// <summary>
    /// The details to change for a users password from an Api client call
    /// </summary>
    public class UpdateUserPasswordApiModel
    {
        /// <summary>
        /// The current user password
        /// </summary>
        public string CurrentPassword { get; set; }

        /// <summary>
        /// The new user password
        /// </summary>
        public string NewPassword { get; set; }
    }
}
