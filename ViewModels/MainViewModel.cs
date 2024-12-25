using Quiz_App.Core;
using Quiz_App.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz_App.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        public ViewModelBase? CurrentViewModel => _navigationService.CurrentViewModel;

        public MainViewModel()
        {
            _navigationService = NavigationService.Instance;
            _navigationService.CurrentViewModelChanged += () => OnPropertyChanged(nameof(CurrentViewModel));
            _navigationService.Navigate(new HomeViewModel());
        }
    }
}
