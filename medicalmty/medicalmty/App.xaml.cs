﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace medicalmty
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new startPage())
            {
                BarBackgroundColor = Color.FromHex("#111620"),
                BarTextColor = Color.White

            };
            
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
