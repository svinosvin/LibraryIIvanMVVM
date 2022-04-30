using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Project.Commands.Helpers
{
    public interface IDelegateCommand : ICommand
    {
         public void RaiseCanExecuteChange();  

    }
}
