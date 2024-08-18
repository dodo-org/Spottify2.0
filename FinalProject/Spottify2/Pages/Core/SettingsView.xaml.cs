namespace Spottify2.Pages.Core;
using Spottify2.Core.Services;

public partial class SettingsView : ContentView
{
    private readonly NavigationService _navigationService;
    public SettingsView(NavigationService navigationService)
	{
		InitializeComponent();
        _navigationService = navigationService;
    }
}