using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SQLApi.Models;
using System.Data.Sql;
using System.Data.SqlClient;

namespace SQLApi.Controllers
{
    public class SQLController : ApiController
    {

        LoginEntities csi4999 = new LoginEntities();              

        [HttpGet]
        [ActionName("PROJECT_16_LOGIN")]
        public HttpResponseMessage Project_16_Login(string username, string password, string process)
        {
            if (process == "LOGIN")
            {
                Login user = csi4999.Logins.Where(x => x.Username == username && x.Password == password).FirstOrDefault();
                if (user == null)
                {
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, "Please Enter a valid Username and Password");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Accepted, "Success");
                }
            }
            else
            {
                try
                {
                    SqlConnection sqlConnection = new SqlConnection();
                    sqlConnection.ConnectionString = "Server = tcp:csi4999.database.windows.net,1433;" +
                                                     "Initial Catalog = CSI4999;" +
                                                     "Persist Security Info = False;" +
                                                     "User ID = csi4999;" +
                                                     "Password = Temp1234;" +
                                                     "MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30;";
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO Logins (Username, Password) VALUES (@val1, @val2);", sqlConnection);
                    cmd.Parameters.AddWithValue("@val1", username);
                    cmd.Parameters.AddWithValue("@val2", password);
                    cmd.ExecuteNonQuery();
                    return Request.CreateResponse(HttpStatusCode.Accepted, "Successfully Created");
                }
                catch (Exception ex)
                {
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, "error submittings");
                }
            }
        }

    
    }
}
