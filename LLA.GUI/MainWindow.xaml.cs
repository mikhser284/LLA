using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Win32;

namespace LLA.GUI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            BindCommands();
            //
            UserSession.Load();
            foreach (var fileName in UserSession.Data.OpenedFiles) WordsTable.LoadFromFile(fileName, ctrl_Files);
        }

        private void BindCommands()
        {
            CommandBindings.Add(new CommandBinding(AppCommands.FileCreate, CommandFileCreate_Executed, CommandFileCreate_CanExecute));
            CommandBindings.Add(new CommandBinding(AppCommands.FileOpen, CommandFileOpen_Executed, CommandFileOpen_CanExecute));
            CommandBindings.Add(new CommandBinding(AppCommands.FilesOpenLastSession, CommandFilesOpenLastSession_Executed, CommandFilesOpenLastSession_CanExecute));
            CommandBindings.Add(new CommandBinding(AppCommands.FileSave, CommandFileSave_Executed, CommandFileSave_CanExecute));
            CommandBindings.Add(new CommandBinding(AppCommands.FileSaveAs, FileSaveAs_CommandExecuted, CommandFileSaveAs_CanExecute));
            CommandBindings.Add(new CommandBinding(AppCommands.FilesSaveAll, CommandFilesSaveAll_Executed, CommandFilesSaveAll_CanExecute));
            CommandBindings.Add(new CommandBinding(AppCommands.FileClose, CommandFileClose_Executed, CommandFileClose_CanExecute));
            //
            CommandBindings.Add(new CommandBinding(AppCommands.TabSwitchToNext, CommandTabSwitchToNext_Executed, CommandTabSwitchToNext_CanExecute));
            CommandBindings.Add(new CommandBinding(AppCommands.TabSwitchToPrev, CommandTabSwitchToPrev_Executed, CommandTabSwitchToPrev_CanExecute));
            //
            CommandBindings.Add(new CommandBinding(AppCommands.Print, CommandPrint_Executed, CommandPrint_CanExecute));
            //
            CommandBindings.Add(new CommandBinding(AppCommands.AppClose, CommandAppClose_Executed, CommandAppClose_CanExecute));
            //
            //
            CommandBindings.Add(new CommandBinding(AppCommands.Undo, CommandUndo_Executed, CommandUndo_CanExecute));
            CommandBindings.Add(new CommandBinding(AppCommands.Redo, CommandRedo_Executed, CommandRedo_CanExecute));
            //
            //
            CommandBindings.Add(new CommandBinding(AppCommands.AppSettings, CommandAppSettings_Executed, CommandAppSettings_CanExecute));
            CommandBindings.Add(new CommandBinding(AppCommands.AppAbout, CommandAppAbout_Executed, CommandAppAbout_CanExecute));
        }

        private WordsTable GetSelectedWordsTable()
        {
            Int32 selectedTabIndex = ctrl_Files.SelectedIndex;
            TabItem selectedTabItem = (TabItem)ctrl_Files.Items[selectedTabIndex];
            return (WordsTable) selectedTabItem.Content;
        }

        private List<WordsTable> GetOpenedWordTables()
        {
            return ctrl_Files.Items.Cast<TabItem>().Where(x => x.Content != null && x.Content is WordsTable).Select(x => x.Content as WordsTable).ToList();
        }
    }


    public partial class MainWindow
    {
        // Command FileCreate

        private void CommandFileCreate_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            //TODO CommandFileCreate_CanExecute
            e.CanExecute = true;
        }

        private void CommandFileCreate_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //TODO CommandFileCreate_Executed
            MessageBox.Show("Не реализовано");
        }

        // Command FileOpen

        private void CommandFileOpen_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CommandFileOpen_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Dictionary<Int32, String> filter = new Dictionary<Int32, String>
            {
                {1, "L2Dict (*.L2Dict)|*L2Dict"},
                {2, "Все файлы (*.*)|*.*"}
            };

            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Multiselect = true,
                Filter = String.Join("|", filter.Values)
            };
            if (openFileDialog.ShowDialog() == false) return;

            HashSet<String> alreadyOpenedFiles = new HashSet<String>();
            GetOpenedWordTables().ForEach(x => alreadyOpenedFiles.Add(x.WorkingFile));

            foreach(var fileName in openFileDialog.FileNames)
            {
                if (alreadyOpenedFiles.Contains(fileName)) continue;
                WordsTable.LoadFromFile(fileName, ctrl_Files);
            }
        }

        //Command FilesOpenLastSession

        private void CommandFilesOpenLastSession_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            //TODO Implement CommandFilesOpenLastSession_CanExecute
            e.CanExecute = true;
        }

        private void CommandFilesOpenLastSession_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //TODO Implement CommandFilesOpenLastSession_Executed
            MessageBox.Show($"Метод \"{nameof(CommandFilesOpenLastSession_Executed)}\" не реализован", "Не реализовано",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        //Command FileSave

        private void CommandFileSave_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CommandFileSave_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            GetSelectedWordsTable()?.SaveToFile();
        }

        //Command FileSaveAs

        private void CommandFileSaveAs_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            //TODO Implement CommandFileSaveAs_CanExecute
            e.CanExecute = true;
        }

        private void FileSaveAs_CommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            GetSelectedWordsTable()?.SaveToNewFile();
        }

        //Command FilesSaveAll

        private void CommandFilesSaveAll_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CommandFilesSaveAll_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            GetOpenedWordTables().ForEach(x => x.SaveToFile());
        }

        //Command FileClose

        private void CommandFileClose_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CommandFileClose_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            GetSelectedWordsTable()?.CloseFile();
        }

        //Command Print

        private void CommandPrint_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            //TODO Implement CommandPrint_CanExecute
            e.CanExecute = true;
        }

        private void CommandPrint_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //TODO Implement CommandPrint_Executed
            MessageBox.Show($"Метод \"{nameof(CommandPrint_Executed)}\" не реализован", "Не реализовано",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        //Command AppClose

        private void CommandAppClose_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            //TODO Implement CommandAppClose_CanExecute
            e.CanExecute = true;
        }

        private void CommandAppClose_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //TODO Implement CommandAppClose_Executed
            MessageBox.Show($"Метод \"{nameof(CommandAppClose_Executed)}\" не реализован", "Не реализовано",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        //Command Undo

        private void CommandUndo_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            //TODO Implement CommandUndo_CanExecute
            e.CanExecute = true;
        }

        private void CommandUndo_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //TODO Implement CommandUndo_Executed
            MessageBox.Show($"Метод \"{nameof(CommandUndo_Executed)}\" не реализован", "Не реализовано",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        //Command Redo

        private void CommandRedo_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            //TODO Implement CommandRedo_CanExecute
            e.CanExecute = true;
        }

        private void CommandRedo_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //TODO Implement CommandRedo_Executed
            MessageBox.Show($"Метод \"{nameof(CommandRedo_Executed)}\" не реализован", "Не реализовано",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        //Command AppSettings

        private void CommandAppSettings_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            //TODO Implement CommandAppSettings_CanExecute
            e.CanExecute = true;
        }

        private void CommandAppSettings_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //TODO Implement CommandAppSettings_Executed
            MessageBox.Show($"Метод \"{nameof(CommandAppSettings_Executed)}\" не реализован", "Не реализовано",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        //Command AppAbout

        private void CommandAppAbout_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            //TODO Implement CommandAppAbout_CanExecute
            e.CanExecute = true;
        }

        private void CommandAppAbout_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //TODO Implement CommandAppAbout_Executed
            MessageBox.Show($"Метод \"{nameof(CommandAppAbout_Executed)}\" не реализован", "Не реализовано",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        //Command TabSwitchToNext

        private void CommandTabSwitchToNext_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        { 
            e.CanExecute = ctrl_Files.Items.Count > 0;
        }

        private void CommandTabSwitchToNext_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Int32 newSelectedTabIndex = ctrl_Files.SelectedIndex + 1;
            ctrl_Files.SelectedIndex = newSelectedTabIndex < ctrl_Files.Items.Count ? newSelectedTabIndex : 0;
        }

        //Command TabSwitchToPrev

        private void CommandTabSwitchToPrev_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ctrl_Files.Items.Count > 0;
        }

        private void CommandTabSwitchToPrev_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Int32 newSelectedTabIndex = ctrl_Files.SelectedIndex - 1;
            ctrl_Files.SelectedIndex = newSelectedTabIndex >= 0 ? newSelectedTabIndex : ctrl_Files.Items.Count - 1;
        }
    }

}