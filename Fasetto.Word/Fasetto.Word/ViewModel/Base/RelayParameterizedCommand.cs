using System;
using System.Windows.Input;

namespace Fasetto.Word
{
    /// <summary>
    /// A Basic command to run an Action
    /// </summary>
    // so this command run whatever function we pass in constractor
    public class RelayParameterizedCommand : ICommand
    {
        #region Private Members

        /// <summary>
        /// The Action to run
        /// </summary>
        // Action like a function without parameters or return, it just do something
        private Action<object> mAction;

        #endregion

        #region Public Events

        /// <summary>
        /// the event that fired when the <see cref="CanExecute(object)"/> method has changed
        /// </summary>
        public event EventHandler CanExecuteChanged = (sender, e) => { };

        #endregion

        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public RelayParameterizedCommand(Action<object> action)
        {
            mAction = action;
        }

        #endregion

        #region Command Methods

        /// <summary>
        /// A Relay Command can always execute 
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Execute the command Action
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            mAction(parameter);
        }

        #endregion
    }
}
