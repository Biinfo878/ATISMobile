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
    public partial class LoadAllocationsPage : ContentPage
    {
        private Int64 _CurrentUserId;

        public LoadAllocationsPage()
        {
            InitializeComponent();
            ViewLoadAllocations(ATISMobileMClassPublicProcedures.GetCurrentSoftwareUserId());
        }

        public async void ViewLoadAllocations(Int64 YourUserId)
        {
            try
            {
                _CurrentUserId = YourUserId;
                List<LoadAllocationsforTruckDriver> _List = new List<LoadAllocationsforTruckDriver>();
                HttpResponseMessage response = await ATISMobileMClassPublicProcedures.GetResponse("/api/LoadAllocations/GetLoadAllocationsforTruckDriver/?YourUserId=" + YourUserId + "");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    _List = JsonConvert.DeserializeObject<List<LoadAllocationsforTruckDriver>>(content);
                    if (_List.Count == 0)
                    { _ListView.IsVisible = false; _StackLayoutEmptyAllocations.IsVisible = true; }
                    else
                    { _ListView.ItemsSource = _List; }
                }
            }
            catch (Exception ex)
            { await DisplayAlert("ATISMobile", ex.Message, "OK"); }
        }

        async void OnClicked_DeleteLoadAllocation(object sender, EventArgs args)
        {
            try
            {
                var LoadAllocationId = (((StackLayout)((ImageButton)sender).Parent.Parent.FindByName("_StackLayoutInformation")).FindByName("_LabelLAId") as Label).Text.Split('-')[0].Split(':')[1].Trim();
                HttpResponseMessage response = await ATISMobileMClassPublicProcedures.GetResponse("/api/LoadAllocations/LoadAllocationCancelling/?YourUserId=" + ATISMobileMClassPublicProcedures.GetCurrentSoftwareUserId().ToString() + "&YourLoadAllocationId=" + LoadAllocationId + "");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var myMS = JsonConvert.DeserializeObject<MessageStruct>(content);
                    ViewLoadAllocations(_CurrentUserId);
                    await DisplayAlert("ATISMobile", myMS.Message1, "OK");
                }
            }
            catch (Exception ex)
            { await DisplayAlert("ATISMobile", ex.Message, "OK"); }
        }

        async void OnClicked_InreasePriority(object sender, EventArgs args)
        {
            try
            {
                var LoadAllocationId = (((StackLayout)((ImageButton)sender).Parent.Parent.FindByName("_StackLayoutInformation")).FindByName("_LabelLAId") as Label).Text.Split('-')[0].Split(':')[1].Trim();
                HttpResponseMessage response = await ATISMobileMClassPublicProcedures.GetResponse("/api/LoadAllocations/IncreasePriority/?YourLoadAllocationId=" + LoadAllocationId + "");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var myMS = JsonConvert.DeserializeObject<MessageStruct>(content);
                    ViewLoadAllocations(_CurrentUserId);
                    await DisplayAlert("ATISMobile", myMS.Message1, "OK");
                }
            }
            catch (Exception ex)
            { await DisplayAlert("ATISMobile", ex.Message, "OK"); }

        }

        async void OnClicked_DecreasePriority(object sender, EventArgs args)
        {
            try
            {
                var LoadAllocationId = (((StackLayout)((ImageButton)sender).Parent.Parent.FindByName("_StackLayoutInformation")).FindByName("_LabelLAId") as Label).Text.Split('-')[0].Split(':')[1].Trim();
                HttpResponseMessage response = await ATISMobileMClassPublicProcedures.GetResponse("/api/LoadAllocations/DecreasePriority/?YourLoadAllocationId=" + LoadAllocationId + "");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var myMS = JsonConvert.DeserializeObject<MessageStruct>(content);
                    ViewLoadAllocations(_CurrentUserId);
                    await DisplayAlert("ATISMobile", myMS.Message1, "OK");
                }
            }
            catch (Exception ex)
            { await DisplayAlert("ATISMobile", ex.Message, "OK"); }
        }






    }
}