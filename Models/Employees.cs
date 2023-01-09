using System.ComponentModel.DataAnnotations;

namespace FitnesClub.Models
{
    class employees
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Инициалы")]
        [StringLength(255)]
        public string Initials { get; set; }

        [Display(Name = "Опыт работы")]
        public int WorkExperience { get; set; }

        [Display(Name = "Когда начал работать")]
        [StringLength(30)]
        public string StartWorkingDate { get; set; }

        [Display(Name = "Зарплата")]
        [StringLength(50)]
        public string Salary { get; set; }

        [Display(Name = "Премии")]
        [StringLength(255)]
        public string Awards { get; set; }
    }
}
