using FormsVideoLibrary;
using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using VideoShell.Extension.Abstraction.Models;
using VideoShell.Models;
using VideoShell.Services;
using VideoShell.ViewModels.Base;
using Xamarin.Forms;

namespace VideoShell.ViewModels
{
    public class VideoDetailViewModel : ViewModelBase
    {
        private string originalVideoUrl;
        public string Title { get; set; }

        private VideoSource videoUrl;
        public VideoSource VideoUrl
        {
            get
            {
                return videoUrl;
            }
            set
            {
                videoUrl = value;
                RaisePropertyChanged(() => VideoUrl);
            }
        }
        public Command GetTrueVideoUrlCommand { get; set; }
        public VideoDetailViewModel(Video video)
        {
            Title = video.Title;
            originalVideoUrl = video.VideoUrl;
            GetTrueVideoUrlCommand = new Command(async () => await ExecuteGetTrueVideoUrlCommand());
        }
        async Task ExecuteGetTrueVideoUrlCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            try
            {
                VideoUrl = VideoSource.FromUri(await DataSource.GetTrueVideoUrl(originalVideoUrl));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
        
    }
}
