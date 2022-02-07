using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components;
using System.Runtime.InteropServices;
using Microsoft.JSInterop;
using Blazored.Modal.Services;
using Blazored.Modal;
using System.Timers;
using src.Components;
using src.Classes;
using Radzen;


namespace src.Pages
{
   // string pathProfanity = @"/resources/profanityWords.txt";
   public partial class Index{
      
      [CascadingParameter] public IModalService Modal { get; set; }
      [Parameter] public string NumLettersString { get; set; }
      [Parameter] public string LanguageString { get; set; }
      [Inject] private NotificationService NotificationService {get;set;}
      [Inject] NavigationManager NavigationManager { get; set; }

      List<string> MissingLetters = new List<string>();
      List<string> CorrectLetters = new List<string>();
      List<string> ContainedLetters = new List<string>();
      List<string> DoubleGreenLetters = new List<string>();
      List<string> DoubleYellowLetters = new List<string>();
      List<Tile> tileList = new List<Tile>();
      List<Button> buttonList = new List<Button>();

      public int NumLetters {set; get;} = 5;
      public int Width { get; set; }
      private ElementReference inputDiv;
      Boolean BackspaceAllowed = false;
      int i = 1;
      int j = 1;

      Game CurrentGame;
      Index index;
      
        

      protected override async void OnInitialized()
      {
         var dimension = await js.InvokeAsync<WindowDimension>("getWindowDimensions");
         Width = dimension.Width;

         NumLetters = Int32.Parse(NumLettersString);
         if(NumLetters == 0)
         {
            Random rnd = new Random();
            NumLetters = rnd.Next(3,8);
         }
         
         for (int k = 1; k <= 6; k += 1)
         {
            for(int ind = 1; ind <= NumLetters; ind++){

               Tile tile = new Tile();
               tile.tileId = k * 10 + ind;

               if (!tileList.Contains(tile))
               {
                  tileList.Add(tile);
               }  
            }
         }
         Button buttonQ     = new Button("Q", "40px", "50px",  "white", "Q");
         Button buttonW     = new Button("W", "40px", "50px",  "white", "W");
         Button buttonE     = new Button("E", "40px", "50px",  "white", "E");
         Button buttonR     = new Button("R", "40px", "50px",  "white", "R");
         Button buttonT     = new Button("T", "40px", "50px",  "white", "T");
         Button buttonY     = new Button("Y", "40px", "50px",  "white", "Y");
         Button buttonU     = new Button("U", "40px", "50px",  "white", "U");
         Button buttonI     = new Button("I", "40px", "50px",  "white", "I");
         Button buttonO     = new Button("O", "40px", "50px",  "white", "O");
         Button buttonP     = new Button("P", "40px", "50px",  "white", "P");
         Button buttonA     = new Button("A", "40px", "50px",  "white", "A");
         Button buttonS     = new Button("S", "40px", "50px",  "white", "S");
         Button buttonD     = new Button("D", "40px", "50px",  "white", "D");
         Button buttonF     = new Button("F", "40px", "50px",  "white", "F");
         Button buttonG     = new Button("G", "40px", "50px",  "white", "G");
         Button buttonH     = new Button("H", "40px", "50px",  "white", "H");
         Button buttonJ     = new Button("J", "40px", "50px",  "white", "J");
         Button buttonK     = new Button("K", "40px", "50px",  "white", "K");
         Button buttonL     = new Button("L", "40px", "50px",  "white", "L");
         Button buttonEnter = new Button("enter", "60px", "50px",  "white", "Enter");
         Button buttonZ     = new Button("Z", "40px", "50px",  "white", "Z");
         Button buttonX     = new Button("X", "40px", "50px",  "white", "X");
         Button buttonC     = new Button("C", "40px", "50px",  "white", "C");
         Button buttonV     = new Button("V", "40px", "50px",  "white", "V");
         Button buttonB     = new Button("B", "40px", "50px",  "white", "B");
         Button buttonN     = new Button("N", "40px", "50px",  "white", "N");
         Button buttonM     = new Button("M", "40px", "50px",  "white", "M");
         Button buttonBksp  = new Button("backspace", "60px", "50px",  "white", "⬅");

      
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
            await js.InvokeVoidAsync("OnScrollEvent");

            this.index = new Index(); 
            this.index.setNumLetters(NumLetters);
            this.index.runGame();
            this.index.i = 1;
            this.index.j = 1;

         

        
         }

        
       
        StateHasChanged();

      }

      public void runGame()
      {
         Console.WriteLine("index.i got here!");
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
            System.Environment.Exit(0); //TODO: Maybe add 404 page
         }
      }

      public async Task lostFocus()
      {
         if(NavigationManager.Uri.Contains("game"))
            await inputDiv.FocusAsync();
      }

      void ShowNotification(NotificationMessage message)
      {
            NotificationService.Notify(message);
      }

      public void setNumLetters(int numLetters)
      {
         this.NumLetters = numLetters;
      }
     
  
      public Boolean CheckWordIfExists(String currentWord)
      {
         String path;
         switch(index.NumLetters)
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
               {  
                  
                  return true;
                  //return true;
               }
                  
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
         String givenWord = index.CurrentGame.getWord().ToString();
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
               if(countInGiven > 1 && countInCurrent == 1)
                  if(tileStates[k] == "correct")
                     tileStates[k] = "doubleGreen";
                  else if(tileStates[k] == "contained")
                     tileStates[k] = "doubleYellow";
                     
         }
      }
           
      private async void ThrowAlertAnimation()
      {
         // Console.WriteLine("Makin it red when j is " + index.j);
         await js.InvokeAsync<string>("GiveAlert", index.NumLetters, index.j, tileList);
        

         StateHasChanged();
      }


      public void ClearRow(int row)
      {
         for(index.i = index.NumLetters; index.i > 0; index.i--)
         {
            foreach (Tile tile in tileList)
               if (tile.tileId == row*10 + index.i)
               {
                  tile.State = "default";
                  tile.Letter = "";
               }
         }
         index.i = 1;
      }

      public void UpdateKeysColour()
      {
         // change the colour of each key
         Console.WriteLine("Missing: " + string.Join(", ", this.MissingLetters));
         Console.WriteLine("Contained: " + string.Join(", ", this.ContainedLetters));
         Console.WriteLine("Double Yellows: " + string.Join(", ", this.DoubleYellowLetters));
         Console.WriteLine("Correct: " + string.Join(", ", this.CorrectLetters));
         foreach (Button Button in buttonList)
         {
            foreach(String letter in this.MissingLetters)
               if(letter == Button.sId)
                  Button.State = "missing";
            foreach(String letter in this.ContainedLetters)
               if(letter == Button.sId)
                  Button.State = "contained";
            foreach(String letter in this.DoubleYellowLetters)
               if(letter == Button.sId)
                  Button.State = "doubleYellow";
            foreach(String letter in this.CorrectLetters)
               if(letter == Button.sId)
                  Button.State = "correct";
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
            if(!(this.ContainedLetters.Count > 0 && this.ContainedLetters.Contains(key)) && !(this.DoubleYellowLetters.Count > 0 
            && this.DoubleYellowLetters.Contains(key)) && !(this.CorrectLetters.Count > 0 && this.CorrectLetters.Contains(key)) &&
            this.MissingLetters.Count == 0 || !this.MissingLetters.Contains(key))
               this.MissingLetters.Add(key);
         }
      }
      public Boolean CheckWordIfCorrect(String currentWord)
      {
         String givenWord = index.CurrentGame.getWord().ToString().ToUpper();
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
               //ChangeTileState(k, index.j, "correct");
            else
            if(partialMatch)
               tileStates[k-1] = "contained";
               //ChangeTileState(k, index.j, "contained");
            else
            if(!exactMatch && !partialMatch)
               tileStates[k-1] = "missing";
               //ChangeTileState(k, index.j, "missing");
         }
         CheckDoubles(currentWord, tileStates);
         for(int k = 0; k < index.NumLetters; k++)
            {
               ChangeKeyColour(currentWord[k].ToString(), tileStates[k]);
               ChangeTileState(k+1, index.j, tileStates[k]);
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
            var parameters = new ModalParameters();
            parameters.Add(nameof(GameModal.Title), "You Guessed The Word!");
            string guess = "You Guessed The Word: " + index.CurrentGame.getWord().ToString().ToUpper();
            parameters.Add(nameof(GameModal.Text), guess);
            var options = new ModalOptions() 
            { 
                  Animation = ModalAnimation.FadeInOut(1)
            };
            Modal.Show<GameModal>("", parameters, options);
            // show game won popup
            return true;
         }
         if(index.i == index.NumLetters && index.j == 6)
         {
            Console.WriteLine("The game has ended! You have not guessed the given word! The given word was: " + index.CurrentGame.getWord().ToString().ToUpper());
            var parameters = new ModalParameters();
            parameters.Add(nameof(GameModal.Title), "You Didn't Guess The Word!");
            string guess = "The Given Word Was: " + index.CurrentGame.getWord().ToString().ToUpper();
            parameters.Add(nameof(GameModal.Text), guess);
            var options = new ModalOptions() 
            { 
                  Animation = ModalAnimation.FadeInOut(1)
            };
            Modal.Show<GameModal>("", parameters, options);
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
                  // index.j = index.j + 1
                  // index.i = 1
         // else:
            // throw incorrect word exception
            // clear all tiles
            // index.i = 1

         // EDIT: IMPLEMENTED, CHECK IF IT'S RIGHT

         String CurrentWord = "";
         Boolean CompleteWord = false;
         if(index.i == index.NumLetters)
         {  
            foreach (Tile tile in tileList)
                  if (tile.tileId == index.j*10 + index.i && tile.Letter != "")
                     CompleteWord = true;
            if(CompleteWord == true)
            {
               for(int k = 1; k <= index.NumLetters; k++)
                  foreach (Tile tile in tileList)
                     if (tile.tileId == index.j*10 + k)
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
                     Console.WriteLine("The word has been submitted!");
                     index.i = 1;
                     index.j += 1;
                     BackspaceAllowed = false;
                  }
               }
               else
               {
                  //give inexistent word exception
                  //this.ClearRow(j);
                  Console.WriteLine("Word does not exist!");
                  ThrowAlertAnimation();
                  if(Width > 700){
                     ShowNotification(new NotificationMessage { Style = "position: absolute; left: -59vw; top:-3vw;", Severity = NotificationSeverity.Error, Summary = "Word doesn't exist. ", Detail = "Try again.", Duration = 3000 });
                  }else{
                     ShowNotification(new NotificationMessage { Style = "position: absolute; left: -77vw; top:-15vw; font-size:10px !important; width:100px !important; min-width: 220px; width:221px;", Severity = NotificationSeverity.Error, Summary = "Word doesn't exist. ", Detail = "Try again.", Duration = 3000 });
               
                  }
                 // if we want to clear the row we can use this:
                  //await js.InvokeVoidAsync("shakeFunction");
                 
                 
                  // give inexistent word exception
           
                  // if we want to clear the row we can use this:
                  // this.ClearRow(index.j);
                  // otherwise we don't do anything, just give the exception
               }
            }
            else
            {
               ThrowAlertAnimation();
               // give incomplete word exception
               Console.WriteLine("Incomplete word!");
               if(Width > 700){
                  ShowNotification(new NotificationMessage { Style = "position: absolute; left: -59vw; top:-3vw;", Severity = NotificationSeverity.Error, Summary = "Incomplete word!", Detail = "Try again.", Duration = 3000 });
               }else{
                  ShowNotification(new NotificationMessage { Style = "position: absolute; left: -77vw; top:-15vw; font-size:10px !important; width:100px !important; min-width: 220px; width:221px;", Severity = NotificationSeverity.Error, Summary = "Word doesn't exist. ", Detail = "Try again.", Duration = 3000 });
            
               }
            }     
         }
         else
         {
            // give incomplete word exception
            ThrowAlertAnimation();
            Console.WriteLine("Incomplete word!");
            if(Width > 700){
               ShowNotification(new NotificationMessage { Style = "position: absolute; left: -59vw; top:-3vw;", Severity = NotificationSeverity.Error, Summary = "Incomplete word!", Detail = "Try again.", Duration = 3000 });
            }else{
               ShowNotification(new NotificationMessage { Style = "position: absolute; left: -77vw; top:-15vw; font-size:10px !important; width:100px !important; min-width: 220px; width:221px;", Severity = NotificationSeverity.Error, Summary = "Word doesn't exist. ", Detail = "Try again.", Duration = 3000 });
            
            }
         }
      }
      
      private void AlphaKeyEventHandler(String key)
      {
         foreach (Tile tile in tileList)
         {
            if (tile.tileId == index.j*10 + index.i)
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
         if(index.i != index.NumLetters)
            index.i++;
      }
      private void BackspaceEventHandler()
      {
         // the decementation has to be done after the tile is found
         // as when it gets to the last tile index.i=5, index.j=6, it needs to delete
         // its content if there is one, otherwise, it need to decrement
         // the index.i again to tile index.i=4, index.j=6 and delete its content
         // quite a corner case

         // EDIT: PLEASE CHECK BUT index.i THINK index.i VE DONE IT
         if(BackspaceAllowed)
         {
            Boolean deleted = false;
            foreach (Tile tile in tileList)
            {  

               if (tile.tileId == index.j*10 + index.i)
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
         //if(deleted == false && index.i != 1 && index.i != 5) 
         if(deleted == false && index.i != 1)
         {
            index.i--;
            foreach (Tile tile in tileList)
            {
               if (tile.tileId == index.j*10 + index.i)
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
         if(index.i == 1)
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

      private void OnTimedEvent(object source, ElapsedEventArgs e)
      {
         int k = 1;
         foreach(Tile tile in this.tileList)
         {       
            if(tile.tileId == index.j*10 + k)
               tile.State = "default";
            k++;
         }
      }

   }

}

