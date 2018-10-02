// Decompiled with JetBrains decompiler
// Type: System.AttributeTargets
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System
{
  /// <summary>
  ///   Задает элементы приложения, в которых допустимо применять аргумент.
  /// </summary>
  [Flags]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public enum AttributeTargets
  {
    [__DynamicallyInvokable] Assembly = 1,
    [__DynamicallyInvokable] Module = 2,
    [__DynamicallyInvokable] Class = 4,
    [__DynamicallyInvokable] Struct = 8,
    [__DynamicallyInvokable] Enum = 16, // 0x00000010
    [__DynamicallyInvokable] Constructor = 32, // 0x00000020
    [__DynamicallyInvokable] Method = 64, // 0x00000040
    [__DynamicallyInvokable] Property = 128, // 0x00000080
    [__DynamicallyInvokable] Field = 256, // 0x00000100
    [__DynamicallyInvokable] Event = 512, // 0x00000200
    [__DynamicallyInvokable] Interface = 1024, // 0x00000400
    [__DynamicallyInvokable] Parameter = 2048, // 0x00000800
    [__DynamicallyInvokable] Delegate = 4096, // 0x00001000
    [__DynamicallyInvokable] ReturnValue = 8192, // 0x00002000
    [__DynamicallyInvokable] GenericParameter = 16384, // 0x00004000
    [__DynamicallyInvokable] All = GenericParameter | ReturnValue | Delegate | Parameter | Interface | Event | Field | Property | Method | Constructor | Enum | Struct | Class | Module | Assembly, // 0x00007FFF
  }
}
