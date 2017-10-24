using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataAccess;

namespace InLineWebApi.Controllers
{
    public class RestaurantController : ApiController
    {
        public IEnumerable<Restaurant> Get()
        {
            using (RestaurantEntities entities = new RestaurantEntities())
            {
                return entities.Restaurants.ToList();
            }
        }
        public Restaurant Get(int id)
        {
            using (RestaurantEntities entities = new RestaurantEntities())
            {
                return entities.Restaurants.FirstOrDefault(e => e.RestaurantId == id);
            }
        }

    }
}
