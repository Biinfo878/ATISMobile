using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ATISMobile.Enums;

namespace ATISMobile.AnnouncementHallsManagement
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AnnouncementHallsPage : ContentPage
    {
        #region "General Properties"
        #endregion

        #region "Subroutins And Functions"

        public AnnouncementHallsPage()
        { InitializeComponent(); }

        private async void ViewProvinces(int YourAHId, int YourAHSGId)
        {
            try
            {
                LoadCapacitorManagement.LoadCapacitorLoadsListTypeSelectionPage _LoadCapacitorLoadsListTypeSelectionPage = new LoadCapacitorManagement.LoadCapacitorLoadsListTypeSelectionPage();
                _LoadCapacitorLoadsListTypeSelectionPage.ViewInformation(YourAHId, YourAHSGId);
                await Navigation.PushAsync(_LoadCapacitorLoadsListTypeSelectionPage);
            }
            catch (Exception ex)
            { await DisplayAlert("ATISMobile-Error", ex.Message, "OK"); }
        }

        #endregion

        #region "Events"
        #endregion

        #region "Event Handlers"
        private void RoadIron_ClickedEvent(Object sender, EventArgs e)
        { try { ViewProvinces(2, 7); } catch (Exception ex) { DisplayAlert("ATISMobile-Error", ex.Message, "OK"); } }
        private void RoadIngots_ClickedEvent(Object sender, EventArgs e)
        { try { ViewProvinces(2, 8); } catch (Exception ex) { DisplayAlert("ATISMobile-Error", ex.Message, "OK"); } }
        private void RoadExport_ClickedEvent(Object sender, EventArgs e)
        { try { ViewProvinces(2, 9); } catch (Exception ex) { DisplayAlert("ATISMobile-Error", ex.Message, "OK"); } }
        private void RoadCoil_ClickedEvent(Object sender, EventArgs e)
        { try { ViewProvinces(2, 11); } catch (Exception ex) { DisplayAlert("ATISMobile-Error", ex.Message, "OK"); } }
        private void UrbanIron_ClickedEvent(Object sender, EventArgs e)
        { try { ViewProvinces(5, 12); } catch (Exception ex) { DisplayAlert("ATISMobile-Error", ex.Message, "OK"); } }
        private void UrbanCoil_ClickedEvent(Object sender, EventArgs e)
        { try { ViewProvinces(5, 13); } catch (Exception ex) { DisplayAlert("ATISMobile-Error", ex.Message, "OK"); } }
        #endregion

        #region "Override Methods"
        #endregion

        #region "Abstract Methods"
        #endregion

        #region "Implemented Members"
        #endregion




    }
}