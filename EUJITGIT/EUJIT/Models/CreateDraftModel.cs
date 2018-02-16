using System;
using System.Collections.Generic;
using EUJIT.Models;

namespace EUJIT.Models
{
    public class CreateDraftModel
    {
        public string practiceId { get; set; }

        public string practiceHeader { get; set; }

        public string princpleId { get; set; }

        public string plantId { get; set; }

        public List<ImageDraftModel> practiceImage { get; set; }
    }
}
