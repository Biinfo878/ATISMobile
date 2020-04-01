using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Net.Http;


using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Content.PM;
using Newtonsoft.Json;
using Xamarin.Android;

namespace ATISMobile.Droid
{
    namespace PublicProcedures
    {
        public class ATISMobileMClassPublicProcedures
        {
            public static void ViewMessage(Context YourContext, string YourMessage)
            {
                AlertDialog.Builder builder = new AlertDialog.Builder(YourContext);
                builder.SetTitle("ATISMobile");
                builder.SetMessage(YourMessage);
                builder.SetPositiveButton("OK", (senderAlert, args) => { });
                AlertDialog dialog = builder.Create();
                dialog.Show();
            }

            private void OkAction(object sender, DialogClickEventArgs e)
            { }

            private void CancelAction(object sender, DialogClickEventArgs e)
            { }


        }

    }

}