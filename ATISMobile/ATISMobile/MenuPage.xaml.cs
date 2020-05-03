using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ATISMobile.PublicProcedures;

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
        }

        private async void _ViewLoadAllocations_ClickedEvent(Object sender, EventArgs e)
        {
            LoadAllocationsPage _LoadAllocationsPage = new LoadAllocationsPage();
            _LoadAllocationsPage.ViewLoadAllocations(ATISMobileMClassPublicProcedures.GetCurrentMobileUserId());
            await Navigation.PushAsync(_LoadAllocationsPage);
        }

        private async void _ViewTruckDriver_ClickedEvent(Object sender, EventArgs e)
        {
            TruckDriverPage _TruckDriverPage = new TruckDriverPage();
            _TruckDriverPage.ViewInformation(ATISMobileMClassPublicProcedures.GetCurrentMobileUserId());
            await Navigation.PushAsync(_TruckDriverPage);
        }

        private async void _ViewTruck_ClickedEvent(Object sender, EventArgs e)
        {
            TruckPage _TruckPage = new TruckPage();
            _TruckPage.ViewInformation(ATISMobileMClassPublicProcedures.GetCurrentMobileUserId());
            await Navigation.PushAsync(_TruckPage);
        }

        private async void _ViewTurn_ClickedEvent(Object sender, EventArgs e)
        {
            TurnsPage _TurnsPage = new TurnsPage();
            _TurnsPage.ViewInformation(ATISMobileMClassPublicProcedures.GetCurrentMobileUserId());
            await Navigation.PushAsync(_TurnsPage);
        }

        private async void _LoadAllocation_ClickedEvent(Object sender, EventArgs e)
        {
            AnnouncementHallsSelectionPage _AnnouncementHallsSelectionPage = new AnnouncementHallsSelectionPage();
            await Navigation.PushAsync(_AnnouncementHallsSelectionPage);
        }

        private async void _LoadCapacitor_ClickedEvent(Object sender, EventArgs e)
        {
            AnnouncementHallsSelectionPage _AnnouncementHallsSelectionPage = new AnnouncementHallsSelectionPage();
            await Navigation.PushAsync(_AnnouncementHallsSelectionPage);
        }

        protected override bool OnBackButtonPressed()
        {
            if (_IsBackButtonActive)
            { return false; }
            else
            { return true ; }
        }


    }
}