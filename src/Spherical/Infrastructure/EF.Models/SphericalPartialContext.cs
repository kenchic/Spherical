using Microsoft.EntityFrameworkCore;
using Spherical.Client.DTO.Defender;

namespace Spherical.Infrastructure.EF.Models
{
    public partial class SphericalContext : DbContext
    {
        public virtual DbSet<UserMenuDTO> UserMenuDTO { get; set; }
    }
}