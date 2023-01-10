using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserRegistrationDotNetCore.GenericRepo
{
    public interface IGenericRepo<T> where T: class
    {
        IEnumerable<T> GetAll();
        T GetById(int Id);
    }
}
