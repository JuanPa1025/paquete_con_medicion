using System;
using System.Collections.Generic;

namespace Glamping_Addventure.Models;

public partial class Mueble
{
    public int Idmueble { get; set; }

    public string? NombreMueble { get; set; }

    public byte[]? ImagenMueble { get; set; }

    public int CantidadDisponible { get; set; }

    public double? Valor { get; set; }

    public bool? Estado { get; set; }

    public virtual ICollection<HabitacionMueble> HabitacionMuebles { get; set; } = new List<HabitacionMueble>();
}
