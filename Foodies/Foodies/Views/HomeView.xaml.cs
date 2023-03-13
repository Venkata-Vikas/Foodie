using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using FoodiesApp;
using Xamarin.Forms;
namespace Foodies.Views
{	
	public partial class HomeView : ContentPage
	{
        private ObservableCollection<Restaurant> _restaurants;
        public bool IsAdmin;
        public int UserId;
        public HomeView(bool isAdmin, int Userid)
        {
            InitializeComponent();

            if (Device.RuntimePlatform == Device.iOS)
            {
                mainLayout.Margin = new Thickness(0, 50, 0, 0);
            }

            IsAdmin = isAdmin;
            UserId = Userid;
            adminAccess.IsVisible = isAdmin;
        }

        async override protected void OnAppearing()
        {
            var res = await ServiceModule.ServiceModuleInstance.GetRestaurantsAsync();
            if (res != null)
            {
                _restaurants = new ObservableCollection<Restaurant>(res);
                listView.ItemsSource = _restaurants;
            }
            else
            {
                await DisplayAlert("Alert", "No Restaurants available in your area.Try Again!", "OK");
            }
            base.OnAppearing();
        }

        public async void AddRestaurant(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddRestaurantView(UserId));
        }

        public async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selectedItem = (Restaurant)e.Item;
            await Navigation.PushAsync(new RestaurantDetailsView(selectedItem, IsAdmin));
        }

        public void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            listView.ItemsSource = GetSearches(e.NewTextValue);
        }

        public IEnumerable<Restaurant> GetSearches(string searchText = null)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                return _restaurants;
            }
            else
            {
                return _restaurants.Where(x => x.displayName.Contains(searchText));
            }
        }
    }
}

