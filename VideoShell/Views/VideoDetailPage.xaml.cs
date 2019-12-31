using FormsVideoLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (viewModel.VideoUrl == null)
                viewModel.GetTrueVideoUrlCommand.Execute(null);
        }
    }
}