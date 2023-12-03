using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ASM1670.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [DisplayName("Display Order of Category")]
        [Range(1, 100)]
        public int DisplayOrder { get; set; }
    }
}
