using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;

namespace EUJIT.Droid
{
    [Activity(MainLauncher = true, NoHistory = true, Theme = "@style/Theme.Splash", ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreen : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            var intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
            Finish();//Finish Activity

        }
    }
}