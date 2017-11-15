namespace API.Responses
{
    public class BusinessResponse
    {
        public int BusinessId { get; set; }

        public int TypeBusinessId { get; set; }

        public string Image { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public decimal Latituded { get; set; }

        public decimal Longitud { get; set; }
    }
}