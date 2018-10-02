// Decompiled with JetBrains decompiler
// Type: System.Reflection.PropertyAttributes
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>
  ///   Определяет атрибуты, которые могут быть связаны со свойством.
  ///    Значения этих атрибутов определены в файле corhdr.h.
  /// </summary>
  [Flags]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public enum PropertyAttributes
  {
    [__DynamicallyInvokable] None = 0,
    [__DynamicallyInvokable] SpecialName = 512, // 0x00000200
    ReservedMask = 62464, // 0x0000F400
    [__DynamicallyInvokable] RTSpecialName = 1024, // 0x00000400
    [__DynamicallyInvokable] HasDefault = 4096, // 0x00001000
    Reserved2 = 8192, // 0x00002000
    Reserved3 = 16384, // 0x00004000
    Reserved4 = 32768, // 0x00008000
  }
}
