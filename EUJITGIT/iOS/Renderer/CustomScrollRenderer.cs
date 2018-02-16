using System;
using EUJIT.CustomControl;
using EUJIT.iOS.Renderer;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomScroll), typeof(CustomScrollRenderer))]
namespace EUJIT.iOS.Renderer
{
    public class CustomScrollRenderer : ScrollViewRenderer
    {
        public CustomScrollRenderer()
        {
        }


        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
            this.ShowsVerticalScrollIndicator = false;

        }
    }
}
