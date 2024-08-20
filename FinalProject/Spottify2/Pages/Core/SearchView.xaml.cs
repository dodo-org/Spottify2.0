using Spottify2.Core.Services;
using Spottify2.ViewModels.Core;
namespace Spottify2.Pages.Core;

public partial class SearchView : ContentView
{
    private readonly NavigationService _navigationService;
    public SearchView(NavigationService navigationService, string SearchView)
	{
		InitializeComponent();
        _navigationService = navigationService;
        BindingContext = new SearchView_ViewModel(navigationService, SearchView);
    }
}