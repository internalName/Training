// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.WindowsRuntime.IVectorView`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Collections.Generic;

namespace System.Runtime.InteropServices.WindowsRuntime
{
  [Guid("bbe1fa4c-b0e3-4583-baef-1f1b2e483e56")]
  [ComImport]
  internal interface IVectorView<T> : IIterable<T>, IEnumerable<T>, IEnumerable
  {
    T GetAt(uint index);

    uint Size { get; }

    bool IndexOf(T value, out uint index);

    uint GetMany(uint startIndex, [Out] T[] items);
  }
}
