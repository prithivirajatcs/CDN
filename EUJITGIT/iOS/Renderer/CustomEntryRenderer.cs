using System;
using CoreAnimation;
using EUJIT.CustomControl;
using EUJIT.iOS.Renderer;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace EUJIT.iOS.Renderer
{
    public class CustomEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                var textField = (UITextField)Control;

                textField.BorderStyle = UITextBorderStyle.None;
            }
        }
    }
}