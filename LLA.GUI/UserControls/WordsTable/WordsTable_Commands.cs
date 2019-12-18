﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace LLA.GUI
{
    public static class WordsTable_Commands
    {
        public static readonly RoutedCommand Temp_SwitchToLearnMode
            = new RoutedUICommand("Переключится в режим изучения"
            , nameof(Temp_SwitchToLearnMode)
            , typeof(WordsTable_Commands)
            , new InputGestureCollection() { new KeyGesture(Key.Q, ModifierKeys.Alt) });

        public static readonly RoutedCommand Temp_SwitchToNormalMode
            = new RoutedUICommand("Переключится в обычный реижим"
            , nameof(Temp_SwitchToNormalMode)
            , typeof(WordsTable_Commands)
            , new InputGestureCollection() { new KeyGesture(Key.N, ModifierKeys.Alt) });

        public static readonly RoutedCommand Temp_Dlg001
            = new RoutedUICommand("Диалог 001"
            , nameof(Temp_Dlg001)
            , typeof(WordsTable_Commands)
            , new InputGestureCollection() { new KeyGesture(Key.F12) });

        public static readonly RoutedCommand Temp_ItemLearn
            = new RoutedUICommand("Показать диалог теста"
            , nameof(Temp_ItemLearn)
            , typeof(WordsTable_Commands)
            , new InputGestureCollection() { new KeyGesture(Key.Space, ModifierKeys.Control) });

        public static readonly RoutedCommand Close
            = new RoutedUICommand("Закрыть"
                , nameof(Close)
                , typeof(WordsTable_Commands)
                , new InputGestureCollection() { new KeyGesture(Key.O, ModifierKeys.Control) });

        public static readonly RoutedCommand ItemsLoadFromFile
            = new RoutedUICommand("Загрузить из файла"
            , nameof(ItemsLoadFromFile)
            , typeof(WordsTable_Commands)
            , new InputGestureCollection() { new KeyGesture(Key.O, ModifierKeys.Control) });


        public static readonly RoutedCommand ItemsSaveToFile
            = new RoutedUICommand("Сохранить в файл"
            , nameof(ItemsSaveToFile)
            , typeof(WordsTable_Commands)
            , new InputGestureCollection() { new KeyGesture(Key.S, ModifierKeys.Control) });


        public static readonly RoutedCommand ItemsSaveToNewFile
            = new RoutedUICommand("Сохранить в новый файл ..."
            , nameof(ItemsSaveToNewFile)
            , typeof(WordsTable_Commands)
            , new InputGestureCollection() { new KeyGesture(Key.S, ModifierKeys.Alt | ModifierKeys.Control) });


        public static readonly RoutedCommand ItemAddNew
            = new RoutedUICommand("Добавить новый элемент в конец списка"
            , nameof(ItemAddNew)
            , typeof(WordsTable_Commands)
            , new InputGestureCollection() { new KeyGesture(Key.End, ModifierKeys.Control | ModifierKeys.Shift) });


        public static readonly RoutedCommand ItemInsertNewBefore
            = new RoutedUICommand("Вставить новый элемент перед выделенными"
            , nameof(ItemInsertNewBefore)
            , typeof(WordsTable_Commands)
            , new InputGestureCollection() { new KeyGesture(Key.Up, ModifierKeys.Control | ModifierKeys.Shift) });


        public static readonly RoutedCommand ItemInsertNewAfter
            = new RoutedUICommand("Вставить новый элемент после выделенных"
            , nameof(ItemInsertNewAfter)
            , typeof(WordsTable_Commands)
            , new InputGestureCollection() { new KeyGesture(Key.Down, ModifierKeys.Control | ModifierKeys.Shift) });


        public static readonly RoutedCommand ItemEdit
            = new RoutedUICommand("Редактировать выделенный элемент"
            , nameof(ItemEdit)
            , typeof(WordsTable_Commands)
            , new InputGestureCollection() { new KeyGesture(Key.Space, ModifierKeys.Control | ModifierKeys.Shift) });


        public static readonly RoutedCommand ItemsEdit
            = new RoutedUICommand("Редактировать несколько выделенных элементов"
            , nameof(ItemsEdit)
            , typeof(WordsTable_Commands)
            , new InputGestureCollection() { new KeyGesture(Key.Space, ModifierKeys.Alt) });


        public static readonly RoutedUICommand ItemsEnumerate
            = new RoutedUICommand("Заново пронумеровать выделенные элементы"
            , nameof(ItemsEnumerate)
            , typeof(WordsTable_Commands)
            , new InputGestureCollection() { new KeyGesture(Key.E, ModifierKeys.Control | ModifierKeys.Shift), });


        public static readonly RoutedCommand ItemsDelete
            = new RoutedUICommand("Удалить выделенные элементы"
            , nameof(ItemInsertNewAfter)
            , typeof(WordsTable_Commands)
            , new InputGestureCollection() { new KeyGesture(Key.Down, ModifierKeys.Control | ModifierKeys.Shift) });
    }
}
