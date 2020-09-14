using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using ATISMobile.Enums;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ATISMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AnnouncementHallsPage : ContentPage
    {
        public AnnouncementHallsPage()
        {
            InitializeComponent();
        }

        private async void ViewProvinces(int YourAHId, int YourAHSGId)
        {
            try
            {
                LoadCapacitorLoadsListTypeSelectionPage _LoadCapacitorLoadsListTypeSelectionPage = new LoadCapacitorLoadsListTypeSelectionPage();
                _LoadCapacitorLoadsListTypeSelectionPage.ViewInformation(YourAHId,YourAHSGId);
                await Navigation.PushAsync(_LoadCapacitorLoadsListTypeSelectionPage);
            }
            catch (Exception ex)
            { Debug.WriteLine("\t\tERROR {0}", ex.Message); }
        }

        private void RoadIron_ClickedEvent(Object sender, EventArgs e)
        { try { ViewProvinces(2, 7); } catch (Exception ex) { Debug.WriteLine("\t\tERROR {0}", ex.Message); } }
        private void RoadIngots_ClickedEvent(Object sender, EventArgs e)
        { try { ViewProvinces(2, 8); } catch (Exception ex) { Debug.WriteLine("\t\tERROR {0}", ex.Message); } }
        private void RoadExport_ClickedEvent(Object sender, EventArgs e)
        { try { ViewProvinces(2, 9); } catch (Exception ex) { Debug.WriteLine("\t\tERROR {0}", ex.Message); } }
        private void RoadCoil_ClickedEvent(Object sender, EventArgs e)
        { try { ViewProvinces(2, 11); } catch (Exception ex) { Debug.WriteLine("\t\tERROR {0}", ex.Message); } }
        private void UrbanIron_ClickedEvent(Object sender, EventArgs e)
        { try { ViewProvinces(5, 12); } catch (Exception ex) { Debug.WriteLine("\t\tERROR {0}", ex.Message); } }
        private void UrbanCoil_ClickedEvent(Object sender, EventArgs e)
        { try { ViewProvinces(5, 13); } catch (Exception ex) { Debug.WriteLine("\t\tERROR {0}", ex.Message); } }

    }
}