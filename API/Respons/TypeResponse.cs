
namespace API.Respons
{
    using Domain.Model;
    using System.Collections.Generic;

    public class TypeResponse
    {
        public int TypeBusinessId { get; set; }

        public string Type { get; set; }

        public List<Business> Business { get; set; }
    }
}