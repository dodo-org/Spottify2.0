using Spottify2.Pages.Login;

namespace Spottify2
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var mainPage = new NavigationPage(new LoginPage());

            NavigationPage.SetHasNavigationBar(mainPage, false);

            MainPage = mainPage;
        }
    }
}
