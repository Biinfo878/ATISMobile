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
    public partial class TurnsPage : ContentPage
    {
        public TurnsPage()
        {
            InitializeComponent();
            ViewInformation(ATISMobileMClassPublicProcedures.GetCurrentSoftwareUserId());
        }

        public async void ViewInformation(Int64 YourUserId)
        {
            try
            {
                List<Turns> _List = new List<Turns>();
                HttpResponseMessage response = await ATISMobileMClassPublicProcedures.GetResponse("/api/Turns/GetTurns/?YourUserId=" + YourUserId + "");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    _List = JsonConvert.DeserializeObject<List<Turns>>(content);
                    if (_List.Count == 0)
                    { _ListView.IsVisible = false; _StackLayoutEmptyTurns.IsVisible = true; }
                    else
                    { _ListView.ItemsSource = _List; }
                }
            }
            catch (Exception ex)
            { await DisplayAlert("ATISMobile", ex.Message, "OK"); }
        }

    }
}