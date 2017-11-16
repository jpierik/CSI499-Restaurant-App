using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Web.Http;
using DataAccess;

namespace InLineWebApi.Controllers
{
    public class DealsController : ApiController
    {
        public IEnumerable<Deal> Get()
        {
            using (DealsModel entities = new DealsModel())
            {
                return entities.Deals.ToList();
            }
        }
        public Deal Get(int id)
        {
            using (DealsModel entities = new DealsModel())
            {
                return entities.Deals.FirstOrDefault(e => e.DealId == id);
            }
        }

    }
}