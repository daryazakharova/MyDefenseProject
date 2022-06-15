using DefensiveProject.Authentication;
using DefensiveProject.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace DefensiveProject.Pages
{
    public partial class Basket
    {
        [CascadingParameter]
        private Task<AuthenticationState> authenticationState { get; set; }
        private string Message = string.Empty;
        private double Currency;
        UnitOfGoods product = new UnitOfGoods();
        public List<UnitOfGoods> products = new List<UnitOfGoods>();
        public BasketUser busket = new BasketUser();
        UserAccount user = new UserAccount();

        private async Task<UserAccount> GetAuthorizedUser()
        {
            var authState = await authenticationState;
            string userName = authState.User.Identity.Name;
            UserAccount authorizedUser = UserAccount.GetUserLogin(userName);
            return authorizedUser;
        }

        private void DeleteOne(int article)
        {
            user = UserAccount.GetUserLogin(user.Login);
            products = BasketUser.GetCart(user.Login).ListInCart;
            if (products.Exists(x => x.ArticleProduct == article))
            {
                product = products.Find(x => x.ArticleProduct == article);
                user.DeleteOneBasket(product.ArticleProduct, product.size, user.Login);
                UserAccount.ReplaceUser(new UserAccount(user.Login, user.Email, user.Password, user.Role, user.NumberPhone, user.busketUser, user.shoppingList));
                products = BasketUser.GetCart(user.Login).ListInCart;
                Currency = BasketUser.GetCart(user.Login).Currency;
            }
        }

        private void DeleteAll()
        {
            user.DeleteBasket(user.Login);
            UserAccount.ReplaceUser(new UserAccount(user.Login, user.Email, user.Password, user.Role, user.NumberPhone, user.busketUser, user.shoppingList));
            products = BasketUser.GetCart(user.Login).ListInCart;
            Currency = BasketUser.GetCart(user.Login).Currency;
            Message = "Ваша корзина пуста!";
        }

        private void Buying()
        {
            foreach (var item in products)
            {
                user.Buy(item.ArticleProduct, item.size, item.quantity);
                UserAccount.ReplaceUser(new UserAccount(user.Login, user.Email, user.Password, user.Role, user.NumberPhone, user.busketUser, user.shoppingList));
            }
            user.DeleteBasket(user.Login);
            UserAccount.ReplaceUser(new UserAccount(user.Login, user.Email, user.Password, user.Role, user.NumberPhone, user.busketUser, user.shoppingList));
            products.Clear();
            Currency = 0;
        }
       
        
    }
}
