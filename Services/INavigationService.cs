using Quiz_App.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz_App.Services
{
    public interface INavigationService
    {
        void Navigate(ViewModelBase viewModel);
        event Action? CurrentViewModelChanged;
        ViewModelBase? CurrentViewModel { get; }
    }
}
