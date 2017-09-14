using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SQLApi.Models;

namespace SQLApi.Controllers
{
    public class SQLController : ApiController
    {

        LoginEntities csi4999 = new LoginEntities();


        [HttpPost]
        [ActionName("PROJECT_16_REGISTER")]
        
        public HttpResponseMessage Project_16_Register(string username, string password)
        {
            
            Login login = new Login();
            login.Username = username;
            login.Password = password;
            csi4999.Logins.Add(login);
            csi4999.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.Accepted, "Succefully Created");
            
        }

        [HttpGet]
        [ActionName("PROJECT_16_LOGIN")]
        public HttpResponseMessage Project_16_Login(string username, string password)
        {

            Login user = csi4999.Logins.Where(x => x.Username == username && x.Password == password).FirstOrDefault();
            if(user == null)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "Please Enter a valid Username and Password");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Accepted, user.Username.ToString() + " " +  user.Password.ToString());
            }
        }

    
    }
}
