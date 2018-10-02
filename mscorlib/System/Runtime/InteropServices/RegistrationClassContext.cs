// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.RegistrationClassContext
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Определяет набор контекстов выполнения, в которых объект класса будут доступны для запросов, создающих экземпляры.
  /// </summary>
  [Flags]
  public enum RegistrationClassContext
  {
    InProcessServer = 1,
    InProcessHandler = 2,
    LocalServer = 4,
    InProcessServer16 = 8,
    RemoteServer = 16, // 0x00000010
    InProcessHandler16 = 32, // 0x00000020
    Reserved1 = 64, // 0x00000040
    Reserved2 = 128, // 0x00000080
    Reserved3 = 256, // 0x00000100
    Reserved4 = 512, // 0x00000200
    NoCodeDownload = 1024, // 0x00000400
    Reserved5 = 2048, // 0x00000800
    NoCustomMarshal = 4096, // 0x00001000
    EnableCodeDownload = 8192, // 0x00002000
    NoFailureLog = 16384, // 0x00004000
    DisableActivateAsActivator = 32768, // 0x00008000
    EnableActivateAsActivator = 65536, // 0x00010000
    FromDefaultContext = 131072, // 0x00020000
  }
}
