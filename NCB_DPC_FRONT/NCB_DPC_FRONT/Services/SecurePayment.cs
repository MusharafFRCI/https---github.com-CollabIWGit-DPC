using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCB_DPC_FRONT.Services
{
    public class SecurePayment
    {

        public class _3DS
        {
            public string methodPostData { get; set; }
            public string methodUrl { get; set; }
        }

        public class _3ds2
        {
            public string directoryServerId { get; set; }
            public bool methodCompleted { get; set; }
            public string methodSupported { get; set; }
            public string protocolVersion { get; set; }
            public string requestorId { get; set; }
            public string requestorName { get; set; }
        }

        public class Authentication
        {
            [JsonProperty("3ds2")]
            public _3ds2 _3ds2 { get; set; }
            public string acceptVersions { get; set; }
            public string channel { get; set; }
            public string purpose { get; set; }
            public Redirect redirect { get; set; }
            public string redirectHtml { get; set; }
            public string version { get; set; }
        }

        public class Card
        {
            public string brand { get; set; }
            public string fundingMethod { get; set; }
            public string number { get; set; }
            public string scheme { get; set; }
        }

        public class Customized
        {
            [JsonProperty("3DS")]
            public _3DS _3DS { get; set; }
        }

        public class Order
        {
            public string authenticationStatus { get; set; }
            public DateTime creationTime { get; set; }
            public string currency { get; set; }
            public string id { get; set; }
            public DateTime lastUpdatedTime { get; set; }
            public string merchantCategoryCode { get; set; }
            public string status { get; set; }
            public int totalAuthorizedAmount { get; set; }
            public int totalCapturedAmount { get; set; }
            public int totalRefundedAmount { get; set; }
        }

        public class Provided
        {
            public Card card { get; set; }
        }

        public class Redirect
        {
            public Customized customized { get; set; }
        }

        public class Response
        {
            public string gatewayCode { get; set; }
            public string gatewayRecommendation { get; set; }
        }

        public class Root
        {
            public Authentication authentication { get; set; }
            public string correlationId { get; set; }
            public string merchant { get; set; }
            public Order order { get; set; }
            public Response response { get; set; }
            public string result { get; set; }
            public SourceOfFunds sourceOfFunds { get; set; }
            public DateTime timeOfLastUpdate { get; set; }
            public DateTime timeOfRecord { get; set; }
            public Transaction transaction { get; set; }
            public string version { get; set; }
        }

        public class SourceOfFunds
        {
            public Provided provided { get; set; }
            public string type { get; set; }
        }

        public class Transaction
        {
            public int amount { get; set; }
            public string authenticationStatus { get; set; }
            public string currency { get; set; }
            public string id { get; set; }
            public string type { get; set; }
        }
    }
}
