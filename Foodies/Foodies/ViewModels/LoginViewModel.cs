using System;
using System.ComponentModel;
using Foodies.Models;
using Foodies.Views;
using FoodiesApp;
using Xamarin.Forms;

namespace Foodies.ViewModels
{
	public class LoginViewModel : INotifyPropertyChanged
    {
		public LoginViewModel()
        {
            LogInCommand = new Command(() =>
            {
                Login();
            });
        }

        private string _username;
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                PropertyChanged(this, new PropertyChangedEventArgs("username"));
            }
        }
        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                PropertyChanged(this, new PropertyChangedEventArgs("password"));
            }
        }

        private async void Login()
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                await App.Current.MainPage.DisplayAlert("Alert", "Please fill Username and Password", "OK");
            }
            else
            {
                Login login = new Login()
                {
                    userName = Username,
                    password = Password
                };
                var res = await ServiceModule.ServiceModuleInstance.LoginAsync(login);
                if (res != null)
                {
                    await App.Current.MainPage.Navigation.PushAsync(new HomeView(res.isAdmin, res.id));
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Incorrect cerdentials!", "OK");
                }
            }
        }

        public Command LogInCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

