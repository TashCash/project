using System.ComponentModel.DataAnnotations;

namespace FitnesClub.Models
{
    public class admins
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Логин")]
        [StringLength(30)]
        public string Login { get; set; }

        [Display(Name = "Пароль")]
        [StringLength(30)]
        public string Password { get; set; }
    }
}