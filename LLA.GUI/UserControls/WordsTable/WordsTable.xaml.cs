using LLA.Core;
using LLA.GUI.Dialogs;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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


namespace LLA.GUI.UserControls
{
    public partial class WordsTable : UserControl
    {
        Window ParentWindow;
        public String WorkingFile = String.Empty;
        public Boolean NotSaved { get; private set; }
        public List<CWord> Words = new List<CWord>();

        public WordsTable()
        {
            InitializeComponent();
            InitializeDatagrid(ctrl_WordsTable);
            Loaded += WordsTable_Loaded;
        }

        private void WordsTable_Loaded(object sender, RoutedEventArgs e)
        {
            ParentWindow = Window.GetWindow(this);
            BindCommands(ParentWindow, ctrl_WordsTable);
        }

        private void BindCommands(Window parentWindow, DataGrid ctrl)
        {
            parentWindow.CommandBindings.Add(new CommandBinding(WordsTable_Commands.ItemsLoadFromFile, CommandBinding_OpenFile));
            parentWindow.CommandBindings.Add(new CommandBinding(WordsTable_Commands.ItemsSaveToFile, CommandBinding_SaveFile));
            parentWindow.CommandBindings.Add(new CommandBinding(WordsTable_Commands.ItemsSaveToNewFile, CommandBinding_SaveFileAs));
            //
            parentWindow.CommandBindings.Add(new CommandBinding(WordsTable_Commands.ItemAddNew, Commands_WordsDatagrid_ItemAddNew));
            parentWindow.CommandBindings.Add(new CommandBinding(WordsTable_Commands.ItemInsertNewBefore, Commands_WordsDatagrid_ItemInsertNewBefore));
            parentWindow.CommandBindings.Add(new CommandBinding(WordsTable_Commands.ItemInsertNewAfter, Commands_WordsDatagrid_ItemInsertNewAfter));
            parentWindow.CommandBindings.Add(new CommandBinding(WordsTable_Commands.ItemEdit, Commands_WordsDatagrid_ItemEdit));
            parentWindow.CommandBindings.Add(new CommandBinding(WordsTable_Commands.ItemsEdit, Commands_WordsDatagrid_ItemsEdit));
            parentWindow.CommandBindings.Add(new CommandBinding(WordsTable_Commands.ItemsEnumerate, CommandBinding_TableItems_Enumerate));
            parentWindow.CommandBindings.Add(new CommandBinding(WordsTable_Commands.ItemsDelete, Commands_WordsDatagrid_ItemsDelete));
            //
            //
            //
            ctrl.CommandBindings.Add(new CommandBinding(WordsTable_Commands.ItemsLoadFromFile, CommandBinding_OpenFile));
            ctrl.CommandBindings.Add(new CommandBinding(WordsTable_Commands.ItemsSaveToFile, CommandBinding_SaveFile));
            ctrl.CommandBindings.Add(new CommandBinding(WordsTable_Commands.ItemsSaveToNewFile, CommandBinding_SaveFileAs));
            //
            ctrl.CommandBindings.Add(new CommandBinding(WordsTable_Commands.ItemAddNew, Commands_WordsDatagrid_ItemAddNew));
            ctrl.CommandBindings.Add(new CommandBinding(WordsTable_Commands.ItemInsertNewBefore, Commands_WordsDatagrid_ItemInsertNewBefore));
            ctrl.CommandBindings.Add(new CommandBinding(WordsTable_Commands.ItemInsertNewAfter, Commands_WordsDatagrid_ItemInsertNewAfter));
            ctrl.CommandBindings.Add(new CommandBinding(WordsTable_Commands.ItemEdit, Commands_WordsDatagrid_ItemEdit));
            ctrl.CommandBindings.Add(new CommandBinding(WordsTable_Commands.ItemsEdit, Commands_WordsDatagrid_ItemsEdit));
            ctrl.CommandBindings.Add(new CommandBinding(WordsTable_Commands.ItemsEnumerate, CommandBinding_TableItems_Enumerate));
            ctrl.CommandBindings.Add(new CommandBinding(WordsTable_Commands.ItemsDelete, Commands_WordsDatagrid_ItemsDelete));
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
            ctrl.KeyUp += Ctrl_DataGrid_Words_KeyUp;
            //
            InitializeDatagridColumns(ctrl);
            InitializeDatagridContextMenu(ctrl);
            
            //
            ctrl.ItemsSource = Words;
        }

        private void InitializeDatagridColumns(DataGrid ctrl)
        {
            CWord word = null;
            DataGridTextColumn createdAt = new DataGridTextColumn
            {
                Header = "Создано",
                Binding = new Binding(nameof(word.CreatedAt))
            };
            DataGridTextColumn lessonNumber = new DataGridTextColumn
            {
                Header = "Урок",
                Binding = new Binding(nameof(word.LessonNumber))
            };
            DataGridTextColumn wordOrder = new DataGridTextColumn
            {
                Header = "№ п.п.",
                Binding = new Binding(nameof(word.WordOrder))
            };
            DataGridTextColumn writingEng = new DataGridTextColumn
            {
                Header = "ENG",
                Binding = new Binding(nameof(word.WritingEng))
            };
            DataGridTextColumn speling = new DataGridTextColumn
            {
                Header = "Произношение",
                Binding = new Binding(nameof(word.Speling))
            };
            DataGridTextColumn writingUkr = new DataGridTextColumn
            {
                Header = "UKR",
                Binding = new Binding(nameof(word.WritingUkr))
            };
            DataGridTextColumn remarksUkr = new DataGridTextColumn
            {
                Header = "UKR (примечания)",
                Binding = new Binding(nameof(word.RemarksUkr))
            };
            DataGridTextColumn spelingByUkr = new DataGridTextColumn
            {
                Header = "Произношение по UKR",
                Binding = new Binding(nameof(word.SpelingByUkr))
            };
            DataGridTextColumn writingRus = new DataGridTextColumn
            {
                Header = "RUS",
                Binding = new Binding(nameof(word.WritingRus))
            };
            DataGridTextColumn remarksRus = new DataGridTextColumn
            {
                Header = "RUS (примечания)",
                Binding = new Binding(nameof(word.RemarkssRus))
            };
            DataGridTextColumn spelingByRus = new DataGridTextColumn
            {
                Header = "Произношение по RUS",
                Binding = new Binding(nameof(word.SpelingByRus))
            };

            ctrl.Columns.Add(createdAt);
            ctrl.Columns.Add(lessonNumber);
            ctrl.Columns.Add(wordOrder);
            ctrl.Columns.Add(writingEng);
            ctrl.Columns.Add(speling);
            ctrl.Columns.Add(writingUkr);
            ctrl.Columns.Add(remarksUkr);
            ctrl.Columns.Add(spelingByUkr);
            ctrl.Columns.Add(writingRus);
            ctrl.Columns.Add(remarksRus);
            ctrl.Columns.Add(spelingByRus);
        }

        private void InitializeDatagridContextMenu(DataGrid ctrl)
        {   
            MenuItem itemsLoadFromFile = new MenuItem()
            {
                Header = "Загрузить из файла ...",
                Command = WordsTable_Commands.ItemsLoadFromFile
            };
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
            file.Items.Add(itemsLoadFromFile);
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

        private void Commands_WordsDatagrid_ItemAddNew(object sender, ExecutedRoutedEventArgs e)
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

            Words_Create newItemDialog = new Words_Create() { Owner = ParentWindow };
            if (newItemDialog.ShowDialog() != true) return;
            DataGrid ctrl = ctrl_WordsTable;
            ctrl.ItemsSource = null;
            CWord word = newItemDialog.Word;
            word.CreatedAt = DateTime.Now;
            Words.Add(word);
            ctrl.ItemsSource = Words;
            //
            NotSaved = true;
            ParentWindow.Title = $"{WorkingFile} *";
        }

        private void Commands_WordsDatagrid_ItemInsertNewBefore(object sender, ExecutedRoutedEventArgs e)
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
                Words.InsertRange(indexOfLastItem + 1, newItems);
                ctrl.ItemsSource = Words;
            }
        }

        private void Commands_WordsDatagrid_ItemInsertNewAfter(object sender, ExecutedRoutedEventArgs e)
        {
            DataGrid ctrl = ctrl_WordsTable;

            Int32 selectedIndex = ctrl.SelectedIndex;
            if (selectedIndex >= 0)
            {
                Int32 itemsSelected = ctrl_WordsTable.SelectedItems.Count;
                ctrl_WordsTable.ItemsSource = null;

                Words.InsertRange(selectedIndex, itemsSelected.NewSet(() => new CWord()));
                ctrl_WordsTable.ItemsSource = Words;
            }
        }

        private void Commands_WordsDatagrid_ItemEdit(object sender, ExecutedRoutedEventArgs e)
        {
            //TODO not implemented
            MessageBox.Show("Операция \"Редактирование елемента\" не реализована");
        }

        private void Commands_WordsDatagrid_ItemsEdit(object sender, ExecutedRoutedEventArgs e)
        {
            //TODO not implemented
            MessageBox.Show("Операция \"Редактирование нескольких елементов\" не реализована");
        }

        private void CommandBinding_TableItems_Enumerate(object sender, ExecutedRoutedEventArgs e)
        {
            DataGrid ctrl = ctrl_WordsTable;

            Int32 selectedIndex = ctrl.SelectedIndex;
            if (selectedIndex >= 0 && selectedIndex <= Words.Count - 1)
            {
                Int32 itemsSelected = ctrl_WordsTable.SelectedItems.Count;
                Int32 indexOfFirstItem = selectedIndex;
                CWord word = Words[indexOfFirstItem];
                Int32 lesson = word.LessonNumber;
                Int32 order = word.WordOrder;
                ctrl_WordsTable.ItemsSource = null;
                Int32 countOfSelItems = Math.Min(selectedIndex + itemsSelected, Words.Count) - selectedIndex;
                Words.GetRange(selectedIndex, countOfSelItems).ForEach(x => { x.LessonNumber = lesson; x.WordOrder = order++; });
                ctrl_WordsTable.ItemsSource = Words;
            }
        }

        private void CommandBinding_WordsDatagrid_ItemsEnumerate(object sender, ExecutedRoutedEventArgs e)
        {
            DataGrid ctrl = ctrl_WordsTable;
            List<CWord> selItems = ctrl.SelectedItems.Cast<CWord>().ToList<CWord>();
            CWord firstSelectedItem = selItems.FirstOrDefault();
            DateTime startTime = firstSelectedItem.CreatedAt;
            Int32 secondsPerOneItem = 180;
            Int32 itemNo = 0;
            selItems.ForEach(x => x.CreatedAt = startTime + TimeSpan.FromSeconds(itemNo++ * secondsPerOneItem));
            ctrl.Items.Refresh();
            return;
        }

        private void Commands_WordsDatagrid_ItemsDelete(object sender, ExecutedRoutedEventArgs e)
        {
            //TODO not implemented
        }

        private void CommandBinding_OpenFile(object sender, ExecutedRoutedEventArgs e)
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
            if (openFileDialog.ShowDialog() == true)
            {
                OpenFile(openFileDialog);
            }
        }

        private void OpenFile(OpenFileDialog openFileDialog)
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
                ctrl_WordsTable.ItemsSource = Words;
                WorkingFile = fileName;
                Window mainWindow = Window.GetWindow(this);
                mainWindow.Title = WorkingFile;
            }
        }

        private void CommandBinding_SaveFile(object sender, ExecutedRoutedEventArgs e)
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
                Window mainWindow = Window.GetWindow(this);
                mainWindow.Title = WorkingFile;
                NotSaved = false;
            }
            //using(FileStream sourceStream = new FileStream(WorkingFile, FileMode.Open))
            //{
            //    using (GZipStream compressionStream = new GZipStream(sourceStream, CompressionMode.Compress))
            //    {
            //        sourceStream.CopyTo(compressionStream);
            //    }
            //}


            //using(var zipStream = new GZipStream())
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
    }
}
