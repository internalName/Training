// Decompiled with JetBrains decompiler
// Type: System.Globalization.GregorianCalendarTypes
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Globalization
{
  /// <summary>
  ///   Определяет разные языковые версии григорианского календаря.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public enum GregorianCalendarTypes
  {
    Localized = 1,
    USEnglish = 2,
    MiddleEastFrench = 9,
    Arabic = 10, // 0x0000000A
    TransliteratedEnglish = 11, // 0x0000000B
    TransliteratedFrench = 12, // 0x0000000C
  }
}
