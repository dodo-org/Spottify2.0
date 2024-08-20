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
            NavigateBack = new Command(NavigateBackView);
            NavigateToHome = new Command(NavigateToHomeView);
            NavigateToProfile = new Command(NavigateToProfileView);
            NavigateToSearch = new Command(NavigateToSearchView);
            NavigateToSettings = new Command(NavigateToSettingsView);

            // NavigateBackCommand = new Command(async () => await NavigateBackAsync());
            // NavigateToSearchCommand = new Command(async () => await NavigateToPageAsync(new SearchPage()));
            // NavigateToSettingsCommand = new Command(async () => await NavigateToPageAsync(new SettingsPage()));
            // NavigateToProfileCommand = new Command(async () => await NavigateToPageAsync(new ProfilePage()));
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
        // public ICommand NavigateBackCommand { get; }
        // public ICommand NavigateToSearchCommand { get; }
        // public ICommand NavigateToSettingsCommand { get; }
        // public ICommand NavigateToProfileCommand { get; }
        public ICommand NavigateBack { get; }
        public ICommand NavigateToHome { get; }
        public ICommand NavigateToProfile { get; }
        public ICommand NavigateToSettings { get; }
        public ICommand NavigateToSearch { get; }


        private async void NavigateBackView()
        {
            if (_navigationService.CanGoBack())
            {
                await _navigationService.GoBackAsync();
            }
        }
        // private async Task NavigateToPageAsync(Page page)
        // {
        //     await _navigationService.NavigateToAsync(page);
        // }

        private void NavigateToHomeView()
        {
            _navigationService.NavigateTo(new HomeView(_navigationService));
        }
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
