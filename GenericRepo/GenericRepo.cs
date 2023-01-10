using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserRegistrationDotNetCore.Data;
using UserRegistrationDotNetCore.GenericRepo;

namespace UserRegistrationDotNetCore.GenericRepo
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        private readonly DataContext _context;
        private readonly DbSet<T> entities;

        public GenericRepo(DataContext context)
        {
            _context = context;
            this.entities = _context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return entities.AsEnumerable();
        }

        public T GetById(int Id)
        {
            return entities.Find(Id);
        }
    }
}
