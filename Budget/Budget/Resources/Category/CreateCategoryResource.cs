using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Budget.Resources.Category
{
    public class CreateCategoryResource
    {
        [Required]
        [MaxLength(100)]
        public string Icon { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

    }
}
