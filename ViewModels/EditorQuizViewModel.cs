using Quiz_App.Core;
using Quiz_App.DAL.Models;
using Quiz_App.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Quiz_App.ViewModels
{
    public class EditorQuizViewModel : ViewModelBase
    {
        private readonly IQuizService _quizService;
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;

        private Quiz _editedQuiz;

        public ICommand DeleteQuiz { get; }
        public ICommand CreateNewQuestion { get; }
        public ICommand EditQuestion { get; }
        public ICommand DeleteQuestion { get; }
        public ICommand EditTitleAndDesc { get; }
        public ICommand NavigateToHome { get; }

        public EditorQuizViewModel(Quiz editedQuiz)
        {
            _quizService = QuizService.Instance;
            _navigationService = NavigationService.Instance;
            _dialogService = DialogService.Instance;

            _editedQuiz = editedQuiz;

            Title = editedQuiz.Title;
            Description = editedQuiz.Description;
            Questions = editedQuiz.Questions;

            DeleteQuiz = new RelayCommand(_ => deleteQuiz());
            CreateNewQuestion = new RelayCommand(_ => createNewQuestion());
            EditQuestion = new RelayCommand(_ => editQuestion(), _ => SelectedQuestion != null);
            DeleteQuestion = new RelayCommand(_ => deleteQuestion(), _ => SelectedQuestion != null);
            EditTitleAndDesc = new RelayCommand(_ => editTitleAndDesc());
            NavigateToHome = new RelayCommand(_ => navigateToHome());
        }

        #region Properties
        private ObservableCollection<Question> _questions;
        public ObservableCollection<Question> Questions
        {
            get
            {
                return _questions;
            }
            set
            {
                _questions = value;
                OnPropertyChanged(nameof(Questions));
            }
        }
        private ObservableCollection<Answer> _selectedAnswers;
        public ObservableCollection<Answer> SelectedAnswers
        {
            get
            {
                return _selectedAnswers;
            }
            set
            {
                _selectedAnswers = value;
                OnPropertyChanged(nameof(SelectedAnswers));
            }
        }

        private Question _selectedQuestion;
        public Question SelectedQuestion
        {
            get
            {
                return _selectedQuestion;
            }
            set
            {
                _selectedQuestion = value;
                if (SelectedQuestion != null)
                    SelectedAnswers = SelectedQuestion.Answers;
                else
                    SelectedAnswers = null;

                OnPropertyChanged(nameof(SelectedQuestion));
            }
        }


        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Title));
            }
        }
        #endregion


        #region Commands

        private void deleteQuiz()
        {
            if (_quizService.DeleteQuiz(_editedQuiz))
            {
                var homeViewModel = new HomeViewModel();
                _navigationService.Navigate(homeViewModel);
            }
            else
            {
                _dialogService.ShowMessage("An error occurred while deleting the quiz, please try again!", "Error");
            }
        }

        private void createNewQuestion()
        {
            var newQuizViewModel = new EditorQuestionViewModel(_editedQuiz);
            _navigationService.Navigate(newQuizViewModel);
        }

        private void editQuestion()
        {
            var editorQuizViewModel = new EditorQuestionViewModel(_editedQuiz, SelectedQuestion);
            _navigationService.Navigate(editorQuizViewModel);
        }

        private void deleteQuestion()
        {
            if (!_quizService.DeleteQuestion(_editedQuiz, SelectedQuestion))
            {
                _dialogService.ShowMessage("An error occurred while deleting the quiz, please try again!", "Error");
            }
        }

        private void editTitleAndDesc()
        {
            var editorQuizViewModel = new EditorTDQuizViewModel(_editedQuiz);
            _navigationService.Navigate(editorQuizViewModel);
        }

        private void navigateToHome()
        {
            var newQuizViewModel = new HomeViewModel();
            _navigationService.Navigate(newQuizViewModel);
        }



        #endregion
    }
}
