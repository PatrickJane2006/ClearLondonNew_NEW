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
    public class CountryStorage : IBaseStorage<CountriesDb>
    {
        public readonly ApplicationDbContext _db;

        public CountryStorage(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task Add(CountriesDb item)
        {
            await _db.AddAsync(item);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(CountriesDb item)
        {
            _db.Remove(item);
            await _db.SaveChangesAsync();
        }

        public async Task<CountriesDb> Get(Guid id)
        {
            return await _db.CountriesDb.FirstOrDefaultAsync(x => x.Id == id);
        }

        public IQueryable<CountriesDb> GetAll()
        {
            return _db.CountriesDb;
        }

        public async Task<CountriesDb> Update(CountriesDb item)
        {
            _db.CountriesDb.Update(item);
            await _db.SaveChangesAsync();

            return item;

        }


    }
}
