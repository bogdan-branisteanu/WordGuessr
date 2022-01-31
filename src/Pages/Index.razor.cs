using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components;
using src.Components;


namespace src.Pages
{

    public partial class Index
    {
        int numLetters = 5;
        int i = 1;
        int j = 1;
        private ElementReference inputDiv;

        List<Tile> tileList = new List<Tile>();



         protected override async Task OnInitializedAsync()
        {
            for (int j = 1; j <= 6; j++)
            {
               for(int i = 1; i <= numLetters; i++){

                  Tile tile = new Tile();
                  tile.tileId = j * 10 + i;

                  if (!tileList.Contains(tile))
                  {
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
    
    private void EnterEventHandler()
    {
        Console.WriteLine("The word has been submitted!");
        // form the word using the key of each tile
        // check if word is in the corespondent word file
            // if it is then accept the input, and check with the given word
            // modify the tiles status based on each key
            // check if the game is over
                // if it is, give a game is over popup managing the outcome
            // else:
                // j = j + 1
                // i = 1
        // else:
            // throw incorrect word exception
            // clear all tiles
            // i = 1

    }

    private void KeyboardEventHandler(KeyboardEventArgs args)
    {
        Console.WriteLine("Key Pressed is " + args.Key);
        if(args.Key.ToUpper() == "ENTER")
        {
            this.EnterEventHandler();
        }
        else
        {
            Console.WriteLine(i + " " + j);
            foreach (Tile tile in tileList)
            {
                if (tile.tileId == j*10 + i)
                {
                    tile.Letter = args.Key.ToUpper();
                    tile.State  = "correct";
                    Console.WriteLine(tile.tileId + " contains " + tile.Letter);
                }
            }
            if(!(i == 5 && j == 6))
            {
                i++;
                if (i == 6)
                {
                    i = 1;
                    j += 1;
                }
            }
        }
        
    }

    private void ButtonEventHandler(string key)
    {
        Console.WriteLine("Virtual Key Pressed is " + key);
        if(key == "backspace")
        {   
            // the decementation has to be done after the tile is found
            // as when it gets to the last tile i=5, j=6, it needs to delete
            // its content if there is one, otherwise, it need to decrement
            // the i again to tile i=4, j=6 and delete its content
            // quite a corner case

            // EDIT: PLEASE CHECK BUT I THINK I VE DONE IT
            
            Boolean deleted = false;
            foreach (Tile tile in tileList)
            {
                if (tile.tileId == j*10 + i)
                {
                    if(tile.Letter != "")
                    {   
                        tile.Letter = "";
                        tile.State  = "default";
                        Console.WriteLine(tile.tileId + " delete " + tile.Letter);
                        deleted = true;
                    }
                }
            }
            if(deleted == false && i != 1 || j != 1 && i != 5 || j != 6) 
            {
                i--;
                if (i == 0)
                {
                    i = 5;
                    j -= 1;
                    if(j == 0)
                    {
                        i = 1;
                        j = 1;
                    }
                }
                foreach (Tile tile in tileList)
                {
                    if (tile.tileId == j*10 + i)
                    {
                        if(tile.Letter != "")
                        {   
                            tile.Letter = "";
                            tile.State  = "default";
                            Console.WriteLine(tile.tileId + " delete " + tile.Letter);
                            deleted = true;
                        }
                    }
                }
            }
            
        }
        else if(key == "enter")
        {
            this.EnterEventHandler();
        }
        else
        {
            Console.WriteLine(i + " " + j*10);
            foreach (Tile tile in tileList)
            {
                if (tile.tileId == j*10 + i)
                {
                    tile.Letter = key;
                    tile.State  = "correct";
                    Console.WriteLine(tile.tileId + " contains " + tile.Letter);
                }
            }
            if(!(i == 5 && j == 6))
            {
                i++;
                if (i == 6)
                {
                    i = 1;
                    j += 1;
                }
            }
        }
        
    }




}
}