using System;
using System.Collections.Generic;
using System.Text;

namespace Project16_Mobile
{

    public class Restaurant
    {
        public int RestaurantId { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }
        public int NoOfTables { get; set; }
        public int OwnerId { get; set; }
        public int CurrentWait { get; set; }
    }
}
