using Dna;
using Fasetto.Word.Core;
using System.Threading.Tasks;
using static Fasetto.Word.DI;
using static Dna.FrameworkDI;

namespace Fasetto.Word
{
    /// <summary>
    /// Extension methods for the <see cref="WebRequestResult"/> class
    /// </summary>
    public static class WebRequestResultExtensions
    {
        /// <summary>
        /// Checks the web request result for any errors, displaying them if there are any
        /// or if we are unauthorized automatically loggind us out
        /// </summary>
        /// <typeparam name="T">The type of Api Response</typeparam>
        /// <param name="response">The response to check</param>
        /// <param name="title">The title of the error dialog if there is an error</param>
        /// <returns>Return true if there was an error, or false if all was ok</returns>
        public static async Task<bool> HandleErrorIfFailedAsync(this WebRequestResult response, string title)
        {
            // If there is no response, bad data, or a response with a error message...
            if (response == null || response.ServerResponse == null || (response.ServerResponse as ApiResponse)?.Successful == false)
            {
                // Default error message
                // TODO: Localize strings
                var message = "Unkown error from server call";

                // If we got a response from the server
                if (response?.ServerResponse is ApiResponse apiResponse)
                    // Set message to servers response
                    message = apiResponse.ErrorMessage;
                // If we have a result but deserialize failed...
                else if (!string.IsNullOrWhiteSpace(response?.RawServerResponse))
                    // Set error message
                    message = $"Unexpected response from server. {response.RawServerResponse}";
                // If we have a result but no server response details at all...
                else if (response != null)
                    // Set message to standard HTTP server response details
                    message = response.ErrorMessage ?? $"{response.StatusDescription} ({response.StatusCode})";

                // If this is an unauthorize response
                if (response?.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    // Log out
                    Logger.LogInformationSource("Logging user out due to unauzorized response from server");

                    // Automatically log the user out
                    await ViewModelSettings.LogoutAsync();
                }
                else
                    // Display error
                    await UI.ShowMessage(new MessageBoxDialogViewModel
                    {
                        // Localize strings
                        Title = title,
                        Message = message,
                    });

                // Return that we have an error
                return true;
            }
            // All was ok, so return false for no error
            return false;
        }
    }
}
