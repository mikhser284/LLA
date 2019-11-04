using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace LLA.Core.DataModel
{
    public enum EWordLearningStatus
    {
        [UiTextColor(0, 100, 100)]
        [UiTextList(3, true, TextAlignment = ETextAlignment.Right)]
        [UiName("НЕ УЧИТЬ")]
        SkipLearning = -2,

        [UiTextColor(205, 100, 100)]
        [UiTextList(2, true, TextAlignment = ETextAlignment.Center)]
        [UiName("Я это знаю")]
        KnownWord = -1,

        [UiTextColor(100, 100, 50)]
        [UiTextList(1, true)]
        [UiName("К изучению")]
        ToLearning = 0,

        [UiTextList(4, false)]
        [UiName("Повторение 01")]
        Repetition01 = 1,

        [UiTextList(5, false)]
        [UiName("Повторение 02")]
        Repetition02 = 2,

        [UiTextList(6, false)]
        [UiName("Повторение 03")]
        Repetition03 = 3,

        [UiTextList(7, false)]
        [UiName("Повторение 04")]
        Repetition04 = 4,

        [UiTextList(8, false)]
        [UiName("Повторение 05")]
        Repetition05 = 5,

        [UiTextList(9, false)]
        [UiName("Повторение 06")]
        Repetition06 = 6,

        [UiTextList(10, false)]
        [UiName("Повторение 07")]
        Repetition07 = 7,

        [UiTextList(11, false)]
        [UiName("Повторение 08")]
        Repetition08 = 8,

        [UiTextList(12, false)]
        [UiName("Повторение 09")]
        Repetition09 = 9,

        [UiTextList(13, false)]
        [UiName("Повторение 10")]
        Repetition10 = 10,

        [UiTextList(14, false)]
        [UiName("СЛОВО ИЗУЧЕНО")]
        LearningConfirmed = 100,
    }
}
