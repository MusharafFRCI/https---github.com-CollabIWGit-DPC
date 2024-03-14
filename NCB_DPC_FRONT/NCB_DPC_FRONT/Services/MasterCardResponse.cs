using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCB_DPC_FRONT.Services
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Session
    {
        public string Id { get; set; }
        public string UpdateStatus { get; set; }
        public string Version { get; set; }
    }

    public class MastercardSessionResponse
    {
        public string checkoutMode { get; set; }
        public string Merchant { get; set; }
        public string Result { get; set; }
        public Session Session { get; set; }
        public string SuccessIndicator { get; set; }
    }

    public class MyResponse
    {
        public MastercardSessionResponse MastercardSessionResponse { get; set; }
        public object SessionErrorResponse { get; set; }
    }


}
