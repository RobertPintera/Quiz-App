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
    public class EditorTDQuizViewModel : ViewModelBase
    {
        private readonly IQuizService _quizService;
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;
        private readonly Quiz? _editedQuiz;

        public ICommand Accept { get; }
        public ICommand Cancel { get; }

        public EditorTDQuizViewModel(Quiz? editedQuiz = null)
        {
            _quizService = QuizService.Instance;
            _navigationService = NavigationService.Instance;
            _dialogService = DialogService.Instance;

            _editedQuiz = editedQuiz;

            if (editedQuiz != null)
            {
                Title = editedQuiz.Title;
                Description = editedQuiz.Description;
                Header = "Edit Quiz";
            }
            else
            {
                Header = "New Quiz";
            }


            Accept = new RelayCommand(_ => accept(), _ => !string.IsNullOrWhiteSpace(Title) && !string.IsNullOrWhiteSpace(Description));
            Cancel = new RelayCommand(_ => cancel());
        }

        #region Properties
        private string _header;
        public string Header
        {
            get { return _header; }
            set { _header = value; OnPropertyChanged(nameof(Header)); }
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { _title = value; OnPropertyChanged(nameof(Title)); }
        }

        private string _description;

        public string Description
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged(nameof(Description)); }
        }
        #endregion

        #region Commands
        private void accept()
        {
            if (_editedQuiz == null)
            {
                Quiz newQuiz = new Quiz { Title = Title, Description = Description, Questions = new ObservableCollection<Question>() };

                if (_quizService.AddQuiz(newQuiz))
                {
                    _navigationService.Navigate(new EditorQuizViewModel(newQuiz));
                }
                else
                {
                    _dialogService.ShowMessage("An error occurred while creating the quiz, please try again!", "Error");
                }
            }
            else
            {
                if (_quizService.EditQuiz(_editedQuiz, Title, Description))
                {
                    _navigationService.Navigate(new EditorQuizViewModel(_editedQuiz));
                }
                else
                {
                    _dialogService.ShowMessage("An error occurred while editing the quiz, please try again!", "Error");
                }
            }

        }

        private void cancel()
        {
            if (_editedQuiz == null)
            {
                _navigationService.Navigate(new HomeViewModel());
            }
            else
            {
                _navigationService.Navigate(new EditorQuizViewModel(_editedQuiz));
            }
        }
        #endregion
    }
}
