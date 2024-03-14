using Microsoft.SharePoint.Client;
using NCB_DPC_FRONT.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace NCB_DPC_FRONT.DAL
{
    public class SPListsDataAccess
    {
        private System.Net.NetworkCredential spCredentials;
        private string sharepointURL;
        private string spUserName;
        private string spPassword;
        private string spDomain;

        public SPListsDataAccess()
        {
            sharepointURL = GlobalVar.DPCSharePointURL;
            spUserName = GlobalVar.SPUserName;
            spPassword = GlobalVar.SPPassword;
            spDomain = GlobalVar.SPDomain;

            spCredentials = new System.Net.NetworkCredential(spUserName, spPassword, spDomain);
        }

        public SPListsDataAccess(System.Net.NetworkCredential spCredentials, string sharepointURL)
        {
            this.spCredentials = spCredentials;
            this.sharepointURL = sharepointURL;
        }

        /// Get all titles
        public List<string> GetTitles()
        {
            using (ClientContext clientContext = new ClientContext(sharepointURL))
            {
                clientContext.Credentials = spCredentials;
                List targetList = clientContext.Web.Lists.GetByTitle("Titles");

                CamlQuery oQuery = CamlQuery.CreateAllItemsQuery();

                Microsoft.SharePoint.Client.ListItemCollection oCollection = targetList.GetItems(oQuery);
                clientContext.Load(oCollection);
                clientContext.ExecuteQuery();
                List<string> Titles = new List<string>();

                foreach (Microsoft.SharePoint.Client.ListItem I in oCollection)
                {
                    Titles.Add(I["Title"].ToString());
                }
                return Titles;
            }
        }
    }
}
