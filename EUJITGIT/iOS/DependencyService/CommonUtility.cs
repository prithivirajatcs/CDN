using System;

using Foundation;
using UIKit;
using Xamarin.Forms;
using EUJIT.Interface;
using EUJIT.iOS.DependencyService;
using System.IO;
using AudioToolbox;
using CoreGraphics;

[assembly: Dependency(typeof(CommonUtility))]
namespace EUJIT.iOS.DependencyService
{
    public class CommonUtility : ICommonUtility
    {
        public string GetApplicationPath()
        {
            return System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        }

        public string GetLibraryPath()
        {
            return Path.Combine(GetApplicationPath(), "..", "Library");
        }

        public string GetDeviceID()
        {
            //TODO: To be implemented
            return UIKit.UIDevice.CurrentDevice.IdentifierForVendor.AsString(); ;
        }

        /// <summary>
        /// Used to get device Orientation
        /// </summary>
        /// <returns>The orientation.</returns>
        //public DeviceOrientations GetOrientation()
        //{
        //    var currentOrientation = UIApplication.SharedApplication.StatusBarOrientation;
        //    bool isPortrait = currentOrientation == UIInterfaceOrientation.Portrait
        //        || currentOrientation == UIInterfaceOrientation.PortraitUpsideDown;

        //    return isPortrait ? DeviceOrientations.Portrait : DeviceOrientations.Landscape;
        //}

        public bool PlayVideoInOtherApp<T>(string pathVideo, T appContext)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Used to send email from ios by launching Email composer
        /// </summary>
        /// <returns><c>true</c>, if email was sent, <c>false</c> otherwise.</returns>
        /// <param name="sendTo">Send to.</param>
        /// <param name="sendCC">Send cc.</param>
        /// <param name="subject">Subject.</param>
        /// <param name="content">Content.</param>
        //public bool SendEmail(string[] sendTo, string[] sendCC, string subject, string content,
        //                      object context, ICallback callback)
        //{
        //    MFMailComposeViewController mailController;
        //    if (MFMailComposeViewController.CanSendMail)
        //    {
        //        mailController = new MFMailComposeViewController();

        //        mailController.SetToRecipients(sendTo);
        //        mailController.SetSubject(subject);
        //        mailController.SetMessageBody(content, false);
        //        mailController.Finished += (object s, MFComposeResultEventArgs args) =>
        //        {
        //            Console.WriteLine(args.Result.ToString());
        //            if (callback != null)
        //            {
        //                callback.OnSuccess(new ProcessStatus()
        //                {
        //                    StatusMessage = args.Result.ToString(),
        //                    StatusCode = (int)ProcessStatus.StatusCodeEnum.Success
        //                });
        //            }
        //            args.Controller.DismissViewController(true, null);
        //        };
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }

        //}

        /// <summary>
        /// Used to send SMS
        /// </summary>
        /// <returns><c>true</c>, if sms was sent, <c>false</c> otherwise.</returns>
        /// <param name="sendTo">Send to.</param>
        /// <param name="msg">Message.</param>
        public bool SendSMS(string sendTo, string msg, bool isDefaultApp, object context)
        {
            var smsTo = NSUrl.FromString("sms:" + sendTo);
            if (UIApplication.SharedApplication.CanOpenUrl(smsTo))
            {
                UIApplication.SharedApplication.OpenUrl(smsTo);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Used vibrate the device for given period of time
        /// </summary>
        /// <param name="milliseconds">Milliseconds to play viration</param>
        public void Vibrate(int milliseconds = 500)
        {
            //SystemSound.Vibrate.PlaySystemSound();
            SystemSound systemSound;
            systemSound = new SystemSound(1520); //1520 SoundID gives low vibaration 
            systemSound.PlaySystemSound();
        }

        public void OpenNetworkSettings()
        {
            var url = new NSUrl("App-Prefs:root=Settings");
            UIApplication.SharedApplication.OpenUrl(url);
        }
        public void OpenLocationSettings()
        {
            var url = new NSUrl("App-Prefs:root=Privacy&path=LOCATION");
            UIApplication.SharedApplication.OpenUrl(url);
        }

        public bool IsNetworkAvailable()
        {
            return true;
        }
        public bool IsGPSEnabled()
        {
            return true;
        }

        public bool IsFirstTimeLaunch()
        {
            ISecureStorage storage = new SecureStorage();
            var val = storage.RetrieveString("IsFirstTimeLaunch");
            if (String.IsNullOrEmpty(val))
            {
                return true;
            }
            return Boolean.Parse(val);
        }

        public bool SaveFirstTimeLaunch(bool isFirstTimeLaunch)
        {
            ISecureStorage storage = new SecureStorage();
            storage.StoreString("IsFirstTimeLaunch", isFirstTimeLaunch.ToString());
            return true;
        }

        public bool DialPhone(string phoneNo)
        {
            Device.OpenUri(new Uri(string.Format("tel:{0}", phoneNo)));
            return true;
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
            // AppDelegate.showBadgecount(count);
        }

        public Byte[] CompressImage(Byte[] bytes)
        {
            try
            {
                float mb = 0.0f;
                NSData nsData;
                do
                {
                    UIImage image;


                    using (var data = NSData.FromArray(bytes))
                    {
                        image = UIImage.LoadFromData(data);
                    }

                    nsData = image.AsJPEG(0.1f);

                    mb = (float)((float)nsData.Length / 1024) / 1024;

                } while (mb > 2.0f);

                //UIImage orgImage, scaledImage;

                //orgImage = UIImage.LoadFromData(nsData);

                //CGSize size = new CGSize(750, 750);

                //scaledImage = UIImageHelper.ScaledImage(orgImage, size);

                //Byte[] myByteArray;

                //using (NSData imageData = scaledImage.AsPNG())
                //{
                //    myByteArray = new Byte[imageData.Length];
                //    System.Runtime.InteropServices.Marshal.Copy(imageData.Bytes, myByteArray, 0, Convert.ToInt32(imageData.Length));
                //}

                Byte[] myByteArray = new Byte[nsData.Length];

                System.Runtime.InteropServices.Marshal.Copy(nsData.Bytes, myByteArray, 0, Convert.ToInt32(nsData.Length));

                return myByteArray;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
    public static class UIImageHelper
    {
        public static UIImage ScaledImage(this UIImage self, CGSize newSize)
        {
            CGRect scaledImageRect = CGRect.Empty;

            double aspectWidth = newSize.Width / self.Size.Width;
            double aspectHeight = newSize.Height / self.Size.Height;
            double aspectRatio = Math.Min(aspectWidth, aspectHeight);

            scaledImageRect.Size = new CGSize(self.Size.Width * aspectRatio, self.Size.Height * aspectRatio);
            scaledImageRect.X = (newSize.Width - scaledImageRect.Size.Width) / 2.0f;
            scaledImageRect.Y = (newSize.Height - scaledImageRect.Size.Height) / 2.0f;

            UIGraphics.BeginImageContextWithOptions(newSize, false, 0);
            self.Draw(scaledImageRect);

            UIImage scaledImage = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();
            return scaledImage;
        }
    }
}