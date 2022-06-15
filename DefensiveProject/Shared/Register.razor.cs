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
        private string Message = string.Empty;
        private string Message2 = string.Empty;

        UserAccount user;
        protected override void OnInitialized()
        {
            user = new UserAccount();

        }
        private void Registrated()
        {
            var user2 = UserAccount.GetUserLogin(user.Login);
            var user3 = UserAccount.GetUserAccount(user.Email);
            if (user2 == null)
            {
                Message = "Данный логин свободен!";
            }
            if (user3 == null)
            {
                Message2 = "Данная почта не зарегистрированна!";
            }
            if (user2 == null && user3 == null)
            {
                UserAccount.AddMongoDB(new UserAccount(user.Login, user.Email, user.Password, user.Role));
                Authenticate();
                Hide();
            }
            else if (user.Login == user2.Login && user3 == null)
            {
                Message2 = "Введённый вами логин уже зарегистрирован!";
            }
            else if (user.Email == user3.Email && user2 == null)
            {
                Message = "Введённый вами e-mail уже зарегистрирован!";
            }
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


