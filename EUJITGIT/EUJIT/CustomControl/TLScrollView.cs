using System.Collections;
using Xamarin.Forms;

namespace EUJIT.CustomControl
{
    public class TLScrollView : ScrollView
    {
        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create("ItemsSource", typeof(IEnumerable), typeof(TLScrollView), default(IEnumerable));

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly BindableProperty ItemTemplateProperty =
            BindableProperty.Create("ItemTemplate", typeof(DataTemplate), typeof(TLScrollView), default(DataTemplate));

        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        public void Render()
        {
            if (this.ItemTemplate == null || this.ItemsSource == null)
                return;

            var layout = new StackLayout();
            layout.Orientation = this.Orientation == ScrollOrientation.Vertical
                ? StackOrientation.Vertical : StackOrientation.Horizontal;

            foreach (var item in this.ItemsSource)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    
                        var viewCell = this.ItemTemplate.CreateContent() as ViewCell;
                        viewCell.View.BindingContext = item;
                        layout.Children.Add(viewCell.View);

                });
               
            }

            this.Content = layout;
        }


    }
}
