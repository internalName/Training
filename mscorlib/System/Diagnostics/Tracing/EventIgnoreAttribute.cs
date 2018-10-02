// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.EventIgnoreAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Diagnostics.Tracing
{
  /// <summary>
  ///   Указывает свойство должно игнорироваться при записи типа события с <see cref="M:System.Diagnostics.Tracing.EventSource.Write``1(System.String,System.Diagnostics.Tracing.EventSourceOptions@,``0@)" /> метод.
  /// </summary>
  [AttributeUsage(AttributeTargets.Property)]
  [__DynamicallyInvokable]
  public class EventIgnoreAttribute : Attribute
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Diagnostics.Tracing.EventIgnoreAttribute" />.
    /// </summary>
    [__DynamicallyInvokable]
    public EventIgnoreAttribute()
    {
    }
  }
}
