// Decompiled with JetBrains decompiler
// Type: System.ValueType
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace System
{
  /// <summary>Предоставляет базовый класс для типов значений.</summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public abstract class ValueType
  {
    /// <summary>
    ///   Указывает, равен ли этот экземпляр заданному объекту.
    /// </summary>
    /// <param name="obj">
    ///   Объект для сравнения с текущим экземпляром.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если <paramref name="obj" /> и данный экземпляр относятся к одному типу и представляют одинаковые значения; в противном случае — значение <see langword="false" />.
    /// </returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public override bool Equals(object obj)
    {
      if (obj == null)
        return false;
      RuntimeType type = (RuntimeType) this.GetType();
      if ((RuntimeType) obj.GetType() != type)
        return false;
      object a = (object) this;
      if (ValueType.CanCompareBits((object) this))
        return ValueType.FastEqualsCheck(a, obj);
      FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
      for (int index = 0; index < fields.Length; ++index)
      {
        object obj1 = ((RtFieldInfo) fields[index]).UnsafeGetValue(a);
        object obj2 = ((RtFieldInfo) fields[index]).UnsafeGetValue(obj);
        if (obj1 == null)
        {
          if (obj2 != null)
            return false;
        }
        else if (!obj1.Equals(obj2))
          return false;
      }
      return true;
    }

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern bool CanCompareBits(object obj);

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern bool FastEqualsCheck(object a, object b);

    /// <summary>Возвращает хэш-код данного экземпляра.</summary>
    /// <returns>
    ///   32-разрядное целое число со знаком, являющееся хэш-кодом для данного экземпляра.
    /// </returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public override extern int GetHashCode();

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern int GetHashCodeOfPtr(IntPtr ptr);

    /// <summary>Возвращает полное имя типа этого экземпляра.</summary>
    /// <returns>Полное имя типа.</returns>
    [__DynamicallyInvokable]
    public override string ToString()
    {
      return this.GetType().ToString();
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.ValueType" />.
    /// </summary>
    [__DynamicallyInvokable]
    protected ValueType()
    {
    }
  }
}
