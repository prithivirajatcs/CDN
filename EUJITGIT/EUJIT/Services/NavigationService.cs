using EUJIT.Interface;
using System;
using System.Collections.Generic;
using EUJIT.Enum;
using Xamarin.Forms;
using EUJIT.Services;
using EUJIT.Views;
using EUJIT.CustomControl;
using System.Diagnostics;
using EUJIT;

[assembly: Dependency(typeof(NavigationService))]
namespace EUJIT.Services
{
    /// <summary>
    /// This class is pecifically done for MAPICS POC
    /// </summary>
    public class NavigationService : INavigationService
    {
        public static Dictionary<PageName, Type> PageMapping = new Dictionary<PageName, Type>();
        private ContentPage appContext;
        public NavigationService()
        {

        }

        public void CreatePageMap()
        {
            #region "Page Mapping Init"
            PageMapping.Clear();

            //iApprove Page Mapping for Navigation
            PageMapping.Add(PageName.LOGIN, typeof(LoginPage));
            PageMapping.Add(PageName.HOME, typeof(HomePage));
            PageMapping.Add(PageName.CREATEDRAFT, typeof(CreateDraft));
            PageMapping.Add(PageName.EDITDRAFT, typeof(EditBestPracticePage));

            #endregion
        }

        public void NavigateTo(PageName page, object param, bool isStartPage = false)
        {
            try
            {
                BaseContentPage screenObj = null;
                if (param != null)
                {
                    try
                    {
                        screenObj = (BaseContentPage)Activator.CreateInstance(PageMapping[page], param);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message.ToString());
                    }
                }
                else
                {
                    screenObj = (BaseContentPage)Activator.CreateInstance(PageMapping[page]);
                }

                if (!isStartPage)
                {
                    if (screenObj != null)
                    {
                        if (App.IsMasterDetailFlow)
                        {
                            //((MasterDetailPage)Application.Current.MainPage).Detail.Navigation.PushAsync(screenObj, true);

                            ((MasterDetailPage)Application.Current.MainPage).Detail = screenObj;
                        }
                        else
                        {
                            Device.BeginInvokeOnMainThread(() => { Application.Current.MainPage.Navigation.PushAsync(screenObj, true); });

                        }
                    }
                }
                else
                {
                    SetStartPage(screenObj);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message.ToString());
            }

        }
        /// <summary>
        /// Used to set the starting page to the application
        /// </summary>
        /// <param name="page">Page.</param>
        public void SetStartPage(Page page)
        {
            //if (page as LoginPage != null)
            //{
            //  Application.Current.MainPage = new CustomNavigationPage(page); //, false);
            //}
            //else
            //{
            //  Application.Current.MainPage = new CustomNavigationPage(page);
            //}
            Application.Current.MainPage = new CustomNavigationPage(page);
            App.CustomNavigation = Application.Current.MainPage.Navigation;
        }

        public T GetPageInstance<T>(PageName page) where T : new()
        {
            return (T)Activator.CreateInstance(PageMapping[page]);
        }

        public void SetAppContext(object context)
        {
            this.appContext = (ContentPage)context;
        }

        public void GotoHomePage()
        {
            Application.Current.MainPage.Navigation.PopToRootAsync(true);
        }

        public void GoBack()
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
