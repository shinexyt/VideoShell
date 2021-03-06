﻿using System;
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
using VideoShell.Database;
using System.Threading.Tasks;
using System.Threading;
using HtmlAgilityPack;

namespace VideoShell
{
    public partial class App : Application
    {
        static VideoSourceDatabase database;
        static HtmlWeb htmlWeb;
        public App()
        {
            InitializeComponent();
            new WebInstance().Initialize();
            MainPage = new MainPage();
        }
        public static VideoSourceDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new VideoSourceDatabase();
                }
                return database;
            }
        }
        public static HtmlWeb HtmlWeb
        {
            get
            {
                if (htmlWeb == null)
                {
                    htmlWeb = new HtmlWeb();
                }
                return htmlWeb;
            }
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
