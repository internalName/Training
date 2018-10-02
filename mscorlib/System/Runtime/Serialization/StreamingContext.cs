// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.StreamingContext
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
  /// <summary>
  ///   Описывает источник и назначение заданного потока сериализации и обеспечивает дополнительный контекст, определяемый вызывающим объектом.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public struct StreamingContext
  {
    internal object m_additionalContext;
    internal StreamingContextStates m_state;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.Serialization.StreamingContext" /> класса с заданным состоянием контекста.
    /// </summary>
    /// <param name="state">
    ///   Побитовое сочетание <see cref="T:System.Runtime.Serialization.StreamingContextStates" /> значения, которые задают контекст источника или назначения для этого <see cref="T:System.Runtime.Serialization.StreamingContext" />.
    /// </param>
    public StreamingContext(StreamingContextStates state)
    {
      this = new StreamingContext(state, (object) null);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.Serialization.StreamingContext" /> класса с заданным состоянием контекста и некоторыми дополнительными сведениями.
    /// </summary>
    /// <param name="state">
    ///   Побитовое сочетание <see cref="T:System.Runtime.Serialization.StreamingContextStates" /> значения, которые задают контекст источника или назначения для этого <see cref="T:System.Runtime.Serialization.StreamingContext" />.
    /// </param>
    /// <param name="additional">
    ///   Никаких дополнительных сведений, связанных с <see cref="T:System.Runtime.Serialization.StreamingContext" />.
    ///    Эта информация доступна для любой объект, реализующий <see cref="T:System.Runtime.Serialization.ISerializable" /> или любой суррогат сериализации.
    ///    Большинство пользователей не обязательно устанавливать для этого параметра.
    /// </param>
    public StreamingContext(StreamingContextStates state, object additional)
    {
      this.m_state = state;
      this.m_additionalContext = additional;
    }

    /// <summary>
    ///   Возвращает контекст, заданный как часть дополнительного контекста.
    /// </summary>
    /// <returns>
    ///   Контекст, заданный как часть дополнительного контекста.
    /// </returns>
    public object Context
    {
      get
      {
        return this.m_additionalContext;
      }
    }

    /// <summary>
    ///   Определяет неравенство двух <see cref="T:System.Runtime.Serialization.StreamingContext" /> экземпляры содержат одинаковые значения.
    /// </summary>
    /// <param name="obj">
    ///   Объект для сравнения с текущим экземпляром.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если указанный объект является экземпляром <see cref="T:System.Runtime.Serialization.StreamingContext" /> и равен значению текущего экземпляра; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public override bool Equals(object obj)
    {
      return obj is StreamingContext && ((StreamingContext) obj).m_additionalContext == this.m_additionalContext && ((StreamingContext) obj).m_state == this.m_state;
    }

    /// <summary>Возвращает хэш-код данного объекта.</summary>
    /// <returns>
    ///   <see cref="T:System.Runtime.Serialization.StreamingContextStates" /> Значение, содержащее источник или назначение для этой сериализации <see cref="T:System.Runtime.Serialization.StreamingContext" />.
    /// </returns>
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      return (int) this.m_state;
    }

    /// <summary>
    ///   Возвращает источник или назначение переданных данных.
    /// </summary>
    /// <returns>
    ///   При сериализации назначение переданных данных.
    ///    Во время десериализации источник данных.
    /// </returns>
    public StreamingContextStates State
    {
      get
      {
        return this.m_state;
      }
    }
  }
}
