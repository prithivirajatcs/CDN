using System;
using System.Collections.Generic;
using EUJIT.Interface;
using EUJIT.Models;
using System.IO;
using System.Reflection;
using EUJIT.Views;
using Newtonsoft.Json;
using EUJIT.Enum;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;

namespace EUJIT.Services
{
    public class DataService : IDataSource
    {

        static HttpClient client;
        static Object rootobject;
        static Object response;


        public static String cookie;
        public static KeyValuePair<string, string> cookieCollection { get; private set; }
        public DataService()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
        }
        public DataService(string flag)
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
            switch (flag)
            {
                case "QA":
                    UtilService.Login_URL = Constants.Login_URL_QA;
                    UtilService.HomePageURL = Constants.HomePage_URL_QA;
      

                    break;
                case "PROD":
                    UtilService.Login_URL = Constants.Login_URL_PROD;
                    UtilService.HomePageURL = Constants.HomePage_URL_PROD;
     
                    break;

            }
        }




        public void DownloadDoc()
        {
            if (Device.OS == TargetPlatform.Android)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    Application.Current.MainPage.DisplayAlert("Download Successfully", "Path :: storage/emulated/0/Android/data/com.adient/files/Download/" + UtilService.docName, "OK");
                });
            }


        }
        public void SaveResponse()
        {

            string saveResponse = response.ToString();
            if ((saveResponse.Contains("Success")) || (saveResponse.Contains("approved")))

            {

                Device.BeginInvokeOnMainThread(() =>
                {
                    Application.Current.MainPage.DisplayAlert("Your comments saved successfully", "", "OK");
                });
            }

            else if ((saveResponse.Contains("Denied")) || (saveResponse.Contains("_1")) || (saveResponse.Contains("_2")) || (saveResponse.Contains("Approved")))
            {

                Device.BeginInvokeOnMainThread(() =>
                {
                    Application.Current.MainPage.DisplayAlert("The Request has been processed successfully", "", "OK");

                    var navStack = App.CustomNavigation.NavigationStack;
                    var existingPages = App.CustomNavigation.NavigationStack.ToList();

                    if ((existingPages.Count.ToString() == "4") && (existingPages[2].GetType().Name != "NotificationsPage"))
                    {
                        foreach (var page in existingPages)
                        {
                            if (page.GetType().Name == "PendingRequestsPage" || page.GetType().Name == "PendingRequestDetailsPage")
                            {
                                App.CustomNavigation.RemovePage(page);

                            }

                        }

                       // App.NavigationServiceInstance.NavigateTo(Enum.PageName.IA_PENDINGREQUESTS, 1, false);
                    }

                    else if (existingPages[2].GetType().Name == "NotificationsPage")
                    {
                        foreach (var page in existingPages)
                        {
                            if (page.GetType().Name == "NotificationsPage" || page.GetType().Name == "PendingRequestDetailsPage")
                            {
                                App.CustomNavigation.RemovePage(page);

                            }

                        }

                       // App.NavigationServiceInstance.NavigateTo(Enum.PageName.IA_NOTIFICATIONS, "", false);

                    }


                    else
                    {
                        foreach (var page in existingPages)
                        {
                            if ((page.GetType().Name == "HomePage") || (page.GetType().Name == "PendingRequestDetailsPage"))
                            {
                                App.CustomNavigation.RemovePage(page);

                            }

                        }
                       //App.NavigationServiceInstance.NavigateTo(Enum.PageName.IA_HOME, "", false);

                    }
                });
            }
            else
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    Application.Current.MainPage.DisplayAlert("Error", "Something went wrong Please contact your Admin", "OK");
                });
            }

        }



        public static Object GetStubResponse(ServiceName enumValue)
        {
            try
            {


                var assembly = typeof(LoginPage).GetTypeInfo().Assembly;
                Stream stream = new MemoryStream();

                DataService service = new DataService(Constants.Environment);
                switch (enumValue)
                {
                    case ServiceName.LOGIN:



                        //string parms = "{\"globalId\":\"akarthsu\",\"systemName\":\"\"}";
                        string parms = "{\"globalId\":" + string.Format("\"{0}\"", UtilService.globalId) + "," + "\"systemName\":" + string.Format("\"{0}\"", string.Empty) + "}";

                        var baseAddress = new System.Uri(UtilService.HomePageURL);

                        service.GetResult(cookie, baseAddress, parms, string.Empty);
                        //rootobject = JsonConvert.DeserializeObject<SessionInfo>(response.ToString());
                        break;

                    case ServiceName.PENDINGREQUEST:

                        //parms = "{\"globalId\":\"akarthsu\",\"systemName\":\"itaras\",\"actionName\":\"TodoList\"}";
                        parms = "{\"globalId\":" + string.Format("\"{0}\"", UtilService.globalId) + "," + "\"systemName\":" + string.Format("\"{0}\"", UtilService.systemName) + ",\"actionName\":\"TodoList\"}";
                        baseAddress = new System.Uri(UtilService.ToDoURL);
                        service.GetResult(cookie, baseAddress, parms, string.Empty);
                        //rootobject = JsonConvert.DeserializeObject<PendingRequests>(response.ToString());

                        // stream = assembly.GetManifestResourceStream("iApprove.Stubs.getPendingRequestsResponse.json");
                        //using (var reader = new System.IO.StreamReader(stream))
                        //{
                        //  var json = reader.ReadToEnd();

                        //  //var jsonObject = JsonConvert.DeserializeObject(json) as JObject;

                        //  //rootobject = jsonObject["PendingRequestDetails"].ToObject<PendingRequestDetails>();

                        //  rootobject = JsonConvert.DeserializeObject<PendingRequests>(json);
                        //}
                        break;

                    case ServiceName.PENDINGREQUESTDETAILS:

                        string page = "PENDINGREQUESTDETAILS";
                        // baseAddress = new System.Uri("https://agqa.adient.com/ITUAS/iapprove/approval.action");
                        baseAddress = new System.Uri(UtilService.ToDoListURL);
                        parms = "{\"globalId\":" + string.Format("\"{0}\"", UtilService.globalId) + "," + "\"requestId\":" + string.Format("\"{0}\"", UtilService.requestId) + "," + "\"taskName\":" + string.Format("\"{0}\"", UtilService.taskName) + "," + "\"taskId\":" + string.Format("\"{0}\"", UtilService.taskId) + "," + "\"applicationName\":" + string.Format("\"{0}\"", UtilService.systemName) + "," + "\"systemName\":" + string.Format("\"{0}\"", UtilService.systemName) + "," + "\"actionName\":" + string.Format("\"{0}\"", UtilService.actionName) + "}";
                        //parms =   "{\"globalId\":\"akarthsu\",\"requestId\":\"2025152\",\"taskName\":\"ManagerApproval\",\"taskId\":\"_TKI:a01b015d.c614b763.9b44ebf5.f5f698d6\",\"systemName\":\"ITARAS\",\"actionName\":\"TodoDetail\"}";
                        service.GetResult(cookie, baseAddress, parms, page);
                       // rootobject = JsonConvert.DeserializeObject<PendingRequestDetails>(response.ToString());
                        break;


                    //stream = assembly.GetManifestResourceStream("iApprove.Stubs.getPendingRequestDetails.json");
                    //using (var reader = new System.IO.StreamReader(stream))
                    //{
                    //    var json = reader.ReadToEnd();

                    //    //var jsonObject = JsonConvert.DeserializeObject(json) as JObject;

                    //    //rootobject = jsonObject["PendingRequestDetails"].ToObject<PendingRequestDetails>();

                    //    rootobject = JsonConvert.DeserializeObject<PendingRequestDetails>(json);
                    //}

                    //break;
                    case ServiceName.NOTIFICATIONS:




                        parms = "{\"globalId\":\"akarthsu\"}";
                        //string  parms = "{\"globalId\":" + string.Format("\"{0}\"", UtilService.globalId) + "," + "\"systemName\":" + string.Format("\"{0}\"", string.Empty) + "}";

                        baseAddress = new System.Uri(UtilService.GetAllNotificationURL);

                        service.GetResult(cookie, baseAddress, parms, string.Empty);
                        //rootobject = JsonConvert.DeserializeObject<NotificationsInfo>(response.ToString());
                        break;


                    //stream = assembly.GetManifestResourceStream("iApprove.Stubs.getNotificationsResponse.json");
                    //using (var reader = new System.IO.StreamReader(stream))
                    //{
                    //    var json = reader.ReadToEnd();
                    //    var jsonObject = JsonConvert.DeserializeObject(json) as JObject;

                    //    rootobject = jsonObject["NotificationModel"].ToObject<List<NotificationModel>>();
                    //    }
                    //break;
                    default:
                        break;
                }

                return rootobject;
            }
            catch (Exception ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Application.Current.MainPage.DisplayAlert("Sorry !!!", "Service is temporarily unavailable. Please try after sometime", "OK");
                    App.NavigationServiceInstance.GoBack();

                });
                throw ex;
            }
        }




        //  public async static void GetResult(string Cookie, System.Uri baseAddress, string parms, string page)
        //  {
        //      rootobject = new Object();
        //      response = new object();
        //      try
        //      {
        //          using (var handler = new HttpClientHandler { UseCookies = false })
        //          using (var client = new HttpClient(handler) { BaseAddress = baseAddress })
        //          {
        //              var message = new HttpRequestMessage(HttpMethod.Post, baseAddress);
        //              message.Headers.TryAddWithoutValidation("Cookie", Cookie);
        //              message.Content = new StringContent(parms, Encoding.UTF8, "application/json");
        //              HttpResponseMessage result = client.SendAsync(message).Result;      

        //              if (result.Content.Headers.ContentType.CharSet == null)
        //              {
        //                  byte[] item = await result.Content.ReadAsByteArrayAsync();
        //                  DependencyService.Get<ISaveFile>().SaveFiles(UtilService.docName, item);    

        //}
        //            else
        //            {
        //                if (result.Content.Headers.ContentType.MediaType=="application/json")
        //                {
        //                    response = result.Content.ReadAsStringAsync().Result;
        //                }
        //                //else
        //                //{
        //                //  //  response = string.Empty;
        //                //  //await Application.Current.MainPage.DisplayAlert("Error", "Service temporarily unavailable. Please try after sometime", "OK");  
        //                //}
        //            }                

        //        }
        //    }
        //    catch (Exception exceptionservice)
        //    {
        //    }
        //}


        public async void GetResult(string Cookie, System.Uri baseAddress, string parms, string page)
        {
            rootobject = new Object();
            response = new object();
            try
            {
                //if (!CrossConnectivity.Current.IsConnected)
                //{
                //    Application.Current.MainPage.DisplayAlert("Error", "No Network connection", "OK");
                //    //App.NavigationServiceInstance.GoBack();

                //}
                //else
                //{
                using (var handler = new HttpClientHandler { UseCookies = false })
                using (var client = new HttpClient(handler) { BaseAddress = baseAddress })
                {
                    var message = new HttpRequestMessage(HttpMethod.Post, baseAddress);
                    message.Headers.TryAddWithoutValidation("Cookie", Cookie);
                    message.Content = new StringContent(parms, Encoding.UTF8, "application/json");
                    // App.NetworkCheck();

                    HttpResponseMessage result = client.SendAsync(message).Result;

                    if (result.Content.Headers.ContentType.CharSet == null)
                    {
                        byte[] item = await result.Content.ReadAsByteArrayAsync();
                       // DependencyService.Get<ISaveFile>().SaveFiles(UtilService.docName, item);

                    }
                    else
                    {
                        if (result.Content.Headers.ContentType.MediaType == "application/json")
                        {
                            response = result.Content.ReadAsStringAsync().Result;
                        }
                        //else
                        //{
                        //  //  response = string.Empty;
                        //  //await Application.Current.MainPage.DisplayAlert("Error", "Service temporarily unavailable. Please try after sometime", "OK");  
                        //}
                    }

                }
                //}
            }
            catch (Exception exceptionservice)
            {
            }
        }


        //public async Task<String> GetSMcookie(UserInfoModel loginModel)
        //{
        //    StringBuilder cookieVal = new StringBuilder();
        //    StringContent str = new StringContent("USER=" + loginModel.UserName + "&PASSWORD=" + loginModel.Password, Encoding.UTF8, "application/x-www-form-urlencoded");
        //    UtilService.globalId = loginModel.UserName;

        //    try
        //    {
        //        //if (!CrossConnectivity.Current.IsConnected)
        //        //{
        //        //    Application.Current.MainPage.DisplayAlert("Error", "No Network connection", "OK");
        //        //    //App.NavigationServiceInstance.GoBack();

        //        //}
        //        //else{
        //        var response1 = await client.PostAsync(new Uri(UtilService.Login_URL), str);
        //        if (response1.IsSuccessStatusCode)
        //        {

        //            foreach (var item in response1.Headers.GetValues("Date"))
        //            {
        //                UtilService.loggedInTime = item;
        //            }

        //            var smcookie = response1.Headers.GetValues("Set-Cookie");

        //            foreach (var item in smcookie)
        //            {
        //                cookieVal.Append(item).Append(',');
        //            }
        //            if (cookieVal.ToString().Contains("SMSESSION"))
        //            {
        //                UtilService.cookie = cookieVal.ToString();
        //            }
        //            else
        //            {

        //                return await Task.FromResult(string.Empty);
        //            }

        //        }
        //        else
        //        {
        //            UtilService.alert = true;
        //            await Application.Current.MainPage.DisplayAlert("Error", "Service temporarily unavailable. Please try after sometime", "OK");
        //            return await Task.FromResult(string.Empty);
        //        }
        //        //}
        //    }
        //    catch (WebException exception)
        //    {
        //        string responseText;
        //        using (var reader = new StreamReader(exception.Response.GetResponseStream()))
        //        {
        //            responseText = reader.ReadToEnd();
        //        }
        //    }

        //    return cookieVal.ToString();
        //}



        //public string GetUserNameByGlobalId(string GlobalID)
        //{
        //    DataService service = new DataService(Constants.Environment);
        //    //var parms = "{\"globalId\":\"akarthsu\"}";
        //    var parms = "{\"globalId\":" + string.Format("\"{0}\"", GlobalID) + "}";
        //    try
        //    {
        //        //if (!CrossConnectivity.Current.IsConnected)
        //        //{
        //        //    Application.Current.MainPage.DisplayAlert("Error", "No Network connection", "OK");
        //        //    //App.NavigationServiceInstance.GoBack();

        //        //}
        //        //else
        //        //{
        //        using (var handler = new HttpClientHandler { UseCookies = false })
        //        using (var client = new HttpClient(handler) { BaseAddress = new Uri(Constants.GetUserName_URL_QA) })
        //        {
        //            var message = new HttpRequestMessage(HttpMethod.Post, new Uri(Constants.GetUserName_URL_QA));
        //            message.Headers.TryAddWithoutValidation("Cookie", UtilService.cookie);
        //            message.Content = new StringContent(parms, Encoding.UTF8, "application/json");
        //            //App.NetworkCheck();
        //            HttpResponseMessage result = client.SendAsync(message).Result;


        //            if (result.Content.Headers.ContentType.MediaType == "application/json")
        //            {
        //                response = result.Content.ReadAsStringAsync().Result;
        //            }
        //            //else
        //            //{
        //            //  //  response = string.Empty;
        //            //  //await Application.Current.MainPage.DisplayAlert("Error", "Service temporarily unavailable. Please try after sometime", "OK");  
        //            //}


        //        }
        //        //}
        //    }
        //    catch (Exception exceptionservice)
        //    {
        //    }

        //    //  StringContent str = new StringContent("globalId=" + GlobalID, Encoding.UTF8, "application/x-www-form-urlencoded");
        //    //object response2 = await client.PostAsync(new Uri(Constants.GetUserName_URL_QA), str);
        //    //service.GetResult(UtilService.cookie, new Uri(Constants.GetUserName_URL_QA), parms, string.Empty);
        //    UserProfileModel val = JsonConvert.DeserializeObject<UserProfileModel>(response.ToString());
        //    return val.userName.ToString();

        //}

        public Task<UserInfoModel> AuthenticateUser(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RegisterNotification(string token)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetSMCookieforLogin(UserInfoModel loginModel)
        {
            throw new NotImplementedException();
        }



        public Task<string> GetSMcookie(UserInfoModel loginModel)
        {
            throw new NotImplementedException();
        }

        public Task<object> GetBestPracticesList()
        {
            throw new NotImplementedException();
        }

        Task<BestPracticesListModel> IDataSource.GetBestPracticesList()
        {
            throw new NotImplementedException();
        }

       

        public Task<List<BestPractice>> CreateBestPractice(object createdraft)
        {
            throw new NotImplementedException();
        }

        public Task<List<BestPractice>> RemoveBestPractice(string practiceId)
        {
            throw new NotImplementedException();
        }

        public Task<List<BestPractice>> EditBestPractice(object createdraft)
        {
            throw new NotImplementedException();
        }

        public Task<BestPractice> LoadBestPractice(object loaddraft)
        {
            throw new NotImplementedException();
        }

        Task<List<BestPractice>> IDataSource.LoadBestPractice(object loaddraft)
        {
            throw new NotImplementedException();
        }

        public class PendingRequestClass
        {
            //public List<PendingRequests> PendingRequestsList { get; set; }
        }
    }
}
