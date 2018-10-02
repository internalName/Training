// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComTypes.FUNCKIND
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices.ComTypes
{
  /// <summary>Определяет способ доступа к функции.</summary>
  [__DynamicallyInvokable]
  [Serializable]
  public enum FUNCKIND
  {
    [__DynamicallyInvokable] FUNC_VIRTUAL,
    [__DynamicallyInvokable] FUNC_PUREVIRTUAL,
    [__DynamicallyInvokable] FUNC_NONVIRTUAL,
    [__DynamicallyInvokable] FUNC_STATIC,
    [__DynamicallyInvokable] FUNC_DISPATCH,
  }
}
