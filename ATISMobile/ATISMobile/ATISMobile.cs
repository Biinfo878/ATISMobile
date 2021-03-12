using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Net.Http;
using System.Net.NetworkInformation;
using Newtonsoft.Json;
using System.Net;
using System.IO;

using Xamarin.Essentials;

using ATISMobile.Exceptions;
using System.Threading.Tasks;
using Xamarin.Forms.PlatformConfiguration;

namespace ATISMobile
{
    namespace Enums
    {
        public enum LoadCapacitorLoadsListType
        {
            None = 0,
            NotSedimented = 1,
            Sedimented = 2,
            TommorowLoad = 3
        }

    }

    namespace PublicProcedures
    {
        public class ATISMobileMClassPublicProcedures
        {
            public static bool IsThisIPAvailable(String YourIP)
            {
                try
                {
                    Ping p = new Ping();
                    if (p.Send(YourIP).Status == IPStatus.Success) { return true; } else { return false; }
                }
                catch (Exception ex)
                { return false; }
            }

            public static bool IsInternetAvailable()
            {
                try { return IsThisIPAvailable("www.google.com"); }
                catch (Exception ex) { return false; }
            }

            private static string _RestfullPath = "1";
            public static async Task<HttpResponseMessage> GetResponse(string YourapiString)
            {
                try
                {
                    if (_RestfullPath == "1")
                    {
                        try
                        {
                            HttpClient _Client = new HttpClient();
                            var response = await _Client.GetAsync(ATISMobileMClassPublicProcedures.ATISHostURL(1) + YourapiString);
                            _RestfullPath = "1";
                            return response;
                        }
                        catch (Exception ex)
                        {
                            HttpClient _Client = new HttpClient();
                            var response = await _Client.GetAsync(ATISMobileMClassPublicProcedures.ATISHostURL(2) + YourapiString);
                            _RestfullPath = "2";
                            return response;
                        }
                    }
                    else if (_RestfullPath == "2")
                    {
                        try
                        {
                            HttpClient _Client = new HttpClient();
                            var response = await _Client.GetAsync(ATISMobileMClassPublicProcedures.ATISHostURL(2) + YourapiString);
                            _RestfullPath = "2";
                            return response;
                        }
                        catch (Exception ex)
                        {
                            HttpClient _Client = new HttpClient();
                            var response = await _Client.GetAsync(ATISMobileMClassPublicProcedures.ATISHostURL(1) + YourapiString);
                            _RestfullPath = "1";
                            return response;
                        }
                    }
                    throw new Exception();
                }
                catch (Exception ex)
                { throw ex; }
            }

            private static string ATISHostURL(Int64 YourIndex)
            {
                if (YourIndex == 1)
                { return Properties.Resources.RestfulWebServiceProtocol + "://" + Properties.Resources.RestfulWebServiceURLFirst + ":" + Properties.Resources.RestfulWebServicePortNumber; }
                else if (YourIndex == 2)
                { return Properties.Resources.RestfulWebServiceProtocol + "://" + Properties.Resources.RestfulWebServiceURLSecond + ":" + Properties.Resources.RestfulWebServicePortNumber; }
                else
                { throw new Exception("اندیس ارسالی برای اختصاص آی پی سرور نادرست است"); }
            }

            public static Int64 GetCurrentSoftwareUserId()
            {
                try
                {
                    String TargetPath = System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                    TargetPath = Path.Combine(TargetPath, "AMUStatus.txt");
                    if (System.IO.File.Exists(TargetPath) == false)
                    { throw new AMUStatusFileNotFoundException(null); }
                    else
                    { return Convert.ToInt64(System.IO.File.ReadAllText(TargetPath).Split(';')[1]); }
                }
                catch (AMUStatusFileNotFoundException ex)
                { throw ex; }
                catch (Exception ex)
                { throw ex; }
            }

            public static String GetAMUStatus()
            {
                try
                {
                    String TargetPath = System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                    TargetPath = Path.Combine(TargetPath, "AMUStatus.txt");
                    if (System.IO.File.Exists(TargetPath) == false)
                    { throw new AMUStatusFileNotFoundException(null); }
                    else
                    { return System.IO.File.ReadAllText(TargetPath).Split(';')[0]; }
                }
                catch (AMUStatusFileNotFoundException ex)
                { throw ex; }
                catch (Exception ex)
                { throw ex; }
            }


        }

    }

    namespace Updating
    {
        public class ATISMobileMClassUpdating
        {
            private async void UpdateAPP()
            {
                try
                {
                    //    HttpClient _Client = new HttpClient();
                    //    Xamarin.Essentials.VersionTracking.Track();
                    //    string VersionNumber = Xamarin.Essentials.VersionTracking.CurrentVersion;
                    //    string VersionName = Xamarin.Essentials.VersionTracking.CurrentBuild;

                    //    var responseVersion = await _Client.GetAsync(ATISMobileMClassPublicProcedures.ATISHostURL() + "/api/VersionControl?YourVersionNumber=" + VersionNumber + "&YourVersionName=" + VersionName);
                    //    if (responseVersion.IsSuccessStatusCode)
                    //    {
                    //        var content = await responseVersion.Content.ReadAsStringAsync();
                    //        if (!JsonConvert.DeserializeObject<bool>(content)) return;
                    //    }
                    //    bool answer = await DisplayAlert("بروز رسانی آتیس موبایل", "آتیس موبایل اخیرا تغییراتی داشته است", "OK");

                    //    ATISMobile.Droid.PublicProcedures.ATISMobileMClassPublicProcedures.ViewMessage(this, "آپدیت اپلیکیشن - لطفا تا پایان آپدیت ورژن جدید اپلیکیشن منتظر بمانید");

                    //    String RemoteFtpPath = ATISMobile.Properties.Resources.APKFtpURL;
                    //    //String TargetPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "ATISMobile.apk");
                    //    String TargetPath = Path.Combine(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads).ToString(), "ATISMobile.apk");
                    //    //String TargetPath = System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                    //    FtpWebRequest request = (FtpWebRequest)WebRequest.Create(RemoteFtpPath);
                    //    request.Method = WebRequestMethods.Ftp.DownloadFile;
                    //    request.KeepAlive = false; request.UsePassive = true; request.UseBinary = true;
                    //    request.Credentials = new NetworkCredential(string.Empty, string.Empty);
                    //    FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                    //    Stream responseStream = response.GetResponseStream();
                    //    using (FileStream writer = new FileStream(TargetPath, FileMode.Create))
                    //    {
                    //        int bufferSize = 2048;
                    //        int readCount;
                    //        byte[] buffer = new byte[2048];

                    //        readCount = responseStream.Read(buffer, 0, bufferSize);
                    //        while (readCount > 0)
                    //        {
                    //            writer.Write(buffer, 0, readCount);
                    //            readCount = responseStream.Read(buffer, 0, bufferSize);
                    //        }
                    //    }
                    //    response.Close();
                    //    responseStream.Close();

                    //    //var destination = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments ), "ATISMobile.apk");
                    //    var destination = Path.Combine(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads).ToString(), "ATISMobile.apk");
                    //    Intent install = new Intent(Intent.ActionInstallPackage);
                    //    install.SetDataAndType(Android.Net.Uri.FromFile(new Java.IO.File(destination)), "application/vnd.android.package-archive");
                    //    install.SetFlags(ActivityFlags.ClearTask | ActivityFlags.NewTask);
                    //    install.AddFlags(ActivityFlags.GrantReadUriPermission);
                    //    this.StartActivity(install);

                    //    //var activity = (Activity)this;
                    //    //activity.FinishAffinity();

                }
                catch (Exception ex)
                { await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("خطا در فرآیند آپدیت اپلیکیشن", ex.Message, "تایید"); }

            }

        }


    }

    namespace Exceptions
    {
        public class AMUStatusFileNotFoundException : Exception
        {
            public AMUStatusFileNotFoundException(string message) : base(message)
            {
                message = "خطای اساسی - مجددا تلاش نمایید";
            }
        }
    }

}
