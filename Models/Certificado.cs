using System;
using System.Collections.Generic;

namespace Sistema.Models;

public partial class Certificado
{
    public int Id { get; set; }

    public string CodigoUnico { get; set; } = null!;

    public DateTime FechaEmision { get; set; }

    public decimal? CalificacionFinal { get; set; }

    public int InscripcionId { get; set; }

    public virtual Inscripcion Inscripcion { get; set; } = null!;
}
