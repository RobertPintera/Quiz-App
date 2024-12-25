using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz_App.DAL.Models
{
    public class Question
    {
        public int Id { get; set; }
        public int IdQuiz { get; set; }
        public string Content { get; set; }
        public ObservableCollection<Answer> Answers { get; set; } = new ObservableCollection<Answer>();
        public Quiz Quiz { get; set; }
    }
}
