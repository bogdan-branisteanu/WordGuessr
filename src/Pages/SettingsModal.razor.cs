using Blazored.Modal.Services;
using Blazored.Modal;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace src.Pages{

    
    public partial class SettingsModal{

    [CascadingParameter] BlazoredModalInstance BlazoredModal { get; set; }
    [Parameter]public string Title { get; set; }
    [Parameter]public string Text { get; set;}
    [Inject] private NavigationManager NavigationManager { get; set; }

    public void SettingsChangeColorblind(Object obj){
       // js.InvokeVoidAsync("JsColorblind");
    }

    

    async Task Close() => await BlazoredModal.CloseAsync(ModalResult.Ok(true));

    }
   
}