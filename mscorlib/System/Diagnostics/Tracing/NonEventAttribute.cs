// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.NonEventAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Diagnostics.Tracing
{
  /// <summary>Определяет метод, который не создает событие.</summary>
  [AttributeUsage(AttributeTargets.Method)]
  [__DynamicallyInvokable]
  public sealed class NonEventAttribute : Attribute
  {
    /// <summary>
    ///   Создает новый экземпляр класса <see cref="T:System.Diagnostics.Tracing.NonEventAttribute" />.
    /// </summary>
    [__DynamicallyInvokable]
    public NonEventAttribute()
    {
    }
  }
}
