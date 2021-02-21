using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ATISMobile.Models;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ATISMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MoneyWalletChargingPage : ContentPage
    {
        public MoneyWalletChargingPage()
        {
            InitializeComponent();
        }

        private void _Btn50000_ClickedEvent(object sender, EventArgs e)
        { _LblAmount.Text = $"{50000:n0}"; }

        private void _Btn40000_ClickedEvent(object sender, EventArgs e)
        { _LblAmount.Text = $"{40000:n0}"; }

        private void _Btn30000_ClickedEvent(object sender, EventArgs e)
        { _LblAmount.Text = $"{30000:n0}"; }

        private void _Btn20000_ClickedEvent(object sender, EventArgs e)
        { _LblAmount.Text = $"{20000:n0}"; }

        private void _Btn10000_ClickedEvent(object sender, EventArgs e)
        { _LblAmount.Text = $"{10000:n0}"; }

        private void _Btn5000_ClickedEvent(object sender, EventArgs e)
        { _LblAmount.Text = $"{5000:n0}"; }

        private async void _BtnGotoCharge_ClickedEvent(object sender, EventArgs e)
        {
            try
            {
                Int64 Amount = System.Convert.ToInt64(_LblAmount.Text.Replace(",", string.Empty));
                var UserId = PublicProcedures.ATISMobileMClassPublicProcedures.GetCurrentSoftwareUserId();
                HttpClient _Client = new HttpClient();
                var response = await _Client.GetAsync(Properties.Resources.RestfulWebServiceURL + "/api/MoneyWalletChargingAPI/PaymentRequest/?YourUserId=" + UserId + "&YourAmount=" + Amount.ToString());
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var myMS = JsonConvert.DeserializeObject<MessageStruct>(content);
                    //NoError : Message2=https://sandbox.zarinpal.com/pg/StartPay/ Message1=Autority
                    //Error   : Message1=Error Message String
                    if (myMS.ErrorCode == false)
                    { Device.OpenUri(new Uri(myMS.Message2 + myMS.Message1)); }
                    else
                    { await DisplayAlert("ATISMobile", myMS.Message1, "OK"); }
                }
            }
            catch (Exception ex)
            { Debug.WriteLine("\t\tERROR {0}", ex.Message); }

        }

    }
}