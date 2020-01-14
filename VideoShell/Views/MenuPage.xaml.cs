using VideoShell.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Composition.Hosting;
using System.Reflection;
using VideoShell.Extensions.Abstraction;
using VideoShell.Extensions.Abstraction.Models;
using System.Composition;
using System.Linq;

namespace VideoShell.Views
{
    public partial class MenuPage : ContentPage
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        //List<HomeMenuItem> menuItems;
        public MenuPage()
        {
            InitializeComponent();
            
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            

            //menuItems = new List<HomeMenuItem>
            //{
            //    new HomeMenuItem {Id = MenuItemType.Videos, Title="Latest Videos" },
            //    new HomeMenuItem {Id = MenuItemType.Browse, Title="Browse" },
            //    new HomeMenuItem {Id = MenuItemType.About, Title="About" }
            //};

            ListViewMenu.ItemsSource = WebInstance.VideoSources;

            ListViewMenu.SelectedItem = WebInstance.VideoSources[0];

            
            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;
                WebInstance.Url = ((VideoSourceItem)e.SelectedItem).Url;
            };
        }
    }
}