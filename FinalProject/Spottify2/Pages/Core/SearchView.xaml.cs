using Spottify2.Core.Services;
namespace Spottify2.Pages.Core;

public partial class SearchView : ContentView
{
    private readonly NavigationService _navigationService;
    public SearchView(NavigationService navigationService)
	{
		InitializeComponent();
        _navigationService = navigationService;
    }
}