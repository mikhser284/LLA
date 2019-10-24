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
using System.IO.Compression;

namespace LLA.GUI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            CommandBindings.Add(new CommandBinding(Commands_Application.AppClose, CommandBinding_CloseApp));            
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Properties, CommandBinding_ShowDialog_Settings));            
        }
        
        private void CommandBinding_ShowDialog_Settings(object sender, ExecutedRoutedEventArgs e)
        {
            SettingsDialog settingsDialog = new SettingsDialog();
            if (settingsDialog.ShowDialog() == true)
            {

            }
        }

        private void CommandBinding_CloseApp(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
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
