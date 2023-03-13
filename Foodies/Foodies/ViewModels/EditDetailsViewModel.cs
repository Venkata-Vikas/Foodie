using System;
using FoodiesApp;
using System.ComponentModel;
using Xamarin.Forms;

namespace Foodies.ViewModels
{
	public class EditDetailsViewModel : INotifyPropertyChanged
	{
        public EditDetailsViewModel()
        {
            UpdateCommand = new Command(() =>
            {
                Edit();
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

        private int _id;
        public int id
        {
            get { return _id; }
            set
            {
                _id = value;
                PropertyChanged(this, new PropertyChangedEventArgs("id"));
            }
        }

        private async void Edit()
        {
            Restaurant resturant = new Restaurant()
            {
                id = id,
                displayName = displayName,
                address = address,
                priceForTwo = priceForTwo,
                adminId = adminId
            };
            var res = await ServiceModule.ServiceModuleInstance.UpdateRestaurantAsync(resturant);
            if (res != null)
            {
                await App.Current.MainPage.DisplayAlert("Success", "Updation Successful", "OK");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Updation Error", "Plaese Try Again!", "OK");
            }
        }

        public Command UpdateCommand { get; }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}

