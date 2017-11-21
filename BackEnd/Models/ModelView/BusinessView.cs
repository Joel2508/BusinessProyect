namespace BackEnd.Models.ModelView
{
    using Domain.Model;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web;

    [NotMapped]
    public class BusinessView : Business
    {
        public HttpPostedFileBase ImageView { get; set; }
        public float LatitudedView { get; set; }

        public float LongitudView { get; set; }
    }
}