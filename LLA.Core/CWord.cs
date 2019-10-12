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
        public Int32 IndexA_Lesson { get; set; }

        [JsonProperty("2", Order = 3)]
        public Int32 IndexB_Order { get; set; }

        [JsonProperty("3", Order = 4)]
        public String Word_Eng { get; set; }

        [JsonProperty("4", Order = 5)]
        public String Word_EngSpeling { get; set; }

        [JsonProperty("5", Order = 6)]
        public String Word_Ukr { get; set; }

        [JsonProperty("6", Order = 7)]
        public String Word_UkrRemarks { get; set; }

        [JsonProperty("7", Order = 8)]
        public String Word_Rus { get; set; }

        [JsonProperty("8", Order = 9)]
        public String Word_RusRemarks { get; set; }



        public CWord()
        {
            CreatedAt = DateTime.Now;
        }

        public CWord(Int32 indexA_Text, Int32 indexB_Order, String wordEng, String wordUkr, String wordUkrRemarks = null, String wordEngSpeling = null, String wordRus = null, String wordRusRemarks = null)
        {
            CreatedAt = DateTime.Now;
            IndexA_Lesson = indexA_Text;
            IndexB_Order = indexB_Order;
            Word_Eng = wordEng;
            Word_Ukr = wordUkr;
            Word_UkrRemarks = wordUkrRemarks;
            Word_EngSpeling = wordEngSpeling;
            Word_Rus = wordRus;
            Word_RusRemarks = wordRusRemarks;
        }
    }
}
