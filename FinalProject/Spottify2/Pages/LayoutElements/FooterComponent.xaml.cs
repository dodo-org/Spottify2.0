namespace Spottify2.Pages.LayoutElements;
using Spottify2.Core.Services;
using Spottify2.ViewModels.LayoutElements;

public partial class FooterComponent : ContentView
{
    private readonly NavigationService _navigationService;
    public FooterComponent(NavigationService navigationService)
	{
		InitializeComponent();
        BindingContext = new FooterComponent_ViewModel(_navigationService);
    }
}