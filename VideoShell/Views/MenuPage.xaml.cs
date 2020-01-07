using VideoShell.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Composition.Hosting;
using System.Reflection;
using VideoShell.Extension.Abstraction;
using VideoShell.Extension.Abstraction.Models;
using System.Composition;
using VideoShell.Extensions.Abstraction;

namespace VideoShell.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MenuPage : ContentPage
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        List<HomeMenuItem> menuItems;

        [ImportMany]
        public IEnumerable<Lazy<IDataSource<Video>, WebMetadataModel>> DataSources { get; set; }

        public MenuPage()
        {
            InitializeComponent();
            using (var host = new ContainerConfiguration().WithAssembly(Assembly.GetExecutingAssembly()).CreateContainer())
            {
                host.SatisfyImports(this);
            }
            foreach (var item in DataSources)
            {
                WebInstance.Url = item.Metadata.DefaultUrl;
                WebInstance.DataSource = item.Value;
            }
            menuItems = new List<HomeMenuItem>
            {
                new HomeMenuItem {Id = MenuItemType.Videos, Title="Latest Videos" },
                new HomeMenuItem {Id = MenuItemType.Browse, Title="Browse" },
                new HomeMenuItem {Id = MenuItemType.About, Title="About" }
            };

            ListViewMenu.ItemsSource = menuItems;

            ListViewMenu.SelectedItem = menuItems[0];
            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;

                var id = (int)((HomeMenuItem)e.SelectedItem).Id;
                await RootPage.NavigateFromMenu(id);
            };
        }
    }
}