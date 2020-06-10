using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace twog
{
    public class DialogueInfo
    {
        public string Character { get; set; }
        public string Text { get; set; }
        public string[] Options { get; set; }
        public string[] Paths { get; set; }
        public bool CanEnd { get; set; }
    }
}
