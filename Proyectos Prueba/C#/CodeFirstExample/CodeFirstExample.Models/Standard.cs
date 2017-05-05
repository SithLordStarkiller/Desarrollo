namespace CodeFirstExample.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Standard
    {
        public Standard()
        {

        }

        [Key]
        public int StandardId { get; set; }
        [Required]
        [MaxLength(100)]
        public string StandardName { get; set; }

        public ICollection<Student> Students { get; set; }

    }
}