using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Diagnostics;
using ATISMobile.Enums;
using Newtonsoft.Json;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ATISMobile.Models;
using ATISMobile.PublicProcedures;

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
        private LoadCapacitorLoadsListType _LoadCapacitorLoadsListType;

        public async void ViewInformation(Int64 YourAHId, Int64 YourAHSGId, LoadCapacitorLoadsListType YourLoadCapacitorLoadsListType)
        {
            _AHId = YourAHId; _AHSGId = YourAHSGId;
            _LoadCapacitorLoadsListType = YourLoadCapacitorLoadsListType;
            try
            {
                List<Province> _List = new List<Province>();
                HttpResponseMessage response = await ATISMobileMClassPublicProcedures.GetResponse("/api/Provinces/GetProvinces/?YourAHId=" + YourAHId.ToString() + "&YourAHSGId=" + YourAHSGId.ToString() + "&YourLoadCapacitorLoadsListType=" + YourLoadCapacitorLoadsListType.ToString());
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
            { await DisplayAlert("ATISMobile", ex.Message, "OK"); }
        }

        async void OnTapGestureRecognizerTapped(object sender, EventArgs args)
        {
            try
            {
                LoadsPage _LoadsPage = new LoadsPage();
                _LoadsPage.ViewLoads(_AHId, _AHSGId, Convert.ToInt64((((Label)sender).Parent.FindByName("_ProvinceId") as Label).Text.Split(':')[1]), (((Label)sender).Parent.FindByName("_ProvinceTitle") as Label).Text.Split(':')[0], _LoadCapacitorLoadsListType);
                await Navigation.PushAsync(_LoadsPage);
            }
            catch (Exception ex)
            { Debug.WriteLine("\t\tERROR {0}", ex.Message); }
        }

    }
}