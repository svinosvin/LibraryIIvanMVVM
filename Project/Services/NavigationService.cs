using Project.Base;
using Project.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Services
{
    public class NavigationService<TViewModel>
    where TViewModel : ViewModel
    {
        private readonly NavigationStore _navigationStore;
        private readonly Func<TViewModel> _createVM;

        public NavigationService(NavigationStore navigationStore, Func<TViewModel> createVM)
        {
            _navigationStore = navigationStore;
            _createVM = createVM;
     
        }

        public void Navigate()
        {
            _navigationStore.CurrentViewModel = _createVM.Invoke();
        }
    }
}
