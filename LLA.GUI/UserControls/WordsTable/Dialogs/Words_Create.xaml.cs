using LLA.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Newtonsoft.Json;

namespace LLA.GUI.Dialogs
{
    public partial class Words_Create
    {
        public DateTime CreatedAt
        {
            get { return (DateTime) GetValue(CreatedAtProperty); }
            set { SetValue(CreatedAtProperty, value); }
        }

        public static readonly DependencyProperty CreatedAtProperty =
            DependencyProperty.Register(nameof(CreatedAt), typeof(DateTime), typeof(Words_Create), new PropertyMetadata(default(DateTime)));


        public DateTime ModifiedAt
        {
            get { return (DateTime) GetValue(ModifiedAtProperty); }
            set { SetValue(ModifiedAtProperty, value); }
        }

        public static readonly DependencyProperty ModifiedAtProperty =
            DependencyProperty.Register(nameof(ModifiedAt), typeof(DateTime), typeof(Words_Create), new PropertyMetadata(default(DateTime)));

        public Int32 Version
        {
            get { return (Int32) GetValue(VersionProperty); }
            set { SetValue(VersionProperty, value); }
        }

        public static readonly DependencyProperty VersionProperty =
            DependencyProperty.Register(nameof(Version), typeof(Int32), typeof(Words_Create), new PropertyMetadata(default(Int32)));

        public Guid WordUid
        {
            get { return (Guid) GetValue(WordUidProperty); }
            set { SetValue(WordUidProperty, value); }
        }

        public static readonly DependencyProperty WordUidProperty =
            DependencyProperty.Register(nameof(Uid), typeof(Guid), typeof(Words_Create), new PropertyMetadata(default(Guid)));

        public Int32 LessonNumber
        {
            get { return (Int32) GetValue(LessonNumberProperty); }
            set { SetValue(LessonNumberProperty, value); }
        }

        public static readonly DependencyProperty LessonNumberProperty =
            DependencyProperty.Register(nameof(LessonNumber), typeof(Int32), typeof(Words_Create), new PropertyMetadata(default(Int32)));

        public Int32 WordOrder
        {
            get { return (Int32) GetValue(WordOrderProperty); }
            set { SetValue(WordOrderProperty, value); }
        }

        public static readonly DependencyProperty WordOrderProperty =
            DependencyProperty.Register(nameof(WordOrder), typeof(Int32), typeof(Words_Create), new PropertyMetadata(default(Int32)));

        public String WritingEng
        {
            get { return (String) GetValue(WritingEngProperty); }
            set { SetValue(WritingEngProperty, value); }
        }

        public static readonly DependencyProperty WritingEngProperty =
            DependencyProperty.Register(nameof(WritingEng), typeof(String), typeof(Words_Create), new PropertyMetadata(default(String)));

        public String Speling
        {
            get { return (String) GetValue(SpelingProperty); }
            set { SetValue(SpelingProperty, value); }
        }

        public static readonly DependencyProperty SpelingProperty =
            DependencyProperty.Register(nameof(Speling), typeof(String), typeof(Words_Create), new PropertyMetadata(default(String)));
        
        public String SpelingByUkr
        {
            get { return (String)GetValue(SpelingByUkrProperty); }
            set { SetValue(SpelingByUkrProperty, value); }
        }

        public static readonly DependencyProperty SpelingByUkrProperty =
            DependencyProperty.Register(nameof(SpelingByUkr), typeof(String), typeof(Words_Create), new PropertyMetadata(default(String)));

        public String WritingUkr
        {
            get { return (String) GetValue(WritingUkrProperty); }
            set { SetValue(WritingUkrProperty, value); }
        }

        public static readonly DependencyProperty WritingUkrProperty =
            DependencyProperty.Register(nameof(WritingUkr), typeof(String), typeof(Words_Create), new PropertyMetadata(default(String)));

        public String RemarksUkr
        {
            get { return (String) GetValue(RemarksUkrProperty); }
            set { SetValue(RemarksUkrProperty, value); }
        }

        public static readonly DependencyProperty RemarksUkrProperty =
            DependencyProperty.Register(nameof(RemarksUkr), typeof(String), typeof(Words_Create), new PropertyMetadata(default(String)));
        
        public String SpelingByRus
        {
            get { return (String)GetValue(SpelingByRusProperty); }
            set { SetValue(SpelingByRusProperty, value); }
        }

        public static readonly DependencyProperty SpelingByRusProperty =
            DependencyProperty.Register(nameof(SpelingByRus), typeof(String), typeof(Words_Create), new PropertyMetadata(default(String)));

        public String WritingRus
        {
            get { return (String) GetValue(WritingRusProperty); }
            set { SetValue(WritingRusProperty, value); }
        }

        public static readonly DependencyProperty WritingRusProperty =
            DependencyProperty.Register(nameof(WritingRus), typeof(String), typeof(Words_Create), new PropertyMetadata(default(String)));

        public String RemarksRus
        {
            get { return (String) GetValue(RemarksRusProperty); }
            set { SetValue(RemarksRusProperty, value); }
        }

        public static readonly DependencyProperty RemarksRusProperty =
            DependencyProperty.Register(nameof(RemarksRus), typeof(String), typeof(Words_Create), new PropertyMetadata(default(String)));
    }

    public partial class Words_Create : Window
    {
        public Words_Create()
        {
            InitializeComponent();
            SetBindingsAndHandlers();
        }

        private void SetBindingsAndHandlers()
        {
            CultureInfo cultureEng = new CultureInfo("en-US");
            CultureInfo cultureRus = new CultureInfo("ru-UA");
            CultureInfo cultureUkr = new CultureInfo("uk-UA");

            TextBox engWriting = Ctrl_EngWriting as TextBox;
            if(engWriting != null)
            {
                Binding bind = new Binding()
                {
                    Source = this,
                    Path = new PropertyPath(nameof(WritingEng)),
                    Mode = BindingMode.TwoWay
                };
                BindingOperations.SetBinding(engWriting, TextBox.TextProperty, bind);
                engWriting.GotKeyboardFocus += (sender, e) => InputLanguageManager.Current.CurrentInputLanguage = cultureEng;
            }

            TextBox engSpeling = Ctrl_EngSpeling;
            if(engSpeling != null)
            {
                Binding bind = new Binding()
                {
                    Source = this,
                    Path = new PropertyPath(nameof(Speling)),
                    Mode = BindingMode.TwoWay
                };
                BindingOperations.SetBinding(engSpeling, TextBox.TextProperty, bind);
                engSpeling.GotKeyboardFocus += (sender, e) => InputLanguageManager.Current.CurrentInputLanguage = cultureEng;
            }

            TextBox engSpelingByUkr = Ctrl_EngSpelingByUkr;
            if(engSpelingByUkr != null)
            {
                Binding bind = new Binding()
                {
                    Source = this,
                    Path = new PropertyPath(nameof(SpelingByUkr)),
                    Mode = BindingMode.TwoWay
                };
                BindingOperations.SetBinding(engSpelingByUkr, TextBox.TextProperty, bind);
                engSpelingByUkr.GotKeyboardFocus += (sender, e) => InputLanguageManager.Current.CurrentInputLanguage = cultureUkr;
            }

            TextBox engSpelingByRus = Ctrl_EngSpelingByRus;
            if (engSpelingByRus != null)
            {
                Binding bind = new Binding()
                {
                    Source = this,
                    Path = new PropertyPath(nameof(SpelingByRus)),
                    Mode = BindingMode.TwoWay
                };
                BindingOperations.SetBinding(engSpelingByRus, TextBox.TextProperty, bind);
                engSpelingByRus.GotKeyboardFocus += (sender, e) => InputLanguageManager.Current.CurrentInputLanguage = cultureRus;
            }

            TextBox ukrWriting = Ctrl_UrkWriting;
            if(ukrWriting != null)
            {
                Binding bind = new Binding()
                {
                    Source = this,
                    Path = new PropertyPath(nameof(WritingUkr)),
                    Mode = BindingMode.TwoWay
                };
                BindingOperations.SetBinding(ukrWriting, TextBox.TextProperty, bind);
                ukrWriting.GotKeyboardFocus += (sender, e) => InputLanguageManager.Current.CurrentInputLanguage = cultureUkr;
            }

            TextBox ukrRemarks = Ctrl_UkrRemarks;
            if(ukrRemarks != null)
            {
                Binding bind = new Binding()
                {
                    Source = this,
                    Path = new PropertyPath(nameof(RemarksUkr)),
                    Mode = BindingMode.TwoWay
                };
                BindingOperations.SetBinding(ukrRemarks, TextBox.TextProperty, bind);
                ukrRemarks.GotKeyboardFocus += (sender, e) => InputLanguageManager.Current.CurrentInputLanguage = cultureUkr;
            }

            TextBox rusWriting = Ctrl_RusWriting;
            if(rusWriting != null)
            {
                Binding bind = new Binding()
                {
                    Source = this,
                    Path = new PropertyPath(nameof(WritingRus)),
                    Mode = BindingMode.TwoWay
                };
                BindingOperations.SetBinding(rusWriting, TextBox.TextProperty, bind);
                rusWriting.GotKeyboardFocus += (sender, e) => InputLanguageManager.Current.CurrentInputLanguage = cultureRus;
            }

            TextBox rusRemarks = Ctrl_RusRemarks;
            if (rusRemarks != null)
            {
                Binding bind = new Binding()
                {
                    Source = this,
                    Path = new PropertyPath(nameof(RemarksRus)),
                    Mode = BindingMode.TwoWay
                };
                BindingOperations.SetBinding(rusWriting, TextBox.TextProperty, bind);
                rusRemarks.GotKeyboardFocus += (sender, e) => InputLanguageManager.Current.CurrentInputLanguage = cultureRus;
            }
        }

        private void ButtonClick_Ok(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void ButtonClick_Cancel(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
