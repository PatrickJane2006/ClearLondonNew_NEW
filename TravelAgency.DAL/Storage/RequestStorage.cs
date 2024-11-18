using System;
using System.Linq;
using System.Threading.Tasks;
using TravelAgency.DAL.Interfaces;
using TravelAgency.Domain.ModelsDb;
using Microsoft.EntityFrameworkCore;

namespace TravelAgency.DAL.Storage
{
    public class RequestStorage : IBaseStorage<RequestsDb>
    {
        public readonly ApplicationDbContext _db;

        public RequestStorage(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task Add(RequestsDb item)
        {
            await _db.AddAsync(item);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(RequestsDb item)
        {
            _db.Remove(item);
            await _db.SaveChangesAsync();
        }

        public async Task<RequestsDb> Get(Guid id)
        {
            return await _db.RequestsDb.FirstOrDefaultAsync(x => x.Id == id);
        }

        public IQueryable<RequestsDb> GetAll()
        {
            return _db.RequestsDb;
        }

        public async Task<RequestsDb> Update(RequestsDb item)
        {
            _db.RequestsDb.Update(item);
            await _db.SaveChangesAsync();

            return item;

        }


    }
}
