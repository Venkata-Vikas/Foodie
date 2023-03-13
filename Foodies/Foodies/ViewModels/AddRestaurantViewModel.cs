using System;
using System.ComponentModel;
using FoodiesApp;
using Xamarin.Forms;

namespace Foodies.ViewModels
{
	public class AddRestaurantViewModel : INotifyPropertyChanged
	{
        public AddRestaurantViewModel()
        {
            RegisterCommand = new Command(() =>
            {
                Register();
            });
        }

        private string _displayName;
        public string displayName
        {
            get { return _displayName; }
            set
            {
                _displayName = value;
                PropertyChanged(this, new PropertyChangedEventArgs("displayName"));
            }
        }

        private string _address;
        public string address
        {
            get { return _address; }
            set
            {
                _address = value;
                PropertyChanged(this, new PropertyChangedEventArgs("address"));
            }
        }

        private int _priceForTwo;
        public int priceForTwo
        {
            get { return _priceForTwo; }
            set
            {
                _priceForTwo = value;
                PropertyChanged(this, new PropertyChangedEventArgs("priceForTwo"));
            }
        }

        private int _adminId;
        public int adminId
        {
            get { return _adminId; }
            set
            {
                _adminId = value;
                PropertyChanged(this, new PropertyChangedEventArgs("adminId"));
            }
        }

        private async void Register()
        {
            if (string.IsNullOrEmpty(displayName) || string.IsNullOrEmpty(address) || string.IsNullOrEmpty(priceForTwo.ToString()))
            {
                await App.Current.MainPage.DisplayAlert("Alret", "Please fill all the details, Try Again!", "OK"); ;
            }
            else
            {
                var res = await ServiceModule.ServiceModuleInstance.PostRestaurantAsync(displayName, address, priceForTwo, adminId);
                if (res != null)
                {
                    await App.Current.MainPage.DisplayAlert("Register", "Registered!", "OK");
                    displayName = "";
                    address = "";
                    priceForTwo = 0;
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Register Fail", "Plaese Try Again!", "OK");
                }
            }
        }

        public Command RegisterCommand { get; }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}

