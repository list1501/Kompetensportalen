using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kompetensportalen
{
    public class Answer
    {
        public int correctOrNot { get; set; } //0 for correct, 1 for wrong
        public string text { get; set; }
    }
}