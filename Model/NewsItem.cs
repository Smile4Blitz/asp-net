using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NewsItems.Model
{
    public class NewsItem
    {
        public int? Id { get; set; }
        
        [Required]
        public string Title { get; set; }
        
        [Required]
        public string Message { get; set; }
        
        [Required]
        public DateTime DateTime { get; set; }
    }
}
