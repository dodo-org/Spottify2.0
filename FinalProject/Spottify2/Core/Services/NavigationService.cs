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
    }
}
