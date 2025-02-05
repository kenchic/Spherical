using Microsoft.EntityFrameworkCore;
using Spherical.Client.DTO.Defender;

namespace Spherical.Api.Models
{
    public partial class SphericalContext : DbContext
    {
        public virtual DbSet<UserMenuDTO> UserMenuDTO { get; set; }
    }
}