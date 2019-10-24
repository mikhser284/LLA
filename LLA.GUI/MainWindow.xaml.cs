﻿using System;
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
using System.IO.Compression;

namespace LLA.GUI
{
    public partial class MainWindow : Window
    {
        public List<CWord> Words = new List<CWord>();
        public String WorkingFile = String.Empty;

        public MainWindow()
        {
            InitializeComponent();
            CommandBindings.Add(new CommandBinding(Commands_Application.AppClose, CommandBinding_CloseApp));
            CommandBindings.Add(new CommandBinding(Commands_Application.FileOpen, CommandBinding_OpenFiles));
            CommandBindings.Add(new CommandBinding(Commands_Application.FileSave, CommandBinding_SaveFile));
            CommandBindings.Add(new CommandBinding(Commands_Application.FileSaveAs, CommandBinding_SaveFileAs));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Properties, CommandBinding_ShowDialog_Settings));
            //
            CommandBindings.Add(new CommandBinding(Commands_WordsDatagrid.ItemAddNew, Commands_WordsDatagrid_ItemAddNew));
            CommandBindings.Add(new CommandBinding(Commands_WordsDatagrid.ItemInsertNewBefore, Commands_WordsDatagrid_ItemInsertNewBefore));
            CommandBindings.Add(new CommandBinding(Commands_WordsDatagrid.ItemInsertNewAfter, Commands_WordsDatagrid_ItemInsertNewAfter));
            CommandBindings.Add(new CommandBinding(Commands_WordsDatagrid.ItemEdit, Commands_WordsDatagrid_ItemEdit));
            CommandBindings.Add(new CommandBinding(Commands_WordsDatagrid.ItemsEdit, Commands_WordsDatagrid_ItemsEdit));
            CommandBindings.Add(new CommandBinding(Commands_WordsDatagrid.ItemsEnumerate, CommandBinding_WordsDatagrid_ItemsEnumerate));            
            CommandBindings.Add(new CommandBinding(Commands_WordsDatagrid.ItemsDelete, Commands_WordsDatagrid_ItemsDelete));
            //            
            ctrl_DataGrid_Words.ItemsSource = Words;
        }


        private void Commands_WordsDatagrid_ItemAddNew(object sender, ExecutedRoutedEventArgs e)
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
        
        private void Commands_WordsDatagrid_ItemInsertNewBefore(object sender, ExecutedRoutedEventArgs e)
        {
            DataGrid ctrl = ctrl_DataGrid_Words;

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
            //DataGrid ctrl = ctrl_DataGrid_Words;
            //List<CWord> selItems = ctrl.SelectedItems.Cast<CWord>().ToList<CWord>();
            //CWord firstSelectedItem = selItems.FirstOrDefault();
            //DateTime startTime = firstSelectedItem.CreatedAt;
            //Int32 secondsPerOneItem = 180;
            //Int32 itemNo = 0;
            //selItems.ForEach(x => x.CreatedAt = startTime + TimeSpan.FromSeconds(itemNo++ * secondsPerOneItem));
            //ctrl.Items.Refresh();
            //return;

            Words_Create newItemDialog = new Words_Create() { Owner = this };
            if (newItemDialog.ShowDialog() != true) return;
            DataGrid ctrl = ctrl_DataGrid_Words;
            ctrl.ItemsSource = null;
            CWord word = newItemDialog.Word;
            word.CreatedAt = DateTime.Now;
            Words.Add(word);
            ctrl.ItemsSource = Words;
        }

        private void Commands_WordsDatagrid_ItemEdit(object sender, ExecutedRoutedEventArgs e)
        {
            //TODO not implemented
        }

        private void Commands_WordsDatagrid_ItemsEdit(object sender, ExecutedRoutedEventArgs e)
        {
            //TODO not implemented
        }


        private void CommandBinding_WordsDatagrid_ItemsEnumerate(object sender, ExecutedRoutedEventArgs e)
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

        private void Commands_WordsDatagrid_ItemsDelete(object sender, ExecutedRoutedEventArgs e)
        {
            //TODO not implemented
        }



        private void CommandBinding_ShowDialog_Settings(object sender, ExecutedRoutedEventArgs e)
        {
            SettingsDialog settingsDialog = new SettingsDialog();
            if (settingsDialog.ShowDialog() == true)
            {

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
                Int32 lesson = word.LessonNumber;
                Int32 order = word.WordOrder;
                ctrl_DataGrid_Words.ItemsSource = null;
                Int32 countOfSelItems = Math.Min(selectedIndex + itemsSelected, Words.Count) - selectedIndex;
                Words.GetRange(selectedIndex, countOfSelItems).ForEach(x => { x.LessonNumber = lesson; x.WordOrder = order++; });
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
