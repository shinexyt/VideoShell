using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using VideoShell.Models;
using VideoShell.Views;
using VideoShell.ViewModels;
using VideoShell.Extensions.Abstraction.Models;

namespace VideoShell.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class VideosView : ContentPage
    {

        public VideosView()
        {
            InitializeComponent();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Video;
            if (item == null)
                return;

            await Navigation.PushAsync(new VideoDetailPage(new VideoDetailViewModel(item)));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }
    }
}