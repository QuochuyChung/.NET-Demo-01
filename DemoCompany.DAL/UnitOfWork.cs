using DemoCompany.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCompany.DAL
{
    public class UnitOfWork : IDisposable
    {
        private readonly DemoCompanyContext _context;

        public UnitOfWork()
        {
            _context = new DemoCompanyContext(); 
        }

        public void Dispose()
        {
            _context.Dispose(); 
        }

        public int SaveChanges()
        {
            return _context.SaveChanges(); 
        }

    }
}
