// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.EventListener
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Threading;

namespace System.Diagnostics.Tracing
{
  /// <summary>
  ///   Предоставляет методы для включения и отключения событий из источников событий.
  /// </summary>
  [__DynamicallyInvokable]
  public class EventListener : IDisposable
  {
    private static readonly object s_EventSourceCreatedLock = new object();
    private static bool s_CreatingListener = false;
    private static bool s_EventSourceShutdownRegistered = false;
    internal volatile EventListener m_Next;
    internal ActivityFilter m_activityFilter;
    internal static EventListener s_Listeners;
    internal static List<WeakReference> s_EventSources;

    private event EventHandler<EventSourceCreatedEventArgs> _EventSourceCreated;

    /// <summary>
    ///   Происходит, когда источник событий (объект <see cref="T:System.Diagnostics.Tracing.EventSource" />) подключается к диспетчеру.
    /// </summary>
    public event EventHandler<EventSourceCreatedEventArgs> EventSourceCreated
    {
      add
      {
        lock (EventListener.s_EventSourceCreatedLock)
        {
          this.CallBackForExistingEventSources(false, value);
          this._EventSourceCreated += value;
        }
      }
      remove
      {
        lock (EventListener.s_EventSourceCreatedLock)
          this._EventSourceCreated -= value;
      }
    }

    /// <summary>
    ///   Происходит, когда событие записано источником события (объектом <see cref="T:System.Diagnostics.Tracing.EventSource" />), для которого прослушиватель события включил события.
    /// </summary>
    public event EventHandler<EventWrittenEventArgs> EventWritten;

    /// <summary>
    ///   Создает новый экземпляр класса <see cref="T:System.Diagnostics.Tracing.EventListener" />.
    /// </summary>
    [__DynamicallyInvokable]
    public EventListener()
    {
      this.CallBackForExistingEventSources(true, (EventHandler<EventSourceCreatedEventArgs>) ((obj, args) => args.EventSource.AddListener(this)));
    }

    /// <summary>
    ///   Освобождает ресурсы, используемые текущим экземпляром класса <see cref="T:System.Diagnostics.Tracing.EventListener" />.
    /// </summary>
    [__DynamicallyInvokable]
    public virtual void Dispose()
    {
      lock (EventListener.EventListenersLock)
      {
        if (EventListener.s_Listeners == null)
          return;
        if (this == EventListener.s_Listeners)
        {
          EventListener listeners = EventListener.s_Listeners;
          EventListener.s_Listeners = this.m_Next;
          EventListener.RemoveReferencesToListenerInEventSources(listeners);
        }
        else
        {
          EventListener eventListener = EventListener.s_Listeners;
          EventListener next;
          while (true)
          {
            next = eventListener.m_Next;
            if (next != null)
            {
              if (next != this)
                eventListener = next;
              else
                goto label_7;
            }
            else
              break;
          }
          return;
label_7:
          eventListener.m_Next = next.m_Next;
          EventListener.RemoveReferencesToListenerInEventSources(next);
        }
      }
    }

    /// <summary>
    ///   Включает события для заданного источника событий, который содержит указанный уровень детализации или ниже.
    /// </summary>
    /// <param name="eventSource">
    ///   Источник события, для которого требуется включить события.
    /// </param>
    /// <param name="level">
    ///   Уровень событий, который требуется разрешить.
    /// </param>
    [__DynamicallyInvokable]
    public void EnableEvents(EventSource eventSource, EventLevel level)
    {
      this.EnableEvents(eventSource, level, EventKeywords.None);
    }

    /// <summary>
    ///   Включает события для заданного источника события, который содержит указанный уровень детализации или ниже, и соответствующие флаги ключевого слова.
    /// </summary>
    /// <param name="eventSource">
    ///   Источник события, для которого требуется включить события.
    /// </param>
    /// <param name="level">
    ///   Уровень событий, который требуется разрешить.
    /// </param>
    /// <param name="matchAnyKeyword">
    ///   Флаги ключевых слов, необходимые для включения событий.
    /// </param>
    [__DynamicallyInvokable]
    public void EnableEvents(EventSource eventSource, EventLevel level, EventKeywords matchAnyKeyword)
    {
      this.EnableEvents(eventSource, level, matchAnyKeyword, (IDictionary<string, string>) null);
    }

    /// <summary>
    ///   Включает события для заданного источника события, который содержит указанный уровень детализации или ниже, соответствующие флаги ключевого слова и аргументы.
    /// </summary>
    /// <param name="eventSource">
    ///   Источник события, для которого требуется включить события.
    /// </param>
    /// <param name="level">
    ///   Уровень событий, который требуется разрешить.
    /// </param>
    /// <param name="matchAnyKeyword">
    ///   Флаги ключевых слов, необходимые для включения событий.
    /// </param>
    /// <param name="arguments">
    ///   Аргументы, сопоставляемые для реализации событий.
    /// </param>
    [__DynamicallyInvokable]
    public void EnableEvents(EventSource eventSource, EventLevel level, EventKeywords matchAnyKeyword, IDictionary<string, string> arguments)
    {
      if (eventSource == null)
        throw new ArgumentNullException(nameof (eventSource));
      eventSource.SendCommand(this, 0, 0, EventCommand.Update, true, level, matchAnyKeyword, arguments);
    }

    /// <summary>
    ///   Отключает все события для заданного источника события.
    /// </summary>
    /// <param name="eventSource">
    ///   Источник событий, для которого требуется отключить события.
    /// </param>
    [__DynamicallyInvokable]
    public void DisableEvents(EventSource eventSource)
    {
      if (eventSource == null)
        throw new ArgumentNullException(nameof (eventSource));
      eventSource.SendCommand(this, 0, 0, EventCommand.Update, false, EventLevel.LogAlways, EventKeywords.None, (IDictionary<string, string>) null);
    }

    /// <summary>
    ///   Возвращает маленькое неотрицательное число, представляющее указанный источник события.
    /// </summary>
    /// <param name="eventSource">
    ///   Источник события, для которого требуется найти индекс.
    /// </param>
    /// <returns>
    ///   Маленькое неотрицательное число, представляющее указанный источник события.
    /// </returns>
    [__DynamicallyInvokable]
    public static int EventSourceIndex(EventSource eventSource)
    {
      return eventSource.m_id;
    }

    /// <summary>
    ///   Вызывается для всех существующих источников событий, когда прослушиватель события создан и когда новый источник события вложен в прослушиватель.
    /// </summary>
    /// <param name="eventSource">Источник события.</param>
    [__DynamicallyInvokable]
    protected internal virtual void OnEventSourceCreated(EventSource eventSource)
    {
      // ISSUE: reference to a compiler-generated field
      EventHandler<EventSourceCreatedEventArgs> eventSourceCreated = this._EventSourceCreated;
      if (eventSourceCreated == null)
        return;
      eventSourceCreated((object) this, new EventSourceCreatedEventArgs()
      {
        EventSource = eventSource
      });
    }

    /// <summary>
    ///   Вызывается, когда событие было записано источником события, для которого прослушиватель события включил события.
    /// </summary>
    /// <param name="eventData">
    ///   Аргументы события, описывающие событие.
    /// </param>
    [__DynamicallyInvokable]
    protected internal virtual void OnEventWritten(EventWrittenEventArgs eventData)
    {
      // ISSUE: reference to a compiler-generated field
      EventHandler<EventWrittenEventArgs> eventWritten = this.EventWritten;
      if (eventWritten == null)
        return;
      eventWritten((object) this, eventData);
    }

    internal static void AddEventSource(EventSource newEventSource)
    {
      lock (EventListener.EventListenersLock)
      {
        if (EventListener.s_EventSources == null)
          EventListener.s_EventSources = new List<WeakReference>(2);
        if (!EventListener.s_EventSourceShutdownRegistered)
        {
          EventListener.s_EventSourceShutdownRegistered = true;
          AppDomain.CurrentDomain.ProcessExit += new EventHandler(EventListener.DisposeOnShutdown);
          AppDomain.CurrentDomain.DomainUnload += new EventHandler(EventListener.DisposeOnShutdown);
        }
        int num = -1;
        if (EventListener.s_EventSources.Count % 64 == 63)
        {
          int count = EventListener.s_EventSources.Count;
          while (0 < count)
          {
            --count;
            WeakReference eventSource = EventListener.s_EventSources[count];
            if (!eventSource.IsAlive)
            {
              num = count;
              eventSource.Target = (object) newEventSource;
              break;
            }
          }
        }
        if (num < 0)
        {
          num = EventListener.s_EventSources.Count;
          EventListener.s_EventSources.Add(new WeakReference((object) newEventSource));
        }
        newEventSource.m_id = num;
        for (EventListener listener = EventListener.s_Listeners; listener != null; listener = listener.m_Next)
          newEventSource.AddListener(listener);
      }
    }

    private static void DisposeOnShutdown(object sender, EventArgs e)
    {
      lock (EventListener.EventListenersLock)
      {
        foreach (WeakReference eventSource in EventListener.s_EventSources)
          (eventSource.Target as EventSource)?.Dispose();
      }
    }

    private static void RemoveReferencesToListenerInEventSources(EventListener listenerToRemove)
    {
      using (List<WeakReference>.Enumerator enumerator = EventListener.s_EventSources.GetEnumerator())
      {
label_10:
        while (enumerator.MoveNext())
        {
          EventSource target = enumerator.Current.Target as EventSource;
          if (target != null)
          {
            if (target.m_Dispatchers.m_Listener == listenerToRemove)
            {
              target.m_Dispatchers = target.m_Dispatchers.m_Next;
            }
            else
            {
              EventDispatcher eventDispatcher = target.m_Dispatchers;
              EventDispatcher next;
              while (true)
              {
                next = eventDispatcher.m_Next;
                if (next != null)
                {
                  if (next.m_Listener != listenerToRemove)
                    eventDispatcher = next;
                  else
                    break;
                }
                else
                  goto label_10;
              }
              eventDispatcher.m_Next = next.m_Next;
            }
          }
        }
      }
    }

    [Conditional("DEBUG")]
    internal static void Validate()
    {
      lock (EventListener.EventListenersLock)
      {
        Dictionary<EventListener, bool> dictionary = new Dictionary<EventListener, bool>();
        for (EventListener key = EventListener.s_Listeners; key != null; key = key.m_Next)
          dictionary.Add(key, true);
        int num = -1;
        foreach (WeakReference eventSource in EventListener.s_EventSources)
        {
          ++num;
          EventSource target = eventSource.Target as EventSource;
          if (target != null)
          {
            EventDispatcher eventDispatcher1 = target.m_Dispatchers;
            while (eventDispatcher1 != null)
              eventDispatcher1 = eventDispatcher1.m_Next;
            using (Dictionary<EventListener, bool>.KeyCollection.Enumerator enumerator = dictionary.Keys.GetEnumerator())
            {
label_15:
              while (enumerator.MoveNext())
              {
                EventListener current = enumerator.Current;
                EventDispatcher eventDispatcher2 = target.m_Dispatchers;
                while (true)
                {
                  if (eventDispatcher2.m_Listener != current)
                    eventDispatcher2 = eventDispatcher2.m_Next;
                  else
                    goto label_15;
                }
              }
            }
          }
        }
      }
    }

    internal static object EventListenersLock
    {
      get
      {
        if (EventListener.s_EventSources == null)
          Interlocked.CompareExchange<List<WeakReference>>(ref EventListener.s_EventSources, new List<WeakReference>(2), (List<WeakReference>) null);
        return (object) EventListener.s_EventSources;
      }
    }

    private void CallBackForExistingEventSources(bool addToListenersList, EventHandler<EventSourceCreatedEventArgs> callback)
    {
      lock (EventListener.EventListenersLock)
      {
        if (EventListener.s_CreatingListener)
          throw new InvalidOperationException(Environment.GetResourceString("EventSource_ListenerCreatedInsideCallback"));
        try
        {
          EventListener.s_CreatingListener = true;
          if (addToListenersList)
          {
            this.m_Next = EventListener.s_Listeners;
            EventListener.s_Listeners = this;
          }
          foreach (WeakReference weakReference in EventListener.s_EventSources.ToArray())
          {
            EventSource target = weakReference.Target as EventSource;
            if (target != null)
              callback((object) this, new EventSourceCreatedEventArgs()
              {
                EventSource = target
              });
          }
        }
        finally
        {
          EventListener.s_CreatingListener = false;
        }
      }
    }
  }
}
