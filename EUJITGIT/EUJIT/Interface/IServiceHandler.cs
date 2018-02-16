using System;
using System.Net.Http;
using System.Threading.Tasks;
using EUJIT.Enum;
using EUJIT.Models;

namespace EUJIT.Interface
{
    public interface IServiceHandler
    {
        string BaseURL { get; set; }
        Miscellaneous.SericeType ServiceType { get; set; }

        void Init(string baseUrl, Miscellaneous.SericeType serType);
        Task<T> GetRequest<T>(string urlSuffix, string requestStr, Type reqType) where T : new();
        Task<T> PostRequest<T>(string urlSuffix, string requestStr, object reqObj, Type reqType) where T : new();
    }
}