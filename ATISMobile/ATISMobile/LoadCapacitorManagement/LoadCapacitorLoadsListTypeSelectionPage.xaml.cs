using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ATISMobile.Enums;

namespace ATISMobile.LoadCapacitorManagement
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoadCapacitorLoadsListTypeSelectionPage : ContentPage
    {
        #region "General Properties"
        private Int64 _AHId, _AHSGId;
        private LoadCapacitorLoadsListType _LoadCapacitorLoadsListType;

        #endregion

        #region "Subroutins And Functions"
        public LoadCapacitorLoadsListTypeSelectionPage()
        { InitializeComponent(); }

        public void ViewInformation(Int64 YourAHId, Int64 YourAHSGId)
        { _AHId = YourAHId; _AHSGId = YourAHSGId; }

        private async void ViewProvinces(LoadCapacitorLoadsListType YourLoadCapacitorLoadsListType)
        {
            try
            {
                ProvincesManagement.ProvinceSelectionPage _ProvinceSelectionPage = new ProvincesManagement.ProvinceSelectionPage();
                _ProvinceSelectionPage.ViewInformation(_AHId, _AHSGId, YourLoadCapacitorLoadsListType);
                await Navigation.PushAsync(_ProvinceSelectionPage);
            }
            catch (Exception ex)
            { await DisplayAlert("ATISMobile-Error", ex.Message, "OK"); }
        }

        #endregion

        #region "Events"
        #endregion

        #region "Event Handlers"
        private void BtnNotSedimented_ClickedEvent(Object sender, EventArgs e)
        {
            try { ViewProvinces(LoadCapacitorLoadsListType.NotSedimented); }
            catch (Exception ex)
            { DisplayAlert("ATISMobile-Error", ex.Message, "OK"); }
        }

        private void BtnSedimented_ClickedEvent(Object sender, EventArgs e)
        {
            try { ViewProvinces(LoadCapacitorLoadsListType.Sedimented); }
            catch (Exception ex)
            { DisplayAlert("ATISMobile-Error", ex.Message, "OK"); }
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