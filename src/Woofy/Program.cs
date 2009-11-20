using System;
using System.Windows.Forms;
using Microsoft.Win32;
using Woofy.Core;
using Woofy.Gui;
using Woofy.Properties;
using Woofy.Settings;

namespace Woofy
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            UserSettings.Initialize();

            //TODO:conditia e gresita

            

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            Application.Run(new MainForm());
            //Application.Run(new AboutForm());
        }

        /// <summary>
        /// Log exceptions on the additional threads.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Logger.LogException((Exception)e.ExceptionObject);
        }

        /// <summary>
        /// Log exceptions on the main UI thread.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            Logger.LogException(e.Exception);
        }
    }
}