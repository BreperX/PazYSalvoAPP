using System;
using System.Collections.Generic;

namespace PazYSalvoAPP.Data.Context;

public partial class Factura
{
    public int Id { get; set; }

    public decimal? Saldo { get; set; }

    public int? ClienteId { get; set; }

    public int? ServicioAdquiridoId { get; set; }

    public int? MedioDePagoId { get; set; }

    public int? EstadoId { get; set; }

    public DateTime? FechaDeCreacion { get; set; }

    public virtual Cliente Cliente { get; set; }

    public virtual ICollection<DetallesDeFactura> DetallesDeFacturas { get; set; } = new List<DetallesDeFactura>();

    public virtual Estado Estado { get; set; }

    public virtual MediosDePago MedioDePago { get; set; }

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();

    public virtual Servicio ServicioAdquirido { get; set; }
}
