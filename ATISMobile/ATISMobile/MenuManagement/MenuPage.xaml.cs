using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Headers;

using ATISMobile.PublicProcedures;
using ATISMobile.Models;
using ATISMobile.HttpClientInstance;

namespace ATISMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
        #region "General Properties"
        private Boolean _IsBackButtonActive = true;
        #endregion

        #region "Subroutins And Functions"
        public MenuPage(Boolean YourIsBackButtonActive)
        {
            InitializeComponent();
            this.Appearing += MenuPage_Appearing;
            _IsBackButtonActive = YourIsBackButtonActive;
            ShowProcesses();
        }

        private async void ShowProcesses()
        {
            try
            {
                //HttpClient _Client = new HttpClient();
                //_Client.BaseAddress = new Uri(ATISMobileWebApiMClassManagement.GetATISMobileWebApiHostUrl());
                //_Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "/api/MobileProcesses/GetMobileProcesses");
                request.Headers.Add("AuthCode", ATISMobileWebApiMClassManagement.GetAuthCode3PartHashed());
                request.Headers.Add("ApiKey", ATISMobileWebApiMClassManagement.GetApiKey());
                HttpResponseMessage response = await HttpClientOnlyInstance.HttpClientInstance().SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var _Lst = JsonConvert.DeserializeObject<List<MobileProcess>>(content);
                    if (_Lst.Count == 0)
                    {; }
                    else
                    { _ListView.ItemsSource = _Lst; }
                }
                else
                { await DisplayAlert("ATISMobile-Failed", "HttpStatusCode:" + response.StatusCode.ToString(), "OK"); }
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
        private void MenuPage_Appearing(object sender, EventArgs e)
        { }

        async void OnTapGestureRecognizerTapped(object sender, EventArgs args)
        {
            try
            {
                string TargetMobileProcess = (((Label)sender).Parent.FindByName("_TargetMobileProcess") as Label).Text;
                string TargetMobileProcessId = (((Label)sender).Parent.FindByName("_TargetMobileProcessId") as Label).Text;

                //HttpClient _Client = new HttpClient();
                //_Client.BaseAddress = new Uri(ATISMobileWebApiMClassManagement.GetATISMobileWebApiHostUrl());
                //_Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, new Uri("/api/Permissions/ExistPermission"));
                request.Headers.Add("AuthCode", ATISMobileWebApiMClassManagement.GetAuthCode3PartHashed());
                request.Headers.Add("ApiKey", ATISMobileWebApiMClassManagement.GetApiKey());
                request.Headers.Add("PermissionTypeId", 1.ToString());
                request.Headers.Add("EntityIdSecond", TargetMobileProcessId);
                HttpResponseMessage response = await HttpClientOnlyInstance.HttpClientInstance().SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var ExistPermission = JsonConvert.DeserializeObject<bool>(content);
                    if (ExistPermission)
                    {
                        var pageType = Type.GetType(TargetMobileProcess);
                        var page = Activator.CreateInstance(pageType) as Page;
                        await Navigation.PushAsync(page);
                    }
                    else
                    { await DisplayAlert("ATISMobile", "مجوز دسترسی به این فرآیند را ندارید", "تایید"); }
                }
                else
                { await DisplayAlert("ATISMobile-Failed", JsonConvert.DeserializeObject<string>(response.Content.ReadAsStringAsync().Result), "تایید"); }
            }
            catch (System.Net.WebException ex)
            { await DisplayAlert("ATISMobile-Error", ATISMobilePredefinedMessages.ATISWebApiNotReachedMessage, "OK"); }
            catch (Exception ex)
            { await DisplayAlert("ATISMobile-Error", ex.Message, "OK"); }
        }

        protected override bool OnBackButtonPressed()
        {
            if (_IsBackButtonActive)
            { return false; }
            else
            { return true; }
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