// Decompiled with JetBrains decompiler
// Type: System.Collections.Generic.IEnumerable`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
  /// <summary>
  ///   Предоставляет перечислитель, который поддерживает простой перебор элементов в указанной коллекции.
  /// 
  ///   Просмотреть исходный код .NET Framework для этого типа Reference Source.
  /// </summary>
  /// <typeparam name="T">Тип объектов для перечисления.</typeparam>
  [TypeDependency("System.SZArrayHelper")]
  [__DynamicallyInvokable]
  public interface IEnumerable<out T> : IEnumerable
  {
    /// <summary>
    ///   Возвращает перечислитель, выполняющий перебор элементов в коллекции.
    /// </summary>
    /// <returns>
    ///   Перечислитель, который можно использовать для итерации по коллекции.
    /// </returns>
    [__DynamicallyInvokable]
    IEnumerator<T> GetEnumerator();
  }
}
