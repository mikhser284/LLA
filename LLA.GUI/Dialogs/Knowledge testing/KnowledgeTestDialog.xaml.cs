using LLA.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LLA.GUI.Dialogs
{
    public enum EKnosledgeTestDialogState
    {
        ShowTest = 0,
        ShowTestResultCorrect = 1,
        ShowTestResultWrong = -1
    }


    // Properties
    public partial class KnowledgeTestDialog
    {
        public EKnosledgeTestDialogState DialogState
        {
            get { return (EKnosledgeTestDialogState)GetValue(DialogStateProperty); }
            private set { SetValue(DialogStateProperty, value); }
        }

        public static readonly DependencyProperty DialogStateProperty = DependencyProperty.Register(nameof(DialogState)
            , typeof(EKnosledgeTestDialogState), typeof(KnowledgeTestDialog), new PropertyMetadata(default(EKnosledgeTestDialogState)));

        public String WritingUkr
        {
            get { return (String) GetValue(WritingUkrProperty); }
            set { SetValue(WritingUkrProperty, value); }
        }

        public static readonly DependencyProperty WritingUkrProperty = DependencyProperty.Register(nameof(WritingUkr)
            , typeof(String), typeof(KnowledgeTestDialog), new PropertyMetadata(default(String)));


        public String RemarksUkr
        {
            get { return (String) GetValue(RemarksUkrProperty); }
            set { SetValue(RemarksUkrProperty, value); }
        }

        public static readonly DependencyProperty RemarksUkrProperty = DependencyProperty.Register(nameof(RemarksUkr)
            , typeof(String), typeof(KnowledgeTestDialog), new PropertyMetadata(default(String)));



        public String WritingRus
        {
            get { return (String) GetValue(WritingRusProperty); }
            set { SetValue(WritingRusProperty, value); }
        }

        public static readonly DependencyProperty WritingRusProperty = DependencyProperty.Register(nameof(WritingRus)
            , typeof(String), typeof(KnowledgeTestDialog), new PropertyMetadata(default(String)));



        public String RemarksRus
        {
            get { return (String) GetValue(RemarksRusProperty); }
            set { SetValue(RemarksRusProperty, value); }
        }

        public static readonly DependencyProperty RemarksRusProperty = DependencyProperty.Register(nameof(RemarksRus)
            , typeof(String), typeof(KnowledgeTestDialog), new PropertyMetadata(default(String)));



        public String Synonims
        {
            get { return (String) GetValue(SynonimsProperty); }
            set { SetValue(SynonimsProperty, value); }
        }

        public static readonly DependencyProperty SynonimsProperty = DependencyProperty.Register(nameof(Synonims)
            , typeof(String), typeof(KnowledgeTestDialog), new PropertyMetadata(default(String)));



        public String UserAnswer
        {
            get { return (String) GetValue(UserAnswerProperty); }
            set { SetValue(UserAnswerProperty, value); }
        }

        public static readonly DependencyProperty UserAnswerProperty = DependencyProperty.Register(nameof(UserAnswer)
            , typeof(String), typeof(KnowledgeTestDialog), new PropertyMetadata(default(String)));



        public String CorrectAnswer
        {
            get { return (String) GetValue(CorrectAnswerProperty); }
            set { SetValue(CorrectAnswerProperty, value); }
        }

        public static readonly DependencyProperty CorrectAnswerProperty = DependencyProperty.Register(nameof(CorrectAnswer)
            , typeof(String), typeof(KnowledgeTestDialog), new PropertyMetadata(default(String)));



        public String Speling
        {
            get { return (String) GetValue(SpelingProperty); }
            set { SetValue(SpelingProperty, value); }
        }

        public static readonly DependencyProperty SpelingProperty = DependencyProperty.Register(nameof(Speling)
            , typeof(String), typeof(KnowledgeTestDialog), new PropertyMetadata(default(String)));



        public String SpelingByUkr
        {
            get { return (String) GetValue(SpelingByUkrProperty); }
            set { SetValue(SpelingByUkrProperty, value); }
        }

        public static readonly DependencyProperty SpelingByUkrProperty = DependencyProperty.Register(nameof(SpelingByUkr)
            , typeof(String), typeof(KnowledgeTestDialog), new PropertyMetadata(default(String)));



        public String SpelingByRus
        {
            get { return (String) GetValue(SpelingByRusProperty); }
            set { SetValue(SpelingByRusProperty, value); }
        }

        public static readonly DependencyProperty SpelingByRusProperty = DependencyProperty.Register(nameof(SpelingByRus)
            , typeof(String), typeof(KnowledgeTestDialog), new PropertyMetadata(default(String)));

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogState = UserAnswer == CorrectAnswer ? EKnosledgeTestDialogState.ShowTestResultCorrect : EKnosledgeTestDialogState.ShowTestResultWrong;
            SetDialogHeader();
        }
    }



    // Construct
    public partial class KnowledgeTestDialog : Window
    {
        public KnowledgeTestDialog()
        {
            InitializeComponent();
            SetBindingsAndHanlders();
        }

        public KnowledgeTestDialog(CWord word)
        {
            InitializeComponent();
            SetProperties(word);
            SetBindingsAndHanlders();
        }

        private void SetProperties(CWord word)
        {
            WritingUkr = word.WritingUkr;
            RemarksUkr = String.IsNullOrEmpty(word.RemarksUkr) ? String.Empty : $"({word.RemarksUkr})";
            WritingRus = word.WritingRus;
            RemarksRus = String.IsNullOrEmpty(word.RemarksRus) ? String.Empty : $"({word.RemarksRus})";
            Synonims = word.Synomims;
            CorrectAnswer = word.WritingEng;
            Speling = word.Speling;
            SpelingByUkr = word.SpelingByUkr;
            SpelingByRus = word.SpelingByRus;
        }


        private void SetDialogHeader()
        {
            Label header = Ctrl_DialogHeader as Label;
            if (header == null)
                return;
            else
            {
                header.Height = 70;                
                header.HorizontalContentAlignment = HorizontalAlignment.Center;
                header.VerticalContentAlignment = VerticalAlignment.Center;
                header.FontWeight = FontWeights.Bold;                
            }
            switch(DialogState)
            {
                case EKnosledgeTestDialogState.ShowTest:
                    {
                        header.Foreground = new SolidColorBrush(Color.FromRgb(127, 191, 255));
                        header.Background = new SolidColorBrush(Color.FromRgb(218, 230, 242));
                        header.FontSize = 20;
                        header.Content = "Как правильно пишется это слово на английском?";
                        break;
                    }
                case EKnosledgeTestDialogState.ShowTestResultCorrect:
                    {
                        header.Foreground = new SolidColorBrush(Color.FromRgb(0, 128, 0));
                        header.Background = new SolidColorBrush(Color.FromRgb(218, 242, 218));
                        header.FontSize = 40;
                        header.Content = "✔   Верно";
                        break;
                    }
                case EKnosledgeTestDialogState.ShowTestResultWrong:
                    {
                        header.Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                        header.Background = new SolidColorBrush(Color.FromRgb(242, 218, 218));
                        header.FontSize = 40;
                        header.Content = "✖   Не верно";
                        break;
                    }
                default:
                    throw new InvalidOperationException($"Unknown value of enumeration \"{nameof(EKnosledgeTestDialogState)}\"");
            }
        }

        private void SetBindingsAndHanlders()
        {
            SetDialogHeader();

            Visibility visibitlityOfUkrGroup = Visibility.Visible;

            Label writingUkr = Ctrl_WritingUkr as Label;
            if (writingUkr != null)
            {
                Binding bind = new Binding()
                {
                    Source = this,
                    Path = new PropertyPath(nameof(WritingUkr)),
                    Mode = BindingMode.TwoWay
                };
                BindingOperations.SetBinding(writingUkr, Label.ContentProperty, bind);
                if (String.IsNullOrEmpty(WritingUkr)) visibitlityOfUkrGroup = Visibility.Collapsed;
            }

            Visibility ukrRemarksVisibility = String.IsNullOrEmpty(RemarksUkr) ? Visibility.Collapsed : Visibility.Visible;
            Label remarksUkr = Ctrl_RemarksUkr as Label;
            if (remarksUkr != null )
            {
                Binding bind = new Binding()
                {
                    Source = this,
                    Path = new PropertyPath(nameof(RemarksUkr)),
                    Mode = BindingMode.TwoWay
                };
                BindingOperations.SetBinding(remarksUkr, Label.ContentProperty, bind);
                remarksUkr.Visibility = ukrRemarksVisibility;
            }

            Label remarksUkrHeader = Ctrl_RemarksUkrHeader as Label;
            if(remarksUkrHeader != null) remarksUkrHeader.Visibility = ukrRemarksVisibility;

            GroupBox ukrGroup = Ctrl_UkrGroup as GroupBox;
            if (ukrGroup != null) ukrGroup.Visibility = visibitlityOfUkrGroup;





            Visibility visibilityOfRusGroup = Visibility.Visible;

            Label writingRus = Ctrl_WritingRus as Label;
            if (writingRus != null)
            {
                Binding bind = new Binding()
                {
                    Source = this,
                    Path = new PropertyPath(nameof(WritingRus)),
                    Mode = BindingMode.TwoWay
                };
                BindingOperations.SetBinding(writingRus, Label.ContentProperty, bind);
                if (String.IsNullOrEmpty(WritingRus)) visibilityOfRusGroup = Visibility.Collapsed;
            }

            Visibility visibilityOfRemarkRus = String.IsNullOrEmpty(RemarksRus) ? Visibility.Collapsed : Visibility.Visible;
            Label remarksRus = Ctrl_RemarksRus as Label;
            if (remarksRus != null)
            {
                Binding bind = new Binding()
                {
                    Source = this,
                    Path = new PropertyPath(nameof(RemarksRus)),
                    Mode = BindingMode.TwoWay
                };
                BindingOperations.SetBinding(remarksRus, Label.ContentProperty, bind);
                remarksRus.Visibility = visibilityOfRemarkRus;
            }

            Label remarksRusHeader = Ctrl_RemarksRusHeader as Label;
            if (remarksRusHeader != null) remarksRusHeader.Visibility = visibilityOfRemarkRus;

            GroupBox rusGroup = Ctrl_RusGroup as GroupBox;
            if (rusGroup != null) rusGroup.Visibility = visibilityOfRusGroup;

            Visibility visibilityOfSynonims = String.IsNullOrEmpty(Synonims) ? Visibility.Collapsed : Visibility.Visible;
            WrapPanel synonims = Ctrl_Synonims as WrapPanel;
            if(synonims != null)
            {
                synonims.Visibility = visibilityOfSynonims;
                if (visibilityOfSynonims == Visibility.Visible)
                {
                    StackPanel stack = new StackPanel();
                    var text = Synonims.Split('|').Select(x => $" {x.Trim().Replace(' ', ' ')} ").OrderBy(x => x);
                    var words = text.Select(x => new Border { CornerRadius = new CornerRadius(3), Margin = new Thickness(3), Background = Brushes.DarkRed
                        , Child = new Label { Foreground = Brushes.White, FontWeight = FontWeights.Bold, Content = new Run(x) } });
                    words.ForEach(x => synonims.Children.Add(x));
                }
            }

            Label synonimsHeader = Ctrl_SynonimsHeader as Label;
            if (synonimsHeader != null) synonimsHeader.Visibility = visibilityOfSynonims;

            TextBox userAnswer = Ctrl_UserAnswer as TextBox;
            if(userAnswer != null)
            {
                Binding bind = new Binding
                {
                    Source = this,
                    Path = new PropertyPath(nameof(UserAnswer)),
                    Mode = BindingMode.TwoWay
                };
                BindingOperations.SetBinding(userAnswer, TextBox.TextProperty, bind);
            }


            //Visibility visibilityOfRightAnswer = DialogState == EKnosledgeTestDialogState.ShowTest ? Visibility.Collapsed : Visibility.Visible;
            Label rightAnswer = Ctrl_RightAnswer as Label;
            if(rightAnswer != null)
            {
                Binding contentBinding = new Binding
                {
                    Source = this,
                    Path = new PropertyPath(nameof(CorrectAnswer)),
                    Mode = BindingMode.TwoWay
                };
                BindingOperations.SetBinding(rightAnswer, Label.ContentProperty, contentBinding);
                Binding visibilityBinding = new Binding
                {
                    Source = this,
                    Path = new PropertyPath(nameof(DialogState)),
                    Mode = BindingMode.OneWay,
                    Converter = new DialogStateToRightAnswerVisibilityConverter(),
                    ConverterParameter = CorrectAnswer
                };
                BindingOperations.SetBinding(rightAnswer, Label.VisibilityProperty, visibilityBinding);
            }

            Label rightAnswerHeader = Ctrl_RightAnswerHeader as Label;
            if (rightAnswerHeader != null)
            {
                Binding visibilityBinding = new Binding
                {
                    Source = this,
                    Path = new PropertyPath(nameof(DialogState)),
                    Mode = BindingMode.OneWay,
                    Converter = new DialogStateToRightAnswerVisibilityConverter(),
                    ConverterParameter = CorrectAnswer
                };
                BindingOperations.SetBinding(rightAnswerHeader, Label.VisibilityProperty, visibilityBinding);
            }

            Label speling = Ctrl_Speling;
            if(speling != null)
            {
                Binding bind = new Binding
                {
                    Source = this,
                    Path = new PropertyPath(nameof(Speling)),
                    Mode = BindingMode.TwoWay
                };
                BindingOperations.SetBinding(speling, Label.ContentProperty, bind);
                Binding visibilityBinding = new Binding
                {
                    Source = this,
                    Path = new PropertyPath(nameof(DialogState)),
                    Mode = BindingMode.OneWay,
                    Converter = new DialogStateToRightAnswerVisibilityConverter(),
                    ConverterParameter = Speling
                };
                BindingOperations.SetBinding(speling, Label.VisibilityProperty, visibilityBinding);
            }

            Label spelingHeader = Ctrl_SpelingHeader as Label;
            if(spelingHeader != null)
            {
                Binding visibilityBinding = new Binding
                {
                    Source = this,
                    Path = new PropertyPath(nameof(DialogState)),
                    Mode = BindingMode.OneWay,
                    Converter = new DialogStateToRightAnswerVisibilityConverter(),
                    ConverterParameter = Speling
                };
                BindingOperations.SetBinding(spelingHeader, Label.VisibilityProperty, visibilityBinding);
            }

            Label spelingByUkr = Ctrl_SpelingByUkr as Label;
            if(spelingByUkr != null)
            {
                Binding bind = new Binding
                {
                    Source = this,
                    Path = new PropertyPath(nameof(SpelingByUkr)),
                    Mode = BindingMode.TwoWay,
                };
                BindingOperations.SetBinding(spelingByUkr, Label.ContentProperty, bind);
                Binding visibilityBinding = new Binding
                {
                    Source = this,
                    Path = new PropertyPath(nameof(DialogState)),
                    Mode = BindingMode.OneWay,
                    Converter = new DialogStateToRightAnswerVisibilityConverter(),
                    ConverterParameter = SpelingByUkr
                };
                BindingOperations.SetBinding(spelingByUkr, Label.VisibilityProperty, visibilityBinding);
            }

            Label spelingByUkrHeader = Ctrl_SpelingByUkrHeader as Label;
            if(spelingByUkrHeader != null)
            {
                Binding visibilityBinding = new Binding
                {
                    Source = this,
                    Path = new PropertyPath(nameof(DialogState)),
                    Mode = BindingMode.OneWay,
                    Converter = new DialogStateToRightAnswerVisibilityConverter(),
                    ConverterParameter = SpelingByUkr
                };
                BindingOperations.SetBinding(spelingByUkrHeader, Label.VisibilityProperty, visibilityBinding);
            }

            Label spelingByRus = Ctrl_SpelingByRus as Label;
            if (spelingByRus != null)
            {
                Binding bind = new Binding
                {
                    Source = this,
                    Path = new PropertyPath(nameof(SpelingByRus)),
                    Mode = BindingMode.TwoWay,
                };
                BindingOperations.SetBinding(spelingByRus, Label.ContentProperty, bind);
                Binding visibilityBinding = new Binding
                {
                    Source = this,
                    Path = new PropertyPath(nameof(DialogState)),
                    Mode = BindingMode.OneWay,
                    Converter = new DialogStateToRightAnswerVisibilityConverter(),
                    ConverterParameter = SpelingByRus
                };
                BindingOperations.SetBinding(spelingByRus, Label.VisibilityProperty, visibilityBinding);
            }

            Label spelingByRusHeader = Ctrl_SpelingByRusHeader as Label;
            if(spelingByRusHeader != null)
            {
                Binding visibilityBinding = new Binding
                {
                    Source = this,
                    Path = new PropertyPath(nameof(DialogState)),
                    Mode = BindingMode.OneWay,
                    Converter = new DialogStateToRightAnswerVisibilityConverter(),
                    ConverterParameter = SpelingByRus
                };
                BindingOperations.SetBinding(spelingByRusHeader, Label.VisibilityProperty, visibilityBinding);
            }
        }
    }

    public partial class KnowledgeTestDialog
    {

    }

    public class DialogStateToRightAnswerVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is EKnosledgeTestDialogState))
                throw new InvalidOperationException($"Тип данных \"{value.GetType()}\" не соответствует ожидаемому \"{nameof(EKnosledgeTestDialogState)}\"");
            EKnosledgeTestDialogState dlgState = (EKnosledgeTestDialogState)value;
            return dlgState == EKnosledgeTestDialogState.ShowTest || parameter == null ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
