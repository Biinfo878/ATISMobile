using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATISMobile.Enums;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ATISMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AnnouncementHallsSelectionPage : TabbedPage
    {
        public AnnouncementHallsSelectionPage()
        {
            InitializeComponent();
        }

        private async void _AnnouncementHalls_ClickedEvent(Object sender, EventArgs e)
        {
            AnnouncementHallsPage _AnnouncementHallsPage = new AnnouncementHallsPage();
            await Navigation.PushAsync(_AnnouncementHallsPage);
        }
        private async void _Warehouse_ClickedEvent(Object sender, EventArgs e)
        {
            ProvinceSelectionPage _ProvinceSelectionPage = new ProvinceSelectionPage();
            _ProvinceSelectionPage.ViewInformation(3,Int64.MinValue,LoadCapacitorLoadsListType.Sedimented  );
            await Navigation.PushAsync(_ProvinceSelectionPage);
        }

        private async void _RoomyKhavar_ClickedEvent(Object sender, EventArgs e)
        {
            ProvinceSelectionPage _ProvinceSelectionPage = new ProvinceSelectionPage();
            _ProvinceSelectionPage.ViewInformation(4, 1, LoadCapacitorLoadsListType.Sedimented);
            await Navigation.PushAsync(_ProvinceSelectionPage);
        }

        private async void _Roomy6Wheel_ClickedEvent(Object sender, EventArgs e)
        {
            ProvinceSelectionPage _ProvinceSelectionPage = new ProvinceSelectionPage();
            _ProvinceSelectionPage.ViewInformation(4, 2, LoadCapacitorLoadsListType.Sedimented);
            await Navigation.PushAsync(_ProvinceSelectionPage);
        }

        private async void _Roomy10Wheel_ClickedEvent(Object sender, EventArgs e)
        {
            ProvinceSelectionPage _ProvinceSelectionPage = new ProvinceSelectionPage();
            _ProvinceSelectionPage.ViewInformation(4, 3, LoadCapacitorLoadsListType.Sedimented);
            await Navigation.PushAsync(_ProvinceSelectionPage);
        }

        
    }
}