using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Classes.Services
{
    public interface IDelegateCommand : ICommand
    {
         public void RaiseCanExecuteChange();  

    }
}
