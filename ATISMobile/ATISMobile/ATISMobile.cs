using System;
using System.Text;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.IO;
using System.Security.Cryptography;
using System.Globalization;

using ATISMobile.Exceptions;
using System.Net;
using System.Net.Http.Headers;
using ATISMobile.PublicProcedures;
using System.Threading;

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
        public class ATISMobilePredefinedMessages
        {
            public static readonly string ATISWebApiNotReachedMessage = "ارتباط با سرور آتیس برقرار نیست";
        }

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

            public static string GetPersianDate(DateTime YourDateTime)
            {
                try
                {
                    PersianCalendar PersianCalendar1 = new PersianCalendar();
                    return string.Format(@"{0}/{1}/{2}", PersianCalendar1.GetYear(YourDateTime), PersianCalendar1.GetMonth(YourDateTime), PersianCalendar1.GetDayOfMonth(YourDateTime));
                }
                catch (Exception ex)
                { throw ex; }
            }


        }

        public class ATISMobileWebApiMClassManagement
        {
            private static string _Last5Digit = String.Empty;
            public static string UserLast5Digit
            { get { return _Last5Digit; } set { if (value == string.Empty) { _Last5Digit = string.Empty; } else { _Last5Digit = GetMD5Hashe(value); } } }

            public static string GetATISMobileWebApiHostUrlFirstWithoutPortNumber()
            { return Properties.Resources.RestfulWebServiceURLFirst; }

            public static string GetATISMobileWebApiHostUrlSecondWithoutPortNumber()
            { return Properties.Resources.RestfulWebServiceURLSecond; }

            private static string GetATISMobileWebApiHostUrlFirst()
            { return Properties.Resources.RestfulWebServiceProtocol + "://" + Properties.Resources.RestfulWebServiceURLFirst + ":" + Properties.Resources.RestfulWebServicePortNumber; }
            private static string GetATISMobileWebApiHostUrlSecond()
            { return Properties.Resources.RestfulWebServiceProtocol + "://" + Properties.Resources.RestfulWebServiceURLSecond + ":" + Properties.Resources.RestfulWebServicePortNumber; }

            private static string _ATISMobileWebApiHostUrl = string.Empty;
            public static string GetATISMobileWebApiHostUrl()
            {
                if (!(_ATISMobileWebApiHostUrl == string.Empty)) { return _ATISMobileWebApiHostUrl; }
                try
                {
                    if (ATISMobileMClassPublicProcedures.IsThisIPAvailable(GetATISMobileWebApiHostUrlFirstWithoutPortNumber()))
                    { _ATISMobileWebApiHostUrl = GetATISMobileWebApiHostUrlFirst(); return _ATISMobileWebApiHostUrl; }
                    else
                    {
                        if (ATISMobileMClassPublicProcedures.IsThisIPAvailable(GetATISMobileWebApiHostUrlSecondWithoutPortNumber()))
                        { _ATISMobileWebApiHostUrl = GetATISMobileWebApiHostUrlSecond(); return _ATISMobileWebApiHostUrl; }
                        else
                        { throw new Exception(); }
                    }
                }
                catch (Exception ex)
                { throw new Exception(ATISMobilePredefinedMessages.ATISWebApiNotReachedMessage); }
            }

            private static string GetAPPId()
            { return "0D992C8C-3F8A-428A-8638-25B94D04BEA7"; }

            public static string GetAuthCode2PartHashed()
            { return GetMD5Hashe(GetAPPId() + ":" + DateTime.Now.Day); }

            public static string GetAuthCode3PartHashed()
            { try { return GetMD5Hashe(GetAPPId() + ":" + DateTime.Now.Day + ":" + GetMD5Hashe(GetCurrentSoftwareUserId().ToString())); } catch (Exception ex) { throw ex; } }

            public static string GetAuthCode4PartHashed()
            { try { return GetMD5Hashe(GetAPPId() + ":" + DateTime.Now.Day + ":" + GetMD5Hashe(GetCurrentSoftwareUserId().ToString()) + ":" + UserLast5Digit); } catch (Exception ex) { throw ex; } }

            public static string GetMD5Hashe(string input)
            {
                StringBuilder hash = new StringBuilder();
                MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
                byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));
                for (int i = 0; i < bytes.Length; i++)
                { hash.Append(bytes[i].ToString("x2")); }
                return hash.ToString();
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

            public static string GetApiKey()
            {
                try
                { return ATISMobileWebApiMClassManagement.GetMD5Hashe(GetCurrentSoftwareUserId().ToString()); }
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

            public static String GetRegisteredMobileNumberIntoWebApi()
            {
                try
                {
                    String TargetPath = System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                    TargetPath = Path.Combine(TargetPath, "AMUStatus.txt");
                    if (System.IO.File.Exists(TargetPath) == false)
                    { throw new AMUStatusFileNotFoundException(null); }
                    else
                    { return System.IO.File.ReadAllText(TargetPath).Split(';')[2]; }
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
            public async void UpdateAPP()
            {
                //try
                //{
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

                //}
                //catch (Exception ex)
                //{ await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("خطا در فرآیند آپدیت اپلیکیشن", ex.Message, "تایید"); }

            }

        }


    }

    namespace SecurityAlgorithmsManagement
    {
        namespace Hashing
        {
            public class Hashing
            {
                public string GetSHA256StringHash(String input)
                {
                    SHA256 shaM = new SHA256Managed();
                    byte[] data = shaM.ComputeHash(Encoding.UTF8.GetBytes(input));
                    StringBuilder sBuilder = new StringBuilder();
                    for (int i = 0; i < data.Length; i++)
                    { sBuilder.Append(data[i].ToString("x2")); }
                    input = sBuilder.ToString();
                    return (input);
                }
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

    namespace HttpClientInstance
    {
        public class HttpClientOnlyInstance
        {
            private static HttpClient _HttpClient = null;
            public static HttpClient HttpClientInstance()
            {
                try
                {
                    if (_HttpClient is null)
                    {
                        Uri baseUri = new Uri(ATISMobileWebApiMClassManagement.GetATISMobileWebApiHostUrl());
                        _HttpClient = new HttpClient();
                        _HttpClient.BaseAddress = baseUri;
                        _HttpClient.DefaultRequestHeaders.Clear();
                        _HttpClient.DefaultRequestHeaders.ConnectionClose = false;
                        _HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        try { ServicePointManager.FindServicePoint(baseUri).ConnectionLeaseTimeout = 60 * 1000; }
                        catch (Exception ex)
                        {; }
                        ServicePointManager.DnsRefreshTimeout = 100;
                        return _HttpClient;
                    }
                    else
                    { return _HttpClient; }
                }
                catch (Exception ex)
                { throw ex; }
            }
        }
    }
}
