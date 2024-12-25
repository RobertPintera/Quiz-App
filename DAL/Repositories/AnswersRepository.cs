using Quiz_App.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz_App.DAL.Repositories
{
    public class AnswersRepository
    {
        public static bool AddAnswerToDb(Answer answer)
        {
            bool state = false;

            using (var db = new QuizDbContext())
            {
                if (answer != null)
                {
                    db.Answers.Add(answer);
                    db.SaveChanges();
                    state = true;
                }
            }
            return state;
        }

        public static bool EditAnswerFromDb(int answerId, string newContent, bool isCorrect)
        {
            bool state = false;

            using (var db = new QuizDbContext())
            {
                var answer = db.Answers.Find(answerId);

                if (answer != null)
                {
                    answer.Content = newContent;
                    answer.IsCorrect = isCorrect;
                    db.SaveChanges();
                    state = true;
                }
            }
            return state;
        }

        public static bool DeleteAnswerFromDb(Answer answer)
        {
            bool state = false;

            using (var db = new QuizDbContext())
            {
                var answerToRemove = db.Answers.SingleOrDefault(q => q.Id == answer.Id);
                if (answerToRemove != null)
                {
                    db.Answers.Remove(answerToRemove);
                    db.SaveChanges();
                    state = true;
                }
            }
            return state;
        }
    }
}
