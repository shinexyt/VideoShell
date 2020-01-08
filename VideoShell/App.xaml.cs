using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using VideoShell.Services;
using VideoShell.Views;
using VideoShell.ViewModels.Base;
using System.Composition.Hosting;
using System.Reflection;
using System.Composition;
using VideoShell.ViewModels;
using System.Composition.Convention;
using VideoShell.Extensions.Abstraction.Models;
using VideoShell.Extensions.Abstraction;
using System.Collections.Generic;
using VideoShell.Models;

namespace VideoShell
{
    public partial class App : Application
    {
        //[ImportMany]
        //public IEnumerable<Lazy<IDataSource<Video>, WebMetadataModel>> DataSources { get; set; }
        public App()
        {
            InitializeComponent();

           
        }

        protected override async void OnStart()
        {
            
            await new WebInstance().Initialize();
            MainPage = new MainPage();
            //using (var host = new ContainerConfiguration().WithAssembly(Assembly.GetExecutingAssembly()).CreateContainer())
            //{
            //    host.SatisfyImports(this);
            //}
            //var videoSourceList = new List<VideoSourceItem>();
            //foreach (var item in DataSources)
            //{
            //    videoSourceList.Add(new VideoSourceItem
            //    {
            //        Url = item.Metadata.DefaultUrl,
            //        Name = item.Metadata.Name,
            //        IsDefaultUrl = true
            //    });
            //    //WebInstance.Url = item.Metadata.DefaultUrl;
            //    //WebInstance.DataSource = item.Value;
            //}

            //Application.Current.Properties.Add("VideoSourceList", videoSourceList);
            //await Application.Current.SavePropertiesAsync();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
