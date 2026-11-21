using System;
using System.Collections.Generic;

namespace Sistema.Models;

public partial class Categorium
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public bool? Activo { get; set; }

    public DateTime FechaCreacion { get; set; }

    public virtual ICollection<Curso> Cursos { get; set; } = new List<Curso>();
}
