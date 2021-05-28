using System.ComponentModel.DataAnnotations;

namespace Budget.Resources.Category
{
    public class EditCategoryResource
    {
        [Required]
        public int id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Icon { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
