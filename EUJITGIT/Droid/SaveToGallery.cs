using System;
using EUJIT.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(SaveToGallery))]
namespace EUJIT.Droid
{
    public class SaveToGallery : ISaveToGallery
    {
        public SaveToGallery()
        {
        }

        public void sendPhoto(byte[] photo)
        {
            throw new NotImplementedException();
        }
    }
}
