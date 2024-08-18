using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spottify2.Core;
using Spottify2.Core.Services;

namespace Spottify2.ViewModels.Core
{
    class ProfileView_ViewModel : NotifyPropertyChanged
    {

        #region PrivateProperties

        private readonly NavigationService _navigationService;

        #endregion

        #region Constructor
        public ProfileView_ViewModel(NavigationService navigationService)
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
