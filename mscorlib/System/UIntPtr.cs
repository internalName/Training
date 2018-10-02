// Decompiled with JetBrains decompiler
// Type: System.UIntPtr
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Versioning;
using System.Security;

namespace System
{
  /// <summary>
  ///   Определяемый платформой тип, который используется для представления указателя или дескриптора.
  /// </summary>
  [CLSCompliant(false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public struct UIntPtr : ISerializable
  {
    [SecurityCritical]
    private unsafe void* m_value;
    /// <summary>
    ///   Доступное только для чтения поле, которое предоставляет указатель или дескриптор, инициализированный с нулевым значением.
    /// </summary>
    public static readonly UIntPtr Zero;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.UIntPtr" /> структуры с помощью указанного 32-разрядного указателя или дескриптора.
    /// </summary>
    /// <param name="value">
    ///   Указатель или дескриптор состоит из 32-разрядного целого числа.
    /// </param>
    [SecuritySafeCritical]
    [NonVersionable]
    [__DynamicallyInvokable]
    public unsafe UIntPtr(uint value)
    {
      this.m_value = (void*) value;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.UIntPtr" /> с помощью указанного 64-разрядного указателя или дескриптора.
    /// </summary>
    /// <param name="value">
    ///   Указатель или дескриптор, содержащийся в 64-разрядное целое число без знака.
    /// </param>
    /// <exception cref="T:System.OverflowException">
    ///   На 32-разрядной платформе <paramref name="value" /> слишком велико для представления в качестве <see cref="T:System.UIntPtr" />.
    /// </exception>
    [SecuritySafeCritical]
    [NonVersionable]
    [__DynamicallyInvokable]
    public unsafe UIntPtr(ulong value)
    {
      this.m_value = (void*) checked ((uint) value);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр структуры <see cref="T:System.UIntPtr" /> с использованием заданного указателя на незаданный тип.
    /// </summary>
    /// <param name="value">Указатель незаданного типа.</param>
    [SecurityCritical]
    [CLSCompliant(false)]
    [NonVersionable]
    public unsafe UIntPtr(void* value)
    {
      this.m_value = value;
    }

    [SecurityCritical]
    private unsafe UIntPtr(SerializationInfo info, StreamingContext context)
    {
      ulong uint64 = info.GetUInt64("value");
      if (UIntPtr.Size == 4 && uint64 > (ulong) uint.MaxValue)
        throw new ArgumentException(Environment.GetResourceString("Serialization_InvalidPtrValue"));
      this.m_value = (void*) uint64;
    }

    [SecurityCritical]
    unsafe void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException(nameof (info));
      info.AddValue("value", (ulong) this.m_value);
    }

    /// <summary>
    ///   Возвращает значение, показывающее, равен ли данный экземпляр заданному объекту.
    /// </summary>
    /// <param name="obj">
    ///   Объект, сравниваемый с этим экземпляром или <see langword="null" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если параметр <paramref name="obj" /> является экземпляром типа <see cref="T:System.UIntPtr" /> и равен значению данного экземпляра; в противном случае — значение <see langword="false" />.
    /// </returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public override unsafe bool Equals(object obj)
    {
      if (obj is UIntPtr)
        return this.m_value == ((UIntPtr) obj).m_value;
      return false;
    }

    /// <summary>Возвращает хэш-код данного экземпляра.</summary>
    /// <returns>
    ///   Хэш-код в виде 32-разрядного целого числа со знаком.
    /// </returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public override unsafe int GetHashCode()
    {
      return (int) this.m_value & int.MaxValue;
    }

    /// <summary>
    ///   Преобразует значение данного экземпляра в 32-разрядное целое число без знака.
    /// </summary>
    /// <returns>
    ///   32-разрядное целое число без знака равно значению данного экземпляра.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   На 64-разрядной платформе значение этого экземпляра слишком велик для представления в качестве 32-разрядного целого числа.
    /// </exception>
    [SecuritySafeCritical]
    [NonVersionable]
    [__DynamicallyInvokable]
    public unsafe uint ToUInt32()
    {
      return (uint) this.m_value;
    }

    /// <summary>
    ///   Преобразует значение этого экземпляра в 64-разрядное целое число без знака.
    /// </summary>
    /// <returns>
    ///   64-разрядное целое число без знака равно значению данного экземпляра.
    /// </returns>
    [SecuritySafeCritical]
    [NonVersionable]
    [__DynamicallyInvokable]
    public unsafe ulong ToUInt64()
    {
      return (ulong) this.m_value;
    }

    /// <summary>
    ///   Преобразовывает числовое значение данного экземпляра в эквивалентное ему строковое представление.
    /// </summary>
    /// <returns>Строковое представление значения этого экземпляра.</returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public override unsafe string ToString()
    {
      return ((uint) this.m_value).ToString((IFormatProvider) CultureInfo.InvariantCulture);
    }

    /// <summary>
    ///   Преобразует значение 32-разрядного целого числа для <see cref="T:System.UIntPtr" />.
    /// </summary>
    /// <param name="value">32-разрядное целое число без знака.</param>
    /// <returns>
    ///   Новый экземпляр <see cref="T:System.UIntPtr" />, инициализированный значением <paramref name="value" />.
    /// </returns>
    [NonVersionable]
    public static explicit operator UIntPtr(uint value)
    {
      return new UIntPtr(value);
    }

    /// <summary>
    ///   Преобразует значение 64-разрядного целого числа для <see cref="T:System.UIntPtr" />.
    /// </summary>
    /// <param name="value">64-разрядное целое число без знака.</param>
    /// <returns>
    ///   Новый экземпляр <see cref="T:System.UIntPtr" />, инициализированный значением <paramref name="value" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   На 32-разрядной платформе <paramref name="value" /> слишком велико для представления в качестве <see cref="T:System.UIntPtr" />.
    /// </exception>
    [NonVersionable]
    public static explicit operator UIntPtr(ulong value)
    {
      return new UIntPtr(value);
    }

    /// <summary>
    ///   Преобразует значение заданного объекта <see cref="T:System.UIntPtr" /> для 32-разрядное целое число без знака.
    /// </summary>
    /// <param name="value">
    ///   Указатель или дескриптор, подлежащий преобразованию.
    /// </param>
    /// <returns>
    ///   Содержимое <paramref name="value" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   На 64-разрядной платформе, значение <paramref name="value" /> слишком велико для представления в качестве 32-разрядного целого числа.
    /// </exception>
    [SecuritySafeCritical]
    [NonVersionable]
    public static unsafe explicit operator uint(UIntPtr value)
    {
      return (uint) value.m_value;
    }

    /// <summary>
    ///   Преобразует значение заданного объекта <see cref="T:System.UIntPtr" /> для 64-разрядное целое число без знака.
    /// </summary>
    /// <param name="value">
    ///   Указатель или дескриптор, подлежащий преобразованию.
    /// </param>
    /// <returns>
    ///   Содержимое <paramref name="value" />.
    /// </returns>
    [SecuritySafeCritical]
    [NonVersionable]
    public static unsafe explicit operator ulong(UIntPtr value)
    {
      return (ulong) value.m_value;
    }

    /// <summary>
    ///   Преобразует заданный указатель на незаданный тип <see cref="T:System.UIntPtr" />.
    /// </summary>
    /// <param name="value">Указатель незаданного типа.</param>
    /// <returns>
    ///   Новый экземпляр <see cref="T:System.UIntPtr" />, инициализированный значением <paramref name="value" />.
    /// </returns>
    [SecurityCritical]
    [CLSCompliant(false)]
    [NonVersionable]
    public static unsafe explicit operator UIntPtr(void* value)
    {
      return new UIntPtr(value);
    }

    /// <summary>
    ///   Преобразует значение заданной структуры <see cref="T:System.UIntPtr" /> в указатель на незаданный тип.
    /// </summary>
    /// <param name="value">
    ///   Указатель или дескриптор, подлежащий преобразованию.
    /// </param>
    /// <returns>
    ///   Содержимое <paramref name="value" />.
    /// </returns>
    [SecurityCritical]
    [CLSCompliant(false)]
    [NonVersionable]
    public static unsafe explicit operator void*(UIntPtr value)
    {
      return value.m_value;
    }

    /// <summary>
    ///   Определяет, равны ли два заданных экземпляра класса <see cref="T:System.UIntPtr" />.
    /// </summary>
    /// <param name="value1">
    ///   Первый из сравниваемых указателей или дескрипторов.
    /// </param>
    /// <param name="value2">
    ///   Второй из сравниваемых указателей или дескрипторов.
    /// </param>
    /// <returns>
    ///   Если значение <paramref name="value1" /> равно <paramref name="value2" />, значение <see langword="true" />; в противном случае — значение <see langword="false" />.
    /// </returns>
    [SecuritySafeCritical]
    [NonVersionable]
    [__DynamicallyInvokable]
    public static unsafe bool operator ==(UIntPtr value1, UIntPtr value2)
    {
      return value1.m_value == value2.m_value;
    }

    /// <summary>
    ///   Определяет, являются ли два заданных экземпляра класса <see cref="T:System.UIntPtr" /> неравными.
    /// </summary>
    /// <param name="value1">
    ///   Первый из сравниваемых указателей или дескрипторов.
    /// </param>
    /// <param name="value2">
    ///   Второй из сравниваемых указателей или дескрипторов.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если значения параметров <paramref name="value1" /> и <paramref name="value2" /> не равны; в противном случае — значение <see langword="false" />.
    /// </returns>
    [SecuritySafeCritical]
    [NonVersionable]
    [__DynamicallyInvokable]
    public static unsafe bool operator !=(UIntPtr value1, UIntPtr value2)
    {
      return value1.m_value != value2.m_value;
    }

    /// <summary>Добавляет смещение к значению указателя без знака.</summary>
    /// <param name="pointer">
    ///   Указатель без знака, чтобы добавить смещение.
    /// </param>
    /// <param name="offset">Добавляемое смещение.</param>
    /// <returns>
    ///   Новый указатель без знака, получающийся при добавлении смещения <paramref name="offset" /> для <paramref name="pointer" />.
    /// </returns>
    [NonVersionable]
    public static UIntPtr Add(UIntPtr pointer, int offset)
    {
      return pointer + offset;
    }

    /// <summary>Добавляет смещение к значению указателя без знака.</summary>
    /// <param name="pointer">
    ///   Указатель без знака, чтобы добавить смещение.
    /// </param>
    /// <param name="offset">Добавляемое смещение.</param>
    /// <returns>
    ///   Новый указатель без знака, получающийся при добавлении смещения <paramref name="offset" /> для <paramref name="pointer" />.
    /// </returns>
    [NonVersionable]
    public static UIntPtr operator +(UIntPtr pointer, int offset)
    {
      return new UIntPtr(pointer.ToUInt32() + (uint) offset);
    }

    /// <summary>Вычитает смещение из значения указателя без знака.</summary>
    /// <param name="pointer">
    ///   Указатель без знака, вычитаемое смещение от.
    /// </param>
    /// <param name="offset">Вычитаемое смещение.</param>
    /// <returns>
    ///   Новый указатель без знака, получающийся при вычитании смещения <paramref name="offset" /> из <paramref name="pointer" />.
    /// </returns>
    [NonVersionable]
    public static UIntPtr Subtract(UIntPtr pointer, int offset)
    {
      return pointer - offset;
    }

    /// <summary>Вычитает смещение из значения указателя без знака.</summary>
    /// <param name="pointer">
    ///   Указатель без знака, вычитаемое смещение от.
    /// </param>
    /// <param name="offset">Вычитаемое смещение.</param>
    /// <returns>
    ///   Новый указатель без знака, получающийся при вычитании смещения <paramref name="offset" /> из <paramref name="pointer" />.
    /// </returns>
    [NonVersionable]
    public static UIntPtr operator -(UIntPtr pointer, int offset)
    {
      return new UIntPtr(pointer.ToUInt32() - (uint) offset);
    }

    /// <summary>Получает размер этого экземпляра.</summary>
    /// <returns>
    ///   Размер указателя или дескриптора на этой платформе, в байтах.
    ///    Значение этого свойства — 4 на 32-разрядной платформе и 8 на 64-разрядной платформе.
    /// </returns>
    [__DynamicallyInvokable]
    public static int Size
    {
      [NonVersionable, __DynamicallyInvokable] get
      {
        return 4;
      }
    }

    /// <summary>
    ///   Преобразует значение этого экземпляра в указатель незаданного типа.
    /// </summary>
    /// <returns>
    ///   Указатель на <see cref="T:System.Void" /> (указатель на ячейку памяти, содержащую данные незаданного типа).
    /// </returns>
    [SecuritySafeCritical]
    [CLSCompliant(false)]
    [NonVersionable]
    public unsafe void* ToPointer()
    {
      return this.m_value;
    }
  }
}
