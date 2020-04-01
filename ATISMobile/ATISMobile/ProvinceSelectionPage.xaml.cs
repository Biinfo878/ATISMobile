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

using ATISMobile.Models;

namespace ATISMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProvinceSelectionPage : ContentPage
    {
        public ProvinceSelectionPage()
        {
            InitializeComponent();
        }

        private Int64 _AHId;
        private Int64 _AHSGId;
            
        public async void ViewInformation(Int64  YourAHId, Int64  YourAHSGId)
        {
            _AHId = YourAHId;_AHSGId = YourAHSGId;
            try
            {
                List<Province> _List = new List<Province>();
                HttpClient _Client = new HttpClient();
                var response = await _Client.GetAsync(Properties.Resources.RestfulWebServiceURL + "/api/Provinces/GetProvinces/?YourAHId=" + YourAHId.ToString() + "&YourAHSGId=" + YourAHSGId.ToString());
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    _List = JsonConvert.DeserializeObject<List<Province>>(content);
                    if (_List.Count == 0)
                    {
                        _ListView.IsVisible = false;
                        _StackLayoutEmptyProvince.IsVisible = true;
                    }
                    else
                    { _ListView.ItemsSource = _List; }
                }

            }
            catch (Exception ex)
            { Debug.WriteLine("\t\tERROR {0}", ex.Message); }
        }

        async void OnTapGestureRecognizerTapped(object sender, EventArgs args)
        {
            try
            {
                LoadsPage _LoadsPage = new LoadsPage();
                _LoadsPage.ViewLoads(_AHId, _AHSGId, Convert.ToInt64(((Label)sender).Text.Split(':')[1]));
                await Navigation.PushAsync(_LoadsPage);
            }
            catch (Exception ex)
            { Debug.WriteLine("\t\tERROR {0}", ex.Message); }
        }

    }
}