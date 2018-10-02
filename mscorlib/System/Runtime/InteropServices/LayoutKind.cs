// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.LayoutKind
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Управляет макетом объекта при его экспорте в неуправляемый код.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public enum LayoutKind
  {
    [__DynamicallyInvokable] Sequential = 0,
    [__DynamicallyInvokable] Explicit = 2,
    [__DynamicallyInvokable] Auto = 3,
  }
}
