using Dna;

namespace Fasetto.Word.Core
{
    /// <summary>
    /// The IoC Container for our application
    /// </summary>
    public static class CoreDI
    {
        /// <summary>
        /// A shortcut to access the <see cref="IFileManager"/>
        /// </summary>
        /// instead of IoC.Get<IUIManager>();
        public static IFileManager FileManager => Framework.Service<IFileManager>();

        /// <summary>
        /// A shortcut to access the <see cref="ITaskManager"/>
        /// </summary>
        /// instead of IoC.Get<IUIManager>();
        public static ITaskManager TaskManager => Framework.Service<ITaskManager>();


    }
}
