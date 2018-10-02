// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.EventWrittenEventArgs
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Security;

namespace System.Diagnostics.Tracing
{
  /// <summary>
  ///   Предоставляет данные для обратного вызова <see cref="M:System.Diagnostics.Tracing.EventListener.OnEventWritten(System.Diagnostics.Tracing.EventWrittenEventArgs)" />.
  /// </summary>
  [__DynamicallyInvokable]
  public class EventWrittenEventArgs : EventArgs
  {
    private string m_message;
    private string m_eventName;
    private EventSource m_eventSource;
    private ReadOnlyCollection<string> m_payloadNames;
    internal EventTags m_tags;
    internal EventOpcode m_opcode;
    internal EventKeywords m_keywords;

    /// <summary>Возвращает имя события.</summary>
    /// <returns>Имя события.</returns>
    [__DynamicallyInvokable]
    public string EventName
    {
      [__DynamicallyInvokable] get
      {
        if (this.m_eventName != null || this.EventId < 0)
          return this.m_eventName;
        return this.m_eventSource.m_eventData[this.EventId].Name;
      }
      internal set
      {
        this.m_eventName = value;
      }
    }

    /// <summary>Возвращает идентификатор события.</summary>
    /// <returns>Идентификатор события.</returns>
    [__DynamicallyInvokable]
    public int EventId { [__DynamicallyInvokable] get; internal set; }

    /// <summary>
    ///   [Поддерживается в .NET Framework 4.5.1 и более поздних версиях.]
    /// 
    ///   Получает идентификатор действий в потоке, куда было записано событие.
    /// </summary>
    /// <returns>
    ///   Идентификатор действий в потоке, куда было записано событие.
    /// </returns>
    [__DynamicallyInvokable]
    public Guid ActivityId
    {
      [SecurityCritical, __DynamicallyInvokable] get
      {
        return EventSource.CurrentThreadActivityId;
      }
    }

    /// <summary>
    ///   [Поддерживается в .NET Framework 4.5.1 и более поздних версиях.]
    /// 
    ///   Возвращает идентификатор действия, которое связано с действием, представленным текущим экземпляром.
    /// </summary>
    /// <returns>
    ///   Идентификатор связанного действия, либо значение <see cref="F:System.Guid.Empty" />, если отсутствует связанная действие.
    /// </returns>
    [__DynamicallyInvokable]
    public Guid RelatedActivityId { [SecurityCritical, __DynamicallyInvokable] get; internal set; }

    /// <summary>Возвращает полезные данные для события.</summary>
    /// <returns>Полезные данные для события.</returns>
    [__DynamicallyInvokable]
    public ReadOnlyCollection<object> Payload { [__DynamicallyInvokable] get; internal set; }

    /// <summary>
    ///   Возвращает список строк, представляющих имена свойств события.
    /// </summary>
    /// <returns>
    ///   Возвращает <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" />.
    /// </returns>
    [__DynamicallyInvokable]
    public ReadOnlyCollection<string> PayloadNames
    {
      [__DynamicallyInvokable] get
      {
        if (this.m_payloadNames == null)
        {
          List<string> stringList = new List<string>();
          foreach (ParameterInfo parameter in this.m_eventSource.m_eventData[this.EventId].Parameters)
            stringList.Add(parameter.Name);
          this.m_payloadNames = new ReadOnlyCollection<string>((IList<string>) stringList);
        }
        return this.m_payloadNames;
      }
      internal set
      {
        this.m_payloadNames = value;
      }
    }

    /// <summary>Возвращает объект источника события.</summary>
    /// <returns>Объект источника события.</returns>
    [__DynamicallyInvokable]
    public EventSource EventSource
    {
      [__DynamicallyInvokable] get
      {
        return this.m_eventSource;
      }
    }

    /// <summary>Возвращает ключевые слова для события.</summary>
    /// <returns>Ключевые слова для события.</returns>
    [__DynamicallyInvokable]
    public EventKeywords Keywords
    {
      [__DynamicallyInvokable] get
      {
        if (this.EventId < 0)
          return this.m_keywords;
        return (EventKeywords) this.m_eventSource.m_eventData[this.EventId].Descriptor.Keywords;
      }
    }

    /// <summary>Возвращает код операции для события.</summary>
    /// <returns>Код операции для события.</returns>
    [__DynamicallyInvokable]
    public EventOpcode Opcode
    {
      [__DynamicallyInvokable] get
      {
        if (this.EventId < 0)
          return this.m_opcode;
        return (EventOpcode) this.m_eventSource.m_eventData[this.EventId].Descriptor.Opcode;
      }
    }

    /// <summary>Возвращает задачу для события.</summary>
    /// <returns>Задача для события.</returns>
    [__DynamicallyInvokable]
    public EventTask Task
    {
      [__DynamicallyInvokable] get
      {
        if (this.EventId < 0)
          return EventTask.None;
        return (EventTask) this.m_eventSource.m_eventData[this.EventId].Descriptor.Task;
      }
    }

    /// <summary>
    ///   Возвращает указанные теги в вызове метода <see cref="M:System.Diagnostics.Tracing.EventSource.Write(System.String,System.Diagnostics.Tracing.EventSourceOptions)" />.
    /// </summary>
    /// <returns>
    ///   Возвращает <see cref="T:System.Diagnostics.Tracing.EventTags" />.
    /// </returns>
    [__DynamicallyInvokable]
    public EventTags Tags
    {
      [__DynamicallyInvokable] get
      {
        if (this.EventId < 0)
          return this.m_tags;
        return this.m_eventSource.m_eventData[this.EventId].Tags;
      }
    }

    /// <summary>Возвращает сообщение для события.</summary>
    /// <returns>Сообщение для события.</returns>
    [__DynamicallyInvokable]
    public string Message
    {
      [__DynamicallyInvokable] get
      {
        if (this.EventId < 0)
          return this.m_message;
        return this.m_eventSource.m_eventData[this.EventId].Message;
      }
      internal set
      {
        this.m_message = value;
      }
    }

    /// <summary>Возвращает канал события.</summary>
    /// <returns>Канал события.</returns>
    [__DynamicallyInvokable]
    public EventChannel Channel
    {
      [__DynamicallyInvokable] get
      {
        if (this.EventId < 0)
          return EventChannel.None;
        return (EventChannel) this.m_eventSource.m_eventData[this.EventId].Descriptor.Channel;
      }
    }

    /// <summary>Возвращает версию события.</summary>
    /// <returns>Версия события.</returns>
    [__DynamicallyInvokable]
    public byte Version
    {
      [__DynamicallyInvokable] get
      {
        if (this.EventId < 0)
          return 0;
        return this.m_eventSource.m_eventData[this.EventId].Descriptor.Version;
      }
    }

    /// <summary>Возвращает уровень события.</summary>
    /// <returns>Уровень события.</returns>
    [__DynamicallyInvokable]
    public EventLevel Level
    {
      [__DynamicallyInvokable] get
      {
        if (this.EventId < 0)
          return EventLevel.LogAlways;
        return (EventLevel) this.m_eventSource.m_eventData[this.EventId].Descriptor.Level;
      }
    }

    internal EventWrittenEventArgs(EventSource eventSource)
    {
      this.m_eventSource = eventSource;
    }
  }
}
