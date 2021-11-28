using System;
using System.Security;
using System.Threading.Tasks;
using System.Windows.Input;
using Fasetto.Word.Core;
using static Fasetto.Word.DI;


namespace Fasetto.Word
{
    /// <summary>
    /// The view model for a password entry to edit a password
    /// </summary>
    public class PasswordEntryViewModel : BaseViewModel
    {

        #region Public Properties

        /// <summary>
        /// The label to identify what this value is for
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// The fake password display string
        /// </summary>
        public string FakePassword { get; set; }

        /// <summary>
        /// The current password Hint string
        /// </summary>
        public string CurrentPasswordHintText { get; set; }

        /// <summary>
        /// The new password Hint string
        /// </summary>
        public string NewPasswordHintText { get; set; }

        /// <summary>
        /// The confirm password Hint string
        /// </summary>
        public string ConfirmPasswordHintText { get; set; }

        /// <summary>
        /// The current saved password
        /// </summary>
        public SecureString CurrentPassword { get; set; }

        /// <summary>
        /// The current non-commit edited password
        /// </summary>
        public SecureString NewPassword { get; set; }

        /// <summary>
        /// The current non-commit edited confirmed password
        /// </summary>
        public SecureString ConfirmPassword { get; set; }

        /// <summary>
        /// Indicates if the current text is in edit mode
        /// </summary>
        public bool Editing { get; set; }

        /// <summary>
        /// Indicates if the current control is pending an update (in progress)
        /// </summary>
        public bool Working { get; set; }

        /// <summary>
        /// The action to run when saving the text
        /// Returns true if the commit was successful, or false otherwise
        /// </summary>
        public Func<Task<bool>> ComitAction { get; set; }


        #endregion

        #region Public Commands

        /// <summary>
        /// Puts the control into edit mode
        /// </summary>
        public ICommand EditCommand { get; set; }

        /// <summary>
        /// Cancels out of edit mode
        /// </summary>
        public ICommand CancelCommand { get; set; }

        /// <summary>
        /// Commits the edits and save the value
        /// as well as goes back to non-edit mode
        /// </summary>
        public ICommand SaveCommand { get; set; }

        #endregion

        #region Public Constractor

        /// <summary>
        /// Default constractor
        /// </summary>
        public PasswordEntryViewModel()
        {
            // Create commands
            EditCommand = new RelayCommand(Edit);
            CancelCommand = new RelayCommand(Cancel);
            SaveCommand = new RelayCommand(Save);

            // Set default hints
            // TODO: Replace With localization text
            CurrentPasswordHintText = "Current password";
            NewPasswordHintText = "New password";
            ConfirmPasswordHintText = "Confirm password";
        }

        #endregion

        #region Command Methods

        /// <summary>
        /// Puts the control into edit mode
        /// </summary>
        private void Edit()
        {
            // Clear all password
            NewPassword = new SecureString();
            ConfirmPassword = new SecureString();

            // Go into edit mode
            Editing = true;
        }

        /// <summary>
        /// Cancels out of edit mode
        /// </summary>
        private void Cancel()
        {
            Editing = false;
        }

        /// <summary>
        /// Commits the content and exits out of edit mode
        /// </summary>
        private void Save()
        {
            // Store the result of a commit call
            var result = default(bool);

            RunCommandAsync(() => Working, async () =>
            {
                // While working, come out of edit mode
                Editing = false;

                // Try and do the work
                result = ComitAction == null ? true : await ComitAction();
            }).ContinueWith(t =>
            {
                // If we succeeded
                // Nothing to do

                // If we fail
                if (!result)
                {
                    // Go back into edit mode
                    Editing = true;
                }
            });
        }

        #endregion

    }
}
