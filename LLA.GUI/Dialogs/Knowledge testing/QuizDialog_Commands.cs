using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace LLA.GUI.Dialogs
{
    public static class QuizDialog_Commands
    {
        public static readonly RoutedCommand Quiz_CheckAnswer
            = new RoutedUICommand("Проверить"
            , nameof(Quiz_CheckAnswer)
            , typeof(QuizDialog_Commands)
            , new InputGestureCollection() { new KeyGesture(Key.Space, ModifierKeys.Control) });
        
        public static readonly RoutedCommand Quiz_CheckAnswerAndGoToNextText
            = new RoutedUICommand("Следующий"
            , nameof(Quiz_CheckAnswerAndGoToNextText)
            , typeof(QuizDialog_Commands)
            , new InputGestureCollection() { new KeyGesture(Key.Space, ModifierKeys.Control | ModifierKeys.Shift) });

        public static readonly RoutedCommand Quiz_DeferTest
            = new RoutedUICommand("Отложить"
            , nameof(Quiz_DeferTest)
            , typeof(QuizDialog_Commands)
            , new InputGestureCollection() { new KeyGesture(Key.Escape, ModifierKeys.Alt) });

        public static readonly RoutedCommand Quiz_FinishTesting
            = new RoutedUICommand("Завершить"
            , nameof(Quiz_FinishTesting)
            , typeof(QuizDialog_Commands)
            , new InputGestureCollection() { new KeyGesture(Key.Escape)});
    }
}
