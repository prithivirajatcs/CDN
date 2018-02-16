using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EUJIT.LangResource
{
    [ContentProperty("Text")]
    public class TranslateExtension : IMarkupExtension
    {
        public string Text { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            CultureInfo cu = new CultureInfo("en-US");
            if (Text == null)
                return null;
            return AppData.ResourceManager.GetString(Text, cu);
        }
    }
}
