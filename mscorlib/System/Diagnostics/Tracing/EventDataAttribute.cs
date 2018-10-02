// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.EventDataAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Diagnostics.Tracing
{
  /// <summary>
  ///   Указывает тип передаваемых <see cref="M:System.Diagnostics.Tracing.EventSource.Write``1(System.String,System.Diagnostics.Tracing.EventSourceOptions,``0)" /> метод.
  /// </summary>
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false)]
  [__DynamicallyInvokable]
  public class EventDataAttribute : Attribute
  {
    private EventLevel level = ~EventLevel.LogAlways;
    private EventOpcode opcode = ~EventOpcode.Info;

    /// <summary>
    ///   Возвращает или задает имя, присваиваемое событию, если его тип или свойство не именованы явно.
    /// </summary>
    /// <returns>Имя, назначаемое событию или свойству.</returns>
    [__DynamicallyInvokable]
    public string Name { [__DynamicallyInvokable] get; [__DynamicallyInvokable] set; }

    internal EventLevel Level
    {
      get
      {
        return this.level;
      }
      set
      {
        this.level = value;
      }
    }

    internal EventOpcode Opcode
    {
      get
      {
        return this.opcode;
      }
      set
      {
        this.opcode = value;
      }
    }

    internal EventKeywords Keywords { get; set; }

    internal EventTags Tags { get; set; }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Diagnostics.Tracing.EventDataAttribute" />.
    /// </summary>
    [__DynamicallyInvokable]
    public EventDataAttribute()
    {
    }
  }
}
