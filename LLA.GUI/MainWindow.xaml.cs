using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LLA.Core;
using LLA.GUI.AppCommands;
using LLA.GUI.Dialogs;
using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace LLA.GUI
{
    public partial class MainWindow : Window
    {
        public List<CWord> Words = new List<CWord>();
        public String workingFile = String.Empty;

        public MainWindow()
        {
            InitializeComponent();
            CommandBindings.Add(new CommandBinding(Commands_Application.AppClose, CommandBinding_CloseApp));
            CommandBindings.Add(new CommandBinding(Commands_Application.FileOpen, CommandBinding_OpenFiles));
            CommandBindings.Add(new CommandBinding(Commands_Application.FileSave, CommandBinding_SaveFile));
            CommandBindings.Add(new CommandBinding(Commands_Application.FileSaveAs, CommandBinding_SaveFileAs));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Delete, CommandBinding_TableItem_Delete));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Paste, CommandBinding_TableItem_Insert));
            CommandBindings.Add(new CommandBinding(MediaCommands.IncreaseBass, CommandBinding_TableItems_Enumerate));
            CommandBindings.Add(new CommandBinding(MediaCommands.BoostBass, CommandBinding_TableItems_InsertBelow));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Properties, CommandBinding_ShowDialog_Settings));
            CommandBindings.Add(new CommandBinding(Commands_WordsDatagrid.SpreadData, CommandBinding_WordsDatagrid_SpreadData));
            //
            Words = CreateWordsTestSet();
            ctrl_DataGrid_Words.ItemsSource = Words;
        }



        private void CommandBinding_ShowDialog_Settings(object sender, ExecutedRoutedEventArgs e)
        {
            SettingsDialog settingsDialog = new SettingsDialog();
            if (settingsDialog.ShowDialog() == true)
            {

            }
        }

        private void CommandBinding_TableItem_Delete(object sender, ExecutedRoutedEventArgs e)
        {
            //TODO not implemented
        }

        private void CommandBinding_TableItem_Insert(object sender, ExecutedRoutedEventArgs e)
        {
            DataGrid ctrl = ctrl_DataGrid_Words;

            Int32 selectedIndex = ctrl.SelectedIndex;
            if (selectedIndex >= 0)
            {
                Int32 itemsSelected = ctrl_DataGrid_Words.SelectedItems.Count;
                ctrl_DataGrid_Words.ItemsSource = null;

                Words.InsertRange(selectedIndex, itemsSelected.NewSet(() => new CWord()));
                ctrl_DataGrid_Words.ItemsSource = Words;
            }
        }

        private void CommandBinding_TableItems_InsertBelow(object sender, ExecutedRoutedEventArgs e)
        {
            DataGrid ctrl = ctrl_DataGrid_Words;

            Int32 selectedIndex = ctrl.SelectedIndex;
            if (selectedIndex >= 0)
            {
                Int32 itemsSelected = ctrl_DataGrid_Words.SelectedItems.Count;
                Int32 indexOfLastItem = (selectedIndex + itemsSelected - 1);
                indexOfLastItem = Math.Min(indexOfLastItem, Words.Count - 1);
                Int32 lesson = Words[indexOfLastItem].IndexA_Lesson;
                Int32 order = Words[indexOfLastItem].IndexB_Order;

                var newItems = itemsSelected.NewSet<CWord>(() => new CWord() { IndexA_Lesson = lesson, IndexB_Order = ++order });
                ctrl_DataGrid_Words.ItemsSource = null;
                Words.InsertRange(indexOfLastItem + 1, newItems);
                ctrl_DataGrid_Words.ItemsSource = Words;
            }
        }

        private void CommandBinding_TableItems_Enumerate(object sender, ExecutedRoutedEventArgs e)
        {
            DataGrid ctrl = ctrl_DataGrid_Words;

            Int32 selectedIndex = ctrl.SelectedIndex;
            if (selectedIndex >= 0 && selectedIndex <= Words.Count - 1)
            {
                Int32 itemsSelected = ctrl_DataGrid_Words.SelectedItems.Count;
                Int32 indexOfFirstItem = selectedIndex;
                CWord word = Words[indexOfFirstItem];
                Int32 lesson = word.IndexA_Lesson;
                Int32 order = word.IndexB_Order;
                ctrl_DataGrid_Words.ItemsSource = null;
                Int32 countOfSelItems = Math.Min(selectedIndex + itemsSelected, Words.Count) - selectedIndex;
                Words.GetRange(selectedIndex, countOfSelItems).ForEach(x => { x.IndexA_Lesson = lesson; x.IndexB_Order = order++; });
                ctrl_DataGrid_Words.ItemsSource = Words;
            }
        }

        private void CommandBinding_OpenFiles(object sender, ExecutedRoutedEventArgs e)
        {
            Dictionary<Int32, String> filter = new Dictionary<Int32, String>
            {
                { 1, "L2Dict (*.L2Dict)|*L2Dict" },
                { 2, "Все файлы (*.*)|*.*" }
            };

            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Multiselect = false,
                Filter = String.Join("|", filter.Values)
            };
            if (openFileDialog.ShowDialog() == true) OpenFiles(openFileDialog);
        }

        private void CommandBinding_CloseApp(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }

        private List<CWord> CreateWordsTestSet()
        {
            List<CWord> words = new List<CWord>
            {
                new CWord( 12,  23, "strained",      "напружений; вимучений"),
                new CWord( 08,  30, "cater",         "поставляти; враховувати; приймати до уваги"),
                new CWord( 15,   1, "put up",        "миритись; знайомитись"),
                new CWord( 12,  19, "impair",        "ослаблювати; знижувати; погіршувати"),
                new CWord( 11,  12, "fairly",        "належним чином; досить; цілком"),
                new CWord( 12,  10, "covet",         "домагатись; сильно бажати"),
                new CWord(  8,  24, "contribute",    "вносити вклад"),
                new CWord(  3,   3, "claim",         "стверджувати; вимагати"),
                new CWord(  4,  75, "manifested",    "очевидний, доведений"),
                new CWord( 12,  12, "deprive",       "відбирати; позбавляти"),
                new CWord(  8,  49, "preservation",  "охрана; сохранность"),
                new CWord( 16,   5, "buck",          "самець тварини",                              "(оленя, антилопи, зайця, тощо)"),
                new CWord( 17,  20, "condiment",     "приправа; спеції"),
                new CWord( 15,   2, "inevitably",    "неминуче"),
                new CWord(  4,   8, "exquisite",     "вишуканий"),
                new CWord( 12,  32, "desperate",     "безнадійний; відчайдушний"),
                new CWord( 13,  09, "stove",         "пічка"),
                new CWord( 13,  11, "starving",      "голодаючий"),
                new CWord( 16,  15, "settle",        "приймати рішення"),
                new CWord( 17,  21, "mustard",       "гірчиця"),
                new CWord( 17,  33, "mild",          "м'який; негострий; не крепкий",               "(про їжу або напої)"),
                new CWord( 18,   6, "bargain",       "договір; торгова угода"),
                new CWord( 18,   7, "no doubt",      "безумовно"),
            };
            return words;
        }

        private void OpenFiles(OpenFileDialog openFileDialog)
        {
            switch (openFileDialog.FilterIndex)
            {
                case 1: // L2Dict
                    {
                        LoadDictionary(openFileDialog.FileNames.FirstOrDefault());
                        break;
                    }
                default:
                    {
                        MessageBox.Show("Операция не поддерживается.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.ServiceNotification);
                        break;
                    }
            }
        }

        private void LoadDictionary(String fileName)
        {
            if (String.IsNullOrEmpty(fileName))
            {
                MessageBox.Show("Не удалось получить название файла.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.ServiceNotification);
                return;
            }
            using (StreamReader file = File.OpenText(fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                Words = (List<CWord>)serializer.Deserialize(file, typeof(List<CWord>));
                ctrl_DataGrid_Words.ItemsSource = Words;
            }
        }

        private void CommandBinding_SaveFile(object sender, ExecutedRoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(workingFile))
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog()
                {
                    DefaultExt = "L2Dict",
                    AddExtension = true,
                };
                if (saveFileDialog.ShowDialog() != true) return;
                workingFile = saveFileDialog.FileName;
            }
            using (StreamWriter file = File.CreateText(workingFile))
            {
                JsonSerializer serializer = JsonSerializer.Create(new JsonSerializerSettings() { Formatting = Formatting.Indented, NullValueHandling = NullValueHandling.Ignore });
                serializer.Serialize(file, Words);
            }
        }

        private void CommandBinding_SaveFileAs(object sender, ExecutedRoutedEventArgs e)
        {
            //TODO not implemented
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
                { "UKR",  cultureUkr },
                { "UKR (примечания)",  cultureUkr },
                { "ENG",  cultureEng },
                { "RUS",  cultureRus },
                { "RUS (примечания)",  cultureRus },
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

        private void Ctrl_DataGrid_Words_KeyUp(object sender, KeyEventArgs e)
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

        private void CommandBinding_WordsDatagrid_SpreadData(object sender, ExecutedRoutedEventArgs e)
        {
            DataGrid ctrl = ctrl_DataGrid_Words;
            List<CWord> selItems = ctrl.SelectedItems.Cast<CWord>().ToList<CWord>();
            CWord firstSelectedItem = selItems.FirstOrDefault();
            DateTime startTime = firstSelectedItem.CreatedAt;
            Int32 secondsPerOneItem = 180;
            Int32 itemNo = 0;
            selItems.ForEach(x => x.CreatedAt = startTime + TimeSpan.FromSeconds(itemNo++ * secondsPerOneItem));
            ctrl.Items.Refresh();
            return;
        }
    }

    //public static class Ext_DataGrid
    //{
    //    public static List<T> GetSelectedItems<T>(this DataGrid ctrl)
    //    {
    //        if (ctrl == null) return new List<T>();
    //        if (ctrl.ItemsSource.GetType().GetGenericArguments().Single() != typeof(T)) throw new ArgumentException("Incorrect collection type");

    //        List<T> selectedItems = new List<T>();
    //        var = ctrl.SelectedItems

    //        return selectedItems;
    //    }

    //}

    public static class Ext_Linq
    {
        public static List<T> NewSet<T>(this Int32 count, Func<T> constructor)
        {
            List<T> set = new List<T>();
            for (Int32 i = 0; i < count; i++)
            {
                set.Add(constructor());
            }
            return set;
        }

    }
}
