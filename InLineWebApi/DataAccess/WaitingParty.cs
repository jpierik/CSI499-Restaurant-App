//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class WaitingParty
    {
        public int PartyId { get; set; }
        public int RestaurantID { get; set; }
        public int NoOfGuests { get; set; }
        public System.DateTime AddTime { get; set; }
        public bool PriorityLvl { get; set; }
        public string FullName { get; set; }
        public Nullable<int> MobileUserId { get; set; }
    }
}
