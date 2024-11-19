using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Domain.Enum;

namespace TravelAgency.Domain.Response
{
    public class BaseResponce<T> : IBaseResponce<T>
    {
        public string Description { get; set; }

        public StatusCode StatusCode { get; set; }

        public T Data { get; set; }

    }

    public interface IBaseResponce<T>
    {
        T Data { get; set; }
    }

    public enum StatusCode
    {
        OK = 200,
        BadRequest = 400,
        NotFound = 404,
        InternalServerError = 500,
    }


}
