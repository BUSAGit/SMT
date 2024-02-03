using System;

// By Eve-O-Preview

namespace EVEData.Utils.WindowsManager
{
    sealed class ProcessInfo : IProcessInfo
        {
            public ProcessInfo(IntPtr handle, string title)
            {
                this.Handle = handle;
                this.Title = title;
            }

            public IntPtr Handle { get; }
            public string Title { get; }
        }
}
