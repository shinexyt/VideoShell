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
using VideoShell.Extension.Abstraction.Models;
using VideoShell.Extension.Abstraction;

namespace VideoShell
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
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
