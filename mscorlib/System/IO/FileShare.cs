// Decompiled with JetBrains decompiler
// Type: System.IO.FileShare
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.IO
{
  /// <summary>
  ///   Содержит константы для управления типом доступа других <see cref="T:System.IO.FileStream" /> объекты могут иметь в тот же файл.
  /// </summary>
  [Flags]
  [ComVisible(true)]
  [Serializable]
  public enum FileShare
  {
    None = 0,
    Read = 1,
    Write = 2,
    ReadWrite = Write | Read, // 0x00000003
    Delete = 4,
    Inheritable = 16, // 0x00000010
  }
}
