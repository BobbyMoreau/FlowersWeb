using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace flowers.web.Models
{
    public class FlowerModel : BaseModel
    {
        public string Color { get; set; }   
        public int Height { get; set; }
        public int FamilyId { get; set; }
        public string ImageUrl { get; set; }

        [ForeignKey("FamilyId")]
        
        public FamilyModel Family { get; set; }
    }
}