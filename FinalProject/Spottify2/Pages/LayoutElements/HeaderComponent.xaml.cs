using Spottify2.Core.Services;
using Spottify2.ViewModels.LayoutElements;

namespace Spottify2.Pages.LayoutElements;

public partial class HeaderComponent : ContentView
{
	public HeaderComponent(NavigationService _navigationService)
	{
		InitializeComponent();
		BindingContext = new HeaderComponent_ViewModel(_navigationService);
	}
}