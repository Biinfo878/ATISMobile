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
    public partial class TruckDriverPage : ContentPage
    {
        public TruckDriverPage()
        {
            InitializeComponent();
        }

        public async void ViewInformation(Int64 YourMUId)
        {
            try
            {
                HttpClient _Client = new HttpClient();
                var response = await _Client.GetAsync(Properties.Resources.RestfulWebServiceURL + "/api/TruckDrivers/GetTruckDriver/?YourMUId=" + YourMUId + "");
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
                }
                else
                { LblNameFamily.Text = "اطلاعاتی یافت نشد"; }
            }
            catch (Exception ex)
            { Debug.WriteLine("\t\tERROR {0}", ex.Message); }
        }
    }
}