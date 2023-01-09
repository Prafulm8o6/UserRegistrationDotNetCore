using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserRegistrationDotNetCore.Repo;

namespace UserRegistrationDotNetCore.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployee _repository;

        public EmployeeController(IEmployee repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            var employees = _repository.GetAll();
            return View(employees);
        }
    }
}
