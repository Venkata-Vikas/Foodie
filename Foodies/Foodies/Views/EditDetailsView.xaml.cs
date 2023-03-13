using System;
using System.Collections.Generic;
using Foodies.ViewModels;
using FoodiesApp;
using Xamarin.Forms;

namespace Foodies.Views
{	
	public partial class EditDetailsView : ContentPage
	{
        public EditDetailsView(Restaurant data)
        {
            InitializeComponent();
            BindingContext = new EditDetailsViewModel();
            priceForTwo.Text = data.priceForTwo.ToString();
            id.Text = data.id.ToString();
            address.Text = data.address;
            displayName.Text = data.displayName;
        }
    }
}

