using Fasetto.Word.Core;
using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using static Dna.FrameworkDI;

namespace Fasetto.Word.Web.Server
{
    /// <summary>
    /// Sends emails using the SendGrid service
    /// </summary>
    public class SendGridEmailSender : IEmailSender
    {
        public async Task<SendEmailResponse> SendEmailAsync(SendEmailDetails details)
        {
            // Get the secret key
            var apiKey = Configuration["SendGridKey"];

            // Create a new SendGrid client
            var client = new SendGridClient(apiKey);

            // From
            var from = new EmailAddress(details.FromEmail, details.FromName);

            // To
            var to = new EmailAddress(details.ToEmail, details.ToName);

            // Subject
            var subject = details.Subject;

            //var plainTextContent = "and easy to do anywhere, even with C#";
            //var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";

            // Content
            var content = details.Content;

            // Create email class ready to send
            var msg = MailHelper.CreateSingleEmail(
                from,
                to,
                subject,
                // Plain Content
                details.IsHTML ? null : details.Content , 
                // HTML Content
                details.IsHTML ? details.Content : null);

            // Finally, send the email
            var response = await client.SendEmailAsync(msg);

            // If we succeeded...
            if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
                // Return successful response
                return new SendEmailResponse();

            // Otherwise, it failed...

            try
            {
                // Get the result in the boady
                var boadyResult = await response.Body.ReadAsStringAsync();

                // Deserialize the response
                var sendGridResponse = JsonConvert.DeserializeObject<SendGridResponse>(boadyResult);

                // Add any error to the response
                var errorResponse = new SendEmailResponse
                {
                    Errors = sendGridResponse?.Errors.Select(f => f.Message).ToList()
                };

                // Make sure we have at least one error
                if (errorResponse.Errors == null || errorResponse.Errors.Count == 0)
                    // Add an unknown error
                    // TODO: Localization
                    errorResponse.Errors = new List<string>(new[] { "Unkown error from email sending service" });

                // Return the response
                return errorResponse;
            }
            catch(Exception ex)
            {
                // TODO: Localization

                // Breake if we are debugging
                if (Debugger.IsAttached)
                    Debugger.Break();

                // If something unexpected happened, return message
                return new SendEmailResponse
                {
                    Errors = new List<string>(new[] { "Unkown error occurred" } )
                };
            }

        }
    }
}
