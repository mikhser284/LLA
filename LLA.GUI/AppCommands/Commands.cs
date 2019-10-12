using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace LLA.GUI.AppCommands
{
    public static class Commands
    {
        public static readonly ICommand AppClose;
        public static readonly ICommand FileOpen;
        public static readonly ICommand FileSave;
        public static readonly ICommand FileSaveAs;

        static Commands()
        {
            var inputGestures = new InputGestureCollection();
            inputGestures.Add(new _MultiKeyGesture(new Key[] { Key.A, Key.Q }, ModifierKeys.Control));
        }
    }

    public static class Commands_Application
    {
        public static readonly RoutedUICommand AppClose = new RoutedUICommand ("Close application"
                , nameof(AppClose)
                , typeof(Commands_Application)
                , new InputGestureCollection() { new KeyGesture(Key.F4, ModifierKeys.Alt), } );

        public static readonly RoutedUICommand FileOpen = new RoutedUICommand ("Open dictionary file"
                , nameof(FileOpen)
                , typeof(Commands_Application)
                , new InputGestureCollection() { new KeyGesture(Key.O, ModifierKeys.Control), } );

        public static readonly RoutedUICommand FileSave = new RoutedUICommand("Save dictionary file"
                , nameof(FileSave)
                , typeof(Commands_Application)
                , new InputGestureCollection() { new KeyGesture(Key.S, ModifierKeys.Control), } );

        public static readonly RoutedUICommand FileSaveAs = new RoutedUICommand("Save dictionary file as ..."
                , nameof(FileSaveAs)
                , typeof(Commands_Application));
    }

    public static class Commands_WordsDatagrid
    {
        public static readonly RoutedUICommand SpreadData = new RoutedUICommand ("CreatingTime = Spread date, increase time"
                , nameof(SpreadData)
                , typeof(Commands_WordsDatagrid)
                , new InputGestureCollection() { new KeyGesture(Key.Q, ModifierKeys.Control), } );
        
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
