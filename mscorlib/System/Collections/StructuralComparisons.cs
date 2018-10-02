// Decompiled with JetBrains decompiler
// Type: System.Collections.StructuralComparisons
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Collections
{
  /// <summary>
  ///   Предоставляет объекты для структурного сравнения двух объектов коллекции.
  /// </summary>
  [__DynamicallyInvokable]
  public static class StructuralComparisons
  {
    private static volatile IComparer s_StructuralComparer;
    private static volatile IEqualityComparer s_StructuralEqualityComparer;

    /// <summary>
    ///   Получает предопределенный объект, выполняющий структурное сравнение двух объектов.
    /// </summary>
    /// <returns>
    ///   Предопределенный объект, который используется для выполнения структурного сравнения двух объектов коллекции.
    /// </returns>
    [__DynamicallyInvokable]
    public static IComparer StructuralComparer
    {
      [__DynamicallyInvokable] get
      {
        IComparer comparer = StructuralComparisons.s_StructuralComparer;
        if (comparer == null)
        {
          comparer = (IComparer) new System.Collections.StructuralComparer();
          StructuralComparisons.s_StructuralComparer = comparer;
        }
        return comparer;
      }
    }

    /// <summary>
    ///   Получает предопределенный объект, который сравнивает два объекта на предмет структурного равенства.
    /// </summary>
    /// <returns>
    ///   Предопределенный объект, который используется для сравнения двух объектов коллекции на предмет структурного равенства.
    /// </returns>
    [__DynamicallyInvokable]
    public static IEqualityComparer StructuralEqualityComparer
    {
      [__DynamicallyInvokable] get
      {
        IEqualityComparer equalityComparer = StructuralComparisons.s_StructuralEqualityComparer;
        if (equalityComparer == null)
        {
          equalityComparer = (IEqualityComparer) new System.Collections.StructuralEqualityComparer();
          StructuralComparisons.s_StructuralEqualityComparer = equalityComparer;
        }
        return equalityComparer;
      }
    }
  }
}
