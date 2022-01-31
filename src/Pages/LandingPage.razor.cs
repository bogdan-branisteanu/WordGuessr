using Microsoft.AspNetCore.Components;

namespace src.Pages{
    public partial class LandingPage{
        string NumLettersString { get; set; } = "5";

        [Inject] private NavigationManager NavigationManager { get; set; }

        public void GameRedirect(){
            NavigationManager.NavigateTo("/game/" + NumLettersString);
        }

    }
}