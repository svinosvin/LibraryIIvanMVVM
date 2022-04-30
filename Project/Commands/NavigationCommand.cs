using Project.Base;
using Project.Commands.Helpers;
using Project.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Project.Commands
{
    public class NavigationCommand<TViewModel> : CommandBase
        where TViewModel : ViewModel
    {
        private readonly NavigationStore _navigationStore;
        private readonly Func<TViewModel> _createVM;

        public NavigationCommand(NavigationStore navigationStore, Func<TViewModel> createVM)
        {
            _navigationStore = navigationStore;
            _createVM = createVM;
        }

        public override void Execute(object? parameter)
        {
            _navigationStore.CurrentViewModel = _createVM.Invoke();
        }
    }

}
