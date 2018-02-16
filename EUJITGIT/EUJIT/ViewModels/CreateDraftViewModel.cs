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
    public class CreateDraftViewModel : INotifyPropertyChanged
    {

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

        ObservableCollection<ImageDraftModel> _imageDraftList = new ObservableCollection<ImageDraftModel>();
        public ObservableCollection<ImageDraftModel> ImageDraftList
        {
            get 
            { return _imageDraftList; }
            set
            {
                if (_imageDraftList == value)
                    return;
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

        string _practiceHeader;
        public string PracticeHeader
        {
            get { return _practiceHeader; }
            set
            {
                this._practiceHeader = value;
                OnPropertyChanged("PracticeHeader");
            }
        }

        ImageDraftModel _selectedImageDraft = new ImageDraftModel();
        public ImageDraftModel SelectedImageDraft
        {
            get { return _selectedImageDraft; }
            set
            {
                this._selectedImageDraft = value;
                OnPropertyChanged("SelectedImageDraft");
            }
        }

       

        public CreateDraftViewModel()
        {
        }

        public void PrepareDraftsPage()
        {

            PrincipleList = new ObservableCollection<Principle>(UtilService.Instance.RawPrincipleList);
            PlantLocationList = new ObservableCollection<PlantLocation>(UtilService.Instance.RawPlantLocationList);
        }

        #region "Methods"       
        public CreateDraftViewModel(INavigationService navService, BaseContentPage myContentPage)
        {

            navigationService = navService;
            PrepareDraftsPage();
            myPage = myContentPage;

            MessagingCenter.Subscribe<ImageDraftModel>(this, "addImageDraft", (sender) =>
            {
                if (ImageDraftList.Count >= 5)
                {
                    Application.Current.MainPage.DisplayAlert(Constants.MSG_HEADER, Constants.MAX_PHOTOS_MSG, Constants.strOK);
                }
                else
                {
                    //Device.BeginInvokeOnMainThread(() =>
                    //{
                    ImageDraftList.Add(sender);
                    //});
                }
            });

            MessagingCenter.Subscribe<ImageDraftModel>(this, "removeImageDraft", (sender) =>
            {
                ImageDraftList.Remove(sender);
            });

            this.OnTextChanged = new Command((obj) =>
            {

            });

            this.SaveDraftCommand = new Command(async (obj) =>
           {
               if (!CrossConnectivity.Current.IsConnected)
               {
                   Application.Current.MainPage.DisplayAlert("", "Please check your internet connection", "OK");
               }
               else
               {
                   try
                   {
                       CreateDraftViewModel vm = obj as CreateDraftViewModel;

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
                       if (vm.PracticeHeader == null || vm.PracticeHeader.Trim().Length == 0)
                       {
                           await Application.Current.MainPage.DisplayAlert(Constants.MSG_HEADER, Constants.MSG_POPUP_HEADER, Constants.strOK);
                           return;
                       }

                       //if(vm.ImageDraftList.Count==0)
                       //{

                       //}

                       CreateDraftModel draftModel = new CreateDraftModel
                       {
                           plantId = vm.SelectedPlant.plantId,
                           practiceHeader = vm.PracticeHeader,
                           practiceImage = vm.ImageDraftList.ToList(),
                           princpleId = vm.SelectedPrinciple.principleId
                       };

                       IsBusy = true;

                       var BPModel = await App.MyApplicationDataSource.CreateBestPractice(draftModel);

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
        public ICommand OnTextChanged { get; set; }

        //DraftList
        public ICommand SaveDraftCommand { get; set; }
       

    }

}

