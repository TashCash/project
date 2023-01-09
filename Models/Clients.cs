using System.ComponentModel.DataAnnotations;

namespace FitnesClub.Models
{
    class clients
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Логин")]
        [StringLength(30)]
        public string Login { get; set; }

        [Display(Name = "Пароль")]
        [StringLength(30)]
        public string Password { get; set; }

        [Display(Name = "Инициалы")]
        [StringLength(255)]
        public string Initials { get; set; }

        [Display(Name = "Возраст")]
        public int Age { get; set; }

        [Display(Name = "Адрес")]
        [StringLength(255)]
        public string Address { get; set; }

        [Display(Name = "Телефон")]
        [StringLength(20)]
        public string Phone { get; set; }
    }
}
