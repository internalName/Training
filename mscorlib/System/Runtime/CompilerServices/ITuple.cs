// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.ITuple
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.CompilerServices
{
  /// <summary>
  ///   Определяет реализацию кортежа общего назначения, предоставляющую доступ к членам экземпляра кортежа без сведений о базовом типе кортежа.
  /// </summary>
  public interface ITuple
  {
    /// <summary>
    ///   Получает число элементов в экземпляре <see langword="Tuple" />.
    /// </summary>
    /// <returns>
    ///   Количество элементов в этом экземпляре <see langword="Tuple" />.
    /// </returns>
    int Length { get; }

    /// <summary>
    ///   Возвращает значение указанного элемента <see langword="Tuple" />.
    /// </summary>
    /// <param name="index">
    ///   Индекс указанного элемента <see langword="Tuple" />.
    ///   <paramref name="index" /> может находиться в диапазоне от 0 для <see langword="Item1" /><see langword="Tuple" /> до значения, на единицу меньше, чем число элементов в <see langword="Tuple" />.
    /// </param>
    /// <returns>
    ///   Значение указанного элемента <see langword="Tuple" />.
    /// </returns>
    object this[int index] { get; }
  }
}
