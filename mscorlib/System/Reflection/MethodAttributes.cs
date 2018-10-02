// Decompiled with JetBrains decompiler
// Type: System.Reflection.MethodAttributes
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>
  ///   Задает флаги для атрибутов метода.
  ///    Эти флаги определяются в файле corhdr.h.
  /// </summary>
  [Flags]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public enum MethodAttributes
  {
    [__DynamicallyInvokable] MemberAccessMask = 7,
    [__DynamicallyInvokable] PrivateScope = 0,
    [__DynamicallyInvokable] Private = 1,
    [__DynamicallyInvokable] FamANDAssem = 2,
    [__DynamicallyInvokable] Assembly = FamANDAssem | Private, // 0x00000003
    [__DynamicallyInvokable] Family = 4,
    [__DynamicallyInvokable] FamORAssem = Family | Private, // 0x00000005
    [__DynamicallyInvokable] Public = Family | FamANDAssem, // 0x00000006
    [__DynamicallyInvokable] Static = 16, // 0x00000010
    [__DynamicallyInvokable] Final = 32, // 0x00000020
    [__DynamicallyInvokable] Virtual = 64, // 0x00000040
    [__DynamicallyInvokable] HideBySig = 128, // 0x00000080
    [__DynamicallyInvokable] CheckAccessOnOverride = 512, // 0x00000200
    [__DynamicallyInvokable] VtableLayoutMask = 256, // 0x00000100
    [__DynamicallyInvokable] ReuseSlot = 0,
    [__DynamicallyInvokable] NewSlot = VtableLayoutMask, // 0x00000100
    [__DynamicallyInvokable] Abstract = 1024, // 0x00000400
    [__DynamicallyInvokable] SpecialName = 2048, // 0x00000800
    [__DynamicallyInvokable] PinvokeImpl = 8192, // 0x00002000
    [__DynamicallyInvokable] UnmanagedExport = 8,
    [__DynamicallyInvokable] RTSpecialName = 4096, // 0x00001000
    ReservedMask = 53248, // 0x0000D000
    [__DynamicallyInvokable] HasSecurity = 16384, // 0x00004000
    [__DynamicallyInvokable] RequireSecObject = 32768, // 0x00008000
  }
}
