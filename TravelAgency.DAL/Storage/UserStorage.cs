using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using TravelAgency.Domain.ModelsDb;

namespace TravelAgency.DAL.Storage
{
    public class UserStorage : IBaseStorage<UsersDb>
    {
        public readonly ApplicationDbContext _db;

        public UserStorage(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task Add(UsersDb item)
        {
            await _db.AddAsync(item);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(UsersDb item)
        {
            _db.Remove(item);
            await _db.SaveChangesAsync();
        }

        public async Task<UsersDb> Get(Guid id)
        {
            return await _db.UsersDb.FirstOrDefaultAsync(x => x.Id == id );
        }

        public IQueryable<UsersDb> GetAll()
        {
            return _db.UsersDb;
        }

        public async Task<UsersDb> Update(UsersDb item)
        {
            _db.UsersDb.Update(item);
            await _db.SaveChangesAsync();

            return item;

        }

    }
}
