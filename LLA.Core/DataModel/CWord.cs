using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LLA.Core
{
    [JsonObject("word")]
    public class CWord : INotifyPropertyChanged
    {
        public DateTime _createdAt;
        [JsonProperty("0", Order = 1)]
        public DateTime CreatedAt
        {
            get { return _createdAt; }
            private set
            {
                _createdAt = value;
                OnPropChanged(nameof(CreatedAt));
            }
        }


        public DateTime _modifiedAt;
        [JsonProperty("11", Order = 12)]
        public DateTime ModifiedAt
        {
            get { return _modifiedAt; }
            set
            {
                _modifiedAt = value;
                OnPropChanged(nameof(ModifiedAt));
            }
        }


        public Int32 _version;
        [JsonProperty("12", Order = 13)]
        public Int32 Version
        {
            get { return _version; }
            set
            {
                _version = value;
                OnPropChanged(nameof(Version));
            }
        }


        public Guid _uid;
        [JsonProperty("13", Order = 14)]
        public Guid Uid
        {
            get { return _uid; }
            set
            {
                _uid = value;
                OnPropChanged(nameof(Uid));
            }
        }


        public Int32 _lessonNumber;
        [JsonProperty("1", Order = 2)]
        public Int32 LessonNumber
        {
            get { return _lessonNumber; }
            set
            {
                _lessonNumber = value;
                OnPropChanged(nameof(LessonNumber));
            }
        }


        public Int32 _wordOrder;
        [JsonProperty("2", Order = 3)]
        public Int32 WordOrder
        {
            get { return _wordOrder; }
            set
            {
                _wordOrder = value;
                OnPropChanged(nameof(_wordOrder));
            }
        }
        

        public String _writingEng;
        [JsonProperty("3", Order = 4)]
        public String WritingEng
        {
            get { return _writingEng; }
            set
            {
                _writingEng = value;
                OnPropChanged(nameof(WritingEng));
            }
        }
        

        public String _speling;
        [JsonProperty("4", Order = 5)]
        public String Speling
        {
            get { return _speling; }
            set
            {
                _speling = value;
                OnPropChanged(nameof(Speling));
            }
        }


        public String _writingUkr;
        [JsonProperty("5", Order = 6)]
        public String WritingUkr
        {
            get { return _writingUkr; }
            set
            {
                _writingUkr = value;
                OnPropChanged(nameof(WritingUkr));
            }
        }


        public String _remarksUkr;
        [JsonProperty("6", Order = 7)]
        public String RemarksUkr
        {
            get { return _remarksUkr; }
            set
            {
                _remarksUkr = value;
                OnPropChanged(nameof(RemarksUkr));
            }
        }


        public String _writingRus;
        [JsonProperty("7", Order = 8)]
        public String WritingRus
        {
            get { return _writingRus; }
            set
            {
                _writingRus = value;
                OnPropChanged(nameof(WritingRus));
            }
        }


        public String _remarksRus;
        [JsonProperty("8", Order = 9)]
        public String RemarksRus
        {
            get { return _remarksRus; }
            set
            {
                _remarksRus = value;
                OnPropChanged(nameof(RemarksRus));
            }
        }


        public String _spelingByUkr;
        [JsonProperty("9", Order = 10)]
        public String SpelingByUkr
        {
            get { return _spelingByUkr; }
            set
            {
                _spelingByUkr = value;
                OnPropChanged(nameof(SpelingByUkr));
            }
        }


        public String _spelingByRus;
        [JsonProperty("10", Order = 11)]
        public String SpelingByRus
        {
            get { return _spelingByRus; }
            set
            {
                _spelingByRus = value;
                OnPropChanged(nameof(SpelingByRus));
            }
        }


        public EWordLearningSheduler _learningSheduler;
        [JsonProperty("14", Order = 15)]
        public EWordLearningSheduler LearningSheduler
        {
            get { return _learningSheduler; }
            set
            {
                _learningSheduler = value;
                OnPropChanged(nameof(LearningSheduler));
            }
        }


        public CWord()
        {
            _createdAt = DateTime.Now;
            _uid = Guid.NewGuid();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropChanged([CallerMemberName] String prop = "")
        {
            if (PropertyChanged == null) return;
            if (prop != nameof(ModifiedAt) && prop != nameof(Version))
            {
                ModifiedAt = DateTime.Now;
                ++Version;
            }
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
