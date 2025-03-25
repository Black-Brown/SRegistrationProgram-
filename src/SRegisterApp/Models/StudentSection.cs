using static System.Collections.Specialized.BitVector32;

namespace SRegisterApp.Models
{
    public class StudentSection
    {
        public int ID { get; set; }

        // Claves foráneas
        public int StudentID { get; set; }
        public Students Student { get; set; }

        public int SectionID { get; set; }
        public Sections Section { get; set; }
    }
}
