using Quiz_App.Core;
using Quiz_App.DAL.Models;
using Quiz_App.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace Quiz_App.ViewModels
{
    public class SolveQuizViewModel : ViewModelBase
    {
        private readonly IQuizService _quizService;
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;

        private Quiz _solvedQuiz;
        private Question _selectedQuestion;

        private DispatcherTimer _timer;


        public ICommand Next { get; }
        public ICommand EndQuiz { get; }

        public SolveQuizViewModel(Quiz solvedQuiz)
        {
            _quizService = QuizService.Instance;
            _navigationService = NavigationService.Instance;
            _dialogService = DialogService.Instance;

            _solvedQuiz = solvedQuiz;
            _title = solvedQuiz.Title;
            _score = 0;
            _questionNumber = $"Question 1 of {solvedQuiz.Questions.Count}";
            _nextText = "Next";

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += OnTimerTick;
            _elapsedTime = TimeSpan.Zero;
            _timer.Start();

            Next = new RelayCommand(_ => next());
            EndQuiz = new RelayCommand(_ => endQuiz());

            _idQuestion = 0;
            _selectedQuestion = solvedQuiz.Questions[_idQuestion];
            _contentQuestion = _selectedQuestion.Content;
            _content1 = _selectedQuestion.Answers[0].Content;
            _content2 = _selectedQuestion.Answers[1].Content;
            _content3 = _selectedQuestion.Answers[2].Content;
            _content4 = _selectedQuestion.Answers[3].Content;
        }

        #region Properties
        private int _idQuestion;

        private string _title;
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        private TimeSpan _elapsedTime;
        public TimeSpan ElapsedTime
        {
            get => _elapsedTime;
            set
            {
                _elapsedTime = value;
                OnPropertyChanged(nameof(TotalTime));
            }
        }

        public string TotalTime => ElapsedTime.ToString(@"mm\:ss");


        private string _nextText;
        public string NextText
        {
            get
            {
                return _nextText;
            }
            set
            {
                _nextText = value;
                OnPropertyChanged(nameof(NextText));
            }
        }
        private string _questionNumber;
        public string QuestionNumber
        {
            get
            {
                return _questionNumber;
            }
            set
            {
                _questionNumber = value;
                OnPropertyChanged(nameof(QuestionNumber));
            }
        }

        private int _score;
        public int Score
        {
            get
            {
                return _score;
            }
            set
            {
                _score = value;
                OnPropertyChanged(nameof(ScoreText));
            }
        }

        public string ScoreText => $"Score: {_score}";

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

        private bool _isChecked1;
        public bool IsChecked1
        {
            get { return _isChecked1; }
            set
            {
                _isChecked1 = value;
                OnPropertyChanged(nameof(IsChecked1));
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

        private bool _isChecked2;
        public bool IsChecked2
        {
            get { return _isChecked2; }
            set
            {
                _isChecked2 = value;
                OnPropertyChanged(nameof(IsChecked2));
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

        private bool _isChecked3;
        public bool IsChecked3
        {
            get { return _isChecked3; }
            set
            {
                _isChecked3 = value;
                OnPropertyChanged(nameof(IsChecked3));
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

        private bool _isChecked4;
        public bool IsChecked4
        {
            get { return _isChecked4; }
            set
            {
                _isChecked4 = value;
                OnPropertyChanged(nameof(IsChecked4));
            }
        }
        #endregion

        #region Command

        private void next()
        {
            if (_idQuestion >= _solvedQuiz.Questions.Count - 1)
            {
                if (IsChecked1 == _selectedQuestion.Answers[0].IsCorrect &&
                    IsChecked2 == _selectedQuestion.Answers[1].IsCorrect &&
                    IsChecked3 == _selectedQuestion.Answers[2].IsCorrect &&
                    IsChecked4 == _selectedQuestion.Answers[3].IsCorrect)
                {
                    Score++;
                }

                _timer.Stop();
                _navigationService.Navigate(new ResultViewModel(_solvedQuiz, _score, _elapsedTime));
            }
            else
            {
                if (IsChecked1 == _selectedQuestion.Answers[0].IsCorrect &&
                    IsChecked2 == _selectedQuestion.Answers[1].IsCorrect &&
                    IsChecked3 == _selectedQuestion.Answers[2].IsCorrect &&
                    IsChecked4 == _selectedQuestion.Answers[3].IsCorrect)
                {
                    Score++;
                }

                _idQuestion++;
                _selectedQuestion = _solvedQuiz.Questions[_idQuestion];
                ContentQuestion = _selectedQuestion.Content;

                Content1 = _selectedQuestion.Answers[0].Content;
                Content2 = _selectedQuestion.Answers[1].Content;
                Content3 = _selectedQuestion.Answers[2].Content;
                Content4 = _selectedQuestion.Answers[3].Content;
                IsChecked1 = false;
                IsChecked2 = false;
                IsChecked3 = false;
                IsChecked4 = false;
                QuestionNumber = $"Question {_idQuestion + 1} of {_solvedQuiz.Questions.Count}";
            }

        }

        private void endQuiz()
        {
            if (IsChecked1 == _selectedQuestion.Answers[0].IsCorrect &&
                    IsChecked2 == _selectedQuestion.Answers[1].IsCorrect &&
                    IsChecked3 == _selectedQuestion.Answers[2].IsCorrect &&
                    IsChecked4 == _selectedQuestion.Answers[3].IsCorrect)
            {
                Score++;
            }

            _timer.Stop();
            _navigationService.Navigate(new ResultViewModel(_solvedQuiz, _score, _elapsedTime));
        }

        #endregion

        private void OnTimerTick(object sender, EventArgs e)
        {
            ElapsedTime = _elapsedTime.Add(TimeSpan.FromSeconds(1));
        }
    }
}
