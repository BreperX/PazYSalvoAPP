using System;
using System.Collections.Generic;

namespace PazYSalvoAPP.Data.Context;

public partial class Pago
{
    public int Id { get; set; }

    public decimal? MontoDePago { get; set; }

    public int? FacturaId { get; set; }

    public bool? Activo { get; set; }

    public DateTime? FechaDeCreacion { get; set; }

    public virtual ICollection<DetallesDeFactura> DetallesDeFacturas { get; set; } = new List<DetallesDeFactura>();

    public virtual Factura Factura { get; set; }
}
