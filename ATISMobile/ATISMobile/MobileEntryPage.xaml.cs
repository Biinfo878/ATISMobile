using System;
using System.Windows;
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
    public partial class MobileEntryPage : ContentPage
    {
        public MobileEntryPage()
        {
            InitializeComponent();
        }

        private async void _Button_ClickedEvent(Object sender, EventArgs e)
        {
            try
            {
                string myMobileNumber = _EntryMobileNumber.Text.Trim();
                string myNameFamily = _EntryNameFamily.Text.Trim();
                HttpClient _Client = new HttpClient();
                var response = await _Client.GetAsync(Properties.Resources.RestfulWebServiceURL + "/api/MobileUsers/RegisterMobileUser/?YourMobileNumber=" + myMobileNumber + "&YourNameFamily=" + myNameFamily);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var myMS = JsonConvert.DeserializeObject<MessageStruct>(content);
                    if (myMS.ErrorCode == false)
                    {
                        VerificationCodeEntryPage _VerificationCodeEntryPage = new VerificationCodeEntryPage();
                        _VerificationCodeEntryPage.SetInf(myMS.Message1, _EntryMobileNumber.Text);
                        await Navigation.PushAsync(_VerificationCodeEntryPage);
                    }
                    else
                    { await DisplayAlert("ATISMobile", myMS.Message1, "OK"); }
                }
            }
            catch (Exception ex) { Debug.WriteLine("\t\tERROR {0}", ex.Message); }
        }
    }
}