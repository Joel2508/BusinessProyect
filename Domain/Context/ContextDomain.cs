
namespace Domain.Context
{
    using System.Data.Entity;

    public class ContextDomain : DbContext
    {
        public ContextDomain() : base("DefaultConnection")
        {

        }

        public DbSet<Model.TypeBusiness> TypeBusinesses { get; set; }

        public DbSet<Model.Business> Businesses { get; set; }
    }
}
