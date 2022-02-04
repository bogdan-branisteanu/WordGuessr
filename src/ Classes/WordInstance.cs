using Microsoft.JSInterop;


namespace src.Classes{

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
                    this.ProfanitySafe = !(this.Word.Equals(line.ToUpper()));
            
            } else Console.WriteLine("Specified path for " + path + " does not exist!");
        }

    }

}