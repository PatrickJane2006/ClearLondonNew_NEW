using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TravelAgency.Domain.ModelsDb;


namespace TravelAgency.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<UsersDb> UsersDb { get; set; }

        public DbSet<ServicesDb> ServiceDb { get; set; }

        public DbSet<CountriesDb> CountriesDb { get; set; }

        public DbSet<OrdersDb> OrdersDb { get; set; }

        public DbSet<RequestsDb> RequestsDb { get; set; }

        public DbSet<Picture_servicesDb> Picture_servicesDb { get; set; }


        protected readonly IConfiguration Configuration;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            


        }


    }
}
