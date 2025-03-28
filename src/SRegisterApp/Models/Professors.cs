﻿using static System.Collections.Specialized.BitVector32;

namespace SRegisterApp.Models
{
    public class Professors
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }


        // Relación: Un profesor puede tener muchas secciones
        public List<Sections>? Sections { get; set; }

    }
}