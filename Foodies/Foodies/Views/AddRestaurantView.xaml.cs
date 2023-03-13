using System;
using System.Collections.Generic;
using Foodies.ViewModels;
using Xamarin.Forms;

namespace Foodies.Views
{	
	public partial class AddRestaurantView : ContentPage
	{
        public AddRestaurantView(int AdminID)
        {
            InitializeComponent();
            BindingContext = new AddRestaurantViewModel();
        }
    }
}

