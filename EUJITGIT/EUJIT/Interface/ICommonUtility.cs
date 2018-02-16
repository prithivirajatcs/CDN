using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EUJIT.Enum;

namespace EUJIT.Interface
{
    public interface ICommonUtility
    {
        string GetApplicationPath();
        string GetLibraryPath();
        string GetDeviceID(); //http://www.markarteaga.com/quick-tip-getting-device-id-on-windows-windows-phone/
        //DeviceOrientations GetOrientation(); //https://developer.xamarin.com/guides/xamarin-forms/dependency-service/device-orientation/#Android_Implementation
        bool PlayVideoInOtherApp<T>(string pathVideo, T context);
        //bool SendEmail(string[] sendTo, string[] sendCC, string subject, string content, object context, ICallback callbac);
        bool SendSMS(string sendTo, string msg, bool isDefaultApp, object context);
        void Vibrate(int milliseconds = 500);
        void OpenNetworkSettings();
        void OpenLocationSettings();
        bool IsNetworkAvailable();
        bool IsGPSEnabled();
         bool IsFirstTimeLaunch();
         bool SaveFirstTimeLaunch(bool isFirstTimeLaunch);
        bool DialPhone(string phoneNo);
        bool CreateNewDirectory(string dirPath);
        bool DeleteNewDirectory(string dirPath);
        Byte[] CompressImage(Byte[] bytes);
    }
}
