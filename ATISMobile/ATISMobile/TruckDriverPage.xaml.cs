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

using ATISMobile.Models;
using ATISMobile.PublicProcedures;

namespace ATISMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TruckDriverPage : ContentPage
    {
        public TruckDriverPage()
        {
            InitializeComponent();
            ViewInformation(ATISMobileMClassPublicProcedures.GetCurrentSoftwareUserId());
        }

        public async void ViewInformation(Int64 YourUserId)
        {
            try
            {
                HttpClient _Client = new HttpClient();
                var response = await _Client.GetAsync(ATISMobileMClassPublicProcedures.ATISHostURL + "/api/TruckDrivers/GetTruckDriver/?YourUserId=" + YourUserId + "");
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
                { LblErrorMessage.Text = "اطلاعاتی یافت نشد"; }
            }
            catch (Exception ex)
            { LblErrorMessage.Text = ex.Message; }
        }
    }
}