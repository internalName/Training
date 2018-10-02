// Decompiled with JetBrains decompiler
// Type: System.IO.FileOptions
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.IO
{
  /// <summary>
  ///   Представляет Дополнительные параметры для создания <see cref="T:System.IO.FileStream" /> объекта.
  /// </summary>
  [Flags]
  [ComVisible(true)]
  [Serializable]
  public enum FileOptions
  {
    None = 0,
    WriteThrough = -2147483648, // -0x80000000
    Asynchronous = 1073741824, // 0x40000000
    RandomAccess = 268435456, // 0x10000000
    DeleteOnClose = 67108864, // 0x04000000
    SequentialScan = 134217728, // 0x08000000
    Encrypted = 16384, // 0x00004000
  }
}
