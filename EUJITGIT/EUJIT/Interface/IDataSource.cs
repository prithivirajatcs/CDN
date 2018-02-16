using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EUJIT.Models;

namespace EUJIT.Interface
{
    public interface IDataSource
    {
        Task<BestPracticesListModel> GetBestPracticesList();
        Task<List<BestPractice>> RemoveBestPractice(string practiceId);
		Task<List<BestPractice>> CreateBestPractice(object createdraft);
        Task<List<BestPractice>> EditBestPractice(object createdraft);
        Task<List<BestPractice>> LoadBestPractice(object loaddraft);
        //Object Login(LoginModel loginModel);
        //object GetNotifications();
        //Object GetPendingRequests(string departmentIdentifier);
        //Object GetPendingRequestDetails(string departmentIdentifier);
        //void GetDenyReasons();
        //void GetComments();
        //void GetCDSIDs();
        Task<string> GetSMcookie(UserInfoModel loginModel);
        //string GetUserNameByGlobalId(string GlobalID);
    }
}
