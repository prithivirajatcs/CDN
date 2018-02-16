using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using EUJIT.Models;
using EUJIT.Services;
using EUJIT.Views;
using EUJIT.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using EUJIT;
using EUJIT.CustomControl;
using System.Diagnostics;
using EUJIT.Interface;
using System.IO;
using EUJIT.ViewModels;
using Plugin.DeviceInfo;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;

namespace EUJIT.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class iApproveNavBar : ContentView
    {

        ICommonUtility utility = DependencyService.Get<ICommonUtility>();

        bool _isBusy;
        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }
            set
            {
                _isBusy = value;

                OnPropertyChanged("IsBusy");
            }
        }
        void Handle_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            var navStack = App.CustomNavigation.NavigationStack;
            var bindingContext = navStack[navStack.Count - 1].BindingContext;

            //if (bindingContext is PendingRequestsViewModel)
            //{
            //    var pVM = bindingContext as PendingRequestsViewModel;
            //    //pVM.PendingRequestList = new ObservableCollection<PendingRequests>(Helper.Instance.SearchByRequests(e.NewTextValue));
            //}
            //else if (bindingContext is HomeViewModel)
            //{
            //    var hVM = bindingContext as HomeViewModel;
            //    //hVM.PendingRequestList = new ObservableCollection<PendingRequests>(Helper.Instance.SearchByRequests(e.NewTextValue));

            //}

        }

        public static iApproveNavBar MyNavigationBar { get; set; }
        public iApproveNavBar()
        {


            InitializeComponent();

            MessagingCenter.Subscribe<BaseContentPage>(this, "showSearch", (sender) =>
            {
                //if (sender.GetType() == typeof(HomePage))
                //{
                //    menuSearch.IsVisible = true;
                //}
            });

            MessagingCenter.Subscribe<BaseContentPage>(this, "hideSearch", (sender) =>
            {
                //if (sender.GetType() == typeof(HomePage))
                //{
                //    menuSearch.IsVisible = false;
                //}
            });



            // string userName=string.Empty;

            //UserName.Text = "Welcome  " + userName;
            //LoggedIn.Text = "Logged In: " + UtilService.loggedInTime;

            MyNavigationBar = this;
            if (Device.OS == TargetPlatform.iOS)
            {
                MainContent.Padding = new Thickness(0, 20, 0, 0);
                MainContent.HeightRequest = 70;
                HeaderParentGrid.RowDefinitions[0].Height = 70;
            }

        }

        //Events
        //Methods
        public void OnBackButtonPressed(object sender, EventArgs args)
        {
            if (App.CustomNavigation != null)
            {
                if (App.CustomNavigation.ModalStack.Count > 0)
                {
                    App.CustomNavigation.PopModalAsync();
                }
                else
                {
                    var navStack = App.CustomNavigation.NavigationStack;

                    var bindingContext = navStack[navStack.Count - 1].BindingContext;

                    if (bindingContext is CreateDraftViewModel)
                    {
                        var vm = bindingContext as CreateDraftViewModel;
                        vm.ImageDraftList.Clear();
                    }
                    else if (bindingContext is EditBestPracticeViewModel)
                    {
                        var vm = bindingContext as EditBestPracticeViewModel;
                        vm.ImageDraftList.Clear();
                    }
                    App.CustomNavigation.PopAsync();
                }
            }
        }

        void Privacy(object sender, System.EventArgs e)
        {
            Device.OpenUri(new Uri("https://connect.adient.com/privacy"));
        }

        void Onconsent(object sender, System.EventArgs e)
        {
            Device.OpenUri(new Uri("https://connect.adient.com/sites/Privacyprocesses/Pages/Photos.aspx"));
        }


        public void OnHamburgerButtonPressed(object sender, EventArgs args)
        {
            if (!string.IsNullOrEmpty(UtilService.globalId))
            {
                //UserName.Text = UtilService.loggedInGlobalID;
                UserName.Text = UtilService.Instance.RawUserProfile.userProfileName;

                //string Vrsion = ;
                if (Constants.Environment == "QA")
                {
                    Version.Text = "V " + DependencyService.Get<IAppVersion>().Version + " - " + Constants.Environment;
                }
                else
                {
                    Version.Text = "V " + DependencyService.Get<IAppVersion>().Version;
                }
                    
                //DataService ds = new DataService(Constants.Environment);
                //var userName = ds.GetUserNameByGlobalId(UtilService.globalId);
                //if (!string.IsNullOrEmpty(UtilService.LoginUser.ToString().Trim()))
                //{
                //    UserName.Text = "Welcome  " + UtilService.LoginUser;
                //}
                //else
                //{
                //    UserName.Text = "Welcome  " + UtilService.globalId;
                //}
            }
            //Show Hamburger Menu Here
            if (HamburgerCommand != null)
            {
                //HamburgerCommand.Execute(null);
                userProfileSection.IsVisible = !userProfileSection.IsVisible;
                if (userProfileSection.IsVisible)
                {
                    menuHamburger.Source = "Close.png";
                }
                else
                {
                    menuHamburger.Source = "Menubar.png";

                }
            }
        }


        public void OnSearchButtonPressed(object sender, EventArgs args)
        {

            // App.NavigationServiceInstance.NavigateTo(Enum.PageName.IA_SEARCH, "", false);

        }


        public void OnCreateDraftPressed(object sender, EventArgs args)
        {
            try
            {
                App.NavigationServiceInstance.NavigateTo(Enum.PageName.CREATEDRAFT, "", false);
                //App.NavigationServiceInstance.NavigateTo(Enum.PageName.EDITDRAFT, "", false);
            }
            catch (Exception ex)
            {

            }
        }



        public async void OnSelectGalleryPressedAsync(object sender, EventArgs args)
        {
            if (HeaderTitle.Text == "Draft Best Practice")
            {
                try
                {            
                    var accessLib = await Plugin.Media.CrossMedia.Current.PickPhotoAsync( new Plugin.Media.Abstractions.PickMediaOptions() { MaxWidthHeight=700 });

                    if (accessLib != null)
                    {
                        string[] values = accessLib.Path.ToString().Split('/');                       
                        //Byte[] byteArrays = ConverToByteArray(accessLib.GetStream());
                        Byte[] byteArrays = ConverToByteArray(accessLib.GetStreamWithImageRotatedForExternalStorage());
                        byte[] byteArray = await Plugin.ImageResizer.CrossImageResizer.Current.ResizeImageWithAspectRatioAsync(byteArrays, 700, 700);

                        ImageDraftModel draft = new ImageDraftModel { pictureName = values[values.Length - 1], pictureByte = byteArray, Timestamp = "Uploaded on " + DateTime.Now.Date.ToString("dd-MMM-yyyy") };

                        draft.RemovePhotoCommand = new Command(() =>
                        {
                            MessagingCenter.Send<ImageDraftModel>(draft, "removeImageDraft");
                        });
                        MessagingCenter.Send<ImageDraftModel>(draft, "addImageDraft");                   
                    }
                }
                catch (Exception ex)
                {
                }
            }
            else
            {
                try
                {
                    var accessLib = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions() { MaxWidthHeight=700});

                    if (accessLib != null)
                    {
                        string[] values = accessLib.Path.ToString().Split('/');
                       // Byte[] byteArrays = ConverToByteArray(accessLib.GetStream());   
                        Byte[] byteArrays = ConverToByteArray(accessLib.GetStreamWithImageRotatedForExternalStorage());   
                        byte[] byteArray = await Plugin.ImageResizer.CrossImageResizer.Current.ResizeImageWithAspectRatioAsync(byteArrays, 700, 700);                       
                        PracticeImage draft = new PracticeImage { pictureName = values[values.Length - 1], testImageSource = byteArray, pictureDate = "Uploaded on " + DateTime.Now.Date.ToString("dd-MMM-yyyy") };
                        MessagingCenter.Send<PracticeImage>(draft, "EditaddImageDraft");                       
                    }
                    return;
                }
                catch (Exception ex)
                {
                }
            }

        }

        public async void OnOpenCameraPressed(object sender, EventArgs args)
        {
            if (HeaderTitle.Text == "Draft Best Practice")
            {
                try
                {
                    var request = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Camera});   
                   
                    foreach (var item in request)
                    {
                        Constants.Access = item.Value.ToString();
                    }

                    // object permission = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);

                    if (Constants.Access == "Granted")
                    {

                        var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions() {  SaveToAlbum = true, MaxWidthHeight=700});               

                    if (photo != null)
                    {                       
                            string[] values = photo.Path.ToString().Split('/');
                           Byte[] byteArrays = ConverToByteArray(photo.GetStreamWithImageRotatedForExternalStorage());
                           // Byte[] byteArrays = ConverToByteArray(photo.GetStream());
                            byte[] byteArray = await Plugin.ImageResizer.CrossImageResizer.Current.ResizeImageWithAspectRatioAsync(byteArrays, 700, 700);
                            ImageDraftModel draft = new ImageDraftModel { pictureName = values[values.Length - 1], pictureByte = byteArray, Timestamp = "Uploaded on " + DateTime.Now.Date.ToString("dd-MMM-yyyy") };//
                      
                        draft.RemovePhotoCommand = new Command(() =>
                        {
                            MessagingCenter.Send<ImageDraftModel>(draft, "removeImageDraft");
                        });

                        MessagingCenter.Send<ImageDraftModel>(draft, "addImageDraft");
                    }
                    }

                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("", "Please use system settings to enable access to camera to take pictures", "OK");

                    }

                    return;
                }
                catch (Exception ex)
                {

                }
            }
            else
            {

                try
                {
                    var request = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Camera });

                    foreach (var item in request)
                    {
                        Constants.Access = item.Value.ToString();
                    }

                   

                    if (Constants.Access == "Granted")
                    {
                        var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions() { SaveToAlbum = true, MaxWidthHeight=700});
                        if (photo != null)
                        {
                            string[] values = photo.Path.ToString().Split('/');
                          Byte[] byteArrays = ConverToByteArray(photo.GetStreamWithImageRotatedForExternalStorage());
                            //Byte[] byteArrays = ConverToByteArray(photo.GetStream());
                            byte[] byteArray = await Plugin.ImageResizer.CrossImageResizer.Current.ResizeImageWithAspectRatioAsync(byteArrays, 700, 700);
                            PracticeImage draft = new PracticeImage { pictureName = values[values.Length - 1], testImageSource = byteArray, pictureDate = "Uploaded on " + DateTime.Now.Date.ToString("dd-MMM-yyyy") };
                            MessagingCenter.Send<PracticeImage>(draft, "EditaddImageDraft");
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert("", "Image size should be less than 3 Mb", "Retry");
                        }
                        }

                    return;
                }
                catch (Exception ex)
                {
                }
            }

        }

        public static byte[] ConverToByteArray(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        public void OnLogoutPressed(object sender, EventArgs args)
        {
            try
            {
                if (App.CustomNavigation.NavigationStack.Count > 0)
                {
                    if (App.CustomNavigation.NavigationStack.Count == 2)
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            var result = await App.Current.MainPage.DisplayAlert("Logout", "Are you sure you want to logout?", "Yes", "No");
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
            }
            catch (Exception ex)
            {

            }

        }

        public void OnClearFilterButtonPressed(object sender, EventArgs args)
        {
            if (ClearFilterCommand != null)
            {
                ClearFilterCommand.Execute(null);
            }
        }



        public void InitHeader(string title, bool isMainPage = false, bool isSearchEnabled = false,
                               bool isNoIcons = false,
                               bool isNotificationCount = false,
                               bool isClearButtonVisible = false, bool isRefreshIconVisible = false, bool isCreateDraftsIconVisible = false)
        {
            HeaderTitle.Text = title;
            HeaderTitle.HorizontalTextAlignment = TextAlignment.Start;
            HeaderTitle.IsVisible = true;

            if (isCreateDraftsIconVisible)
            {
                selectCamera.IsVisible = true;
                selectGallery.IsVisible = true;
                CreateDraftImg.IsVisible = false;
                //LogOffPng.IsVisible = false;
            }
            else
            {
                selectCamera.IsVisible = false;
                selectGallery.IsVisible = false;
            }
            if (isNoIcons)
            {
                //menuNotification.IsVisible = false;
                //menuSearch.IsVisible = false;

                menuBack.IsVisible = false;
                menuHamburger.IsVisible = false;

                //HeaderTitle.HorizontalTextAlignment = TextAlignment.Center;
                //clearFilter.IsVisible = false;
                //  refreshNotification.IsVisible = false;
                return;
            }

            //Right Side icons visible/invisible
            // menuNotification.IsVisible = isMainPage;


            //Left Side icons visible/invisible
            menuBack.IsVisible = !isMainPage;
            menuHamburger.IsVisible = isMainPage;
            //  refreshNotification.IsVisible = false;

            if (isSearchEnabled)
            {
                //  menuSearch.IsVisible = true;
                // MyNavigationBar.menuSearch.IsVisible = true;

            }
            else
            {
                //MyNavigationBar.menuSearch.IsVisible = false;
                //menuSearch.IsVisible = false;

            }
            // menuSearch.IsVisible = !menuSearch.IsVisible;

            //TODO: make null all command
            userProfileSection.IsVisible = false;

        }
        public ICommand SearchCommand { get; set; }
        public ICommand SearchTextEntryCommand { get; set; }
        public ICommand HamburgerCommand { get; set; }
        public ICommand ClearFilterCommand { get; set; }

    }
}
