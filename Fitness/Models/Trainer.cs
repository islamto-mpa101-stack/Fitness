using Fitness.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace Fitness.Models
{
    public class Trainer : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Profession { get; set; }
        [Required]
        public string ImagePath { get; set; }
    }
}
