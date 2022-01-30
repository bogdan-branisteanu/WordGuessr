using Microsoft.AspNetCore.Components;


namespace src.Components{

    public partial class TileComponent : ComponentBase {
      
        [Parameter] public Tile Tile { get; set; }

    }
   

}