using System;  
using System.IO; 
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components;
using src.Components;
using System.Runtime.InteropServices;

namespace src.Pages
{
   // string pathProfanity = @"/resources/profanityWords.txt";
   public class WordInstance
   {

      // Constructor method that take the word itself as a parameter
      public WordInstance(string word)
      {
         Word = word;
      }
      public string Word {set; get;}
      
      public override string ToString()
      {
         return Word;
      }

      public Boolean ProfanitySafe {set; get;}

      public void checkProfanity(string path)
      {
         if(File.Exists(path))
         {
            string[] lines = File.ReadAllLines(path);
            this.ProfanitySafe = true;
            foreach(string line in lines) if (this.ProfanitySafe == true)
            {
               this.ProfanitySafe = !(this.Word.Equals(line));
            }
         }
         else Console.WriteLine("Specified path for " + path + " does not exist!");
      }
   }

   public class Game{
      
      public Game(int numLetters)
      {
         NumLetters = numLetters;
      }
      public int NumLetters {set; get;}
      public WordInstance Word {set; get;}
      public void chooseWord()
      {
         string Path;
         switch(this.NumLetters)
         {
            case 0:
               Path = String.Concat(Directory.GetCurrentDirectory(), @"\Resources\CommonWordsEN.txt");
                 if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows)){
               
               Path = Path.Replace("\\", "/");
            }
            break;
            case 3:
               Path = String.Concat(Directory.GetCurrentDirectory(), @"\Resources\3LetterCommonWordsEN.txt");
               if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows)){
               Path = Path.Replace("\\", "/");
            }
            break;
            case 4:
               Path = String.Concat(Directory.GetCurrentDirectory(), @"\Resources\4LetterCommonWordsEN.txt");
               if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows)){
               Path = Path.Replace("\\", "/");
            }
            break;
            case 5:
               Path = String.Concat(Directory.GetCurrentDirectory(), @"\Resources\5LetterCommonWordsEN.txt");
               if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows)){
               Path = Path.Replace("\\", "/");
            }
            break;
            case 6:
               Path = String.Concat(Directory.GetCurrentDirectory(), @"\Resources\6LetterCommonWordsEN.txt");
               if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows)){
               Path = Path.Replace("\\", "/");
            }
            break;
            case 7:
               Path = String.Concat(Directory.GetCurrentDirectory(), @"\Resources\7LetterCommonWordsEN.txt");
               if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows)){
               Path = Path.Replace("\\", "/");
            }
            break;
            default:
               Path = "invalid";
            break;

          
         }
         if(File.Exists(Path))
         {
            string[] lines = File.ReadAllLines(@Path);
            Random rdn = new Random();
            int index = rdn.Next(0, lines.Length);
            Console.WriteLine("No of lines: " + lines.Length + " index: " + index);
            this.Word = new WordInstance(lines[index]);

            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)){
               this.Word.checkProfanity(String.Concat(Directory.GetCurrentDirectory(), @"/resources/ProfanityWordsEN.txt"));
            } else {
               this.Word.checkProfanity(String.Concat(Directory.GetCurrentDirectory(), @"\resources\ProfanityWordsEN.txt"));
            }
            while(this.Word.ProfanitySafe == false)
            {
               index = rdn.Next(0, lines.Length);
               this.Word = new WordInstance(lines[index]);
               
               if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)){
                  this.Word.checkProfanity(String.Concat(Directory.GetCurrentDirectory(), @"/resources/ProfanityWordsEN.txt"));
               } else {
                  this.Word.checkProfanity(String.Concat(Directory.GetCurrentDirectory(), @"\resources\ProfanityWordsEN.txt"));
               }
            }
         }
         else Console.WriteLine("Specified path for " + @Path + " does not exist!");
      }

      public WordInstance getWord()
      {
         return this.Word;
      }
   }
   public partial class Index{
      int NumLetters {set; get;} =5;
     
      public void runGame()
      {
         Console.WriteLine(this.NumLetters);
         if(this.NumLetters == 0 || this.NumLetters >= 3 && this.NumLetters <= 7)
         {
            Game newGame = new Game(this.NumLetters);
            newGame.chooseWord();
            Console.WriteLine(newGame.getWord());
         }
         else
            Console.WriteLine("Invalid number of letters: " + this.NumLetters);

      }

        int numLetters = 5;
        int i = 1;
        int j = 1;
        private ElementReference inputDiv;

        List<Tile> tileList = new List<Tile>();

      public void setNumLetters(int numLetters)
      {
         this.NumLetters = numLetters;
      }


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
            Index index = new Index(); 
            index.setNumLetters(NumLetters);
            index.runGame();

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

    private void BackspaceEventHandler()
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

    private void KeyboardEventHandler(KeyboardEventArgs args)
    {
        Console.WriteLine("Key Pressed is " + args.Key);
        if(args.Key.ToUpper() == "ENTER")
            this.EnterEventHandler();
        
        else 
        if(args.Key.ToUpper() == "BACKSPACE")
            this.BackspaceEventHandler();
        else
        if(String.Compare(args.Key.ToUpper(),"A") >= 0 && String.Compare(args.Key.ToUpper(),"Z") <= 0)
        {
            Console.WriteLine(i + " " + j);
            foreach (Tile tile in tileList)
            {
                if (tile.tileId == j*10 + i)
                {
                    if(tile.Letter == "")
                        {
                            tile.Letter = args.Key.ToUpper();
                            tile.State  = "correct";
                            Console.WriteLine(tile.tileId + " contains " + tile.Letter);
                        }
                        else
                            Console.WriteLine(tile.tileId + " is already occupied");
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
        else
            Console.WriteLine("Input of key: " + args.Key + " is not accepted!");
        
    }

    private void ButtonEventHandler(string key)
    {
        Console.WriteLine("Virtual Key Pressed is " + key);
        if(key == "backspace")   
            this.BackspaceEventHandler();     
        else 
        if(key == "enter")
            this.EnterEventHandler();
        else
        {
            Console.WriteLine(i + " " + j*10);
            foreach (Tile tile in tileList)
            {
                if (tile.tileId == j*10 + i)
                {
                    if(tile.Letter == "")
                    {
                        tile.Letter = key;
                        tile.State  = "correct";
                        Console.WriteLine(tile.tileId + " contains " + tile.Letter);
                    }
                    else
                        Console.WriteLine(tile.tileId + " is already occupied");
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

