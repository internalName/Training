// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.WindowsRuntime.IIterator`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices.WindowsRuntime
{
  [Guid("6a79e863-4300-459a-9966-cbb660963ee1")]
  [ComImport]
  internal interface IIterator<T>
  {
    T Current { get; }

    bool HasCurrent { get; }

    bool MoveNext();

    int GetMany([Out] T[] items);
  }
}
