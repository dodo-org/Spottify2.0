using MJM_Systems.ApiCalls;
using Spottify2.Core;
using Spottify2.Core.Services;
using Spottify2.Core.Singeltons;
using Spottify2.Models.Reply;
using System.Windows.Input;


namespace Spottify2.ViewModels.Core
{
    class SearchView_ViewModel : NotifyPropertyChanged
    {

        #region PrivateProperties

        private string _SearchString = "";

        private readonly NavigationService _navigationService;

        private List<TitleSearchReply_Model>? _SearchResults = new List<TitleSearchReply_Model>();

        #endregion

        #region Constructor
        public SearchView_ViewModel(NavigationService navigationService, String SearchString)
        {
            _navigationService = navigationService;
            _SearchString = SearchString;

            SearchResults = Api_Communication.Instance.Get<List<TitleSearchReply_Model>>(URL_S.GetTitles + _SearchString);

            PlayCommand = new Command(OnPlayClicked);
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

        public List<TitleSearchReply_Model> SearchResults
        {
            get => _SearchResults;
            set
            {
                _SearchResults = value;
                OnPropertyChanged();
            }
        }
        #endregion


        #region Commands


        #endregion

        public ICommand PlayCommand { get; }

        // public SearchViewModel()
        // {
        //     PlayCommand = new Command<TitleSearchReply_Model>(OnPlayClicked);
        // }
        private void OnPlayClicked(object obj)
        {
            TitleSearchReply_Model Song = (TitleSearchReply_Model)obj;
            Application.Current.MainPage.DisplayAlert("Error" , Song.name, "OK");

            
        }
        
    }
}
