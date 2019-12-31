using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using VideoShell.Models;
using VideoShell.Views;

namespace VideoShell.ViewModels
{
    public class VideosViewModel : VideoBaseViewModel
    {
        public ObservableCollection<Video> Videos { get; set; }
        public Command LoadVideosCommand { get; set; }

        public VideosViewModel()
        {
            Title = "Latest Video";
            Videos = new ObservableCollection<Video>();
            LoadVideosCommand = new Command(async () => await ExecuteLoadVideosCommand());
        }

        async Task ExecuteLoadVideosCommand()
        {
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