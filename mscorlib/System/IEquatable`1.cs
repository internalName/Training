// Decompiled with JetBrains decompiler
// Type: System.IEquatable`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System
{
  /// <summary>
  ///   Определяет обобщенный метод, который реализуется типом значения или классом для создания метода с целью определения экземпляров.
  /// </summary>
  /// <typeparam name="T">Тип объектов для сравнения.</typeparam>
  [__DynamicallyInvokable]
  public interface IEquatable<T>
  {
    /// <summary>
    ///   Указывает, эквивалентен ли текущий объект другому объекту того же типа.
    /// </summary>
    /// <param name="other">
    ///   Объект, который требуется сравнить с данным объектом.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если текущий объект эквивалентен параметру <paramref name="other" />, в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    bool Equals(T other);
  }
}
