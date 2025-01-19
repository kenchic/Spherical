using System;
using System.Collections.Generic;
using Creative.DTO.Defender;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Defender.Api.Models
{
    public partial class SphericalContext : DbContext
    {
        public virtual DbSet<MenuUsuarioDTO> MenuUsuario { get; set; }
    }
}
