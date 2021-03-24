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

namespace ATISMobile.TruckDriverManagement
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TruckDriverPage : ContentPage
    {
        #region "General Properties"
        #endregion

        #region "Subroutins And Functions"

        public TruckDriverPage()
        { InitializeComponent(); ViewInformation(); }

        public async void ViewInformation()
        {
            try
            {
                //HttpClient _Client = new HttpClient();
                //_Client.BaseAddress = new Uri(ATISMobileWebApiMClassManagement.GetATISMobileWebApiHostUrl());
                //_Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "/api/TruckDrivers/GetTruckDriver");
                request.Headers.Add("AuthCode", ATISMobileWebApiMClassManagement.GetAuthCode3PartHashed());
                request.Headers.Add("ApiKey", ATISMobileWebApiMClassManagement.GetApiKey());
                HttpResponseMessage response = await HttpClientOnlyInstance.HttpClientInstance().SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var myTruckDriver = JsonConvert.DeserializeObject<TruckDriver>(content);
                    LblNameFamily.Text = myTruckDriver.NameFamily;
                    LblFatherName.Text = myTruckDriver.FatherName;
                    LblSmartCardNo.Text = myTruckDriver.SmartCardNo;
                    LblNationalCode.Text = myTruckDriver.NationalCode;
                    LblTel.Text = myTruckDriver.Tel;
                    LblDriverLicenceNo.Text = myTruckDriver.DrivingLicenceNo;
                    LblDriverId.Text = myTruckDriver.DriverId;
                    LblThisSoftwareUserInformation.Text = "مشخصات کاربری راننده";
                    LblSoftwareUserId.Text = "شناسه کاربری: " + ATISMobileWebApiMClassManagement.GetCurrentSoftwareUserId().ToString();
                    LblRegisteredMobileNumberIntoWebApi.Text = "شماره موبایل فعال شده: " + ATISMobileWebApiMClassManagement.GetRegisteredMobileNumberIntoWebApi();
                    LblCurrentUserStatus.Text = "وضعیت کاربر: " + ATISMobileWebApiMClassManagement.GetAMUStatus();
                }
                else
                { await DisplayAlert("ATISMobile-Failed", JsonConvert.DeserializeObject<string>(response.Content.ReadAsStringAsync().Result), "تایید"); }
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