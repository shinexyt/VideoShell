using System;
using System.Collections.Generic;
using System.Composition;
using System.Composition.Hosting;
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
        [ImportMany]
        public static IEnumerable<Lazy<IDataSource<Video>, WebMetadataModel>> DataSources { get; set; }
        public static string Url { get; set; }
        public async Task Initialize()
        {
            using (var host = new ContainerConfiguration().WithAssembly(Assembly.GetExecutingAssembly()).CreateContainer())
            {
                host.SatisfyImports(this);
            }
            //TODO: Properties only use base type, so this will be instead by using sqlite.
            if (!Application.Current.Properties.ContainsKey("VideoSourceList"))
            {
                System.Diagnostics.Debug.WriteLine("Write Properties");
                var videoSourceList = new List<VideoSourceItem>();
                foreach (var item in DataSources)
                {
                    videoSourceList.Add(new VideoSourceItem
                    {
                        Url = item.Metadata.DefaultUrl,
                        Name = item.Metadata.Name,
                        IsDefaultUrl = true
                    });
                }

                Application.Current.Properties.Add("VideoSourceList", videoSourceList);
                await Application.Current.SavePropertiesAsync();
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Read Properties");;
            }
        }
    }
}
