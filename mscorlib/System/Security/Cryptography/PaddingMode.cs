// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.PaddingMode
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>
  ///   Задает тип заполнения, используемого, когда блок данных сообщения короче полного числа байтов, необходимого для криптографической операции.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public enum PaddingMode
  {
    None = 1,
    PKCS7 = 2,
    Zeros = 3,
    ANSIX923 = 4,
    ISO10126 = 5,
  }
}
