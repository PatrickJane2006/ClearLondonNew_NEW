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
    public class OrderStorage : IBaseStorage<OrdersDb>
    {
        public readonly ApplicationDbContext _db;

        public OrderStorage(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task Add(OrdersDb item)
        {
            await _db.AddAsync(item);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(OrdersDb item)
        {
            _db.Remove(item);
            await _db.SaveChangesAsync();
        }

        public async Task<OrdersDb> Get(Guid id)
        {
            return await _db.OrdersDb.FirstOrDefaultAsync(x => x.Id == id);
        }

        public IQueryable<OrdersDb> GetAll()
        {
            return _db.OrdersDb;
        }

        public async Task<OrdersDb> Update(OrdersDb item)
        {
            _db.OrdersDb.Update(item);
            await _db.SaveChangesAsync();

            return item;

        }



    }
}
