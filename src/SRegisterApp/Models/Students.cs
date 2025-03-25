namespace SRegisterApp.Models
{
    public class Students
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Matricula { get; set; }

        // Relación: Un estudiante puede tener muchas secciones
        public ICollection<StudentSection> StudentSections { get; set; } = new List<StudentSection>();
    }
}
