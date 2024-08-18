using Microsoft.Maui.Controls;
using Spottify2.ViewModels;

namespace Spottify2.Pages.Login;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
        InitializeComponent();

        BindingContext = new LoginPage_ViewModel(Navigation);
	}

}