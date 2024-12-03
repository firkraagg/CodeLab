using CodeHub.Data.Entities;
using CodeHub.Data.Models;
using CodeHub.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace CodeHub.Components.Pages
{
    public partial class Register
    {
        [Inject] private UserService userService { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }
        [SupplyParameterFromForm] private RegistrationModel rm { get; set; } = new();

        private bool _showAlert;
        private string _alertMessage = String.Empty;
        private string _alertColor = "alert-danger";

        public async Task HandleFormSubmitAsync(EditContext editContext)
        {
            _alertColor = "alert-danger";

            if (await userService.FindByUsernameAsync(rm.Username) != null)
            {
                _alertMessage = "Pou��vate� s t�mto pou��vate�sk�m menom u� existuje. Zvo�te in� pou��vate�sk� meno.";
                _showAlert = true;
            }
            else if (await userService.FindByEmailAsync(rm.Email) != null)
            {
                _alertMessage = "Pou��vate� s touto e-mailovou adresou u� existuje. Skontrolujte zadan� e-mail alebo sa prihl�ste.";
                _showAlert = true;
            }
            else
            {
                var newUser = (RegistrationModel)editContext.Model;
                await userService.CreateUserFromRegistrationModelAsync(newUser);
                NavigationManager.NavigateTo("/login?message=registered");
            }
        }
    }
}