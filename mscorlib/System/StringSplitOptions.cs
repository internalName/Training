// Decompiled with JetBrains decompiler
// Type: System.StringSplitOptions
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System
{
  /// <summary>
  ///   Указывает ли применимо <see cref="Overload:System.String.Split" /> перегрузки метода, включают или опускают пустые подстроки возвращаемого значения.
  /// </summary>
  [ComVisible(false)]
  [Flags]
  [__DynamicallyInvokable]
  public enum StringSplitOptions
  {
    [__DynamicallyInvokable] None = 0,
    [__DynamicallyInvokable] RemoveEmptyEntries = 1,
  }
}
