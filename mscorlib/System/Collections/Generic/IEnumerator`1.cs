// Decompiled with JetBrains decompiler
// Type: System.Collections.Generic.IEnumerator`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Collections.Generic
{
  /// <summary>
  ///   Поддерживает простой перебор элементов универсальной коллекции.
  /// </summary>
  /// <typeparam name="T">Тип объектов для перечисления.</typeparam>
  [__DynamicallyInvokable]
  public interface IEnumerator<out T> : IDisposable, IEnumerator
  {
    /// <summary>
    ///   Возвращает элемент коллекции, соответствующий текущей позиции перечислителя.
    /// </summary>
    /// <returns>
    ///   Элемент коллекции, соответствующий текущей позиции перечислителя.
    /// </returns>
    [__DynamicallyInvokable]
    T Current { [__DynamicallyInvokable] get; }
  }
}
