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

namespace LLA.GUI.Dialogs
{
    /// <summary>
    /// Interaction logic for Words_AddNewItem.xaml
    /// </summary>
    public partial class Words_Create : Window
    {
        public CWord Word { get; set; }

        public Words_Create()
        {
            InitializeComponent();
            Word = new CWord();
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
                    Source = Word,
                    Path = new PropertyPath(nameof(Word.WritingEng)),
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
                    Source = Word,
                    Path = new PropertyPath(nameof(Word.Speling)),
                    Mode = BindingMode.TwoWay
                };
                BindingOperations.SetBinding(engSpeling, TextBox.TextProperty, bind);
                engSpeling.GotKeyboardFocus += (sender, e) => InputLanguageManager.Current.CurrentInputLanguage = cultureEng;
            }

            TextBox engSpelingByUkr = Ctrl_EngSpelingByUkr;
            if(engSpelingByUkr != null)
            {
                //TODO - modify class CWord
                //Binding bind = new Binding()
                //{
                //    Source = Word,
                //    Path = new PropertyPath(nameof(Word.Word_EngSpeling)),
                //    Mode = BindingMode.TwoWay
                //};
                //BindingOperations.SetBinding(engSpelingByUkr, TextBox.TextProperty, bind);
                engSpelingByUkr.GotKeyboardFocus += (sender, e) => InputLanguageManager.Current.CurrentInputLanguage = cultureUkr;
            }

            TextBox engSpelingByRus = Ctrl_EngSpelingByRus;
            if (engSpelingByRus != null)
            {
                //TODO - modify class CWord
                //Binding bind = new Binding()
                //{
                //    Source = Word,
                //    Path = new PropertyPath(nameof(Word.Word_EngSpeling)),
                //    Mode = BindingMode.TwoWay
                //};
                //BindingOperations.SetBinding(engSpelingByRus, TextBox.TextProperty, bind);
                engSpelingByRus.GotKeyboardFocus += (sender, e) => InputLanguageManager.Current.CurrentInputLanguage = cultureRus;
            }

            TextBox ukrWriting = Ctrl_UrkWriting;
            if(ukrWriting != null)
            {
                Binding bind = new Binding()
                {
                    Source = Word,
                    Path = new PropertyPath(nameof(Word.WritingUkr)),
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
                    Source = Word,
                    Path = new PropertyPath(nameof(Word.RemarksUkr)),
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
                    Source = Word,
                    Path = new PropertyPath(nameof(Word.WritingRus)),
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
                    Source = Word,
                    Path = new PropertyPath(nameof(Word.RemarksRus)),
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
