using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATISMobile.Enums;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ATISMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoadCapacitorLoadsListTypeSelectionPage : ContentPage
    {
        public LoadCapacitorLoadsListTypeSelectionPage()
        {
            InitializeComponent();

        }

        private Int64 _AHId, _AHSGId;
        private LoadCapacitorLoadsListType _LoadCapacitorLoadsListType;

        public void ViewInformation(Int64 YourAHId, Int64 YourAHSGId)
        { _AHId = YourAHId; _AHSGId = YourAHSGId; }

        private async void ViewProvinces(LoadCapacitorLoadsListType YourLoadCapacitorLoadsListType)
        {
            try
            {
                ProvinceSelectionPage _ProvinceSelectionPage = new ProvinceSelectionPage();
                _ProvinceSelectionPage.ViewInformation(_AHId, _AHSGId, YourLoadCapacitorLoadsListType);
                await Navigation.PushAsync(_ProvinceSelectionPage);
            }
            catch (Exception ex)
            { Debug.WriteLine("\t\tERROR {0}", ex.Message); }
        }

        private void BtnNotSedimented_ClickedEvent(Object sender, EventArgs e)
        { try { ViewProvinces(LoadCapacitorLoadsListType.NotSedimented); } catch (Exception ex) { Debug.WriteLine("\t\tERROR {0}", ex.Message); } }

        private void BtnSedimented_ClickedEvent(Object sender, EventArgs e)
        { try { ViewProvinces(LoadCapacitorLoadsListType.Sedimented); } catch (Exception ex) { Debug.WriteLine("\t\tERROR {0}", ex.Message); } }



    }
}