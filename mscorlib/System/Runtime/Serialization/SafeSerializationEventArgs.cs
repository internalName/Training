// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.SafeSerializationEventArgs
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;

namespace System.Runtime.Serialization
{
  /// <summary>
  ///   Предоставляет данные для события <see cref="E:System.Exception.SerializeObjectState" />.
  /// </summary>
  public sealed class SafeSerializationEventArgs : EventArgs
  {
    private List<object> m_serializedStates = new List<object>();
    private StreamingContext m_streamingContext;

    internal SafeSerializationEventArgs(StreamingContext streamingContext)
    {
      this.m_streamingContext = streamingContext;
    }

    /// <summary>Сохраняет состояние исключения.</summary>
    /// <param name="serializedState">
    ///   Объект состояния, который сериализуется с экземпляром.
    /// </param>
    public void AddSerializedState(ISafeSerializationData serializedState)
    {
      if (serializedState == null)
        throw new ArgumentNullException(nameof (serializedState));
      if (!serializedState.GetType().IsSerializable)
        throw new ArgumentException(Environment.GetResourceString("Serialization_NonSerType", (object) serializedState.GetType(), (object) serializedState.GetType().Assembly.FullName));
      this.m_serializedStates.Add((object) serializedState);
    }

    internal IList<object> SerializedStates
    {
      get
      {
        return (IList<object>) this.m_serializedStates;
      }
    }

    /// <summary>
    ///   Возвращает или задает объект, описывающий источник и назначение сериализованного потока.
    /// </summary>
    /// <returns>
    ///   Объект, описывающий источник и назначение сериализованного потока.
    /// </returns>
    public StreamingContext StreamingContext
    {
      get
      {
        return this.m_streamingContext;
      }
    }
  }
}
