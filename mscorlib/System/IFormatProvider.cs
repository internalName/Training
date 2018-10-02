// Decompiled with JetBrains decompiler
// Type: System.IFormatProvider
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System
{
  /// <summary>
  ///   Предоставляет механизм для извлечения объекта с целью управления форматированием.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public interface IFormatProvider
  {
    /// <summary>
    ///   Возвращает объект, предоставляющий службы форматирования для заданного типа.
    /// </summary>
    /// <param name="formatType">
    ///   Объект, указывающий тип возвращаемого объекта форматирования.
    /// </param>
    /// <returns>
    ///   Экземпляр объекта, заданного параметром <paramref name="formatType" />, если <see cref="T:System.IFormatProvider" /> реализация может предоставить этому типу объекта; в противном случае <see langword="null" />.
    /// </returns>
    [__DynamicallyInvokable]
    object GetFormat(Type formatType);
  }
}
