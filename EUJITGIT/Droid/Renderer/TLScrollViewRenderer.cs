using System;
using EUJIT.CustomControl;
using EUJIT.Droid.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(TLScrollView), typeof(TLScrollViewRenderer))]
namespace EUJIT.Droid.Renderer
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
