// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.EventCommandEventArgs
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;

namespace System.Diagnostics.Tracing
{
  /// <summary>
  ///   Предоставляет аргументы для <see cref="M:System.Diagnostics.Tracing.EventSource.OnEventCommand(System.Diagnostics.Tracing.EventCommandEventArgs)" /> обратного вызова.
  /// </summary>
  [__DynamicallyInvokable]
  public class EventCommandEventArgs : EventArgs
  {
    internal EventSource eventSource;
    internal EventDispatcher dispatcher;
    internal EventListener listener;
    internal int perEventSourceSessionId;
    internal int etwSessionId;
    internal bool enable;
    internal EventLevel level;
    internal EventKeywords matchAnyKeyword;
    internal EventCommandEventArgs nextCommand;

    /// <summary>Возвращает команду для обратного вызова.</summary>
    /// <returns>Команда обратного вызова.</returns>
    [__DynamicallyInvokable]
    public EventCommand Command { [__DynamicallyInvokable] get; internal set; }

    /// <summary>Возвращает массив аргументов для обратного вызова.</summary>
    /// <returns>Массив аргументов обратного вызова.</returns>
    [__DynamicallyInvokable]
    public IDictionary<string, string> Arguments { [__DynamicallyInvokable] get; internal set; }

    /// <summary>Включает события с указанным идентификатором.</summary>
    /// <param name="eventId">
    ///   Идентификатор события, чтобы включить.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если <paramref name="eventId" /> находится в диапазоне; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool EnableEvent(int eventId)
    {
      if (this.Command != EventCommand.Enable && this.Command != EventCommand.Disable)
        throw new InvalidOperationException();
      return this.eventSource.EnableEventForDispatcher(this.dispatcher, eventId, true);
    }

    /// <summary>Отключает события с указанным идентификатором.</summary>
    /// <param name="eventId">
    ///   Идентификатор события, чтобы отключить.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если <paramref name="eventId" /> находится в диапазоне; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool DisableEvent(int eventId)
    {
      if (this.Command != EventCommand.Enable && this.Command != EventCommand.Disable)
        throw new InvalidOperationException();
      return this.eventSource.EnableEventForDispatcher(this.dispatcher, eventId, false);
    }

    internal EventCommandEventArgs(EventCommand command, IDictionary<string, string> arguments, EventSource eventSource, EventListener listener, int perEventSourceSessionId, int etwSessionId, bool enable, EventLevel level, EventKeywords matchAnyKeyword)
    {
      this.Command = command;
      this.Arguments = arguments;
      this.eventSource = eventSource;
      this.listener = listener;
      this.perEventSourceSessionId = perEventSourceSessionId;
      this.etwSessionId = etwSessionId;
      this.enable = enable;
      this.level = level;
      this.matchAnyKeyword = matchAnyKeyword;
    }
  }
}
