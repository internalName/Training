// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.CharSet
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Определяет, какой набор символов должны использовать упакованные строки.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public enum CharSet
  {
    None = 1,
    [__DynamicallyInvokable] Ansi = 2,
    [__DynamicallyInvokable] Unicode = 3,
    Auto = 4,
  }
}
