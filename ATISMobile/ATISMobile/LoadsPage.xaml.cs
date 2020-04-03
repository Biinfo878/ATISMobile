using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Diagnostics;
using System.Windows.Input;
using Microsoft.Win32;
using Newtonsoft.Json;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using Xamarin.Forms.Internals;

using ATISMobile.Models;

namespace ATISMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoadsPage : ContentPage
    {
        public LoadsPage()
        {
            InitializeComponent();
        }

        public async void ViewLoads(int YourAHId, int YourAHSGId)
        {
            try
            {
                List<LoadCapacitorLoad> _List = new List<LoadCapacitorLoad>();
                HttpClient _Client = new HttpClient();
                var response = await _Client.GetAsync(Properties.Resources.RestfulWebServiceURL + "/api/LoadCapacitor/GetLoadCapacitorLoads/?YourAHId=" + YourAHId.ToString() + "&YourAHSGId=" + YourAHSGId.ToString());
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    _List = JsonConvert.DeserializeObject<List<LoadCapacitorLoad>>(content);
                    if (_List.Count == 0)
                    {
                        _ListView.IsVisible = false;
                        _StackLayoutEmptyAnnounce.IsVisible = true;
                    }
                    else
                    {
                        _ListView.ItemsSource = _List;

                    }
                }

            }
            catch (Exception ex)
            { Debug.WriteLine("\t\tERROR {0}", ex.Message); }
        }

        public async void ViewLoads(Int64  YourAHId, Int64  YourAHSGId, Int64 YourProvinceId)
        {
            try
            {
                List<LoadCapacitorLoad> _List = new List<LoadCapacitorLoad>();
                HttpClient _Client = new HttpClient();
                var response = await _Client.GetAsync(Properties.Resources.RestfulWebServiceURL + "/api/LoadCapacitor/GetLoadCapacitorLoads/?YourAHId=" + YourAHId.ToString() + "&YourAHSGId=" + YourAHSGId.ToString()+ "&YourProvinceId=" +YourProvinceId.ToString());
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    _List = JsonConvert.DeserializeObject<List<LoadCapacitorLoad>>(content);
                    if (_List.Count == 0)
                    {
                        _ListView.IsVisible = false;
                        _StackLayoutEmptyAnnounce.IsVisible = true;
                    }
                    else
                    {
                        _ListView.ItemsSource = _List;

                    }
                }

            }
            catch (Exception ex)
            { Debug.WriteLine("\t\tERROR {0}", ex.Message); }
        }

        async void OnTapGestureRecognizerTapped(object sender, EventArgs args)
        {
            try
            {
                var MUId = PublicProcedures.ATISMobileMClassPublicProcedures.GetCurrentMobileUserId();
                var nEstelamId = ((Label)sender).Text.Split(':')[1].Trim();
                HttpClient _Client = new HttpClient();
                var response = await _Client.GetAsync(Properties.Resources.RestfulWebServiceURL + "/api/LoadAllocations/LoadAllocationAgent/?YourMUId=" + MUId + "&YournEstelamId=" + nEstelamId);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var myMS = JsonConvert.DeserializeObject<MessageStruct>(content);
                    await DisplayAlert("ATISMobile", myMS.Message1, "OK");
                }
            }
            catch (Exception ex)
            { Debug.WriteLine("\t\tERROR {0}", ex.Message); }
        }
    }
}