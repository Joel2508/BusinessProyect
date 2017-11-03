
namespace BusinessProyect.Models
{
    using System.Collections.Generic;

    public class TypeBusiness
    {
        public int TypeBusinessId { get; set; }
        public string Type { get; set; }
        public List<Businesss> Businesss { get; set; }
    }
}
