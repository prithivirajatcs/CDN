using System;
using Newtonsoft.Json;

namespace EUJIT.Models
{
    public class UserInfoModel
    {
        public UserInfoModel()
        {
        }

        private string user_name;
        public string User_Name
        {
            get { return this.user_name; }
            set { this.user_name = value; }
        }

        private string password;
        public string Password
        {
            get 
            {
                return this.password; }
            set { this.password = value; }
        }


        private string smcookie;
        public string SMCookie
        {
            get
            {
                return this.smcookie;
            }
            set { this.smcookie = value; }
        }

       


    }
}
