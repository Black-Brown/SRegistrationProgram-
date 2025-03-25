using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SRegisterApp.Models
{
    public class Sections
    {
        public int ID { get; set; }

        [Required]
        public string Nombre { get; set; }

        public int ProfesorID { get; set; }
        [ForeignKey("ProfesorID")]
        public Professors? Profesor { get; set; }

        public ICollection<StudentSection>? StudentSections { get; set; }
    }
}
