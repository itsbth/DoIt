using System;
using System.Collections.Generic;
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
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Interop;

using DoItLib;
using SearchableCatalog;
using System.Windows.Markup;
using System.IO;
using Microsoft.Win32;

namespace WPFGui
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DoItManager _manager;
        private HotKey _hk;

        public static RoutedCommand RebuildCatalogCommand = new RoutedCommand();
        public static RoutedCommand ChangeStyleCommand = new RoutedCommand();

        public MainWindow()
        {
            LoadStyle("Style.xaml");
            InitializeComponent();
            _manager = new DoItManager();
            DataContext = new DoItDataProvider(_manager);
            ((DoItDataProvider)DataContext).PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(MainWindow_PropertyChanged);
        }

        private void LoadStyle(Stream fs)
        {
            ResourceDictionary dict = (ResourceDictionary)XamlReader.Load(fs);
            Resources.MergedDictionaries.Add(dict);
        }

        private void LoadStyle(string fname)
        {
            using (FileStream fs = new FileStream(fname, FileMode.Open))
            {
                LoadStyle(fs);
            }
        }

        void MainWindow_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SearchString")
            {
                HitList.SelectedIndex = 0;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_hk != null) return;
            HwndSource source = HwndSource.FromHwnd(new WindowInteropHelper(this).Handle);
            source.AddHook(new HwndSourceHook(WndProc));
            _hk = new HotKey(new WindowInteropHelper(this).Handle);
            _hk.RegisterGlobalHotKey(Key.Space, HotKey.MOD_CONTROL);
        }

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == HotKey.WM_HOTKEY)
            {
                Show();
                Activate();
                Search.Focus();
                Search.SelectAll();
                handled = true;
            }
            return IntPtr.Zero;
        }

        private void SearchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up:
                    Popup.IsOpen = true;
                    if (HitList.SelectedIndex > 0) HitList.SelectedIndex -= 1;
                    else Popup.IsOpen = false;
                    break;
                case Key.Down:
                    Popup.IsOpen = true;
                    HitList.SelectedIndex += 1;
                    break;
                case Key.Return:
                    if (((DoItDataProvider)DataContext).SearchResults == null) break; 
                    CatalogItem item = (CatalogItem)HitList.SelectedItem;
                    if (item == null) item = ((DoItDataProvider)DataContext).SearchResults[0];
                    Hide();
                    Popup.IsOpen = false;
                    Process p = new Process();
                    p.StartInfo.FileName = item.Path;
                    p.Start();
                    item.IncreaseCount(Search.Text);
                    break;
                case Key.Escape:
                    Hide();
                    Popup.IsOpen = false;
                    break;
                default:
                    break;
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            _manager.Save();
        }

        private void CloseCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }

        private void CloseCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        private void RebuildCatalogCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }

        private void RebuildCatalogCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _manager.RebuildCatalog();
        }

        private void ChangeStyleCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }

        private void ChangeStyleCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "XAML Styles (*.xaml)|*.xaml";
            dlg.InitialDirectory = Environment.CurrentDirectory;
            if (dlg.ShowDialog().Value)
            {
                using (Stream fs = dlg.OpenFile())
                {
                    LoadStyle(fs);
                }
            }
        }
    }

    class FileImageExtractor : IValueConverter
    {

        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string path = (string)value;
            System.Drawing.Icon icon = System.Drawing.Icon.ExtractAssociatedIcon(path);
                //IconReader.GetFileIcon(path, IconReader.IconSize.Large, false);
            System.Drawing.Bitmap bitmap = icon.ToBitmap();
            return System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(bitmap.GetHbitmap(),
                IntPtr.Zero, System.Windows.Int32Rect.Empty,
                System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
    
    
    class StringRightConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return "..." + ((string)value).Substring(((string)value).Length - int.Parse((string)parameter));
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

}
