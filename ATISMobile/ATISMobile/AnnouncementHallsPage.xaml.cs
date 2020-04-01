using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

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

        private async void ViewLoads(int YourAHId, int YourAHSGId)
        {
            try
            {
                LoadsPage _LoadsPage = new LoadsPage();
                _LoadsPage.ViewLoads(YourAHId, YourAHSGId);
                await Navigation.PushAsync(_LoadsPage);

            }
            catch (Exception ex)
            { Debug.WriteLine("\t\tERROR {0}", ex.Message); }
        }

        private async void ViewProvinces(Int64 YourAHId, Int64 YourAHSGId)
        {
            try
            {
                ProvinceSelectionPage _ProvinceSelectionPage = new ProvinceSelectionPage();
                _ProvinceSelectionPage.ViewInformation(YourAHId, YourAHSGId);
                await Navigation.PushAsync(_ProvinceSelectionPage);

            }
            catch (Exception ex)
            { Debug.WriteLine("\t\tERROR {0}", ex.Message); }

        }

        private void RoadIron_ClickedEvent(Object sender, EventArgs e)
        { try { ViewLoads(2, 7); } catch (Exception ex) { Debug.WriteLine("\t\tERROR {0}", ex.Message); } }
        private void RoadIngots_ClickedEvent(Object sender, EventArgs e)
        { try { ViewLoads(2, 8); } catch (Exception ex) { Debug.WriteLine("\t\tERROR {0}", ex.Message); } }
        private void RoadExport_ClickedEvent(Object sender, EventArgs e)
        { try { ViewLoads(2, 9); } catch (Exception ex) { Debug.WriteLine("\t\tERROR {0}", ex.Message); } }
        private void RoadCoil_ClickedEvent(Object sender, EventArgs e)
        { try { ViewLoads(2, 11); } catch (Exception ex) { Debug.WriteLine("\t\tERROR {0}", ex.Message); } }
        private void UrbanIron_ClickedEvent(Object sender, EventArgs e)
        { try { ViewLoads(5, 12); } catch (Exception ex) { Debug.WriteLine("\t\tERROR {0}", ex.Message); } }
        private void UrbanCoil_ClickedEvent(Object sender, EventArgs e)
        { try { ViewLoads(2, 13); } catch (Exception ex) { Debug.WriteLine("\t\tERROR {0}", ex.Message); } }
        private void WarehouseLoadIron_ClickedEvent(Object sender, EventArgs e)
        { try { ViewProvinces(3, 4); } catch (Exception ex) { Debug.WriteLine("\t\tERROR {0}", ex.Message); } }
        private void WarehouseLoadIngots_ClickedEvent(Object sender, EventArgs e)
        { try { ViewProvinces(3, 5); } catch (Exception ex) { Debug.WriteLine("\t\tERROR {0}", ex.Message); } }
        private void WarehouseLoadExport_ClickedEvent(Object sender, EventArgs e)
        { try { ViewProvinces(3, 6); } catch (Exception ex) { Debug.WriteLine("\t\tERROR {0}", ex.Message); } }
        private void WarehouseLoadCoil_ClickedEvent(Object sender, EventArgs e)
        { try { ViewProvinces(3, 10); } catch (Exception ex) { Debug.WriteLine("\t\tERROR {0}", ex.Message); } }
        private void RoomyKhavar_ClickedEvent(Object sender, EventArgs e)
        { try { ViewProvinces(4, 1); } catch (Exception ex) { Debug.WriteLine("\t\tERROR {0}", ex.Message); } }
        private void Roomy6Wheel_ClickedEvent(Object sender, EventArgs e)
        { try { ViewProvinces(4, 2); } catch (Exception ex) { Debug.WriteLine("\t\tERROR {0}", ex.Message); } }
        private void Roomy10Wheel_ClickedEvent(Object sender, EventArgs e)
        { try { ViewProvinces(4, 3); } catch (Exception ex) { Debug.WriteLine("\t\tERROR {0}", ex.Message); } }

    }
}