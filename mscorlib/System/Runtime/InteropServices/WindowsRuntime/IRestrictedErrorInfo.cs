// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.WindowsRuntime.IRestrictedErrorInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices.WindowsRuntime
{
  [Guid("82BA7092-4C88-427D-A7BC-16DD93FEB67E")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  internal interface IRestrictedErrorInfo
  {
    void GetErrorDetails([MarshalAs(UnmanagedType.BStr)] out string description, out int error, [MarshalAs(UnmanagedType.BStr)] out string restrictedDescription, [MarshalAs(UnmanagedType.BStr)] out string capabilitySid);

    void GetReference([MarshalAs(UnmanagedType.BStr)] out string reference);
  }
}
