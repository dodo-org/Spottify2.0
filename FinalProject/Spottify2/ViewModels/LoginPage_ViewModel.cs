using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Spottify2.Core;
using Spottify2.Models.ViewLayoutsModels;
using Microsoft.Maui.Controls;
using Spottify2.Models.Request;
using Spottify2.Core.Singeltons;
using MJM_Systems.ApiCalls;


namespace Spottify2.ViewModels
{
    public class LoginPage_ViewModel : NotifyPropertyChanged
    {
        private LoginLayoutModel _Propertys;
        private readonly INavigation _navigation;

        #region Constructor
        public LoginPage_ViewModel()
        {
            _Propertys = new LoginLayoutModel();
            LoginCommand = new Command(Login);
            RegisterCommand = new Command(Register);
        }
        public LoginPage_ViewModel(INavigation navigation)
        {
            _Propertys = new LoginLayoutModel();
            LoginCommand = new Command(Login);
            RegisterCommand = new Command(Register);

            _navigation = navigation;
        }

        #endregion

        #region DisplayedProperties


        public string UserName
        {
            get => _Propertys.UserName;
            set
            {
                _Propertys.UserName = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get => _Propertys.Password;
            set
            {
                _Propertys.Password = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Commands
        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }


        private async void Login()
        {
            if(!string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password))
            {
                //Todo Api Request 
                LoginRequest_Model loginRequest = new LoginRequest_Model
                {
                    UserName = UserName,
                    Password = Password
                };

                bool? Reply = await Api_Communication.Instance.Login(loginRequest.ToDictionary());

                if (Reply == true)
                {
                    await _navigation.PushAsync(new MainPage());
                }
                else
                {

                   Application.Current.MainPage.DisplayAlert("Error", "Falsche Benutzerdaten", "OK");
                }

                
            }
            else
            {
                Application.Current.MainPage.DisplayAlert("Error", "Bitte füllen Sie alle Felder aus.", "OK");

            }

        }
        

        private async void Register()
        {
            // Navigieren Sie zur Registrierungsseite
            await _navigation.PushAsync(new Pages.Login.RegisterPage());
        }
        #endregion

        

    }
}
