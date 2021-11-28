using Dna;
using Fasetto.Word.Core;
using Microsoft.Extensions.DependencyInjection;

namespace Fasetto.Word
{
    /// <summary>
    /// Extension methods for <see cref="FrameworkConstruction"/>
    /// </summary>
    public static class FrameworkConstructionExtensions
    {
        /// <summary>
        /// Injects the view models needed for Fesatto Word application
        /// </summary>
        /// <param name="construction"></param>
        /// <returns></returns>
        public static FrameworkConstruction AddFasettoWorldViewModels(this FrameworkConstruction construction)
        {
            // Bind to a single instance of application view model
            construction.Services.AddSingleton<ApplicationViewModel>();

            // Bind to a single instance of settings view model
            construction.Services.AddSingleton<SettingsViewModel>();

            // Return the construction for chaning
            return construction;
        }

        /// <summary>
        /// Injects the Fasetto Word client application services needed
        /// for the Fasetto Word application
        /// </summary>
        /// <param name="construction"></param>
        /// <returns></returns>
        public static FrameworkConstruction AddFasettoWordClienServices(this FrameworkConstruction construction)
        {

            // Bind a task manager
            construction.Services.AddTransient<ITaskManager, BaseTaskManager>();

            // Bind a file manager
            construction.Services.AddTransient<IFileManager, BaseFileManager>();

            // Bind a UI manager
            construction.Services.AddTransient<IUIManager, UIManager>();

            // Return the construction for chaning
            return construction;
        }
    }
}
