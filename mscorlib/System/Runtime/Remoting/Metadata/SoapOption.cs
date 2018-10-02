// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Metadata.SoapOption
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata
{
  /// <summary>
  ///   Задает параметры конфигурации SOAP для использования с <see cref="T:System.Runtime.Remoting.Metadata.SoapTypeAttribute" /> класса.
  /// </summary>
  [Flags]
  [ComVisible(true)]
  [Serializable]
  public enum SoapOption
  {
    None = 0,
    AlwaysIncludeTypes = 1,
    XsdString = 2,
    EmbedAll = 4,
    Option1 = 8,
    Option2 = 16, // 0x00000010
  }
}
