namespace MyEnterpriseWinFormsApp
{
    using System;
    using System.Windows.Forms;

    using MyEnterpriseWinFormsApp.Forms;

    /// <summary>
    /// Defines the wrapper for the application logic.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Defines the main entry point for the forms application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}