using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace EUJIT
{
    public class RootObject
    {
        private string statusCode;
        [JsonProperty("StatusCode")]
        public string StatusCode
        {
            get { return this.statusCode; }
            set { this.statusCode = value; }
        }

        private string statusMessage;
        [JsonProperty("StatusMessage")]
        public string StatusMessage
        {
            get { return this.statusMessage; }
            set { this.statusMessage = value; }
        }

    }

    //public class RootObjectPOList : RootObject
    //{
    //    private ObservableCollection<PurchaseOrderModel> data;
    //    [JsonProperty("Data")]
    //    public ObservableCollection<PurchaseOrderModel> Data
    //    {
    //        get { return this.data; }
    //        set { this.data = value; }
    //    }
    //}

    //public class RootObjectPODetail : RootObject
    //{
    //    private PODetailModel data;
    //    [JsonProperty("Data")]
    //    public PODetailModel Data
    //    {
    //        get { return this.data; }
    //        set { this.data = value; }
    //    }
    //}
    public class RootObjectViewProfile: RootObject
    {
        private object data;

        public object Data
        {
            get { return this.data; }
            set { this.data = value; }
        }
    }
    public class RootObjectAuthentication : RootObject
    {
        private object data;

        public object Data
        {
            get { return this.data; }
            set { this.data = value; }
        }
    }

    //public class RootObjectShipmentConfirmation : RootObject
    //{

    //    private shipmentConfiramationModel data;
    //    [JsonProperty("Data")]
    //    public shipmentConfiramationModel Data
    //    {
    //        get { return this.data; }
    //        set { this.data = value; }
    //    }
    //}


    //public class RootObjectPOAck : RootObject
    //{

    //    private PoAckModel data;
    //    [JsonProperty("Data")]
    //    public PoAckModel Data
    //    {
    //        get { return this.data; }
    //        set { this.data = value; }
    //    }
    //}

}
