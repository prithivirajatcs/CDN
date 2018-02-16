using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using EUJIT.CustomControl;
using EUJIT.Interface;
using Xamarin.Forms;
using System;
using EUJIT.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using EUJIT.Services;
using System.Linq;
using Plugin.Connectivity;

namespace EUJIT.ViewModel
{
    public class HomeViewModel : INotifyPropertyChanged
    {

        #region "Properties"
        public INavigationService navigationService;
        public new event PropertyChangedEventHandler PropertyChanged;

        bool _isBusy;
        public bool IsBusy
        {
            get { return this._isBusy; }
            set
            {
                this._isBusy = value;
                OnPropertyChanged("IsBusy");
            }
        }

        ObservableCollection<PracticeImage> _bPImage;
        public ObservableCollection<PracticeImage> BPImage
        {
            get { return this._bPImage; }
            set
            {
                this._bPImage = value;
                OnPropertyChanged("BPImage");
            }
        }

        bool _isPullToRefreshEnabled;
        public bool IsPullToRefreshEnabled
        {
            get { return this._isPullToRefreshEnabled; }
            set
            {
                this._isPullToRefreshEnabled = value;
                OnPropertyChanged("IsPullToRefreshEnabled");
            }
        }



        ObservableCollection<ExtenededBestPractice> _bPList;
        public ObservableCollection<ExtenededBestPractice> BPList
        {
            get { return this._bPList; }
            set
            {
                this._bPList = value;
                OnPropertyChanged("BPList");
            }
        }

        BestPractice _selectedDraft;
        public BestPractice SelectedDraft
        {
            get
            {
                return _selectedDraft;
            }
            set
            {
                _selectedDraft = value;
                //if (_selectedDraft != null)
                //{
                //    App.NavigationServiceInstance.NavigateTo(Enum.PageName.EDITDRAFT, "", false);
                //}
                //if (_selectedDraft != null)
                //{

                //    OnPropertyChanged("SelectedDraft");

                //    UtilService.requestId = _selectedRequest.field1;
                //    UtilService.taskId = _selectedRequest.field5;
                //    UtilService.taskName = _selectedRequest.field6;
                //    if (UtilService.taskName.Contains("UpdateMe"))
                //    {
                //        UtilService.actionName = "TodoDetailUpdateMe";
                //    }
                //    else
                //        UtilService.actionName = "TodoDetail";

                //    App.NavigationServiceInstance.NavigateTo(Enum.PageName.IA_PENDINGREQUESTSDETAILS, "", false);
                //    _selectedRequest = null;
                //}
            }
        }


        private string bpListCount;
        public string BPListCount
        {
            get { return this.bpListCount; }
            set
            {
                this.bpListCount = value;
                OnPropertyChanged("BPListCount");
            }
        }

        ImageSource _bpImageSource;
        public ImageSource BPImageSource
        {
            get { return _bpImageSource; }
            set
            {
                _bpImageSource = value;
                OnPropertyChanged("BPImageSource");
            }
        }

        private BestPracticesListModel bpModel;
        public BestPracticesListModel BPModel
        {
            get { return this.bpModel; }
            set
            {
                this.bpModel = value;
            }
        }


        ImageSource _photoImageSource;
        public ImageSource PhotoImageSource
        {
            get { return _photoImageSource; }
            set
            {
                _photoImageSource = value;
                OnPropertyChanged("PhotoImageSource");
            }
        }

        string _searchString;
        public string SearchString
        {
            get { 
                return this._searchString; }
            set
            {
                this._searchString = value;
                OnPropertyChanged("SearchString");
                if (_searchString.Length > 0)
                {
                    ObservableCollection<BestPractice> searchList = new ObservableCollection<BestPractice>(UtilService.Instance.RawPracticeList.Where(x => x.PracticeHeader.ToUpper().Contains(_searchString.ToUpper())));
                    PrepareHomePageAsync(searchList.ToList());
                }
                else
                {
                    //BPList = new ObservableCollection<ExtenededBestPractice>(UtilService.Instance.RawPracticeList);
                    ObservableCollection<BestPractice> searchList = new ObservableCollection<BestPractice>(UtilService.Instance.RawPracticeList);
                    PrepareHomePageAsync(searchList.ToList());
                }
            }
        }



        #endregion

        #region "Events"   


        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this,
                    new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
        BaseContentPage myPage;

        #region "Methods"    
        public void PrepareHomePageAsync(List<BestPractice> list)
        {
            try
            {
                String bPracticeId = "";
                List<ExtenededBestPractice> eBPractice = new List<ExtenededBestPractice>();
                foreach (var item in list)
                {
                    ExtenededBestPractice practice = new ExtenededBestPractice { BestPractice = item };

                    if (practice.BestPractice.practiceImage.Count == 0)
                        practice.IsPracticePreviewVisible = false;
                    else
                        practice.IsPracticePreviewVisible = true;

                    bPracticeId = practice.BestPractice.PracticeId;

                    practice.EditCommand = new Command(async (param) =>
                    {
                        if (!CrossConnectivity.Current.IsConnected)
                        {
                            Application.Current.MainPage.DisplayAlert("", "Please check your internet connection", "OK");
                        }
                        else
                        {
                            ExtenededBestPractice bPractice = param as ExtenededBestPractice;

                            List<ImageDraftModel> imageList = new List<ImageDraftModel>();
                            foreach (var pImageItem in bPractice.BestPractice.practiceImage)
                            {
                                ImageDraftModel draft = new ImageDraftModel { pictureName = pImageItem.pictureName, pictureByte = pImageItem.testImageSource };

                                imageList.Add(draft);
                            }

                            CreateDraftModel draftModel = new CreateDraftModel
                            {
                                practiceId = bPractice.BestPractice.PracticeId,
                                plantId = bPractice.BestPractice.bpPlantId,
                                practiceHeader = bPractice.BestPractice.PracticeHeader,
                                practiceImage = imageList,
                                princpleId = bPractice.BestPractice.bpPrincipleId
                            };

                            //List<BestPractice> BPModel;



                            IsBusy = true;
                            List<BestPractice> editItem = await App.MyApplicationDataSource.LoadBestPractice(draftModel);



                            App.NavigationServiceInstance.NavigateTo(Enum.PageName.EDITDRAFT, editItem[0], false);

                            IsBusy = false;
                        }
                    });

                    practice.DeleteCommand = new Command(async (param) =>
                    {
                        if (!CrossConnectivity.Current.IsConnected)
                        {
                            Application.Current.MainPage.DisplayAlert("", "Please check your internet connection", "OK");
                        }
                        else
                        {
                            try
                            {
                                ExtenededBestPractice bPractice = param as ExtenededBestPractice;
                                string id = bPractice.BestPractice.PracticeId;
                                IsBusy = true;
                                var bPList = await App.MyApplicationDataSource.RemoveBestPractice(id);

                                if (bPList is List<BestPractice>)
                                {
                                    BPList.RemoveAt(BPList.ToList().FindIndex(x => x.BestPractice.PracticeId == bPractice.BestPractice.PracticeId));
                                    BPListCount = BPList.Count.ToString();
                                    UtilService.Instance.RawPracticeList = bPList;
                                }
                                IsBusy = false;
                            }
                            catch (Exception ex)
                            {

                            }
                        }

                    });

                    eBPractice.Add(practice);
                }

                BPList = new ObservableCollection<ExtenededBestPractice>(eBPractice);
                BestPractice bpcmd = new BestPractice();
                UtilService.loggedInGlobalID = UtilService.Instance.RawUserProfile.userGlobalId;

                BPListCount = BPList.Count.ToString();

                foreach (var items in BPList)
                {

                    foreach (var items1 in items.BestPractice.practiceImage)
                    {
                        // BPImage = new ObservableCollection<PracticeImage>(items.practiceImage);
                        byte[] bytes = Convert.FromBase64String(items1.pictureByte);

                        items1.testImageSource = bytes;

                        //BPImage = new ObservableCollection<PracticeImage>((System.Collections.Generic.IEnumerable<camerapoc.Models.PracticeImage>)items);
                        //BPImageSource = items1.pictureByte;
                    }
                }

            }
            catch (Exception ex)
            {

            }





            //BPImage = new ObservableCollection<PracticeImage>(BPModel.BestPracticeList.practiceImage);

            //List<PracticeImage> pimg = new List<PracticeImage>();


            //byte[] bytes = Convert.FromBase64String(""); 

            //BPImageSource = ImageSource.FromStream(() => new MemoryStream(bytes));


        }

        public HomeViewModel(INavigationService navService, BaseContentPage myContentPage)
        {

            navigationService = navService;

            PrepareHomePageAsync(UtilService.Instance.RawPracticeList);






            this.TakePhotoCommand = new Command(async (obj) =>
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
            });

            this.SelectPhotoCommand = new Command(async (obj) =>
            {

                var accessLib = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions() { });

                if (accessLib != null)
                {
                    PhotoImageSource = ImageSource.FromStream(() => { return accessLib.GetStream(); });
                }
                using (var memoryStream = new MemoryStream())
                {
                    accessLib.GetStream().CopyTo(memoryStream);
                    accessLib.Dispose();
                    byte[] imageAsBytes = memoryStream.ToArray();
                    Debug.WriteLine(imageAsBytes);
                }

                return;
            });
            this.RefreshBestPracticesCommand = new Command(async () =>
            {
                if (!CrossConnectivity.Current.IsConnected)
                {
                    Application.Current.MainPage.DisplayAlert("", "Please check your internet connection", "OK");
                    IsPullToRefreshEnabled = false;
                }
                else
                {
                    //IsPullToRefreshEnabled = true;
                    BestPracticesListModel BPModel = await App.MyApplicationDataSource.GetBestPracticesList();

                    UtilService.Instance.RawPracticeList = BPModel.BestPracticeList;
                    UtilService.Instance.RawPrincipleList = BPModel.PrincpleList;
                    UtilService.Instance.RawPlantLocationList = BPModel.PlantLocationList;
                    UtilService.Instance.RawUserProfile = BPModel.UserProfile;

                    PrepareHomePageAsync(UtilService.Instance.RawPracticeList);
                    IsPullToRefreshEnabled = false;
                }

            });
        }

        public ICommand TakePhotoCommand { get; set; }
        public ICommand SelectPhotoCommand { get; set; }
        public ICommand RefreshBestPracticesCommand { get; set; }

        #endregion
    }

    public class ExtenededBestPractice
    {
        BestPractice _bestPractice;
        public BestPractice BestPractice
        {
            get
            {
                return _bestPractice;
            }
            set
            {
                _bestPractice = value;
            }
        }

        bool _isPracticePreviewVisible;
        public bool IsPracticePreviewVisible
        {
            get { return _isPracticePreviewVisible; }
            set { _isPracticePreviewVisible = value; }
        }

        public ICommand EditCommand { get; set; }

        public ICommand DeleteCommand { get; set; }
    }

}








