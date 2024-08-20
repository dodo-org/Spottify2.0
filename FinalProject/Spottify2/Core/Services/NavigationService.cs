using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spottify2.Core.Services
{
    public class NavigationService
    {
        private ContentView _mainViewContainer;

        public NavigationService(ContentView mainViewContainer)
        {
            _mainViewContainer = mainViewContainer;
        }

        public void NavigateTo(View newView)
        {
            _mainViewContainer.Content = newView;
        }

        private readonly INavigation _navigation;

        // public NavigationService(INavigation navigation)
        // {
        //     _navigation = navigation;
        // }

        // public async Task NavigateToAsync(Page newPage)
        // {
        //     await _navigation.PushAsync(newPage);
        // }

        public bool CanGoBack()
        {
            return _navigation.NavigationStack.Count > 1;
        }

        public Task GoBackAsync()
        {
            if (CanGoBack())
            {
                return _navigation.PopAsync();
            }
            return Task.CompletedTask;
        }
    }
}
