using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kompetensportalen
{
    public class Question
    {
        public int id { get; set; }
        public string question { get; set; }
        public int category { get; set; }
        public List<Answer> correctAnswer { get; set; }
        public List<Answer> wrongAnswer { get; set; }
        public List<Answer> userAnswer { get; set; }
        public bool correct { get; set; }
    }
}