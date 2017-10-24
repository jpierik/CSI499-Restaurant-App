using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Json;
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
        private SQLLibrary()
        {

        }
        //POST api/SQL?username={username}&password={password}
        public bool Login(string username, string password)
        {
            try
            {
                string url = "141.210.25.6/InLineWebApi/api/login";
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
                request.ContentType = "application/json";
                request.Method = "GET";
                string responseString;
                using (WebResponse response = request.GetResponse())
                {
                    responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                }
                if (responseString.Contains("SUCCESS"))
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public bool Register(int rID, string username, int level, string password, string email)
        {
            try
            {
                string url = "141.210.25.6/InLineWebApi/api/login";
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
                request.ContentType = "application/json";
                request.Method = "POST";
                string message = "{\"RestaurantID\":\"" + rID + "\", \"username\":\"" + username + "\", \"alevel\":\"" + level + "\", \"pwd\":\"" + password + "\", \"email\":\"" + email + "\" }";

                HttpRequestMessage reqMessage = new HttpRequestMessage(HttpMethod.Post, "relativeAddress");
                reqMessage.Content = new StringContent(message, Encoding.UTF8, "application/json");

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                client.SendAsync(reqMessage).ContinueWith(responseTask =>
                {
                    Console.WriteLine("Resonse: {0}", responseTask.Result);
                });

              
                    return true;
            }
            catch (Exception ex)
            {
                return false;
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


    }
}
