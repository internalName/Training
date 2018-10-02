// Decompiled with JetBrains decompiler
// Type: System.Reflection.MethodImplAttributes
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>Задает флаги для атрибутов реализации метода.</summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public enum MethodImplAttributes
  {
    [__DynamicallyInvokable] IL = 0,
    [__DynamicallyInvokable] Managed = 0,
    [__DynamicallyInvokable] Native = 1,
    [__DynamicallyInvokable] OPTIL = 2,
    [__DynamicallyInvokable] CodeTypeMask = 3,
    [__DynamicallyInvokable] Runtime = 3,
    [__DynamicallyInvokable] ManagedMask = 4,
    [__DynamicallyInvokable] Unmanaged = 4,
    [__DynamicallyInvokable] NoInlining = 8,
    [__DynamicallyInvokable] ForwardRef = 16, // 0x00000010
    [__DynamicallyInvokable] Synchronized = 32, // 0x00000020
    [__DynamicallyInvokable] NoOptimization = 64, // 0x00000040
    [__DynamicallyInvokable] PreserveSig = 128, // 0x00000080
    [ComVisible(false), __DynamicallyInvokable] AggressiveInlining = 256, // 0x00000100
    [__DynamicallyInvokable] InternalCall = 4096, // 0x00001000
    MaxMethodImplVal = 65535, // 0x0000FFFF
  }
}
