using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace GestorReservas.Models
{
    public partial class Mesa
    {
        public Mesa()
        {
            Reserva = new HashSet<Reserva>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe ingresar un número valido")]
        [Range(minimum: 0, maximum: 20)]
        [Display(Name ="Número")]
        public string Numero { get; set; }

        [Required(ErrorMessage = "Debe ingresar un número mayor o igual a cero")]
        [Range(minimum: 0, maximum: 9999999)]
        [Display(Name ="Precio de seña")]
        public decimal PrecioSeña { get; set; }

        public virtual ICollection<Reserva> Reserva { get; set; }
    }
}
