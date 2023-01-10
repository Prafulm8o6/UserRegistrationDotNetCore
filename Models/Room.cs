using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserRegistrationDotNetCore.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int MaxAdults { get; set; }
        public int MaxChilds { get; set; }
        public decimal RoomFare { get; set; }
        public string Description { get; set; }
        public int NoOfBeds { get; set; }
        public string RoomPictureUri { get; set; }
        public RoomType TypeOfRoom { get; set; }
        public int RoomTypeId { get; set; }
        public RoomFacility FacilityOfRoom { get; set; }

    }
}
