using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCatalog.Model
{
    public class APIRequestModel
    {
        public dynamic? Meta { get; set; }
        public dynamic? Errors { get; set; }
        public dynamic? Data { get; set; }
        public dynamic? Links { get; set; }
    }
}
