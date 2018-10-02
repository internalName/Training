// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.WindowsRuntime.IDisposableToIClosableAdapter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
  internal sealed class IDisposableToIClosableAdapter
  {
    private IDisposableToIClosableAdapter()
    {
    }

    [SecurityCritical]
    public void Close()
    {
      JitHelpers.UnsafeCast<IDisposable>((object) this).Dispose();
    }
  }
}
