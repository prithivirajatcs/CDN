using System;
using EUJIT.ViewModel;
using Xamarin.Forms;
using EUJIT.Enum;
using EUJIT.Views;

namespace EUJIT.CustomControl
{
    public class BaseContentPage : ContentPage
    {
        public static ControlTemplate controlTemplate;

        private Page page;
        bool IsMenuSet = false;
        public BaseContentPage()
        {
            BackgroundColor = Color.White;
            var indicator = new ActivityIndicator
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Color = Color.Black,
                IsVisible = false
            };
            NavigationPage.SetHasNavigationBar(this, false);

            this.ControlTemplate = new ControlTemplate(typeof(iApproveNavBar));
        }

        public void SetChildPage(Page childPage)
        {
            this.page = childPage;
        }
        private object sourceData = "";
        public void SetSourceData<T>(T viewModel) where T : new()
        {
            this.sourceData = (T)(object)viewModel;
        }
        public T GetSourceData<T>() where T : new()
        {
            return (T)(object)this.sourceData;
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (Device.OS == TargetPlatform.Android)
            {
                /*Device.BeginInvokeOnMainThread(() =>
                {
                    this.Animate("anim", (s) => Layout(new Rectangle(((1 - s) * Width),
                                                          AnchorY, Width, Height)),
                                 16, 300, Easing.Linear, null, null);
                });
                      */
            }
        }
        //private Enum.PageName currentPageName;
        //public Enum.PageName CurrentPageName
        //{
        //    get { return currentPageName; }
        //    set { currentPageName = value; }
        //}

        protected override bool OnBackButtonPressed()
        {
            try
            {
                if (App.CustomNavigation.NavigationStack.Count > 0)
                {
                    if (App.CustomNavigation.NavigationStack.Count == 2)
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            var result = await this.DisplayAlert("Logout", "Are you sure you want to logout?", "Yes", "No");
                            if (result)
                            {

                                App.Logout();
                            }
                        });
                    }
                    else
                    {
                        App.NavigationServiceInstance.GoBack();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}
