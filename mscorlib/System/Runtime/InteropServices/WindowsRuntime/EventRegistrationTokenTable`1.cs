// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.WindowsRuntime.EventRegistrationTokenTable`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Runtime.InteropServices.WindowsRuntime
{
  /// <summary>
  ///   Хранит сопоставления между делегатами и токенами событий для поддержки реализации события Среда выполнения Windows в управляемом коде.
  /// </summary>
  /// <typeparam name="T">
  ///   Тип обработчика события делегата для определенного события.
  /// </typeparam>
  [__DynamicallyInvokable]
  public sealed class EventRegistrationTokenTable<T> where T : class
  {
    private Dictionary<EventRegistrationToken, T> m_tokens = new Dictionary<EventRegistrationToken, T>();
    private volatile T m_invokeList;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.InteropServices.WindowsRuntime.EventRegistrationTokenTable`1" />.
    /// </summary>
    /// <exception cref="T:System.InvalidOperationException">
    ///   <paramref name="T" /> не является типом делегата.
    /// </exception>
    [__DynamicallyInvokable]
    public EventRegistrationTokenTable()
    {
      if (!typeof (Delegate).IsAssignableFrom(typeof (T)))
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EventTokenTableRequiresDelegate", (object) typeof (T)));
    }

    /// <summary>
    ///   Возвращает или задает делегат типа <paramref name="T" /> список вызовов которого включает все делегатов обработчиков событий, добавленных и, еще не были удалены.
    ///    Вызов этого делегата вызывает все обработчики событий.
    /// </summary>
    /// <returns>
    ///   Делегат типа <paramref name="T" /> представляющий всех делегатов обработчиков событий, зарегистрированных для события.
    /// </returns>
    [__DynamicallyInvokable]
    public T InvocationList
    {
      [__DynamicallyInvokable] get
      {
        return this.m_invokeList;
      }
      [__DynamicallyInvokable] set
      {
        lock (this.m_tokens)
        {
          this.m_tokens.Clear();
          this.m_invokeList = default (T);
          if ((object) value == null)
            return;
          this.AddEventHandlerNoLock(value);
        }
      }
    }

    /// <summary>
    ///   Добавляет указанный обработчик события в таблицу и в список вызова и возвращает маркер, который может использоваться для удаления обработчика событий.
    /// </summary>
    /// <param name="handler">Добавляемый обработчик событий.</param>
    /// <returns>
    ///   Токен, который может использоваться для удаления обработчика событий из таблицы и списки вызовов.
    /// </returns>
    [__DynamicallyInvokable]
    public EventRegistrationToken AddEventHandler(T handler)
    {
      if ((object) handler == null)
        return new EventRegistrationToken(0UL);
      lock (this.m_tokens)
        return this.AddEventHandlerNoLock(handler);
    }

    private EventRegistrationToken AddEventHandlerNoLock(T handler)
    {
      EventRegistrationToken key = EventRegistrationTokenTable<T>.GetPreferredToken(handler);
      while (this.m_tokens.ContainsKey(key))
        key = new EventRegistrationToken(key.Value + 1UL);
      this.m_tokens[key] = handler;
      this.m_invokeList = (T) Delegate.Combine((Delegate) (object) this.m_invokeList, (Delegate) (object) handler);
      return key;
    }

    [FriendAccessAllowed]
    internal T ExtractHandler(EventRegistrationToken token)
    {
      T obj = default (T);
      lock (this.m_tokens)
      {
        if (this.m_tokens.TryGetValue(token, out obj))
          this.RemoveEventHandlerNoLock(token);
      }
      return obj;
    }

    private static EventRegistrationToken GetPreferredToken(T handler)
    {
      Delegate[] invocationList = ((Delegate) (object) handler).GetInvocationList();
      return new EventRegistrationToken((ulong) (uint) typeof (T).MetadataToken << 32 | (invocationList.Length != 1 ? (ulong) (uint) handler.GetHashCode() : (ulong) (uint) invocationList[0].Method.GetHashCode()));
    }

    /// <summary>
    ///   Удаляет обработчик событий, связанный с указанным маркером из таблицы и списки вызовов.
    /// </summary>
    /// <param name="token">
    ///   Токен, который был возвращен при добавлении обработчика событий.
    /// </param>
    [__DynamicallyInvokable]
    public void RemoveEventHandler(EventRegistrationToken token)
    {
      if (token.Value == 0UL)
        return;
      lock (this.m_tokens)
        this.RemoveEventHandlerNoLock(token);
    }

    /// <summary>
    ///   Удаляет делегата обработчика события из таблицы и списки вызовов.
    /// </summary>
    /// <param name="handler">
    ///   Обработчик событий, который требуется удалить.
    /// </param>
    [__DynamicallyInvokable]
    public void RemoveEventHandler(T handler)
    {
      if ((object) handler == null)
        return;
      lock (this.m_tokens)
      {
        EventRegistrationToken preferredToken = EventRegistrationTokenTable<T>.GetPreferredToken(handler);
        T obj;
        if (this.m_tokens.TryGetValue(preferredToken, out obj) && (object) obj == (object) handler)
        {
          this.RemoveEventHandlerNoLock(preferredToken);
        }
        else
        {
          foreach (KeyValuePair<EventRegistrationToken, T> token in this.m_tokens)
          {
            if ((object) token.Value == (object) handler)
            {
              this.RemoveEventHandlerNoLock(token.Key);
              break;
            }
          }
        }
      }
    }

    private void RemoveEventHandlerNoLock(EventRegistrationToken token)
    {
      T obj;
      if (!this.m_tokens.TryGetValue(token, out obj))
        return;
      this.m_tokens.Remove(token);
      this.m_invokeList = (T) Delegate.Remove((Delegate) (object) this.m_invokeList, (Delegate) (object) obj);
    }

    /// <summary>
    ///   Возвращает маркер таблицы регистрации указанного события, если это не <see langword="null" />; в противном случае — возвращает новую таблицу маркера регистрации событий.
    /// </summary>
    /// <param name="refEventTable">
    ///   Регистрация маркера таблицу событий, передаваемый по ссылке.
    /// </param>
    /// <returns>
    ///   Маркер таблицы регистрации событий, определяемый <paramref name="refEventTable" />, если он не <see langword="null" />; в противном случае — новая таблица маркера регистрации событий.
    /// </returns>
    [__DynamicallyInvokable]
    public static EventRegistrationTokenTable<T> GetOrCreateEventRegistrationTokenTable(ref EventRegistrationTokenTable<T> refEventTable)
    {
      if (refEventTable == null)
        Interlocked.CompareExchange<EventRegistrationTokenTable<T>>(ref refEventTable, new EventRegistrationTokenTable<T>(), (EventRegistrationTokenTable<T>) null);
      return refEventTable;
    }
  }
}
