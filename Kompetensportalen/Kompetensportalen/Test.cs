using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kompetensportalen
{
    public class Test
    {
        public string employee { get; set; }
        public int testType { get; set; }
        public DateTime date { get; set; }

        List<Question> newQuestion = new List<Question>();
    }
}