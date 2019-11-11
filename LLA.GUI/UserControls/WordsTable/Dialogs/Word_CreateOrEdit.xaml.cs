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
    public partial class Word_CreateOrEdit
    {
        public DateTime CreatedAt
        {
            get { return (DateTime) GetValue(CreatedAtProperty); }
            set { SetValue(CreatedAtProperty, value); }
        }

        public static readonly DependencyProperty CreatedAtProperty =
            DependencyProperty.Register(nameof(CreatedAt), typeof(DateTime), typeof(Word_CreateOrEdit), new PropertyMetadata(default(DateTime)));


        public DateTime ModifiedAt
        {
            get { return (DateTime) GetValue(ModifiedAtProperty); }
            set { SetValue(ModifiedAtProperty, value); }
        }

        public static readonly DependencyProperty ModifiedAtProperty =
            DependencyProperty.Register(nameof(ModifiedAt), typeof(DateTime), typeof(Word_CreateOrEdit), new PropertyMetadata(default(DateTime)));

        public Int32 Version
        {
            get { return (Int32) GetValue(VersionProperty); }
            set { SetValue(VersionProperty, value); }
        }

        public static readonly DependencyProperty VersionProperty =
            DependencyProperty.Register(nameof(Version), typeof(Int32), typeof(Word_CreateOrEdit), new PropertyMetadata(default(Int32)));

        public Guid WordUid
        {
            get { return (Guid) GetValue(WordUidProperty); }
            set { SetValue(WordUidProperty, value); }
        }

        public static readonly DependencyProperty WordUidProperty =
            DependencyProperty.Register(nameof(Uid), typeof(Guid), typeof(Word_CreateOrEdit), new PropertyMetadata(default(Guid)));

        public Int32 LessonNumber
        {
            get { return (Int32) GetValue(LessonNumberProperty); }
            set { SetValue(LessonNumberProperty, value); }
        }

        public static readonly DependencyProperty LessonNumberProperty =
            DependencyProperty.Register(nameof(LessonNumber), typeof(Int32), typeof(Word_CreateOrEdit), new PropertyMetadata(default(Int32)));

        public Int32 WordOrder
        {
            get { return (Int32) GetValue(WordOrderProperty); }
            set { SetValue(WordOrderProperty, value); }
        }

        public static readonly DependencyProperty WordOrderProperty =
            DependencyProperty.Register(nameof(WordOrder), typeof(Int32), typeof(Word_CreateOrEdit), new PropertyMetadata(default(Int32)));

        public String WritingEng
        {
            get { return (String) GetValue(WritingEngProperty); }
            set { SetValue(WritingEngProperty, value); }
        }

        public static readonly DependencyProperty WritingEngProperty =
            DependencyProperty.Register(nameof(WritingEng), typeof(String), typeof(Word_CreateOrEdit), new PropertyMetadata(default(String)));

        public String Synonims
        {
            get { return (String)GetValue(SynonimsProperty); }
            set { SetValue(SynonimsProperty, value); }
        }

        public static readonly DependencyProperty SynonimsProperty =
            DependencyProperty.Register(nameof(Synonims), typeof(String), typeof(Word_CreateOrEdit), new PropertyMetadata(default(String)));
        
        public String Speling
        {
            get { return (String) GetValue(SpelingProperty); }
            set { SetValue(SpelingProperty, value); }
        }

        public static readonly DependencyProperty SpelingProperty =
            DependencyProperty.Register(nameof(Speling), typeof(String), typeof(Word_CreateOrEdit), new PropertyMetadata(default(String)));
        
        public String SpelingByUkr
        {
            get { return (String)GetValue(SpelingByUkrProperty); }
            set { SetValue(SpelingByUkrProperty, value); }
        }

        public static readonly DependencyProperty SpelingByUkrProperty =
            DependencyProperty.Register(nameof(SpelingByUkr), typeof(String), typeof(Word_CreateOrEdit), new PropertyMetadata(default(String)));

        public String WritingUkr
        {
            get { return (String) GetValue(WritingUkrProperty); }
            set { SetValue(WritingUkrProperty, value); }
        }

        public static readonly DependencyProperty WritingUkrProperty =
            DependencyProperty.Register(nameof(WritingUkr), typeof(String), typeof(Word_CreateOrEdit), new PropertyMetadata(default(String)));

        public String RemarksUkr
        {
            get { return (String) GetValue(RemarksUkrProperty); }
            set { SetValue(RemarksUkrProperty, value); }
        }

        public static readonly DependencyProperty RemarksUkrProperty =
            DependencyProperty.Register(nameof(RemarksUkr), typeof(String), typeof(Word_CreateOrEdit), new PropertyMetadata(default(String)));
        
        public String SpelingByRus
        {
            get { return (String)GetValue(SpelingByRusProperty); }
            set { SetValue(SpelingByRusProperty, value); }
        }

        public static readonly DependencyProperty SpelingByRusProperty =
            DependencyProperty.Register(nameof(SpelingByRus), typeof(String), typeof(Word_CreateOrEdit), new PropertyMetadata(default(String)));

        public String WritingRus
        {
            get { return (String) GetValue(WritingRusProperty); }
            set { SetValue(WritingRusProperty, value); }
        }

        public static readonly DependencyProperty WritingRusProperty =
            DependencyProperty.Register(nameof(WritingRus), typeof(String), typeof(Word_CreateOrEdit), new PropertyMetadata(default(String)));

        public String RemarksRus
        {
            get { return (String) GetValue(RemarksRusProperty); }
            set { SetValue(RemarksRusProperty, value); }
        }

        public static readonly DependencyProperty RemarksRusProperty =
            DependencyProperty.Register(nameof(RemarksRus), typeof(String), typeof(Word_CreateOrEdit), new PropertyMetadata(default(String)));
    }

    public partial class Word_CreateOrEdit : Window
    {
        public Word_CreateOrEdit()
        {
            InitializeComponent();
            SetBindingsAndHandlers();
        }

        public Word_CreateOrEdit(CWord word) : this()
        {
            CreatedAt = word.CreatedAt;
            ModifiedAt = word.ModifiedAt;
            Version = word.Version;
            WordUid = word.Uid;
            //
            LessonNumber = word.LessonNumber;
            WordOrder = word.WordOrder;
            //
            WritingEng = word.WritingEng;
            Speling = word.Speling;
            //
            SpelingByUkr = word.SpelingByUkr;
            WritingUkr = word.WritingUkr;
            RemarksUkr = word.RemarksUkr;
            //
            SpelingByRus = word.SpelingByRus;
            WritingRus = word.WritingRus;
            RemarksRus = word.RemarksRus;
        }

        public CWord GetResult()
        {
            CWord word = new CWord
            {
                //TODO
                LessonNumber = this.LessonNumber,
                WordOrder = this.WordOrder,
                //
                WritingEng = this.WritingEng,
                Synomims = this.Synonims,
                Speling = this.Speling,
                //
                SpelingByUkr = this.SpelingByUkr,
                WritingUkr = this.WritingUkr,
                RemarksUkr = this.RemarksUkr,
                //
                SpelingByRus = this.SpelingByRus,
                WritingRus = this.WritingRus,
                RemarksRus = this.RemarksRus
            };
            return word;
        }

        private void SetBindingsAndHandlers()
        {
            CultureInfo cultureEng = new CultureInfo("en-US");
            CultureInfo cultureRus = new CultureInfo("ru-UA");
            CultureInfo cultureUkr = new CultureInfo("uk-UA");

            TextBox writingEng = Ctrl_EngWriting as TextBox;
            if(writingEng != null)
            {
                Binding bind = new Binding()
                {
                    Source = this,
                    Path = new PropertyPath(nameof(WritingEng)),
                    Mode = BindingMode.TwoWay
                };
                BindingOperations.SetBinding(writingEng, TextBox.TextProperty, bind);
                writingEng.GotKeyboardFocus += (sender, e) => InputLanguageManager.Current.CurrentInputLanguage = cultureEng;
            }

            TextBox synomims = Ctrl_Synonims as TextBox;
            if(synomims != null)
            {
                Binding bind = new Binding
                {
                    Source = this,
                    Path = new PropertyPath(nameof(Synonims)),
                    Mode = BindingMode.TwoWay
                };
                BindingOperations.SetBinding(synomims, TextBox.TextProperty, bind);
                synomims.GotKeyboardFocus += (sender, e) => InputLanguageManager.Current.CurrentInputLanguage = cultureEng;
            }

            TextBox speling = Ctrl_EngSpeling;
            if(speling != null)
            {
                Binding bind = new Binding()
                {
                    Source = this,
                    Path = new PropertyPath(nameof(Speling)),
                    Mode = BindingMode.TwoWay
                };
                BindingOperations.SetBinding(speling, TextBox.TextProperty, bind);
                speling.GotKeyboardFocus += (sender, e) => InputLanguageManager.Current.CurrentInputLanguage = cultureEng;
            }

            TextBox spelingByUkr = Ctrl_EngSpelingByUkr;
            if(spelingByUkr != null)
            {
                Binding bind = new Binding()
                {
                    Source = this,
                    Path = new PropertyPath(nameof(SpelingByUkr)),
                    Mode = BindingMode.TwoWay
                };
                BindingOperations.SetBinding(spelingByUkr, TextBox.TextProperty, bind);
                spelingByUkr.GotKeyboardFocus += (sender, e) => InputLanguageManager.Current.CurrentInputLanguage = cultureUkr;
            }

            TextBox writingUkr = Ctrl_UrkWriting;
            if(writingUkr != null)
            {
                Binding bind = new Binding()
                {
                    Source = this,
                    Path = new PropertyPath(nameof(WritingUkr)),
                    Mode = BindingMode.TwoWay
                };
                BindingOperations.SetBinding(writingUkr, TextBox.TextProperty, bind);
                writingUkr.GotKeyboardFocus += (sender, e) => InputLanguageManager.Current.CurrentInputLanguage = cultureUkr;
            }

            TextBox remarksUkr = Ctrl_UkrRemarks;
            if(remarksUkr != null)
            {
                Binding bind = new Binding()
                {
                    Source = this,
                    Path = new PropertyPath(nameof(RemarksUkr)),
                    Mode = BindingMode.TwoWay
                };
                BindingOperations.SetBinding(remarksUkr, TextBox.TextProperty, bind);
                remarksUkr.GotKeyboardFocus += (sender, e) => InputLanguageManager.Current.CurrentInputLanguage = cultureUkr;
            }

            TextBox spelingByRus = Ctrl_EngSpelingByRus;
            if (spelingByRus != null)
            {
                Binding bind = new Binding()
                {
                    Source = this,
                    Path = new PropertyPath(nameof(SpelingByRus)),
                    Mode = BindingMode.TwoWay
                };
                BindingOperations.SetBinding(spelingByRus, TextBox.TextProperty, bind);
                spelingByRus.GotKeyboardFocus += (sender, e) => InputLanguageManager.Current.CurrentInputLanguage = cultureRus;
            }

            TextBox writingRus = Ctrl_RusWriting;
            if(writingRus != null)
            {
                Binding bind = new Binding()
                {
                    Source = this,
                    Path = new PropertyPath(nameof(WritingRus)),
                    Mode = BindingMode.TwoWay
                };
                BindingOperations.SetBinding(writingRus, TextBox.TextProperty, bind);
                writingRus.GotKeyboardFocus += (sender, e) => InputLanguageManager.Current.CurrentInputLanguage = cultureRus;
            }

            TextBox remarksRus = Ctrl_RusRemarks;
            if (remarksRus != null)
            {
                Binding bind = new Binding()
                {
                    Source = this,
                    Path = new PropertyPath(nameof(RemarksRus)),
                    Mode = BindingMode.TwoWay
                };
                BindingOperations.SetBinding(remarksRus, TextBox.TextProperty, bind);
                remarksRus.GotKeyboardFocus += (sender, e) => InputLanguageManager.Current.CurrentInputLanguage = cultureRus;
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
