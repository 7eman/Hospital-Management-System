﻿using Hospital.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Repositories.Implementation
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }
        private bool disposed=false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
                
        }
        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

       public IGenericRepository<T> GenericRepository<T>() where T:class
        {
             IGenericRepository<T> repo=new GenericRepository<T>(_context);
            return repo;
        }

        void IUnitOfWork.Save()
        {
           _context.SaveChanges();
        }
    }
}
