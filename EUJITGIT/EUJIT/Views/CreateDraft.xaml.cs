using System;
using EUJIT.CustomControl;
using EUJIT.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EUJIT.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateDraft : BaseContentPage 
    {
        




        private CreateDraftViewModel vm;
        private object navParam;

        public CreateDraft()
        {
            InitPage();
        }

        public CreateDraft(object args)
        {
            iApproveNavBar.MyNavigationBar.InitHeader("Draft Best Practice", isMainPage: false,
                                                      isNotificationCount: true,
                                                      isClearButtonVisible: false,
                                                      isRefreshIconVisible: true, isCreateDraftsIconVisible: true,isNoIcons: false);

            iApproveNavBar.MyNavigationBar.HamburgerCommand = new Command((param) =>
            {

            });
            this.navParam = args;
            InitPage();
        }

        private void InitPage()
        {
           

            InitializeComponent();
            Imagevisible.IsVisible = false;
            vm = new CreateDraftViewModel(App.NavigationServiceInstance, this);

            this.BindingContext = vm;          
            //rela.RaiseChild(rer);
        }


        void Handle_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            Entry txtBox = sender as Entry;
            if (txtBox.Text.Length > 100)
            {
                String entryText = txtBox.Text;
                entryText = entryText.Remove(entryText.Length - 1);
                txtBox.Text = entryText;
                Application.Current.MainPage.DisplayAlert(Constants.MSG_HEADER, Constants.MAX_CHAR_MSG,Constants.strOK);
            }
        }

        void Handle_Tapped(object sender, System.EventArgs e)
        {
            
            Image pic = (sender as Grid).Children[1] as Image;
            imageview.Source = pic.Source;
            iApproveNavBar.MyNavigationBar.InitHeader("Draft Image", isMainPage: false, isNoIcons: true,
                                                     isNotificationCount: false,
                                                    isClearButtonVisible: false,
                                                     isRefreshIconVisible: false, isCreateDraftsIconVisible: false);
            Imagevisible.IsVisible = true;
          
           
        }


        void back(object sender, System.EventArgs e)
        {
       
            iApproveNavBar.MyNavigationBar.InitHeader("Draft Best Practice", isMainPage: false,
                                                      isNotificationCount: true,
                                                      isClearButtonVisible: false,
                                                      isRefreshIconVisible: true, isCreateDraftsIconVisible: true, isNoIcons: false);

            Imagevisible.IsVisible = false;
            imageview.Source = null;
        }





        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            //vm.ImageDraftList.Clear();
        }
    }
}
