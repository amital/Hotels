//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Payoneer.Payoneer.Hotels.Model.HotelsDomain
{
    using System;
    using System.Collections.Generic;
    
    public partial class Reservation
    {
        public int ReservationId { get; set; }
        public int RoomId { get; set; }
        public System.DateTime ReservedFrom { get; set; }
        public System.DateTime ReservedTo { get; set; }
        public int CustomerId { get; set; }
    
        public virtual Customer Customer { get; set; }
        public virtual Room Room { get; set; }
    }
}
