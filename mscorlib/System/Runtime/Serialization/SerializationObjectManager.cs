// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.SerializationObjectManager
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Security;

namespace System.Runtime.Serialization
{
  /// <summary>
  ///   Управляет процессами сериализации во время выполнения.
  ///    Этот класс не наследуется.
  /// </summary>
  public sealed class SerializationObjectManager
  {
    private Hashtable m_objectSeenTable = new Hashtable();
    private SerializationEventHandler m_onSerializedHandler;
    private StreamingContext m_context;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.Serialization.SerializationObjectManager" />.
    /// </summary>
    /// <param name="context">
    ///   Экземпляр <see cref="T:System.Runtime.Serialization.StreamingContext" /> класс, содержащий сведения о текущей операции сериализации.
    /// </param>
    public SerializationObjectManager(StreamingContext context)
    {
      this.m_context = context;
      this.m_objectSeenTable = new Hashtable();
    }

    /// <summary>Регистрирует объект, по которому будет события.</summary>
    /// <param name="obj">Регистрируемый объект.</param>
    [SecurityCritical]
    public void RegisterObject(object obj)
    {
      SerializationEvents serializationEventsForType = SerializationEventsCache.GetSerializationEventsForType(obj.GetType());
      if (!serializationEventsForType.HasOnSerializingEvents || this.m_objectSeenTable[obj] != null)
        return;
      this.m_objectSeenTable[obj] = (object) true;
      serializationEventsForType.InvokeOnSerializing(obj, this.m_context);
      this.AddOnSerialized(obj);
    }

    /// <summary>
    ///   Вызывает событие обратного вызова OnSerializing, если тип объекта содержит его. и регистрирует объект для вызова события OnSerialized, если имеется тип объекта.
    /// </summary>
    public void RaiseOnSerializedEvent()
    {
      if (this.m_onSerializedHandler == null)
        return;
      this.m_onSerializedHandler(this.m_context);
    }

    [SecuritySafeCritical]
    private void AddOnSerialized(object obj)
    {
      this.m_onSerializedHandler = SerializationEventsCache.GetSerializationEventsForType(obj.GetType()).AddOnSerialized(obj, this.m_onSerializedHandler);
    }
  }
}
