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
        public IEnumerable<MobileUser> Get()
        {
            using (MobileUserEntities entities = new MobileUserEntities())
            {
                return entities.MobileUsers.ToList();
            }
        }
        public MobileUser Get(int id)
        {
            using (MobileUserEntities entities = new MobileUserEntities())
            {
                return entities.MobileUsers.FirstOrDefault(e => e.UserId == id);
            }
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
                    email = user.email,
                    pwd = user.pwd
                });
                ctx.SaveChanges();
            }
            return Ok();
        }
        public IHttpActionResult Put(MobileUser user)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            using (var ctx = new MobileUserEntities())
            {
                var existingUser = ctx.MobileUsers.Where(s => s.UserId == user.UserId)
                                                        .FirstOrDefault<MobileUser>();

                if (existingUser != null)
                {
                    existingUser.FullName = user.FullName;
                    existingUser.email = user.email;
                    ctx.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            }

            return Ok();
        }

    }
}
