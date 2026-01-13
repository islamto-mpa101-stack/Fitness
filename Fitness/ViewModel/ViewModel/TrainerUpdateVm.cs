using System.ComponentModel.DataAnnotations;

namespace Fitness.ViewModel.TeacherViwModel
{
    public class TrainerUpdateVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Profession { get; set; }
        public IFormFile? Image { get; set; }
    }
}
