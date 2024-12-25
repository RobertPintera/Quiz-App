using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz_App.DAL.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public int IdQuestion { get; set; }
        public string Content { get; set; }
        public bool IsCorrect { get; set; }
        public Question Question { get; set; }
    }
}
