using System;
using Plugin.Connectivity;
using Xamarin.Forms;
using EUJIT.Interface;
using EUJIT.Services;
using EUJIT.ViewModel;

namespace EUJIT
{
    public partial class App : Application
    {
        public static INavigationService NavigationServiceInstance;
        public static INavigation CustomNavigation;
        public static ICommonUtility MyUtility;
        public static IDataSource MyApplicationDataSource;
        public static bool IsMasterDetailFlow = false;
        public static bool IsTestModeEnabled = false;
        public static ISecureStorage storage;
        public static ContentPage presentContentPage;

        public App()
        {
           InitializeComponent();
            //TODO Comment before build
            IsTestModeEnabled = false;
            //MyApplicationDataSource = new StubDataSource();
            MyApplicationDataSource = new RestDataSource(Constants.Environment);
            MyUtility = DependencyService.Get<ICommonUtility>();
            NavigationServiceInstance = DependencyService.Get<INavigationService>();
            NavigationServiceInstance.CreatePageMap();
            // MainPage = new camerapocPage();

            storage = DependencyService.Get<ISecureStorage>();


            bool isFirstTimeLaunch = MyUtility.IsFirstTimeLaunch();
            if (isFirstTimeLaunch)
            {
                MyUtility.SaveFirstTimeLaunch(false);
                //DependencyService.Get<ICommonUtility>().showBadgeCount(0);
            }

            //TODO: Enable this for RT POC
            if (IsMasterDetailFlow)
            {
                // MainPage = new MasterMainPage();
            }
            else
            {
                NavigationServiceInstance.NavigateTo(Enum.PageName.LOGIN, "", true);
            }

        }


        protected override void OnStart()
        {
            // Handle when your app starts
            NetworkCheck();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        public static void NetworkCheck()
        {
            try
            {
                if (!CrossConnectivity.Current.IsConnected)
                {
                    // updated error message
                    Application.Current.MainPage.DisplayAlert("", "Please check your internet connection", "OK");
                    //App.NavigationServiceInstance.GoBack();

                }

                CrossConnectivity.Current.ConnectivityChanged += (sender, e) =>
                {

                    if (!e.IsConnected)
                    {
                        // updated error message
                        Application.Current.MainPage.DisplayAlert("", "Please check your internet connection", "OK");
                    // App.NavigationServiceInstance.GoBack();
                }
                };
            }
            catch (Exception ex)
            {

            }

        }

        public static void Logout()
        {
            if (storage != null && storage.Contains("SMCookie"))
            {
               storage.Delete("SMCookie");
            }
            var navStack = App.CustomNavigation.NavigationStack;
            var bindingContext = navStack[navStack.Count - 2].BindingContext;

           // if (bindingContext is LoginViewModel)
            //{
            if (storage != null)
                {
                bool tempRememberMe = storage.RetriveBool("RememeberMe");
                    if (tempRememberMe)
                    {

                    if (storage.Contains("UserName") && storage.Contains("Password"))
                        {
                        (bindingContext as LoginViewModel).UserName = storage.RetrieveString("UserName");
                        (bindingContext as LoginViewModel).Password = storage.RetrieveString("Password");
                            //(bindingContext as LoginViewModel).RememberMe = true;
                            //(bindingContext as LoginViewModel).RememberMe = "RememberMeSelected.png";
                        }

                    }
                    else
                    {
                        (bindingContext as LoginViewModel).UserName = string.Empty;
                        (bindingContext as LoginViewModel).Password = string.Empty;
                       // (bindingContext as LoginViewModel).RememberMe = false;
                        //(bindingContext as LoginViewModel).ToggleImageSource = "RememberMe.png";
                    }
                }
                else
                {
                    (bindingContext as LoginViewModel).UserName = string.Empty;
                    (bindingContext as LoginViewModel).Password = string.Empty;
                    //(bindingContext as LoginViewModel).RememberMe = false;
                    //(bindingContext as LoginViewModel).ToggleImageSource = "RememberMe.png";
                }
         //   }
            //DependencyService.Get<ICommonUtility>().showBadgeCount(0);
            //UtilService.BadgeCount = 0;
            // b ApplicationModel.Current.SetSession(null);
            App.NavigationServiceInstance.GoBack();
        }
    }
}
