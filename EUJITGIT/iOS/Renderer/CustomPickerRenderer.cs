using System;
using EUJIT.CustomControl;
using EUJIT.iOS.Renderer;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomPicker), typeof(CustomPickerRenderer))]

namespace EUJIT.iOS.Renderer
{
    public class CustomPickerRenderer : PickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Picker> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                this.Control.AttributedPlaceholder = new Foundation.NSAttributedString(this.Control.AttributedPlaceholder.Value, foregroundColor: UIColor.FromRGB(147, 147, 147));

                
                Control.TextColor = UIColor.White;

                Control.BorderStyle = UITextBorderStyle.None;

                Control.Font = UIFont.FromName("Arial", 14.0f);


            }
        }

    }
}
