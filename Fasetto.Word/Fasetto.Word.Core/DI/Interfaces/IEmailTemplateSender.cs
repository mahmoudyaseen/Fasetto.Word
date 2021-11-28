using System.Threading.Tasks;

namespace Fasetto.Word.Core
{
    /// <summary>
    /// Sends emails using the <see cref="IEmailSender"/> and creating the HTML
    /// email from specific templates
    /// </summary>
    public interface IEmailTemplateSender
    {
        /// <summary>
        /// Sends an email with the given details using the general template
        /// </summary>
        /// <param name="details">The email message details, Note the content property is ignored and replaced with the template</param>
        /// <param name="title">The title</param>
        /// <param name="content1">The first line content</param>
        /// <param name="content2">The second line content</param>
        /// <param name="buttonText">The button text</param>
        /// <param name="buttonUrl">The button Url</param>
        /// <returns></returns>
        Task<SendEmailResponse> SendGeneralEmailAsync(SendEmailDetails details, string title, string content1, string content2, string buttonText, string buttonUrl);
    }
}
