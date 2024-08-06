using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProjectEcommerce.Models
{
    public class CategoryModel
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        [MaxLength(30)]
        [DisplayName("Category Name")]
        public string CategoryName { get; set; }
        [Required]
        [DisplayName("Category Description")]
        
        public string CategoryDescription { get; set; }
        [DisplayName("Display Order")]
        [Range(1, 100)]
        public int DisplayOrder {  get; set; }
    }
}
