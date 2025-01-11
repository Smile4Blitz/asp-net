using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NewsItems.Model
{
    public class NewsItem
    {
        [JsonIgnore]
        public int? Id { get; set; }
        
        [Required]
        public string Title { get; set; }
        
        [Required]
        public string Message { get; set; }
        
        [Required]
        public DateTime DateTime { get; set; }
    }
}
