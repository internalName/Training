// Decompiled with JetBrains decompiler
// Type: System.Reflection.TypeAttributes
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>Задает атрибуты типа.</summary>
  [Flags]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [System.Serializable]
  public enum TypeAttributes
  {
    [__DynamicallyInvokable] VisibilityMask = 7,
    [__DynamicallyInvokable] NotPublic = 0,
    [__DynamicallyInvokable] Public = 1,
    [__DynamicallyInvokable] NestedPublic = 2,
    [__DynamicallyInvokable] NestedPrivate = NestedPublic | Public, // 0x00000003
    [__DynamicallyInvokable] NestedFamily = 4,
    [__DynamicallyInvokable] NestedAssembly = NestedFamily | Public, // 0x00000005
    [__DynamicallyInvokable] NestedFamANDAssem = NestedFamily | NestedPublic, // 0x00000006
    [__DynamicallyInvokable] NestedFamORAssem = NestedFamANDAssem | Public, // 0x00000007
    [__DynamicallyInvokable] LayoutMask = 24, // 0x00000018
    [__DynamicallyInvokable] AutoLayout = 0,
    [__DynamicallyInvokable] SequentialLayout = 8,
    [__DynamicallyInvokable] ExplicitLayout = 16, // 0x00000010
    [__DynamicallyInvokable] ClassSemanticsMask = 32, // 0x00000020
    [__DynamicallyInvokable] Class = 0,
    [__DynamicallyInvokable] Interface = ClassSemanticsMask, // 0x00000020
    [__DynamicallyInvokable] Abstract = 128, // 0x00000080
    [__DynamicallyInvokable] Sealed = 256, // 0x00000100
    [__DynamicallyInvokable] SpecialName = 1024, // 0x00000400
    [__DynamicallyInvokable] Import = 4096, // 0x00001000
    [__DynamicallyInvokable] Serializable = 8192, // 0x00002000
    [ComVisible(false), __DynamicallyInvokable] WindowsRuntime = 16384, // 0x00004000
    [__DynamicallyInvokable] StringFormatMask = 196608, // 0x00030000
    [__DynamicallyInvokable] AnsiClass = 0,
    [__DynamicallyInvokable] UnicodeClass = 65536, // 0x00010000
    [__DynamicallyInvokable] AutoClass = 131072, // 0x00020000
    [__DynamicallyInvokable] CustomFormatClass = AutoClass | UnicodeClass, // 0x00030000
    [__DynamicallyInvokable] CustomFormatMask = 12582912, // 0x00C00000
    [__DynamicallyInvokable] BeforeFieldInit = 1048576, // 0x00100000
    ReservedMask = 264192, // 0x00040800
    [__DynamicallyInvokable] RTSpecialName = 2048, // 0x00000800
    [__DynamicallyInvokable] HasSecurity = 262144, // 0x00040000
  }
}
