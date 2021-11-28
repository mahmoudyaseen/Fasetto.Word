using Fasetto.Word.Core;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Dna;

namespace Fasetto.Word
{
    /// <summary>
    /// A base page for all pages to gain base functionality
    /// </summary>
    /// 1- when a page inherit from that first this page will be collapsed so its invisible
    /// 2- we start the animation that make the page in the right and go to center(default), begin() work concrently
    /// when we start the animation we make the page visible so u can see the animation
    /// invisible -> start animation (from) -> make it visible -> end animation (To)
    public class BasePage : UserControl
    {

        #region Private Properties

        /// <summary>
        /// The view model associated to this page
        /// </summary>
        private Object mViewModel;

        #endregion

        #region Public Properties

        /// <summary>
        /// The animation that play when the page is first loaded
        /// </summary>
        public PageAnimation pageLoadAnimation { get; set; } = PageAnimation.SlideAndFadeInFromRight;

        /// <summary>
        /// The animation that play when the page is  unloaded
        /// </summary>
        public PageAnimation pageUnLoadAnimation { get; set; } = PageAnimation.SlideAndFadeOutToLeft;

        /// <summary>
        /// The time any slide animation take to complete
        /// </summary>
        public float SlideSeconds { get; set; } = 0.4f;

        /// <summary>
        /// A flag to indicate if this page should animate out on load.
        /// Useful for when we are moving the page in another frame
        /// </summary>
        public bool ShouldAnimateOut { get; set; }

        /// <summary>
        /// The view model associated to this page
        /// </summary>
        public Object ViewModelObject
        {
            get => mViewModel;
            set
            {
                // If nothing has changed, return
                if (mViewModel == value)
                    return;

                // Update the View Model
                mViewModel = value;

                // Fire the view model changed method
                OnViewModelChanged();

                // Set the data context for this page
                DataContext = mViewModel; 
            }
        }

        #endregion

        #region Default Constractor

        /// <summary>
        /// Default Constractor
        /// </summary>
        public BasePage()
        {
            // Don't bother animation in design time
            if (DesignerProperties.GetIsInDesignMode(this))
                return;

            // If we are animating in, hide to begin with
            if (pageLoadAnimation != PageAnimation.None)
                Visibility = Visibility.Collapsed;

            // Listen out for the page loading
            Loaded += BasePage_LoadedAsync;

        }

        #endregion

        #region Animation Loaded / UnLoaded

        /// <summary>
        /// Once the page loaded, perform any required animation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BasePage_LoadedAsync(object sender, System.Windows.RoutedEventArgs e)
        {
            // If we are setup to animate out on load
            if (ShouldAnimateOut)
                // animate the page out 
                await AnimateOutAsync();
            // Otherwise...
            else
                // Animate the page in
                await AnimateInAsync();
        }

        /// <summary>
        /// Animate the page in
        /// </summary>
        /// <returns></returns>
        public async Task AnimateInAsync()
        {
            // Make sure we have something to do
            if (pageLoadAnimation == PageAnimation.None)
                return;

            switch (pageLoadAnimation)
            {
                case PageAnimation.SlideAndFadeInFromRight:

                    // Start the animation
                    // when the page don't loaded it has no width, so i path the width of the main window
                    await this.SlideAndFadeInAsync(AnimationSlideInDirection.Right, false, SlideSeconds, size: (int)Application.Current.MainWindow.Width);

                    break;
            }
        }

        /// <summary>
        /// Animate the page out
        /// </summary>
        /// <returns></returns>
        public async Task AnimateOutAsync()
        {
            // Make sure we have something to do
            if (pageUnLoadAnimation == PageAnimation.None)
                return;

            switch (pageUnLoadAnimation)
            {
                case PageAnimation.SlideAndFadeOutToLeft:

                    // Start the animation
                    await this.SlideAndFadeOutAsync(AnimationSlideInDirection.Left, SlideSeconds);

                    break;
            }
        }

        #endregion

        /// <summary>
        /// Fires when view model changes
        /// </summary>
        protected virtual void OnViewModelChanged() { }
    }


    /// <summary>
    /// A base page with added a ViewModel support
    /// </summary>
    public class BasePage<VM> : BasePage
        where VM : BaseViewModel, new()
    {
        #region Public Properties    

        /// <summary>
        /// The view model associated to this page
        /// </summary>
        public VM ViewModel
        {
            get => (VM)ViewModelObject;
            set => ViewModelObject = value;
        }

        #endregion

        #region Default Constractor

        /// <summary>
        /// Default Constractor
        /// </summary>
        public BasePage() : base()
        {
            // If in design time mode
            if (DesignerProperties.GetIsInDesignMode(this))
                // Just use a new instance of VM
                ViewModel = new VM();
            else
                // Create a default view model
                ViewModel = Framework.Service<VM>() ?? new VM();
        }

        /// <summary>
        /// Constractor wuth specific view model
        /// </summary>
        /// <param name="specificViewModel">The specific view model to use, if any</param>
        public BasePage(VM specificViewModel = null) : base()
        {
            // Set specific view model
            if (specificViewModel != null)
                ViewModel = specificViewModel;
            else
            {
                // If in design time mode
                if (DesignerProperties.GetIsInDesignMode(this))
                    // Just use a new instance of VM
                    ViewModel = new VM();
                else
                    // Create a default view model
                    ViewModel = Framework.Service<VM>() ?? new VM();
            }
        }

        #endregion

    }
}
