using System;
using System.Collections.Generic;
using System.Text;

namespace LLA.Core
{
    public class CWordToLearning
    {
        public CWord WordToLearning { get; set; }

        public EWordLearningStatus LearningStatus { get; set; }

        public CWordToLearning(CWord wordToLearning)
        {
            WordToLearning = wordToLearning;
            LearningStatus = EWordLearningStatus.ToLearning;
        }
    }
}
