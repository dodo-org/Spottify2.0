using Spottify2.Core.Services;
using Spottify2.ViewModels.Core;

namespace Spottify2.Pages.Core;

public partial class HomeView : ContentView
{
    private readonly NavigationService _navigationService;
    public HomeView(NavigationService navigationService)
	{
        InitializeComponent(); 
        _navigationService = navigationService;
        BindingContext = new HomeView_ViewModel(_navigationService);
    }

    
}