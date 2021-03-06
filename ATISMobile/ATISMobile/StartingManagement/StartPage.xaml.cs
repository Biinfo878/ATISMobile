﻿using System;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

using ATISMobile.PublicProcedures;
using ATISMobile.HttpClientInstance;

namespace ATISMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartPage : ContentPage
    {

        #region "General Properties"
        #endregion

        #region "Subroutins And Functions"
        public StartPage()
        {
            InitializeComponent();
            _BtnAppUpdateChecker.Clicked += _BtnAppUpdateChecker_Clicked;
            try
            { ATISMobileWebApiConnect(); }
            catch (Exception ex)
            { DisplayAlert("ATISMobile-Error", ex.Message, "OK"); }
        }

        private async void ATISMobileWebApiConnect()
        {
            try
            {
                //این تابع فقط یکبار فراخوانی می کردد تا متغیر پابلیک اتصال تنظیم گردد
                await ATISMobileWebApiMClassManagement.SetATISMobileWebApiHostUrl();

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "/api/VersionControl/HaveNewerVersion");
                request.Headers.Add("AuthCode", ATISMobileWebApiMClassManagement.GetAuthCode2PartHashed());
                Xamarin.Essentials.VersionTracking.Track();
                string VersionNumber = Xamarin.Essentials.VersionTracking.CurrentBuild;
                string VersionName = Xamarin.Essentials.VersionTracking.CurrentVersion;
                request.Headers.Add("VersionNumber", VersionNumber);
                request.Headers.Add("VersionName", VersionName);
                HttpResponseMessage response = await HttpClientOnlyInstance.HttpClientInstance().SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    bool AppVersionIsNew = JsonConvert.DeserializeObject<bool>(content);
                    if (AppVersionIsNew)
                    { Disable_StartApplicationButton(VersionNumber + "." + VersionName); }
                    else
                    { Enable_StartApplicationButton(VersionNumber + "." + VersionName); }
                }
                else
                {
                    _LblExpander.IsVisible = true;
                    ViewErrorMessage(JsonConvert.DeserializeObject<string>(response.Content.ReadAsStringAsync().Result));
                }
            }
            catch (System.Net.WebException ex)
            { ViewErrorMessage(ATISMobilePredefinedMessages.ATISWebApiNotReachedMessage); }
            catch (Exception ex)
            { ViewErrorMessage(ex.Message); }
        }

        private void Enable_StartApplicationButton(string YourAppVersion)
        {
            _StartApplication.IsEnabled = true; _StartApplication.IsVisible = true;
            _MessageBox.Text = "نسخه اپلیکیشن : " + YourAppVersion; _MessageBox.IsVisible = true; _MessageBox.BackgroundColor = Color.Green;
            _BtnAppUpdateChecker.IsVisible = false; _LblHelper.IsVisible = true; _LblExpander.IsVisible = false; ShowPublicMessage();
        }

        private void Disable_StartApplicationButton(string YourAppVersion)
        {
            _StartApplication.IsEnabled = false; _StartApplication.IsVisible = false;
            _MessageBox.Text = "نسخه اپلیکیشن : " + YourAppVersion; _MessageBox.IsVisible = true; _MessageBox.BackgroundColor = Color.Green;
            _BtnAppUpdateChecker.IsVisible = true; _LblHelper.IsVisible = true; _LblExpander.IsVisible = true;
            Device.BeginInvokeOnMainThread(() =>
            { DisplayAlert("ATISMobile", "اپلیکیشن نیاز به بروزرسانی دارد.کلید آبی رنگ را در صفحه اصلی برای بروزرسانی انتخاب نمایید ", "تایید"); });
        }

        private void ViewErrorMessage(string YourMessage)
        { _MessageBox.IsVisible = true; _MessageBox.Text = YourMessage; _MessageBox.BackgroundColor = Color.Red; }

        private async void ShowPublicMessage()
        {
            try
            {
                //HttpClient _Client = new HttpClient();
                //_Client.BaseAddress = new Uri(ATISMobileWebApiMClassManagement.GetATISMobileWebApiHostUrl());
                //_Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "/api/PublicMessages/GetPublicMessage");
                request.Headers.Add("AuthCode", ATISMobileWebApiMClassManagement.GetAuthCode2PartHashed());
                HttpResponseMessage response = await HttpClientOnlyInstance.HttpClientInstance().SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    string PublicMessage = JsonConvert.DeserializeObject<String>(content);
                    if (PublicMessage.Trim() != string.Empty)
                    { Device.BeginInvokeOnMainThread(() => { DisplayAlert("ATISMobile", PublicMessage.Trim(), "تایید"); }); }
                }
                else
                { Device.BeginInvokeOnMainThread(() => { DisplayAlert("ATISMobile-Failed", JsonConvert.DeserializeObject<string>(response.Content.ReadAsStringAsync().Result), "تایید"); });  }
            }
            catch (System.Net.WebException ex)
            { await DisplayAlert("ATISMobile-Error", ATISMobilePredefinedMessages.ATISWebApiNotReachedMessage, "OK"); }
            catch (Exception ex)
            { await DisplayAlert("ATISMobile-Error", ex.Message, "OK"); }
        }

        #endregion

        #region "Events"
        #endregion

        #region "Event Handlers"
        async private void _BtnAppUpdateChecker_Clicked(object sender, EventArgs e)
        {
            try
            { await Browser.OpenAsync(Properties.Resources.DownloadLinkURL, BrowserLaunchMode.SystemPreferred); }
            catch (Exception ex)
            { await DisplayAlert("ATISMobile-Error", ex.Message, "OK"); }
        }

        private async void _StartApplication_ClickedEvent(Object sender, EventArgs e)
        {
            try
            {
                String TargetPath = System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                TargetPath = Path.Combine(TargetPath, "AMUStatus.txt");
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
                        await Navigation.PushAsync(_MenuPage);
                    }
                }
            }
            catch (Exception ex)
            { await DisplayAlert("ATISMobile-Error", ex.Message, "OK"); }
        }

        #endregion

        #region "Override Methods"
        #endregion

        #region "Abstract Methods"
        #endregion

        #region "Implemented Members"
        #endregion








    }
}