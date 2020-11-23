using Authors.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Authors.Repository
{
    public class Repository<T> : IRepository<T>, IDisposable where T : BaseEntity
    {
        private readonly ApplicationContext _context;
        private DbSet<T> entities;
        private bool disposed = false;

        public Repository(ApplicationContext context)
        {
            _context = context;
            entities = context.Set<T>();
        }

        public List<T> GetAll()
        {
            return entities.ToList();
        }

        public T Get(Guid id)
        {
            return entities.SingleOrDefault(s => s.Id == id);
        }

        public bool Exists(Guid Id)
        {
            if (Id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(Id));
            }

            return entities.Any(a => a.Id == Id);
        }

        public void Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _context.SaveChanges();
        }

        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            _context.SaveChanges();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
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

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}