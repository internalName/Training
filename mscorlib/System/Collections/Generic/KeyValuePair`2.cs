// Decompiled with JetBrains decompiler
// Type: System.Collections.Generic.KeyValuePair`2
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Text;

namespace System.Collections.Generic
{
  /// <summary>
  ///   Определяет пару «ключ-значение», которая может быть задана или получена.
  /// </summary>
  /// <typeparam name="TKey">Тип ключа.</typeparam>
  /// <typeparam name="TValue">Тип значения.</typeparam>
  [__DynamicallyInvokable]
  [Serializable]
  public struct KeyValuePair<TKey, TValue>
  {
    private TKey key;
    private TValue value;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Collections.Generic.KeyValuePair`2" /> структуру с указанными ключом и значением.
    /// </summary>
    /// <param name="key">
    ///   Объект, определенный в каждой паре "ключ-значение".
    /// </param>
    /// <param name="value">
    ///   Определение, связанное с <paramref name="key" />.
    /// </param>
    [__DynamicallyInvokable]
    public KeyValuePair(TKey key, TValue value)
    {
      this.key = key;
      this.value = value;
    }

    /// <summary>Возвращает ключ из пары "ключ-значение".</summary>
    /// <returns>
    ///   Объект <paramref name="TKey" /> является ключом для <see cref="T:System.Collections.Generic.KeyValuePair`2" />.
    /// </returns>
    [__DynamicallyInvokable]
    public TKey Key
    {
      [__DynamicallyInvokable] get
      {
        return this.key;
      }
    }

    /// <summary>Возвращает значение из пары "ключ-значение".</summary>
    /// <returns>
    ///   Объект <paramref name="TValue" /> представляющий значение <see cref="T:System.Collections.Generic.KeyValuePair`2" />.
    /// </returns>
    [__DynamicallyInvokable]
    public TValue Value
    {
      [__DynamicallyInvokable] get
      {
        return this.value;
      }
    }

    /// <summary>
    ///   Возвращает строковое представление <see cref="T:System.Collections.Generic.KeyValuePair`2" />, используя строковые представления ключа и значения.
    /// </summary>
    /// <returns>
    ///   Строковое представление объекта <see cref="T:System.Collections.Generic.KeyValuePair`2" />, который включает строковые представления ключа и значения.
    /// </returns>
    [__DynamicallyInvokable]
    public override string ToString()
    {
      StringBuilder sb = StringBuilderCache.Acquire(16);
      sb.Append('[');
      if ((object) this.Key != null)
        sb.Append(this.Key.ToString());
      sb.Append(", ");
      if ((object) this.Value != null)
        sb.Append(this.Value.ToString());
      sb.Append(']');
      return StringBuilderCache.GetStringAndRelease(sb);
    }
  }
}
