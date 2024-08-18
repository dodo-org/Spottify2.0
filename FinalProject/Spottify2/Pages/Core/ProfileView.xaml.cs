using Spottify2.Core.Services;
using Spottify2.ViewModels.Core;

namespace Spottify2.Pages.Core;

public partial class ProfileView : ContentView
{
	private readonly NavigationService _navigationService;
	public ProfileView(NavigationService navigationService)
	{
		InitializeComponent();
		BindingContext = new ProfileView_ViewModel(_navigationService);
	}
}