// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.Formatters.TypeFilterLevel
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Runtime.Serialization.Formatters
{
  /// <summary>
  ///   Задает уровень автоматической десериализации для удаленного взаимодействия .NET Framework.
  /// </summary>
  [ComVisible(true)]
  public enum TypeFilterLevel
  {
    Low = 2,
    Full = 3,
  }
}
