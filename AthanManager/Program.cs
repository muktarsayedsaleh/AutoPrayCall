using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AthanManager
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //myVars.myMainForm = new Form1();
            //Application.Run(myVars.myMainForm);
            Application.Run(new Form2());
        }

        private static void OnExit(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
