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

        // private void OnPlayClicked(TitleSearchReply_Model item)
        // {
        //     // Handle playing the music file here
        //     // You might want to call a method in your audio playback service
        //     // For example:
        //     // _audioService.PlayAudio(item.FilePath);
        // }
    }
}
