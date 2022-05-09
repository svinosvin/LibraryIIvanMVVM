using Models.BaseModels;
using Project.Services.AunthenticationService;
using Project.Stores;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Project.Services.AccountService
{


    public interface ICurrentAccountService
    {
        public ITypeOfAccount CurrentAccount { get;}
        
        public AccountsVariation Variation { get; }
        public bool isLoggedIn { get; }

        public Task<RegisterResult> Register(string username, string password, string confirmPassword, string email, string telnumber, AccountsVariation variation);

        public Task<bool> Login(string username, string password, string confirmPassword,bool variation);
        public ICommand GetAuthorizeCommand(NavigationStore _navigationStore);
        public void Logout();

    }
}
