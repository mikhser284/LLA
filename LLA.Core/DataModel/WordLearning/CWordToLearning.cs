using System;
using System.Collections.Generic;
using System.Text;

namespace LLA.Core
{
    public enum EWordLearningStatus
    {
        /// <summary> Не изучать, пропустить слово </summary>
        DontLearnSkipWord,
        /// <summary> Не изучать, знакомое слово </summary>
        DontLearnKnownWord,
        /// <summary> К изучению </summary>
        ToLearning,
        /// <summary> Изучение завершено </summary>
        LearningFinished
    }
}
