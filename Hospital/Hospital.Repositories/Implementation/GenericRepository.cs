using Hospital.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Hospital.Repositories;

namespace Hospital.Repositories.Implementation
{
    public class GenericRepository<T> : IDisposable, IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        internal DbSet<T> dbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            dbSet = _context.Set<T>();
        }
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        //public Task<T> AddAsync(T entity)
        //{
        //    dbSet.Add(entity);
        //    return entity;
        //}

        public void Delete(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
        }

        //public Task DeleteAsync(T entity)
        //{
        //    if (_context.Entry(entity).State == EntityState.Detached)
        //    {
        //        dbSet.Attach(entity);
        //    }
        //    dbSet.Remove(entity);
        //    return entity;
        //}
        public bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool disposing)
        {
            if (!this.disposed) {
                if (disposing)
                {
                    _context.Dispose();
                }
            
            }
            this.disposed = true;

        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> fillter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
        {
            IQueryable<T> query = dbSet;
            if(fillter != null)
            {
                query=query.Where(fillter);
            }
            foreach (var includeProperty in includeProperties.Split(new char[] {',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query=query.Include(includeProperty);
            }
            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public T GetById(object id)
        {
            return dbSet.Find(id);
        }

        public async Task<T> GetByIdAsync(object id)
        {
            return await dbSet.FindAsync(id);
        }

        public void Update(T entity)
        {
             dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        //public Task UpdateAsync(T entity)
        //{

        //    dbSet.Attach(entity);
        //    _context.Entry(entity).State = EntityState.Modified;
        //    return entity;
        //}
    }
}
