using Microsoft.Maui.Controls;

namespace Spottify2.Pages.Login;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
        InitializeComponent();
	}

    private async void OnLoginButtonClicked(object sender, EventArgs e)
    {
        //string username = UsernameEntry.Text;
        //string password = PasswordEntry.Text;

        //// TODO: Implementieren Sie die Authentifizierung (API-Aufruf oder lokale Überprüfung)

        //if (/* Überprüfen, ob die Anmeldedaten korrekt sind */)
        //{
        //    await DisplayAlert("Erfolg", "Sie haben sich erfolgreich angemeldet!", "OK");
        //    // Weiter zur Hauptseite der App
        //    await Navigation.PushAsync(new MainPage());
        //}
        //else
        //{
        //    await DisplayAlert("Fehler", "Ungültiger Benutzername oder Passwort.", "OK");
        //}
    }

    private async void OnRegisterButtonClicked(object sender, EventArgs e)
    {
        // Navigieren Sie zur Registrierungsseite
        await Navigation.PushAsync(new RegisterPage());
    }



}