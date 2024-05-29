using System;
using System.Collections.Generic;

namespace PazYSalvoAPP.Data.Context;

public partial class Cliente
{
    public int Id { get; set; }

    public int? PersonaId { get; set; }

    public int? RolId { get; set; }

    public DateTime? FechaDeCreacion { get; set; }

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();

    public virtual Persona Persona { get; set; }

    public virtual Role Rol { get; set; }
}
