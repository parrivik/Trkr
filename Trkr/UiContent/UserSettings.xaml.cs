using System;
using System.Collections.Generic;
using Trkr;

using Xamarin.Forms;

namespace Trkr.UiContent
{
    public partial class UserSettings : ContentPage
    {
        public UserSettings()
        {
            InitializeComponent();
        }

        public void OnLogout(object sender, EventArgs e)
        {
            //MyLocation.trackingThread.Abort();
            App.logedInUser = null;
            Application.Current.MainPage = new LoginPage();

        }
    }
}
