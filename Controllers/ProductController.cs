using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserRegistrationDotNetCore.Data;

namespace UserRegistrationDotNetCore.Controllers
{
    public class ProductController : Controller
    {
        private readonly DataContext _context;

        public ProductController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index(string searchProductName)
        {
            if (String.IsNullOrEmpty(searchProductName))
            {
                return View();  
            }
            var searchResult = _context.Products.Where(x => x.Title.Contains(searchProductName)).ToList();
            if (searchResult.Count == 0)
            {
                ViewBag.products = "Search Product Not Found";
                return View();
            }
            return View(searchResult);
        }
    }
}
