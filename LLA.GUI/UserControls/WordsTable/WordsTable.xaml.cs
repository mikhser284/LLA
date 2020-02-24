using LLA.Core;
using LLA.GUI.Dialogs;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
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
using LLA.Core.DataModel;
using Path = System.IO.Path;
using System.Collections;

namespace LLA.GUI
{
    public partial class WordsTable
    {
        public String Header
        {
            get { return (String)GetValue(HeaderProperty); }
            private set { SetValue(HeaderProperty, value); }
        }
        private void UpdateHeader()
        {
            Header = $"{(NotSaved == true ? "⯁ " : "")}{Path.GetFileNameWithoutExtension(WorkingFile)}";
        }
        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(nameof(Header), typeof(String), typeof(WordsTable), new PropertyMetadata(default(String)));

        public String WorkingFile
        {
            get { return (String)GetValue(WorkingFileProperty); }
            set
            {
                SetValue(WorkingFileProperty, value);
                UpdateHeader();
            }
        }
        public static readonly DependencyProperty WorkingFileProperty = DependencyProperty.Register(nameof(WorkingFile), typeof(String), typeof(WordsTable), new PropertyMetadata(default(String)));

        public Boolean NotSaved
        {
            get { return (Boolean)GetValue(NotSavedProperty); }
            private set
            {
                SetValue(NotSavedProperty, value);
                UpdateHeader();
            }
        }
        public static readonly DependencyProperty NotSavedProperty = DependencyProperty.Register(nameof(NotSaved), typeof(Boolean), typeof(WordsTable), new PropertyMetadata(false));

        public CtrlMode Mode
        {
            get { return (CtrlMode)GetValue(ModeProperty); }
            private set { SetValue(ModeProperty, value); }
        }
        public static readonly DependencyProperty ModeProperty = DependencyProperty.Register(nameof(Mode), typeof(CtrlMode), typeof(WordsTable), new PropertyMetadata(CtrlMode.Normal));

        Window ParentWindow { get; set; }

        public ObservableCollection<CWord> Words { get; set; }
    }


    public class EnumConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value == null ? DependencyProperty.UnsetValue : ((Enum)value).GetDisplayName();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Type enumType = parameter as Type;
            String enumDisplayName = value as String;
            
            return Ext_Enum.GetEnumValue(enumType, enumDisplayName);
        }

        public static string GetDescription(Enum en)
        {
            Type type = en.GetType();
            MemberInfo[] memInfo = type.GetMember(en.ToString());
            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attrs != null && attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }
            return en.ToString();
        }
    }


    public partial class WordsTable : UserControl
    {
        public WordsTable()
        {
            InitializeComponent();
            Words = new ObservableCollection<CWord>();
            Words.CollectionChanged += WordsOnCollectionChanged;

            InitializeDatagrid(ctrl_WordsTable);
            this.Loaded += (sender, args) => { ParentWindow ??= Window.GetWindow(this); };
        }

        private void WordsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            NotSaved = true;
        }

        private void BindCommandsAndHandlers(Window parentWindow, DataGrid ctrl)
        {
            
            CommandBindings.Add(new CommandBinding(WordsTable_Commands.Temp_SwitchToLearnMode, SwitchToLearnMode_Executed, SwitchToLearnMode_CanExecute));
            CommandBindings.Add(new CommandBinding(WordsTable_Commands.Temp_SwitchToNormalMode, SwitchToNormalMode_Executed, SwitchToNormalMode_CanExecute));
            CommandBindings.Add(new CommandBinding(WordsTable_Commands.SelectNextItemsToLearn, SelectNextItemsToLearn_Executed, SelectNextItemsToLearn_CanExecute));

            CommandBindings.Add(new CommandBinding(WordsTable_Commands.MarkAsItemsToLearn, MarkAsItemsToLearn_Executed, MarkAsItemsToLearn_CanExecute));
            CommandBindings.Add(new CommandBinding(WordsTable_Commands.MarkAsItemsNotToLearn, MarkAsItemsNotToLearn_Executed, MarkAsItemsNotToLearn_CanExecute));
            CommandBindings.Add(new CommandBinding(WordsTable_Commands.MarkNewItemsToLearn, MarkNewItemsToLearn_Executed, MarkNewItemsToLearn_CanExecute));
            CommandBindings.Add(new CommandBinding(WordsTable_Commands.MarkWorstItemsToLearn, MarkWorstItemsToLearn_Executed, MarkWorstItemsToLearn_CanExecute));
            //
            CommandBindings.Add(new CommandBinding(WordsTable_Commands.Temp_ItemLearn, CommandItemLearn_Executed, CommandItemLearn_CanExecute));
            CommandBindings.Add(new CommandBinding(WordsTable_Commands.Temp_Dlg001, CommandDlg001_Executed, CommandDlg001_CanExecute));
            //
            ctrl.CommandBindings.Add(new CommandBinding(WordsTable_Commands.ItemsSaveToFile, ExecuteCommand_ItemsSaveToFile));
            ctrl.CommandBindings.Add(new CommandBinding(WordsTable_Commands.ItemsSaveToNewFile, ExecuteCommand_ItemsSaveToNewFile));
            //
            ctrl.CommandBindings.Add(new CommandBinding(WordsTable_Commands.ItemAddNew, ExecureCommand_ItemAddNew));
            ctrl.CommandBindings.Add(new CommandBinding(WordsTable_Commands.ItemInsertNewBefore, ExecuteCommand_ItemInsertNewBefore));
            ctrl.CommandBindings.Add(new CommandBinding(WordsTable_Commands.ItemInsertNewAfter, ExecuteCommand_ItemInsertNewAfter));
            ctrl.CommandBindings.Add(new CommandBinding(WordsTable_Commands.ItemEdit, ExecuteCommand_ItemEdit, ItemEdit_CanExecute));
            ctrl.CommandBindings.Add(new CommandBinding(WordsTable_Commands.ItemsEdit, ExecuteCommand_ItemsEdit));
            ctrl.CommandBindings.Add(new CommandBinding(WordsTable_Commands.ItemsEnumerate, ExecuteCommand_ItemsEnumerate));
            ctrl.CommandBindings.Add(new CommandBinding(WordsTable_Commands.ItemsDelete, ExecuteCommand_ItemsDelete));
        }

        private void InitializeDatagrid(DataGrid ctrl)
        {
            ctrl.AutoGenerateColumns = false;
            ctrl.AlternatingRowBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F4F7F9"));
            ctrl.GridLinesVisibility = DataGridGridLinesVisibility.Vertical;
            ctrl.VerticalGridLinesBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B8C6D8"));
            ctrl.HorizontalGridLinesBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EFF4F7"));
            ctrl.SelectionMode = DataGridSelectionMode.Extended;
            ctrl.CanUserResizeRows = false;
            ctrl.CanUserAddRows = true;
            ctrl.BeginningEdit += Ctrl_DataGrid_Words_BeginningEdit;
            //ctrl.KeyUp += Ctrl_DataGrid_Words_KeyUp;
            //
            InitializeDatagridColumns(ctrl);
            InitializeDatagridContextMenu(ctrl);
            BindCommandsAndHandlers(ParentWindow, ctrl_WordsTable);
            //
            ctrl.ItemsSource = Words;
        }

        private void InitializeDatagridColumns(DataGrid ctrl)
        {
            CWord word;
            var enumConverter = new EnumConverter();

            DataGridCheckBoxColumn learningSheduler = new DataGridCheckBoxColumn
            {
                Header = "Учить",
                Binding = new Binding(nameof(word.LearningSheduler)) { Mode = BindingMode.TwoWay}
            };

            DataGridTextColumn random = new DataGridTextColumn
            {
                Header = "rnd",
                Binding = new Binding(nameof(word.Random)) { Mode = BindingMode.TwoWay }
            };

            DataGridTextColumn learningUserPriority = new DataGridTextColumn
            {
                Header = "Приор.",
                Binding = new Binding(nameof(word.LearningUserPriority)) { Mode = BindingMode.TwoWay }
            };

            DataGridTextColumn lessonNumber = new DataGridTextColumn
            {
                Header = "Урок",
                Binding = new Binding(nameof(word.LessonNumber)) { Mode = BindingMode.TwoWay }
            };
            DataGridTextColumn wordOrder = new DataGridTextColumn
            {
                Header = "№ п.п.",
                Binding = new Binding(nameof(word.WordOrder)) { Mode = BindingMode.TwoWay }
            };
            //
            DataGridTextColumn writingEng = new DataGridTextColumn
            {
                Header = "ENG",
                Binding = new Binding(nameof(word.WritingEng)) { Mode = BindingMode.TwoWay }
            };
            DataGridTag.SetTag(writingEng, true);
            DataGridTextColumn synonims = new DataGridTextColumn
            {
                Header = "Синонимы",
                Binding = new Binding(nameof(word.Synomims)) { Mode = BindingMode.TwoWay }
            };
            DataGridTextColumn speling = new DataGridTextColumn
            {
                Header = "Произношение",
                Binding = new Binding(nameof(word.Speling)) { Mode = BindingMode.TwoWay }
            };
            DataGridTag.SetTag(speling, true);
            DataGridTextColumn writingUkr = new DataGridTextColumn
            {
                Header = "UKR",
                Binding = new Binding(nameof(word.WritingUkr)) { Mode = BindingMode.TwoWay }
            };
            DataGridTextColumn remarksUkr = new DataGridTextColumn
            {
                Header = "UKR (примечания)",
                Binding = new Binding(nameof(word.RemarksUkr)) { Mode = BindingMode.TwoWay }
            };
            DataGridTextColumn spelingByUkr = new DataGridTextColumn
            {
                Header = "Произношение по UKR",
                Binding = new Binding(nameof(word.SpelingByUkr)) { Mode = BindingMode.TwoWay }
            };
            DataGridTag.SetTag(spelingByUkr, true);
            DataGridTextColumn writingRus = new DataGridTextColumn
            {
                Header = "RUS",
                Binding = new Binding(nameof(word.WritingRus)) { Mode = BindingMode.TwoWay }
            };
            DataGridTextColumn remarksRus = new DataGridTextColumn
            {
                Header = "RUS (примечания)",
                Binding = new Binding(nameof(word.RemarksRus)) { Mode = BindingMode.TwoWay }
            };
            //

            //DataGridTextColumn learningSheduler = new DataGridTextColumn()
            //{
            //    Header = "Изучение",
            //    //ItemsSource = Ext_Enum.GetUiAvailableDisplayNames<EWordLearningStatus>(),
            //    Binding = new Binding(nameof(word.LearningStatus)) { Mode = BindingMode.OneWay, Converter = enumConverter, ConverterParameter = typeof(EWordLearningStatus) },
            //};

            DataGridTextColumn createdAt = new DataGridTextColumn
            {
                Header = "Создано",
                Binding = new Binding(nameof(word.CreatedAt)) { Mode = BindingMode.OneWay, StringFormat = "{0:yyyy.MM.dd-ddd\tHH:mm:ss}", ConverterCulture = CultureInfo.CurrentCulture }
            };
            DataGridTag.SetTag(createdAt, true);
            DataGridTextColumn modifiedAt = new DataGridTextColumn
            {
                Header = "Изменено",
                Binding = new Binding(nameof(word.ModifiedAt)) { Mode = BindingMode.OneWay, StringFormat = "{0:yyyy.MM.dd-ddd\tHH:mm:ss}", ConverterCulture = CultureInfo.CurrentCulture }
            };
            DataGridTag.SetTag(modifiedAt, true);

            DataGridTextColumn version = new DataGridTextColumn
            {
                Header = "Ver.",
                Binding = new Binding(nameof(word.Version)) { Mode = BindingMode.OneWay }
            };
            DataGridTextColumn spelingByRus = new DataGridTextColumn
            {
                Header = "Произношение по RUS",
                Binding = new Binding(nameof(word.SpelingByRus)) { Mode = BindingMode.TwoWay }
            };
            DataGridTag.SetTag(spelingByRus, true);
            DataGridTextColumn uid = new DataGridTextColumn
            {
                Header = "Uid",
                Binding = new Binding(nameof(word.Uid)) { Mode = BindingMode.OneWay }
            };
            DataGridTag.SetTag(uid, true);


            ctrl.Columns.Add(learningSheduler);
            ctrl.Columns.Add(learningUserPriority);
            ctrl.Columns.Add(random);
            //
            ctrl.Columns.Add(lessonNumber);
            ctrl.Columns.Add(wordOrder);
            ctrl.Columns.Add(writingEng);
            ctrl.Columns.Add(synonims);
            ctrl.Columns.Add(speling);
            ctrl.Columns.Add(writingUkr);
            ctrl.Columns.Add(remarksUkr);
            ctrl.Columns.Add(spelingByUkr);
            ctrl.Columns.Add(writingRus);
            ctrl.Columns.Add(remarksRus);
            ctrl.Columns.Add(spelingByRus);
            //
            ctrl.Columns.Add(createdAt);
            ctrl.Columns.Add(modifiedAt);
            ctrl.Columns.Add(version);
            ctrl.Columns.Add(uid);
        }

        private void InitializeDatagridContextMenu(DataGrid ctrl)
        {   
            MenuItem itemsSaveToFile = new MenuItem()
            {
                Header = "Сохранить в файл ...",
                Command = WordsTable_Commands.ItemsSaveToFile
            };
            MenuItem itemsSaveToNewFile = new MenuItem()
            {
                Header = "Сохранить в новый файл ...",
                Command = WordsTable_Commands.ItemsSaveToNewFile
            };
            MenuItem file = new MenuItem() { Header = "Файл" };
            
            file.Items.Add(itemsSaveToFile);
            file.Items.Add(itemsSaveToNewFile);
            MenuItem itemAddNew = new MenuItem()
            {
                Header = "Добавить ...",
                Command = WordsTable_Commands.ItemAddNew
            };
            MenuItem itemInsertBefore = new MenuItem()
            {
                Header = "Добавить перед ...",
                Command = WordsTable_Commands.ItemInsertNewBefore
            };
            MenuItem itemInsertAfter = new MenuItem()
            {
                Header = "Добавить после ...",
                Command = WordsTable_Commands.ItemInsertNewAfter
            };
            MenuItem itemEdit = new MenuItem()
            {
                Header = "Редактировать ...",
                Command = WordsTable_Commands.ItemEdit
            };
            MenuItem itemsEdit = new MenuItem()
            {
                Header = "Редактировать несколько ...",
                Command = WordsTable_Commands.ItemsEdit
            };
            MenuItem itemsEnumerate = new MenuItem()
            {
                Header = "Перенумеровать выделенные",
                Command = WordsTable_Commands.ItemsEnumerate
            };
            MenuItem itemsDelete = new MenuItem()
            {
                Header = "Удалить выделенные",
                Command = WordsTable_Commands.ItemsDelete
            };

            ctrl.ContextMenu = new ContextMenu();
            ItemCollection mnu = ctrl.ContextMenu.Items;
            mnu.Add(file);
            mnu.Add(new Separator());
            mnu.Add(itemAddNew);
            mnu.Add(itemInsertBefore);
            mnu.Add(itemInsertAfter);
            mnu.Add(new Separator());
            mnu.Add(itemEdit);
            mnu.Add(itemsEdit);
            mnu.Add(itemsEnumerate);
            mnu.Add(new Separator());
            mnu.Add(itemsDelete);
        }

        private void OpenFile(OpenFileDialog openFileDialog)
        {
            switch (openFileDialog.FilterIndex)
            {
                case 1: // L2Dict
                    {
                        LoadDictionary(openFileDialog.FileName);
                        break;
                    }
                default:
                    {
                        MessageBox.Show("Операция не поддерживается.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                        break;
                    }
            }
        }

        public void LoadDictionary(String fileName)
        {
            if (String.IsNullOrEmpty(fileName))
            {
                MessageBox.Show("Не удалось получить название файла.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                return;
            }
            if(!File.Exists(fileName))
            {
                MessageBox.Show($"Не удалось найти файл \"{WorkingFile}\"", "Файл не найден", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            using (StreamReader file = File.OpenText(fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                Words = (ObservableCollection<CWord>)serializer.Deserialize(file, typeof(ObservableCollection<CWord>));
                AllItems = Words;
                ctrl_WordsTable.ItemsSource = Words;
                WorkingFile = fileName;
                ParentWindow = Window.GetWindow(this);
                
                UserSession.Data.OpenedFiles.Add(WorkingFile);
                UserSession.Save();
            }

            foreach (var word in Words)
            {
                if(word.Uid == Guid.Empty) word.Uid = Guid.NewGuid();
                if (word.ModifiedAt < word.CreatedAt) word.ModifiedAt = word.CreatedAt;
                if (word.Version < 1) word.Version = 1;
            }
        }

        public static void LoadFromFile(String fileName, TabControl tabCtrl)
        {
            if (!File.Exists(fileName)) return;
            WordsTable wordsTable = new WordsTable();
            wordsTable.LoadDictionary(fileName);
            TabItem tabItem = new TabItem() { Content = wordsTable };
            //tabItem.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#364E6F"));
            //tabItem.Foreground = Brushes.White;
            tabItem.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#293955"));
            tabItem.CommandBindings.Clear();

            Binding headerBinding = new Binding { Source = wordsTable, Path = new PropertyPath(nameof(Header)), Mode = BindingMode.OneWay };
            BindingOperations.SetBinding(tabItem, TabItem.HeaderProperty, headerBinding);
            Binding tooltipBinding = new Binding { Source = wordsTable, Path = new PropertyPath(nameof(WorkingFile)), Mode = BindingMode.OneWay };
            BindingOperations.SetBinding(tabItem, TabItem.ToolTipProperty, tooltipBinding);


            tabCtrl.Items.Add(tabItem);
            tabItem.IsSelected = true;
        }

        private void Ctrl_DataGrid_Words_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            DataGrid ctrl = sender as DataGrid;
            if (ctrl == null) return;
            CultureInfo cultureEng = new CultureInfo("en-US");
            CultureInfo cultureRus = new CultureInfo("ru-RU");
            CultureInfo cultureUkr = new CultureInfo("uk-UA");
            Dictionary<String, CultureInfo> headerAndInputLang = new Dictionary<String, CultureInfo>
            {
                { "ENG",  cultureEng },

                { "UKR",  cultureUkr },
                { "UKR (примечания)",  cultureUkr },
                { "Произношение по UKR",  cultureUkr },
                
                { "RUS",  cultureRus },
                { "RUS (примечания)",  cultureRus },
                { "Произношение по RUS",  cultureRus },
            };
            //
            String headerName = ctrl.CurrentCell.Column.Header.ToString();
            CultureInfo inputLang = null;
            //
            if (headerAndInputLang.TryGetValue(headerName, out inputLang))
            {
                InputLanguageManager.Current.CurrentInputLanguage = inputLang;
            }
        }

        private void Ctrl_DataGrid_Words_KeyUp_(object sender, KeyEventArgs e)
        {
            //Todo get indexex of keyboard focused cell
            DataGrid ctrl = sender as DataGrid;
            if (ctrl == null) return;
            if (e.Key != Key.Enter) return;
            ctrl.SelectedItem = ctrl.Items[ctrl.SelectedIndex];
            ctrl.ScrollIntoView(ctrl.SelectedItem);
            DataGridRow dgRow = (DataGridRow)ctrl.ItemContainerGenerator.ContainerFromItem(ctrl.SelectedItem);
            dgRow.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            ctrl.CurrentCell = new DataGridCellInfo(ctrl.Items[ctrl.SelectedIndex], ctrl.Columns[2]);
            ctrl.BeginEdit();
        }

        public void CloseFile()
        {
            TabItem tabItem = this.Parent as TabItem;
            TabControl tabControl = tabItem?.Parent as TabControl;
            if (tabControl == null) return;
            if (NotSaved)
            {
                MessageBoxResult msgBoxResult = MessageBox.Show($"Сохранить файл \"{WorkingFile}\"", "Сохранить изменения?", MessageBoxButton.YesNoCancel);
                if (msgBoxResult == MessageBoxResult.Yes) { SaveToFile(); }
                else if (msgBoxResult == MessageBoxResult.Cancel) return;
            }
            tabItem.Content = null;
            tabControl.Items.Remove(tabItem);
            UserSession.Data.OpenedFiles.Remove(WorkingFile);
            UserSession.Save();
        }

        public void SaveToFile()
        {
            if (String.IsNullOrEmpty(WorkingFile))
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog()
                {
                    DefaultExt = "L2Dict",
                    AddExtension = true,
                };
                if (saveFileDialog.ShowDialog() != true) return;
                WorkingFile = saveFileDialog.FileName;
            }
            using (StreamWriter file = File.CreateText(WorkingFile))
            {
                JsonSerializer serializer = JsonSerializer.Create(new JsonSerializerSettings() { Formatting = Formatting.Indented, NullValueHandling = NullValueHandling.Ignore });
                serializer.Serialize(file, Words);
                //                
                NotSaved = false;
            }
        }

        public void SaveToNewFile()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                DefaultExt = "L2Dict",
                AddExtension = true,
            };
            if (saveFileDialog.ShowDialog() != true) return;
            WorkingFile = saveFileDialog.FileName;
            using (StreamWriter file = File.CreateText(WorkingFile))
            {
                JsonSerializer serializer = JsonSerializer.Create(new JsonSerializerSettings() { Formatting = Formatting.Indented, NullValueHandling = NullValueHandling.Ignore });
                serializer.Serialize(file, Words);
                //                
                NotSaved = false;
            }
        }
    }


    public enum CtrlMode
    {
        Learning = 1,
        Normal = 0
    }

    // COMMANDS
    public partial class WordsTable
    {
        public IEnumerable AllItems { get; set; }
        private ObservableCollection<CWord> ItemsToLearn { get; set; }

        private void SelectNextItemsToLearn_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DataGrid ctrl = ctrl_WordsTable;
            if (ctrl.Items == null || ctrl.Items.Count == 0) return;
            List<CWord> words = new List<CWord>();
            foreach (var item in ctrl.Items) if (item is CWord word) words.Add(word);
            words.ForEach(x => x.LearningSheduler = false );

            Int32 AttemptAfterWhichWordIsWeakKnown = 3;
            Int32 AttemptAfterWhichWordIsWellKnown = 6;
            Int32 AttemptAfterWhichWordIsSuccesfulyLearned = 10;

            Int32 UnknownWordsMinCountInLearningSet = 5;
            Int32 UnknownWordsMaxCountInLearningSet = UnknownWordsMinCountInLearningSet + 5;
            Int32 MaxSizeOfLearningSet = UnknownWordsMaxCountInLearningSet + 20;

            Int32 WordsInTest = 10;
            Int32 UnknownWordsInTest = 2;
            Int32 WellKnownWordsInTest = 1;

            List<CWord> learningSet = words.Where(x => x.LearningSheduler == false && x.LearningUserPriority != null && x.LearningUserPriority <= AttemptAfterWhichWordIsWellKnown).ToList();
            List<CWord> setOfNewWords = words.Where(x => x.LearningSheduler == false && x.LearningUserPriority == null).ToList();
            List<CWord> setOfUnknownWords = learningSet.Where(x => x.LearningUserPriority <= AttemptAfterWhichWordIsWeakKnown).ToList();
            List<CWord> setOfWeakKnownWords = learningSet.Where(x => x.LearningUserPriority > AttemptAfterWhichWordIsWeakKnown && x.LearningUserPriority <= AttemptAfterWhichWordIsWellKnown).ToList();
            List<CWord> setOfWellKnownWords = words.Where(x => x.LearningSheduler == false && x.LearningUserPriority != null && x.LearningUserPriority > AttemptAfterWhichWordIsWellKnown && x.LearningUserPriority < AttemptAfterWhichWordIsSuccesfulyLearned).ToList();
            List<CWord> wordsForTest = new List<CWord>();

            Int32 freeSlotsInLearningSet = MaxSizeOfLearningSet - setOfWeakKnownWords.Count - setOfUnknownWords.Count;
            Int32 rangeOfSlotsInUnknownWordsSet = UnknownWordsMaxCountInLearningSet - UnknownWordsMinCountInLearningSet;
            Int32 freeSlotsInSetOfUnknownWords = Math.Min(UnknownWordsMinCountInLearningSet - setOfUnknownWords.Count, Math.Min(freeSlotsInLearningSet, rangeOfSlotsInUnknownWordsSet));

            if(freeSlotsInSetOfUnknownWords > 0 && setOfNewWords.Count > 0)
            {
                var newWords = setOfNewWords.OrderBy(x => x.CreatedAt).Take(UnknownWordsInTest).ToList();
                newWords.ForEach(x => x.LearningUserPriority = 0);
                setOfUnknownWords.AddRange(newWords);
            }
            
            Random rnd = new Random();

            setOfUnknownWords.ForEach(x => x.Random = GetRandomValue());
            Stack<CWord> stackOfUnknownWords = new Stack<CWord>(setOfUnknownWords.OrderBy(x => x.Random));
            setOfWellKnownWords.ForEach(x => x.Random = GetRandomValue());
            Stack<CWord> stackOfWellKnownWords = new Stack<CWord>(setOfWellKnownWords.OrderBy(x => x.Random));
            setOfWeakKnownWords.ForEach(x => x.Random = GetRandomValue());
            Stack<CWord> stackOfWeakKnownWords = new Stack<CWord>(setOfWeakKnownWords.OrderBy(x => x.Random));

            Int32 counter = 0;
            while(counter++ < UnknownWordsInTest && stackOfUnknownWords.Count > 0) wordsForTest.Add(stackOfUnknownWords.Pop());
            
            counter = 0;
            while(counter++ < WellKnownWordsInTest && stackOfWellKnownWords.Count > 0) wordsForTest.Add(stackOfWellKnownWords.Pop());

            counter = 0;
            while(counter++ < WordsInTest && stackOfWeakKnownWords.Count > 0) wordsForTest.Add(stackOfWeakKnownWords.Pop());
            
            
            counter = wordsForTest.Count;
            if(counter < WordsInTest)
                while(counter++ < WordsInTest && stackOfUnknownWords.Count > 0) wordsForTest.Add(stackOfUnknownWords.Pop());

            counter = wordsForTest.Count;
            if(counter < WordsInTest)
                while(counter++ < WordsInTest && stackOfWellKnownWords.Count > 0) wordsForTest.Add(stackOfWellKnownWords.Pop());

            wordsForTest.ForEach(x => x.LearningSheduler = true);

            Int32 GetRandomValue() => rnd.Next(1, 1001);
        }

        private void SelectNextItemsToLearn_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void MarkAsItemsToLearn_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DataGrid ctrl = ctrl_WordsTable;
            if (ctrl.SelectedItems == null || ctrl.SelectedItems.Count == 0) return;
            List<CWord> selectedItems = new List<CWord>();
            foreach (var item in ctrl.SelectedItems) selectedItems.Add((CWord)item);

            foreach (var item in selectedItems) item.LearningSheduler = true;
        }

        private void MarkAsItemsToLearn_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void MarkAsItemsNotToLearn_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DataGrid ctrl = ctrl_WordsTable;
            if (ctrl.SelectedItems == null || ctrl.SelectedItems.Count == 0) return;
            List<CWord> selectedItems = new List<CWord>();
            foreach (var item in ctrl.SelectedItems) selectedItems.Add((CWord)item);

            foreach (var item in selectedItems) item.LearningSheduler = false;
        }

        private void MarkNewItemsToLearn_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DataGrid ctrl = ctrl_WordsTable;
            if (ctrl.Items == null || ctrl.Items.Count == 0) return;
            List<CWord> words = new List<CWord>();
            foreach (var item in ctrl.Items) if (item is CWord word) words.Add(word);
            words.ForEach(x => x.LearningSheduler = false);

            words.Where(x => x.LearningUserPriority == null).OrderBy(x => x.CreatedAt).Take(5).ForEach(x => x.LearningSheduler = true);
        }

        private void MarkNewItemsToLearn_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void MarkWorstItemsToLearn_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DataGrid ctrl = ctrl_WordsTable;
            if (ctrl.Items == null || ctrl.Items.Count == 0) return;
            List<CWord> words = new List<CWord>();
            foreach (var item in ctrl.Items) if (item is CWord word) words.Add(word);
            words.ForEach(x => x.LearningSheduler = false);

            words.Where(x => x.LearningUserPriority != null && x.LearningUserPriority < 8).OrderBy(x => x.LearningUserPriority).Take(5).ForEach(x => x.LearningSheduler = true);
        }

        private void MarkWorstItemsToLearn_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void MarkAsItemsNotToLearn_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void SwitchToLearnMode_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (AllItems == null) return;
            List<CWord> selectedItems = new List<CWord>();
            foreach(var item in AllItems) if(item is CWord word && word.LearningSheduler == true) selectedItems.Add(word);
            ItemsToLearn = new ObservableCollection<CWord>(selectedItems.OrderBy(x => x.Random));
            //
            DataGrid ctrl = ctrl_WordsTable;
            ctrl.ItemsSource = ItemsToLearn;
            foreach (var col in ctrl.Columns)
            {
                Object tag = DataGridTag.GetTag(col);
                if (tag == null || !(tag is Boolean)) continue;
                if ((Boolean)DataGridTag.GetTag(col)) col.Visibility = Visibility.Collapsed;
            }
            Mode = CtrlMode.Learning;
        }

        private void SwitchToLearnMode_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void SwitchToNormalMode_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DataGrid ctrl = ctrl_WordsTable;
            ctrl.ItemsSource = AllItems;
            foreach (var col in ctrl.Columns)
            {
                Object tag = DataGridTag.GetTag(col);
                if (tag == null || !(tag is Boolean)) continue;
                if ((Boolean)DataGridTag.GetTag(col)) col.Visibility = Visibility.Visible;
            }
            Mode = CtrlMode.Normal;
        }

        private void SwitchToNormalMode_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CommandDlg001_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CommandDlg001_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            WordsLearningList dlg001 = new WordsLearningList() { Owner = ParentWindow };
            
            if (dlg001.ShowDialog() != true) return;            
        }



        private void CommandItemLearn_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            DataGrid ctrl = ctrl_WordsTable;
            CWord selectedItem = ctrl.CurrentItem as CWord;

            e.CanExecute = selectedItem != null;
        }

        private void CommandItemLearn_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DataGrid ctrl = ctrl_WordsTable;
            CWord selectedItem = ctrl.CurrentItem as CWord;

            if (selectedItem == null) return;

            KnowledgeTestDialog dialog = new KnowledgeTestDialog(selectedItem) { Owner = ParentWindow };
            if (dialog.ShowDialog() == true)
            {
                if (dialog.DialogState == EKnowledgeTestDialogState.ShowTest) return;
                if (selectedItem.LearningUserPriority == null) selectedItem.LearningUserPriority = 0;

                selectedItem.LearningUserPriority += dialog.DialogState == EKnowledgeTestDialogState.ShowTestResultWrong ? -1 : 1;
                //if (selectedItem.LearningUserPriority >= 10) selectedItem.LearningSheduler = false;


            }            
        }

        // Command FileCreate


        private void ExecuteCommand_ItemsSaveToFile(object sender, ExecutedRoutedEventArgs e)
        {
            SaveToFile();
        }

        private void ExecuteCommand_ItemsSaveToNewFile(object sender, ExecutedRoutedEventArgs e)
        {
            SaveToNewFile();
        }

        private void ExecureCommand_ItemAddNew(object sender, ExecutedRoutedEventArgs e)
        {
            //DataGrid ctrl = ctrl_DataGrid_Words;
            //List<CWord> selItems = ctrl.SelectedItems.Cast<CWord>().ToList<CWord>();
            //CWord firstSelectedItem = selItems.FirstOrDefault();
            //DateTime startTime = firstSelectedItem.CreatedAt;
            //Int32 secondsPerOneItem = 180;
            //Int32 itemNo = 0;
            //selItems.ForEach(x => x.CreatedAt = startTime + TimeSpan.FromSeconds(itemNo++ * secondsPerOneItem));
            //ctrl.Items.Refresh();
            //return;

            Word_CreateOrEdit newItemDialog = new Word_CreateOrEdit() { Owner = ParentWindow };
            if (newItemDialog.ShowDialog() != true) return;
            DataGrid ctrl = ctrl_WordsTable;
            ctrl.ItemsSource = null;

            Words.Add(newItemDialog.GetResult());
            ctrl.ItemsSource = Words;
            //
            NotSaved = true;
        }

        

        private void ExecuteCommand_ItemInsertNewBefore(object sender, ExecutedRoutedEventArgs e)
        {
            DataGrid ctrl = ctrl_WordsTable;

            Int32 selectedIndex = ctrl.SelectedIndex;
            if (selectedIndex >= 0)
            {
                Int32 itemsSelected = ctrl.SelectedItems.Count;
                Int32 indexOfLastItem = (selectedIndex + itemsSelected - 1);
                indexOfLastItem = Math.Min(indexOfLastItem, Words.Count - 1);
                Int32 lesson = Words[indexOfLastItem].LessonNumber;
                Int32 order = Words[indexOfLastItem].WordOrder;

                var newItems = itemsSelected.NewSet<CWord>(() => new CWord() { LessonNumber = lesson, WordOrder = ++order });
                ctrl.ItemsSource = null;
                newItems.ForEach(x => Words.Insert(indexOfLastItem + 1, x));
                ctrl.ItemsSource = Words;
            }
        }

        private void ExecuteCommand_ItemInsertNewAfter(object sender, ExecutedRoutedEventArgs e)
        {
            //TODO ExecuteCommand_ItemInsertNewAfter
            MessageBox.Show("Операция не реализована");

            //DataGrid ctrl = ctrl_WordsTable;

            //Int32 selectedIndex = ctrl.SelectedIndex;
            //if (selectedIndex >= 0)
            //{
            //    Int32 itemsSelected = ctrl_WordsTable.SelectedItems.Count;
            //    ctrl_WordsTable.ItemsSource = null;

            //    Words.Insert(); .InsertRange(selectedIndex, itemsSelected.NewSet(() => new CWord()));
            //    ctrl_WordsTable.ItemsSource = Words;
            //}
        }

        private void ExecuteCommand_ItemEdit(object sender, ExecutedRoutedEventArgs e)
        {
            //TODO not implemented

            DataGrid ctrl = ctrl_WordsTable;
            CWord selectedItem = ctrl.CurrentItem as CWord;
            if (selectedItem == null) return;

            Word_CreateOrEdit editDialog = new Word_CreateOrEdit(selectedItem) { Owner = ParentWindow };
            if (editDialog.ShowDialog() != true) return;
            (ctrl.CurrentItem as CWord).Update(editDialog.GetResult());
            ctrl.Items.Refresh();
            
            
        }

        private void ExecuteCommand_ItemsEdit(object sender, ExecutedRoutedEventArgs e)
        {
            //TODO not implemented
            MessageBox.Show("Операция \"Редактирование нескольких елементов\" не реализована");
        }

        private void ItemEdit_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Mode == CtrlMode.Normal;
        }
        

        private void ExecuteCommand_ItemsEnumerate(object sender, ExecutedRoutedEventArgs e)
        {
            DataGrid ctrl = ctrl_WordsTable;

            Int32 selectedIndex = ctrl.SelectedIndex;
            if (selectedIndex >= 0 && selectedIndex <= Words.Count)
            {
                Int32 itemsSelected = ctrl_WordsTable.SelectedItems.Count;
                Int32 indexOfFirstItem = selectedIndex;
                CWord word = Words[indexOfFirstItem];
                Int32 lesson = word.LessonNumber;
                Int32 order = word.WordOrder;
                ctrl_WordsTable.ItemsSource = null;
                Int32 countOfSelItems = Math.Min(selectedIndex + itemsSelected, Words.Count) - selectedIndex;
                Words.ForEach(selectedIndex, countOfSelItems, x => { x.LessonNumber = lesson; x.WordOrder = order++; });
                ctrl_WordsTable.ItemsSource = Words;
            }
        }

        private void ExecuteCommand_ItemsEnumerate_Obsolete(object sender, ExecutedRoutedEventArgs e)
        {
            DataGrid ctrl = ctrl_WordsTable;
            List<CWord> selItems = ctrl.SelectedItems.Cast<CWord>().ToList<CWord>();
            CWord firstSelectedItem = selItems.FirstOrDefault();
            DateTime startTime = firstSelectedItem.CreatedAt;
            //Int32 secondsPerOneItem = 180;
            //Int32 itemNo = 0;
            //selItems.ForEach(x => x.CreatedAt = startTime + TimeSpan.FromSeconds(itemNo++ * secondsPerOneItem));
            ctrl.Items.Refresh();
            return;
        }

        private void ExecuteCommand_ItemsDelete(object sender, ExecutedRoutedEventArgs e)
        {
            //TODO not implemented
        }
    }



    public static class DataGridTag
    {
        public static readonly DependencyProperty TagProperty = DependencyProperty.RegisterAttached(
           "Tag",
           typeof(object),
           typeof(DataGridTag),
           new FrameworkPropertyMetadata(null));

        public static object GetTag(DependencyObject dependencyObject)
        {
            return dependencyObject.GetValue(TagProperty);
        }

        public static void SetTag(DependencyObject dependencyObject, object value)
        {
            dependencyObject.SetValue(TagProperty, value);
        }
    }
}
