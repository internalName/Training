// Decompiled with JetBrains decompiler
// Type: System.Resources.UltimateResourceFallbackLocation
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Resources
{
  /// <summary>
  ///   Указывает, является ли <see cref="T:System.Resources.ResourceManager" /> объекта ищет ресурсы приложения по умолчанию языка и региональных параметров в основную сборку или во вспомогательную сборку.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public enum UltimateResourceFallbackLocation
  {
    MainAssembly,
    Satellite,
  }
}
