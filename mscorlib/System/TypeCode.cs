// Decompiled with JetBrains decompiler
// Type: System.TypeCode
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System
{
  /// <summary>Задает тип объекта.</summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public enum TypeCode
  {
    [__DynamicallyInvokable] Empty = 0,
    [__DynamicallyInvokable] Object = 1,
    DBNull = 2,
    [__DynamicallyInvokable] Boolean = 3,
    [__DynamicallyInvokable] Char = 4,
    [__DynamicallyInvokable] SByte = 5,
    [__DynamicallyInvokable] Byte = 6,
    [__DynamicallyInvokable] Int16 = 7,
    [__DynamicallyInvokable] UInt16 = 8,
    [__DynamicallyInvokable] Int32 = 9,
    [__DynamicallyInvokable] UInt32 = 10, // 0x0000000A
    [__DynamicallyInvokable] Int64 = 11, // 0x0000000B
    [__DynamicallyInvokable] UInt64 = 12, // 0x0000000C
    [__DynamicallyInvokable] Single = 13, // 0x0000000D
    [__DynamicallyInvokable] Double = 14, // 0x0000000E
    [__DynamicallyInvokable] Decimal = 15, // 0x0000000F
    [__DynamicallyInvokable] DateTime = 16, // 0x00000010
    [__DynamicallyInvokable] String = 18, // 0x00000012
  }
}
