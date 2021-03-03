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
    public partial class MoneyWalletTransactionsPage : ContentPage
    {
        public MoneyWalletTransactionsPage()
        {
            InitializeComponent();
            ViewInformation(ATISMobileMClassPublicProcedures.GetCurrentSoftwareUserId());
        }

        public async void ViewInformation(Int64 YourUserId)
        {
            try
            {
                List<MoneyWalletAccounting> _List = new List<MoneyWalletAccounting>();
                HttpResponseMessage response = await ATISMobileMClassPublicProcedures.GetResponse("/api/MoneyWalletAccounting/GetMoneyWalletAccounting/?YourUserId=" + YourUserId + "");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    _List = JsonConvert.DeserializeObject<List<MoneyWalletAccounting>>(content);
                    if (_List.Count == 0)
                    { _ListView.IsVisible = false; }
                    else
                    { _ListView.ItemsSource = _List; }
                }
            }
            catch (Exception ex)
            { await DisplayAlert("ATISMobile", ex.Message, "OK"); }
        }


        private void _BtnRefreshTransactions_ClickedEvent(object sender, EventArgs e)
        {
            try
            { ViewInformation(ATISMobileMClassPublicProcedures.GetCurrentSoftwareUserId()); }
            catch (Exception ex)
            { Debug.WriteLine("\t\tERROR {0}", ex.Message); }
        }

    }
}