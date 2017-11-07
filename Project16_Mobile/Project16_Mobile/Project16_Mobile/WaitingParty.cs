using System;
using System.Collections.Generic;
using System.Text;

namespace Project16_Mobile
{
    public class WaitingParty
    {
        public int PartyId { get; set; }
        public int RestaurantID { get; set; }
        public int NoOfGuests { get; set; }
        public DateTime? AddTime { get; set; }
        public bool? PriorityLvl { get; set; }
        public string FullName { get; set; }
        public int MobileUserId { get; set; }

    }
}
