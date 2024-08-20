using Spottify2.Core;
using Spottify2.Core.Services;
using Spottify2.Pages.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Spottify2.ViewModels.LayoutElements
{
    class HeaderComponent_ViewModel : NotifyPropertyChanged
    {
        #region PrivateProperties

        private string _SearchString = ""; 


        private readonly NavigationService _navigationService;

        #endregion

        #region Constructor
        public HeaderComponent_ViewModel(NavigationService navigationService)
        {
            _navigationService = navigationService;
            NavigateToProfile = new Command(NavigateToProfileView);
            NavigateToSearch = new Command(NavigateToSearchView);
            NavigateToSettings = new Command(NavigateToSettingsView);
        }

        #endregion

        #region DisplayedProperties

        public string SearchString
        {
            get => _SearchString;
            set
            {
                _SearchString = value;
                OnPropertyChanged(); 
            }
        }

        #endregion

        #region Commands

        public ICommand NavigateToProfile { get; }
        public ICommand NavigateToSettings { get; }
        public ICommand NavigateToSearch { get; }


        private void NavigateToProfileView()
        {
            _navigationService.NavigateTo(new ProfileView(_navigationService));
        }

        private void NavigateToSettingsView()
        {
            _navigationService.NavigateTo(new SettingsView(_navigationService));
        }

        private void NavigateToSearchView()
        {
            _navigationService.NavigateTo(new SearchView(_navigationService, SearchString));
        }


        #endregion
    }
}
