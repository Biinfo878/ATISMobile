using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATISMobile.PublicProcedures;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ATISMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MoneyWalletMenuPage : ContentPage
    {
        public MoneyWalletMenuPage()
        {
            InitializeComponent();
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