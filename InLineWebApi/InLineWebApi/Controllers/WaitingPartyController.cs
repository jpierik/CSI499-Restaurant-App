using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataAccess;

namespace InLineWebApi.Controllers
{
    public class WaitingPartyController : ApiController
    {
        public IEnumerable<WaitingParty> Get()
        {
            using (WaitingPartyModel entities = new WaitingPartyModel())
            {
                return entities.WaitingParties.ToList();
            }
        }
        public WaitingParty Get(int id)
        {
            using (WaitingPartyModel entities = new WaitingPartyModel())
            {
                return entities.WaitingParties.FirstOrDefault(e => e.PartyId == id);
            }
        }
        public IHttpActionResult Post(WaitingParty party)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");
            using (var ctx = new WaitingPartyModel())
            {
                ctx.WaitingParties.Add(new WaitingParty()
                {
                    PartyId = party.PartyId,
                    RestaurantID = party.RestaurantID,
                    NoOfGuests = party.NoOfGuests,
                    AddTime = party.AddTime,
                    PriorityLvl = party.PriorityLvl,
                    FullName = party.FullName,
                    MobileUserId = party.MobileUserId
                   
                });
                ctx.SaveChanges();
            }
            return Ok();
        }      
    }
}
