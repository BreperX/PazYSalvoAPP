using System;
using System.Collections.Generic;

namespace PazYSalvoAPP.Data.Context;

public partial class Usuario
{
    public int Id { get; set; }

    public int? PersonaId { get; set; }

    public string NombreUsuario { get; set; }

    public string Contrasena { get; set; }

    public DateTime? FechaDeCreacion { get; set; }

    public virtual Persona Persona { get; set; }
}
