
namespace Domain.Model
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class TypeBusiness
    {
        [Key]
        public int TypeBusinessId { get; set; }
        [Display(Name ="Type")]
        [MaxLength(15,ErrorMessage = "The filed {0} you must enter maximum in characteres {1}")]
        [Required(ErrorMessage ="This filed is required")]
        public string Type { get; set; }

        public virtual ICollection<Business> Business { get; set; }
    }
}
