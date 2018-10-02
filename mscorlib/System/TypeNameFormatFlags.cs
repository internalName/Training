// Decompiled with JetBrains decompiler
// Type: System.TypeNameFormatFlags
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System
{
  internal enum TypeNameFormatFlags
  {
    FormatBasic = 0,
    FormatNamespace = 1,
    FormatFullInst = 2,
    FormatAssembly = 4,
    FormatSignature = 8,
    FormatNoVersion = 16, // 0x00000010
    FormatAngleBrackets = 64, // 0x00000040
    FormatStubInfo = 128, // 0x00000080
    FormatGenericParam = 256, // 0x00000100
    FormatSerialization = 259, // 0x00000103
  }
}
