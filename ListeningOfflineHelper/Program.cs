﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ListeningOfflineHelper
{
    class Program
    {
        static void Main(string[] args)
        {
            QuestionViewer questionViewer = new QuestionViewer();
            questionViewer.SectionA();
        }
    }
}
