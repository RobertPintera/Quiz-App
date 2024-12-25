using Microsoft.EntityFrameworkCore;
using Quiz_App.DAL.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz_App.DAL.Repositories
{
    public class QuizzesRepository
    {
        public static ObservableCollection<Quiz> GetAllQuizesDataFromDb()
        {
            var list = new ObservableCollection<Quiz>();
            using (var db = new QuizDbContext())
            {
                var quizes = db.Quizzes.Include(q => q.Questions).ThenInclude(q => q.Answers).ToList();
                foreach (var q in quizes)
                {
                    list.Add(q);
                }
            }
            return list;
        }

        public static bool AddQuizToDb(Quiz quiz)
        {
            bool state = false;

            using (var db = new QuizDbContext())
            {
                if (quiz != null)
                {
                    db.Quizzes.Add(quiz);
                    state = true;
                    db.SaveChanges();
                }
            }
            return state;
        }

        public static bool EditQuizFromDb(int quizId, string newTitle, string newDescription)
        {
            bool state = false;

            using (var db = new QuizDbContext())
            {
                var quiz = db.Quizzes.Find(quizId);

                if (quiz != null)
                {
                    quiz.Title = newTitle;
                    quiz.Description = newDescription;
                    db.SaveChanges();
                    state = true;
                }
            }
            return state;
        }

        public static bool DeleteQuizFromDb(Quiz quiz)
        {
            bool state = false;

            using (var db = new QuizDbContext())
            {
                var quizToRemove = db.Quizzes.SingleOrDefault(q => q.Id == quiz.Id);
                if (quizToRemove != null)
                {
                    db.Quizzes.Remove(quizToRemove);
                    state = true;
                    db.SaveChanges();
                }
            }
            return state;
        }
    }
}
