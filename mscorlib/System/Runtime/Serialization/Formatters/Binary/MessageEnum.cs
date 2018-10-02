// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.Formatters.Binary.MessageEnum
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.Serialization.Formatters.Binary
{
  [Flags]
  [Serializable]
  internal enum MessageEnum
  {
    NoArgs = 1,
    ArgsInline = 2,
    ArgsIsArray = 4,
    ArgsInArray = 8,
    NoContext = 16, // 0x00000010
    ContextInline = 32, // 0x00000020
    ContextInArray = 64, // 0x00000040
    MethodSignatureInArray = 128, // 0x00000080
    PropertyInArray = 256, // 0x00000100
    NoReturnValue = 512, // 0x00000200
    ReturnValueVoid = 1024, // 0x00000400
    ReturnValueInline = 2048, // 0x00000800
    ReturnValueInArray = 4096, // 0x00001000
    ExceptionInArray = 8192, // 0x00002000
    GenericMethod = 32768, // 0x00008000
  }
}
