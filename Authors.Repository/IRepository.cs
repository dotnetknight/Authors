using Authors.Domain;
using System;
using System.Collections.Generic;

namespace Authors.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        List<T> GetAll();
        T Get(Guid id);
        bool Exists(Guid Id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        void SaveChanges();
    }
}
