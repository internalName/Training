// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.EventAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Diagnostics.Tracing
{
  /// <summary>Задает дополнительную информацию схемы для события.</summary>
  [AttributeUsage(AttributeTargets.Method)]
  [__DynamicallyInvokable]
  public sealed class EventAttribute : Attribute
  {
    private EventOpcode m_opcode;
    private bool m_opcodeSet;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Diagnostics.Tracing.EventAttribute" /> класса с указанным идентификатором события.
    /// </summary>
    /// <param name="eventId">Идентификатор события для события.</param>
    [__DynamicallyInvokable]
    public EventAttribute(int eventId)
    {
      this.EventId = eventId;
      this.Level = EventLevel.Informational;
      this.m_opcodeSet = false;
    }

    /// <summary>Возвращает или задает идентификатор события.</summary>
    /// <returns>
    ///   Идентификатор события.
    ///    Это значение должно находиться в диапазоне от 0 до 65535.
    /// </returns>
    [__DynamicallyInvokable]
    public int EventId { [__DynamicallyInvokable] get; private set; }

    /// <summary>Возвращает или задает уровень для события.</summary>
    /// <returns>
    ///   Одно из значений перечисления, определяющее уровень события.
    /// </returns>
    [__DynamicallyInvokable]
    public EventLevel Level { [__DynamicallyInvokable] get; [__DynamicallyInvokable] set; }

    /// <summary>Возвращает или задает ключевые слова для события.</summary>
    /// <returns>Побитовое сочетание значений перечисления.</returns>
    [__DynamicallyInvokable]
    public EventKeywords Keywords { [__DynamicallyInvokable] get; [__DynamicallyInvokable] set; }

    /// <summary>Возвращает или задает код операции для события.</summary>
    /// <returns>
    ///   Одно из значений перечисления, определяющее код операции.
    /// </returns>
    [__DynamicallyInvokable]
    public EventOpcode Opcode
    {
      [__DynamicallyInvokable] get
      {
        return this.m_opcode;
      }
      [__DynamicallyInvokable] set
      {
        this.m_opcode = value;
        this.m_opcodeSet = true;
      }
    }

    internal bool IsOpcodeSet
    {
      get
      {
        return this.m_opcodeSet;
      }
    }

    /// <summary>Возвращает или задает задачу для события.</summary>
    /// <returns>Задача для события.</returns>
    [__DynamicallyInvokable]
    public EventTask Task { [__DynamicallyInvokable] get; [__DynamicallyInvokable] set; }

    /// <summary>
    ///   Возвращает или задает дополнительный журнал событий, в который должно быть записано событие.
    /// </summary>
    /// <returns>
    ///   Дополнительный журнал событий, в который должно быть записано событие.
    /// </returns>
    [__DynamicallyInvokable]
    public EventChannel Channel { [__DynamicallyInvokable] get; [__DynamicallyInvokable] set; }

    /// <summary>Возвращает или задает версию события.</summary>
    /// <returns>Версия события.</returns>
    [__DynamicallyInvokable]
    public byte Version { [__DynamicallyInvokable] get; [__DynamicallyInvokable] set; }

    /// <summary>Возвращает или задает сообщение для события.</summary>
    /// <returns>Сообщение для события.</returns>
    [__DynamicallyInvokable]
    public string Message { [__DynamicallyInvokable] get; [__DynamicallyInvokable] set; }

    /// <summary>
    ///   Возвращает и задает <see cref="T:System.Diagnostics.Tracing.EventTags" /> значение для этой <see cref="T:System.Diagnostics.Tracing.EventAttribute" /> объекта.
    ///    Тег события является определяемое пользователем значение, которое передается через регистрируется событие.
    /// </summary>
    /// <returns>
    ///   Возвращает <see cref="T:System.Diagnostics.Tracing.EventTags" /> значение.
    /// </returns>
    [__DynamicallyInvokable]
    public EventTags Tags { [__DynamicallyInvokable] get; [__DynamicallyInvokable] set; }

    /// <summary>
    ///   Задает поведение события запуска и остановки действия.
    ///    Действие — это регион времени в приложении между start и stop.
    /// </summary>
    /// <returns>
    ///   Возвращает <see cref="T:System.Diagnostics.Tracing.EventActivityOptions" />.
    /// </returns>
    [__DynamicallyInvokable]
    public EventActivityOptions ActivityOptions { [__DynamicallyInvokable] get; [__DynamicallyInvokable] set; }
  }
}
