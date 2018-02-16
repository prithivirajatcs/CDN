using System;
using System.Collections.Generic;
using EUJIT.CustomControl;
using EUJIT.Interface;
using EUJIT.Models;
using EUJIT.Services;
using EUJIT.ViewModel;
using Xamarin.Forms;

namespace EUJIT.Views
{
    public partial class HomePage : BaseContentPage
    {

        #region "Properties"
        private HomeViewModel vm;
        private object navParam;
        ISecureStorage secureStoragetInstance = DependencyService.Get<ISecureStorage>();
        #endregion
        public HomePage()
        {
            InitPage();

        }
        public HomePage(object args)
        {
            iApproveNavBar.MyNavigationBar.InitHeader("EU JIT Good Practices", isMainPage: true,
                                                      isNotificationCount: true,
                                                      isClearButtonVisible: false,
                                                      isRefreshIconVisible: true);

            iApproveNavBar.MyNavigationBar.HamburgerCommand = new Command((param) =>
            {

            });
            this.navParam = args;
            InitPage();
        }


        private void InitPage()
        {
            InitializeComponent();

            vm = new HomeViewModel(App.NavigationServiceInstance, this);

            this.BindingContext = vm;

            PrepareImagePanel();
        }

        public void PrepareImagePanel()
        {

        }
    }


}
