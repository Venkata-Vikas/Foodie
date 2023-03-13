using System;
using System.Collections.Generic;
using FoodiesApp;
using Xamarin.Forms;

namespace Foodies.Views
{	
	public partial class OrdersView : ContentPage
	{
        public OrdersView(Items foodItems)
        {
            InitializeComponent();
            BindingContext = this;
            Name.Text = foodItems.item + " of cost " + foodItems.cost;
        }
    }
}

