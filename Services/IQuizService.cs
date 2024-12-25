using Quiz_App.DAL.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz_App.Services
{
    public interface IQuizService
    {
        ObservableCollection<Quiz> GetQuizzes();
        bool AddQuiz(Quiz quiz);
        bool EditQuiz(Quiz quiz, string newTitle, string newDescription);
        bool DeleteQuiz(Quiz quiz);
        bool AddQuestion(Quiz quiz, Question question);
        bool EditQuestion(Question question, string newContent);
        bool DeleteQuestion(Quiz quiz, Question question);
        bool AddAnswer(Question question, Answer answer);
        bool EditAnswer(Answer answer, string content, bool isCorrect);
        bool DeleteAnswer(Question question, Answer answer);
    }
}
