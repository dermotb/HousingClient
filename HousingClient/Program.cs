
namespace Housing
{
    using Housing.Models;
    using System.Net.Http.Headers;
    using System.Net.Http.Json;

    public class Program
    {
        static void Main(string[] args)
        {
            GetAllHouses().Wait();
        }

        private static async Task GetAllHouses()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://housingead.azurewebsites.net/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("api/House");

            if (response.IsSuccessStatusCode) 
            {
                IEnumerable<Property> houses = await response.Content.ReadFromJsonAsync<IEnumerable<Property>>();

                foreach(Property p in houses)
                {
                    Console.WriteLine(p.Address.ToString());
                }
            }
            else
            {
                Console.WriteLine(response.StatusCode+" "+response.ReasonPhrase);
            }
        }
    }
}