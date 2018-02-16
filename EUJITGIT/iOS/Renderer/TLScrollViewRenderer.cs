using System;
using EUJIT.CustomControl;
using EUJIT.iOS.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(TLScrollView), typeof(TLScrollViewRenderer))]
namespace EUJIT.iOS.Renderer
{
    public class TLScrollViewRenderer : ScrollViewRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            var element = e.NewElement as TLScrollView;
            element?.Render();
        }


    }
}
