using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.IO;

namespace WPFGui
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            File.AppendAllText("Error.log", e.Exception.ToString() + "\n------------------------\n");
            MessageBox.Show("Error details saved to error.log", "Unhandled exception", MessageBoxButton.OK, MessageBoxImage.Error);
            e.Dispatcher.InvokeShutdown();
        }
    }
}
