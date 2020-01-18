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
            MessagingCenter.Subscribe<NewUrlView, VideoSourceItem>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as VideoSourceItem;
                WebInstance.VideoSources.Add(newItem);
                await App.Database.SaveItemAsync(newItem);
            });
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            ListViewMenu.ItemsSource = WebInstance.VideoSources;
            var selectedItemIndex = 0;
            if (Application.Current.Properties.ContainsKey("SelectedItemIndex"))
            {
                selectedItemIndex = (int)Application.Current.Properties["SelectedItemIndex"];
            }
            ListViewMenu.SelectedItem = WebInstance.VideoSources[selectedItemIndex];
            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;
                Application.Current.Properties["SelectedItemIndex"] = e.SelectedItemIndex;
                await Application.Current.SavePropertiesAsync();

                var videoSourceItem = e.SelectedItem as VideoSourceItem;
                WebInstance.Url = videoSourceItem.Url;
                WebInstance.DataSource = WebInstance.DataSources.FirstOrDefault(d => d.Metadata.Name == videoSourceItem.Name).Value;

                MessagingCenter.Send(this, "ChangeSource");
            };
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewUrlView()));
        }
    }
}