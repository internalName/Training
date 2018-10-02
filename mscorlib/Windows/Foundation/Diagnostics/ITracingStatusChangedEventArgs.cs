// Decompiled with JetBrains decompiler
// Type: Windows.Foundation.Diagnostics.ITracingStatusChangedEventArgs
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace Windows.Foundation.Diagnostics
{
  [Guid("410B7711-FF3B-477F-9C9A-D2EFDA302DC3")]
  [ComImport]
  internal interface ITracingStatusChangedEventArgs
  {
    bool Enabled { get; }

    CausalityTraceLevel TraceLevel { get; }
  }
}
