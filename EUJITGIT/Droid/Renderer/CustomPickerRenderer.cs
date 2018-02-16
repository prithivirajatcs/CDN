using System;
using Android.Util;
using EUJIT.CustomControl;
using EUJIT.Droid.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomPicker), typeof(CustomPickerRenderer))]
namespace EUJIT.Droid.Renderer
{
    public class CustomPickerRenderer : PickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Picker> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.Hint = e.NewElement.Title;
               // Control.SetHeight(30);
                Control.SetHintTextColor(Color.FromHex("#939393").ToAndroid());
                //Control.AttributedPlaceholder = new Foundation.NSAttributedString(this.Control.AttributedPlaceholder.Value, foregroundColor: UIColor.FromRGB(147, 147, 147));
                //nativePickerView.Hint = Control.Hint;
                Control.SetBackgroundResource(Resource.Drawable.edittext_normal);
                Control.SetTextColor(Color.FromHex("#ffffff").ToAndroid());
                Control.SetTextSize(ComplexUnitType.Dip, 12);




               // Control.BorderStyle = UITextBorderStyle.None;

            }
        }
    }
}
