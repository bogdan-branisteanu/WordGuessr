using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components;

namespace src.Pages
{
    public partial class Index 
    {
        int numLetters = 5;
        private ElementReference inputDiv;

        protected async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await inputDiv.FocusAsync();
            }
        }

        private void KeyboardEventHandler(KeyboardEventArgs args)
        {
            Console.WriteLine("Key Pressed is " + args.Key);
        }

        





    }
}