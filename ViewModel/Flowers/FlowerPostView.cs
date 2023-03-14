using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json.Serialization;

namespace flowers.web.ViewModel.Flowers
{
    public class FlowerPostView : BaseViewModel
    {
        [Required(ErrorMessage = "You have to write the flowers color")]
        [JsonPropertyName("color")]
        public string Color { get; set; }  
        [Required(ErrorMessage = "You have to write the flowers height")] 
        [JsonPropertyName("height")]
        public int Height { get; set; }

        [Required(ErrorMessage = "You have to choose the flowers family")]
        [JsonPropertyName("familyId")]
        public int FamilyId { get; set;}
        [JsonPropertyName("family")]
        public string Family { get; set; }
        public List<SelectListItem> Families { get; set;}

    }
}