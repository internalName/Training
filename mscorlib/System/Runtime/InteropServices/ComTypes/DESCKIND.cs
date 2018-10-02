// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComTypes.DESCKIND
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices.ComTypes
{
  /// <summary>
  ///   Определяет, к описанию какого типа выполняется привязка.
  /// </summary>
  [__DynamicallyInvokable]
  [Serializable]
  public enum DESCKIND
  {
    [__DynamicallyInvokable] DESCKIND_NONE,
    [__DynamicallyInvokable] DESCKIND_FUNCDESC,
    [__DynamicallyInvokable] DESCKIND_VARDESC,
    [__DynamicallyInvokable] DESCKIND_TYPECOMP,
    [__DynamicallyInvokable] DESCKIND_IMPLICITAPPOBJ,
    [__DynamicallyInvokable] DESCKIND_MAX,
  }
}
