using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EUJIT.Enum;
using Xamarin.Forms;

namespace EUJIT.CustomControl
{
    public class CustomButton : Button
    {
        #region "Global Member"
        public static readonly BindableProperty ButtonTypeNameProperty =
                    BindableProperty.Create("ButtonType", typeof(ButtonTypeName),
                    typeof(CustomButton), ButtonTypeName.POSITIVE);
        #endregion

        #region "Property"
        private ButtonTypeName buttonType = ButtonTypeName.POSITIVE;
        public ButtonTypeName ButtonType
        {
            get
            {
                return (ButtonTypeName)GetValue(ButtonTypeNameProperty);
            }
            set
            {
                SetValue(ButtonTypeNameProperty, value);
            }
        }
        #endregion

        #region "Constructor"
        public CustomButton()
        {

        }
        #endregion

        #region "Functions"
        public static void ValueUpdated(BindableObject bindable, float oldValue, float newValue)
        {

        }
        #endregion
    }

    #region "Enum"
    public enum ButtonTypeName
    {
        POSITIVE = 0,
        NEGATIVE = 1,
        CONFIRM = 2,
        DIAL = 3,
        EMAIL = 4,
        None = 5
    }
    #endregion
}
