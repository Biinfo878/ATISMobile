using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ATISMobile.Models;
using ATISMobile.PublicProcedures;


namespace ATISMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MoneyWalletPage : TabbedPage
    {
        public MoneyWalletPage()
        {
            InitializeComponent();
        }



        public async void ViewInformation(Int64 YourMUId)
        {
            try
            {
                List<MoneyWalletAccounting> _List = new List<MoneyWalletAccounting>();
                HttpClient _Client = new HttpClient();
                var response = await _Client.GetAsync(Properties.Resources.RestfulWebServiceURL + "/api/MoneyWalletAccounting/GetMoneyWalletAccounting/?YourMUId=" + YourMUId + "");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    _List = JsonConvert.DeserializeObject<List<MoneyWalletAccounting>>(content);
                    if (_List.Count == 0)
                    { _ListView.IsVisible = false; }
                    else
                    { _ListView.ItemsSource = _List; }
                }
                else { Debug.WriteLine(response.RequestMessage); }
            }
            catch (Exception ex)
            { Debug.WriteLine("\t\tERROR {0}", ex.Message); }
        }

        private void _Btn50000_ClickedEvent(object sender, EventArgs e)
        {
            Int64 Amount = System.Convert.ToInt64(_LblAmount.Text.Replace(",", string.Empty)) + 50000;
            _LblAmount.Text = $"{Amount:n0}";
        }

        private void _Btn40000_ClickedEvent(object sender, EventArgs e)
        {
            Int64 Amount = System.Convert.ToInt64(_LblAmount.Text.Replace(",", string.Empty)) + 40000;
            _LblAmount.Text = $"{Amount:n0}";
        }

        private void _Btn30000_ClickedEvent(object sender, EventArgs e)
        {
            Int64 Amount = System.Convert.ToInt64(_LblAmount.Text.Replace(",", string.Empty)) + 30000;
            _LblAmount.Text = $"{Amount:n0}";
        }

        private void _Btn20000_ClickedEvent(object sender, EventArgs e)
        {
            Int64 Amount = System.Convert.ToInt64(_LblAmount.Text.Replace(",", string.Empty)) + 20000;
            _LblAmount.Text = $"{Amount:n0}";
        }

        private void _Btn10000_ClickedEvent(object sender, EventArgs e)
        {
            Int64 Amount = System.Convert.ToInt64(_LblAmount.Text.Replace(",", string.Empty)) + 10000;
            _LblAmount.Text = $"{Amount:n0}";
        }

        private void _Btn5000_ClickedEvent(object sender, EventArgs e)
        {
            Int64 Amount = System.Convert.ToInt64(_LblAmount.Text.Replace(",", string.Empty)) + 5000;
            _LblAmount.Text = $"{Amount:n0}";
        }

        private async void _BtnGotoCharge_ClickedEvent(object sender, EventArgs e)
        {
            try
            {
                Int64 Amount = System.Convert.ToInt64(_LblAmount.Text.Replace(",", string.Empty));
                var MUId = PublicProcedures.ATISMobileMClassPublicProcedures.GetCurrentMobileUserId();
                HttpClient _Client = new HttpClient();
                var response = await _Client.GetAsync(Properties.Resources.RestfulWebServiceURL + "/api/MoneyWalletChargingAPI/PaymentRequest/?YourMUId=" + MUId + "&YourAmount=" + Amount.ToString());
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

        private void _BtnRefreshTransactions_ClickedEvent(object sender, EventArgs e)
        {
            try
            { ViewInformation(ATISMobileMClassPublicProcedures.GetCurrentMobileUserId()); }
            catch (Exception ex)
            { Debug.WriteLine("\t\tERROR {0}", ex.Message); }
        }
    }
}