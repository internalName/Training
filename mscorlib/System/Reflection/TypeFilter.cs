// Decompiled with JetBrains decompiler
// Type: System.Reflection.TypeFilter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>
  ///   Отфильтровывает классы, представленные в массиве объектов <see cref="T:System.Type" />.
  /// </summary>
  /// <param name="m">
  ///   Объект <see langword="Type" />, к которому применяется фильтр.
  /// </param>
  /// <param name="filterCriteria">
  ///   Произвольный объект, используемый для фильтрации списка.
  /// </param>
  /// <returns>
  ///   Значение <see langword="true" /> для включения объекта <see cref="T:System.Type" /> в отфильтрованный список. В противном случае — значение <see langword="false" />.
  /// </returns>
  [ComVisible(true)]
  [Serializable]
  public delegate bool TypeFilter(Type m, object filterCriteria);
}
