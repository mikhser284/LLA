using Newtonsoft.Json;
using System;

namespace LLA.Core
{
    public enum TypeOfWordEng
    {
        Noun,
        NounPhrasal,
        NounPlural,
        ProNoun,
        Adjective,
        Verb,
        VerbPhrasal,
        VerbIrregular,
        AdVerb,
        QuestionWord,
        Number,
        Conjunction,
        Preposition,
        Interjection,
        Unclasified,
        Phrase,
        Idiom,
        Article,
    }

    public enum EntityDescriptor
    {
        Object,
        ObjectRef,
        ObjectProperty,
        ObjectsReletionship,
        Action,
        ActionCharacteristic,
        Query,
        Quantity,
        Conjunction,
        Time
    }

    [JsonObject("word")]
    public class CWord
    {
        [JsonProperty("0", Order = 1)]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("1", Order = 2)]
        public Int32 LessonNumber { get; set; }

        [JsonProperty("2", Order = 3)]
        public Int32 WordOrder { get; set; }

        [JsonProperty("3", Order = 4)]
        public String WritingEng { get; set; }

        [JsonProperty("4", Order = 5)]
        public String Speling { get; set; }


        [JsonProperty("5", Order = 6)]
        public String WritingUkr { get; set; }

        [JsonProperty("6", Order = 7)]
        public String RemarksUkr { get; set; }

        [JsonProperty("7", Order = 8)]
        public String WritingRus { get; set; }

        [JsonProperty("8", Order = 9)]
        public String RemarkssRus { get; set; }

        [JsonProperty("9", Order = 10)]
        public String SpelingByUkr { get; set; }

        [JsonProperty("10", Order = 11)]
        public String SpelingByRus { get; set; }


        public CWord()
        {
            CreatedAt = DateTime.Now;
        }
    }
}
