using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using Newtonsoft.Json;
using System.Diagnostics;

using ATISMobile.PublicProcedures;
using ATISMobile.Models;


namespace ATISMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
        private Boolean _IsBackButtonActive = true;

        public MenuPage(Boolean YourIsBackButtonActive)
        {
            InitializeComponent();
            _IsBackButtonActive = YourIsBackButtonActive;
            ShowProcesses();
        }

        private async void ShowProcesses()
        {
            try
            {
                List<MobileProcess> _Lst = new List<MobileProcess>();
                HttpResponseMessage response = await ATISMobileMClassPublicProcedures.GetResponse("/api/MobileProcesses/GetMobileProcesses/?YourSoftwareUserId=" + ATISMobileMClassPublicProcedures.GetCurrentSoftwareUserId());
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    _Lst = JsonConvert.DeserializeObject<List<MobileProcess>>(content);
                    if (_Lst.Count == 0)
                    {; }
                    else
                    { _ListView.ItemsSource = _Lst; }
                }
            }
            catch (Exception ex)
            { await DisplayAlert("ATISMobile", ex.Message, "OK"); }
        }

        async void OnTapGestureRecognizerTapped(object sender, EventArgs args)
        {
            try
            {
                string TargetMobileProcess = (((Label)sender).Parent.FindByName("_TargetMobileProcess") as Label).Text;
                string TargetMobileProcessId = (((Label)sender).Parent.FindByName("_TargetMobileProcessId") as Label).Text;
                HttpResponseMessage response = await ATISMobileMClassPublicProcedures.GetResponse("/api/Permissions/ExistPermission/?YourPermissionTypeId=1&YourEntityIdFirst=" + ATISMobileMClassPublicProcedures.GetCurrentSoftwareUserId().ToString() + "&YourEntityIdSecond=" + TargetMobileProcessId.ToString() + "");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var myMS = JsonConvert.DeserializeObject<MessageStruct>(content);
                    if (myMS.ErrorCode == false)
                    {
                        if (myMS.Message1 == "True")
                        {
                            var pageType = Type.GetType(TargetMobileProcess);
                            var page = Activator.CreateInstance(pageType) as Page;
                            await Navigation.PushAsync(page);
                        }
                        else
                        { await DisplayAlert("ATISMobile", "مجوز دسترسی به این فرآیند را ندارید", "OK"); }
                    }
                    else
                    { await DisplayAlert("ATISMobile", myMS.Message1, "OK"); }
                }
            }
            catch (Exception ex)
            { await DisplayAlert("ATISMobile", ex.Message, "OK"); }
        }

        protected override bool OnBackButtonPressed()
        {
            if (_IsBackButtonActive)
            { return false; }
            else
            { return true; }
        }


    }
}