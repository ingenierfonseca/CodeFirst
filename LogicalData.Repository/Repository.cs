using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalData.Repository
{
    public class Repository<T> where T: class
    {
        public ApplicationDbContext db = new ApplicationDbContext();
        public DbSet<T> DbSet { get; set; }

        public Repository()
        {
            DbSet = db.Set<T>();
        }

        public void Insert(T t)
        {
            DbSet.Add(t);
            db.SaveChanges();
        }

        public void Update(T t)
        {
            db.Entry(t).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(T t)
        {
            db.Entry(t).State = EntityState.Deleted;
            db.SaveChanges();
        }

        public T Get(int id)
        {
            return DbSet.Find(id);
        }

        public T Get(string id)
        {
            return DbSet.Find(id);
        }

        public List<T> GetAll()
        {
            return DbSet.ToList();
        }
    }
}
