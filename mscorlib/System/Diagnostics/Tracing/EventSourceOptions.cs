// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.EventSourceOptions
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Diagnostics.Tracing
{
  /// <summary>
  ///   Задает переопределения параметров события по умолчанию, таких как уровень ведения журнала, ключевые слова и код операции, при вызове метода <see cref="M:System.Diagnostics.Tracing.EventSource.Write``1(System.String,System.Diagnostics.Tracing.EventSourceOptions,``0)" />.
  /// </summary>
  [__DynamicallyInvokable]
  public struct EventSourceOptions
  {
    internal EventKeywords keywords;
    internal EventTags tags;
    internal EventActivityOptions activityOptions;
    internal byte level;
    internal byte opcode;
    internal byte valuesSet;
    internal const byte keywordsSet = 1;
    internal const byte tagsSet = 2;
    internal const byte levelSet = 4;
    internal const byte opcodeSet = 8;
    internal const byte activityOptionsSet = 16;

    /// <summary>
    ///   Возвращает или задает уровень, применяемый к событию.
    /// </summary>
    /// <returns>
    ///   Уровень события.
    ///    Если значение не задано, по умолчанию используется значение Verbose (5).
    /// </returns>
    [__DynamicallyInvokable]
    public EventLevel Level
    {
      [__DynamicallyInvokable] get
      {
        return (EventLevel) this.level;
      }
      [__DynamicallyInvokable] set
      {
        this.level = checked ((byte) (uint) value);
        this.valuesSet |= (byte) 4;
      }
    }

    /// <summary>
    ///   Возвращает или задает код операции для указанного события.
    /// </summary>
    /// <returns>
    ///   Код операции для указанного события.
    ///    Если значение не задано, по умолчанию используется значение <see langword="Info" /> (0).
    /// </returns>
    [__DynamicallyInvokable]
    public EventOpcode Opcode
    {
      [__DynamicallyInvokable] get
      {
        return (EventOpcode) this.opcode;
      }
      [__DynamicallyInvokable] set
      {
        this.opcode = checked ((byte) (uint) value);
        this.valuesSet |= (byte) 8;
      }
    }

    internal bool IsOpcodeSet
    {
      get
      {
        return ((uint) this.valuesSet & 8U) > 0U;
      }
    }

    /// <summary>
    ///   Возвращает или задает ключевые слова, применяемые к событию.
    ///    Если это свойство не задано, используется значение <see langword="None" />.
    /// </summary>
    /// <returns>
    ///   Ключевые слова, применяемые к событию, или <see langword="None" />, если ключевые слова не заданы.
    /// </returns>
    [__DynamicallyInvokable]
    public EventKeywords Keywords
    {
      [__DynamicallyInvokable] get
      {
        return this.keywords;
      }
      [__DynamicallyInvokable] set
      {
        this.keywords = value;
        this.valuesSet |= (byte) 1;
      }
    }

    /// <summary>
    ///   Теги событий, определенные для этого источника событий.
    /// </summary>
    /// <returns>
    ///   Возвращает <see cref="T:System.Diagnostics.Tracing.EventTags" />.
    /// </returns>
    [__DynamicallyInvokable]
    public EventTags Tags
    {
      [__DynamicallyInvokable] get
      {
        return this.tags;
      }
      [__DynamicallyInvokable] set
      {
        this.tags = value;
        this.valuesSet |= (byte) 2;
      }
    }

    /// <summary>
    ///   Параметры действий, определенные для данного источника событий.
    /// </summary>
    /// <returns>
    ///   Возвращает <see cref="T:System.Diagnostics.Tracing.EventActivityOptions" />.
    /// </returns>
    [__DynamicallyInvokable]
    public EventActivityOptions ActivityOptions
    {
      [__DynamicallyInvokable] get
      {
        return this.activityOptions;
      }
      [__DynamicallyInvokable] set
      {
        this.activityOptions = value;
        this.valuesSet |= (byte) 16;
      }
    }
  }
}
