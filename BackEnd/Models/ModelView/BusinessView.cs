namespace BackEnd.Models.ModelView
{
    using Domain.Model;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web;

    [NotMapped]
    public class BusinessView : Business
    {
        public HttpPostedFileBase ImageView { get; set; }
        public string LatitudView { get; set; }
        public string LogintudView { get; set; }
    }
}