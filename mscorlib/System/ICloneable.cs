// Decompiled with JetBrains decompiler
// Type: System.ICloneable
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System
{
  /// <summary>
  ///   Поддерживает копирование, который создает новый экземпляр класса с тем же значением как существующий экземпляр.
  /// </summary>
  [ComVisible(true)]
  public interface ICloneable
  {
    /// <summary>
    ///   Создает новый объект, являющийся копией текущего экземпляра.
    /// </summary>
    /// <returns>Новый объект, являющийся копией этого экземпляра.</returns>
    object Clone();
  }
}
