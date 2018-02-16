using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using EUJIT.CustomControl;
using EUJIT.Interface;
using EUJIT.Models;
using EUJIT.Services;
using EUJIT.ViewModel;
using Plugin.Connectivity;
using Xamarin.Forms;

namespace EUJIT.ViewModels
{
    public class EditBestPracticeViewModel : INotifyPropertyChanged
    {

        public INavigationService navigationService;
        public new event PropertyChangedEventHandler PropertyChanged;
        BaseContentPage myPage;

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

        ObservableCollection<Principle> _principleList;
        public ObservableCollection<Principle> PrincipleList
        {
            get { return this._principleList; }
            set
            {
                this._principleList = value;
                OnPropertyChanged("PrincipleList");
            }
        }


        BestPractice _selectedBestPractice;
        public BestPractice SelectedBestPractice
        {
            get { return this._selectedBestPractice; }
            set
            {
                this._selectedBestPractice = value;
                OnPropertyChanged("SelectedBestPractice");
            }
        }

        ObservableCollection<PlantLocation> _plantLocationList;
        public ObservableCollection<PlantLocation> PlantLocationList
        {
            get { return this._plantLocationList; }
            set
            {
                this._plantLocationList = value;
                OnPropertyChanged("PlantLocationList");
            }
        }

        int _principleIndex;
        public int PrincipleIndex
        {
            get { return _principleIndex; }
            set
            {
                _principleIndex = value;
                OnPropertyChanged("PrincipleIndex");
            }
        }

        int _plantIndex;
        public int PlantIndex
        {
            get { return _plantIndex; }
            set
            {
                _plantIndex = value;
                OnPropertyChanged("PlantIndex");
            }
        }

        String _headerText;
        public String HeaderText
        {
            get { return _headerText; }
            set
            {
                _headerText = value;
                OnPropertyChanged("HeaderText");
            }
        }

        ObservableCollection<ExtendedPracticeImage> _imageDraftList = new ObservableCollection<ExtendedPracticeImage>();
        public ObservableCollection<ExtendedPracticeImage> ImageDraftList
        {
            get
            {
                return _imageDraftList;
            }
            set
            {
                this._imageDraftList = value;
                OnPropertyChanged("ImageDraftList");
            }
        }

        Principle _selectedPrinciple;
        public Principle SelectedPrinciple
        {
            get { return _selectedPrinciple; }
            set
            {
                this._selectedPrinciple = value;
                OnPropertyChanged("SelectedPrinciple");
            }
        }


        PlantLocation _selectedPlant;
        public PlantLocation SelectedPlant
        {
            get { return _selectedPlant; }
            set
            {
                this._selectedPlant = value;
                OnPropertyChanged("SelectedPlant");
            }
        }

        //ObservableCollection<ExtendedPracticeImage> _editDraftList = new ObservableCollection<ExtendedPracticeImage>();
        //public ObservableCollection<ExtendedPracticeImage> EditDraftList
        //{
        //    get { return _editDraftList; }
        //    set
        //    {
        //        if (_editDraftList == value)
        //            return;
        //        this._editDraftList = value;
        //        OnPropertyChanged("EditDraftList");
        //    }
        //}


        #region "Methods"       
        public EditBestPracticeViewModel(INavigationService navService, BaseContentPage myContentPage)
        {

            navigationService = navService;

            myPage = myContentPage;

            MessagingCenter.Subscribe<PracticeImage>(this, "EditaddImageDraft", (sender) =>
            {
                if (ImageDraftList.Count >= 5)
                {
                    Application.Current.MainPage.DisplayAlert(Constants.MSG_HEADER, Constants.MAX_PHOTOS_MSG, Constants.strOK);
                    return;
                }
                else
                {
                    ExtendedPracticeImage ePraticeImage = new ExtendedPracticeImage { PracticeImage = sender };

                    ePraticeImage.RemovePhotoCommand = new Command((param) =>
                    {
                        ExtendedPracticeImage exPracticeImage = param as ExtendedPracticeImage;
                        ImageDraftList.Remove(exPracticeImage);
                    });
                    ImageDraftList.Add(ePraticeImage);
                }
            });

            this.SaveCommand = new Command(async (obj) =>
           {

               if (!CrossConnectivity.Current.IsConnected)
               {
                   Application.Current.MainPage.DisplayAlert("", "Please check your internet connection", "OK");
               }
               else
               {

                   try
                   {
                       EditBestPracticeViewModel vm = obj as EditBestPracticeViewModel;

                       if (vm.SelectedPrinciple == null || vm.SelectedPrinciple.principleId.Trim().Length == 0)
                       {
                           await Application.Current.MainPage.DisplayAlert(Constants.MSG_HEADER, Constants.MSG_POPUP_PRINCIPLE, Constants.strOK);
                           return;
                       }
                       if (vm.SelectedPlant == null || vm.SelectedPlant.plantId.Trim().Length == 0)
                       {
                           await Application.Current.MainPage.DisplayAlert(Constants.MSG_HEADER, Constants.MSG_POPUP_PLANT, Constants.strOK);
                           return;
                       }
                       if (vm.HeaderText == null || vm.HeaderText.Trim().Length == 0)
                       {
                           await Application.Current.MainPage.DisplayAlert(Constants.MSG_HEADER, Constants.MSG_POPUP_HEADER, Constants.strOK);
                           return;
                       }


                       List<ImageDraftModel> imageList = new List<ImageDraftModel>();
                       foreach (var item in ImageDraftList)
                       {
                           ImageDraftModel draft = new ImageDraftModel { pictureName = item.PracticeImage.pictureName, pictureByte = item.PracticeImage.testImageSource };

                           imageList.Add(draft);
                       }

                       CreateDraftModel draftModel = new CreateDraftModel
                       {
                           practiceId = SelectedBestPractice.PracticeId,
                           plantId = vm.SelectedPlant.plantId,
                           practiceHeader = vm.HeaderText,
                           practiceImage = imageList,
                           princpleId = vm.SelectedPrinciple.principleId
                       };

                       IsBusy = true;

                       List<BestPractice> BPModel = await App.MyApplicationDataSource.EditBestPractice(draftModel);

                       if (BPModel is List<BestPractice>)
                       {
                           var navStack = App.CustomNavigation.NavigationStack;

                           var bindingContext = navStack[navStack.Count - 2].BindingContext;

                           HomeViewModel hVM = bindingContext as HomeViewModel;

                           ObservableCollection<BestPractice> updateRawPracticeList = new ObservableCollection<BestPractice>(BPModel);
                           UtilService.Instance.RawPracticeList = updateRawPracticeList.ToList();

                           hVM.PrepareHomePageAsync(BPModel);

                           IsBusy = false;
                           await Application.Current.MainPage.DisplayAlert("", Constants.MSG_SAVED_SUCCESS, Constants.strOK);

                           ImageDraftList.Clear();

                           App.NavigationServiceInstance.GoBack();


                       }



                   }
                   catch (Exception ex)
                   {

                   }
               }

           });
        }
        public void LoadData(BestPractice bestpracticemodel)
        {

            SelectedBestPractice = bestpracticemodel;
            //var practiceId = bestpracticemodel.PracticeId;
            PrincipleList = new ObservableCollection<Principle>(UtilService.Instance.RawPrincipleList);
            PlantLocationList = new ObservableCollection<PlantLocation>(UtilService.Instance.RawPlantLocationList);


            PrincipleIndex = UtilService.Instance.RawPrincipleList.FindIndex(x => x.principleId == SelectedBestPractice.bpPrincipleId);
            PlantIndex = UtilService.Instance.RawPlantLocationList.FindIndex(x => x.plantId == SelectedBestPractice.bpPlantId);

            HeaderText = SelectedBestPractice.PracticeHeader;

            List<ExtendedPracticeImage> practiceImageList = new List<ExtendedPracticeImage>();

            foreach (var item in SelectedBestPractice.practiceImage)
            {
                ExtendedPracticeImage practiceImage = new ExtendedPracticeImage { PracticeImage = item };

                practiceImage.PracticeImage.testImageSource = Convert.FromBase64String(practiceImage.PracticeImage.pictureByte);

                practiceImage.PracticeImage.pictureDate = "Uploaded on " + practiceImage.PracticeImage.pictureDate.Substring(0, 11);

                practiceImage.RemovePhotoCommand = new Command((param) =>
                {
                    ExtendedPracticeImage exPracticeImage = param as ExtendedPracticeImage;
                    ImageDraftList.Remove(exPracticeImage);
                });

                practiceImageList.Add(practiceImage);
            }

            ImageDraftList = new ObservableCollection<ExtendedPracticeImage>(practiceImageList);
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
        public EditBestPracticeViewModel()
        {
        }

        public ICommand SaveCommand { get; set; }
    }

    public class ExtendedPracticeImage
    {
        PracticeImage _practiceImage;
        public PracticeImage PracticeImage
        {
            get
            {
                return _practiceImage;
            }
            set
            {
                _practiceImage = value;
            }
        }

        public ICommand RemovePhotoCommand { get; set; }

    }

}
