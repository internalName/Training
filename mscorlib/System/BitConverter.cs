// Decompiled with JetBrains decompiler
// Type: System.BitConverter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;

namespace System
{
  /// <summary>
  ///   Преобразует базовые типы данных в массив байтов, а массив байтов — в базовые типы данных.
  /// </summary>
  [__DynamicallyInvokable]
  public static class BitConverter
  {
    /// <summary>
    ///   Указывает порядок байтов (порядок следования байтов), по которому данные сохраняются в архитектуре этого компьютера.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если в архитектуре используется прямой порядок байтов, и <see langword="false" />, если используется обратный порядок байтов.
    /// </returns>
    [__DynamicallyInvokable]
    public static readonly bool IsLittleEndian = true;

    /// <summary>
    ///   Возвращает заданное логическое значение в виде массива байтов.
    /// </summary>
    /// <param name="value">Значение типа Boolean.</param>
    /// <returns>Массив байтов с длиной 1.</returns>
    [__DynamicallyInvokable]
    public static byte[] GetBytes(bool value)
    {
      return new byte[1]{ value ? (byte) 1 : (byte) 0 };
    }

    /// <summary>
    ///   Возвращает значение заданного знака Юникода в виде массива байтов.
    /// </summary>
    /// <param name="value">
    ///   Знак, который необходимо преобразовать.
    /// </param>
    /// <returns>Массив байтов с длиной 2.</returns>
    [__DynamicallyInvokable]
    public static byte[] GetBytes(char value)
    {
      return BitConverter.GetBytes((short) value);
    }

    /// <summary>
    ///   Возвращает значение заданного 16-разрядного знакового целого числа в виде массива байтов.
    /// </summary>
    /// <param name="value">Преобразуемое число.</param>
    /// <returns>Массив байтов с длиной 2.</returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static unsafe byte[] GetBytes(short value)
    {
      byte[] numArray = new byte[2];
      fixed (byte* numPtr = numArray)
        *(short*) numPtr = value;
      return numArray;
    }

    /// <summary>
    ///   Возвращает значение заданного 32-разрядного знакового целого числа в виде массива байтов.
    /// </summary>
    /// <param name="value">Преобразуемое число.</param>
    /// <returns>Массив байтов с длиной 4.</returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static unsafe byte[] GetBytes(int value)
    {
      byte[] numArray = new byte[4];
      fixed (byte* numPtr = numArray)
        *(int*) numPtr = value;
      return numArray;
    }

    /// <summary>
    ///   Возвращает указанное 64-разрядные целые числа со знаком в качестве массива байтов.
    /// </summary>
    /// <param name="value">Преобразуемое число.</param>
    /// <returns>Массив байтов с шириной 8.</returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static unsafe byte[] GetBytes(long value)
    {
      byte[] numArray = new byte[8];
      fixed (byte* numPtr = numArray)
        *(long*) numPtr = value;
      return numArray;
    }

    /// <summary>
    ///   Возвращает указанного 16-разрядного целого числа без знака в виде массива байтов.
    /// </summary>
    /// <param name="value">Преобразуемое число.</param>
    /// <returns>Массив байтов с длиной 2.</returns>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static byte[] GetBytes(ushort value)
    {
      return BitConverter.GetBytes((short) value);
    }

    /// <summary>
    ///   Возвращает значение заданного 32-разрядное целое число без знака в виде массива байтов.
    /// </summary>
    /// <param name="value">Преобразуемое число.</param>
    /// <returns>Массив байтов с длиной 4.</returns>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static byte[] GetBytes(uint value)
    {
      return BitConverter.GetBytes((int) value);
    }

    /// <summary>
    ///   Возвращает значение заданного 64-разрядное целое число без знака в виде массива байтов.
    /// </summary>
    /// <param name="value">Преобразуемое число.</param>
    /// <returns>Массив байтов с шириной 8.</returns>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static byte[] GetBytes(ulong value)
    {
      return BitConverter.GetBytes((long) value);
    }

    /// <summary>
    ///   Возвращает значение заданного числа одинарной точности с плавающей запятой в виде массива байтов.
    /// </summary>
    /// <param name="value">Преобразуемое число.</param>
    /// <returns>Массив байтов с длиной 4.</returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static unsafe byte[] GetBytes(float value)
    {
      return BitConverter.GetBytes(*(int*) &value);
    }

    /// <summary>
    ///   Возвращает значение заданного числа двойной точности с плавающей запятой в виде массива байтов.
    /// </summary>
    /// <param name="value">Преобразуемое число.</param>
    /// <returns>Массив байтов с шириной 8.</returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static unsafe byte[] GetBytes(double value)
    {
      return BitConverter.GetBytes(*(long*) &value);
    }

    /// <summary>
    ///   Возвращает знак Юникода, преобразованный из двух байтов с указанной позиции в массиве байтов.
    /// </summary>
    /// <param name="value">Массив.</param>
    /// <param name="startIndex">
    ///   Начальная позиция в <paramref name="value" />.
    /// </param>
    /// <returns>
    ///   Знак, сформированный на два байта, начиная с <paramref name="startIndex" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="startIndex" />равно сумме длины <paramref name="value" /> минус 1.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="startIndex" />меньше нуля или больше, чем длина <paramref name="value" /> минус 1.
    /// </exception>
    [__DynamicallyInvokable]
    public static char ToChar(byte[] value, int startIndex)
    {
      if (value == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.value);
      if ((long) (uint) startIndex >= (long) value.Length)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.startIndex, ExceptionResource.ArgumentOutOfRange_Index);
      if (startIndex > value.Length - 2)
        ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
      return (char) BitConverter.ToInt16(value, startIndex);
    }

    /// <summary>
    ///   Возвращает 16-разрядное целое число со знаком, преобразованное из двух байтов в указанной позиции в массиве байтов.
    /// </summary>
    /// <param name="value">Массив байтов.</param>
    /// <param name="startIndex">
    ///   Начальная позиция в <paramref name="value" />.
    /// </param>
    /// <returns>
    ///   16-разрядное целое число со знаком, образованное двумя байтами, начиная с <paramref name="startIndex" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="startIndex" />равно сумме длины <paramref name="value" /> минус 1.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="startIndex" />меньше нуля или больше, чем длина <paramref name="value" /> минус 1.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static unsafe short ToInt16(byte[] value, int startIndex)
    {
      if (value == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.value);
      if ((long) (uint) startIndex >= (long) value.Length)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.startIndex, ExceptionResource.ArgumentOutOfRange_Index);
      if (startIndex > value.Length - 2)
        ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
      fixed (byte* numPtr = &value[startIndex])
      {
        if (startIndex % 2 == 0)
          return *(short*) numPtr;
        if (BitConverter.IsLittleEndian)
          return (short) ((int) *numPtr | (int) numPtr[1] << 8);
        return (short) ((int) *numPtr << 8 | (int) numPtr[1]);
      }
    }

    /// <summary>
    ///   Возвращает 32-разрядное целое число со знаком, преобразованное из четырех байт с указанной позицией в массив байтов.
    /// </summary>
    /// <param name="value">Массив байтов.</param>
    /// <param name="startIndex">
    ///   Начальная позиция в <paramref name="value" />.
    /// </param>
    /// <returns>
    ///   32-разрядное целое число со знаком, образованное четырьмя байтами, начиная с <paramref name="startIndex" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="startIndex" />больше или равно длине <paramref name="value" /> минус 3, и меньше или равна длине <paramref name="value" /> минус 1.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="startIndex" />меньше нуля или больше, чем длина <paramref name="value" /> минус 1.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static unsafe int ToInt32(byte[] value, int startIndex)
    {
      if (value == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.value);
      if ((long) (uint) startIndex >= (long) value.Length)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.startIndex, ExceptionResource.ArgumentOutOfRange_Index);
      if (startIndex > value.Length - 4)
        ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
      fixed (byte* numPtr = &value[startIndex])
      {
        if (startIndex % 4 == 0)
          return *(int*) numPtr;
        if (BitConverter.IsLittleEndian)
          return (int) *numPtr | (int) numPtr[1] << 8 | (int) numPtr[2] << 16 | (int) numPtr[3] << 24;
        return (int) *numPtr << 24 | (int) numPtr[1] << 16 | (int) numPtr[2] << 8 | (int) numPtr[3];
      }
    }

    /// <summary>
    ///   Возвращает 64-разрядное целое число со знаком, преобразованное из восьми байтов в указанной позиции в массиве байтов.
    /// </summary>
    /// <param name="value">Массив байтов.</param>
    /// <param name="startIndex">
    ///   Начальная позиция в <paramref name="value" />.
    /// </param>
    /// <returns>
    ///   64-разрядное знаковое целое число, сформированное восемь байтов, начиная с <paramref name="startIndex" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="startIndex" />больше или равно длине <paramref name="value" /> минус 7, и меньше или равна длине <paramref name="value" /> минус 1.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="startIndex" />меньше нуля или больше, чем длина <paramref name="value" /> минус 1.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static unsafe long ToInt64(byte[] value, int startIndex)
    {
      if (value == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.value);
      if ((long) (uint) startIndex >= (long) value.Length)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.startIndex, ExceptionResource.ArgumentOutOfRange_Index);
      if (startIndex > value.Length - 8)
        ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
      fixed (byte* numPtr = &value[startIndex])
      {
        if (startIndex % 8 == 0)
          return *(long*) numPtr;
        if (BitConverter.IsLittleEndian)
          return (long) (uint) ((int) *numPtr | (int) numPtr[1] << 8 | (int) numPtr[2] << 16 | (int) numPtr[3] << 24) | (long) ((int) numPtr[4] | (int) numPtr[5] << 8 | (int) numPtr[6] << 16 | (int) numPtr[7] << 24) << 32;
        int num = (int) *numPtr << 24 | (int) numPtr[1] << 16 | (int) numPtr[2] << 8 | (int) numPtr[3];
        return (long) ((uint) ((int) numPtr[4] << 24 | (int) numPtr[5] << 16 | (int) numPtr[6] << 8) | (uint) numPtr[7]) | (long) num << 32;
      }
    }

    /// <summary>
    ///   Возвращает 16-разрядное целое число без знака, преобразованное из двух байтов в указанной позиции в массиве байтов.
    /// </summary>
    /// <param name="value">Массив байтов.</param>
    /// <param name="startIndex">
    ///   Начальная позиция в <paramref name="value" />.
    /// </param>
    /// <returns>
    ///   16-разрядное целое число без знака, образованное двумя байтами, начиная с <paramref name="startIndex" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="startIndex" />равно сумме длины <paramref name="value" /> минус 1.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="startIndex" />меньше нуля или больше, чем длина <paramref name="value" /> минус 1.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ushort ToUInt16(byte[] value, int startIndex)
    {
      if (value == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.value);
      if ((long) (uint) startIndex >= (long) value.Length)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.startIndex, ExceptionResource.ArgumentOutOfRange_Index);
      if (startIndex > value.Length - 2)
        ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
      return (ushort) BitConverter.ToInt16(value, startIndex);
    }

    /// <summary>
    ///   Возвращает 32-разрядное целое число без знака, преобразованное из четырех байт с указанной позицией в массив байтов.
    /// </summary>
    /// <param name="value">Массив байтов.</param>
    /// <param name="startIndex">
    ///   Начальная позиция в <paramref name="value" />.
    /// </param>
    /// <returns>
    ///   32-разрядное целое число без знака, образованное четырьмя байтами, начиная с <paramref name="startIndex" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="startIndex" />больше или равно длине <paramref name="value" /> минус 3, и меньше или равна длине <paramref name="value" /> минус 1.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="startIndex" />меньше нуля или больше, чем длина <paramref name="value" /> минус 1.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static uint ToUInt32(byte[] value, int startIndex)
    {
      if (value == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.value);
      if ((long) (uint) startIndex >= (long) value.Length)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.startIndex, ExceptionResource.ArgumentOutOfRange_Index);
      if (startIndex > value.Length - 4)
        ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
      return (uint) BitConverter.ToInt32(value, startIndex);
    }

    /// <summary>
    ///   Возвращает 64-разрядное целое число без знака, преобразованное из восьми байтов в указанной позиции в массиве байтов.
    /// </summary>
    /// <param name="value">Массив байтов.</param>
    /// <param name="startIndex">
    ///   Начальная позиция в <paramref name="value" />.
    /// </param>
    /// <returns>
    ///   64-разрядное целое число без знака, образованное восемь байтов, начиная с <paramref name="startIndex" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="startIndex" />больше или равно длине <paramref name="value" /> минус 7, и меньше или равна длине <paramref name="value" /> минус 1.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="startIndex" />меньше нуля или больше, чем длина <paramref name="value" /> минус 1.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ulong ToUInt64(byte[] value, int startIndex)
    {
      if (value == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.value);
      if ((long) (uint) startIndex >= (long) value.Length)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.startIndex, ExceptionResource.ArgumentOutOfRange_Index);
      if (startIndex > value.Length - 8)
        ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
      return (ulong) BitConverter.ToInt64(value, startIndex);
    }

    /// <summary>
    ///   Возвращает число одинарной точности с плавающей запятой, преобразованное из четырех байт с указанной позицией в массив байтов.
    /// </summary>
    /// <param name="value">Массив байтов.</param>
    /// <param name="startIndex">
    ///   Начальная позиция в <paramref name="value" />.
    /// </param>
    /// <returns>
    ///   Число с плавающей запятой одиночной точности, сформированный на четыре байта, начиная с <paramref name="startIndex" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="startIndex" />больше или равно длине <paramref name="value" /> минус 3, и меньше или равна длине <paramref name="value" /> минус 1.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="startIndex" />меньше нуля или больше, чем длина <paramref name="value" /> минус 1.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static unsafe float ToSingle(byte[] value, int startIndex)
    {
      if (value == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.value);
      if ((long) (uint) startIndex >= (long) value.Length)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.startIndex, ExceptionResource.ArgumentOutOfRange_Index);
      if (startIndex > value.Length - 4)
        ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
      return *(float*) &BitConverter.ToInt32(value, startIndex);
    }

    /// <summary>
    ///   Возвращает число двойной точности с плавающей запятой, преобразованное из восьми байтов в указанной позиции в массиве байтов.
    /// </summary>
    /// <param name="value">Массив байтов.</param>
    /// <param name="startIndex">
    ///   Начальная позиция в <paramref name="value" />.
    /// </param>
    /// <returns>
    ///   Число двойной точности с плавающей запятой, образованное восемь байтов, начиная с <paramref name="startIndex" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="startIndex" />больше или равно длине <paramref name="value" /> минус 7, и меньше или равна длине <paramref name="value" /> минус 1.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="startIndex" />меньше нуля или больше, чем длина <paramref name="value" /> минус 1.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static unsafe double ToDouble(byte[] value, int startIndex)
    {
      if (value == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.value);
      if ((long) (uint) startIndex >= (long) value.Length)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.startIndex, ExceptionResource.ArgumentOutOfRange_Index);
      if (startIndex > value.Length - 8)
        ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
      return *(double*) &BitConverter.ToInt64(value, startIndex);
    }

    private static char GetHexValue(int i)
    {
      if (i < 10)
        return (char) (i + 48);
      return (char) (i - 10 + 65);
    }

    /// <summary>
    ///   Преобразует числовое значение каждого элемента указанного дочернего массива байтов в эквивалентное ему шестнадцатеричное строковое представление.
    /// </summary>
    /// <param name="value">Массив байтов.</param>
    /// <param name="startIndex">
    ///   Начальная позиция в <paramref name="value" />.
    /// </param>
    /// <param name="length">
    ///   Число элементов в массиве <paramref name="value" /> для преобразования.
    /// </param>
    /// <returns>
    ///   Строка шестнадцатеричных пар, разделенных дефисами, где каждая пара предоставляет соответствующий элемент в подмассиве <paramref name="value" />, например «7F-2 C-4A-00».
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="startIndex" /> или <paramref name="length" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="startIndex" />больше нуля и больше или равно длине <paramref name="value" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Сочетание <paramref name="startIndex" /> и <paramref name="length" /> не указывает позицию в <paramref name="value" />, то есть <paramref name="startIndex" /> параметр больше, чем длина <paramref name="value" /> минус <paramref name="length" /> параметра.
    /// </exception>
    [__DynamicallyInvokable]
    public static string ToString(byte[] value, int startIndex, int length)
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      if (startIndex < 0 || startIndex >= value.Length && startIndex > 0)
        throw new ArgumentOutOfRangeException(nameof (startIndex), Environment.GetResourceString("ArgumentOutOfRange_StartIndex"));
      if (length < 0)
        throw new ArgumentOutOfRangeException(nameof (length), Environment.GetResourceString("ArgumentOutOfRange_GenericPositive"));
      if (startIndex > value.Length - length)
        throw new ArgumentException(Environment.GetResourceString("Arg_ArrayPlusOffTooSmall"));
      if (length == 0)
        return string.Empty;
      if (length > 715827882)
        throw new ArgumentOutOfRangeException(nameof (length), Environment.GetResourceString("ArgumentOutOfRange_LengthTooLarge", (object) 715827882));
      int length1 = length * 3;
      char[] chArray = new char[length1];
      int num1 = startIndex;
      int index = 0;
      while (index < length1)
      {
        byte num2 = value[num1++];
        chArray[index] = BitConverter.GetHexValue((int) num2 / 16);
        chArray[index + 1] = BitConverter.GetHexValue((int) num2 % 16);
        chArray[index + 2] = '-';
        index += 3;
      }
      return new string(chArray, 0, chArray.Length - 1);
    }

    /// <summary>
    ///   Преобразует числовое значение каждого элемента указанного массива байтов в эквивалентное ему шестнадцатеричное строковое представление.
    /// </summary>
    /// <param name="value">Массив байтов.</param>
    /// <returns>
    ///   Строка шестнадцатеричных пар, разделенных дефисами, где каждая пара предоставляет соответствующий элемент в <paramref name="value" />, например «7F-2 C-4A-00».
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static string ToString(byte[] value)
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      return BitConverter.ToString(value, 0, value.Length);
    }

    /// <summary>
    ///   Преобразует числовое значение каждого элемента указанного дочернего массива байтов в эквивалентное ему шестнадцатеричное строковое представление.
    /// </summary>
    /// <param name="value">Массив байтов.</param>
    /// <param name="startIndex">
    ///   Начальная позиция в <paramref name="value" />.
    /// </param>
    /// <returns>
    ///   Строка шестнадцатеричных пар, разделенных дефисами, где каждая пара предоставляет соответствующий элемент в подмассиве <paramref name="value" />, например «7F-2 C-4A-00».
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="startIndex" />меньше нуля или больше, чем длина <paramref name="value" /> минус 1.
    /// </exception>
    [__DynamicallyInvokable]
    public static string ToString(byte[] value, int startIndex)
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      return BitConverter.ToString(value, startIndex, value.Length - startIndex);
    }

    /// <summary>
    ///   Возвращает логическое значение, преобразованное из байтов с указанной позицией в массив байтов.
    /// </summary>
    /// <param name="value">Массив байтов.</param>
    /// <param name="startIndex">
    ///   Индекс байта в <paramref name="value" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если байт в <paramref name="startIndex" /> в <paramref name="value" /> не равен нулю; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="startIndex" /> меньше нуля или больше, чем длина <paramref name="value" /> минус 1.
    /// </exception>
    [__DynamicallyInvokable]
    public static bool ToBoolean(byte[] value, int startIndex)
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      if (startIndex < 0)
        throw new ArgumentOutOfRangeException(nameof (startIndex), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (startIndex > value.Length - 1)
        throw new ArgumentOutOfRangeException(nameof (startIndex), Environment.GetResourceString("ArgumentOutOfRange_Index"));
      return value[startIndex] != (byte) 0;
    }

    /// <summary>
    ///   Преобразует заданного числа двойной точности с плавающей запятой в 64-разрядное целое число со знаком.
    /// </summary>
    /// <param name="value">Преобразуемое число.</param>
    /// <returns>
    ///   64-разрядное целое число со знаком, значение которого эквивалентно значению <paramref name="value" />.
    /// </returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static unsafe long DoubleToInt64Bits(double value)
    {
      return *(long*) &value;
    }

    /// <summary>
    ///   Преобразует заданного 64-разрядного целого числа со знаком в числа с плавающей запятой двойной точности.
    /// </summary>
    /// <param name="value">Преобразуемое число.</param>
    /// <returns>
    ///   Число двойной точности с плавающей запятой, значение которого эквивалентно значению <paramref name="value" />.
    /// </returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static unsafe double Int64BitsToDouble(long value)
    {
      return *(double*) &value;
    }
  }
}
