using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
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
    public partial class VerificationCodeEntryPage : ContentPage
    {
        public VerificationCodeEntryPage()
        {
            InitializeComponent();
        }

        public void SetInf(string YourVerificationCode, string YourMobileNumber)
        {
            _EntryVerificatinCode.Text = YourVerificationCode;
            _LabelMobileNumber.Text = YourMobileNumber;
        }

        private async void _ButtonSend_ClickedEvent(Object sender, EventArgs e)
        {
            try
            {
                _ButtonSend.IsEnabled = false; _ButtonSend.BackgroundColor = Color.Gray;
                string myMobileNumber = _LabelMobileNumber.Text.Trim();
                string myVerificationCode = _EntryVerificatinCode.Text.Trim();
                HttpClient _Client = new HttpClient();
                var response = await _Client.GetAsync(Properties.Resources.RestfulWebServiceURL + "/api/MobileUsers/ActiveMobileUser/?YourMobileNumber=" + myMobileNumber + "&YourVerificationCode=" + myVerificationCode);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var myMS = JsonConvert.DeserializeObject<MessageStruct>(content);
                    if (myMS.ErrorCode == false)
                    {
                        String TargetPath = System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                        TargetPath = Path.Combine(TargetPath, "AMUStatus.txt");
                        if (System.IO.File.Exists(TargetPath) == false)
                        { await DisplayAlert("ATISMobile", "بروز خطا - مجددا تلاش نمایید", "OK"); }
                        else
                        {
                            System.IO.File.WriteAllText(TargetPath, "login;" + myMS.Message1.Trim() + ";" + _LabelMobileNumber.Text.Trim());
                            NavigationPage _MenuPage = new NavigationPage(new MenuPage(false));

                            NavigationPage.SetHasNavigationBar(_MenuPage, false);
                            _MenuPage.BarBackgroundColor = Color.Black;
                            await Navigation.PushAsync(_MenuPage);
                        }
                    }
                    else { await DisplayAlert("ATISMobile", myMS.Message1, "OK"); }
                }
            }
            catch (Exception ex) { await DisplayAlert("ATISMobile", ex.Message, "OK"); }
            _ButtonSend.IsEnabled = true; _ButtonSend.BackgroundColor = Color.Green;
        }

        protected override bool OnBackButtonPressed()
        { return true; }

    }
}