using System;
using System.Collections.Generic;
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
        public static List<VideoSourceItem> VideoSources { get; private set; }
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
            VideoSources = App.Database.GetItemsAsync().Result;
            WebInstance.Url = VideoSources[0].Url;
            WebInstance.DataSource = WebInstance.DataSources.ElementAt(0).Value;
        }
    }
}