using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.BaseModels
{
    public enum AccountsVariation
    {
        Admin,
        Worker,
        User,
        None
    }
    public interface ITypeOfAccount
    {
        public AccountsVariation GetAccountsVariation();
    }
}
