using Quiz_App.Core;
using Quiz_App.DAL.Models;
using Quiz_App.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Quiz_App.ViewModels
{
    public class ResultViewModel : ViewModelBase
    {
        private readonly IQuizService _quizService;
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;

        private Quiz _solvedQuiz;
        private int _score;
        private TimeSpan _elapsedTime;

        public ICommand NavigateToHome { get; }

        public ResultViewModel(Quiz solvedQuiz, int score, TimeSpan elapsedTime)
        {
            _quizService = QuizService.Instance;
            _navigationService = NavigationService.Instance;
            _dialogService = DialogService.Instance;

            _solvedQuiz = solvedQuiz;
            _score = score;
            _elapsedTime = elapsedTime;

            NavigateToHome = new RelayCommand(_ => navigateToNewQuiz());
        }

        #region Properties
        public string ResultText =>
            $"Quiz: {_solvedQuiz.Title} \n" +
            $"Total time: {_elapsedTime.ToString(@"mm\:ss")} \n" +
            $"Score: {_score} of {_solvedQuiz.Questions.Count}";
        #endregion


        #region Commands
        private void navigateToNewQuiz()
        {
            var homeViewModel = new HomeViewModel();
            _navigationService.Navigate(homeViewModel);
        }
        #endregion
    }
}
