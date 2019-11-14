using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace LLA.GUI.Dialogs
{
    public static class QuizDialog_Commands
    {
        public static readonly RoutedCommand Quiz_Defer
            = new RoutedUICommand("Отложить тест"
            , nameof(Quiz_Defer)
            , typeof(QuizDialog_Commands)
            , new InputGestureCollection() { new KeyGesture(Key.Space, ModifierKeys.Shift) });

        public static readonly RoutedCommand Quiz_Check
            = new RoutedUICommand("Проверить ответ"
            , nameof(Quiz_Check)
            , typeof(QuizDialog_Commands)
            , new InputGestureCollection() { new KeyGesture(Key.Space, ModifierKeys.Shift) });
        
        public static readonly RoutedCommand Quiz_CheckAndClose
            = new RoutedUICommand("Проверить ответ и закрыть диалог"
            , nameof(Quiz_CheckAndClose)
            , typeof(QuizDialog_Commands)
            , new InputGestureCollection() { new KeyGesture(Key.Space, ModifierKeys.Shift) });

        public static readonly RoutedCommand Quiz_CheckAndNext
            = new RoutedUICommand("Проверить ответ и перейти к следующему тесту"
            , nameof(Quiz_CheckAndNext)
            , typeof(QuizDialog_Commands)
            , new InputGestureCollection() { new KeyGesture(Key.Space, ModifierKeys.Shift) });
    }
}
