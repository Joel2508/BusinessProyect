
namespace API.Respons
{
    using Domain.Model;
    using System.Collections.Generic;

    public class BusinesResponse
    {
        public int BusinessId { get; set; }

        public int TypeBusinessId { get; set; }

        public string Image { get; set; }

        public string Name { get; set; }

        public string RNC { get; set; }

        public string Email { get; set; }

        public string PhoneBusiness { get; set; }

        public string Latitude { get; set; }

        public string Lengthe { get; set; }
    }
}