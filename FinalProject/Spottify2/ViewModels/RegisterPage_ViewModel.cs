using Spottify2.Core;
using Spottify2.Core.Singeltons;
using Spottify2.Models.Reply;
using Spottify2.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Spottify2.Resources.Languages.Buttons;

namespace Spottify2.ViewModels
{
    public class RegisterPage_ViewModel : NotifyPropertyChanged
    {
        #region PrivateProperties
        
        private string _userName = "";
        private string _password = "";
        private string _confirmPassword = "";
        private string _email = "";

        private string _RegisterButton_Text = Buttons.Register;



        private readonly INavigation _navigation;
        #endregion

        #region Constructor
        
        public RegisterPage_ViewModel(INavigation navigation)
        {
            RegisterCommand = new Command(Register);
            _navigation = navigation;
        }

        #endregion

        #region DisplayedProperties

        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                _confirmPassword = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Commands

        public ICommand RegisterCommand { get; }

        private async void Register()
        {
            
            if (
                !string.IsNullOrEmpty(_userName) && 
                !string.IsNullOrEmpty(_password) && 
                !string.IsNullOrEmpty(_confirmPassword) && 
                !string.IsNullOrEmpty(_email))
            {
                if(_password != _confirmPassword)
                {
                    Application.Current.MainPage.DisplayAlert("Error", "Passwords need to be the same!", "OK");
                    return;
                }
                RegisterRequest_Model registerRequest = new RegisterRequest_Model
                {
                    UserName = UserName,
                    Password = Password,
                    Email = Email
                };

                RegistrationReply_Model? registrationReply = await Api_Communication.Instance.Register(registerRequest.ToDictionary());

                if(registrationReply == null)
                {
                    Application.Current.MainPage.DisplayAlert("Error", "Critical ERROR!", "OK");
                }
                else if (registrationReply.Response == RegistrationResponses.Success)
                {
                    Application.Current.MainPage.DisplayAlert("Erfolg", "Successful registration", "OK");
                    await _navigation.PushAsync(new MainPage());
                }
                else if(registrationReply.Response == RegistrationResponses.UsernameExists)
                {
                    Application.Current.MainPage.DisplayAlert("Error", "Username already exists!", "OK");
                }
                else if(registrationReply.Response == RegistrationResponses.EmailExists)
                {
                    Application.Current.MainPage.DisplayAlert("Error", "Email already exists!", "OK");
                }
                
            }
            else
            {
                Application.Current.MainPage.DisplayAlert("Error", "Please fill out all forms!", "OK");
            }
        }

        #endregion
    }
}
