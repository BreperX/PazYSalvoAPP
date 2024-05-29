using System;
using System.Collections.Generic;

namespace PazYSalvoAPP.Data.Context;

public partial class Servicio
{
    public int Id { get; set; }

    public string Nombre { get; set; }

    public string Descripcion { get; set; }

    public decimal Precio { get; set; }

    public DateTime? FechaDeCreacion { get; set; }

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();
}
