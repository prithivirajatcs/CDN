using Android.Graphics;
using Android.Widget;
using EUJIT.CustomControl;
using EUJIT.Droid.DependencyService;
using EUJIT.Droid.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomSwitch), typeof(CustomSwitchRenderer))]
namespace EUJIT.Droid.Renderer
{
    public class CustomSwitchRenderer : SwitchRenderer
    {

        // private Color greyColor = new Color(215, 218, 220);
        //private Color greenColor = new Color(32, 156, 68);

        protected override void Dispose(bool disposing)
        {
            this.Control.CheckedChange -= this.OnCheckedChange;
            base.Dispose(disposing);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Switch> e)
        {
            base.OnElementChanged(e);

            if (this.Control != null)
            {


                if (this.Control.Checked)
                {
                    this.Control.ThumbDrawable.SetColorFilter(Android.Graphics.Color.Rgb(174, 205, 38), PorterDuff.Mode.SrcAtop);
                }
                else
                {
                    this.Control.ThumbDrawable.SetColorFilter(Android.Graphics.Color.Rgb(215, 218, 220), PorterDuff.Mode.SrcAtop);
                }

                this.Control.CheckedChange += this.OnCheckedChange;
            }
        }

        private void OnCheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            if (this.Control.Checked)
            {
                this.Control.ThumbDrawable.SetColorFilter(Android.Graphics.Color.Rgb(174, 205, 38), PorterDuff.Mode.SrcAtop);
                App.storage.StoreBool("RememeberMe", true);
            }
            else
            {
                this.Control.ThumbDrawable.SetColorFilter(Android.Graphics.Color.Rgb(215, 218, 220), PorterDuff.Mode.SrcAtop);
                App.storage.StoreBool("RememeberMe", false);
            }
        }
    }
}

