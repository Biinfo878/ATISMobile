using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ATISMobile.PublicProcedures;
using ATISMobile.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;

namespace ATISMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MoneyWalletMenuPage : ContentPage
    {
        public MoneyWalletMenuPage()
        {
            InitializeComponent();
            try { ViewMoneyWalletIDandReminderCharge(); }
            catch (Exception ex) { DisplayAlert("ATISMobile", ex.Message, "OK"); }
        }

        private async void ViewMoneyWalletIDandReminderCharge()
        {
            try
            {
                HttpResponseMessage response = await ATISMobileMClassPublicProcedures.GetResponse("/api/MoneyWalletAccounting/GetMoneyWalletIDandReminderCharge/?YourUserId=" + ATISMobileMClassPublicProcedures.GetCurrentSoftwareUserId().ToString() + "");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    MessageStruct Result = JsonConvert.DeserializeObject<MessageStruct>(content);
                    _LblMoneyWalletId.Text = Result.Message1;
                    _LblReminderCharge.Text = Result.Message2;
                    _LblMoneyWalletIdHeader.IsVisible = true;
                    _LblReminderChargeHeader.IsVisible = true;
                }
            }
            catch (Exception ex)
            { await DisplayAlert("ATISMobile", ex.Message, "OK"); }
        }

        private async void _BtnMoneyWalletTransactions_ClickedEvent(object sender, EventArgs e)
        {
            MoneyWalletTransactionsPage _MoneyWallettTransactionsPage = new MoneyWalletTransactionsPage();
            _MoneyWallettTransactionsPage.ViewInformation(ATISMobileMClassPublicProcedures.GetCurrentSoftwareUserId());
            await Navigation.PushAsync(_MoneyWallettTransactionsPage);
        }

        private async void _BtnMoneyWalletCharging_ClickedEvent(object sender, EventArgs e)
        {
            MoneyWalletChargingPage _MoneyWalletChargingPage = new MoneyWalletChargingPage();
            await Navigation.PushAsync(_MoneyWalletChargingPage);
        }

    }
}