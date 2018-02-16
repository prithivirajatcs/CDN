using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using Plugin.Connectivity;
using System.Diagnostics;
using EUJIT.Interface;
using EUJIT.Models;
using EUJIT.Services;
using System.IO;

namespace EUJIT.ViewModel
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        #region "Properties"
        public INavigationService navigationService;

        string _userName;
        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
                CheckLoginStatus();
                OnPropertyChanged("UserName");
            }
        }


        string _password;
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                CheckLoginStatus();
                OnPropertyChanged("Password");
            }
        }

        Color _loginButtonBackgroundColor;
        public Color LoginButtonBackgroundColor
        {
            get
            {
                return _loginButtonBackgroundColor;
            }
            set
            {
                _loginButtonBackgroundColor = value;

                OnPropertyChanged("LoginButtonBackgroundColor");
            }
        }

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

        bool _isRemebered;
        public bool IsRemebered
        {
            get
            {
                return _isRemebered;
            }
            set
            {
                _isRemebered = value;

                App.storage.StoreBool("RememeberMe", IsRemebered);

                //bool tempRem = App.storage.RetriveBool("RememeberMe");
                //if (!tempRem)
                //{
                //    App.storage.StoreBool("RememeberMe", IsRemebered);
                //}
                //else
                //{
                //    App.storage.StoreBool("RememeberMe", IsRemebered);
                //}

                OnPropertyChanged("IsRemebered");

            }
        }

        Color _loginButtonTextColor;
        public Color LoginButtonTextColor
        {
            get
            {
                return _loginButtonTextColor;
            }
            set
            {
                _loginButtonTextColor = value;

                OnPropertyChanged("LoginButtonTextColor");
            }
        }
        public ICommand LoginCommand { get; set; }
        public ICommand ClearCommand { get; set; }
        public ICommand RememberMeCommand { get; set; }
        public ICommand ForgotPasswordCmd { get; set; }
        #endregion

        #region "Events And Methods"

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        /// <summary>
        /// Used to handle Login Button
        /// </summary>
        /// <param name="navService"></param>

        public LoginViewModel(INavigationService navService)
        {
            navigationService = navService;
            if (App.storage.RetriveBool("RememeberMe"))
            {
                UserName = App.storage.RetrieveString("UserName").Trim();
                Password = App.storage.RetrieveString("Password");
                IsRemebered = true;
                LoginButtonBackgroundColor = (Color)Application.Current.Resources["Primary"];                 LoginButtonTextColor = (Color)Application.Current.Resources["LoginActiveTextColor"];
            }
            else
            {
                UserName = string.Empty;
                Password = string.Empty;
                IsRemebered = false;
                LoginButtonBackgroundColor = (Color)Application.Current.Resources["BannerColor"];                 LoginButtonTextColor = (Color)Application.Current.Resources["LoginActiveTextColor"];

            }
           
            //LoginButtonBackgroundColor = (Color)Application.Current.Resources["BannerColor"];
            //LoginButtonTextColor = (Color)Application.Current.Resources["LoginInActiveTextColor"];
            //LoginButtonBackgroundColor = (Color)Application.Current.Resources["BannerColor"];
            //LoginButtonTextColor = (Color)Application.Current.Resources["LoginInActiveTextColor"];

            this.LoginCommand = new Command(async (action) =>
            {
                if (!CrossConnectivity.Current.IsConnected)
                {
                    Application.Current.MainPage.DisplayAlert("", "Please check your internet connection", "OK");
                }
                else
                {
                    UserInfoModel loginmodel = new UserInfoModel();
                    var currentApp = Application.Current as App;
                    if (ValidateLogin())
                    {
                        this.IsBusy = true;
                        if (App.IsTestModeEnabled)
                        {
                            loginmodel.User_Name = "akarthsu";
                            loginmodel.Password = "Autumn17";
                            UtilService.UserName = "akarthsu";
                            UtilService.Password = "Autumn17";
                        }
                        else
                        {
                            loginmodel.User_Name = UserName.ToLower().Trim();
                            loginmodel.Password = Password;
                            UtilService.UserName = UserName.ToLower().Trim();
                            UtilService.Password = Password;
                        }

                        string cookie = await App.MyApplicationDataSource.GetSMcookie(loginmodel);
                        loginmodel.SMCookie = UtilService.cookie;
                        if (string.IsNullOrEmpty(UtilService.cookie))
                        {
                            IsBusy = false;
                            await Application.Current.MainPage.DisplayAlert("", "We are unable to verify your adient user id and /or password", "Retry");
                            return;
                        }
                        else
                        {
                            App.storage.StoreString("LoginDatetime", Convert.ToString(DateTime.Now));
                            App.storage.StoreString("SMCookie", loginmodel.SMCookie);
                            App.storage.StoreString("UserName", UserName);
                            App.storage.StoreString("Password", Password);
                            //App.storage.StoreBool("RememeberMe", IsRemebered);

                            BestPracticesListModel BPModel = await App.MyApplicationDataSource.GetBestPracticesList();

                            UtilService.Instance.RawPracticeList = BPModel.BestPracticeList;
                            UtilService.Instance.RawPrincipleList = BPModel.PrincpleList;
                            UtilService.Instance.RawPlantLocationList = BPModel.PlantLocationList;
                            UtilService.Instance.RawUserProfile = BPModel.UserProfile;
                            App.NavigationServiceInstance.NavigateTo(Enum.PageName.HOME, "", false);
                        }
                    }
                }

            });

            this.ClearCommand = new Command((action) =>
            {
                //Todo
            },
               (condition) =>
               {
                   return true;
               });
            this.ForgotPasswordCmd = new Command(() =>
            {
                Device.OpenUri(new Uri(Constants.Login_URL_QA));
            });
        }
        public bool ValidateLogin()
        {
            try
            {
                if (!string.IsNullOrEmpty((UserName.Trim())) && !string.IsNullOrEmpty((Password.Trim())) && UserName != Password)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return false;
            }

        }
        private void CheckLoginStatus()
        {
            if (!string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password))
            {
                //LoginButtonBackgroundColor = (Color)Application.Current.Resources["Primary"];
                LoginButtonBackgroundColor = Color.FromHex("#00465B");
                //LoginButtonTextColor = (Color)Application.Current.Resources["LoginInActiveTextColor"];
                LoginButtonTextColor =  Color.FromHex("#BCD432");
            }
            else
            {
                LoginButtonBackgroundColor = (Color)Application.Current.Resources["BannerColor"];
                LoginButtonTextColor = (Color)Application.Current.Resources["LoginInActiveTextColor"];
            }

        }
        #endregion
    }
}
