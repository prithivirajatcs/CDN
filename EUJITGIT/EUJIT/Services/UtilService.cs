using System;
using System.Collections.Generic;
using EUJIT.Models;

namespace EUJIT.Services
{
    public class UtilService
    {
        private static UtilService _instance;
        public static UtilService Instance => _instance ?? (_instance = new UtilService());

        public static String loggedInGlobalID;
        public static String cookie;
        public static String globalId;
        public static String systemName;
        public static String taskId;
        public static String taskName;
        public static String requestId;
        public static String actionName;
        public static String saveComment;
        public static String approvalComment;
        public static String flag;
        public static String Indicator;
        public static String commentsTab;
        public static String loggedInTime;
        public static String docName;
        public static String docId;
        public static bool alert;
        public static bool cookieFlag;

        public static string Login_URL;
        public static string HomePageURL;
        public static string ToDoURL;
        public static string ToDoListURL;
        public static string GetAllNotificationURL;
        public static string ActionItemContentView_URL;
        public static string ActionItemModel_URL;
        public static string Comments_URL;
        public static string LoginViewModel_URL;
        public static string PushNotification_URL;
        public static string ClearAll_URL;
        public static string GetUserName;
        public static string UserName;
        public static string Password;
        public static string fcmToken;
        public static string LoginUser;
        public static string AppVersion;
        public static int BuildNumber;
        public static int BadgeCount;

        public List<BestPractice> RawPracticeList
        {
            get;
            set;
        }

        public List<Principle> RawPrincipleList
        {
            get;
            set;
        }

        public List<PlantLocation> RawPlantLocationList
        {
            get;
            set;
        }

        public UserProfile RawUserProfile
        {
            get;
            set;
        }
    }


}
