using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataAccess;

namespace InLineWebApi.Controllers
{
    public class MobileUserController : ApiController
    {
        public IEnumerable<MobileUser> Get()
        {
            using (MobileUserEntities entities = new MobileUserEntities())
            {
                return entities.MobileUsers.ToList();
            }
        }
        public MobileUser Get(string email)
        {
            using (MobileUserEntities entities = new MobileUserEntities())
            
                return entities.MobileUsers.FirstOrDefault(e => e.email == email);
            }
        
        public IHttpActionResult Post(MobileUser user)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");
            using (var ctx = new MobileUserEntities())
            {
                ctx.MobileUsers.Add(new MobileUser()
                {
                    UserId = user.UserId,
                    FullName = user.FullName,
                    pwd = user.pwd,
                    email = user.email

                });
                ctx.SaveChanges();
            }
            return Ok();
        }

    }
}
