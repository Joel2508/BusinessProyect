
namespace API.Responses
{
    using System.Collections.Generic;

    public class TypeBusinessResponse
    {
        public int TypeBusinessId { get; set; }

        public string Type { get; set; }

        public  List<BusinessResponse> Business { get; set; }
    }
}