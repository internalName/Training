// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComTypes.DISPPARAMS
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices.ComTypes
{
  /// <summary>
  ///   Содержит аргументы, передаваемые методу или свойству <see langword="IDispatch::Invoke" />.
  /// </summary>
  [__DynamicallyInvokable]
  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
  public struct DISPPARAMS
  {
    /// <summary>Представляет ссылку на массив аргументов.</summary>
    [__DynamicallyInvokable]
    public IntPtr rgvarg;
    /// <summary>
    ///   Представляет диспетчерские идентификаторы именованных аргументов.
    /// </summary>
    [__DynamicallyInvokable]
    public IntPtr rgdispidNamedArgs;
    /// <summary>Представляет число аргументов.</summary>
    [__DynamicallyInvokable]
    public int cArgs;
    /// <summary>Представляет число именованных аргументов</summary>
    [__DynamicallyInvokable]
    public int cNamedArgs;
  }
}
