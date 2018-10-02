// Decompiled with JetBrains decompiler
// Type: System.Collections.DictionaryEntry
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Collections
{
  /// <summary>
  ///   Определяет пару «ключ-значение», которую можно задать или извлечь.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public struct DictionaryEntry
  {
    private object _key;
    private object _value;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Collections.DictionaryEntry" /> типа с указанным ключом и значением.
    /// </summary>
    /// <param name="key">
    ///   Объект, определенный в каждой паре "ключ-значение".
    /// </param>
    /// <param name="value">
    ///   Определение, связанное с <paramref name="key" />.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="key" /> — <see langword="null" /> и .NET Framework версии 1.0 или 1.1.
    /// </exception>
    [__DynamicallyInvokable]
    public DictionaryEntry(object key, object value)
    {
      this._key = key;
      this._value = value;
    }

    /// <summary>Возвращает или задает ключ в паре «ключ значение».</summary>
    /// <returns>Ключ в паре «ключ значение».</returns>
    [__DynamicallyInvokable]
    public object Key
    {
      [__DynamicallyInvokable] get
      {
        return this._key;
      }
      [__DynamicallyInvokable] set
      {
        this._key = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает значение в паре «ключ значение».
    /// </summary>
    /// <returns>Значение в паре «ключ значение».</returns>
    [__DynamicallyInvokable]
    public object Value
    {
      [__DynamicallyInvokable] get
      {
        return this._value;
      }
      [__DynamicallyInvokable] set
      {
        this._value = value;
      }
    }
  }
}
