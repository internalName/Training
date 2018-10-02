// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.OnDeserializedAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
  /// <summary>
  ///   При применении к методу указывает, что метод вызывается сразу после десериализации объекта в графе объекта.
  ///    Порядок десериализации относительно других объектов в графе является недетерминированным.
  /// </summary>
  [AttributeUsage(AttributeTargets.Method, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class OnDeserializedAttribute : Attribute
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.Serialization.OnDeserializedAttribute" />.
    /// </summary>
    [__DynamicallyInvokable]
    public OnDeserializedAttribute()
    {
    }
  }
}
