// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.EventFieldAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Diagnostics.Tracing
{
  /// <summary>
  ///   <see cref="T:System.Diagnostics.Tracing.EventFieldAttribute" /> Помещается на определяемые пользователем типы, которые передаются в качестве поля <see cref="T:System.Diagnostics.Tracing.EventSource" /> полезных данных.
  /// </summary>
  [AttributeUsage(AttributeTargets.Property)]
  [__DynamicallyInvokable]
  public class EventFieldAttribute : Attribute
  {
    /// <summary>
    ///   Возвращает и задает определяемую пользователем <see cref="T:System.Diagnostics.Tracing.EventFieldTags" /> значение, которое требуется для полей, содержащих данные, не поддерживаемым типам.
    /// </summary>
    /// <returns>
    ///   Возвращает <see cref="T:System.Diagnostics.Tracing.EventFieldTags" />.
    /// </returns>
    [__DynamicallyInvokable]
    public EventFieldTags Tags { [__DynamicallyInvokable] get; [__DynamicallyInvokable] set; }

    internal string Name { get; set; }

    /// <summary>
    ///   Возвращает и задает значение, определяющее способ форматирования значения определяемого пользователем типа.
    /// </summary>
    /// <returns>
    ///   Возвращает<see cref="T:System.Diagnostics.Tracing.EventFieldFormat" /> значение.
    /// </returns>
    [__DynamicallyInvokable]
    public EventFieldFormat Format { [__DynamicallyInvokable] get; [__DynamicallyInvokable] set; }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Diagnostics.Tracing.EventFieldAttribute" />.
    /// </summary>
    [__DynamicallyInvokable]
    public EventFieldAttribute()
    {
    }
  }
}
