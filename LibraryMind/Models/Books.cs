using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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

        //public ICollection<Enrollment> Enrollments { get; set; }
    }
}
