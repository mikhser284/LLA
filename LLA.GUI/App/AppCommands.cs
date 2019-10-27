using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace LLA.GUI
{
    public static class AppCommands
    {
        /// <summary> Создать новый файл </summary>
        public static readonly RoutedUICommand FileCreate
            = new RoutedUICommand("Создать..."
                , nameof(FileCreate)
                , typeof(AppCommands)
                , new InputGestureCollection() { new KeyGesture(Key.N, ModifierKeys.Control), });

        /// <summary> Открыть файл </summary>
        public static readonly RoutedUICommand FileOpen 
            = new RoutedUICommand ("Открыть"
            , nameof(FileOpen)
            , typeof(AppCommands)
            , new InputGestureCollection() { new KeyGesture(Key.O, ModifierKeys.Control), } );

        /// <summary> Открыть все файлы из последней сессии </summary>
        public static readonly RoutedUICommand FilesOpenLastSession
            = new RoutedUICommand("Открыть последнюю сессию"
                , nameof(FilesOpenLastSession)
                , typeof(AppCommands)
                , new InputGestureCollection() { new KeyGesture(Key.O, ModifierKeys.Control | ModifierKeys.Alt), });

        /// <summary> Сохранить файл </summary>
        public static readonly RoutedUICommand FileSave
            = new RoutedUICommand("Сохранить"
                , nameof(FileSave)
                , typeof(AppCommands)
                , new InputGestureCollection() { new KeyGesture(Key.S, ModifierKeys.Control), });

        /// <summary> Сохранить копию как новый файл </summary>
        public static readonly RoutedUICommand FileSaveAs
            = new RoutedUICommand("Сохранить как ..."
                , nameof(FileSaveAs)
                , typeof(AppCommands)
                , new InputGestureCollection() { new KeyGesture(Key.S, ModifierKeys.Control | ModifierKeys.Shift), });

        /// <summary> Сохранить все файлы </summary>
        public static readonly RoutedUICommand FilesSaveAll
            = new RoutedUICommand("Сохранить как ..."
                , nameof(FilesSaveAll)
                , typeof(AppCommands)
                , new InputGestureCollection() { new KeyGesture(Key.S, ModifierKeys.Control | ModifierKeys.Alt), });

        /// <summary> Закрыть файл </summary>
        public static readonly RoutedUICommand FileClose
            = new RoutedUICommand("Закрыть"
                , nameof(FileClose)
                , typeof(AppCommands)
                , new InputGestureCollection() { new KeyGesture(Key.W, ModifierKeys.Control), });

        /// <summary> Печать текущего файла </summary>
        public static readonly RoutedUICommand Print
            = new RoutedUICommand("Печать"
                , nameof(Print)
                , typeof(AppCommands)
                , new InputGestureCollection() { new KeyGesture(Key.P, ModifierKeys.Control), });

        /// <summary> Выйти из приложения </summary>
        public static readonly RoutedUICommand AppClose 
            = new RoutedUICommand ("Выход"
            , nameof(AppClose)
            , typeof(AppCommands)
            , new InputGestureCollection() { new KeyGesture(Key.F4, ModifierKeys.Alt), } );

        /// <summary> Отменить действие </summary>
        public static readonly RoutedUICommand Undo
            = new RoutedUICommand("Выход"
                , nameof(Undo)
                , typeof(AppCommands)
                , new InputGestureCollection() { new KeyGesture(Key.Z, ModifierKeys.Control), });

        /// <summary> Вернуть действие </summary>
        public static readonly RoutedUICommand Redo
            = new RoutedUICommand("Выход"
                , nameof(Redo)
                , typeof(AppCommands)
                , new InputGestureCollection() { new KeyGesture(Key.Z, ModifierKeys.Control | ModifierKeys.Shift), });

        /// <summary> Настройки приложения </summary>
        public static readonly RoutedUICommand AppSettings
            = new RoutedUICommand("Настройки..."
                , nameof(AppSettings)
                , typeof(AppCommands)
                , new InputGestureCollection() { new KeyGesture(Key.F12, ModifierKeys.Control | ModifierKeys.Shift), });

        /// <summary> О приложении </summary>
        public static readonly RoutedUICommand AppAbout
            = new RoutedUICommand("О приложении..."
                , nameof(AppAbout)
                , typeof(AppCommands)
                , new InputGestureCollection() { new KeyGesture(Key.F1, ModifierKeys.Control | ModifierKeys.Shift), });

    }




    public class MultiKeyInputGesture : InputGesture
    {
        private const int MaxPauseMs = 5000;
        private InputGestureCollection _gestures = new InputGestureCollection();
        private DateTime _lastWhen = DateTime.Now;
        private int _checkIdx;
        public String DisplayString { get; set; }

        public MultiKeyInputGesture(List<KeyGesture> keys)
        {
            if (keys == null || keys.Count < 0) throw new ArgumentException($"Collection \"{nameof(keys)}\" is null or empty");            
            _gestures = new InputGestureCollection(keys);
            DisplayString = "Text";// String.Join(", ", keys.Select(x => x.DisplayString));
        }

        public override bool Matches(object targetElement, InputEventArgs inputEventArgs)
        {
            KeyEventArgs inpEventArgs = inputEventArgs as KeyEventArgs;
            if (inpEventArgs is null) return false;
            DateTime now = DateTime.Now;
            if ((now - _lastWhen).TotalMilliseconds > MaxPauseMs) _checkIdx = 0;
            _lastWhen = now;
            if (inpEventArgs.IsRepeat) return false;
            if (!_gestures[_checkIdx++].Matches(null, inpEventArgs))
            {
                _checkIdx = 0;
                return false;
            }
            if (_checkIdx != _gestures.Count) return false;
            _checkIdx = 0;
            inpEventArgs.Handled = true;
            return true;
        }
    }

    [TypeConverter(typeof(_MultiKeyGestureConverter))]
    public class _MultiKeyGesture : KeyGesture
    {
        private readonly IList<Key> _keys;
        private readonly ReadOnlyCollection<Key> _readOnlyKeys;
        private int _currentKeyIndex;
        private DateTime _lastKeyPress;
        private static readonly TimeSpan _maximumDelayBetweenKeyPresses = TimeSpan.FromSeconds(1);

        public _MultiKeyGesture(IEnumerable<Key> keys, ModifierKeys modifiers)
            : this(keys, modifiers, string.Empty)
        {
        }

        public _MultiKeyGesture(IEnumerable<Key> keys, ModifierKeys modifiers, string displayString)
            : base(Key.None, modifiers, displayString)
        {
            _keys = new List<Key>(keys);
            _readOnlyKeys = new ReadOnlyCollection<Key>(_keys);

            if (_keys.Count == 0)
            {
                throw new ArgumentException("At least one key must be specified.", "keys");
            }
        }

        public ICollection<Key> Keys
        {
            get { return _readOnlyKeys; }
        }

        public override bool Matches(object targetElement, InputEventArgs inputEventArgs)
        {
            var args = inputEventArgs as KeyEventArgs;

            if ((args == null) || !IsDefinedKey(args.Key))
            {
                return false;
            }

            if (_currentKeyIndex != 0 && ((DateTime.Now - _lastKeyPress) > _maximumDelayBetweenKeyPresses))
            {
                //took too long to press next key so reset
                _currentKeyIndex = 0;
                return false;
            }

            //the modifier only needs to be held down for the first keystroke, but you could also require that the modifier be held down for every keystroke
            if (_currentKeyIndex == 0 && Modifiers != Keyboard.Modifiers)
            {
                //wrong modifiers
                _currentKeyIndex = 0;
                return false;
            }

            if (_keys[_currentKeyIndex] != args.Key)
            {
                //wrong key
                _currentKeyIndex = 0;
                return false;
            }

            ++_currentKeyIndex;

            if (_currentKeyIndex != _keys.Count)
            {
                //still matching
                _lastKeyPress = DateTime.Now;
                inputEventArgs.Handled = true;
                return false;
            }

            //match complete
            _currentKeyIndex = 0;
            return true;
        }

        private static bool IsDefinedKey(Key key)
        {
            return ((key >= Key.None) && (key <= Key.OemClear));
        }
    }

    //I have NOT fleshed this class out fully - just enough to get this demo working
    public class _MultiKeyGestureConverter : TypeConverter
    {
        private readonly KeyConverter _keyConverter;
        private readonly ModifierKeysConverter _modifierKeysConverter;

        public _MultiKeyGestureConverter()
        {
            _keyConverter = new KeyConverter();
            _modifierKeysConverter = new ModifierKeysConverter();
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            var keyStrokes = (value as string).Split(',');
            var firstKeyStroke = keyStrokes[0];
            var firstKeyStrokeParts = firstKeyStroke.Split('+');

            var modifierKeys = (ModifierKeys)_modifierKeysConverter.ConvertFrom(firstKeyStrokeParts[0]);
            var keys = new List<Key>();

            keys.Add((Key)_keyConverter.ConvertFrom(firstKeyStrokeParts[1]));

            for (var i = 1; i < keyStrokes.Length; ++i)
            {
                keys.Add((Key)_keyConverter.ConvertFrom(keyStrokes[i]));
            }

            return new _MultiKeyGesture(keys, modifierKeys);
        }
    }

    public class _MultiKeyBinding : InputBinding
    {
        [TypeConverter(typeof(_MultiKeyGestureConverter))]
        public override InputGesture Gesture
        {
            get { return base.Gesture as _MultiKeyGesture; }
            set
            {
                if (!(value is _MultiKeyGesture))
                {
                    throw new ArgumentException();
                }

                base.Gesture = value;
            }
        }
    }
}
