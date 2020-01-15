using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using VideoShell.Models;
using VideoShell.Extensions.Abstraction;

namespace VideoShell.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class NewUrlView : ContentPage
    {
        public VideoSourceItem Item { get; set; } = new VideoSourceItem();
        public List<string> Names { get; } = new List<string>();

        public NewUrlView()
        {
            InitializeComponent();

            foreach (var item in WebInstance.DataSources)
            {
                Names.Add(item.Metadata.Name);
            }
            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddItem", Item);
            await Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}