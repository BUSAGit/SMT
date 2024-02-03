using System;

namespace EVEData.Utils.WindowsManager
{
    public interface IProcessInfo
    {
        IntPtr Handle { get; }
        string Title { get; }
    }
}
