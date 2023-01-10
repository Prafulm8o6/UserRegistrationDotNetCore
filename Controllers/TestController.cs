using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserRegistrationDotNetCore.Data;
using UserRegistrationDotNetCore.GenericRepo;
using UserRegistrationDotNetCore.Helper;
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
        private readonly IGenericRepo<RoomType> _genericRepo;

        public TestController(DataContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IGenericRepo<RoomType> genericRepo)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _genericRepo = genericRepo;
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
            var user = await _context.Users.Where(x => x.Id == Id).SingleOrDefaultAsync();
            var userRole = await _context.UserRoles.Where(x => x.UserId == Id).Select(y => y.RoleId).ToListAsync();
            var userInClaims = await _context.UserClaims.Where(x => x.UserId == Id).Select(y => y.ClaimValue).ToListAsync();
            
            vm.AppRoles = await _roleManager.Roles.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id,
                Selected = userRole.Contains(x.Id)
            }).ToListAsync();
            vm.AppUser = user;

            vm.AppClaims = ClaimStore.All.Select(x => new SelectListItem()
            {
                Text = x.Type,
                Value = x.Value,
                Selected = userInClaims.Contains(x.Value)
            }).ToList();
            
            return View(vm);
        }

        [HttpPost]
        public IActionResult Edit(ManageUserRole vm)
        {
            var selectedRoleId = vm.AppRoles.Where(x => x.Selected).Select(y => y.Value).ToList();
            var aleradyExistRoleId = _context.UserRoles.Where(x => x.UserId == vm.AppUser.Id).Select(y => y.RoleId).ToList();
            var toAddRole = selectedRoleId.Except(aleradyExistRoleId);
            var toRemoveRole = aleradyExistRoleId.Except(selectedRoleId);

            foreach (var item in toRemoveRole)
            {
                _context.UserRoles.Remove(new IdentityUserRole<string> { 
                    RoleId = item,
                    UserId = vm.AppUser.Id
                });
            }
            foreach (var item in toAddRole)
            {
                _context.UserRoles.Add(new IdentityUserRole<string> { 
                    RoleId = item,
                    UserId = vm.AppUser.Id
                });
            }

            var selectedClaimId = vm.AppClaims.Where(x => x.Selected).Select(y => y.Value).ToList();
            var aleradyExistClaimId = _context.UserClaims.Where(x => x.UserId == vm.AppUser.Id).Select(y => y.Id.ToString()).ToList();
            var toAddClaim = selectedClaimId.Except(aleradyExistClaimId);
            var toRemoveClaim = aleradyExistClaimId.Except(selectedClaimId);

            foreach (var item in toRemoveClaim)
            {
                _context.UserClaims.Remove(new IdentityUserClaim<string>
                {
                    Id = int.Parse(item),
                    UserId = vm.AppUser.Id
                });
            }

            foreach (var item in toAddClaim)
            {
                _context.UserClaims.Add(new IdentityUserClaim<string>
                {
                    ClaimType = item,
                    ClaimValue = item,
                    UserId = vm.AppUser.Id
                });
            }

            _context.SaveChanges();
            return RedirectToAction("Index","Test");
        }

        [HttpGet]
        public IActionResult GenericRepoGetAll()
        {
            var listOfRoomType = _genericRepo.GetAll();
            return View(listOfRoomType);
        }

        [HttpGet]
        public IActionResult GenericRepoGetById(int Id)
        {
            var listOfRoomType = _genericRepo.GetById(Id);
            return View(listOfRoomType);
        }

        public async Task<IActionResult> SearchUserAsync(int pageNumber = 1, string searchUser = null)
        {
            var searchResult = _context.Users.AsNoTracking();
            ViewBag.searchUser = searchUser;
            if (String.IsNullOrEmpty(searchUser))
            {
                searchResult = _context.Users.Where(x => x.FirstName.StartsWith(searchUser) || x.LastName.StartsWith(searchUser) || x.Email.StartsWith(searchUser) || x.UserName.StartsWith(searchUser)).AsNoTracking();
                if (searchResult.Count() == 0)
                {
                    ViewBag.users = "Search User Not Found";
                    return View();
                }
                return View(searchResult);
            }

            int totalPages = 3;

            return View(await PaginatedList<ApplicationUser>.CreateAsync(searchResult,pageNumber,totalPages));
        }
    }
}
