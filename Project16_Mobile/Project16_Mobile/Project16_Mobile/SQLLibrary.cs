using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace Project16_Mobile
{
    class SQLLibrary
    {
        private static SQLLibrary instance;

        public static SQLLibrary getInstance()
        {
            if (instance == null)
            {
                instance = new SQLLibrary();
            }
            return instance;
        }
        CurrentLocation mCurrentLocation;
        User mUser;
        private SQLLibrary()
        {
            mCurrentLocation = new CurrentLocation();
        }
        Random random = new Random();
        public int GetNextRandom()
        {
            return random.Next(0, 9);
        }

        public struct CurrentLocation
        {
            public double Latitude { get; set; }
            public double Longitude { get; set; }
        }; 
        public User GetUser()
        {
            return mUser;
        }
        public void SetUser(User user)
        {
            mUser = user;
        }
        public void SetCurrentLocation(double lati, double longi)
        {
            mCurrentLocation.Latitude = lati;
            mCurrentLocation.Longitude = longi;
        }
        public CurrentLocation GetCurrentLocation()
        {
            return mCurrentLocation;
        }
        //POST api/SQL?username={username}&password={password}
        public User Login(string username, string password)
        {
            try
            {
                string url = "http://141.210.25.6/InLineWebApi/api/login";
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
                request.ContentType = "application/json";
                request.Method = "GET";
                string responseString;
                using (WebResponse response = request.GetResponse())
                {
                    responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                    Console.Write(responseString);
                }

                //console write that stuff up there ^, then copy result to notepadd++, then go to json converter.
                //after parsing check if list exists and check username and pass. if(list.exists(i => i.shitToCheck = stuff)) 
                var array = JArray.Parse(responseString);
                List<User> list = new List<User>();
                foreach (var item in array)
                {
                    try
                    {
                        list.Add(item.ToObject<User>());
                    }
                    catch (Exception ex)
                    {
                        InvalidJsonElements = InvalidJsonElements ?? new List<string>();
                        InvalidJsonElements.Add(item.ToString());
                    }
                }
                foreach(User u in list)
                {
                    if(u.email == username && u.pwd == password)
                    {
                        return u;
                    }
                }
                return null;

            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public async Task<bool> Register(string fullname, string email, string password)
        {
            bool mFlag = false;
            try
            { // string testUrl = "http://10.0.0.183/api/mobileuser";
              string url = "http://141.210.25.6/InLineWebApi/api/login";
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
                request.ContentType = "application/json";
                request.Method = "POST";
                string message = "{\"FullName\":\"" + fullname + "\", \"email\":\"" + email +  "\", \"pwd\":\"" + password + "\" }";

                HttpRequestMessage reqMessage = new HttpRequestMessage(HttpMethod.Post, url);
                reqMessage.Content = new StringContent(message, Encoding.UTF8, "application/json");

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                await client.SendAsync(reqMessage).ContinueWith(responseTask =>
                {
                    Console.WriteLine("Resonse: {0}", responseTask.Result);
                    if(responseTask.Result.StatusCode == HttpStatusCode.OK)
                    {
                        mFlag = true;
                    }
                });

                return mFlag;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> UpdateUser(int mID, string fullname, string email)
        {
            bool mFlag = false;
            try
            {
                string url = "http://141.210.25.6/InLineWebApi/api/login";
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
                request.ContentType = "application/json";
                request.Method = "PUT";
                string message = "{\"UserId\":" + mID + ", \"FullName\":\"" + fullname + "\", \"email\":\"" + email + "\" }";

                HttpRequestMessage reqMessage = new HttpRequestMessage(HttpMethod.Put, url);
                reqMessage.Content = new StringContent(message, Encoding.UTF8, "application/json");

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                await client.SendAsync(reqMessage).ContinueWith(responseTask =>
                {
                    Console.WriteLine("Resonse: {0}", responseTask.Result);
                    if (responseTask.Result.StatusCode == HttpStatusCode.OK)
                    {
                        mFlag = true;
                    }
                    return mFlag;
                });
                return mFlag;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> InsertWaitingParty(int restID, int partyNum, string fullName, int mID )
        {
            bool mFlag = false;
            try
            {
                string url = "http://141.210.25.6/InLineWebApi/api/waitingparty";
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
                request.ContentType = "application/json";
                request.Method = "POST";
                string message = "{\"RestaurantID\":\"" + restID + "\", \"NoOfGuests\":\"" + partyNum + "\", \"AddTime\":\"" + DateTime.Now.ToString() + "\", \"PriorityLvl\":" + 0 + ", \"FullName\":\"" + fullName + "\", \"MobileUserId\":\"" + mID + "\" }";

                HttpRequestMessage reqMessage = new HttpRequestMessage(HttpMethod.Post, url);
                reqMessage.Content = new StringContent(message, Encoding.UTF8, "application/json");

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                await client.SendAsync(reqMessage).ContinueWith(responseTask =>
                {
                    Console.WriteLine("Resonse: {0}", responseTask.Result);
                     if (responseTask.Result.StatusCode == HttpStatusCode.OK)
                    {
                        mFlag = true;
                    }
                    return mFlag;
                });
                return mFlag;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> UpdateWaitingParty(int pID)
        {
            bool mFlag = false;
            try
            {
                string url = "http://141.210.25.6/InLineWebApi/api/waitingparty";
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
                request.ContentType = "application/json";
                request.Method = "PUT";
                string message = "{\"PartyId\":\"" + pID + "\", \"PriorityLvl\":" + 1 + "\" }";

                HttpRequestMessage reqMessage = new HttpRequestMessage(HttpMethod.Put, url);
                reqMessage.Content = new StringContent(message, Encoding.UTF8, "application/json");

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                await client.SendAsync(reqMessage).ContinueWith(responseTask =>
                {
                    Console.WriteLine("Resonse: {0}", responseTask.Result);
                    if (responseTask.Result.StatusCode == HttpStatusCode.OK)
                    {
                        mFlag = true;
                    }
                    return mFlag;
                });
                return mFlag;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> RemoveWaitingParty(int pID)
        {
            bool mFlag = false;
            try
            {
                string url = "http://141.210.25.6/InLineWebApi/api/waitingparty?id=" + pID;
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
                request.ContentType = "application/json";
                request.Method = "DELETE";
                string message = "";

                HttpRequestMessage reqMessage = new HttpRequestMessage(HttpMethod.Delete, url);
                reqMessage.Content = new StringContent(message, Encoding.UTF8, "application/json");

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                await client.SendAsync(reqMessage).ContinueWith(responseTask =>
                {
                    Console.WriteLine("Resonse: {0}", responseTask.Result);
                    if (responseTask.Result.StatusCode == HttpStatusCode.OK)
                    {
                        mFlag = true;
                    }
                    return mFlag;
                });
                return mFlag;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public List<WaitingParty> GetWaitingParties()
        {
            InvalidJsonElements = null;
            try
            {
                string url = "http://141.210.25.6/InLineWebApi/api/waitingparty";
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
                request.ContentType = "application/json";
                request.Method = "GET";
                string responseString;
                using (WebResponse response = request.GetResponse())
                {
                    responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                }
                Console.WriteLine(responseString);
                // copy this shit down here and replace restaurant with user
                var array = JArray.Parse(responseString);
                List<WaitingParty> list = new List<WaitingParty>();
                foreach (var item in array)
                {
                    try
                    {
                        list.Add(item.ToObject<WaitingParty>());
                    }
                    catch (Exception ex)
                    {
                        InvalidJsonElements = InvalidJsonElements ?? new List<string>();
                        InvalidJsonElements.Add(item.ToString());
                    }
                }
                return list;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static List<string> InvalidJsonElements;
        public List<Restaurant> GetRestaurants()
        {
            InvalidJsonElements = null;
            try
            {
                string url = "http://141.210.25.6/InLineWebApi/api/restaurant";
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
                request.ContentType = "application/json";
                request.Method = "GET";
                string responseString;
                using (WebResponse response = request.GetResponse())
                {
                    responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                }
                Console.WriteLine(responseString);
                // copy this shit down here and replace restaurant with user
                var array = JArray.Parse(responseString);
                List<Restaurant> list = new List<Restaurant>();
                foreach(var item in array)
                {
                    try
                    {
                        list.Add(item.ToObject<Restaurant>());
                    }
                    catch(Exception ex)
                    {
                        InvalidJsonElements = InvalidJsonElements ?? new List<string>();
                        InvalidJsonElements.Add(item.ToString());
                    }
                }
                return list;
            
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public Restaurant GetRestaurant(int id)
        {
            InvalidJsonElements = null;
            try
            {
                string url = "http://141.210.25.6/InLineWebApi/api/restaurant";
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
                request.ContentType = "application/json";
                request.Method = "GET";
                string responseString;
                using (WebResponse response = request.GetResponse())
                {
                    responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                }
                Console.WriteLine(responseString);
                // copy this shit down here and replace restaurant with user
                var array = JArray.Parse(responseString);
                List<Restaurant> list = new List<Restaurant>();
                foreach (var item in array)
                {
                    try
                    {
                        list.Add(item.ToObject<Restaurant>());
                    }
                    catch (Exception ex)
                    {
                        InvalidJsonElements = InvalidJsonElements ?? new List<string>();
                        InvalidJsonElements.Add(item.ToString());
                    }
                }
                foreach(Restaurant r in list)
                {
                    if(r.RestaurantId == id)
                    {
                        return r;
                    }
                }
                return null;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public WeatherObject GetWeather(string zipcode)
        {
            try
            {
                string url = "http://api.openweathermap.org/data/2.5/weather?zip=" + zipcode + ",us&units=imperial";
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
                request.ContentType = "application/json";
                request.Method = "GET";
                string responseString;
                request.Headers.Add("x-api-key", "333b72fbdb1a3223348929ba8b979fe4");
                using (WebResponse response = request.GetResponse())
                {
                    responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                }
                Console.WriteLine(responseString);
                // copy this shit down here and replace restaurant with user
                var jobject = JObject.Parse(responseString);
                WeatherObject weather = null;
                try
                {
                    weather = jobject.ToObject<WeatherObject>();
                }
                catch (Exception ex)
                {
                    InvalidJsonElements = InvalidJsonElements ?? new List<string>();
                    InvalidJsonElements.Add(jobject.ToString());
                }

                return weather;
            }catch(Exception ex)
            {
                return null;
            }
        }

        /*public List<string[]> getRestaurantInfo(string restaurant, string addresss, int distance, int tables, int waitTime)
        {
            try
            {
                string url = "141.210.25.6/InLineWebApi/api/Restaurant";
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
                request.ContentType = "application/json";
                request.Method = "GET";
                string restaurantString;
                using (WebResponse response = request.GetResponse())
                {
                    responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                }
                return responseString;
            }
            catch
            {
                return restaurantList;
            }
        }
        
          public void reservationInfo(int restaurantId, int partyId, int partySize)
          {
            try
            {
                string url = "141.210.25.6/InLineWebApi/api/WaitingParty";
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
                request.ContentType = "application/json";
                request.Method = "POST";
                using (WebResponse response = request.GetResponse())
                {
                    responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                }
                return responseString;
            }
          }*/

        /*
        private void ParseAndDisplay(JsonValue json)
        {
            JsonValue login = json["Login"];
            usern
            
        }
   */

        public List<Deal> GetDeals()
        {
            InvalidJsonElements = null;
            try
            {
                string url = "http://141.210.25.6/InLineWebApi/api/deals";
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
                request.ContentType = "application/json";
                request.Method = "GET";
                string responseString;
                using (WebResponse response = request.GetResponse())
                {
                    responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                }
                var array = JArray.Parse(responseString);
                List<Deal> list = new List<Deal>();
                foreach (var item in array)
                {
                    try
                    {
                        list.Add(item.ToObject<Deal>());
                    }
                    catch (Exception ex)
                    {
                        InvalidJsonElements = InvalidJsonElements ?? new List<string>();
                        InvalidJsonElements.Add(item.ToString());
                    }
                }
                return list;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<Deal> GetDeals(int id)
        {
            InvalidJsonElements = null;
            try
            {
                string url = "http://141.210.25.6/InLineWebApi/api/deals";
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
                request.ContentType = "application/json";
                request.Method = "GET";
                string responseString;
                using (WebResponse response = request.GetResponse())
                {
                    responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                }
                Console.WriteLine(responseString);
                var array = JArray.Parse(responseString);
                List<Deal> list = new List<Deal>();
                foreach (var item in array)
                {
                    try
                    {
                        list.Add(item.ToObject<Deal>());
                    }
                    catch (Exception ex)
                    {
                        InvalidJsonElements = InvalidJsonElements ?? new List<string>();
                        InvalidJsonElements.Add(item.ToString());
                        return null;
                    }
                }
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].RestaurantId != id)
                        list.RemoveAt(i);
                }
                return list;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
