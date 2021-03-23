
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ATISMobile.PublicProcedures;
using ATISMobile.HttpClientInstance;

namespace ATISMobile.SecurityManagement
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Last5DigitEntryPage : ContentPage
    {
        #region "General Properties"
        #endregion

        #region "Subroutins And Functions"
        public Last5DigitEntryPage()
        {
            InitializeComponent();
            _ButtonConfirmation.Clicked += _ButtonConfirmation_Clicked;
            CheckforLastEntry();
            LblMilladiDateTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
            LblShamsiDateTime.Text = ATISMobileMClassPublicProcedures.GetPersianDate(DateTime.Now);
            _EntryLast5Digit.Focus();
        }


        public void CheckforLastEntry()
        {
            if (ATISMobileWebApiMClassManagement.UserLast5Digit == string.Empty)
            { _ButtonConfirmation.BackgroundColor = Color.Red; }
            else
            { _ButtonConfirmation.BackgroundColor = Color.Green; }
        }

        #endregion

        #region "Events"
        #endregion

        #region "Event Handlers"
        private async void _ButtonConfirmation_Clicked(object sender, EventArgs e)
        {
            try
            {
                ATISMobileWebApiMClassManagement.UserLast5Digit = _EntryLast5Digit.Text;

                //HttpClient _Client = new HttpClient();
                //_Client.BaseAddress = new Uri(ATISMobileWebApiMClassManagement.GetATISMobileWebApiHostUrl());
                //_Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "/api/SoftwareUsers/ApiKeyLast5DigitParing");
                request.Headers.Add("AuthCode", ATISMobileWebApiMClassManagement.GetAuthCode4PartHashed());
                request.Headers.Add("ApiKey", ATISMobileWebApiMClassManagement.GetApiKey());
                request.Headers.Add("Last5Digit", ATISMobileWebApiMClassManagement.UserLast5Digit);
                HttpResponseMessage response = await HttpClientOnlyInstance.HttpClientInstance().SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    _EntryLast5Digit.IsVisible = false;
                    _ButtonConfirmation.IsVisible = false;
                    _LblLast5DigitEntry.IsVisible = false;
                    await DisplayAlert("ATISMobile", "رمز شخصی با موفقیت ثبت گردید", "تایید");
                }
                else
                {
                    ATISMobileWebApiMClassManagement.UserLast5Digit = string.Empty;
                    await DisplayAlert("ATISMobile-Failed", JsonConvert.DeserializeObject<string>(response.Content.ReadAsStringAsync().Result), "تایید");
                }
            }
            catch (System.Net.WebException ex)
            { await DisplayAlert("ATISMobile-Error", ATISMobilePredefinedMessages.ATISWebApiNotReachedMessage, "OK"); }
            catch (Exception ex)
            { await DisplayAlert("ATISMobile-Error", ex.Message, "OK"); }
        }

        #endregion

        #region "Override Methods"
        #endregion

        #region "Abstract Methods"
        #endregion

        #region "Implemented Members"
        #endregion



    }
}