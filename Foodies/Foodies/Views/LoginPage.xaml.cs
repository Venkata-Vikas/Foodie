using System;
using System.Collections.Generic;
using Foodies.ViewModels;
using Xamarin.Forms;

namespace Foodies.Views
{	
	public partial class LoginPage : ContentPage
	{	
		public LoginPage ()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = new LoginViewModel();
        }
	}
}

