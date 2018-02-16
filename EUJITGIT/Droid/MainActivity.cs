using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Xamarin.Forms.Platform.Android;
using Plugin.CurrentActivity;

namespace EUJIT.Droid
{
    //[Activity(Label = "camerapoc.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    [Activity(Label = "EUJIT", Icon = "@drawable/appIcon", ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : FormsApplicationActivity
    {
        public static MainActivity CurrentActivity { get; private set; }
        //public static ProgressDialog progressDialog;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            //CrossCurrentActivity.Current.Activity = this;
            CurrentActivity = this;
            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App()); 

            //CrossCurrentActivity.Current.Activity = this;
          
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {


            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
