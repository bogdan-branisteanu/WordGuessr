using System;  
using System.IO; 

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
            case 3:
               Path = String.Concat(Directory.GetCurrentDirectory(), @"\resources\threeLetterWords.txt");
            break;
            case 4:
               Path = String.Concat(Directory.GetCurrentDirectory(), @"\resources\fourLetterWords.txt");
            break;
            case 5:
               Path = String.Concat(Directory.GetCurrentDirectory(), @"\resources\fiveLetterWords.txt");
            break;
            case 6:
               Path = String.Concat(Directory.GetCurrentDirectory(), @"\resources\sixLetterWords.txt");
            break;
            case 7:
               Path = String.Concat(Directory.GetCurrentDirectory(), @"\resources\sevenLetterWords.txt");
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
            this.Word = new WordInstance(lines[index]);
            this.Word.checkProfanity(String.Concat(Directory.GetCurrentDirectory(), @"\resources\profanityWords.txt"));
            while(this.Word.ProfanitySafe == false)
            {
               index = rdn.Next(0, lines.Length);
               this.Word = new WordInstance(lines[index]);
               this.Word.checkProfanity(String.Concat(Directory.GetCurrentDirectory(), @"\resources\profanityWords.txt"));
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
      int NumLetters {set; get;}
     
      public void runGame()
      {
         Console.WriteLine(this.NumLetters);
         if(this.NumLetters == 0)
         {
            Random rdn = new Random();
            this.NumLetters = rdn.Next(3,8);
         }
         Game newGame = new Game(this.NumLetters);
         newGame.chooseWord();
         Console.WriteLine(newGame.getWord());
         
      }

      public void setNumLetters(int numLetters)
      {
         this.NumLetters = numLetters;
      }

   }

}

