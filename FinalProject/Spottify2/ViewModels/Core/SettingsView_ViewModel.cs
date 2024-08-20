using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using Spottify2.Core;
using Spottify2.Core.Services;



namespace Spottify2.ViewModels.Core
{
    class SettingsView_ViewModel : NotifyPropertyChanged
    {
        
        
        #region PrivateProperties

        private readonly NavigationService _navigationService;

        #endregion

        #region Constructor

        public SettingsView_ViewModel(NavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        #endregion

        #region DisplayedProperties



        #endregion


        #region Commands




        #endregion
    }
}
