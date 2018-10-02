// Decompiled with JetBrains decompiler
// Type: System.Reflection.ParameterAttributes
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>
  ///   Определяет атрибуты, которые могут быть связаны с параметром.
  ///    Они определяются в файле CorHdr.h.
  /// </summary>
  [Flags]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public enum ParameterAttributes
  {
    [__DynamicallyInvokable] None = 0,
    [__DynamicallyInvokable] In = 1,
    [__DynamicallyInvokable] Out = 2,
    [__DynamicallyInvokable] Lcid = 4,
    [__DynamicallyInvokable] Retval = 8,
    [__DynamicallyInvokable] Optional = 16, // 0x00000010
    ReservedMask = 61440, // 0x0000F000
    [__DynamicallyInvokable] HasDefault = 4096, // 0x00001000
    [__DynamicallyInvokable] HasFieldMarshal = 8192, // 0x00002000
    Reserved3 = 16384, // 0x00004000
    Reserved4 = 32768, // 0x00008000
  }
}
