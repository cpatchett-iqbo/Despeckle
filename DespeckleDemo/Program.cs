namespace IQBackOffice.Despeckle.Demo
{
    #region Usings

    using System;
    using System.Windows.Forms;

    #endregion

    internal static class Program
    {
        #region Private Methods

        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new DemoForm());
        }

        #endregion Private Methods
    }
}