using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserRegistrationDotNetCore.Data;

namespace UserRegistrationDotNetCore.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly DataContext _context;

        public EmployeeController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();  
        }


    }
}
