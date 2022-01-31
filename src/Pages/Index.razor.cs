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
      int NumLetters {set; get;} = 5;
      Game CurrentGame;
      Index index;
      Boolean isItYou = false;
      Boolean BackspaceAllowed = false;
   
      public void runGame()
      {
         Console.WriteLine("I got here!");
         if(this.NumLetters == 0 || this.NumLetters >= 3 && this.NumLetters <= 7)
         {
            CurrentGame = new Game(this.NumLetters);
            CurrentGame.chooseWord();
            Console.WriteLine(CurrentGame);
            Console.WriteLine(this.CurrentGame.getWord());
         }
         else
         {
            Console.WriteLine("Invalid number of letters: " + this.NumLetters);
            System.Environment.Exit(0);
         }
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
            this.index = new Index(); 
            this.index.setNumLetters(NumLetters);
            this.index.runGame();
         }
      }

      public Boolean CheckWordIfExists(String currentWord)
      {
         // check if word in corespondent file
         return true;
      }

      public void ChangeTileState(int column, int row, String state)
      {
         foreach (Tile tile in tileList)
            if (tile.tileId == row*10 + column)
               tile.State = state;
      }

      public void CheckDoubles(String currentWord)
      {
         // check all doubles cases
      }
      
      public void ClearRow(int row)
      {
         for(this.i = NumLetters; this.i > 0; this.i--)
         {
            foreach (Tile tile in tileList)
            if (tile.tileId == row*10 + i)
            {
               tile.State = "default";
               tile.Letter = "";
            }
         }
      }

      public Boolean CheckWordIfCorrect(String currentWord)
      {
         String givenWord = this.index.CurrentGame.getWord().ToString().ToUpper();
         Boolean gameWon = currentWord.Equals(givenWord);
         for(int k = 1; k <= NumLetters; k++)
         {  
            Boolean exactMatch = false;
            Boolean partialMatch = false;
            for(int ind = 1; ind <= NumLetters; ind++)
               if(currentWord[k-1] == givenWord[ind-1])
                  if(k == ind)
                     exactMatch = true;
                  else
                     partialMatch = true;
            if(exactMatch && partialMatch)
               if(!gameWon)
                  ChangeTileState(k, j, "double");
               else
                  ChangeTileState(k, j, "correct");
            else
            {
               if(exactMatch)
                  ChangeTileState(k, j, "correct");
               if(partialMatch)
                  ChangeTileState(k, j, "contained");
               if(!exactMatch && !partialMatch)
                  ChangeTileState(k, j, "missing");
            }
         }
         return gameWon;
      }

      public Boolean GameFinished(String currentWord)
      {
         Boolean won = CheckWordIfCorrect(currentWord);
         if(won)
         {
            Console.WriteLine("Congrats! You have guessed the word!");
            // show game won popup
            return true;
         }
         if(this.i == NumLetters && this.j == 6)
         {
            Console.WriteLine("The game has ended! You have not guessed the given word! The given word was: " + index.CurrentGame.getWord().ToString().ToUpper());
            return true;
         }
         return false;
      }

      private void EnterEventHandler()
      {
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

         // EDIT: IMPLEMENTED, CHECK IF IT'S RIGHT

         Console.WriteLine("The word has been submitted!");
         String CurrentWord = "";
         Boolean CompleteWord = false;
         if(i == NumLetters)
         {  
            foreach (Tile tile in tileList)
                  if (tile.tileId == j*10 + i && tile.Letter != "")
                     CompleteWord = true;
            if(CompleteWord == true)
            {
               for(int k = 1; k <= NumLetters; k++)
                  foreach (Tile tile in tileList)
                     if (tile.tileId == j*10 + k)
                     {
                        CurrentWord += tile.Letter;
                     }
               if(CheckWordIfExists(CurrentWord))
               {
                  if(GameFinished(CurrentWord))
                  {
                     //do smth
                     Console.WriteLine("The game has now ended!");
                  }
                  else
                  {
                     i = 1;
                     j += 1;
                     BackspaceAllowed = false;
                  }
               }
               else
               {
                  //give inexistent word exception
                  Console.WriteLine("Word does not exist!");
                  this.ClearRow(j);
               }
            }
            else
            {
               //give incomplete word exception
               Console.WriteLine("Incomplete word!");
            }     
         }
         else
         {
            //give incomplete word exception
            Console.WriteLine("Incomplete word!");
         }
      }
      
      private void AlphaKeyEventHandler(String key)
      {
         Console.WriteLine(i + " " + j);
         foreach (Tile tile in tileList)
         {
            if (tile.tileId == j*10 + i)
            {
               if(tile.Letter == "")
                     {
                        tile.Letter = key;
                        tile.State  = "default";
                        Console.WriteLine(tile.tileId + " contains " + tile.Letter);
                        BackspaceAllowed = true;
                     }
                     else
                        Console.WriteLine(tile.tileId + " is already occupied");
            }
         }
         if(i != 5)
            i++;
      }
      private void BackspaceEventHandler()
      {
         // the decementation has to be done after the tile is found
         // as when it gets to the last tile i=5, j=6, it needs to delete
         // its content if there is one, otherwise, it need to decrement
         // the i again to tile i=4, j=6 and delete its content
         // quite a corner case

         // EDIT: PLEASE CHECK BUT I THINK I VE DONE IT
         if(BackspaceAllowed)
         {
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
         //if(deleted == false && i != 1 && i != 5) 
         if(deleted == false && i != 1)
         {
            i--;
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
         if(i == 1)
            BackspaceAllowed = false;
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
         if(args.Key.ToUpper().All(Char.IsLetter) && args.Key.Length == 1)
            AlphaKeyEventHandler(args.Key.ToUpper());
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
            AlphaKeyEventHandler(key);
      }

   }

}

