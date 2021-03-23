using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ATISMobile.Enums;

namespace ATISMobile.AnnouncementHallsManagement
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AnnouncementHallsSelectionPage : TabbedPage
    {
        #region "General Properties"
        #endregion

        #region "Subroutins And Functions"
        public AnnouncementHallsSelectionPage()
        { InitializeComponent(); }

        #endregion

        #region "Events"
        #endregion

        #region "Event Handlers"
        private async void _AnnouncementHalls_ClickedEvent(Object sender, EventArgs e)
        {
            AnnouncementHallsPage _AnnouncementHallsPage = new AnnouncementHallsPage();
            await Navigation.PushAsync(_AnnouncementHallsPage);
        }

        private async void _Warehouse_ClickedEvent(Object sender, EventArgs e)
        {
            try
            {
                ProvincesManagement.ProvinceSelectionPage _ProvinceSelectionPage = new ProvincesManagement.ProvinceSelectionPage();
                _ProvinceSelectionPage.ViewInformation(3, Int64.MinValue, LoadCapacitorLoadsListType.NotSedimented);
                await Navigation.PushAsync(_ProvinceSelectionPage);
            }
            catch (Exception ex)
            { await DisplayAlert("ATISMobile-Error", ex.Message, "OK"); }
        }

        private async void _RoomyKhavar_ClickedEvent(Object sender, EventArgs e)
        {
            try
            {
                ProvincesManagement.ProvinceSelectionPage _ProvinceSelectionPage = new ProvincesManagement.ProvinceSelectionPage();
                _ProvinceSelectionPage.ViewInformation(4, 1, LoadCapacitorLoadsListType.NotSedimented);
                await Navigation.PushAsync(_ProvinceSelectionPage);
            }
            catch (Exception ex)
            { await DisplayAlert("ATISMobile-Error", ex.Message, "OK"); }
        }

        private async void _Roomy6Wheel_ClickedEvent(Object sender, EventArgs e)
        {
            try
            {
                ProvincesManagement.ProvinceSelectionPage _ProvinceSelectionPage = new ProvincesManagement.ProvinceSelectionPage();
                _ProvinceSelectionPage.ViewInformation(4, 2, LoadCapacitorLoadsListType.NotSedimented);
                await Navigation.PushAsync(_ProvinceSelectionPage);
            }
            catch (Exception ex)
            { await DisplayAlert("ATISMobile-Error", ex.Message, "OK"); }
        }

        private async void _Roomy10Wheel_ClickedEvent(Object sender, EventArgs e)
        {
            try
            {
                ProvincesManagement.ProvinceSelectionPage _ProvinceSelectionPage = new ProvincesManagement.ProvinceSelectionPage();
                _ProvinceSelectionPage.ViewInformation(4, 3, LoadCapacitorLoadsListType.NotSedimented);
                await Navigation.PushAsync(_ProvinceSelectionPage);
            }
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