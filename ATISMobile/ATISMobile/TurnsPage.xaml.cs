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

namespace ATISMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TurnsPage : ContentPage
    {
        public TurnsPage()
        {
            InitializeComponent();
        }

        public async void ViewInformation(Int64 YourMUId)
        {
            try
            {
                List<Turns> _List = new List<Turns>();
                HttpClient _Client = new HttpClient();
                var response = await _Client.GetAsync(Properties.Resources.RestfulWebServiceURL + "/api/Turns/GetTurns/?YourMUId=" + YourMUId + "");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    _List = JsonConvert.DeserializeObject<List<Turns>>(content);
                    if (_List.Count == 0)
                    {
                        _ListView.IsVisible = false;
                        _StackLayoutEmptyTurns.IsVisible = true;
                    }
                    else
                    {
                        _ListView.ItemsSource = _List;

                    }
                }

            }
            catch (Exception ex)
            { Debug.WriteLine("\t\tERROR {0}", ex.Message); }
        }

    }
}