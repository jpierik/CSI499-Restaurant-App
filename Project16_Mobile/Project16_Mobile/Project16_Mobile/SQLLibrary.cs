using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using System.IO;

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
        public string TestConnection(string username, string password, string proccess)
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
                return responseString;
            }
            catch (Exception ex)
            {
                return "Timeout";
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
