using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImSoftwareApi.Entidades
{
    public class Usuario
    {
        public int id { get; set; }
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public string Email { get; set; }

        public Usuario() { }

        public Usuario(int id, string Nombre, int Edad, string Email)
        {
            this.id = id;
            this.Nombre = Nombre;
            this.Edad = Edad;
            this.Email = Email;
        }
    }
}
