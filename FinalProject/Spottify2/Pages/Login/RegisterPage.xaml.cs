using Spottify2.ViewModels;

namespace Spottify2.Pages.Login;
public partial class RegisterPage : ContentPage
{
	public RegisterPage()
	{
		InitializeComponent();
		BindingContext = new RegisterPage_ViewModel(Navigation);
	}

    
}