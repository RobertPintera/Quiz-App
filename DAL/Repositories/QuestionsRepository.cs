using Quiz_App.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz_App.DAL.Repositories
{
    public class QuestionsRepository
    {
        public static bool AddQuestionToDb(Question question)
        {
            bool state = false;

            using (var db = new QuizDbContext())
            {
                if (question != null)
                {
                    db.Questions.Add(question);
                    db.SaveChanges();
                    state = true;
                }
            }
            return state;
        }

        public static bool EditQuestionFromDb(int questionId, string newContent)
        {
            bool state = false;

            using (var db = new QuizDbContext())
            {
                var question = db.Questions.Find(questionId);

                if (question != null)
                {
                    question.Content = newContent;
                    db.SaveChanges();
                    state = true;
                }
            }
            return state;
        }

        public static bool DeleteQuestionFromDb(Question question)
        {
            bool state = false;

            using (var db = new QuizDbContext())
            {
                var questionToRemove = db.Questions.SingleOrDefault(q => q.Id == question.Id);
                if (questionToRemove != null)
                {
                    db.Questions.Remove(questionToRemove);
                    db.SaveChanges();
                    state = true;
                }
            }
            return state;
        }
    }
}
