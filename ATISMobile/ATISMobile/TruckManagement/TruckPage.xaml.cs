using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Diagnostics;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http.Headers;

using ATISMobile.Models;
using ATISMobile.PublicProcedures;
using ATISMobile.HttpClientInstance;

namespace ATISMobile.TruckManagement
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TruckPage : ContentPage
    {

        #region "General Properties"
        #endregion

        #region "Subroutins And Functions"
        public TruckPage()
        {
            InitializeComponent();
            ViewInformation(ATISMobileWebApiMClassManagement.GetCurrentSoftwareUserId());
        }

        public async void ViewInformation(Int64 YourUserId)
        {
            try
            {
                //HttpClient _Client = new HttpClient();
                //_Client.BaseAddress = new Uri(ATISMobileWebApiMClassManagement.GetATISMobileWebApiHostUrl());
                //_Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "/api/Trucks/GetTruck");
                request.Headers.Add("AuthCode", ATISMobileWebApiMClassManagement.GetAuthCode3PartHashed());
                request.Headers.Add("ApiKey", ATISMobileWebApiMClassManagement.GetApiKey());
                HttpResponseMessage response = await HttpClientOnlyInstance.HttpClientInstance().SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Truck myTruck = JsonConvert.DeserializeObject<Truck>(content);
                    LblTruckId.Text = myTruck.TruckId;
                    LblLPString.Text = myTruck.LPString;
                    LblLoaderTitle.Text = myTruck.LoaderTitle;
                    LblSmartCardNo.Text = myTruck.SmartCardNo;
                    LblAnnouncementHallSubGroups.Text = myTruck.AnnouncementHallSubGroups;
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
        #endregion

        #region "Override Methods"
        #endregion

        #region "Abstract Methods"
        #endregion

        #region "Implemented Members"
        #endregion


    }
}