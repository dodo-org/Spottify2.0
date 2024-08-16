namespace Spottify2.Pages.Login;

public partial class RegisterPage : ContentPage
{
	public RegisterPage()
	{
		InitializeComponent();
	}

    private async void OnRegisterButtonClicked(object sender, EventArgs e)
    {
        string username = UsernameEntry.Text;
        string password = PasswordEntry.Text;
        string confirmPassword = ConfirmPasswordEntry.Text;

        if (password != confirmPassword)
        {
            await DisplayAlert("Fehler", "Die Passwörter stimmen nicht überein.", "OK");
            return;
        }

        // TODO: Implementieren Sie die Registrierung (API-Aufruf oder lokale Speicherung)

        await DisplayAlert("Erfolg", "Ihr Konto wurde erfolgreich erstellt!", "OK");
        // Zurück zur Anmeldeseite
        await Navigation.PopAsync();
    }
}