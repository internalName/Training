// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.Formatters.FormatterAssemblyStyle
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Runtime.Serialization.Formatters
{
  /// <summary>
  ///   Указывает метод, который будет использоваться во время десериализации для расположения и загрузки сборок.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public enum FormatterAssemblyStyle
  {
    Simple,
    Full,
  }
}
