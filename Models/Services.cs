using System.ComponentModel.DataAnnotations;

namespace FitnesClub.Models
{
    class services
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Название")]
        [StringLength(255)]
        public string Name { get; set; }

        [Display(Name = "Спецификация")]
        [StringLength(255)]
        public string Specifications { get; set; }

        [Display(Name = "Рабочий график")]
        [StringLength(255)]
        public string WorkSchedule { get; set; }

        [Display(Name = "Количество человек на услугу")]
        public int PeopleCount { get; set; }

        [Display(Name = "Стоимость")]
        [StringLength(50)]
        public string Cost { get; set; }
    }
}