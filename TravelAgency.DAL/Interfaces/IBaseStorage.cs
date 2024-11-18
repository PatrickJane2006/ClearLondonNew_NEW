﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.DAL.Interfaces
{
    public interface IBaseStorage<T>
    {
        Task Add(T item);

        Task Delete(T item);

        Task<T> Get(Guid item);

        Task<T> Update(T item);

        IQueryable<T> GetAll();

    }
}
