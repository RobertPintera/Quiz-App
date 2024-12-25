using Quiz_App.DAL.Models;
using Quiz_App.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz_App.Services
{
    public class QuizService : IQuizService
    {
        private static QuizService? _instance;

        private readonly ObservableCollection<Quiz> _quizzes;
        private QuizService()
        {
            _quizzes = new ObservableCollection<Quiz>();
            _quizzes = QuizzesRepository.GetAllQuizesDataFromDb();
        }

        public static QuizService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new QuizService();
                }
                return _instance;
            }
        }

        public ObservableCollection<Quiz> GetQuizzes()
        {
            return _quizzes;
        }

        public bool AddQuiz(Quiz newQuiz)
        {
            if (QuizzesRepository.AddQuizToDb(newQuiz))
            {
                _quizzes.Add(newQuiz);
                return true;
            }

            return false;
        }

        public bool EditQuiz(Quiz quiz, string newTitle, string newDescription)
        {
            if (QuizzesRepository.EditQuizFromDb(quiz.Id, newTitle, newDescription))
            {
                quiz.Title = newTitle;
                quiz.Description = newDescription;
                return true;
            }
            return false;
        }

        public bool DeleteQuiz(Quiz quiz)
        {
            if (QuizzesRepository.DeleteQuizFromDb(quiz))
            {
                _quizzes.Remove(quiz);
                return true;
            }
            return false;
        }


        public bool AddQuestion(Quiz quiz, Question question)
        {
            if (QuestionsRepository.AddQuestionToDb(question))
            {
                quiz.Questions.Add(question);
                return true;
            }

            return false;
        }

        public bool EditQuestion(Question question, string newContent)
        {
            if (QuestionsRepository.EditQuestionFromDb(question.Id, newContent))
            {
                question.Content = newContent;
                return true;
            }

            return false;
        }

        public bool DeleteQuestion(Quiz quiz, Question question)
        {
            if (QuestionsRepository.DeleteQuestionFromDb(question))
            {
                quiz.Questions.Remove(question);
                return true;
            }
            return false;
        }

        public bool AddAnswer(Question question, Answer answer)
        {
            if (AnswersRepository.AddAnswerToDb(answer))
            {
                question.Answers.Add(answer);
                return true;
            }

            return false;
        }

        public bool EditAnswer(Answer answer, string content, bool isCorrect)
        {
            if (AnswersRepository.EditAnswerFromDb(answer.Id, content, isCorrect))
            {
                answer.Content = content;
                answer.IsCorrect = isCorrect;
                return true;
            }

            return false;
        }

        public bool DeleteAnswer(Question question, Answer answer)
        {
            if (AnswersRepository.DeleteAnswerFromDb(answer))
            {
                question.Answers.Add(answer);
                return true;
            }
            return false;
        }
    }
}
