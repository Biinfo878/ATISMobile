using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ATISMobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            NavigationPage StartNavPage = new NavigationPage(new StartPage());
            StartNavPage.BarTextColor = Color.White    ;
            StartNavPage.BarBackgroundColor  = Color.Black    ;
            MainPage = StartNavPage;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
