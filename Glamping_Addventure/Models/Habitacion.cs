using System;
using System.Collections.Generic;

namespace Glamping_Addventure.Models;

public partial class Habitacion
{
    public int Idhabitacion { get; set; }

    public string? NombreHabitacion { get; set; }

    public byte[]? ImagenHabitacion { get; set; }

    public string? Descripcion { get; set; }

    public double? Costo { get; set; }

    public int? IdtipodeHabitacion { get; set; }

    public int? CantidadPersona { get; set; }

    public bool Estado { get; set; }

    public virtual ICollection<HabitacionMueble> HabitacionMuebles { get; set; } = new List<HabitacionMueble>();

    public virtual TipodeHabitacion? IdtipodeHabitacionNavigation { get; set; }

    public virtual ICollection<Paquete> Paquetes { get; set; } = new List<Paquete>();
}
