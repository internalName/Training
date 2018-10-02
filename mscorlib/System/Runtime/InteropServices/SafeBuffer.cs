// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.SafeBuffer
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32.SafeHandles;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security;

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Предоставляет управляемый буфер памяти, который может использоваться для чтения и записи.
  ///    Попытки обращения к памяти за пределами управляемого буфера (опустошение или переполнение) вызывают исключения.
  /// </summary>
  [SecurityCritical]
  [__DynamicallyInvokable]
  public abstract class SafeBuffer : SafeHandleZeroOrMinusOneIsInvalid
  {
    private static readonly UIntPtr Uninitialized = UIntPtr.Size == 4 ? (UIntPtr) uint.MaxValue : (UIntPtr) ulong.MaxValue;
    private UIntPtr _numBytes;

    /// <summary>
    ///   Создает новый экземпляр <see cref="T:System.Runtime.InteropServices.SafeBuffer" /> класса и указывает, является ли дескриптор буфера надежно выпуска.
    /// </summary>
    /// <param name="ownsHandle">
    ///   Значение <see langword="true" />, чтобы надежно освободить маркер на стадии завершения; значение <see langword="false" />, чтобы предотвратить надежное освобождение (не рекомендуется).
    /// </param>
    [__DynamicallyInvokable]
    protected SafeBuffer(bool ownsHandle)
      : base(ownsHandle)
    {
      this._numBytes = SafeBuffer.Uninitialized;
    }

    /// <summary>
    ///   Определяет размер выделяемой области памяти в байтах.
    ///    Перед использованием необходимо вызвать этот метод <see cref="T:System.Runtime.InteropServices.SafeBuffer" /> экземпляра.
    /// </summary>
    /// <param name="numBytes">Количество байтов в буфере.</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="numBytes" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="numBytes" /> больше доступного адресного пространства.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public void Initialize(ulong numBytes)
    {
      if (numBytes < 0UL)
        throw new ArgumentOutOfRangeException(nameof (numBytes), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (IntPtr.Size == 4 && numBytes > (ulong) uint.MaxValue)
        throw new ArgumentOutOfRangeException(nameof (numBytes), Environment.GetResourceString("ArgumentOutOfRange_AddressSpace"));
      if (numBytes >= (ulong) SafeBuffer.Uninitialized)
        throw new ArgumentOutOfRangeException(nameof (numBytes), Environment.GetResourceString("ArgumentOutOfRange_UIntPtrMax-1"));
      this._numBytes = (UIntPtr) numBytes;
    }

    /// <summary>
    ///   Задает размер буфера памяти, используя указанное количество элементов и размер элемента.
    ///    Перед использованием необходимо вызвать этот метод <see cref="T:System.Runtime.InteropServices.SafeBuffer" /> экземпляра.
    /// </summary>
    /// <param name="numElements">Количество элементов в буфере.</param>
    /// <param name="sizeOfEachElement">
    ///   Размер каждого элемента в буфере.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="numElements" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   Значение параметра <paramref name="sizeOfEachElement" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="numElements" /> умноженное на <paramref name="sizeOfEachElement" /> больше доступного адресного пространства.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public void Initialize(uint numElements, uint sizeOfEachElement)
    {
      if (numElements < 0U)
        throw new ArgumentOutOfRangeException(nameof (numElements), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (sizeOfEachElement < 0U)
        throw new ArgumentOutOfRangeException(nameof (sizeOfEachElement), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (IntPtr.Size == 4 && numElements * sizeOfEachElement > uint.MaxValue)
        throw new ArgumentOutOfRangeException("numBytes", Environment.GetResourceString("ArgumentOutOfRange_AddressSpace"));
      if ((ulong) (numElements * sizeOfEachElement) >= (ulong) SafeBuffer.Uninitialized)
        throw new ArgumentOutOfRangeException(nameof (numElements), Environment.GetResourceString("ArgumentOutOfRange_UIntPtrMax-1"));
      this._numBytes = (UIntPtr) checked (numElements * sizeOfEachElement);
    }

    /// <summary>
    ///   Определяет размер выделяемой области памяти, указав количество типов значений.
    ///    Перед использованием необходимо вызвать этот метод <see cref="T:System.Runtime.InteropServices.SafeBuffer" /> экземпляра.
    /// </summary>
    /// <param name="numElements">
    ///   Число элементов для выделения памяти для типа значения.
    /// </param>
    /// <typeparam name="T">Тип значения для выделения памяти.</typeparam>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="numElements" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="numElements" /> умноженное на размер каждого элемента больше доступного адресного пространства.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public void Initialize<T>(uint numElements) where T : struct
    {
      this.Initialize(numElements, Marshal.AlignedSizeOf<T>());
    }

    /// <summary>
    ///   Получает указатель из <see cref="T:System.Runtime.InteropServices.SafeBuffer" /> объекта блока памяти.
    /// </summary>
    /// <param name="pointer">
    ///   Указатель байтов, переданный по ссылке для получения указателя изнутри <see cref="T:System.Runtime.InteropServices.SafeBuffer" /> объекта.
    ///    Необходимо установить указатель на <see langword="null" /> перед вызовом этого метода.
    /// </param>
    /// <exception cref="T:System.InvalidOperationException">
    ///   <see cref="Overload:System.Runtime.InteropServices.SafeBuffer.Initialize" /> Не был вызван метод.
    /// </exception>
    [CLSCompliant(false)]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    public unsafe void AcquirePointer(ref byte* pointer)
    {
      if (this._numBytes == SafeBuffer.Uninitialized)
        throw SafeBuffer.NotInitialized();
      pointer = (byte*) null;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
      }
      finally
      {
        bool success = false;
        this.DangerousAddRef(ref success);
        pointer = (byte*) (void*) this.handle;
      }
    }

    /// <summary>
    ///   Освобождает указатель, который был получен путем <see cref="M:System.Runtime.InteropServices.SafeBuffer.AcquirePointer(System.Byte*@)" /> метод.
    /// </summary>
    /// <exception cref="T:System.InvalidOperationException">
    ///   <see cref="Overload:System.Runtime.InteropServices.SafeBuffer.Initialize" /> Не был вызван метод.
    /// </exception>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    public void ReleasePointer()
    {
      if (this._numBytes == SafeBuffer.Uninitialized)
        throw SafeBuffer.NotInitialized();
      this.DangerousRelease();
    }

    /// <summary>
    ///   Считывает тип значения из памяти с указанным смещением.
    /// </summary>
    /// <param name="byteOffset">
    ///   Расположение, из которого считывается тип значения.
    ///    Может потребоваться продумать проблемы выравнивания.
    /// </param>
    /// <typeparam name="T">Тип значения для чтения.</typeparam>
    /// <returns>Тип значения, считанный из памяти.</returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   <see cref="Overload:System.Runtime.InteropServices.SafeBuffer.Initialize" /> Не был вызван метод.
    /// </exception>
    [CLSCompliant(false)]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public unsafe T Read<T>(ulong byteOffset) where T : struct
    {
      if (this._numBytes == SafeBuffer.Uninitialized)
        throw SafeBuffer.NotInitialized();
      uint sizeofT = Marshal.SizeOfType(typeof (T));
      byte* ptr = (byte*) ((IntPtr) (void*) this.handle + (IntPtr) byteOffset);
      this.SpaceCheck(ptr, (ulong) sizeofT);
      bool success = false;
      RuntimeHelpers.PrepareConstrainedRegions();
      T structure;
      try
      {
        this.DangerousAddRef(ref success);
        SafeBuffer.GenericPtrToStructure<T>(ptr, out structure, sizeofT);
      }
      finally
      {
        if (success)
          this.DangerousRelease();
      }
      return structure;
    }

    /// <summary>
    ///   Считывает указанное количество типов значение из памяти, начиная со смещения и записывает их в массив, начиная с индекса.
    /// </summary>
    /// <param name="byteOffset">
    ///   Расположение, с которого начинается чтение.
    /// </param>
    /// <param name="array">Выходной массив для записи.</param>
    /// <param name="index">
    ///   Расположение в выходном массиве для начала записи.
    /// </param>
    /// <param name="count">
    ///   Количество типов значений для чтения из входного массива и для записи в выходной массив.
    /// </param>
    /// <typeparam name="T">Тип значения для чтения.</typeparam>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="index" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   Значение параметра <paramref name="count" /> меньше нуля.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="array" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Длина массива минус индекс меньше, чем <paramref name="count" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   <see cref="Overload:System.Runtime.InteropServices.SafeBuffer.Initialize" /> Не был вызван метод.
    /// </exception>
    [CLSCompliant(false)]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public unsafe void ReadArray<T>(ulong byteOffset, T[] array, int index, int count) where T : struct
    {
      if (array == null)
        throw new ArgumentNullException(nameof (array), Environment.GetResourceString("ArgumentNull_Buffer"));
      if (index < 0)
        throw new ArgumentOutOfRangeException(nameof (index), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (count < 0)
        throw new ArgumentOutOfRangeException(nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (array.Length - index < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      if (this._numBytes == SafeBuffer.Uninitialized)
        throw SafeBuffer.NotInitialized();
      uint sizeofT = Marshal.SizeOfType(typeof (T));
      uint num = Marshal.AlignedSizeOf<T>();
      byte* ptr = (byte*) ((IntPtr) (void*) this.handle + (IntPtr) byteOffset);
      this.SpaceCheck(ptr, checked ((ulong) ((long) num * (long) count)));
      bool success = false;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this.DangerousAddRef(ref success);
        for (int index1 = 0; index1 < count; ++index1)
          SafeBuffer.GenericPtrToStructure<T>(ptr + (long) num * (long) index1, out array[index1 + index], sizeofT);
      }
      finally
      {
        if (success)
          this.DangerousRelease();
      }
    }

    /// <summary>Записывает тип значения в память в указанном месте.</summary>
    /// <param name="byteOffset">
    ///   Расположение, с которого начинается запись.
    ///    Может потребоваться продумать проблемы выравнивания.
    /// </param>
    /// <param name="value">Значение для записи.</param>
    /// <typeparam name="T">Тип значения для записи.</typeparam>
    /// <exception cref="T:System.InvalidOperationException">
    ///   <see cref="Overload:System.Runtime.InteropServices.SafeBuffer.Initialize" /> Не был вызван метод.
    /// </exception>
    [CLSCompliant(false)]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public unsafe void Write<T>(ulong byteOffset, T value) where T : struct
    {
      if (this._numBytes == SafeBuffer.Uninitialized)
        throw SafeBuffer.NotInitialized();
      uint sizeofT = Marshal.SizeOfType(typeof (T));
      byte* ptr = (byte*) ((IntPtr) (void*) this.handle + (IntPtr) byteOffset);
      this.SpaceCheck(ptr, (ulong) sizeofT);
      bool success = false;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this.DangerousAddRef(ref success);
        SafeBuffer.GenericStructureToPtr<T>(ref value, ptr, sizeofT);
      }
      finally
      {
        if (success)
          this.DangerousRelease();
      }
    }

    /// <summary>
    ///   Записывает указанное количество типов значение ячейки памяти путем чтения байтов, начиная с указанной позиции во входном массиве.
    /// </summary>
    /// <param name="byteOffset">Расположение в памяти для записи.</param>
    /// <param name="array">Входной массив.</param>
    /// <param name="index">
    ///   Смещение в массиве, с которой начинается считывание.
    /// </param>
    /// <param name="count">Число записываемых типов значений.</param>
    /// <typeparam name="T">Тип значения для записи.</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="array" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="index" /> или <paramref name="count" /> меньше нуля.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Длина входного массива за вычетом <paramref name="index" /> — меньше, чем <paramref name="count" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   <see cref="Overload:System.Runtime.InteropServices.SafeBuffer.Initialize" /> Не был вызван метод.
    /// </exception>
    [CLSCompliant(false)]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public unsafe void WriteArray<T>(ulong byteOffset, T[] array, int index, int count) where T : struct
    {
      if (array == null)
        throw new ArgumentNullException(nameof (array), Environment.GetResourceString("ArgumentNull_Buffer"));
      if (index < 0)
        throw new ArgumentOutOfRangeException(nameof (index), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (count < 0)
        throw new ArgumentOutOfRangeException(nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (array.Length - index < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      if (this._numBytes == SafeBuffer.Uninitialized)
        throw SafeBuffer.NotInitialized();
      uint sizeofT = Marshal.SizeOfType(typeof (T));
      uint num = Marshal.AlignedSizeOf<T>();
      byte* ptr = (byte*) ((IntPtr) (void*) this.handle + (IntPtr) byteOffset);
      this.SpaceCheck(ptr, checked ((ulong) ((long) num * (long) count)));
      bool success = false;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this.DangerousAddRef(ref success);
        for (int index1 = 0; index1 < count; ++index1)
          SafeBuffer.GenericStructureToPtr<T>(ref array[index1 + index], ptr + (long) num * (long) index1, sizeofT);
      }
      finally
      {
        if (success)
          this.DangerousRelease();
      }
    }

    /// <summary>Возвращает размер буфера в байтах.</summary>
    /// <returns>Число байтов в буфере памяти.</returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   <see cref="Overload:System.Runtime.InteropServices.SafeBuffer.Initialize" /> Не был вызван метод.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public ulong ByteLength
    {
      [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success), __DynamicallyInvokable] get
      {
        if (this._numBytes == SafeBuffer.Uninitialized)
          throw SafeBuffer.NotInitialized();
        return (ulong) this._numBytes;
      }
    }

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    private unsafe void SpaceCheck(byte* ptr, ulong sizeInBytes)
    {
      if ((ulong) this._numBytes < sizeInBytes)
        SafeBuffer.NotEnoughRoom();
      if ((ulong) (ptr - (byte*) (void*) this.handle) <= (ulong) this._numBytes - sizeInBytes)
        return;
      SafeBuffer.NotEnoughRoom();
    }

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    private static void NotEnoughRoom()
    {
      throw new ArgumentException(Environment.GetResourceString("Arg_BufferTooSmall"));
    }

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    private static InvalidOperationException NotInitialized()
    {
      return new InvalidOperationException(Environment.GetResourceString("InvalidOperation_MustCallInitialize"));
    }

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    internal static unsafe void GenericPtrToStructure<T>(byte* ptr, out T structure, uint sizeofT) where T : struct
    {
      structure = default (T);
      SafeBuffer.PtrToStructureNative(ptr, __makeref (structure), sizeofT);
    }

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern unsafe void PtrToStructureNative(byte* ptr, TypedReference structure, uint sizeofT);

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    internal static unsafe void GenericStructureToPtr<T>(ref T structure, byte* ptr, uint sizeofT) where T : struct
    {
      SafeBuffer.StructureToPtrNative(__makeref (structure), ptr, sizeofT);
    }

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern unsafe void StructureToPtrNative(TypedReference structure, byte* ptr, uint sizeofT);
  }
}
