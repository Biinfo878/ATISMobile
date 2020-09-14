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

namespace ATISMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MoneyWalletPage : ContentPage
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
                    { LstViewMoneyWalletAccounting.IsVisible = false; }
                    else
                    { LstViewMoneyWalletAccounting.ItemsSource = _List; }
                }
            }
            catch (Exception ex)
            { Debug.WriteLine("\t\tERROR {0}", ex.Message); }
        }
    }
}