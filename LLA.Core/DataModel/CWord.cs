using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using LLA.Core.DataModel;

namespace LLA.Core
{
    [JsonObject("word")]
    public class CWord : INotifyPropertyChanged
    {
        private DateTime                _createdAt;
        private DateTime                _modifiedAt;
        private Int32                   _version;
        private Guid                    _uid;

        private Int32                   _lessonNumber;
        private Int32                   _wordOrder;

        private String                  _writingEng;
        private String                  _synonims;
        private String                  _speling;

        private String                  _spelingByUkr;
        private String                  _writingUkr;
        private String                  _remarksUkr;

        private String                  _spelingByRus;
        private String                  _writingRus;
        private String                  _remarksRus;
        private EWordLearningStatus     _learningStatus;

        [JsonProperty("16", Order = 16)]
        public String Synomims
        {
            get { return _synonims; }
            set
            {
                _synonims = value;
                OnPropChanged(nameof(Synomims));
            }
        }


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


        [JsonProperty("14", Order = 15)]
        public EWordLearningStatus LearningStatus
        {
            get { return _learningStatus; }
            set
            {
                _learningStatus = value;
                OnPropChanged(nameof(LearningStatus));
            }
        }


        public CWord()
        {
            _createdAt = DateTime.Now;
            _modifiedAt = _createdAt;
            _version = 1;
            _uid = Guid.NewGuid();
            _learningStatus = EWordLearningStatus.ToLearning;
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

        public override string ToString()
        {
            return $"{WritingEng} — {WritingUkr} — {WritingRus}";
        }
        
        public void Update(CWord source)
        {
            //_createdAt
            _lessonNumber = source.LessonNumber;
            _wordOrder = source.WordOrder;
            //
            _writingEng = source.WritingEng;
            _synonims = source.Synomims;
            _speling = source.Speling;
            //
            _spelingByUkr = source.SpelingByUkr;
            _writingUkr = source.WritingUkr;
            _remarksUkr = source.RemarksUkr;
            //
            _spelingByRus = source.SpelingByRus;
            _writingRus = source.WritingRus;
            _remarksRus = source.RemarksRus;
            //
            _learningStatus = source.LearningStatus;
            _modifiedAt = DateTime.Now;
            ++_version;
        }
    }
}
