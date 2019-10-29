using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LLA.Core
{
    /// <summary> Результаты проверки знаний </summary>
    public enum EWordKnowledgeTestResult
    {
        /// <summary> Неудачно </summary>
        Failed = -1,
        /// <summary> Тест пройден </summary>
        Passed = 1
    }

    public enum EWordLearningSheduler
    {
        Manual = 0,
        Auto = 1,
    }

    public class RepInterval
    {
        public Int32 Order { get; }

        public TimeSpan RepeatThrough { get; }

        public Int32 LearningPriority { get; }

        public RepInterval(Int32 order, TimeSpan repeatThrough, Int32 learningPriority)
        {
            Order = order;
            RepeatThrough = repeatThrough;
            LearningPriority = learningPriority;
        }

        public static List<RepInterval> GetIntervals()
        {
            Int32 hour = 60;
            Int32 day = hour * 24;
            List<Int32> timeIntervals = new List<Int32> { hour, hour * 7, day, day * 2, day * 4, day * 8, day * 16, day * 32, day * 64, day * 128 };
            List<RepInterval> repIntervals = new List<RepInterval>();
            Int32 intervalOrder = 0;
            Int32 learningPriority = timeIntervals.Count;
            TimeSpan timeSpan = new TimeSpan();
            foreach(Int32 timeInterval in timeIntervals)
            {
                timeSpan += TimeSpan.FromMinutes(timeInterval);
                repIntervals.Add(new RepInterval(++intervalOrder, timeSpan, learningPriority--));
            }
            return repIntervals;
        }
    }

    public class LearningSettings
    {
        public List<RepInterval> RepeatingIntervals;

        public Int32 WordsToLearningDaylyPlan { get; set; }

        public Int32 MaxAutoLearningPriorityOrder { get; }

        public LearningSettings()
        {
            WordsToLearningDaylyPlan = 30;
            RepeatingIntervals = RepInterval.GetIntervals();
            MaxAutoLearningPriorityOrder = RepeatingIntervals.Max(x => x.LearningPriority);
        }
    }

    public class CWordLearningHistory
    {
        public DateTime ToCheckAt { get; set; }

        public Int32 TestNo { get; set; }

        public Int32 RepeatingInterval { get; set; }

        public Int32 LearningPriority { get; set; }

        public DateTime FinishedAt { get; set; }
        
        public EWordKnowledgeTestResult? TestResult { get; set; }

        public String UserAnswer { get; set; }

        public String CorrectAnswer { get; set; }
    }
}
