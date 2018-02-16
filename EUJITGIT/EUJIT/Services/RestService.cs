using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using EUJIT.Interface;
using Newtonsoft.Json;
using EUJIT.Models;
using EUJIT.Enum;
using System.IO;
using Xamarin.Forms;

namespace EUJIT.Services
{
    public class RestService : IServiceHandler
    {
        public static RestService objShared;
        public static RestService GetInstance()
        {
            if (objShared == null)
            {
                objShared = new RestService();
            }
            return objShared;
        }
        public void Init(string baseUrl, Miscellaneous.SericeType serType)
        {
            this.baseURL = baseUrl;
            this.serviceType = serType;
        }
        private string baseURL;
        public string BaseURL
        {
            get
            {
                return this.baseURL;
            }

            set
            {
                this.baseURL = value;
            }
        }
        Miscellaneous.SericeType serviceType;
        public Miscellaneous.SericeType ServiceType
        {
            get
            {
                return serviceType;
            }

            set
            {
                serviceType = value;
            }
        }

        public async Task<T> GetRequest<T>(string urlSuffix, string requestStr, Type reqType) where T : new()
        {
            try
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(BaseURL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                    if (urlSuffix != null && urlSuffix.Trim().Length > 0)
                    {
                        if (requestStr != null && requestStr.Trim().Length > 0)
                        {
                            requestStr = urlSuffix + "/" + requestStr;
                        }
                        requestStr = urlSuffix;
                    }

                    var retTask = client.GetAsync(requestStr);
                    Task.WaitAll(retTask);

                    HttpResponseMessage response = retTask.Result;// await client.GetAsync(requestStr);

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {

                        var strResponse = await response.Content.ReadAsStringAsync();

                        var responseObj = (T)JsonConvert.DeserializeObject(strResponse, typeof(T));

                        return responseObj;
                    }
                    else
                    {
                        var responseError = "{\"status_code:'FAILED'\"}";

                        var responseObj = (T)JsonConvert.DeserializeObject(responseError, typeof(T));

                        return responseObj;
                    }
                }
            }
            catch (Exception ex)
            {
                //TODO: write error handler here
            }
            return (T)(object)"{}";
        }

        public async Task<T> PostRequest<T>(string urlSuffix, string requestStr, object reqObj, Type reqType) where T : new()
        {
            try
            {

                using (var handler = new HttpClientHandler { UseCookies = false })
                using (var client = new HttpClient(handler) { BaseAddress = new Uri(BaseURL) })
                {
                    var message = new HttpRequestMessage(HttpMethod.Post, new Uri(BaseURL));
                    message.Headers.TryAddWithoutValidation("Cookie", UtilService.cookie);
                    var json = JsonConvert.SerializeObject(reqObj);

                    message.Content = new StringContent(json, Encoding.UTF8, "application/json");
                    // App.NetworkCheck();

                    HttpResponseMessage result = await client.SendAsync(message);

                    var response = await result.Content.ReadAsStringAsync();

                    var responseObj = (T)JsonConvert.DeserializeObject(response, typeof(T));

                    return responseObj;
                }
            }
            catch (Exception ex)
            {
                //TODO: write error handler here
            }
            return (T)(object)"{}";
        }
    }
}