﻿using System;
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

using ATISMobile.Enums;
using ATISMobile.Models;
using ATISMobile.PublicProcedures;
using ATISMobile.HttpClientInstance;

namespace ATISMobile.ProvincesManagement
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProvinceSelectionPage : ContentPage
    {
        #region "General Properties"
        private Int64 _AHId;
        private Int64 _AHSGId;
        private LoadCapacitorLoadsListType _LoadCapacitorLoadsListType;

        #endregion

        #region "Subroutins And Functions"
        public ProvinceSelectionPage()
        { InitializeComponent(); }

        public async void ViewInformation(Int64 YourAHId, Int64 YourAHSGId, LoadCapacitorLoadsListType YourLoadCapacitorLoadsListType)
        {
            _AHId = YourAHId; _AHSGId = YourAHSGId;
            _LoadCapacitorLoadsListType = YourLoadCapacitorLoadsListType;
            try
            {
                //HttpClient _Client = new HttpClient();
                //_Client.BaseAddress = new Uri(ATISMobileWebApiMClassManagement.GetATISMobileWebApiHostUrl());
                //_Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "/api/Provinces/GetProvinces");
                request.Headers.Add("AuthCode", ATISMobileWebApiMClassManagement.GetAuthCode3PartHashed());
                request.Headers.Add("ApiKey", ATISMobileWebApiMClassManagement.GetApiKey());
                request.Headers.Add("AHId", _AHId.ToString());
                request.Headers.Add("AHSGId", _AHSGId.ToString());
                request.Headers.Add("LoadCapacitorLoadsListType", ((int)_LoadCapacitorLoadsListType).ToString());
                HttpResponseMessage response = await HttpClientOnlyInstance.HttpClientInstance().SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    List<Province> _List = new List<Province>();
                    _List = JsonConvert.DeserializeObject<List<Province>>(content);
                    if (_List.Count == 0)
                    { _ListView.IsVisible = false; _StackLayoutEmptyProvince.IsVisible = true; }
                    else
                    { _ListView.ItemsSource = _List; }
                }
                else
                {
                    await DisplayAlert("ATISMobile-Failed", JsonConvert.DeserializeObject<string>(response.Content.ReadAsStringAsync().Result), "تایید");
                    _ListView.IsVisible = false; _StackLayoutEmptyProvince.IsVisible = true;

                }
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
        async void OnTapGestureRecognizerTapped(object sender, EventArgs args)
        {
            try
            {
                LoadCapacitorManagement.LoadsPage _LoadsPage = new LoadCapacitorManagement.LoadsPage();
                _LoadsPage.ViewLoads(_AHId, _AHSGId, Convert.ToInt64((((Label)sender).Parent.FindByName("_ProvinceId") as Label).Text.Split(':')[1]), (((Label)sender).Parent.FindByName("_ProvinceTitle") as Label).Text.Split(':')[0], _LoadCapacitorLoadsListType);
                await Navigation.PushAsync(_LoadsPage);
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