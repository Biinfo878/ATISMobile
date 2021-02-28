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
using Xamarin.Essentials;

namespace ATISMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartPage : ContentPage
    {
        public StartPage()
        {
            InitializeComponent();
            try
            { Manage_ExitApplicationButton(); ShowApplicationVersion(); ShowPublicMessage(); }
            catch (Exception ex)
            { Debug.WriteLine(ex.Message); }
        }

        private async void ShowPublicMessage()
        {
            try
            {
                HttpClient _Client = new HttpClient();
                var response = await _Client.GetAsync(ATISMobileMClassPublicProcedures.ATISHostURL + "/api/PublicMessages/GetPublicMessage");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    MessageStruct PublicMessage = JsonConvert.DeserializeObject<MessageStruct>(content);
                    if (PublicMessage.ErrorCode == false)
                    {
                        if (PublicMessage.Message1.Trim() != string.Empty)
                        {
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                DisplayAlert("ATISMobile", PublicMessage.Message1.Trim(), "Ok");
                            });
                        }
                    }
                    else
                    { await DisplayAlert("ATISMobile", PublicMessage.Message1.Trim(), "OK"); }
                }
            }
            catch (Exception ex)
            { return; }
        }

        private async void ShowApplicationVersion()
        {
            try
            {
                HttpClient _Client = new HttpClient();
                var response = await _Client.GetAsync(ATISMobileMClassPublicProcedures.ATISHostURL + "/api/VersionControl/GetLastVersionNumber");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    MessageStruct _Version = JsonConvert.DeserializeObject<MessageStruct>(content);
                    if (_Version.ErrorCode == false)
                    {
                        ViewSuccessMessage("نسخه اپلیکیشن : " + _Version.Message1);
                        Enable_StartApplicationButton();
                        Xamarin.Essentials.VersionTracking.Track();
                        string VersionNumber = Xamarin.Essentials.VersionTracking.CurrentVersion;
                        string VersionName = Xamarin.Essentials.VersionTracking.CurrentBuild;

                        var responseVersion = await _Client.GetAsync(ATISMobileMClassPublicProcedures.ATISHostURL + "/api/VersionControl?YourVersionNumber=" + VersionNumber + "&YourVersionName=" + VersionName);
                        if (responseVersion.IsSuccessStatusCode)
                        {
                            var contentt = await responseVersion.Content.ReadAsStringAsync();
                            if (!JsonConvert.DeserializeObject<bool>(contentt)) return;
                        }
                        await DisplayAlert("بروز رسانی آتیس موبایل", "آتیس موبایل اخیرا تغییراتی داشته است", "OK");
                        await Browser.OpenAsync("http://ATISMobile.ir/Downloads.aspx", BrowserLaunchMode.SystemPreferred);
                    }
                    else
                    { ViewATISErrorMessage(); Disable_StartApplicationButton(); }
                }
                else
                { ViewATISErrorMessage(); Disable_StartApplicationButton(); }
            }
            catch (Exception ex)
            { ViewATISErrorMessage(); Disable_StartApplicationButton(); }
        }

        private void ViewInternetErrorMessage()
        { _MessageBox.Text = "اتصال اینترنتی برقرار نیست"; _MessageBox.BackgroundColor = Color.Red; }

        private void ViewATISErrorMessage()
        {
            try
            {
                if (!ATISMobileMClassPublicProcedures.IsInternetAvailable())
                { ViewInternetErrorMessage(); return; }
            }
            catch (Exception ex)
            { ViewInternetErrorMessage(); return; }
            _MessageBox.Text = "ارتباط با سرور آتیس برقرار نیست"; _MessageBox.BackgroundColor = Color.Red;
        }

        private void ViewSuccessMessage(string YourMessage)
        { _MessageBox.Text = YourMessage; _MessageBox.BackgroundColor = Color.Green; }

        private void Enable_StartApplicationButton()
        { _StartApplication.IsEnabled = true; _StartApplication.BackgroundColor = Color.Green; _StartApplication.TextColor = Color.White; _StartApplication.BorderColor = Color.Red ; }

        private void Disable_StartApplicationButton()
        { _StartApplication.IsEnabled = false; _StartApplication.BackgroundColor = Color.Transparent; _StartApplication.TextColor = Color.Transparent; }

        private void Manage_ExitApplicationButton()
        {
            try
            {
                //if (ATISMobileMClassPublicProcedures.GetAMUStatus() == "logout")
                //{ _ExitApplication.IsEnabled = false; }
                //else
                //{ _ExitApplication.IsEnabled = true; }
            }
            catch (Exception ex)
            { Debug.WriteLine("\t\tERROR {0}", ex.Message); }

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            try
            { Manage_ExitApplicationButton(); }
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
                    string myUserId = System.IO.File.ReadAllText(TargetPath).Split(';')[1];
                    HttpClient _Client = new HttpClient();
                    var response = await _Client.GetAsync(ATISMobileMClassPublicProcedures.ATISHostURL + "/api/MobileUsers/UnRegisterMobileUser/?YourUserId=" + myUserId);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var myMS = JsonConvert.DeserializeObject<MessageStruct>(content);
                        if (myMS.ErrorCode == false)
                        {
                            System.IO.File.WriteAllText(TargetPath, "logout;;");
                            Manage_ExitApplicationButton();
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
                        MenuPage _MenuPage = new MenuPage(true);
                        //NavigationPage _MenuPage = new NavigationPage(new MenuPage());
                        //_MenuPage.BarBackgroundColor = Color.Black;
                        await Navigation.PushAsync(_MenuPage);
                    }
                }
            }
            catch (Exception ex)
            { System.Diagnostics.Debug.WriteLine("\t\tERROR {0}", ex.Message); }
        }

    }
}