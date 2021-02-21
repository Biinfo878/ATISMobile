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
using ATISMobile.PublicProcedures;

namespace ATISMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TruckPage : ContentPage
    {
        public TruckPage()
        {
            InitializeComponent();
            ViewInformation(ATISMobileMClassPublicProcedures.GetCurrentSoftwareUserId());
        }

        public async void ViewInformation(Int64 YourUserId)
        {
            try
            {
                HttpClient _Client = new HttpClient();
                var response = await _Client.GetAsync(Properties.Resources.RestfulWebServiceURL + "/api/Trucks/GetTruck/?YourUserId=" + YourUserId + "");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var myTruck = JsonConvert.DeserializeObject<Truck>(content);
                    LblTruckId .Text = myTruck.TruckId ;
                    LblLPString .Text = myTruck.LPString ;
                    LblLoaderTitle .Text = myTruck.LoaderTitle ;
                    LblSmartCardNo .Text = myTruck.SmartCardNo ;
                }
                else
                { LblLPString .Text = "اطلاعاتی یافت نشد"; }
            }
            catch (Exception ex)
            { Debug.WriteLine("\t\tERROR {0}", ex.Message); }
        }

    }
}