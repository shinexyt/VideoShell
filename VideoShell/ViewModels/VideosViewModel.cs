using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using VideoShell.Models;
using VideoShell.Views;
using VideoShell.ViewModels.Base;
using VideoShell.Extension.Abstraction.Models;

namespace VideoShell.ViewModels
{
    public class VideosViewModel : ViewModelBase
    {
        public ObservableCollection<Video> Videos { get; }
        public string Title { get; set; }
        public Command LoadVideosCommand { get; set; }

        public VideosViewModel()
        {
            Title = "Latest Video";
            Videos = new ObservableCollection<Video>();
            LoadVideosCommand = new Command(async () => await ExecuteLoadVideosCommand());
        }

        async Task ExecuteLoadVideosCommand()
        {
            if (Videos.Count > 0)
                return;

            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Videos.Clear();
                var items = await DataSource.GetItemsAsync(false);
                foreach (var item in items)
                {
                    Videos.Add(item);
                }
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