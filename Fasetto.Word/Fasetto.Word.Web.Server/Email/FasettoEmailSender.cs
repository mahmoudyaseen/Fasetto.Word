using Fasetto.Word.Core;
using System.Threading.Tasks;
using static Dna.FrameworkDI;

namespace Fasetto.Word.Web.Server
{
    /// <summary>
    /// Handels sending emails specific to the Fasetto Word Server
    /// </summary>
    public static class FasettoEmailSender
    {
        /// <summary>
        /// Sends a verification email to the specified user
        /// </summary>
        /// <param name="displayName">The users display name (typically first name)</param>
        /// <param name="email">The users email to be verified</param>
        /// <param name="verificationUrl">The Url the user needs to click to verified their email</param>
        /// <returns></returns>
        public static async Task<SendEmailResponse> SendUserVerificationEmailAsync(string displayName, string email, string verificationUrl)
        {
            return await DI.EmailTemplateSender.SendGeneralEmailAsync(new SendEmailDetails
            {
                IsHTML = true,
                FromEmail = Configuration["FasettoSettings:SendEmailFromEmail"],
                FromName = Configuration["FasettoSettings:SendEmailFromName"],
                ToEmail = email,
                ToName = displayName,
                Subject = "Verify Your Email - Fasetto Word"
            },
            "Verify Email",
            $"Hi {displayName ?? "stranger"},",
            "Thanks for creating an account with us.<br/>To continue please verify your email with us.",
            "Verify Email",
            verificationUrl
            );
        }
    }
}
