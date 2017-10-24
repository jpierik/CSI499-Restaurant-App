using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataAccess;

namespace InLineWebApi.Controllers
{
    public class LoginController : ApiController
    {
        public IEnumerable<User> Get()
        {
            using (UserModel entities = new UserModel())
            {
                return entities.Users.ToList();
            }
        }
        public User Get(int id)
        {
            using (UserModel entities = new UserModel())
            {
                return entities.Users.FirstOrDefault(e => e.UserId == id);
            }
        }
        public IHttpActionResult Post(User user)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");
            using (var ctx = new UserModel())
            {
                ctx.Users.Add(new User()
                {
                    RestaurantID = user.RestaurantID,
                    username = user.username,
                    alevel = user.alevel,
                    pwd = user.pwd,
                    email = user.email

                });
                ctx.SaveChanges();
            }
            return Ok();
        }

    }
}
