using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Glamping_Addventure.Models;

public partial class Reserva
{
    public int IdReserva { get; set; }

    public string? NroDocumentoCliente { get; set; }
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
    public DateTime? FechaReserva { get; set; }

    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
    public DateOnly FechaInicio { get; set; }

    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
    public DateOnly FechaFinalizacion { get; set; }

    public double? SubTotal { get; set; }

    public double? Descuento { get; set; }

    public double? Iva { get; set; }

    public double? MontoTotal { get; set; }

    public int? MetodoPago { get; set; }

    public int? IdEstadoReserva { get; set; }

    public virtual ICollection<Abono> Abonos { get; set; } = new List<Abono>();

    public virtual ICollection<DetalleReservaPaquete> DetalleReservaPaquetes { get; set; } = new List<DetalleReservaPaquete>();

    public virtual ICollection<DetalleReservaServicio> DetalleReservaServicios { get; set; } = new List<DetalleReservaServicio>();

    public virtual EstadosReserva? IdEstadoReservaNavigation { get; set; }

    public virtual MetodoPago? MetodoPagoNavigation { get; set; }

    public virtual Cliente? NroDocumentoClienteNavigation { get; set; }
}