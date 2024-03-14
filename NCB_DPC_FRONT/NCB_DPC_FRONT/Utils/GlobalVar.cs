using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCB_DPC_FRONT.Utils
{
    public static class GlobalVar
    {
        public static readonly string DPCSharePointURL = ConfigurationManager.AppSettings["DPCSharePointURL"];
        public static readonly string SPUserName = ConfigurationManager.AppSettings["spUserName"];
        public static readonly string SPPassword = ConfigurationManager.AppSettings["spPassword"];
        public static readonly string SPDomain = ConfigurationManager.AppSettings["spDomain"];
        public static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["MauritiusDBEservices"].ConnectionString;

        public static readonly string WorkspaceUrl = ConfigurationManager.AppSettings["WorkspaceUrl"];
        public static readonly string HomePageUrl = ConfigurationManager.AppSettings["HomePageUrl"];
        public static readonly string DPCUrl = ConfigurationManager.AppSettings["DPC_Url"];

        public static readonly string DPCMerchant = ConfigurationManager.AppSettings["DPC_Merchant"];
        public static readonly string DPCMerchantKey = ConfigurationManager.AppSettings["DPC_Merchant_Key"];


    }
}
