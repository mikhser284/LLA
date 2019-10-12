using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace LLA.GUI
{
    public enum Button
    {
        None = 0,
        Cancel = 1,
        Back = 2,
        Tab = 3,
        LineFeed = 4,
        Clear = 5,
        Enter = 6,
        Return = 6,
        Pause = 7,
        Capital = 8,
        CapsLock = 8,
        HangulMode = 9,
        KanaMode = 9,
        JunjaMode = 10,
        FinalMode = 11,
        HanjaMode = 12,
        KanjiMode = 12,
        Escape = 13,
        ImeConvert = 14,
        ImeNonConvert = 15,
        ImeAccept = 16,
        ImeModeChange = 17,
        Space = 18,
        PageUp = 19,
        Prior = 19,
        Next = 20,
        PageDown = 20,
        End = 21,
        Home = 22,
        Left = 23,
        Up = 24,
        Right = 25,
        Down = 26,
        Select = 27,
        Print = 28,
        Execute = 29,
        PrintScreen = 30,
        Snapshot = 30,
        Insert = 31,
        Delete = 32,
        Help = 33,
        D0 = 34,
        D1 = 35,
        D2 = 36,
        D3 = 37,
        D4 = 38,
        D5 = 39,
        D6 = 40,
        D7 = 41,
        D8 = 42,
        D9 = 43,
        A = 44,
        B = 45,
        C = 46,
        D = 47,
        E = 48,
        F = 49,
        G = 50,
        H = 51,
        I = 52,
        J = 53,
        K = 54,
        L = 55,
        M = 56,
        N = 57,
        O = 58,
        P = 59,
        Q = 60,
        R = 61,
        S = 62,
        T = 63,
        U = 64,
        V = 65,
        W = 66,
        X = 67,
        Y = 68,
        Z = 69,
        LWin = 70,
        RWin = 71,
        Apps = 72,
        Sleep = 73,
        NumPad0 = 74,
        NumPad1 = 75,
        NumPad2 = 76,
        NumPad3 = 77,
        NumPad4 = 78,
        NumPad5 = 79,
        NumPad6 = 80,
        NumPad7 = 81,
        NumPad8 = 82,
        NumPad9 = 83,
        Multiply = 84,
        Add = 85,
        Separator = 86,
        Subtract = 87,
        Decimal = 88,
        Divide = 89,
        F1 = 90,
        F2 = 91,
        F3 = 92,
        F4 = 93,
        F5 = 94,
        F6 = 95,
        F7 = 96,
        F8 = 97,
        F9 = 98,
        F10 = 99,
        F11 = 100,
        F12 = 101,
        F13 = 102,
        F14 = 103,
        F15 = 104,
        F16 = 105,
        F17 = 106,
        F18 = 107,
        F19 = 108,
        F20 = 109,
        F21 = 110,
        F22 = 111,
        F24 = 113,
        NumLock = 114,
        Scroll = 115,
        LeftShift = 116,
        RightShift = 117,
        LeftCtrl = 118,
        RightCtrl = 119,
        LeftAlt = 120,
        RightAlt = 121,
        BrowserBack = 122,
        BrowserForward = 123,
        BrowserRefresh = 124,
        BrowserStop = 125,
        BrowserSearch = 126,
        BrowserFavorites = 127,
        BrowserHome = 128,
        VolumeMute = 129,
        VolumeDown = 130,
        VolumeUp = 131,
        MediaNextTrack = 132,
        MediaPreviousTrack = 133,
        MediaStop = 134,
        MediaPlayPause = 135,
        LaunchMail = 136,
        SelectMedia = 137,
        LaunchApplication1 = 138,
        LaunchApplication2 = 139,
        Oem1 = 140,
        OemSemicolon = 140,
        OemPlus = 141,
        OemComma = 142,
        OemMinus = 143,
        OemPeriod = 144,
        Oem2 = 145,
        OemQuestion = 145,
        Oem3 = 146,
        OemTilde = 146,
        AbntC1 = 147,
        AbntC2 = 148,
        Oem4 = 149,
        OemOpenBrackets = 149,
        Oem5 = 150,
        OemPipe = 150,
        Oem6 = 151,
        OemCloseBrackets = 151,
        Oem7 = 152,
        OemQuotes = 152,
        Oem8 = 153,
        Oem102 = 154,
        OemBackslash = 154,
        ImeProcessed = 155,
        System = 156,
        DbeAlphanumeric = 157,
        OemAttn = 157,
        DbeKatakana = 158,
        OemFinish = 158,
        DbeHiragana = 159,
        OemCopy = 159,
        DbeSbcsChar = 160,
        OemAuto = 160,
        DbeDbcsChar = 161,
        OemEnlw = 161,
        DbeRoman = 162,
        OemBackTab = 162,
        Attn = 163,
        DbeNoRoman = 163,
        CrSel = 164,
        DbeEnterWordRegisterMode = 164,
        DbeEnterImeConfigureMode = 165,
        ExSel = 165,
        DbeFlushString = 166,
        EraseEof = 166,
        DbeCodeInput = 167,
        Play = 167,
        DbeNoCodeInput = 168,
        Zoom = 168,
        DbeDetermineString = 169,
        NoName = 169,
        DbeEnterDialogConversionMode = 170,
        Pa1 = 170,
        OemClear = 171,
        DeadCharProcessed = 172,
        //
        //
        ModifierAlt         = 0b_0001_0000_0000_0000,
        ModifierControl     = 0b_0010_0000_0000_0000,
        MofifierShift       = 0b_0100_0000_0000_0000,
        ModifierWindows     = 0b_1000_0000_0000_0000,
        //
        ModifiersAltControl = ModifierAlt | ModifierControl,
        ModifiersAltShift = ModifierAlt | MofifierShift,
        ModifiersAltWindows = ModifierAlt | ModifierWindows,
        ModifiersControlShift = ModifierControl | MofifierShift,
        ModifiersControlWindows = ModifierControl | ModifierWindows,
        ModifiersShiftWindows = MofifierShift | ModifierWindows,
        ModifiersAltControlShift = ModifierAlt | ModifierControl | MofifierShift,
        ModifiersAltControlWindows = ModifierAlt | ModifierControl | ModifierWindows,
        ModifiersAltShiftWindows = ModifierAlt | MofifierShift | ModifierWindows,
        ModifiersControlShiftWindows = ModifierControl | MofifierShift | ModifierWindows,
        ModifiersAltControlShiftWindows = ModifierAlt | ModifierControl | MofifierShift | ModifierWindows
    }

    public class MultiKeyGesture : KeyGesture
    {
        private const int MaxPauseMs = 5000;
        
        private InputGestureCollection Gestures { get; set; }

        private DateTime _lastWhen = DateTime.Now;
        private int _checkIdx;

        public MultiKeyGesture(Button firstKey, params Button[] otherKeys)
            : base(GetKey(firstKey), GetKeyModifiers(firstKey))//, GetGetsuresAsString(firstKey, otherKeys))
        {
            List<Button> keysSequence = new List<Button> { firstKey };
            if (otherKeys.Length > 0) keysSequence.AddRange(otherKeys);
            Gestures = new InputGestureCollection();
            foreach(var item in keysSequence)
            {
                var res = ConvertToKeyGesture(item);
                Gestures.Add(res);
            }
            //var items = keysSequence.Select(key => ConvertToKeyGesture(key)).ToList();
            //String title = String.Join(", ", Gestures.Select(x => x.DisplayString));            
        }

        private static String GetGetsuresAsString(Button firstKey, Button[] otherKeys)
        {
            List<Button> keysSequence = new List<Button> { firstKey };
            if (otherKeys.Length > 0) keysSequence.AddRange(otherKeys);
            List<KeyGesture> gestures = keysSequence.Select(key => ConvertToKeyGesture(key)).ToList();
            return String.Join(", ", gestures.Select(x => x.DisplayString));
        }
        
        public override bool Matches(object targetElement, InputEventArgs inputEventArgs)
        {
            return false;
            //KeyEventArgs inpEventArgs = inputEventArgs as KeyEventArgs;
            //if (inpEventArgs is null) return false;
            //DateTime now = DateTime.Now;
            //if ((now - _lastWhen).TotalMilliseconds > MaxPauseMs) _checkIdx = 0;
            //_lastWhen = now;
            //if (inpEventArgs.IsRepeat) return false;
            //if (!Gestures[_checkIdx++].Matches(null, inpEventArgs))
            //{
            //    _checkIdx = 0;
            //    return false;
            //}
            //if (_checkIdx != Gestures.Count) return false;
            //_checkIdx = 0;
            //inpEventArgs.Handled = true;
            return true;
        }

        private static KeyGesture ConvertToKeyGesture(Button keyAndModidiers)
        {
            Key key = GetKey(keyAndModidiers);
            ModifierKeys keyModifiers = GetKeyModifiers(keyAndModidiers);
            return new KeyGesture(key, keyModifiers);
        }

        private static Key GetKey(Button keyAndModidiers)
        {
            int mask = 0b_0000_1111_1111_1111;
            return (Key)((int)keyAndModidiers & mask);
        }

        private static ModifierKeys GetKeyModifiers(Button keyAndModidiers)
        {
            int mask = 0b_1111_0000_0000_0000;
            return (ModifierKeys)(((int)keyAndModidiers & mask) >> 12);
        }
    }
}
