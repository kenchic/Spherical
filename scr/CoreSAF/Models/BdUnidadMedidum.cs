﻿using System;
using System.Collections.Generic;

namespace CoreSAF.Models
{
    public partial class BdUnidadMedidum
    {
        public BdUnidadMedidum()
        {
            BdElementos = new HashSet<BdElemento>();
        }

        public byte Id { get; set; }
        public string Nombre { get; set; } = null!;
        public bool Activo { get; set; }

        public virtual ICollection<BdElemento> BdElementos { get; set; }
    }
}
