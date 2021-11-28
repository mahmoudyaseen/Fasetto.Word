﻿using Fasetto.Word.Core;
using Dna;

namespace Fasetto.Word
{
    /// <summary>
    /// A shorthand access class to get DI services with nice clean short code
    /// </summary>
    public static class DI
    {
        /// <summary>
        /// A shortcut to access the <see cref="IUIManager"/>
        /// </summary>
        /// instead of IoC.Get<IUIManager>();
        public static IUIManager UI => Framework.Service<IUIManager>();

        /// <summary>
        /// A shortcut to access the <see cref="ApplicationViewModel"/>
        /// </summary>
        public static ApplicationViewModel ViewModelApplication => Framework.Service<ApplicationViewModel>();

        /// <summary>
        /// A shortcut to access the <see cref="SettingsViewModel"/>
        /// </summary>
        public static SettingsViewModel ViewModelSettings => Framework.Service<SettingsViewModel>();

        /// <summary>
        /// A shortcut to access the <see cref="IClientDataStore"/> service
        /// </summary>
        public static IClientDataStore ClientDataStore => Framework.Service<IClientDataStore>();

    }
}
