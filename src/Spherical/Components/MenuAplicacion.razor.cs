using Spherical.Client.DTO.Defender;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Creative.Components
{
    public partial class MenuAplicacion : ComponentBase
    {
        [Parameter]
        public List<string>? DataSourceApp { get; set; }
    }
}
