// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComTypes.SYSKIND
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices.ComTypes
{
  /// <summary>Указывает целевую платформу операционной системы.</summary>
  [__DynamicallyInvokable]
  [Serializable]
  public enum SYSKIND
  {
    [__DynamicallyInvokable] SYS_WIN16,
    [__DynamicallyInvokable] SYS_WIN32,
    [__DynamicallyInvokable] SYS_MAC,
    [__DynamicallyInvokable] SYS_WIN64,
  }
}
