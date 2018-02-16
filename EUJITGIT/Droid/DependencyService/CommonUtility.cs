using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using EUJIT.Interface;
using Xamarin.Forms;
using EUJIT.Droid.DependencyService;
using Android.Telephony;
using Java.IO;
using Android.Net;
using Android.Locations;
using Android.Preferences;
using Android.Graphics;
using System.IO;
using System.Runtime.InteropServices;

[assembly: Dependency(typeof(CommonUtility))]
namespace EUJIT.Droid.DependencyService
{
    public class CommonUtility : ICommonUtility
    {
        public string GetApplicationPath()
        {
            return System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        }

        public string GetLibraryPath()
        {
            return GetApplicationPath();
        }

        public string GetDeviceID()
        {
            return Android.OS.Build.Serial;
        }

        /// <summary>
        /// Used to get Device Orientation
        /// </summary>
        /// <returns>The orientation.</returns>
        //public DeviceOrientations GetOrientation()
        //{
        //    IWindowManager windowManager = Android.App.Application.Context.GetSystemService
        //                                          (Context.WindowService).JavaCast<IWindowManager>();

        //    var rotation = windowManager.DefaultDisplay.Rotation;
        //    bool isLandscape = rotation == SurfaceOrientation.Rotation90 || rotation == SurfaceOrientation.Rotation270;
        //    return isLandscape ? DeviceOrientations.Landscape : DeviceOrientations.Portrait;
        //}

        public bool PlayVideoInOtherApp<T>(string pathVideo, T appContext)
        {
            //Validation here
            //if (String.IsNullOrEmpty(pathVideo)) return false;
            //if (null == appContext) return false;
            //Context currContext = appContext as Context;
            //if (null == currContext) return false;

            //Intent objExtInt = new Intent(Intent.ActionView);
            //File file = new File(pathVideo);
            //objExtInt.SetDataAndType(Android.Net.Uri.FromFile(file), "video/*");
            //currContext.StartActivity(objExtInt);

            return true;
        }

        /// <summary>
        /// Used to launch Email composer in the android device and pass the email related parameters
        /// </summary>
        /// <returns><c>true</c>, if email was sent, <c>false</c> otherwise.</returns>
        /// <param name="sendTo">Send to.</param>
        /// <param name="sendCC">Send cc.</param>
        /// <param name="subject">Subject.</param>
        /// <param name="content">Content.</param>
        //public bool SendEmail(string[] sendTo, string[] sendCC, string subject,
        //                      string content, object context, ICallback callback)
        //{
        //    var email = new Intent(Android.Content.Intent.ActionSend);

        //    email.PutExtra(Android.Content.Intent.ExtraEmail, sendTo);

        //    email.PutExtra(Android.Content.Intent.ExtraCc, sendCC);

        //    email.PutExtra(Android.Content.Intent.ExtraSubject, subject);

        //    email.PutExtra(Android.Content.Intent.ExtraText, content);

        //    email.SetType("message/rfc822");

        //    Context mycontxt = (Context)context;

        //    mycontxt.StartActivity(email);

        //    return true;
        //}

        /// <summary>
        /// Used to send SMS 
        /// </summary>
        /// <returns><c>true</c>, if sms was sent, <c>false</c> otherwise.</returns>
        /// <param name="sendTo">Send to.</param>
        /// <param name="msg">Message.</param>
        public bool SendSMS(string sendTo, string msg, bool isDefaultApp, object context)
        {
            if (isDefaultApp)
            {
                var smsUri = Android.Net.Uri.Parse("smsto:" + sendTo);
                var smsIntent = new Intent(Intent.ActionSendto, smsUri);
                smsIntent.PutExtra("sms_body", msg);
                ((Context)context).StartActivity(smsIntent);
            }
            else
            {
                SmsManager.Default.SendTextMessage(sendTo, null, msg, null, null);
            }
            return true;

        }

        /// <summary>
        /// Used vibrate the device for given period of time
        /// </summary>
        /// <param name="milliseconds">Milliseconds to play viration</param>
        public void Vibrate(int milliseconds = 500)
        {
            using (var v = (Vibrator)Android.App.Application.Context.GetSystemService(Context.VibratorService))
            {
                if (!v.HasVibrator)
                {
                    //"Android device does not have vibrator."
                    return;
                }

                if (milliseconds < 0)
                    milliseconds = 0;

                try
                {
                    v.Vibrate(milliseconds);
                }
                catch (Exception ex)
                {
                    //"Unable to vibrate Android device, ensure VIBRATE permission is se"
                }
            }
        }

        public void OpenNetworkSettings()
        {
            MainActivity.CurrentActivity.StartActivityForResult(new Intent(Android.Provider.Settings.ActionSettings), 201);
        }
        public void OpenLocationSettings()
        {
            MainActivity.CurrentActivity.StartActivityForResult(new Intent(Android.Provider.Settings.ActionLocationSourceSettings), 202);
        }
        public bool IsNetworkAvailable()
        {
            ConnectivityManager connectivityManager =
                (ConnectivityManager)MainActivity.CurrentActivity.GetSystemService(Context.ConnectivityService);
            NetworkInfo info = connectivityManager.ActiveNetworkInfo;
            if (info == null) return false;
            return info.IsConnected;
        }
        public bool IsGPSEnabled()
        {
            LocationManager locationMgr = (LocationManager)MainActivity.CurrentActivity.GetSystemService(Context.LocationService);
            if (locationMgr != null)
            {
                bool isEnabled = locationMgr.IsProviderEnabled(LocationManager.NetworkProvider) || locationMgr.IsProviderEnabled(LocationManager.GpsProvider);
                return isEnabled;
            }
            return true;
        }

        public bool IsFirstTimeLaunch()
        {
            var pref = PreferenceManager.GetDefaultSharedPreferences((Context)MainActivity.CurrentActivity);
            return pref.GetBoolean("IsFirstTimeLaunch", true);
        }
        public bool SaveFirstTimeLaunch(bool isFirstTimeLaunch)
        {
            var pref = PreferenceManager.GetDefaultSharedPreferences((Context)MainActivity.CurrentActivity);
            var editor = pref.Edit();
            return editor.PutBoolean("IsFirstTimeLaunch", isFirstTimeLaunch).Commit();
        }

        public bool DialPhone(string phoneNo)
        {
            var context = (Context)MainActivity.CurrentActivity;
            var intent = new Intent(Intent.ActionCall);
            intent.SetData(Android.Net.Uri.Parse("tel:" + phoneNo));
            if (IsIntentAvailable(context, intent))
            {
                context.StartActivity(intent);
                return true;
            }
            return false;
        }

        public bool IsIntentAvailable(Context context, Intent intent)
        {
            var packageManager = context.PackageManager;

            var list = packageManager.QueryIntentServices(intent, 0)
                .Union(packageManager.QueryIntentActivities(intent, 0));
            if (list.Any())
                return true;

            TelephonyManager mgr = TelephonyManager.FromContext(context);
            return mgr.PhoneType != PhoneType.None;
        }
        public bool CreateNewDirectory(string dirPath)
        {
            if (string.IsNullOrEmpty(dirPath))
            {
                return false;
            }
            if (!System.IO.Directory.Exists(dirPath))
            {
                var info = System.IO.Directory.CreateDirectory(dirPath);
                if (info.Exists) return true;
            }
            return false;
        }
        public bool DeleteNewDirectory(string dirPath)
        {
            if (string.IsNullOrEmpty(dirPath))
            {
                return false;
            }
            System.IO.Directory.Delete(dirPath, true);
            return false;
        }

        public void showBadgeCount(int count)
        {

        }

        public byte[] CompressImage(byte[] bytes)
        {
            var ghj = bytes.Length;
            var eghg=      (bytes.Length / 1024f) / 1024f;
            var egklhjhg = (bytes.LongLength / 1024f) / 1024f;
            float mb = 0.0f;
            mb = (float)((float)bytes.Length / 1024) / 1024;

            try
            {
                Byte[] compressedBytes;


                //Stream str = new MemoryStream();
                //Bitmap bitmaap = BitmapFactory.DecodeByteArray(bytes, 0, bytes.Length);
                //bitmaap.Compress(Bitmap.CompressFormat.Jpeg, 30, str);
                //compressedBytes = ConverToByteArray(str);           
        
                using (System.IO.Stream stream = new MemoryStream())
                {
                    Bitmap bitmap  = BitmapFactory.DecodeByteArray(bytes, 0, bytes.Length);
                    bitmap.Compress(Bitmap.CompressFormat.Jpeg, 30, stream);                  
              
                    byte[] byteArr = ((MemoryStream)stream).ToArray();

                    compressedBytes = byteArr;

                  
                    //compressedBytes = ConverToByteArray(stream);

                }
                return compressedBytes;
            }
            catch (Exception ex)
            {
                return null;
            }
           
        }

        public static byte[] ConverToByteArray(Stream input)
        {
            try
            {

                //byte[] bytes = new byte[16384];
                //using (MemoryStream memoryStream = new MemoryStream())
                //{
                //    int count;
                //    while ((count = input.Read(bytes, 0, bytes.Length)) > 0)
                //    {
                //        memoryStream.Write(bytes, 0, count);
                //    }
                //    return memoryStream.ToArray();
                //}


                byte[] buffer = new byte[16 * 1024];
                using (MemoryStream ms = new MemoryStream())
                {
                    int read;
                    while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        ms.Write(buffer, 0, read);
                    }
                    return ms.ToArray();
                }


               
                    //using (MemoryStream ms = new MemoryStream())
                    //{
                    //    input.CopyTo(ms);
                    //    return ms.ToArray();
                    //}



            }
            catch (Exception ex)
            {
                return null;
            }
           
        }
    }
}