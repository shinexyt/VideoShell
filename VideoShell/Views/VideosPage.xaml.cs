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

namespace VideoShell.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class VideosPage : ContentPage
    {
        VideosViewModel viewModel;

        public VideosPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new VideosViewModel();
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

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Videos.Count == 0)
                viewModel.LoadVideosCommand.Execute(null);
        }
    }
}