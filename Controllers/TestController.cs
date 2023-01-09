using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserRegistrationDotNetCore.Data;
using UserRegistrationDotNetCore.Models;
using UserRegistrationDotNetCore.ViewModel;
using UserRegistrationDotNetCore.ViewModels;

namespace UserRegistrationDotNetCore.Controllers
{
    public class TestController : Controller
    {
        private readonly DataContext _context;
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public TestController(DataContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var users = _context.Users.ToList();
            return View(users);
        }
        
        [HttpGet]
        public async Task<IActionResult> Edit(string Id)
        {
            ManageUserRole vm = new ManageUserRole();
            var user = await _context.Users.Where(x => x.Id == Id).FirstOrDefaultAsync();
            var userRole = await _context.UserRoles.Where(x => x.UserId == Id).Select(y => y.RoleId).ToListAsync();
            var userInClaims = await _context.UserClaims.Where(x => x.UserId == Id).Select(y => y.ClaimValue).ToListAsync();
            vm.Roles = await _context.Roles.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id,
                Selected = userRole.Contains(x.Id)
            }).ToListAsync();
            vm.AppUser = user;
            vm.ApplicationClaims = ClaimStore.All.Select(x => new SelectListItem()
            {
                Text = x.Type,
                Value = x.Value,
                Selected = userInClaims.Contains(x.Value)
            });;
            return View(vm);
        }

        [HttpPost]
        public IActionResult Edit(ManageUserRole vm)
        {
            var selectedRoleId = vm.Roles.Where(x => x.Selected).Select(y => y.Value).ToList();
            var aleradyExistRoleId = _context.UserRoles.Where(x => x.UserId == vm.AppUser.Id).Select(y => y.RoleId).ToList();
            var toAdd = selectedRoleId.Except(aleradyExistRoleId);
            var toRemove = aleradyExistRoleId.Except(selectedRoleId);

            foreach (var item in toRemove)
            {
                _context.UserRoles.Remove(new IdentityUserRole<string> { 
                    RoleId = item,
                    UserId = vm.AppUser.Id
                });
            }
            foreach (var item in toAdd)
            {
                _context.UserRoles.Add(new IdentityUserRole<string> { 
                    RoleId = item,
                    UserId = vm.AppUser.Id
                });
            }

            _context.SaveChanges();
            return RedirectToAction("Index","Test");
        }
    }
}
