using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Net;
using System.IO;

using Xamarin.Essentials;

using ATISMobile.Exceptions;

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
            public static Int64  GetCurrentMobileUserId()
            {
                try
                {
                    String TargetPath = System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                    TargetPath = Path.Combine(TargetPath, "AMUStatus.txt");
                    if (System.IO.File.Exists(TargetPath) == false)
                    { throw new AMUStatusFileNotFoundException(null);  }
                    else
                    { return Convert.ToInt64( System.IO.File.ReadAllText(TargetPath).Split(';')[1]); }
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
            public async void UpdateAPP()
            {
            }

        }


    }

    namespace Exceptions
    {
        public class AMUStatusFileNotFoundException : Exception
        {
            public  AMUStatusFileNotFoundException(string message) : base(message)
            {
                message = "خطای اساسی - مجددا تلاش نمایید";
            }
        }
    }



}
