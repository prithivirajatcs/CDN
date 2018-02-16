using System;
using EUJIT.Droid.DependencyService;
using EUJIT.Interface;
using Xamarin.Forms;

[assembly: Dependency(typeof(AppVersion))]
namespace EUJIT.Droid.DependencyService
{
    public class AppVersion : Java.Lang.Object, IAppVersion
    {

        public string Version
        {
            get
            {
                var context = Forms.Context;
                return (context.PackageManager.GetPackageInfo(context.PackageName, 0).VersionName).ToString();
            }
        }

        public AppVersion()
        {
        }
    }
}



