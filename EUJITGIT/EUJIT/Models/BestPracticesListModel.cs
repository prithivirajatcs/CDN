using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using Newtonsoft.Json;
using Xamarin.Forms;
using System.Windows.Input;
using System.IO;

namespace EUJIT.Models
{
    // Home Page returns Best practices, principle List, plant list, user profile
    public class BestPracticesListModel
    {
        [JsonProperty("principleList")]
        public List<Principle> PrincpleList { get; set; }
        [JsonProperty("bestPracticeList")]
        public List<BestPractice> BestPracticeList { get; set; }
        [JsonProperty("plantLocationList")]
        public List<PlantLocation> PlantLocationList { get; set; }
        [JsonProperty("userProfile")]
        public UserProfile UserProfile { get; set; }


    }
    // Edit & Remove services return only Best practices
    public class BestPracticesOnlyListModel
    {
        [JsonProperty("bestPracticeList")]
        public List<BestPractice> BestPracticeList { get; set; }
    }


    public class Principle
    {
        [JsonProperty("princpleName")]
        public string princpleName { get; set; }
        [JsonProperty("princpleId")]
        public string principleId { get; set; }
        [JsonProperty("princpleDescription")]
        public string princpleDescription { get; set; }
    }

    public class PracticeImage
    {
        [JsonProperty("pictureName")]
        public string pictureName { get; set; }
        [JsonProperty("pictureId")]
        public string pictureId { get; set; }
        [JsonProperty("pictureOrder")]
        public string pictureOrder { get; set; }
        [JsonProperty("pictureByte")]
        public string pictureByte { get; set; }
        [JsonProperty("pictureDate")]
        public string pictureDate { get; set; }


        public Byte[] testImageSource { get; set; }


    }

    public class BestPractice
    {

        [JsonProperty("deleteFlag")]
        public string deleteFlag { get; set; }
        [JsonProperty("bpUserMail")]
        public string bpUserMail { get; set; }
        [JsonProperty("bpUserName")]
        public string bpUserName { get; set; }
        [JsonProperty("practiceText")]
        public string practiceText { get; set; }
        [JsonProperty("bpOwnerGlobalId")]
        public string bpOwnerGlobalId { get; set; }
        [JsonProperty("PracticeId")]
        public string PracticeId { get; set; }
        [JsonProperty("PracticeHeader")]
        public string PracticeHeader { get; set; }
        [JsonProperty("bpUserId")]
        public string bpUserId { get; set; }
        [JsonProperty("bpPlantId")]
        public string bpPlantId { get; set; }
        [JsonProperty("bpPlantName")]
        public string bpPlantName { get; set; }
        //[JsonProperty("bpPrincpleId")]
        //public string bpPrincpleId { get; set; }
        [JsonProperty("bpPrincpleName")]
        public string bpPrincple { get; set; }
        [JsonProperty("bpCreatedDate")]
        public string bpCreatedDate { get; set; }
        [JsonProperty("practiceImage")]
        public List<PracticeImage> practiceImage { get; set; }
        [JsonProperty("bpPrincpleId")]
        public string bpPrincipleId { get; set; }
    }

    public class PlantLocation
    {
        [JsonProperty("plantName")]
        public string plantName { get; set; }
        [JsonProperty("plantId")]
        public string plantId { get; set; }
    }

    public class UserProfile
    {
        [JsonProperty("userPlant")]
        public string userPlant { get; set; }
        [JsonProperty("userEmail")]
        public string userEmail { get; set; }
        [JsonProperty("userId")]
        public string userId { get; set; }
        [JsonProperty("userProfileName")]
        public string userProfileName { get; set; }
        [JsonProperty("userGlobalId")]
        public string userGlobalId { get; set; }
    }
}
