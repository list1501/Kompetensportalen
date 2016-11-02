using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kompetensportalen
{
    public class Question
    {
        public bool correct { get; set; }
        List<Answer> correctAnswer = new List<Answer>();
        List<Answer> wrongAnswer = new List<Answer>();
        List<Answer> userAnswer = new List<Answer>();
    }
}