using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserRegistrationDotNetCore.Models;

namespace UserRegistrationDotNetCore.ViewModels
{
    public class ManageUserRole
    {
        public ApplicationUser AppUser { get; set; }
        public List<SelectListItem> Roles { get; set; }
    }
}
