// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ArrayWithOffset
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices
{
  /// <summary>Инкапсулирует массив и смещение в указанный массив.</summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public struct ArrayWithOffset
  {
    private object m_array;
    private int m_offset;
    private int m_count;

    /// <summary>
    ///   Инициализирует новый экземпляр структуры <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" />.
    /// </summary>
    /// <param name="array">Управляемый массив.</param>
    /// <param name="offset">
    ///   Смещение в байтах, передаваемое с помощью элемента вызова неуправляемого кода.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Массив является размером более 2 гигабайт (ГБ).
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public ArrayWithOffset(object array, int offset)
    {
      this.m_array = array;
      this.m_offset = offset;
      this.m_count = 0;
      this.m_count = this.CalculateCount();
    }

    /// <summary>
    ///   Возвращает управляемый массив, упоминаемой в этом <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" />.
    /// </summary>
    /// <returns>
    ///   Управляемый массив, который ссылается данный экземпляр.
    /// </returns>
    [__DynamicallyInvokable]
    public object GetArray()
    {
      return this.m_array;
    }

    /// <summary>
    ///   Возвращает смещение, предоставленное при этом <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> был создан.
    /// </summary>
    /// <returns>Смещение для этого экземпляра.</returns>
    [__DynamicallyInvokable]
    public int GetOffset()
    {
      return this.m_offset;
    }

    /// <summary>Возвращает хэш-код для этого типа значения.</summary>
    /// <returns>Хэш-код данного экземпляра.</returns>
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      return this.m_count + this.m_offset;
    }

    /// <summary>
    ///   Указывает, соответствует ли указанный объект текущему <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> объекта.
    /// </summary>
    /// <param name="obj">
    ///   Объект, сравниваемый с данным экземпляром.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если объект соответствует этому <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" />; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public override bool Equals(object obj)
    {
      if (obj is ArrayWithOffset)
        return this.Equals((ArrayWithOffset) obj);
      return false;
    }

    /// <summary>
    ///   Указывает, является ли указанный <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> объект совпадает с текущим экземпляром.
    /// </summary>
    /// <param name="obj">
    ///   Ключ <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" />, с которым сравнивается этот экземпляр.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если указанный <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> объект совпадает с текущим экземпляром; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool Equals(ArrayWithOffset obj)
    {
      if (obj.m_array == this.m_array && obj.m_offset == this.m_offset)
        return obj.m_count == this.m_count;
      return false;
    }

    /// <summary>
    ///   Определяет, совпадают ли значения двух указанных объектов <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" />.
    /// </summary>
    /// <param name="a">
    ///   <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> Объект для сравнения с <paramref name="b" /> параметр.
    /// </param>
    /// <param name="b">
    ///   <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> Объект для сравнения с <paramref name="a" /> параметр.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если значение параметра <paramref name="a" /> совпадает со значением <paramref name="b" />; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool operator ==(ArrayWithOffset a, ArrayWithOffset b)
    {
      return a.Equals(b);
    }

    /// <summary>
    ///   Определяет, являются ли два заданных <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> объектов не имеет то же значение.
    /// </summary>
    /// <param name="a">
    ///   <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> Объект для сравнения с <paramref name="b" /> параметр.
    /// </param>
    /// <param name="b">
    ///   <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> Объект для сравнения с <paramref name="a" /> параметр.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если значение <paramref name="a" /> не является таким же, как значение <paramref name="b" />; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool operator !=(ArrayWithOffset a, ArrayWithOffset b)
    {
      return !(a == b);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private extern int CalculateCount();
  }
}
