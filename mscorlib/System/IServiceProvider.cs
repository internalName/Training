// Decompiled with JetBrains decompiler
// Type: System.IServiceProvider
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System
{
  /// <summary>
  ///   Определяет механизм для извлечения объекта службы, т. е. объекта, обеспечивающего настраиваемую поддержку для других объектов.
  /// </summary>
  [__DynamicallyInvokable]
  public interface IServiceProvider
  {
    /// <summary>Возвращает объект службы указанного типа.</summary>
    /// <param name="serviceType">
    ///   Объект, определяющий тип объекта службы, который необходимо получить.
    /// </param>
    /// <returns>
    ///   Объект службы типа <paramref name="serviceType" />.
    /// 
    ///   -или-
    /// 
    ///   Значение <see langword="null" />, если отсутствует объект службы типа <paramref name="serviceType" />.
    /// </returns>
    [__DynamicallyInvokable]
    object GetService(Type serviceType);
  }
}
