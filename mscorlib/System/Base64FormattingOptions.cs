// Decompiled with JetBrains decompiler
// Type: System.Base64FormattingOptions
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System
{
  /// <summary>
  ///   Указывает, вставляют ли соответствующие методы <see cref="Overload:System.Convert.ToBase64CharArray" /> и <see cref="Overload:System.Convert.ToBase64String" /> разрыва строк в свои выходные данные.
  /// </summary>
  [Flags]
  public enum Base64FormattingOptions
  {
    None = 0,
    InsertLineBreaks = 1,
  }
}
