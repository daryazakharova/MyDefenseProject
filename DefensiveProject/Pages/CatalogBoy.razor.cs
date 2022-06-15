using DefensiveProject.Authentication;
using DefensiveProject.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace DefensiveProject.Pages
{
    public partial class CatalogBoy
    {
        [CascadingParameter]
        private Task<AuthenticationState> authenticationState { get; set; }
        private string Gender = "Мальчик";
        private bool isModal = false;
        private string Message = string.Empty;
        private int Size;
        UnitOfGoods product = new UnitOfGoods();
        public List<UnitOfGoods> products = new List<UnitOfGoods>();
        public List<UnitOfGoods> tShirtsLongsleeves = new List<UnitOfGoods>();
        public List<UnitOfGoods> shorts = new List<UnitOfGoods>();
        public List<UnitOfGoods> trousersJeans = new List<UnitOfGoods>();
        public List<UnitOfGoods> cardigansHoodies = new List<UnitOfGoods>();

        protected override void OnInitialized()
        {
            products = UnitOfGoods.GetProductsInGender(Gender);
            tShirtsLongsleeves = UnitOfGoods.ShowProductsInCategory(Gender, "Футболки и лонгсливы");
            shorts = UnitOfGoods.ShowProductsInCategory(Gender, "Шорты");
            cardigansHoodies = UnitOfGoods.ShowProductsInCategory(Gender, "Худи и толстовки");
            trousersJeans = UnitOfGoods.ShowProductsInCategory(Gender, "Брюки и джинсы");
        }
        private async Task<UserAccount> GetAuthorizedUser()
        {
            var authState = await authenticationState;
            string userName = authState.User.Identity.Name;
            UserAccount authorizedUser = UserAccount.GetUserLogin(userName);
            return authorizedUser;
        }

        private void OpenModal(int article)
        {
            isModal = true;
            product = UnitOfGoods.GetArticleClothes(article);
        }
        private void onCancel()
        {
            isModal = false;
        }
        private void OnClick()
        {
            Message = "Чтобы добавить в корзину необходимо авторизоваться!";
        }

        private void OnClick104()
        {
            Size = 104;
        }
        private void OnClick110()
        {
            Size = 110;
        }
        private void OnClick116()
        {
            Size = 116;
        }
        private void OnClick128()
        {
            Size = 128;
        }
        private void AddToBasket(int article)
        {
            product = UnitOfGoods.GetArticleClothes(article);
            UserAccount user = UserAccount.GetUserLogin(GetAuthorizedUser().GetAwaiter().GetResult().Login);
            user.AddToBasket(product.Category, product.ArticleProduct, Size, product.quantity);
            Message = "Товар добавлен в корзину!"; 
            UserAccount.ReplaceUser(new UserAccount(user.Login,user.Email, user.Password, user.Role,user.NumberPhone, user.busketUser, user.shoppingList));
            Message = string.Empty;
            onCancel();
        }
    }
}
