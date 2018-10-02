// Decompiled with JetBrains decompiler
// Type: System.IO.FileAttributes
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.IO
{
  /// <summary>Предоставляет атрибуты для файлов и каталогов.</summary>
  [Flags]
  [ComVisible(true)]
  [Serializable]
  public enum FileAttributes
  {
    ReadOnly = 1,
    Hidden = 2,
    System = 4,
    Directory = 16, // 0x00000010
    Archive = 32, // 0x00000020
    Device = 64, // 0x00000040
    Normal = 128, // 0x00000080
    Temporary = 256, // 0x00000100
    SparseFile = 512, // 0x00000200
    ReparsePoint = 1024, // 0x00000400
    Compressed = 2048, // 0x00000800
    Offline = 4096, // 0x00001000
    NotContentIndexed = 8192, // 0x00002000
    Encrypted = 16384, // 0x00004000
    [ComVisible(false)] IntegrityStream = 32768, // 0x00008000
    [ComVisible(false)] NoScrubData = 131072, // 0x00020000
  }
}
