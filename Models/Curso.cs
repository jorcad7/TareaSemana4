using System;
using System.Collections.Generic;

namespace Sistema.Models;

public partial class Curso
{
    public int Id { get; set; }

    public string Titulo { get; set; } = null!;

    public int Creditos { get; set; }

    public decimal Precio { get; set; }

    public int? CategoriaId { get; set; }

    public virtual Categorium? Categoria { get; set; }

    public virtual ICollection<Inscripcion> Inscripcions { get; set; } = new List<Inscripcion>();
}
