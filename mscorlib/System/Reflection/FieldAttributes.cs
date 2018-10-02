// Decompiled with JetBrains decompiler
// Type: System.Reflection.FieldAttributes
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>Задает флаги, описывающие атрибуты поля.</summary>
  [Flags]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public enum FieldAttributes
  {
    [__DynamicallyInvokable] FieldAccessMask = 7,
    [__DynamicallyInvokable] PrivateScope = 0,
    [__DynamicallyInvokable] Private = 1,
    [__DynamicallyInvokable] FamANDAssem = 2,
    [__DynamicallyInvokable] Assembly = FamANDAssem | Private, // 0x00000003
    [__DynamicallyInvokable] Family = 4,
    [__DynamicallyInvokable] FamORAssem = Family | Private, // 0x00000005
    [__DynamicallyInvokable] Public = Family | FamANDAssem, // 0x00000006
    [__DynamicallyInvokable] Static = 16, // 0x00000010
    [__DynamicallyInvokable] InitOnly = 32, // 0x00000020
    [__DynamicallyInvokable] Literal = 64, // 0x00000040
    [__DynamicallyInvokable] NotSerialized = 128, // 0x00000080
    [__DynamicallyInvokable] SpecialName = 512, // 0x00000200
    [__DynamicallyInvokable] PinvokeImpl = 8192, // 0x00002000
    ReservedMask = 38144, // 0x00009500
    [__DynamicallyInvokable] RTSpecialName = 1024, // 0x00000400
    [__DynamicallyInvokable] HasFieldMarshal = 4096, // 0x00001000
    [__DynamicallyInvokable] HasDefault = 32768, // 0x00008000
    [__DynamicallyInvokable] HasFieldRVA = 256, // 0x00000100
  }
}
