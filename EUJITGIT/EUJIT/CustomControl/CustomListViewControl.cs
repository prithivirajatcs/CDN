using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EUJIT.CustomControl
{
    public class CustomListViewControl : ListView
    {
        #region "Property"
        private Color myBackGroundColor;
        public Color MyBackgroundColor
        {
            get { return this.myBackGroundColor; }
            set { this.myBackGroundColor = value; }
        }
        #endregion
    }
}
