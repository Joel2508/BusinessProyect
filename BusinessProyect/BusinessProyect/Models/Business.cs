namespace BusinessProyect.Models
{
    public class Business
    {
        public int BusinessId { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public decimal Latituded { get; set; }
        public decimal Longitud { get; set; }

        public string FullImage
        {
            get
            {
                return string.Format("http://www.xtudiaconstructor.somee.com/{0}", Image.Substring(1));
            }
        }
    }
}
