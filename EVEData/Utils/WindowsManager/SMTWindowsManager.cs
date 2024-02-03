using ESI.NET.Models.Universe;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace EVEData.Utils.WindowsManager
{
    sealed class SMTWindowsManager
    {
        // ==============================================================================================================
        // Variables
        
        /// <summary>
        /// List of all possible eve proccess.
        /// </summary>
        private IList<IProcessInfo> ListOfProcesses;

        /// <summary>
        /// String for Eve Client name.
        /// </summary>
        private string EveExeStringName;

        private int SecondsToUpdate;

        private readonly IDictionary<IntPtr, string> processCache;

        private PeriodicTimer timer;
        /**
         public enum HookType : int
         {
             WH_JOURNALRECORD = 0,
             WH_JOURNALPLAYBACK = 1,
             WH_KEYBOARD = 2,
             WH_GETMESSAGE = 3,
             WH_CALLWNDPROC = 4,
             WH_CBT = 5,
             WH_SYSMSGFILTER = 6,
             WH_MOUSE = 7,
             WH_HARDWARE = 8,
             WH_DEBUG = 9,
             WH_SHELL = 10,
             WH_FOREGROUNDIDLE = 11,
             WH_CALLWNDPROCRET = 12,
             WH_KEYBOARD_LL = 13,
             WH_MOUSE_LL = 14
         }

         public delegate int HookProc(int nCode, IntPtr wParam, IntPtr lParam);

         //[DllImport("user32.dll")]
         //private static extern int CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);
         const int HSHELL_WINDOWCREATED = 1;

         //[DllImport("user32.dll", SetLastError = true)]
         //private static extern IntPtr SetWindowsHookEx(HookType hook, HookProc callback, IntPtr hMod, uint dwThreadId);


         [DllImport("User32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
         public static extern int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, int threadId);

         [DllImport("User32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
         public static extern int CallNextHookEx(int idHook, int nCode, IntPtr wParam, IntPtr lParam);


         HookProc winDelegate;
         static int hHook = 0;

         private static int ShellHookProcDelegate(int code, IntPtr wParam, IntPtr lParam)
         {
             //App specific code here

             return CallNextHookEx(0, code, wParam, lParam);

         
         }
      //winDelegate = new HookProc(ShellHookProcDelegate);
      //hHook = SetWindowsHookEx(10, winDelegate, IntPtr.Zero, 0);
      */
        // ==============================================================================================================
        // Functions

        public SMTWindowsManager()
        {
            ListOfProcesses = new List<IProcessInfo>();
            EveExeStringName = "ExeFile";
            SecondsToUpdate = 30;

            // Create a Background thread to fetch clients every few seconds
            RunTimerInBackground(TimeSpan.FromSeconds(SecondsToUpdate), () => FetchAllRunningEveClients());
        }
        async Task RunTimerInBackground(TimeSpan timeSpan, Action action)
        {
            timer = new PeriodicTimer(timeSpan);
            while (await timer.WaitForNextTickAsync())
            {
                action();
            }
        }

        public void FetchAllRunningEveClients()
        {
            IList<IntPtr> knownProcesses = new List<IntPtr>(processCache.Keys);
            List<IProcessInfo>updatedProcesses = new List<IProcessInfo>(16);
            ListOfProcesses.Clear();

            // Iterate through all the proccess and find ExeFile.exe (eve online)
            foreach (Process process in Process.GetProcesses())
            {
                string processName = process.ProcessName;
                if (!String.Equals(processName, EveExeStringName, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                IntPtr mainWindowHandle = process.MainWindowHandle;
                if (mainWindowHandle == IntPtr.Zero)
                {
                    continue; // No need to monitor non-visual processes
                }

                // Fetch Exe's Windows Name
                string mainWindowTitle = process.MainWindowTitle;

                // We Shave off the "EVE - " so we can do a String.Equal rather than String.Compare, 
                // this, in theory, should solve the duplicate names or names with similar but with "jr" at the end.
                mainWindowTitle = mainWindowTitle.Substring(6);

                //processCache.TryGetValue(mainWindowHandle, out string cachedHandle);
                ListOfProcesses.Add(new ProcessInfo(mainWindowHandle, mainWindowTitle));
            }
        }

        public void ActivateWindow(IntPtr handle)
        {
            User32NativeMethods.SetForegroundWindow(handle);

            int style = User32NativeMethods.GetWindowLong(handle, InteropConstants.GWL_STYLE);

            if ((style & InteropConstants.WS_MINIMIZE) == InteropConstants.WS_MINIMIZE)
            {
                User32NativeMethods.ShowWindowAsync(handle, InteropConstants.SW_RESTORE);
            }
        }

        public void OpenWindow(string charactername)
        {
            // Find the character name through the list of processes
            foreach (ProcessInfo process in ListOfProcesses)
            {
                string processname = process.Title;

                // Open that window with the characters name in the exe.
                // Todo : solve duplicate names with jr. at the end.
                if (processname.Equals(charactername))
                {
                    ActivateWindow(process.Handle);
                }
            }
        }

    }
}
