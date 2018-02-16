using System;
using Newtonsoft.Json;

namespace EUJIT
{
    public class PushNotificationTokenModel
    {
        public PushNotificationTokenModel()
        {
        }

        private string token;
        public string Token
        {
            get { return this.token; }
            set { this.token = value; }
        }
    }
}
