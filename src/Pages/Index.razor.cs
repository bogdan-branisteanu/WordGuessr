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
      public int NumLetters { set; get; }
      public WordInstance Word { set; get; }
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
      

      [Parameter] public string NumLettersString { get; set; }

      [Parameter] public string LanguageString { get; set; }
     
      int NumLetters {set; get;} = 5;
      Game CurrentGame;
      Index index;
      Boolean BackspaceAllowed = false;
      List<string> MissingLetters = new List<string>();
      List<string> CorrectLetters = new List<string>();
      List<string> ContainedLetters = new List<string>();
      List<string> DoubleGreenLetters = new List<string>();
      List<string> DoubleYellowLetters = new List<string>();
   
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

        int i = 1;
        int j = 1;
        private ElementReference inputDiv;

        List<Tile> tileList = new List<Tile>();

        List<Button> buttonList = new List<Button>();

      public void setNumLetters(int numLetters)
      {
         this.NumLetters = numLetters;
      }


      protected override async Task OnInitializedAsync()
      {
         NumLetters = Int32.Parse(NumLettersString);
         if(NumLetters == 0)
         {
            Random rnd = new Random();
            NumLetters = rnd.Next(3,8);
         }
         
         for (int j = 1; j <= 6; j++)
         {
            for(int i = 1; i <= NumLetters; i++){

               Tile tile = new Tile();
               tile.tileId = j * 10 + i;

               if (!tileList.Contains(tile))
               {
                  tileList.Add(tile);
               }  
            }
         }
         Button buttonQ     = new Button("Q", "50px", "60px", "white", "Q");
         Button buttonW     = new Button("W", "50px", "60px",  "white", "W");
         Button buttonE     = new Button("E", "50px", "60px",  "white", "E");
         Button buttonR     = new Button("R", "50px", "60px",  "white", "R");
         Button buttonT     = new Button("T", "50px", "60px",  "white", "T");
         Button buttonY     = new Button("Y", "50px", "60px",  "white", "Y");
         Button buttonU     = new Button("U", "50px", "60px",  "white", "U");
         Button buttonI     = new Button("I", "50px", "60px",  "white", "I");
         Button buttonO     = new Button("O", "50px", "60px",  "white", "O");
         Button buttonP     = new Button("P", "50px", "60px",  "white", "P");
         Button buttonA     = new Button("A", "50px", "60px",  "white", "A");
         Button buttonS     = new Button("S", "50px", "60px",  "white", "S");
         Button buttonD     = new Button("D", "50px", "60px",  "white", "D");
         Button buttonF     = new Button("F", "50px", "60px",  "white", "F");
         Button buttonG     = new Button("G", "50px", "60px",  "white", "G");
         Button buttonH     = new Button("H", "50px", "60px",  "white", "H");
         Button buttonJ     = new Button("J", "50px", "60px",  "white", "J");
         Button buttonK     = new Button("K", "50px", "60px",  "white", "K");
         Button buttonL     = new Button("L", "50px", "60px",  "white", "L");
         Button buttonEnter = new Button("enter", "70px", "60px",  "white", "", "fas fa-check-square");
         Button buttonZ     = new Button("Z", "50px", "60px",  "white", "Z");
         Button buttonX     = new Button("X", "50px", "60px",  "white", "X");
         Button buttonC     = new Button("C", "50px", "60px",  "white", "C");
         Button buttonV     = new Button("V", "50px", "60px",  "white", "V");
         Button buttonB     = new Button("B", "50px", "60px",  "white", "B");
         Button buttonN     = new Button("N", "50px", "60px",  "white", "N");
         Button buttonM     = new Button("M", "50px", "60px",  "white", "M");
         Button buttonBksp  = new Button("backspace", "70px", "60px",  "white", "", "fas fa-backspace");
           
        
           
         buttonList.Add(buttonQ);
         buttonList.Add(buttonW);
         buttonList.Add(buttonE);
         buttonList.Add(buttonR);
         buttonList.Add(buttonT);
         buttonList.Add(buttonY);
         buttonList.Add(buttonU);
         buttonList.Add(buttonI);
         buttonList.Add(buttonO);
         buttonList.Add(buttonP);
         buttonList.Add(buttonA);
         buttonList.Add(buttonS);
         buttonList.Add(buttonD);
         buttonList.Add(buttonF);
         buttonList.Add(buttonG);
         buttonList.Add(buttonH);
         buttonList.Add(buttonJ);
         buttonList.Add(buttonK);
         buttonList.Add(buttonL);
         buttonList.Add(buttonEnter);
         buttonList.Add(buttonZ);
         buttonList.Add(buttonX);
         buttonList.Add(buttonC);
         buttonList.Add(buttonV);
         buttonList.Add(buttonB);
         buttonList.Add(buttonN);
         buttonList.Add(buttonM);
         buttonList.Add(buttonBksp);

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
         String path;
         switch(this.index.NumLetters)
         {
            case 3:
               path = String.Concat(Directory.GetCurrentDirectory(), @"\Resources\3LetterWordsEN.txt");
               if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows)){
               path = path.Replace("\\", "/");
            }
            break;
            case 4:
               path = String.Concat(Directory.GetCurrentDirectory(), @"\Resources\4LetterWordsEN.txt");
               if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows)){
               path = path.Replace("\\", "/");
            }
            break;
            case 5:
               path = String.Concat(Directory.GetCurrentDirectory(), @"\Resources\5LetterWordsEN.txt");
               if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows)){
               path = path.Replace("\\", "/");
            }
            break;
            case 6:
               path = String.Concat(Directory.GetCurrentDirectory(), @"\Resources\6LetterWordsEN.txt");
               if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows)){
               path = path.Replace("\\", "/");
            }
            break;
            case 7:
               path = String.Concat(Directory.GetCurrentDirectory(), @"\Resources\7LetterWordsEN.txt");
               if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows)){
               path = path.Replace("\\", "/");
            }
            break;
            default:
               path = "invalid";
            break;
         }
         if(File.Exists(path))
         {
            String[] lines = File.ReadAllLines(@path);
            foreach(String line in lines)
               if(line.ToUpper().Contains(currentWord))
                  return true;
         }
         else
         {
            // throw incorrect path exception
            Console.WriteLine("The path: " + path + " is incorrect. We cannot check if the word: " + currentWord + " exists or not.");
         }
         // check if word in corespondent file
         return false;
      }

      public void ChangeTileState(int column, int row, String state)
      {
         foreach (Tile tile in tileList)
            if (tile.tileId == row*10 + column)
               tile.State = state;
      }

      public void CheckDoubles(String currentWord, String[] tileStates)
      {
         // check all doubles cases
         // cases:
         // doubles present:
            // both double on green, stay like this
            // both double on yellow, stay like this
            // one double on green, one on yellow, maintain colours
            // one double on green, others not present, change green to double state colour
            // one double on yellow, others not present, create maybe a different double state with a different gradient
         // no doubles present:
            // one letter on yellow, the same one again on yellow, only keep the first one
            // one letter on yellow, the same one again on green, only keep the later one
            // one letter on green, anther one on yellow, only keep the green one
         String givenWord = this.index.CurrentGame.getWord().ToString();
         for(int k = 0; k < index.NumLetters; k++)
         {
            int countInGiven = 0;
            int countInCurrent = 0;
            for(int ind = 0; ind < index.NumLetters; ind++)
            {  
               if(currentWord[k] == givenWord[ind])
                  countInGiven++;
               if(currentWord[k] == currentWord[ind])
                  countInCurrent++;
            }
            if(countInGiven == 1 && countInCurrent > 1)
               for(int ind = 0; ind < index.NumLetters; ind++)
               {
                  if(k < ind && currentWord[ind] == currentWord[k])
                  {
                     if(tileStates[k] == "correct" && tileStates[ind] == "contained" || tileStates[k] == "contained" && tileStates[ind] == "contained")
                        tileStates[ind] = "missing";
                     else if(tileStates[k] == "contained" && tileStates[ind] == "correct")
                        tileStates[k] = "missing";
                  }
               }
             else
               if(countInCurrent == 1 && countInGiven > 1)
                  if(tileStates[k] == "correct")
                     tileStates[k] = "doubleGreen";
                  else if(tileStates[k] == "contained")
                     tileStates[k] = "doubleYellow";
                     
         }
      }
           

      public void ClearRow(int row)
      {
         for(this.i = index.NumLetters; this.i > 0; this.i--)
         {
            foreach (Tile tile in tileList)
               if (tile.tileId == row*10 + i)
               {
                  tile.State = "default";
                  tile.Letter = "";
               }
         }
         this.i = 1;
      }

      public void UpdateKeysColour()
      {
         // change the colour of each key
         Console.WriteLine("Missing: " + string.Join(", ", this.MissingLetters));
         Console.WriteLine("Contained: " + string.Join(", ", this.ContainedLetters));
         Console.WriteLine("Double Yellows: " + string.Join(", ", this.DoubleYellowLetters));
         Console.WriteLine("Correct: " + string.Join(", ", this.CorrectLetters));
         foreach (Button button in buttonList)
         {
            foreach(String letter in this.MissingLetters)
               if(letter == button.sId)
                  button.State = "missing";
            foreach(String letter in this.ContainedLetters)
               if(letter == button.sId)
                  button.State = "contained";
            foreach(String letter in this.DoubleYellowLetters)
               if(letter == button.sId)
                  button.State = "doubleYellow";
            foreach(String letter in this.CorrectLetters)
               if(letter == button.sId)
                  button.State = "correct";
         }

      }
      public void ChangeKeyColour(String key, String state)
      {
         if(state == "contained")
            if(this.ContainedLetters.Count == 0 || !this.ContainedLetters.Contains(key))
               this.ContainedLetters.Add(key);
         if(state == "doubleYellow")
         {  
            if(this.ContainedLetters.Count > 0 && this.ContainedLetters.Contains(key))
               this.ContainedLetters.Remove(key);
            if(this.DoubleYellowLetters.Count == 0 || !this.DoubleYellowLetters.Contains(key))
               this.DoubleYellowLetters.Add(key);
         }
         if(state == "correct" || state == "doubleGreen")
         {
            if(this.ContainedLetters.Count > 0 && this.ContainedLetters.Contains(key))
               this.ContainedLetters.Remove(key);
            if(this.DoubleYellowLetters.Count > 0 && this.DoubleYellowLetters.Contains(key))
               this.DoubleYellowLetters.Remove(key);
            if(this.CorrectLetters.Count == 0 || !this.CorrectLetters.Contains(key))
               this.CorrectLetters.Add(key);
         }
         if(state == "missing")
         {
            if(this.ContainedLetters.Count > 0 && this.ContainedLetters.Contains(key))
               this.ContainedLetters.Remove(key);
            if(this.DoubleYellowLetters.Count > 0 && this.DoubleYellowLetters.Contains(key))
               this.DoubleYellowLetters.Remove(key);
            if(this.CorrectLetters.Count > 0 && this.CorrectLetters.Contains(key))
               this.CorrectLetters.Remove(key);
            if(this.MissingLetters.Count == 0 || !this.MissingLetters.Contains(key))
               this.MissingLetters.Add(key);
         }
      }
      public Boolean CheckWordIfCorrect(String currentWord)
      {
         String givenWord = this.index.CurrentGame.getWord().ToString().ToUpper();
         Boolean gameWon = currentWord.Equals(givenWord);
         String[] tileStates = new String[index.NumLetters]; 
         for(int k = 1; k <= index.NumLetters; k++)
         {  
            Boolean exactMatch = false;
            Boolean partialMatch = false;
            for(int ind = 1; ind <= index.NumLetters; ind++)
               if(currentWord[k-1] == givenWord[ind-1])
                  if(k == ind)
                     exactMatch = true;
                  else
                     partialMatch = true;
            if(exactMatch)
               tileStates[k-1] = "correct";
               //ChangeTileState(k, j, "correct");
            else
            if(partialMatch)
               tileStates[k-1] = "contained";
               //ChangeTileState(k, j, "contained");
            else
            if(!exactMatch && !partialMatch)
               tileStates[k-1] = "missing";
               //ChangeTileState(k, j, "missing");
         }
         CheckDoubles(currentWord, tileStates);
         for(int k = 0; k < index.NumLetters; k++)
            {
               ChangeKeyColour(currentWord[k].ToString(), tileStates[k]);
               ChangeTileState(k+1, j, tileStates[k]);
            }
         UpdateKeysColour();
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
         if(this.i == index.NumLetters && this.j == 6)
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
         if(i == index.NumLetters)
         {  
            foreach (Tile tile in tileList)
                  if (tile.tileId == j*10 + i && tile.Letter != "")
                     CompleteWord = true;
            if(CompleteWord == true)
            {
               for(int k = 1; k <= index.NumLetters; k++)
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
                  // if we want to clear the row we can use this:
                  this.ClearRow(j);
                  // otherwise we don't do anything, just give the exception
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
         if(i != index.NumLetters)
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
               Console.WriteLine(tile.tileId);
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
            this.AlphaKeyEventHandler(key);
      }

   }

}

