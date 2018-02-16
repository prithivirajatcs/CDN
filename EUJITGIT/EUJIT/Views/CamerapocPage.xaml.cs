using System;
using System.IO;
using EUJIT.CustomControl;
using EUJIT.ViewModels;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace EUJIT.Views
{

    public partial class CamerapocPage : BaseContentPage
    {
        //public const PhotoSize Custom = (Plugin.Media.Abstractions.PhotoSize)50;
        #region "Properties"
        private CamerapocPageViewModel vm;
        private object navparam;
        #endregion  

        public CamerapocPage()
        {
            InitPage();
        }
        public CamerapocPage(object a) {
            iApproveNavBar.MyNavigationBar.InitHeader("EU Ops", isMainPage: true, 
                                                      isNotificationCount: true, 
                                                      isClearButtonVisible: false,
                                                      isRefreshIconVisible:true); 
            iApproveNavBar.MyNavigationBar.HamburgerCommand = new Command((param) =>
            {

            });
            this.navparam = a;
            InitPage();
        }

        private void InitPage(){
            try
            {
                InitializeComponent();
                CameraButton.Clicked += CameraButton_Clicked;
                GalleryButton.Clicked += OpenGallery_Clicked;

             }
            catch (Exception ex)
            {

            }

            vm = new CamerapocPageViewModel(App.NavigationServiceInstance, navparam);
            this.BindingContext = vm;
        }
        private async void CameraButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions() { SaveToAlbum = true });

                //if (photo != null)
                //PhotoImage.Source = ImageSource.FromStream(() => { return photo.GetStream(); });
            }
            catch (Exception ex)
            {

            }

            //DependencyService.Get<ISaveToGallery>().sendPhoto(ph);
            //if (photo != null)
            //{
            //    using (var memoryStream = new MemoryStream())
            //    {
            //        photo.GetStream().CopyTo(memoryStream);
            //        //photo.Dispose();
            //        byte[] result = memoryStream.ToArray();

            //        DependencyService.Get<ISaveToGallery>().sendPhoto(result);

            //    }
            //   // PhotoImage.Source = ImageSource.FromStream(() => { return photo.GetStream(); });
            //}

            return;


        }



        private async void OpenGallery_Clicked(object sender, EventArgs e)
        {
            var accessLib = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions() { });

            if (accessLib != null)
            {
                PhotoImage.Source = ImageSource.FromStream(() => { return accessLib.GetStream(); });
            }
            return;
        }


    }
}
