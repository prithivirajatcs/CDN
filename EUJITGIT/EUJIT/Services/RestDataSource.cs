using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using EUJIT.Interface;
using EUJIT.Models;
using EUJIT.Services;
using System.Threading.Tasks;
using EUJIT.Enum;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;
using Newtonsoft.Json;

namespace EUJIT.Services
{
    public class RestDataSource : IDataSource
    {
        ICommonUtility utility = DependencyService.Get<ICommonUtility>();
        #region "End-Points"
        static Object rootobject;
        static Object response;
        private string baseURL = "https://demo2974790.mockable.io/viewprofile";
        //private string baseURL = "http://redcscgtmes03.ced.corp.cummins.com:8080/ShippingPOC/"; //TODO: Prod service URL
        public static string Login_URL = "";
        public static string HomePage_URL = "";
        public static string CreateDraft_URL = "";
        public static string DeletePage_URL = "";
        public static string EditDraft_URL = "";
        public static string LoadPractice_URL = "";
        #endregion

        #region "Constructors"
        public RestDataSource()
        {
        }
        public RestDataSource(string flag)
        {
            switch (flag)
            {
                case "QA":
                    Login_URL = Constants.Login_URL_QA;
                    HomePage_URL = Constants.HomePage_URL_QA;
                    CreateDraft_URL=Constants.CreateDraft_URL_QA;
                    DeletePage_URL = Constants.DeletePage_URL_QA;
                    EditDraft_URL = Constants.EditDraft_URL_QA;
                    LoadPractice_URL = Constants.LoadPractice_URL_QA;
                    break;
                case "PROD":
                    Login_URL = Constants.Login_URL_PROD;
                    HomePage_URL = Constants.HomePage_URL_PROD;
                    CreateDraft_URL = Constants.CreateDraft_URL_PROD;
                    DeletePage_URL = Constants.DeletePage_URL_PROD;
                    EditDraft_URL = Constants.EditDraft_URL_PROD;
                    LoadPractice_URL = Constants.LoadPractice_URL_PROD;
                    break;
            }
        }
        #endregion

        #region "Login"
        public async Task<string> GetSMcookie(UserInfoModel loginModel)
        {
            StringBuilder cookieVal = new StringBuilder();
            StringContent str = new StringContent("USER=" + loginModel.User_Name + "&PASSWORD=" + loginModel.Password, Encoding.UTF8, "application/x-www-form-urlencoded");
            UtilService.globalId = loginModel.User_Name;

            try
            {
                HttpClient client = new HttpClient();
                var response1 = await client.PostAsync(new Uri(Login_URL), str);
                if (response1.IsSuccessStatusCode)
                {

                    foreach (var item in response1.Headers.GetValues("Date"))
                    {
                        UtilService.loggedInTime = item;
                    }

                    var smcookie = response1.Headers.GetValues("Set-Cookie");

                    foreach (var item in smcookie)
                    {
                        cookieVal.Append(item).Append(',');
                    }
                    if (cookieVal.ToString().Contains("SMSESSION"))
                    {
                        UtilService.cookie = cookieVal.ToString();
                    }
                    else
                    {
                        UtilService.cookie = "";
                    }

                }
                else
                {
                    UtilService.cookie = "";
                    UtilService.alert = true;
                    await Application.Current.MainPage.DisplayAlert("Error", "Service temporarily unavailable. Please try after sometime", "OK");
                    return await Task.FromResult(string.Empty);
                }
            }
            catch (Exception ex)
            {
          
            }
            return "";
        }
        public async Task<BestPracticesListModel> RemoveBestPractice()
        {
            //try
            //{
            //    BestPracticesListModel retObj = new BestPracticesListModel();
            //    DataService service = new DataService(Constants.Environment);
            //    IServiceHandler serviceHandler = RestService.GetInstance();
            //    serviceHandler.Init(Constants.DeletePage_URL_QA, Miscellaneous.SericeType.STUB);
            //    Dictionary<string, string> reqParams = new Dictionary<string, string>();
            //    reqParams.Add("globalId", UtilService.globalId);
            //    reqParams.Add("SM_USER", UtilService.globalId);
            //    reqParams.Add("channel", "mobile");
            //}
            //catch (Exception ex)
            //{

            //}

            return null;

        }
        public async Task<BestPracticesListModel> GetBestPracticesList()
        {
            try
            {
                BestPracticesListModel retObj = new BestPracticesListModel();
                DataService service = new DataService(Constants.Environment);
                //string baseAddress = "https://demo2974790.mockable.io/";
                //await service.GetResult("", baseAddress, "", "");


                IServiceHandler serviceHandler = RestService.GetInstance();
                serviceHandler.Init(HomePage_URL, Miscellaneous.SericeType.STUB);

                Dictionary<string, string> reqParams = new Dictionary<string, string>();

                reqParams.Add("globalId", UtilService.globalId);
                reqParams.Add("SM_USER", UtilService.globalId);
                reqParams.Add("channel", "mobile");

                retObj = await serviceHandler.PostRequest<BestPracticesListModel>("", "", reqParams, null);
                //serviceTask.Wait();

                //if (serviceTask.IsCompleted)
                //{
                //    //Load data from here
                //    retObj = serviceTask.Result;
                //    if (response != null)
                //    {
                //        //retObj = new ObservableCollection<PurchaseOrderModel>(response.Data);
                //    }
                //    else
                //    {
                //        //TODO: Show Error message here
                //        //Ask standly add attributes 'statusCode ==200/otehrs, statusMessage="Succes/Errormessage")
                //    }
                //}
                return retObj;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<BestPractice>> CreateBestPractice(object createDraftModel)
        {
            try
            {
                CreateDraftModel draftModel = createDraftModel as CreateDraftModel;

                List<BestPractice> retObj = new List<BestPractice>();

                IServiceHandler serviceHandler = RestService.GetInstance();

                serviceHandler.Init(CreateDraft_URL, Miscellaneous.SericeType.STUB);

                Dictionary<string, Object> reqParams = new Dictionary<string, Object>();

                reqParams.Add("globalId", UtilService.globalId);
                reqParams.Add("actionName", "draft");
                reqParams.Add("channel", "mobile");
                reqParams.Add("practiceHeader", draftModel.practiceHeader);
                reqParams.Add("princpleId", draftModel.princpleId);
                reqParams.Add("plantId", draftModel.plantId);

                List<CreatePracticeImage> pImages = new List<CreatePracticeImage>();

                foreach (ImageDraftModel item in draftModel.practiceImage)
                {
                    //Byte[] compressedByteArray = utility.CompressImage(item.pictureByte);
                    string base64String = Convert.ToBase64String(item.pictureByte);
                    //string base64String = ConvertToBase64String(item.pictureByte);
                    CreatePracticeImage pImage = pImage = new CreatePracticeImage { pictureName = item.pictureName, pictureByte = base64String };
                    pImages.Add(pImage);
                }

                reqParams.Add("practiceImage", pImages);

                retObj = await serviceHandler.PostRequest<List<BestPractice>>("", "", reqParams, null);

                return retObj;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<List<BestPractice>> LoadBestPractice(object loaddraft) {

            CreateDraftModel draftModel = loaddraft as CreateDraftModel;

            List<BestPractice> retObj = new List<BestPractice>();

            IServiceHandler serviceHandler = RestService.GetInstance();

            serviceHandler.Init(LoadPractice_URL, Miscellaneous.SericeType.STUB);

            Dictionary<string, Object> reqParams = new Dictionary<string, Object>();

            reqParams.Add("globalId", UtilService.globalId);
            reqParams.Add("actionName", "loadpractice");
            reqParams.Add("channel", "mobile");
            reqParams.Add("practiceId", draftModel.practiceId);
            retObj = await serviceHandler.PostRequest<List<BestPractice>>("", "", reqParams, null);

            return retObj;
        }
        public async Task<List<BestPractice>> EditBestPractice(object createDraftModel)
        {
            CreateDraftModel draftModel = createDraftModel as CreateDraftModel;

            List<BestPractice> retObj = new List<BestPractice>();

            IServiceHandler serviceHandler = RestService.GetInstance();

            serviceHandler.Init(EditDraft_URL, Miscellaneous.SericeType.STUB);

            Dictionary<string, Object> reqParams = new Dictionary<string, Object>();

            reqParams.Add("globalId", UtilService.globalId);
            reqParams.Add("actionName", "draft");
            reqParams.Add("channel", "mobile");
            reqParams.Add("practiceId", draftModel.practiceId);
            reqParams.Add("practiceHeader", draftModel.practiceHeader);
            reqParams.Add("princpleId", draftModel.princpleId);
            reqParams.Add("plantId", draftModel.plantId);

            List<CreatePracticeImage> pImages = new List<CreatePracticeImage>();

            foreach (ImageDraftModel item in draftModel.practiceImage)
            {
                
               // Byte[] compressedByteArray = utility.CompressImage(item.pictureByte);
                string base64String = Convert.ToBase64String(item.pictureByte);
               // string base64String = ConvertToBase64String(item.pictureByte);
                CreatePracticeImage pImage = pImage = new CreatePracticeImage { pictureName = item.pictureName, pictureByte = base64String };
                pImages.Add(pImage);
            }

            reqParams.Add("practiceImage", pImages);

            retObj = await serviceHandler.PostRequest<List<BestPractice>>("", "", reqParams, null);

            return retObj;
        }

        public string ConvertToBase64String(object bytes)
        {
            byte[] byteArray = bytes as byte[];
            String base64String = Convert.ToBase64String(byteArray);
            return base64String;
        }

        public async Task<List<BestPractice>> RemoveBestPractice(string practiceId)
        {
            List<BestPractice> retObj = new List<BestPractice>();

            Dictionary<string, string> reqParams = new Dictionary<string, string>();

            reqParams.Add("globalId", UtilService.globalId);
            reqParams.Add("actionName", "remove");
            reqParams.Add("channel", "mobile");
            reqParams.Add("practiceId", practiceId);

            IServiceHandler serviceHandler = RestService.GetInstance();

            serviceHandler.Init(DeletePage_URL, Miscellaneous.SericeType.STUB);

            retObj = await serviceHandler.PostRequest<List<BestPractice>>("", "", reqParams, null);

            return retObj;
        }

      
       #endregion
 }
    public class CreatePracticeImage
    {
        public string pictureName { get; set; }
        public string pictureByte { get; set; }
    }
}
