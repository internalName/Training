// Decompiled with JetBrains decompiler
// Type: System.Tuple`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace System
{
  /// <summary>Представляет кортеж из одного компонента.</summary>
  /// <typeparam name="T1">
  ///   Тип единственного компонента кортежа.
  /// </typeparam>
  [__DynamicallyInvokable]
  [Serializable]
  public class Tuple<T1> : IStructuralEquatable, IStructuralComparable, IComparable, ITupleInternal, ITuple
  {
    private readonly T1 m_Item1;

    /// <summary>
    ///   Возвращает значение <see cref="T:System.Tuple`1" /> отдельного компонента объекта.
    /// </summary>
    /// <returns>
    ///   Значение текущего <see cref="T:System.Tuple`1" /> отдельного компонента объекта.
    /// </returns>
    [__DynamicallyInvokable]
    public T1 Item1
    {
      [__DynamicallyInvokable] get
      {
        return this.m_Item1;
      }
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Tuple`1" />.
    /// </summary>
    /// <param name="item1">
    ///   Значение единственного компонента кортежа.
    /// </param>
    [__DynamicallyInvokable]
    public Tuple(T1 item1)
    {
      this.m_Item1 = item1;
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли текущий <see cref="T:System.Tuple`1" /> объект равен указанному объекту.
    /// </summary>
    /// <param name="obj">
    ///   Объект, сравниваемый с данным экземпляром.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если текущий экземпляр равен заданному объекту; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public override bool Equals(object obj)
    {
      return ((IStructuralEquatable) this).Equals(obj, (IEqualityComparer) EqualityComparer<object>.Default);
    }

    [__DynamicallyInvokable]
    bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
    {
      if (other == null)
        return false;
      Tuple<T1> tuple = other as Tuple<T1>;
      if (tuple == null)
        return false;
      return comparer.Equals((object) this.m_Item1, (object) tuple.m_Item1);
    }

    [__DynamicallyInvokable]
    int IComparable.CompareTo(object obj)
    {
      return ((IStructuralComparable) this).CompareTo(obj, (IComparer) Comparer<object>.Default);
    }

    [__DynamicallyInvokable]
    int IStructuralComparable.CompareTo(object other, IComparer comparer)
    {
      if (other == null)
        return 1;
      Tuple<T1> tuple = other as Tuple<T1>;
      if (tuple == null)
        throw new ArgumentException(Environment.GetResourceString("ArgumentException_TupleIncorrectType", (object) this.GetType().ToString()), nameof (other));
      return comparer.Compare((object) this.m_Item1, (object) tuple.m_Item1);
    }

    /// <summary>
    ///   Возвращает хэш-код для текущего объекта <see cref="T:System.Tuple`1" />.
    /// </summary>
    /// <returns>
    ///   Хэш-код в виде 32-разрядного целого числа со знаком.
    /// </returns>
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      return ((IStructuralEquatable) this).GetHashCode((IEqualityComparer) EqualityComparer<object>.Default);
    }

    [__DynamicallyInvokable]
    int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
    {
      return comparer.GetHashCode((object) this.m_Item1);
    }

    int ITupleInternal.GetHashCode(IEqualityComparer comparer)
    {
      return ((IStructuralEquatable) this).GetHashCode(comparer);
    }

    /// <summary>
    ///   Возвращает строковое представление значения этого экземпляра <see cref="T:System.Tuple`1" />.
    /// </summary>
    /// <returns>
    ///   Строковое представление конкретного объекта <see cref="T:System.Tuple`1" />.
    /// </returns>
    [__DynamicallyInvokable]
    public override string ToString()
    {
      StringBuilder sb = new StringBuilder();
      sb.Append("(");
      return ((ITupleInternal) this).ToString(sb);
    }

    string ITupleInternal.ToString(StringBuilder sb)
    {
      sb.Append((object) this.m_Item1);
      sb.Append(")");
      return sb.ToString();
    }

    int ITuple.Length
    {
      get
      {
        return 1;
      }
    }

    object ITuple.this[int index]
    {
      get
      {
        if (index != 0)
          throw new IndexOutOfRangeException();
        return (object) this.Item1;
      }
    }
  }
}
