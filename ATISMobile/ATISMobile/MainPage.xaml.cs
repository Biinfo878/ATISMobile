using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using Xamarin.Forms;



namespace ATISMobile
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void ViewLoads(int YourAHId, int YourAHSGId)
        {
            try
            {
        LoadsPage  _LoadsPage = new LoadsPage();
                _LoadsPage.ViewLoads(YourAHId, YourAHSGId);
                await Navigation.PushAsync(_LoadsPage);

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
        private void TapTap_ClickedEvent(Object sender, EventArgs e)
        {
            try
            {
                //String RemoteFtpPath = "ftp://192.168.85.16:1354/ATISMobile/ATISMobile.apk";
                //String TargetPath =Path.Combine( Environment.GetFolderPath(Environment.SpecialFolder.Personal ), "ATISMobile.apk");

                //FtpWebRequest request = (FtpWebRequest)WebRequest.Create(RemoteFtpPath);
                //request.Method = WebRequestMethods.Ftp.DownloadFile;
                //request.KeepAlive = false; request.UsePassive = true ; request.UseBinary = true;
                //request.Credentials = new NetworkCredential(string.Empty, string.Empty);
                //FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                //Stream responseStream = response.GetResponseStream();
                //using (FileStream writer = new FileStream(TargetPath, FileMode.Create))
                //{
                //    int bufferSize = 2048;
                //    int readCount;
                //    byte[] buffer = new byte[2048];

                //    readCount = responseStream.Read(buffer, 0, bufferSize);
                //    while (readCount > 0)
                //    {
                //        writer.Write(buffer, 0, readCount);
                //        readCount = responseStream.Read(buffer, 0, bufferSize);
                //    }
                //}
                //response.Close();
                //responseStream.Close();
                //TapTap.Text = "Download Compeleted ...";


            }
            catch (Exception ex) { Debug.WriteLine("\t\tERROR {0}", ex.Message); }
        }

    }
}
