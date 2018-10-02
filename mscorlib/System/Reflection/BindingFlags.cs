// Decompiled with JetBrains decompiler
// Type: System.Reflection.BindingFlags
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>
  ///   Указывает флаги, управляющие привязкой и способом, используемым отражением при поиске членов и типов.
  /// </summary>
  [Flags]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public enum BindingFlags
  {
    Default = 0,
    [__DynamicallyInvokable] IgnoreCase = 1,
    [__DynamicallyInvokable] DeclaredOnly = 2,
    [__DynamicallyInvokable] Instance = 4,
    [__DynamicallyInvokable] Static = 8,
    [__DynamicallyInvokable] Public = 16, // 0x00000010
    [__DynamicallyInvokable] NonPublic = 32, // 0x00000020
    [__DynamicallyInvokable] FlattenHierarchy = 64, // 0x00000040
    InvokeMethod = 256, // 0x00000100
    CreateInstance = 512, // 0x00000200
    GetField = 1024, // 0x00000400
    SetField = 2048, // 0x00000800
    GetProperty = 4096, // 0x00001000
    SetProperty = 8192, // 0x00002000
    PutDispProperty = 16384, // 0x00004000
    PutRefDispProperty = 32768, // 0x00008000
    [__DynamicallyInvokable] ExactBinding = 65536, // 0x00010000
    SuppressChangeType = 131072, // 0x00020000
    [__DynamicallyInvokable] OptionalParamBinding = 262144, // 0x00040000
    IgnoreReturn = 16777216, // 0x01000000
  }
}
