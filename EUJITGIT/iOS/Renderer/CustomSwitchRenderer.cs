using System;
using EUJIT.CustomControl;
using EUJIT.iOS.Renderer;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomSwitch), typeof(CustomSwitchRenderer))]
namespace EUJIT.iOS.Renderer
{
    public class CustomSwitchRenderer : SwitchRenderer
    {
        public CustomSwitchRenderer()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Switch> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                // do whatever you want to the UISwitch here!
                Control.OnTintColor = UIColor.FromRGB(174, 205, 38);
            }
        }
    }
}
