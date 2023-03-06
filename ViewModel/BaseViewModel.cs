
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace flowers.web.ViewModel
{
    public class BaseViewModel
    {
        [Required(ErrorMessage = "You have to write a name")]
        [DisplayName("Name")] [JsonPropertyName("name")]
        public string Name {get; set;}

        

        
       
    }
}