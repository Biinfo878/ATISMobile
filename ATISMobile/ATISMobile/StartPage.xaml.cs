using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Resources;
using System.Net;
using System.Net.Http;
using System.Diagnostics;
using Newtonsoft.Json;


using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ATISMobile.Models;
using ATISMobile.PublicProcedures;

namespace ATISMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartPage : ContentPage
    {
        public StartPage()
        {
            InitializeComponent();
            
        }

        private void Mange_ExitApplicationButton()
        {
            try
            {
                if (ATISMobileMClassPublicProcedures.GetAMUStatus() == "logout")
                { _ExitApplication.IsEnabled = false; _ExitApplication.BackgroundColor = Color.Gray; }
                else
                { _ExitApplication.IsEnabled = true; _ExitApplication.BackgroundColor = Color.Red; }
            }
            catch (Exception ex)
            { Debug.WriteLine("\t\tERROR {0}", ex.Message); }

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            try
            { Mange_ExitApplicationButton(); }
            catch (Exception ex)
            { Debug.WriteLine("\t\tERROR {0}", ex.Message); }
        }


        private async void _ExitApplication_ClickedEvent(Object sender, EventArgs e)
        {
            try
            {
                String TargetPath = System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                TargetPath = Path.Combine(TargetPath, "AMUStatus.txt");
                if (System.IO.File.Exists(TargetPath) == false)
                { await DisplayAlert("ATISMobile", "بروز خطا - مجددا تلاش نمایید", "OK"); }
                else
                {
                    string myMUId = System.IO.File.ReadAllText(TargetPath).Split(';')[1];
                    HttpClient _Client = new HttpClient();
                    var response = await _Client.GetAsync(Properties.Resources.RestfulWebServiceURL + "/api/MobileUsers/UnRegisterMobileUser/?YourMUId=" + myMUId);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var myMS = JsonConvert.DeserializeObject<MessageStruct>(content);
                        if (myMS.ErrorCode == false)
                        {
                            System.IO.File.WriteAllText(TargetPath, "logout;;");
                            Mange_ExitApplicationButton();
                        }
                        else
                        { await DisplayAlert("ATISMobile", myMS.Message1, "OK"); }
                    }
                    else
                    { await DisplayAlert("ATISMobile", "بروز خطا - مجددا تلاش نمایید", "OK"); }
                }
            }
            catch (Exception ex)
            { System.Diagnostics.Debug.WriteLine("\t\tERROR {0}", ex.Message); }
        }

        private void _UpdateApplication_ClickedEvent(Object sender, EventArgs e)
        { }

        private async void _StartApplication_ClickedEvent(Object sender, EventArgs e)
        {
            try
            {
                String TargetPath = System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                TargetPath = Path.Combine(TargetPath, "AMUStatus.txt");
                //String TargetPath = Path.Combine(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads).ToString(), );
                if (System.IO.File.Exists(TargetPath) == false)
                { System.IO.File.WriteAllText(TargetPath, "logout;;"); }
                else
                {
                    string AMUStatus = System.IO.File.ReadAllText(TargetPath);
                    var splited = AMUStatus.Split(';');
                    if (splited[0] == "logout")
                    {
                        MobileEntryPage _MobileEntryPage = new MobileEntryPage();
                        await Navigation.PushAsync(_MobileEntryPage);
                    }
                    else
                    {
                        MenuPage _MenuPage = new MenuPage();
                        await Navigation.PushAsync(_MenuPage);
                    }
                }
            }
            catch (Exception ex)
            { System.Diagnostics.Debug.WriteLine("\t\tERROR {0}", ex.Message); }
        }

    }
}