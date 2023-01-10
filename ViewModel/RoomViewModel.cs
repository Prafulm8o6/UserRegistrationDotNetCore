using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserRegistrationDotNetCore.Models;

namespace UserRegistrationDotNetCore.ViewModel
{
    public class RoomViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int MaxAdults { get; set; }
        public int MaxChilds { get; set; }
        public decimal RoomFare { get; set; }
        public string Description { get; set; }
        public int NoOfBeds { get; set; }
        public IFormFile RoomPictureUri { get; set; }
        public int RoomTypeId { get; set; }
        public IList<SelectListItem> TypesOfRoom { get; set; }
        public ICollection<SelectListItem> RoomFacilities { get; set; }
    }
}
