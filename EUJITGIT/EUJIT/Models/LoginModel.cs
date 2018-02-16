using System;
namespace EUJIT.Model
{
    public class LoginModel
    {

        public string UserName
        {
            get;
            set;
        }

        public string Password
        {
            get;
            set;
        }

        public ResponseStatus LoginStatus
        {
            get;
            set;
        }
        public string SMCookie
        {
            get;
            set;
        }
        public DateTime LoginDatetime
        {
            get;
            set;
        }
    }
}
