using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Foodies.Models;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace FoodiesApp
{
    public class ServiceModule
    {
        private static ServiceModule _ServiceModuleInstance;
        public static ServiceModule ServiceModuleInstance
        {
            get
            {
                if (_ServiceModuleInstance == null)
                    _ServiceModuleInstance = new ServiceModule();
                return _ServiceModuleInstance;
            }
        }

        private JsonSerializer _serializer = new JsonSerializer();
        private HttpClient httpClient;
        string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Exceptions.txt");
        public ServiceModule()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://xamfoodiesapi.azurewebsites.net/");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<User> LoginAsync(Login login)
        {
            try
            {
                var responseContent = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");
                var res = await httpClient.PostAsync("Login", responseContent);
                res.EnsureSuccessStatusCode();
                using (var stream = await res.Content.ReadAsStreamAsync())
                using (var reader = new StreamReader(stream))
                using (var json = new JsonTextReader(reader))
                {
                    var jsoncontent = _serializer.Deserialize<User>(json);
                    return jsoncontent;
                }
            }
            catch (Exception ex)
            {
                File.WriteAllText(fileName, "Exception in login : " + ex);
                return null;
            }
        }

        public async Task<List<Restaurant>> GetRestaurantsAsync()
        {
            try
            {
                var responseContent = await httpClient.GetStringAsync("api/Restaurants");
                var res = JsonConvert.DeserializeObject<List<Restaurant>>(responseContent);
                return res;
            }
            catch (Exception ex)
            {
                File.WriteAllText(fileName, "Exception in get restaurant : " + ex);
                return null;
            }
        }

        public async Task<Restaurant> PostRestaurantAsync(string displayName, string address, int priceForTwo, int adminId)
        {
            try
            {
                Restaurant resturantModel = new Restaurant()
                {
                    displayName = displayName,
                    address = address,
                    priceForTwo = priceForTwo,
                    adminId = adminId
                };
                var responseContent = new StringContent(JsonConvert.SerializeObject(resturantModel), Encoding.UTF8, "application/json");
                var res = await httpClient.PostAsync("api/Restaurants", responseContent);
                res.EnsureSuccessStatusCode();
                using (var stream = await res.Content.ReadAsStreamAsync())
                using (var reader = new StreamReader(stream))
                using (var json = new JsonTextReader(reader))
                {
                    var jsoncontent = _serializer.Deserialize<Restaurant>(json);
                    return jsoncontent;
                }
            }
            catch (Exception ex)
            {
                File.WriteAllText(fileName, "Exception in post restaurant : " + ex);
                return null;
            }
        }

        public async Task<Restaurant> UpdateRestaurantAsync(Restaurant restaurant)
        {
            try
            {
                var responseContent = new StringContent(JsonConvert.SerializeObject(restaurant), Encoding.UTF8, "application/json");
                var res = await httpClient.PutAsync("api/Restaurants", responseContent);
                res.EnsureSuccessStatusCode();
                using (var stream = await res.Content.ReadAsStreamAsync())
                using (var reader = new StreamReader(stream))
                using (var json = new JsonTextReader(reader))
                {
                    var jsoncontent = _serializer.Deserialize<Restaurant>(json);
                    return jsoncontent;
                }
            }
            catch (Exception ex)
            {
                File.WriteAllText(fileName, "Exception in update restaurant : " + ex);
                return null;
            }
        }

        public async Task<bool> DeleteRestaurantAsync(int id)
        {
            try
            {
                var res = await httpClient.DeleteAsync($"api/Restaurants/{id}");
                res.EnsureSuccessStatusCode();
                return true;
            }
            catch (Exception ex)
            {
                File.WriteAllText(fileName, "Exception in delete restaurant : " + ex);
                return false;
            }
        }
    }
}

