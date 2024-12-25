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
    public class EditorQuestionViewModel : ViewModelBase
    {
        private readonly IQuizService _quizService;
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;

        private Quiz _editedQuiz;
        private Question _editedQuestion;
        private ObservableCollection<KeyValuePair<string, bool>> _answers = new ObservableCollection<KeyValuePair<string, bool>>();

        public ICommand Cancel { get; }
        public ICommand Accept { get; }

        public EditorQuestionViewModel(Quiz editedQuiz, Question? editedQuestion = null)
        {
            _quizService = QuizService.Instance;
            _navigationService = NavigationService.Instance;
            _dialogService = DialogService.Instance;

            _editedQuiz = editedQuiz;
            _editedQuestion = editedQuestion;

            if (_editedQuestion != null)
            {
                ContentQuestion = _editedQuestion.Content;
                Content1 = _editedQuestion.Answers[0].Content;
                IsCorrect1 = _editedQuestion.Answers[0].IsCorrect;
                Content2 = _editedQuestion.Answers[1].Content;
                IsCorrect2 = _editedQuestion.Answers[1].IsCorrect;
                Content3 = _editedQuestion.Answers[2].Content;
                IsCorrect3 = _editedQuestion.Answers[2].IsCorrect;
                Content4 = _editedQuestion.Answers[3].Content;
                IsCorrect4 = _editedQuestion.Answers[3].IsCorrect;
                Header = "Edit Question";
            }
            else
            {
                Header = "New Question";
            }

            _answers.Add(new KeyValuePair<string, bool>(_content1, _isCorrect1));
            _answers.Add(new KeyValuePair<string, bool>(_content2, _isCorrect2));
            _answers.Add(new KeyValuePair<string, bool>(_content3, _isCorrect3));
            _answers.Add(new KeyValuePair<string, bool>(_content4, _isCorrect4));

            Accept = new RelayCommand(_ => accept(), _ => isCompleted());
            Cancel = new RelayCommand(_ => cancel());
        }

        #region Properties
        private string _header;
        public string Header
        {
            get { return _header; }
            set
            {
                _header = value;
                OnPropertyChanged(nameof(Header));
            }
        }

        private string _contentQuestion;
        public string ContentQuestion
        {
            get { return _contentQuestion; }
            set
            {
                _contentQuestion = value;
                OnPropertyChanged(nameof(ContentQuestion));
            }
        }

        private string _content1;
        public string Content1
        {
            get { return _content1; }
            set
            {
                _content1 = value;
                OnPropertyChanged(nameof(Content1));
            }
        }

        private bool _isCorrect1;
        public bool IsCorrect1
        {
            get { return _isCorrect1; }
            set
            {
                _isCorrect1 = value;
                OnPropertyChanged(nameof(IsCorrect1));
            }
        }

        private string _content2;
        public string Content2
        {
            get { return _content2; }
            set
            {
                _content2 = value;
                OnPropertyChanged(nameof(Content2));
            }
        }

        private bool _isCorrect2;
        public bool IsCorrect2
        {
            get { return _isCorrect2; }
            set
            {
                _isCorrect2 = value;
                OnPropertyChanged(nameof(IsCorrect2));
            }
        }

        private string _content3;
        public string Content3
        {
            get { return _content3; }
            set
            {
                _content3 = value;
                OnPropertyChanged(nameof(Content3));
            }
        }

        private bool _isCorrect3;
        public bool IsCorrect3
        {
            get { return _isCorrect3; }
            set
            {
                _isCorrect3 = value;
                OnPropertyChanged(nameof(IsCorrect3));
            }
        }

        private string _content4;
        public string Content4
        {
            get { return _content4; }
            set
            {
                _content4 = value;
                OnPropertyChanged(nameof(Content4));
            }
        }

        private bool _isCorrect4;
        public bool IsCorrect4
        {
            get { return _isCorrect4; }
            set
            {
                _isCorrect4 = value;
                OnPropertyChanged(nameof(IsCorrect4));
            }
        }
        #endregion

        #region Commands

        private void cancel()
        {
            _navigationService.Navigate(new EditorQuizViewModel(_editedQuiz));
        }

        private void accept()
        {
            if (_editedQuestion == null)
            {
                Question newQuestion = new Question { IdQuiz = _editedQuiz.Id, Content = ContentQuestion, Answers = new ObservableCollection<Answer>() };

                if (_quizService.AddQuestion(_editedQuiz, newQuestion))
                {
                    Answer answer1 = new Answer { IdQuestion = newQuestion.Id, Content = Content1, IsCorrect = IsCorrect1 };
                    Answer answer2 = new Answer { IdQuestion = newQuestion.Id, Content = Content2, IsCorrect = IsCorrect2 };
                    Answer answer3 = new Answer { IdQuestion = newQuestion.Id, Content = Content3, IsCorrect = IsCorrect3 };
                    Answer answer4 = new Answer { IdQuestion = newQuestion.Id, Content = Content4, IsCorrect = IsCorrect4 };

                    _quizService.AddAnswer(newQuestion, answer1);
                    _quizService.AddAnswer(newQuestion, answer2);
                    _quizService.AddAnswer(newQuestion, answer3);
                    _quizService.AddAnswer(newQuestion, answer4);

                    _navigationService.Navigate(new EditorQuizViewModel(_editedQuiz));
                }
                else
                {
                    _dialogService.ShowMessage("An error occurred while creating the question, please try again!", "Error");
                }
            }
            else
            {
                if (_quizService.EditQuestion(_editedQuestion, ContentQuestion))
                {
                    if (_editedQuestion.Answers.Count == 4)
                    {
                        _quizService.EditAnswer(_editedQuestion.Answers[0], Content1, IsCorrect1);
                        _quizService.EditAnswer(_editedQuestion.Answers[1], Content2, IsCorrect2);
                        _quizService.EditAnswer(_editedQuestion.Answers[2], Content3, IsCorrect3);
                        _quizService.EditAnswer(_editedQuestion.Answers[3], Content4, IsCorrect4);
                        _navigationService.Navigate(new EditorQuizViewModel(_editedQuiz));
                    }
                    else
                    {
                        _quizService.DeleteQuestion(_editedQuiz, _editedQuestion);
                        _dialogService.ShowMessage("Something has gone wrong! The question is deleted!", "Error");
                    }
                }
                else
                {
                    _dialogService.ShowMessage("An error occurred while editing the question, please try again!", "Error");
                }
            }
        }
        #endregion

        private bool isCompleted()
        {
            return (IsCorrect1 || IsCorrect2 || IsCorrect3 || IsCorrect4) &&
                !string.IsNullOrWhiteSpace(ContentQuestion) &&
                !string.IsNullOrWhiteSpace(Content1) &&
                !string.IsNullOrWhiteSpace(Content2) &&
                !string.IsNullOrWhiteSpace(Content3) &&
                !string.IsNullOrWhiteSpace(Content4);
        }
    }
}
