using Spottify2.Core;
using Spottify2.Core.Services;

namespace Spottify2.ViewModels.Core
{
    class SearchView_ViewModel : NotifyPropertyChanged
    {

        #region PrivateProperties

        private string _SearchString = "";

        private readonly NavigationService _navigationService;

        #endregion

        #region Constructor
        public SearchView_ViewModel(NavigationService navigationService, String SearchString)
        {
            _navigationService = navigationService;
            _SearchString = SearchString;
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




        #endregion
    }
}
