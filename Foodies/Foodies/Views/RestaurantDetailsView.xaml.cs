using System;
using System.Collections.Generic;
using FoodiesApp;
using Xamarin.Forms;

namespace Foodies.Views
{	
	public partial class RestaurantDetailsView : ContentPage
	{
        public List<Items> _foodItems;
        public Restaurant data;
        public RestaurantDetailsView(Restaurant restaurant, bool isAdmin)
        {
            InitializeComponent();
            BindingContext = this;

            data = restaurant;
            adminAccess.IsVisible = isAdmin;

            Name.Text = restaurant.displayName;
            Address.Text = restaurant.address;

            _foodItems = new List<Items>
            {
                new Items{item="Starters",cost=200, description="Cheesy Garlic Bread Recipe – Dominos Style in Cooker · Dahi Ke Kabab Recipe | Hung Curd Dahi Paneer Cutlet – Tea Time Snack"},
                new Items{item="Meals",cost=300, description="A meal is an eating occasion that takes place at a certain time and includes consumption of food"},
                new Items{item="Deserts",cost=100, description="Dessert is a course that concludes a meal. The course consists of sweet foods, such as confections, and possibly a beverage"}
            };
            listView.ItemsSource = _foodItems;
        }

        public async void DeleteRestaurant(object sender, EventArgs e)
        {
            var response = await DisplayAlert("Alert", "Do you wish to delete the restaurant", "YES", "NO");
            if (response)
            {
                var res = await ServiceModule.ServiceModuleInstance.DeleteRestaurantAsync(data.id);
                if (res)
                {
                    await DisplayAlert("Alert", "Deleted", "OK");
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Alert", "Server Error, Try Again!", "OK");
                }
            }
        }

        public async void EditRestaurant(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditDetailsView(data));
        }

        public async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selectedItem = (Items)e.Item;
            await Navigation.PushAsync(new OrdersView(selectedItem));
        }
    }
}

