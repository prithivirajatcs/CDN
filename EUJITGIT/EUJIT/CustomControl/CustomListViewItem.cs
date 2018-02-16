using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EUJIT.CustomControl
{
    public class CustomListViewItem : ViewCell
    {
        #region "Members"
        public Boolean IsAckEnabled { get; set; }
        private readonly object syncLock = new object();
        public Boolean IsHighlightSelected { get; set; }
        #endregion

        #region "Constructor"
        public CustomListViewItem()
        {
            this.IsHighlightSelected = true;
        }
        #endregion

        #region "Methods"
        protected override void OnAppearing()
        {
            base.OnAppearing();

            /*ViewCell viewCell2 = (ViewCell)this;
            CustomListViewItem custLv = (CustomListViewItem)this;
            Grid grid1 = (Grid)viewCell2.View;
            if (Device.OS == TargetPlatform.iOS)
            {
                int count1 = grid1.GestureRecognizers.Count;
                for (int i = 0; i < count1; i++)
                {
                    grid1.GestureRecognizers.RemoveAt(i);
                }
            }
            else if (Device.OS == TargetPlatform.Android)
            {
                int count2 = custLv.ContextActions.Count;
                for (int j = 0; j < count2; j++)
                {
                    custLv.ContextActions.RemoveAt(j);
                }
            }
                try
            {
                if (Device.OS == TargetPlatform.Android)
                {
                    //Disable redraw
                    ViewCell viewCell = (ViewCell)this;
                    lock (viewCell)
                    {
                        Grid grid = (Grid)viewCell.View;
                        Grid firstColumn = (Grid)grid.Children[0];
                        Grid secondColumn = (Grid)grid.Children[1];

                        Xamarin.Forms.Button btnAck = (Xamarin.Forms.Button)secondColumn.Children[0];
                        if (!custLv.IsAckEnabled && MainPage.selIndex != custLv.Id)
                        {
                            firstColumn.TranslateTo(0.5, 0, 0, Easing.Linear);
                            //firstColumn.TranslationX = 0.5;                       
                        }
                        else if (custLv.IsAckEnabled && MainPage.selIndex== custLv.Id)
                        {
                            firstColumn.TranslateTo(-110, 0, 0, Easing.Linear);
                            //firstColumn.TranslationX = -110;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }*/
        }
        #endregion
    }
}
