using System;
using System.Runtime.InteropServices;

namespace Merrigan0 {
    public static class Shell32 {
        [DllImport("shell32.dll")]
        public static extern int ShellAbout(
            IntPtr hwnd,
            string applicationName,
            string description,
            IntPtr hicon);
    }
}
