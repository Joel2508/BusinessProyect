
namespace Domain.Context
{
    using Domain.Model;
    using System.Data.Entity;

    public class ContextDomain : DbContext
    {
        public ContextDomain() : base("DefaultConnection")
        {

        }
        public DbSet<TypeBusiness> TypeBusinesses { get; set; }

        public DbSet<Business> Businesses { get; set; }
        public DbSet<Customer> Customers { get; set; }

    }
}
