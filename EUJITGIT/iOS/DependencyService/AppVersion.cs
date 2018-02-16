using System;
using EUJIT.Interface;
using EUJIT.iOS.DependencyService;
using Foundation;

[assembly: Xamarin.Forms.Dependency(typeof(AppVersion))]
namespace EUJIT.iOS.DependencyService
{
    public class AppVersion : IAppVersion
    {
        public AppVersion()
        {
        }

        public string Version
        {

            get
            {
                NSObject ver = NSBundle.MainBundle.InfoDictionary["CFBundleShortVersionString"];
                return ver.ToString();
            }
        }
    }
}


