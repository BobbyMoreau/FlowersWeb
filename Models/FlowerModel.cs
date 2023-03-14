using System.ComponentModel.DataAnnotations.Schema;

namespace flowers.web.Models
{
    public class FlowerModel : BaseModel
    {
        public string Color { get; set; }   
        public int Height { get; set; }
        public int FamilyId { get; set; }
        public string ImageUrl { get; set; }

       [ForeignKey("FamilyId")]
        
       public FamilyModel Families { get; set; }
    }
}