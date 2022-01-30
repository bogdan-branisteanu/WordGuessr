using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components;
using src.Components;


namespace src.Pages
{

    public partial class Index
    {
        int numLetters = 5;
        int i = 1;
        int j = 10;
        private ElementReference inputDiv;

        List<Tile> tileList = new List<Tile>();



         protected override async Task OnInitializedAsync()
        {
            for (int j = 1; j <= 6; j++)
            {
               for(int i = 1; i <= numLetters; i++){

                  Tile tile = new Tile();
                  tile.tileId = j * 10 + i;

                  if (!tileList.Contains(tile)) {
                     tileList.Add(tile);

                  }
                  
               }
           }
           StateHasChanged();
        } 

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await inputDiv.FocusAsync();

        }

    }





    private void KeyboardEventHandler(KeyboardEventArgs args)
    {

        Console.WriteLine("Key Pressed is " + args.Key);
        foreach (Tile tile in tileList)
        {
            if (tile.tileId == j + i)
            {
                tile.Letter = args.Key;
                tile.State  = "correct";
                Console.WriteLine(tile.tileId + " " + tile.Letter);
                


            }
        }

        i++;

        if (i == 6)
        {
            i = 1;
            j += 10;
            if (j == 6)
            {
                j = 1;
            }
        }

        
      
    }







}
}