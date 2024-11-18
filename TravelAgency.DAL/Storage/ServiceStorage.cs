using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.DAL.Interfaces;
using TravelAgency.Domain.ModelsDb;

namespace TravelAgency.DAL.Storage
{
    public class ServiceStorage : IBaseStorage<ServicesDb>
    {
        public readonly ApplicationDbContext _db;

        public ServiceStorage(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task Add(ServicesDb item)
        {
            await _db.AddAsync(item);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(ServicesDb item)
        {
            _db.Remove(item);
            await _db.SaveChangesAsync();
        }

        public async Task<ServicesDb> Get(Guid id)
        {
            return await _db.ServiceDb.FirstOrDefaultAsync(x => x.Id == id);
        }

        public IQueryable<ServicesDb> GetAll()
        {
            return _db.ServiceDb;
        }

        public async Task<ServicesDb> Update(ServicesDb item)
        {
            _db.ServiceDb.Update(item);
            await _db.SaveChangesAsync();

            return item;

        }
    }
}
