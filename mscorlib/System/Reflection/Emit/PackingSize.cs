// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.PackingSize
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
  /// <summary>
  ///   Задает один из двух факторов, определяющих выравнивание памяти полей при маршалинге типа.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public enum PackingSize
  {
    [__DynamicallyInvokable] Unspecified = 0,
    [__DynamicallyInvokable] Size1 = 1,
    [__DynamicallyInvokable] Size2 = 2,
    [__DynamicallyInvokable] Size4 = 4,
    [__DynamicallyInvokable] Size8 = 8,
    [__DynamicallyInvokable] Size16 = 16, // 0x00000010
    [__DynamicallyInvokable] Size32 = 32, // 0x00000020
    [__DynamicallyInvokable] Size64 = 64, // 0x00000040
    [__DynamicallyInvokable] Size128 = 128, // 0x00000080
  }
}
