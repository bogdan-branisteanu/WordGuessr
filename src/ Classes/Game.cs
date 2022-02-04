using System.Runtime.InteropServices;


namespace src.Classes {
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

            } else Console.WriteLine("Specified path for " + @Path + " does not exist!");
        }

        public WordInstance getWord()
        {
            return this.Word;
        }


    }
   
}