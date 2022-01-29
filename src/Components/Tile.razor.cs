using Microsoft.AspNetCore.Components;
using System;

namespace src.Components{

    public partial class Tile {
        [Parameter] public string Letter { get; set; }
        [Parameter] public string Color { get; set; }
    }
   

}