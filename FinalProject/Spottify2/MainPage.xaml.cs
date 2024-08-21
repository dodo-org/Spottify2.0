using Spottify2.Core.Services;
using Spottify2.Pages.Core;
using Spottify2.Pages.LayoutElements;


namespace Spottify2
{
    public partial class MainPage : ContentPage
    {
        private NavigationService _navigationService;
        int count = 0;

        public MainPage()
        {
            InitializeComponent();

            _navigationService = new NavigationService(MainViewContainer);
            NavigationPage.SetHasNavigationBar(this, false);
            // Start mit der initialen View
            _navigationService.NavigateTo(new HomeView(_navigationService));
            //TokenDisplay.Text = "Token: " + Api_Communication.Instance.Token;

            HeaderContainer.Content = new HeaderComponent(_navigationService);
            FooterContainer.Content = new FooterComponent(_navigationService);
        }


    }

}
