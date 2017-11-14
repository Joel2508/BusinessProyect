namespace Domain.Model
{
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System;

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
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

                

        [Required(ErrorMessage = "The filed {0} is required")]
        [MaxLength(100, ErrorMessage = "The filed {0} you must enter maximum in character is")]
        public string Address { get; set; }

        public decimal Latituded { get; set; }

        public decimal Longitud { get; set; }

        [JsonIgnore]
        public virtual TypeBusiness TypeBusiness { get; set; }            
    }
}
