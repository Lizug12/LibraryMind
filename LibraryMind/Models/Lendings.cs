
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryMind.Models
{
    public class Lendings
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Выберите читателя")]
        [Display(Name = "Читатель")]
        public int ReaderID { get; set; }

        [Required(ErrorMessage = "Ввыберите книгу")]
        [Display(Name = "Книга")]
        public int BookID { get; set; }

        [Required(ErrorMessage = "Введите дату выдачи")]
        [Display(Name = "Дата выдачи")]
        [DataType(DataType.Date)]
        public DateTime LendingDate { get; set; }

        [Required(ErrorMessage = "Введите дату возврата")]
        [Display(Name = "Дата возврата")]
        [DataType(DataType.Date)]
        public DateTime ReturnDate { get; set; }

        public Books Book { get; set; }
        public Readers Reader { get; set; }
    }
}
