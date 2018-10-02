// Decompiled with JetBrains decompiler
// Type: System.Collections.Generic.EqualityComparer`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Security;

namespace System.Collections.Generic
{
  /// <summary>
  ///   Предоставляет базовый класс для реализаций <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> универсальный интерфейс.
  /// </summary>
  /// <typeparam name="T">Тип объектов для сравнения.</typeparam>
  [TypeDependency("System.Collections.Generic.ObjectEqualityComparer`1")]
  [__DynamicallyInvokable]
  [Serializable]
  public abstract class EqualityComparer<T> : IEqualityComparer, IEqualityComparer<T>
  {
    private static readonly EqualityComparer<T> defaultComparer = EqualityComparer<T>.CreateComparer();

    /// <summary>
    ///   Возвращает компаратор для типа, указанного универсальным аргументом.
    /// </summary>
    /// <returns>
    ///   Экземпляр по умолчанию <see cref="T:System.Collections.Generic.EqualityComparer`1" /> класса для типа <paramref name="T" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static EqualityComparer<T> Default
    {
      [__DynamicallyInvokable] get
      {
        return EqualityComparer<T>.defaultComparer;
      }
    }

    [SecuritySafeCritical]
    private static EqualityComparer<T> CreateComparer()
    {
      RuntimeType genericParameter = (RuntimeType) typeof (T);
      if ((Type) genericParameter == typeof (byte))
        return (EqualityComparer<T>) new ByteEqualityComparer();
      if (typeof (IEquatable<T>).IsAssignableFrom((Type) genericParameter))
        return (EqualityComparer<T>) RuntimeTypeHandle.CreateInstanceForAnotherGenericParameter((RuntimeType) typeof (GenericEqualityComparer<int>), genericParameter);
      if (genericParameter.IsGenericType && genericParameter.GetGenericTypeDefinition() == typeof (Nullable<>))
      {
        RuntimeType genericArgument = (RuntimeType) genericParameter.GetGenericArguments()[0];
        if (typeof (IEquatable<>).MakeGenericType((Type) genericArgument).IsAssignableFrom((Type) genericArgument))
          return (EqualityComparer<T>) RuntimeTypeHandle.CreateInstanceForAnotherGenericParameter((RuntimeType) typeof (NullableEqualityComparer<int>), genericArgument);
      }
      if (genericParameter.IsEnum)
      {
        switch (Type.GetTypeCode(Enum.GetUnderlyingType((Type) genericParameter)))
        {
          case TypeCode.SByte:
            return (EqualityComparer<T>) RuntimeTypeHandle.CreateInstanceForAnotherGenericParameter((RuntimeType) typeof (SByteEnumEqualityComparer<sbyte>), genericParameter);
          case TypeCode.Byte:
          case TypeCode.UInt16:
          case TypeCode.Int32:
          case TypeCode.UInt32:
            return (EqualityComparer<T>) RuntimeTypeHandle.CreateInstanceForAnotherGenericParameter((RuntimeType) typeof (EnumEqualityComparer<int>), genericParameter);
          case TypeCode.Int16:
            return (EqualityComparer<T>) RuntimeTypeHandle.CreateInstanceForAnotherGenericParameter((RuntimeType) typeof (ShortEnumEqualityComparer<short>), genericParameter);
          case TypeCode.Int64:
          case TypeCode.UInt64:
            return (EqualityComparer<T>) RuntimeTypeHandle.CreateInstanceForAnotherGenericParameter((RuntimeType) typeof (LongEnumEqualityComparer<long>), genericParameter);
        }
      }
      return (EqualityComparer<T>) new ObjectEqualityComparer<T>();
    }

    /// <summary>
    ///   При переопределении в производном классе определяет, является ли два объекта типа <paramref name="T" /> равны.
    /// </summary>
    /// <param name="x">Первый из сравниваемых объектов.</param>
    /// <param name="y">Второй из сравниваемых объектов.</param>
    /// <returns>
    ///   <see langword="true" />, если указанные объекты равны; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public abstract bool Equals(T x, T y);

    /// <summary>
    ///   При переопределении в производном классе служит хэш-функцией для указанного объекта для алгоритмов хэширования и структур данных, например хэш-таблицы.
    /// </summary>
    /// <param name="obj">
    ///   Объект, для которого нужно получить хэш-код.
    /// </param>
    /// <returns>Хэш-код указанного объекта.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Тип <paramref name="obj" /> является ссылочным типом и <paramref name="obj" /> — <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public abstract int GetHashCode(T obj);

    internal virtual int IndexOf(T[] array, T value, int startIndex, int count)
    {
      int num = startIndex + count;
      for (int index = startIndex; index < num; ++index)
      {
        if (this.Equals(array[index], value))
          return index;
      }
      return -1;
    }

    internal virtual int LastIndexOf(T[] array, T value, int startIndex, int count)
    {
      int num = startIndex - count + 1;
      for (int index = startIndex; index >= num; --index)
      {
        if (this.Equals(array[index], value))
          return index;
      }
      return -1;
    }

    [__DynamicallyInvokable]
    int IEqualityComparer.GetHashCode(object obj)
    {
      if (obj == null)
        return 0;
      if (obj is T)
        return this.GetHashCode((T) obj);
      ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArgumentForComparison);
      return 0;
    }

    [__DynamicallyInvokable]
    bool IEqualityComparer.Equals(object x, object y)
    {
      if (x == y)
        return true;
      if (x == null || y == null)
        return false;
      if (x is T && y is T)
        return this.Equals((T) x, (T) y);
      ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArgumentForComparison);
      return false;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Collections.Generic.EqualityComparer`1" />.
    /// </summary>
    [__DynamicallyInvokable]
    protected EqualityComparer()
    {
    }
  }
}
