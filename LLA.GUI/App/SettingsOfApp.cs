using System;
using System.Collections.Generic;
using System.Text;

namespace LLA.GUI
{
    public class SettingsOfApp
    {
        public String DataRootDirectory { get; set; }

        public SettingsOfWordsLearningSheduler WordsLearningShedulerSettings { get; set; }
    }


    public class SettingsOfWordsLearningSheduler
    {
        public Int32 MarkWordLearnedWhenSuccesfulAttemptsCountEqual { get; set; }

        public Int32 MaxAttemptsCountAtLearningNewWord { get; set; }

        public Int32 CountOfWordsToLearnPerDay { get; set; }

        public List<RepeatingInterval> RepeatingIntervals { get; set; }

        public SettingsOfWordsLearningSheduler()
        {
            RepeatingIntervals = new List<RepeatingInterval>();
        }
    }


    public class RepeatingInterval
    {
        public Int32 Order { get; set; }

        public TimeSpan RepeatThrough { get; set; }

        public Int32 ShedulerLearningPriority { get; set; }
    }
}
