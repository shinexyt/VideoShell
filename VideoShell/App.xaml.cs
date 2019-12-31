using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using VideoShell.Services;
using VideoShell.Views;

namespace VideoShell
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            DependencyService.Register<YaoShe92DataStore>();
            DependencyService.Register<MockDataStore>();
            
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
