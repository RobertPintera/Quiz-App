using Quiz_App.Core;
using Quiz_App.DAL.Models;
using Quiz_App.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace Quiz_App.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        private readonly IQuizService _quizService;
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;

        public ICommand SearchQuiz { get; }
        public ICommand NavigateToNewQuiz { get; }
        public ICommand NavigateToEditor { get; }
        public ICommand NavigateToQuiz { get; }

        public HomeViewModel()
        {
            _quizService = QuizService.Instance;
            _navigationService = NavigationService.Instance;
            _dialogService = DialogService.Instance;

            Quizzes = _quizService.GetQuizzes();
            QuizzesView = CollectionViewSource.GetDefaultView(Quizzes);

            SearchQuiz = new RelayCommand(_ => searchQuiz());
            NavigateToNewQuiz = new RelayCommand(_ => navigateToNewQuiz());
            NavigateToEditor = new RelayCommand(_ => navigateToEditorQuiz(), _ => SelectedQuiz != null);
            NavigateToQuiz = new RelayCommand(_ => navigateToSolveQuiz(), _ => SelectedQuiz != null);
        }

        #region Properties
        public ObservableCollection<Quiz> Quizzes { get; set; }
        public ICollectionView QuizzesView { get; }

        private Quiz _selectedQuiz;
        public Quiz SelectedQuiz
        {
            get
            {
                return _selectedQuiz;
            }
            set
            {
                _selectedQuiz = value;
                if (SelectedQuiz != null)
                    DescriptionSelectedQuiz = SelectedQuiz.Description;
                else
                    DescriptionSelectedQuiz = "";
                OnPropertyChanged(nameof(SelectedQuiz));
            }
        }

        private string _descriptionSelectedQuiz;
        public string DescriptionSelectedQuiz
        {
            get { return _descriptionSelectedQuiz; }
            set
            {
                _descriptionSelectedQuiz = value;
                OnPropertyChanged(nameof(DescriptionSelectedQuiz));
            }
        }

        private string _filterText;
        public string FilterText
        {
            get { return _filterText; }
            set
            {
                _filterText = value;
                OnPropertyChanged(nameof(FilterText));
            }
        }

        #endregion


        #region Commands
        private void searchQuiz()
        {
            QuizzesView.Filter = FilterQuizzes;
            QuizzesView.Refresh();
        }

        private void navigateToNewQuiz()
        {
            var newQuizViewModel = new EditorTDQuizViewModel();
            _navigationService.Navigate(newQuizViewModel);
        }

        private void navigateToEditorQuiz()
        {
            var editorQuizViewModel = new EditorQuizViewModel(SelectedQuiz);
            _navigationService.Navigate(editorQuizViewModel);
        }

        private void navigateToSolveQuiz()
        {
            if (SelectedQuiz.Questions.Count > 0)
            {
                var quizViewModel = new SolveQuizViewModel(SelectedQuiz);
                _navigationService.Navigate(quizViewModel);
            }
            else
            {
                _dialogService.ShowMessage("The quiz has no questions!", "Error");
            }
        }

        #endregion

        private bool FilterQuizzes(object item)
        {
            if (item is Quiz quiz)
            {
                return string.IsNullOrEmpty(FilterText) || quiz.Title.Contains(FilterText, StringComparison.OrdinalIgnoreCase);
            }
            return false;
        }
    }
}
