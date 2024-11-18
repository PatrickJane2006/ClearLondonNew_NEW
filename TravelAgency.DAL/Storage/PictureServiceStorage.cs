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
    public class PictureServiceStorage : IBaseStorage<Picture_servicesDb>
    {
        public readonly ApplicationDbContext _db;

        public PictureServiceStorage(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task Add(Picture_servicesDb item)
        {
            await _db.AddAsync(item);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(Picture_servicesDb item)
        {
            _db.Remove(item);
            await _db.SaveChangesAsync();
        }

        public async Task<Picture_servicesDb> Get(Guid id)
        {
            return await _db.Picture_servicesDb.FirstOrDefaultAsync(x => x.Id == id);
        }

        public IQueryable<Picture_servicesDb> GetAll()
        {
            return _db.Picture_servicesDb;
        }

        public async Task<Picture_servicesDb> Update(Picture_servicesDb item)
        {
            _db.Picture_servicesDb.Update(item);
            await _db.SaveChangesAsync();

            return item;

        }



    }
}
