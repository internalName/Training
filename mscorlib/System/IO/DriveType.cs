// Decompiled with JetBrains decompiler
// Type: System.IO.DriveType
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.IO
{
  /// <summary>
  ///   Определяет константы для типов дисков, включая CDRom, Fixed, сети, NoRootDirectory, ОЗУ, съемных и Unknown.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public enum DriveType
  {
    Unknown,
    NoRootDirectory,
    Removable,
    Fixed,
    Network,
    CDRom,
    Ram,
  }
}
