using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryMind.Models
{
    public class Readers
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Введите ФИО")]
        [Display(Name = "ФИО")]
        public string FIO { get; set; }

        [Required(ErrorMessage = "Введите адрес")]
        [Display(Name = "Адрес")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Введите телефон")]
        [Display(Name = "Телефон")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Введите возраст")]
        [Display(Name = "Возраст")]
        public int Age { get; set; }

        public ICollection<Lendings> Lending { get; set; }
    }
}
