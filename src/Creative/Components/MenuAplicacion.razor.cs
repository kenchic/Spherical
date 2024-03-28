using Creative.DTO.Defender;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Creative.Components
{
    public partial class MenuAplicacion : ComponentBase
    {
        [Parameter]
        public List<MenuUsuarioDTO>? DataSourceApp { get; set; }
    }
}
