// Decompiled with JetBrains decompiler
// Type: System.Collections.IEnumerator
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Collections
{
  /// <summary>
  ///   Поддерживает простой перебор по неуниверсальной коллекции.
  /// </summary>
  [Guid("496B0ABF-CDEE-11d3-88E8-00902754C43A")]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public interface IEnumerator
  {
    /// <summary>
    ///   Перемещает перечислитель к следующему элементу коллекции.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если перечислитель был успешно перемещен к следующему элементу; значение <see langword="false" />, если перечислитель достиг конца коллекции.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Коллекция была изменена после создания перечислителя.
    /// </exception>
    [__DynamicallyInvokable]
    bool MoveNext();

    /// <summary>
    ///   Возвращает элемент коллекции, соответствующий текущей позиции перечислителя.
    /// </summary>
    /// <returns>
    ///   Элемент коллекции, соответствующий текущей позиции перечислителя.
    /// </returns>
    [__DynamicallyInvokable]
    object Current { [__DynamicallyInvokable] get; }

    /// <summary>
    ///   Устанавливает перечислитель в его начальное положение, т. е. перед первым элементом коллекции.
    /// </summary>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Коллекция была изменена после создания перечислителя.
    /// </exception>
    [__DynamicallyInvokable]
    void Reset();
  }
}
