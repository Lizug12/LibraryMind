
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace LibraryMind.Models
{
    public class Books
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Введите название книги")]
        [Display(Name = "Название книги")]
        public string BookName { get; set; }

        [Required(ErrorMessage = "Введите автора")]
        [Display(Name = "Автор")]
        public string Author { get; set; }

        [Required(ErrorMessage = "Введите жанр")]
        [Display(Name = "Жанр")]
        public string Genre { get; set; }

        [Required(ErrorMessage = "Введите количество страниц")]
        [Display(Name = "Количество страниц")]
        public int Pages { get; set; }

        [Required(ErrorMessage = "Введите дату издания")]
        [Display(Name = "Дата издания")]
        [DataType(DataType.Date)]
        public DateTime YearPublishing { get; set; }

        public ICollection<Lendings> Lending { get; set; }
    }
}
