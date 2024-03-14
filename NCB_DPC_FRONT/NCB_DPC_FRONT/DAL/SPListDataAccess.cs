using System;
using System.Collections.Generic;
using Microsoft.SharePoint.Client;


namespace NCB_DPC_FRONT.DAL
{
    public class SPListDataAccess
    {
        private System.Net.NetworkCredential spCredentials;
        private string sharepointURL;
        private string spUserName;
        private string spPassword;
        private string spDomain;

        public SPListDataAccess()
        {
            sharepointURL = "http://gvp-stg-app19:9005/dpc";
            spUserName = "administrator";
            spPassword = "Pa$$w0rd1";
            spDomain = "PORTALPREPROD";

            spCredentials = new System.Net.NetworkCredential(spUserName, spPassword, spDomain);
        }

        public SPListDataAccess(System.Net.NetworkCredential spCredentials, string sharepointURL)
        {
            this.spCredentials = spCredentials;
            this.sharepointURL = sharepointURL;
        }

        public List<string> GetCountries()

        {
            List<string> Titles = new List<string>();
            // var sharepointURL = "http://sp2019:8007/";

            try
            {
                using (ClientContext clientContext = new ClientContext(sharepointURL))
                {
                    //clientContext.Credentials = spCredentials;
                    clientContext.Credentials = spCredentials;



                    List targetList = clientContext.Web.Lists.GetByTitle("Countries");

                    CamlQuery oQuery = CamlQuery.CreateAllItemsQuery();

                    Microsoft.SharePoint.Client.ListItemCollection oCollection = targetList.GetItems(oQuery);
                    clientContext.Load(oCollection);
                    clientContext.ExecuteQuery();
                   

                    foreach (Microsoft.SharePoint.Client.ListItem I in oCollection)
                    {
                        Titles.Add(I["Title"].ToString());
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace + e.Message);
            }

            return Titles;


        }

        public List<string> GetTitles()
        {

            List<string> Titles = new List<string>();

            try
            {
                using (ClientContext clientContext = new ClientContext(sharepointURL))
                {
                    clientContext.Credentials = spCredentials;
                    List targetList = clientContext.Web.Lists.GetByTitle("Titles");

                    CamlQuery oQuery = CamlQuery.CreateAllItemsQuery();

                    Microsoft.SharePoint.Client.ListItemCollection oCollection = targetList.GetItems(oQuery);
                    clientContext.Load(oCollection);
                    clientContext.ExecuteQuery();

                    foreach (Microsoft.SharePoint.Client.ListItem I in oCollection)
                    {
                        Titles.Add(I["Title"].ToString());
                    }
                }
            } 

            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace + e.Message);

            }


            return Titles;

        }


    }
}
