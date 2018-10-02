// Decompiled with JetBrains decompiler
// Type: System.MidpointRounding
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System
{
  /// <summary>
  ///   Задает способ обработки чисел, которые равноудалены от двух соседних чисел, в математических методах округления.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public enum MidpointRounding
  {
    [__DynamicallyInvokable] ToEven,
    [__DynamicallyInvokable] AwayFromZero,
  }
}
