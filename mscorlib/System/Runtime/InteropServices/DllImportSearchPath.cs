// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.DllImportSearchPath
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Указывает пути, используемые для поиска DLL, предоставляющих вызываемые для платформы функции.
  /// </summary>
  [Flags]
  [__DynamicallyInvokable]
  public enum DllImportSearchPath
  {
    [__DynamicallyInvokable] UseDllDirectoryForDependencies = 256, // 0x00000100
    [__DynamicallyInvokable] ApplicationDirectory = 512, // 0x00000200
    [__DynamicallyInvokable] UserDirectories = 1024, // 0x00000400
    [__DynamicallyInvokable] System32 = 2048, // 0x00000800
    [__DynamicallyInvokable] SafeDirectories = 4096, // 0x00001000
    [__DynamicallyInvokable] AssemblyDirectory = 2,
    [__DynamicallyInvokable] LegacyBehavior = 0,
  }
}
