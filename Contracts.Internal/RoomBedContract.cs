﻿using System.ComponentModel.DataAnnotations;

namespace Payoneer.Payoneer.Hotels.Contracts
{
    public class RoomBedContract
    {
        public int RoomId { get; set; }
        public int BedId { get; set; }
        [Required]
        public short BedType { get; set; }
    }
}
