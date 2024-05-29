using System;
using System.Collections.Generic;

namespace PazYSalvoAPP.Data.Context;

public partial class Role
{
    public int Id { get; set; }

    public string Nombre { get; set; }

    public string Descripcion { get; set; }

    public bool? Activo { get; set; }

    public DateTime? FechaDeCreacion { get; set; }

    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();
}
