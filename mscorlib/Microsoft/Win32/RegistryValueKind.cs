// Decompiled with JetBrains decompiler
// Type: Microsoft.Win32.RegistryValueKind
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace Microsoft.Win32
{
  /// <summary>
  ///   Указывает типы данных, используемые для хранения значений в реестре, либо определяет тип данных значения в реестре.
  /// </summary>
  [ComVisible(true)]
  public enum RegistryValueKind
  {
    [ComVisible(false)] None = -1,
    Unknown = 0,
    String = 1,
    ExpandString = 2,
    Binary = 3,
    DWord = 4,
    MultiString = 7,
    QWord = 11, // 0x0000000B
  }
}
