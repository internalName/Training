// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.EventSourceAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Diagnostics.Tracing
{
  /// <summary>
  ///   Позволяет определить имя трассировки событий Windows (ETW) независимо от имени класса источника событий.
  /// </summary>
  [AttributeUsage(AttributeTargets.Class)]
  [__DynamicallyInvokable]
  public sealed class EventSourceAttribute : Attribute
  {
    /// <summary>Возвращает или задает имя источника события.</summary>
    /// <returns>Имя источника событий.</returns>
    [__DynamicallyInvokable]
    public string Name { [__DynamicallyInvokable] get; [__DynamicallyInvokable] set; }

    /// <summary>
    ///   Возвращает или задает идентификатор источника события.
    /// </summary>
    /// <returns>Идентификатор источника события.</returns>
    [__DynamicallyInvokable]
    public string Guid { [__DynamicallyInvokable] get; [__DynamicallyInvokable] set; }

    /// <summary>
    ///   Возвращает или задает имя файла ресурсов для локализации.
    /// </summary>
    /// <returns>
    ///   Имя файла ресурсов для локализации, или <see langword="null" /> при локализации файла ресурсов не существует.
    /// </returns>
    [__DynamicallyInvokable]
    public string LocalizationResources { [__DynamicallyInvokable] get; [__DynamicallyInvokable] set; }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Diagnostics.Tracing.EventSourceAttribute" />.
    /// </summary>
    [__DynamicallyInvokable]
    public EventSourceAttribute()
    {
    }
  }
}
