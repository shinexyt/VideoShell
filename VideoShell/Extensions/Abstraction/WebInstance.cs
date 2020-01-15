using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Composition;
using System.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VideoShell.Extensions.Abstraction;
using VideoShell.Extensions.Abstraction.Models;
using VideoShell.Models;
using VideoShell.Services;
using VideoShell.ViewModels.Base;
using Xamarin.Forms;

namespace VideoShell.Extensions.Abstraction
{

    public class WebInstance
    {
        //TODO: Need refactor
        public static IDataSource<Video> DataSource { get; set; }
        public static ObservableCollection<VideoSourceItem> VideoSources { get; set; }
        [ImportMany]
        public static IEnumerable<Lazy<IDataSource<Video>, WebMetadataModel>> DataSources { get; set; }
        public static string Url { get; set; }
        public void Initialize()
        {
            using (var host = new ContainerConfiguration().WithAssembly(Assembly.GetExecutingAssembly()).CreateContainer())
            {
                host.SatisfyImports(this);
            }
            int localDataItems = App.Database.GetItemsCountAsync().Result;
            if (localDataItems == 0)
            {
                foreach (var item in DataSources)
                {
                    var videoSourceItem = new VideoSourceItem
                    {
                        Url = item.Metadata.DefaultUrl,
                        Name = item.Metadata.Name,
                        IsDefaultUrl = true
                    };
                    App.Database.SaveItemAsync(videoSourceItem);
                }
            }
            VideoSources = new ObservableCollection<VideoSourceItem>(App.Database.GetItemsAsync().Result);
            var selectedItemIndex = 0;
            if (Application.Current.Properties.ContainsKey("SelectedItemIndex"))
            {
                selectedItemIndex = (int)Application.Current.Properties["SelectedItemIndex"];
            }
            var currentVideoSource = VideoSources[selectedItemIndex];
            WebInstance.Url = currentVideoSource.Url;
            WebInstance.DataSource = WebInstance.DataSources.FirstOrDefault(d => d.Metadata.Name == currentVideoSource.Name).Value;
        }
    }
}