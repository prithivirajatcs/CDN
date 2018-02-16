using System;
using System.IO;
using System.Windows.Input;
using Xamarin.Forms;

namespace EUJIT.Models
{
    public class ImageDraftModel
    {
        public string pictureName { get; set; }
        public Byte[] pictureByte { get; set; }
        public string Timestamp { get; set; }
        public ICommand RemovePhotoCommand { get; set; }
    }
}
