using Spottify2.Core;
using Spottify2.Core.Services;
using Spottify2.Pages.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Spottify2.ViewModels.Core
{
    public class HomeView_ViewModel : NotifyPropertyChanged
    {
        #region PrivateProperties
        private readonly NavigationService _navigationService;

        #endregion

        #region Constructor
        public HomeView_ViewModel(NavigationService navigationService)
        {
            _navigationService = navigationService;
            NavigateToSearch = new Command(NavigateToSearchView);
        }

        #endregion

        #region DisplayedProperties



        #endregion


        #region Commands

        public ICommand NavigateToSearch { get; }


        private void NavigateToSearchView()
        {
            _navigationService.NavigateTo(new SearchView(_navigationService));
        }
        #endregion
    }
}
