namespace Fasetto.Word.Core
{
    /// <summary>
    /// The relative routes to all Api calls in the server
    /// </summary>
    public static class ApiRoutes
    {

        /// <summary>
        /// The route to get the register Api method
        /// </summary>
        public const string Register = "api/register";

        /// <summary>
        /// The route to get the Login Api method
        /// </summary>
        public const string Login = "api/login";

        /// <summary>
        /// The route to get the VerifyEmail Api method
        /// </summary>
        /// <remarks>
        /// Pass the user id and email token as get parameters
        /// i.e. /api/verify/email?userId=...&emailToken=...
        /// </remarks>
        public const string VerigyEmail = "api/verify/email";

        /// <summary>
        /// The route to get the GetUserProfile Api method
        /// </summary>
        public const string GetUserProfile = "api/user/profile";

        /// <summary>
        /// The route to get the UpdateUserProfile Api method
        /// </summary>
        public const string UpdateUserProfile = "api/user/profile/update";

        /// <summary>
        /// The route to get the UpdateUserPassword Api method
        /// </summary>
        public const string UpdateUserPassword = "api/user/password/update";

    }
}
