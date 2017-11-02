
namespace Domain.Context
{
    using System.Data.Entity;

    public class ContextDomain : DbContext
    {
        public ContextDomain() : base("DefaultConnection")
        {

        }

        public System.Data.Entity.DbSet<Domain.Model.TypeBusiness> TypeBusinesses { get; set; }

        public System.Data.Entity.DbSet<Domain.Model.Business> Businesses { get; set; }
    }
}
