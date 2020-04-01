using System;
using System.Resources;
using System.Net;
using System.IO;
using System.Net.Http;
using System.Diagnostics;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Newtonsoft.Json;
using Xamarin.Android;


namespace ATISMobile.Droid
{
    [Activity(Label = "ATISMobile", Icon = "@drawable/iconicon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
            UpdateAPP();
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private async void UpdateAPP()
        {
            try
            {
                HttpClient _Client = new HttpClient();
                Xamarin.Essentials.VersionTracking.Track();
                string VersionNumber = Xamarin.Essentials.VersionTracking.CurrentVersion;
                string VersionName = Xamarin.Essentials.VersionTracking.CurrentBuild;

                var responseVersion = await _Client.GetAsync(ATISMobile.Properties.Resources.RestfulWebServiceURL + "/api/VersionControl?YourVersionNumber=" + VersionNumber + "&YourVersionName=" + VersionName);
                if (responseVersion.IsSuccessStatusCode)
                {
                    var content = await responseVersion.Content.ReadAsStringAsync();
                    if (!JsonConvert.DeserializeObject<bool>(content)) return;
                }

                //ATISMobile.Droid.PublicProcedures.ATISMobileMClassPublicProcedures.ViewMessage(this, "آپدیت اپلیکیشن - لطفا تا پایان آپدیت ورژن جدید اپلیکیشن منتظر بمانید");

                String RemoteFtpPath = ATISMobile.Properties.Resources.APKFtpURL;
                //String TargetPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "ATISMobile.apk");
                String TargetPath = Path.Combine(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads).ToString(), "ATISMobile.apk");
                //String TargetPath = System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(RemoteFtpPath);
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                request.KeepAlive = false; request.UsePassive = true; request.UseBinary = true;
                request.Credentials = new NetworkCredential(string.Empty, string.Empty);
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                using (FileStream writer = new FileStream(TargetPath, FileMode.Create))
                {
                    int bufferSize = 2048;
                    int readCount;
                    byte[] buffer = new byte[2048];

                    readCount = responseStream.Read(buffer, 0, bufferSize);
                    while (readCount > 0)
                    {
                        writer.Write(buffer, 0, readCount);
                        readCount = responseStream.Read(buffer, 0, bufferSize);
                    }
                }
                response.Close();
                responseStream.Close();

                //var destination = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments ), "ATISMobile.apk");
                var destination = Path.Combine(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads).ToString(), "ATISMobile.apk");
                Intent install = new Intent(Intent.ActionInstallPackage);
                install.SetDataAndType(Android.Net.Uri.FromFile(new Java.IO.File(destination)), "application/vnd.android.package-archive");
                install.SetFlags(ActivityFlags.ClearTask | ActivityFlags.NewTask);
                install.AddFlags(ActivityFlags.GrantReadUriPermission);
                this.StartActivity(install);

                //var activity = (Activity)this;
                //activity.FinishAffinity();

            }
            catch (Exception ex)
            { await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("خطا در فرآیند آپدیت اپلیکیشن", ex.Message, "تایید"); }

        }

    }
}