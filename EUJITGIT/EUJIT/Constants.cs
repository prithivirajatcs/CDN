namespace EUJIT
{
    public class Constants
    {
        public const string STR_DBNAME = "<iApprove>.db";
        public const string STR_VECHICLE = "Vehicle";
        public const string STR_PROPERTY = "Property";
        public const string strCamera = "Camera Utillity";
        public const string msgCameraNotsupport = "Camera Not Supported";
        public const string STR_PROFILE_PHOTO = "profile.png";

        public const string strOK = "OK";
        public const string strNo = "No";
        public const string strYes = "Yes";
        public const string strCancel = "Cancel";
        public const string strDelete = "Delete";
        public const string strAction = "Action";
        public const string strProfilePin = "Pin";

        public const string MSG_HEADER = "Alert";
        public const string MAX_PHOTOS_MSG = "Please remove an image if you would like to add a new image. A maximum of 5 images are only allowed for Best Practice";
        public const string MAX_CHAR_MSG = "Only 100 characters are allowed";

        public const string MSG_POPUP_PRINCIPLE = "Select a Principle";
        public const string MSG_POPUP_PLANT = "Select a Plant";
        public const string MSG_POPUP_HEADER = "Enter the Practice Header";

        public const string MSG_SAVED_SUCCESS = "Saved Successfully";

        public const float DefaultMapZoomLevel = 15f;

        public const string ActionItemContentView = "ActionItemContentView";
        public const string ActionItemModel = "ActionItemModel";
        public const string CommentsModel = "CommentsModel";
        public const string LoginViewModel = "LoginViewModel";
        public const string PushNotification = "PushNotificationHandler";

        #region QA URLs
        public const string Login_URL_QA = "https://agsmqa.adient.com/siteminderagent/forms/ag_primary.fcc?TYPE=33554433&REALMOID=06-8d818df1-1478-4321-9054-8e012cf39021&GUID=&SMAUTHREASON=0&METHOD=GET&SMAGENTNAME=XhTkd5nKOaScDI466wMXH924wJkj3jDbtdMBzIMHuhJpeI10ZpGajDuWvgo7xW25&TARGET=-SM-https%3a%2f%2fagqa%2eadient%2ecom%2f";
        public const string HomePage_URL_QA = "https://agqa.adient.com/EUJIT/api/viewprofile";
        public const string CreateDraft_URL_QA = "https://agqa.adient.com/EUJIT/api/capturepractice";
        public const string DeletePage_URL_QA = "https://agqa.adient.com/EUJIT/api/removepractice";
        public const string EditDraft_URL_QA = "https://agqa.adient.com/EUJIT/api/editpractice";
        public const string LoadPractice_URL_QA = "https://agqa.adient.com/EUJIT/api/loadpractice";


        #endregion

        #region PROD URLs
        public const string Login_URL_PROD = "https://agsm.adient.com/siteminderagent/forms/ag_primary.fcc?TYPE=33554433&REALMOID=06-7314043b-693b-4eb6-a41d-aaa62f2751c2&GUID=&SMAUTHREASON=0&METHOD=GET&SMAGENTNAME=-SM-CWMNwUeKlmj%2btaX5McgOlBRW9gcoMohxmaYnz0s912iKPcGqgE12iw0ee3rsDE%2b6&TARGET=-SM-http%3a%2f%2fag%2eadient%2ecom%2f";
        public const string HomePage_URL_PROD = "https://ag.adient.com/EUJIT/api/viewprofile";
        public const string CreateDraft_URL_PROD = "https://ag.adient.com/EUJIT/api/capturepractice";
        public const string DeletePage_URL_PROD = "https://ag.adient.com/EUJIT/api/removepractice";
        public const string EditDraft_URL_PROD = "https://ag.adient.com/EUJIT/api/editpractice";
        public const string LoadPractice_URL_PROD = "https://ag.adient.com/EUJIT/api/loadpractice";


        #endregion


        public const string Environment = "PROD";
        public const string STR_STATUS_SUCCESS = "SUCCESS";
        public const string STR_STATUS_SUCCESS_CODE = "200";
        public static string Access;

    }
}
