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
                HttpResponseMessage response = await ATISMobileMClassPublicProcedures.GetResponse("/api/Trucks/GetTruck/?YourUserId=" + YourUserId + "");
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
                { LblErrorMessage.Text = "اطلاعاتی یافت نشد"; }
            }
            catch (Exception ex)
            { LblErrorMessage.Text = ex.Message; }
        }

    }
}