using System.Diagnostics;

namespace ZihoSettings
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            if (args.Length != 0)
            {
                ApplicationConfiguration.Initialize();
                InvisibleForm iform = new InvisibleForm();

                Task task = Task.Run(() => { Application.Run(iform); });

                Thread.Sleep(5*1000);
                Application.Exit();
            }
            else
            {
                ApplicationConfiguration.Initialize();
                Form1 form1 = new Form1();
                Application.Run(form1);
            }
        }
    }
}