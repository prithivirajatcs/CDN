using System;
using System.IO;
using EUJIT;
using EUJIT.iOS;
using Foundation;
using Plugin.Media.Abstractions;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(SaveToGallery))]
namespace EUJIT.iOS
{
    public class SaveToGallery : ISaveToGallery
    {
        public SaveToGallery()
        {
        }

        public async void sendPhoto(byte[] photo)
        {
            //var someImage = UIImage.FromFile("someImage.jpg");
            //someImage.SaveToPhotosAlbum((image, error) => {
            //    var o = image as UIImage;
            //    Console.WriteLine("error:" + error);
            //});

            //var someImage = photo; // UIImage.FromFile("someImage.jpg");

            try
            {
                using (var data =  NSData.FromArray(photo))
                {
                    UIImage mage = UIImage.LoadFromData(data);
                    mage.SaveToPhotosAlbum((image, error) => {
                        var o =  image as UIImage;
                        Console.WriteLine("error:" + error);
                    });
                } 
            }
            catch (Exception ex)
            {

            }

            


        }
    }
}
