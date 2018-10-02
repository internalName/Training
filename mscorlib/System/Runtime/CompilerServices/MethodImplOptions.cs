// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.MethodImplOptions
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
  /// <summary>Сообщает подробные сведения о реализации метода.</summary>
  [Flags]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public enum MethodImplOptions
  {
    Unmanaged = 4,
    ForwardRef = 16, // 0x00000010
    [__DynamicallyInvokable] PreserveSig = 128, // 0x00000080
    InternalCall = 4096, // 0x00001000
    Synchronized = 32, // 0x00000020
    [__DynamicallyInvokable] NoInlining = 8,
    [ComVisible(false), __DynamicallyInvokable] AggressiveInlining = 256, // 0x00000100
    [__DynamicallyInvokable] NoOptimization = 64, // 0x00000040
  }
}
