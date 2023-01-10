using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserRegistrationDotNetCore.Data;
using UserRegistrationDotNetCore.ViewModel;

namespace UserRegistrationDotNetCore.Controllers
{
    public class RoomController : Controller
    {
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public RoomController(DataContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var rooms = _context.Rooms.ToList();
            return View(rooms);
        }

        [HttpGet]
        public IActionResult Create()
        {
            //ViewBag.RoomTypes = _context.RoomTypes.Select(x => new SelectListItem()
            //{
            //    Text = x.Title,
            //    Value = x.Id.ToString()
            //}).ToList(); 
            //ViewBag.Facilities = _context.Facilities.Select(x => new SelectListItem()
            //{
            //    Text = x.Title,
            //    Value = x.Id.ToString()
            //}).ToList();
            var roomTypes = _context.RoomTypes.Select(x => new SelectListItem() { Text = x.Title, Value = x.Id.ToString() }).ToList();
            var facilites = _context.Facilities.Select(x => new SelectListItem() { Text = x.Title, Value = x.Id.ToString() }).ToList();
            RoomViewModel vm = new RoomViewModel();
            vm.TypesOfRoom = roomTypes;
            vm.RoomFacilities = facilites;
            return View(vm);
        }

        [HttpPost]
        public IActionResult Create(RoomViewModel vm)
        {

            return View(vm);
        }
    }
}
