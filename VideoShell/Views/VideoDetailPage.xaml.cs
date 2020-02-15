using Acr.UserDialogs;
using FormsVideoLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoShell.Services;
using VideoShell.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VideoShell.Views
{
    public partial class VideoDetailPage : ContentPage
    {
        VideoDetailViewModel viewModel;
        public VideoDetailPage(VideoDetailViewModel videoDetail)
        {
            InitializeComponent();
            BindingContext = viewModel = videoDetail;
        }

        private void VideoView_UpdateStatus(object sender, EventArgs e)
        {
            var videoPlayer = sender as VideoPlayer;
            var status = videoPlayer.Status;
            if (status == VideoStatus.Playing)
            {
                UserDialogs.Instance.HideLoading();
                videoView.UpdateStatus -= VideoView_UpdateStatus;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (viewModel.VideoUrl == null)
                viewModel.GetTrueVideoUrlCommand.Execute(null);
            if (Device.RuntimePlatform == Device.Android)
            {
                NavigationPage.SetHasNavigationBar(this, false);
                DependencyService.Get<IStatusBar>().HideStatusBar();

                UserDialogs.Instance.ShowLoading("Loading...");
                videoView.UpdateStatus += VideoView_UpdateStatus;
            }
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            if (Device.RuntimePlatform == Device.Android)
            {
                DependencyService.Get<IStatusBar>().ShowStatusBar();
            }

        }
    }
}