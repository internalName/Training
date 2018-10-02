// Decompiled with JetBrains decompiler
// Type: System.Reflection.MemberFilter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>
  ///   Представляет делегат, используемый для фильтрации списка элементов, представленных в массиве объектов <see cref="T:System.Reflection.MemberInfo" />.
  /// </summary>
  /// <param name="m">
  ///   Объект <see cref="T:System.Reflection.MemberInfo" />, к которому применяется фильтр.
  /// </param>
  /// <param name="filterCriteria">
  ///   Произвольный объект, используемый для фильтрации списка.
  /// </param>
  /// <returns>
  ///   Значение <see langword="true" /> для включения элемента в отфильтрованный список. В противном случае — значение <see langword="false" />.
  /// </returns>
  [ComVisible(true)]
  [Serializable]
  public delegate bool MemberFilter(MemberInfo m, object filterCriteria);
}
