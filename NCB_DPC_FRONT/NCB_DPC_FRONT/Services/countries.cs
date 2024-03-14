using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace NCB_DPC_FRONT.Services
{
    public class countries
    {
        public class Country
        {
            public string Name { get; set; }
            // Add any other properties you need from the API response
        }


        public List<string> getCountries()
        {
            var httpClient = new HttpClient();
            var response = httpClient.GetAsync("https://restcountries.eu/rest/v2/all").Result;

            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                var serializer = new JavaScriptSerializer();
                var countries = serializer.Deserialize<Country[]>(content);

                List<string> countryNames = new List<string>();
                foreach (var country in countries)
                {
                    countryNames.Add(country.Name);
                }

                return countryNames;
            }
            else
            {
                Console.WriteLine("Failed to retrieve country data from API.");
                return new List<string>();  // Return an empty list if API request fails
            }



        }

    }
}
