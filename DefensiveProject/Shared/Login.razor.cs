using DefensiveProject.Authentication;
using Microsoft.AspNetCore.Components;


namespace DefensiveProject.Shared
{
    public partial class Login
    {
        [Parameter] public EventCallback onCancel { get; set; }

        bool isLogin = false;
        string message = string.Empty;
        private string Email = string.Empty;
        private string Password = string.Empty;
        public void Show() => isLogin = true;
        public void Hide() => isLogin = false;

        public List<UserAccount> users = new List<UserAccount>();
        protected override void OnInitialized()
        {
            List<UserAccount> users = UserAccount.GetListDB();
        }
        private async Task Authenticate()
        {
            var userAccount = UserAccount.Authorization(Email, Password);
            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                message = "Пожалуйста, заполните все поля!";
                Email = string.Empty;
                Password = string.Empty;
            }
            if (userAccount == null)
            {
                message = "Неверный логин или пароль!";
                Email = string.Empty;
                Password = string.Empty;
            }
            else
            {
                var customAuthStateProvider = (SettingAuthentication)authStateProvider;
                await customAuthStateProvider.UpdateAuthenticationState(new UserSession
                {
                    UserName = userAccount.Login,
                    Role = userAccount.Role
                });
                navManager.NavigateTo("/", true);
                //NameLogin = string.Empty;
                //Password = string.Empty;
                //Hide();
            }


        }
    }
}
