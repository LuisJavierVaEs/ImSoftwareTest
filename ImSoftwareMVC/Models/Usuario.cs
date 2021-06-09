using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ImSoftwareMVC.Models
{
    public class Usuario
    {
        public int id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public int Edad { get; set; }

        [Required]
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
