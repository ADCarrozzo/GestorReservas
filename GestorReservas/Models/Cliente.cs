using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace GestorReservas.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            Reserva = new HashSet<Reserva>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe ingresar un nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Debe ingresar un apellido")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "Debe ingresar un mail")]
        [EmailAddress(ErrorMessage = "Ingrese un mail valido")]
        public string Mail { get; set; }

        [Required(ErrorMessage = "Debe ingresar un telefono")]
        [Phone(ErrorMessage ="ingrese un telefono valido")]
        public string Telefono { get; set; }

        public virtual ICollection<Reserva> Reserva { get; set; }
    }
}
