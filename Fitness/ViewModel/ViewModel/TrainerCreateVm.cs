using System.ComponentModel.DataAnnotations;

namespace Fitness.ViewModel.TeacherViwModel
{
    public class TrainerCreateVm
    {
        [Required]
        [MaxLength(256)]
        public string Name { get; set; }
        [Required]
        [MaxLength(256)]
        public string Profession { get; set; }
        public IFormFile Image { get; set; }

    }
}
