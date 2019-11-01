using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LLA.Core
{
    [JsonObject("word")]
    public class CWord : INotifyPropertyChanged
    {
        private DateTime _createdAt;
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


        private DateTime _modifiedAt;
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


        private Int32 _version;
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


        private Guid _uid;
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


        private Int32 _lessonNumber;
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


        private Int32 _wordOrder;
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


        private String _writingEng;
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


        private String _speling;
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


        private String _spelingByUkr;
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


        private String _writingUkr;
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


        private String _remarksUkr;
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

        
        private String _spelingByRus;
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


        private String _writingRus;
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


        private String _remarksRus;
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


        private EWordLearningSheduler _learningSheduler;
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
            _modifiedAt = _createdAt;
            _version = 1;
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
