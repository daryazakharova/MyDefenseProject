using DefensiveProject.Authentication;
using Microsoft.AspNetCore.Components;

namespace DefensiveProject.Shared
{
    public partial class Register
    {
        [Parameter] public EventCallback onCancel { get; set; }

        bool isRegistration = false;
        public void Show() => isRegistration = true;
        public void Hide() => isRegistration = false;

        UserAccount user;
        protected override void OnInitialized()
        {
            user = new UserAccount();
        }
        private void Registrated()
        {
            UserAccount.AddMongoDB(new UserAccount(user.Login, user.Email, user.Password, user.Role));
            Authenticate();
            Hide();
        }
        private async Task Authenticate()
        {
            var customAuthStateProvider = (SettingAuthentication)authStateProvider;
            await customAuthStateProvider.UpdateAuthenticationState(new UserSession
            {
                UserName = user.Login,
                Role = user.Role
            });
            navManager.NavigateTo("/", true);
        }
    }      
}


