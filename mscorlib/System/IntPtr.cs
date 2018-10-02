// Decompiled with JetBrains decompiler
// Type: System.IntPtr
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Versioning;
using System.Security;

namespace System
{
  /// <summary>
  ///   Определяемый платформой тип, который используется для представления указателя или дескриптора.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public struct IntPtr : ISerializable
  {
    [SecurityCritical]
    private unsafe void* m_value;
    /// <summary>
    ///   Доступное только для чтения поле, которое предоставляет указатель или дескриптор, инициализированный с нулевым значением.
    /// </summary>
    public static readonly IntPtr Zero;

    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    internal unsafe bool IsNull()
    {
      return (IntPtr) this.m_value == IntPtr.Zero;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр структуры <see cref="T:System.IntPtr" /> с помощью заданного 32-битового указателя или дескриптора.
    /// </summary>
    /// <param name="value">
    ///   Указатель или дескриптор состоит из 32-разрядного целого числа со знаком.
    /// </param>
    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
    [NonVersionable]
    [__DynamicallyInvokable]
    public unsafe IntPtr(int value)
    {
      this.m_value = (void*) value;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.IntPtr" /> с использованием заданного 64-разрядного указателя.
    /// </summary>
    /// <param name="value">
    ///   Указатель или дескриптор состоит из 64-разрядного целого числа со знаком.
    /// </param>
    /// <exception cref="T:System.OverflowException">
    ///   На 32-разрядной платформе <paramref name="value" /> слишком большой или слишком мало для представления в виде <see cref="T:System.IntPtr" />.
    /// </exception>
    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
    [NonVersionable]
    [__DynamicallyInvokable]
    public unsafe IntPtr(long value)
    {
      this.m_value = (void*) checked ((int) value);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр структуры <see cref="T:System.IntPtr" /> с использованием заданного указателя на незаданный тип.
    /// </summary>
    /// <param name="value">Указатель незаданного типа.</param>
    [SecurityCritical]
    [CLSCompliant(false)]
    [ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
    [NonVersionable]
    public unsafe IntPtr(void* value)
    {
      this.m_value = value;
    }

    [SecurityCritical]
    private unsafe IntPtr(SerializationInfo info, StreamingContext context)
    {
      long int64 = info.GetInt64("value");
      if (IntPtr.Size == 4 && (int64 > (long) int.MaxValue || int64 < (long) int.MinValue))
        throw new ArgumentException(Environment.GetResourceString("Serialization_InvalidPtrValue"));
      this.m_value = (void*) int64;
    }

    [SecurityCritical]
    unsafe void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException(nameof (info));
      info.AddValue("value", (long) (int) this.m_value);
    }

    /// <summary>
    ///   Возвращает значение, показывающее, равен ли данный экземпляр заданному объекту.
    /// </summary>
    /// <param name="obj">
    ///   Объект, сравниваемый с этим экземпляром или <see langword="null" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если параметр <paramref name="obj" /> является экземпляром типа <see cref="T:System.IntPtr" /> и равен значению данного экземпляра; в противном случае — значение <see langword="false" />.
    /// </returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public override unsafe bool Equals(object obj)
    {
      if (obj is IntPtr)
        return this.m_value == ((IntPtr) obj).m_value;
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
      return (int) this.m_value;
    }

    /// <summary>
    ///   Преобразует значение этого экземпляра в формат 32-разрядного целого числа со знаком.
    /// </summary>
    /// <returns>
    ///   32-разрядное целое число со знаком, равное значению данного экземпляра.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   На 64-разрядной платформе значение этого экземпляра слишком велико или слишком мало для представления в виде 32-разрядное целое число со знаком.
    /// </exception>
    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [NonVersionable]
    [__DynamicallyInvokable]
    public unsafe int ToInt32()
    {
      return (int) this.m_value;
    }

    /// <summary>
    ///   Преобразует значение этого экземпляра в формат 64-разрядного целого числа со знаком.
    /// </summary>
    /// <returns>
    ///   64-и разрядное целое число со знаком равно значению данного экземпляра.
    /// </returns>
    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [NonVersionable]
    [__DynamicallyInvokable]
    public unsafe long ToInt64()
    {
      return (long) (int) this.m_value;
    }

    /// <summary>
    ///   Преобразует числовое значение текущего объекта <see cref="T:System.IntPtr" /> в эквивалентное ему строковое представление.
    /// </summary>
    /// <returns>Строковое представление значения этого экземпляра.</returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public override unsafe string ToString()
    {
      return ((int) this.m_value).ToString((IFormatProvider) CultureInfo.InvariantCulture);
    }

    /// <summary>
    ///   Преобразует числовое значение текущего объекта <see cref="T:System.IntPtr" /> в эквивалентное ему строковое представление.
    /// </summary>
    /// <param name="format">
    ///   Спецификация формата, в которой определен порядок преобразования текущего объекта <see cref="T:System.IntPtr" />.
    /// </param>
    /// <returns>
    ///   Строковое представление значения текущего объекта <see cref="T:System.IntPtr" />.
    /// </returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public unsafe string ToString(string format)
    {
      return ((int) this.m_value).ToString(format, (IFormatProvider) CultureInfo.InvariantCulture);
    }

    /// <summary>
    ///   Преобразует 32-разрядное целочисленное значение со знаком в <see cref="T:System.IntPtr" />.
    /// </summary>
    /// <param name="value">32-разрядное знаковое целое число.</param>
    /// <returns>
    ///   Новый экземпляр <see cref="T:System.IntPtr" />, инициализированный значением <paramref name="value" />.
    /// </returns>
    [ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
    [NonVersionable]
    public static explicit operator IntPtr(int value)
    {
      return new IntPtr(value);
    }

    /// <summary>
    ///   Преобразует 64-разрядное целочисленное значение со знаком в <see cref="T:System.IntPtr" />.
    /// </summary>
    /// <param name="value">64-разрядное целое число со знаком.</param>
    /// <returns>
    ///   Новый экземпляр <see cref="T:System.IntPtr" />, инициализированный значением <paramref name="value" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   На 32-разрядной платформе <paramref name="value" /> слишком велико для представления в качестве <see cref="T:System.IntPtr" />.
    /// </exception>
    [ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
    [NonVersionable]
    public static explicit operator IntPtr(long value)
    {
      return new IntPtr(value);
    }

    /// <summary>
    ///   Преобразует заданный указатель на незаданный тип в <see cref="T:System.IntPtr" />.
    /// </summary>
    /// <param name="value">Указатель незаданного типа.</param>
    /// <returns>
    ///   Новый экземпляр <see cref="T:System.IntPtr" />, инициализированный значением <paramref name="value" />.
    /// </returns>
    [SecurityCritical]
    [CLSCompliant(false)]
    [ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
    [NonVersionable]
    public static unsafe explicit operator IntPtr(void* value)
    {
      return new IntPtr(value);
    }

    /// <summary>
    ///   Преобразует значение заданной структуры <see cref="T:System.IntPtr" /> в указатель на незаданный тип.
    /// </summary>
    /// <param name="value">
    ///   Указатель или дескриптор, подлежащий преобразованию.
    /// </param>
    /// <returns>
    ///   Содержимое <paramref name="value" />.
    /// </returns>
    [SecuritySafeCritical]
    [CLSCompliant(false)]
    [NonVersionable]
    public static unsafe explicit operator void*(IntPtr value)
    {
      return value.m_value;
    }

    /// <summary>
    ///   Преобразует значение заданной структуры <see cref="T:System.IntPtr" /> в формат 32-разрядного целого числа со знаком.
    /// </summary>
    /// <param name="value">
    ///   Указатель или дескриптор, подлежащий преобразованию.
    /// </param>
    /// <returns>
    ///   Содержимое <paramref name="value" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   На 64-разрядной платформе, значение <paramref name="value" /> слишком велико для представления в качестве 32-разрядное целое число со знаком.
    /// </exception>
    [SecuritySafeCritical]
    [NonVersionable]
    public static unsafe explicit operator int(IntPtr value)
    {
      return (int) value.m_value;
    }

    /// <summary>
    ///   Преобразует значение заданной структуры <see cref="T:System.IntPtr" /> в формат 64-разрядного целого числа со знаком.
    /// </summary>
    /// <param name="value">
    ///   Указатель или дескриптор, подлежащий преобразованию.
    /// </param>
    /// <returns>
    ///   Содержимое <paramref name="value" />.
    /// </returns>
    [SecuritySafeCritical]
    [NonVersionable]
    public static unsafe explicit operator long(IntPtr value)
    {
      return (long) (int) value.m_value;
    }

    /// <summary>
    ///   Определяет, равны ли два заданных экземпляра класса <see cref="T:System.IntPtr" />.
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
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [NonVersionable]
    [__DynamicallyInvokable]
    public static unsafe bool operator ==(IntPtr value1, IntPtr value2)
    {
      return value1.m_value == value2.m_value;
    }

    /// <summary>
    ///   Определяет, являются ли два заданных экземпляра класса <see cref="T:System.IntPtr" /> неравными.
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
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [NonVersionable]
    [__DynamicallyInvokable]
    public static unsafe bool operator !=(IntPtr value1, IntPtr value2)
    {
      return value1.m_value != value2.m_value;
    }

    /// <summary>Добавляет смещение к значению указателя.</summary>
    /// <param name="pointer">
    ///   Указатель, к которому требуется добавить смещение.
    /// </param>
    /// <param name="offset">Добавляемое смещение.</param>
    /// <returns>
    ///   Новый указатель, получающийся при добавлении смещения <paramref name="offset" /> к указателю <paramref name="pointer" />.
    /// </returns>
    [ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
    [NonVersionable]
    public static IntPtr Add(IntPtr pointer, int offset)
    {
      return pointer + offset;
    }

    /// <summary>Добавляет смещение к значению указателя.</summary>
    /// <param name="pointer">
    ///   Указатель, к которому требуется добавить смещение.
    /// </param>
    /// <param name="offset">Добавляемое смещение.</param>
    /// <returns>
    ///   Новый указатель, получающийся при добавлении смещения <paramref name="offset" /> к указателю <paramref name="pointer" />.
    /// </returns>
    [ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
    [NonVersionable]
    public static IntPtr operator +(IntPtr pointer, int offset)
    {
      return new IntPtr(pointer.ToInt32() + offset);
    }

    /// <summary>Вычитает смещение из значения указателя.</summary>
    /// <param name="pointer">
    ///   Указатель, из которого требуется вычесть смещение.
    /// </param>
    /// <param name="offset">Вычитаемое смещение.</param>
    /// <returns>
    ///   Новый указатель, получающийся при вычитании смещения <paramref name="offset" /> из указателя <paramref name="pointer" />.
    /// </returns>
    [ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
    [NonVersionable]
    public static IntPtr Subtract(IntPtr pointer, int offset)
    {
      return pointer - offset;
    }

    /// <summary>Вычитает смещение из значения указателя.</summary>
    /// <param name="pointer">
    ///   Указатель, из которого требуется вычесть смещение.
    /// </param>
    /// <param name="offset">Вычитаемое смещение.</param>
    /// <returns>
    ///   Новый указатель, получающийся при вычитании смещения <paramref name="offset" /> из указателя <paramref name="pointer" />.
    /// </returns>
    [ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
    [NonVersionable]
    public static IntPtr operator -(IntPtr pointer, int offset)
    {
      return new IntPtr(pointer.ToInt32() - offset);
    }

    /// <summary>Получает размер этого экземпляра.</summary>
    /// <returns>
    ///   Размер указателя или дескриптора в данном процессе в байтах.
    ///    Значение этого свойства равно 4 в 32-разрядном процессе и 8 в 64-разрядном процессе.
    ///    Можно указать тип процесса, задав параметр <see langword="/platform" /> при компилировании кода с помощью компиляторов C# и Visual Basic.
    /// </returns>
    [__DynamicallyInvokable]
    public static int Size
    {
      [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success), NonVersionable, __DynamicallyInvokable] get
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
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [NonVersionable]
    public unsafe void* ToPointer()
    {
      return this.m_value;
    }
  }
}
