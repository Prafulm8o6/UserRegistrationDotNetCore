using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserRegistrationDotNetCore.Models;

namespace UserRegistrationDotNetCore.Repo
{
    public interface IEmployee
    {
        IEnumerable<Employee> GetAll();
        Employee GetById(int Id);
    }
}
