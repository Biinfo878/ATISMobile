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

namespace ATISMobile.LoadAllocationsManagement
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoadAllocationsPage : ContentPage
    {
        #region "General Properties"
        #endregion

        #region "Subroutins And Functions"
        public LoadAllocationsPage()
        { InitializeComponent(); ViewLoadAllocations(); }

        public async void ViewLoadAllocations()
        {
            try
            {
                //HttpClient _Client = new HttpClient();
                //_Client.BaseAddress = new Uri(ATISMobileWebApiMClassManagement.GetATISMobileWebApiHostUrl());
                //_Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "/api/LoadAllocations/GetLoadAllocationsforTruckDriver");
                request.Headers.Add("AuthCode", ATISMobileWebApiMClassManagement.GetAuthCode3PartHashed());
                request.Headers.Add("ApiKey", ATISMobileWebApiMClassManagement.GetApiKey());
                HttpResponseMessage response = await HttpClientOnlyInstance.HttpClientInstance().SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    List<LoadAllocationsforTruckDriver> _List = new List<LoadAllocationsforTruckDriver>();
                    var content = await response.Content.ReadAsStringAsync();
                    _List = JsonConvert.DeserializeObject<List<LoadAllocationsforTruckDriver>>(content);
                    if (_List.Count == 0)
                    { _ListView.IsVisible = false; _StackLayoutEmptyAllocations.IsVisible = true; }
                    else
                    { _ListView.ItemsSource = _List; }
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

        async void OnClicked_DeleteLoadAllocation(object sender, EventArgs args)
        {
            try
            {
                var Action = await DisplayAlert("ATISMobile", "حذف تخصیص بار را تایید می کنید؟", "بله", "خیر");
                if (Action)
                {
                    var LoadAllocationId = (((StackLayout)((ImageButton)sender).Parent.Parent.FindByName("_StackLayoutInformation")).FindByName("_LabelLAId") as Label).Text.Split('-')[0].Split(':')[1].Trim();
                    //HttpClient _Client = new HttpClient();
                    //_Client.BaseAddress = new Uri(ATISMobileWebApiMClassManagement.GetATISMobileWebApiHostUrl());
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, new Uri("/api/LoadAllocations/LoadAllocationCancelling"));
                    request.Headers.Add("AuthCode", ATISMobileWebApiMClassManagement.GetAuthCode4PartHashed());
                    request.Headers.Add("ApiKey", ATISMobileWebApiMClassManagement.GetApiKey());
                    request.Headers.Add("Last5Digit", ATISMobileWebApiMClassManagement.UserLast5Digit);
                    request.Headers.Add("LoadAllocationId", LoadAllocationId);
                    HttpResponseMessage response = await HttpClientOnlyInstance.HttpClientInstance().SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        ViewLoadAllocations();
                        await DisplayAlert("ATISMobile", "حذف تخصیص بار انجام شد", "تایید");
                    }
                    else
                    { await DisplayAlert("ATISMobile-Failed", JsonConvert.DeserializeObject<string>(response.Content.ReadAsStringAsync().Result), "تایید"); }
                }
            }
            catch (System.Net.WebException ex)
            { await DisplayAlert("ATISMobile-Error", ATISMobilePredefinedMessages.ATISWebApiNotReachedMessage, "OK"); }
            catch (Exception ex)
            { await DisplayAlert("ATISMobile-Error", ex.Message, "OK"); }
        }

        async void OnClicked_InreasePriority(object sender, EventArgs args)
        {
            try
            {
                var Action = await DisplayAlert("ATISMobile", "افزایش اولویت را تایید می کنید؟", "بله", "خیر");
                if (Action)
                {
                    var LoadAllocationId = (((StackLayout)((ImageButton)sender).Parent.Parent.FindByName("_StackLayoutInformation")).FindByName("_LabelLAId") as Label).Text.Split('-')[0].Split(':')[1].Trim();
                    //HttpClient _Client = new HttpClient();
                    //_Client.BaseAddress = new Uri(ATISMobileWebApiMClassManagement.GetATISMobileWebApiHostUrl());
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, new Uri("/api/LoadAllocations/IncreasePriority"));
                    request.Headers.Add("AuthCode", ATISMobileWebApiMClassManagement.GetAuthCode4PartHashed());
                    request.Headers.Add("ApiKey", ATISMobileWebApiMClassManagement.GetApiKey());
                    request.Headers.Add("Last5Digit", ATISMobileWebApiMClassManagement.UserLast5Digit);
                    request.Headers.Add("LoadAllocationId", LoadAllocationId);
                    HttpResponseMessage response = await HttpClientOnlyInstance.HttpClientInstance().SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    { ViewLoadAllocations(); await DisplayAlert("ATISMobile", "افزایش اولویت انجام شد", "تایید"); }
                    else
                    { await DisplayAlert("ATISMobile-Failed", JsonConvert.DeserializeObject<string>(response.Content.ReadAsStringAsync().Result), "تایید"); }
                }
            }
            catch (System.Net.WebException ex)
            { await DisplayAlert("ATISMobile-Error", ATISMobilePredefinedMessages.ATISWebApiNotReachedMessage, "OK"); }
            catch (Exception ex)
            { await DisplayAlert("ATISMobile-Error", ex.Message, "OK"); }

        }

        async void OnClicked_DecreasePriority(object sender, EventArgs args)
        {
            try
            {
                var Action = await DisplayAlert("ATISMobile", "کاهش اولویت را تایید می کنید؟", "بله", "خیر");
                if (Action)
                {
                    var LoadAllocationId = (((StackLayout)((ImageButton)sender).Parent.Parent.FindByName("_StackLayoutInformation")).FindByName("_LabelLAId") as Label).Text.Split('-')[0].Split(':')[1].Trim();
                    //HttpClient _Client = new HttpClient();
                    //_Client.BaseAddress = new Uri(ATISMobileWebApiMClassManagement.GetATISMobileWebApiHostUrl());
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, new Uri("/api/LoadAllocations/DecreasePriority"));
                    request.Headers.Add("AuthCode", ATISMobileWebApiMClassManagement.GetAuthCode4PartHashed());
                    request.Headers.Add("ApiKey", ATISMobileWebApiMClassManagement.GetApiKey());
                    request.Headers.Add("Last5Digit", ATISMobileWebApiMClassManagement.UserLast5Digit);
                    request.Headers.Add("LoadAllocationId", LoadAllocationId);
                    HttpResponseMessage response = await HttpClientOnlyInstance.HttpClientInstance().SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    { ViewLoadAllocations(); await DisplayAlert("ATISMobile", "کاهش اولویت انجام شد", "تایید"); }
                    else
                    { await DisplayAlert("ATISMobile-Failed", JsonConvert.DeserializeObject<string>(response.Content.ReadAsStringAsync().Result), "تایید"); ; }
                }
            }
            catch (System.Net.WebException ex)
            { await DisplayAlert("ATISMobile-Error", ATISMobilePredefinedMessages.ATISWebApiNotReachedMessage, "OK"); }
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