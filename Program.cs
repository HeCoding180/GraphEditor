using System.Diagnostics;
using System.Net;

namespace GraphEditor
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Form1 form1 = new Form1();
            form1.hookId = form1.SetHook(form1.HookCallback);
            Application.Run(form1);
            Form1.UnhookWindowsHookEx(form1.hookId);
        }
    }
}