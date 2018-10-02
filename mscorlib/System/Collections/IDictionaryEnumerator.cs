// Decompiled with JetBrains decompiler
// Type: System.Collections.IDictionaryEnumerator
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Collections
{
  /// <summary>Перечисляет элементы неуниверсального словаря.</summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public interface IDictionaryEnumerator : IEnumerator
  {
    /// <summary>Возвращает ключ текущего элемента словаря.</summary>
    /// <returns>Ключ текущего элемента перечисления.</returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   <see cref="T:System.Collections.IDictionaryEnumerator" /> Располагается перед первым элементом словаря или после последнего элемента.
    /// </exception>
    [__DynamicallyInvokable]
    object Key { [__DynamicallyInvokable] get; }

    /// <summary>Возвращает значение текущего элемента словаря.</summary>
    /// <returns>Значение текущего элемента перечисления.</returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   <see cref="T:System.Collections.IDictionaryEnumerator" /> Располагается перед первым элементом словаря или после последнего элемента.
    /// </exception>
    [__DynamicallyInvokable]
    object Value { [__DynamicallyInvokable] get; }

    /// <summary>
    ///   Возвращает ключ и значение текущего элемента словаря.
    /// </summary>
    /// <returns>
    ///   A <see cref="T:System.Collections.DictionaryEntry" /> содержащий ключ и значение текущего элемента словаря.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   <see cref="T:System.Collections.IDictionaryEnumerator" /> Располагается перед первым элементом словаря или после последнего элемента.
    /// </exception>
    [__DynamicallyInvokable]
    DictionaryEntry Entry { [__DynamicallyInvokable] get; }
  }
}
