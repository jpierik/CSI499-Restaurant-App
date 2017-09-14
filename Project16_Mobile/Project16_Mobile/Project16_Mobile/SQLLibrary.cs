using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Json;
using System.Threading.Tasks;
using System.IO;

namespace Project16_Mobile
{
    class SQLLibrary
    {
        private static SQLLibrary instance;

        public static SQLLibrary getInstance()
        {
            if(instance == null)
            {
                instance = new SQLLibrary();
            }
            return instance;
        }
        private SQLLibrary()
        {

        }
        //POST api/SQL?username={username}&password={password}
        public async Task<JsonValue> TestConnection()
        {
            string url = "http://sqlloginapi.azurewebsites.net/api/SQL?username=" + "test1" + "&password=" + "test1" + "";
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
            request.ContentType = "application/json";
            request.Method = "PUT";
            using (WebResponse response = await request.GetResponseAsync())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    // Use this stream to build a JSON document object:
                    JsonValue jsonDoc = await Task.Run(() => JsonObject.Load(stream));
                    Console.Out.WriteLine("Response: {0}", jsonDoc.ToString
                   ());
                    // Return the JSON document:
                    return jsonDoc;
                }
            
            }

        }
        /*
        private void ParseAndDisplay(JsonValue json)
        {
            JsonValue login = json["Login"];
            usern
            
        }
   */
        
       
    }
}
