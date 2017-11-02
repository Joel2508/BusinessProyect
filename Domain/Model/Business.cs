namespace Domain.Model
{
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Business
    {
        [Key]
        public int BusinessId { get; set; }

        public int TypeBusinessId { get; set; }

        [DataType(DataType.ImageUrl)]
        public string Image { get; set; }

        [Display(Name ="Name")]
        [Required(ErrorMessage ="The filed {0} is required")]
        [MaxLength(15,ErrorMessage = "The filed {0} you must enter maximum in character is")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The filed {0} is required")]
        [MaxLength(20, ErrorMessage = "The filed {0} you must enter maximum in character is")]
        [Index("Index_PhoneBusiness_RNC",Order =1, IsUnique =true)]
        public string RNC { get; set; }

        [Required(ErrorMessage = "The filed {0} is required")]
        [MaxLength(20, ErrorMessage = "The filed {0} you must enter maximum in character is")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "The filed {0} is required")]
        [MaxLength(20, ErrorMessage = "The filed {0} you must enter maximum in character is")]
        [Index("Index_PhoneBusiness_RNC", Order =2, IsUnique = true)]
        [Display(Name = "Phone Business")]
        public string PhoneBusiness { get; set; }

        [Required(ErrorMessage = "The filed {0} is required")]
        [MaxLength(20, ErrorMessage = "The filed {0} you must enter maximum in character is")]
        public string Latitude { get; set; }

        [Required(ErrorMessage = "The filed {0} is required")]
        [MaxLength(20, ErrorMessage = "The filed {0} you must enter maximum in character is")]
        public string Lengthe { get; set; }

        [JsonIgnore]
        public virtual TypeBusiness TypeBusiness { get; set; }
    }
}
