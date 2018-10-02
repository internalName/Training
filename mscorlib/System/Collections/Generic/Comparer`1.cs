// Decompiled with JetBrains decompiler
// Type: System.Collections.Generic.Comparer`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Security;

namespace System.Collections.Generic
{
  /// <summary>
  ///   Предоставляет базовый класс для реализаций <see cref="T:System.Collections.Generic.IComparer`1" /> универсальный интерфейс.
  /// </summary>
  /// <typeparam name="T">Тип объектов для сравнения.</typeparam>
  [TypeDependency("System.Collections.Generic.ObjectComparer`1")]
  [__DynamicallyInvokable]
  [Serializable]
  public abstract class Comparer<T> : IComparer, IComparer<T>
  {
    private static readonly Comparer<T> defaultComparer = Comparer<T>.CreateComparer();

    /// <summary>
    ///   Возвращает по умолчанию сортировки порядок сравнения для типа, указанного универсальным аргументом.
    /// </summary>
    /// <returns>
    ///   Объект, который наследует <see cref="T:System.Collections.Generic.Comparer`1" /> и служит в качестве средства сравнения порядка сортировки для типа <paramref name="T" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static Comparer<T> Default
    {
      [__DynamicallyInvokable] get
      {
        return Comparer<T>.defaultComparer;
      }
    }

    /// <summary>
    ///   Создает блок сравнения с использованием указанного сравнения.
    /// </summary>
    /// <param name="comparison">Сравнение.</param>
    /// <returns>Новые функции сравнения.</returns>
    [__DynamicallyInvokable]
    public static Comparer<T> Create(Comparison<T> comparison)
    {
      if (comparison == null)
        throw new ArgumentNullException(nameof (comparison));
      return (Comparer<T>) new ComparisonComparer<T>(comparison);
    }

    [SecuritySafeCritical]
    private static Comparer<T> CreateComparer()
    {
      RuntimeType genericParameter = (RuntimeType) typeof (T);
      if (typeof (IComparable<T>).IsAssignableFrom((Type) genericParameter))
        return (Comparer<T>) RuntimeTypeHandle.CreateInstanceForAnotherGenericParameter((RuntimeType) typeof (GenericComparer<int>), genericParameter);
      if (genericParameter.IsGenericType && genericParameter.GetGenericTypeDefinition() == typeof (Nullable<>))
      {
        RuntimeType genericArgument = (RuntimeType) genericParameter.GetGenericArguments()[0];
        if (typeof (IComparable<>).MakeGenericType((Type) genericArgument).IsAssignableFrom((Type) genericArgument))
          return (Comparer<T>) RuntimeTypeHandle.CreateInstanceForAnotherGenericParameter((RuntimeType) typeof (NullableComparer<int>), genericArgument);
      }
      return (Comparer<T>) new ObjectComparer<T>();
    }

    /// <summary>
    ///   При переопределении в производном классе выполняет сравнение двух объектов одного типа и возвращает значение, указывающее, является ли один объект меньше, равен или больше другого.
    /// </summary>
    /// <param name="x">Первый из сравниваемых объектов.</param>
    /// <param name="y">Второй из сравниваемых объектов.</param>
    /// <returns>
    /// Знаковое целое число, которое определяет относительные значения параметров <paramref name="x" /> и <paramref name="y" />, как показано в следующей таблице.
    /// 
    ///         Значение
    /// 
    ///         Значение
    /// 
    ///         Меньше нуля
    /// 
    ///         Значение <paramref name="x" /> меньше <paramref name="y" />.
    /// 
    ///         Нуль
    /// 
    ///         <paramref name="x" /> равняется <paramref name="y" />.
    /// 
    ///         Больше нуля
    /// 
    ///         Значение <paramref name="x" /> больше значения <paramref name="y" />.
    ///       </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Тип <paramref name="T" /> не реализует либо <see cref="T:System.IComparable`1" /> универсальный интерфейс или <see cref="T:System.IComparable" /> интерфейса.
    /// </exception>
    [__DynamicallyInvokable]
    public abstract int Compare(T x, T y);

    [__DynamicallyInvokable]
    int IComparer.Compare(object x, object y)
    {
      if (x == null)
        return y != null ? -1 : 0;
      if (y == null)
        return 1;
      if (x is T && y is T)
        return this.Compare((T) x, (T) y);
      ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArgumentForComparison);
      return 0;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Collections.Generic.Comparer`1" />.
    /// </summary>
    [__DynamicallyInvokable]
    protected Comparer()
    {
    }
  }
}
