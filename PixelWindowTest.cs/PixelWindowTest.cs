using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PixelWindowTest {
    static class PixelWindowTest {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            Program test = new Program();


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }

    public class Program {
        // what happens when

    }
}
