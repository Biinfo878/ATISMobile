using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Diagnostics;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;

using ATISMobile.Models;

namespace ATISMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoadsPage : ContentPage
    {
        public LoadsPage()
        {
            InitializeComponent();
        }

        public async void ViewLoads(int YourAHId, int YourAHSGId)
        {
            try
            {
                List<LoadCapacitorLoad> _List = new List<LoadCapacitorLoad>();
                HttpClient _Client = new HttpClient();
                var response = await _Client.GetAsync(Properties.Resources.RestfulWebServiceURL+"/api/defaultt?YourAHId=" + YourAHId.ToString() + "&YourAHSGId=" + YourAHSGId.ToString());
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    _List = JsonConvert.DeserializeObject<List<LoadCapacitorLoad>>(content);
                    if (_List.Count == 0)
                    {
                        _ListView.IsVisible = false;
                        _StackLayoutEmptyAnnounce.IsVisible = true;
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