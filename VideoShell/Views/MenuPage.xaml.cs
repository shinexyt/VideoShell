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
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MenuPage : ContentPage
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        //List<HomeMenuItem> menuItems;
        public MenuPage()
        {
            InitializeComponent();
            List<VideoSourceItem> videoSources = Application.Current.Properties["VideoSourceList"] as List<VideoSourceItem>;
           
            //menuItems = new List<HomeMenuItem>
            //{
            //    new HomeMenuItem {Id = MenuItemType.Videos, Title="Latest Videos" },
            //    new HomeMenuItem {Id = MenuItemType.Browse, Title="Browse" },
            //    new HomeMenuItem {Id = MenuItemType.About, Title="About" }
            //};

            ListViewMenu.ItemsSource = videoSources;

            ListViewMenu.SelectedItem = videoSources[0];
            
            WebInstance.Url = videoSources[0].Url;
            WebInstance.DataSource = WebInstance.DataSources.ElementAt(0).Value;
            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;
                WebInstance.Url = ((VideoSourceItem)e.SelectedItem).Url;
            };
        }
    }
}