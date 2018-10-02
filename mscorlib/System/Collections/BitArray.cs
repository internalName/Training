// Decompiled with JetBrains decompiler
// Type: System.Collections.BitArray
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Threading;

namespace System.Collections
{
  /// <summary>
  ///   Управляет компактным массивом двоичных значений, представленных логическими значениями, где значение <see langword="true" /> соответствует включенному биту (1), а значение <see langword="false" /> соответствует отключенному биту (0).
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class BitArray : ICollection, IEnumerable, ICloneable
  {
    private const int BitsPerInt32 = 32;
    private const int BytesPerInt32 = 4;
    private const int BitsPerByte = 8;
    private int[] m_array;
    private int m_length;
    private int _version;
    [NonSerialized]
    private object _syncRoot;
    private const int _ShrinkThreshold = 256;

    private BitArray()
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Collections.BitArray" />, который может содержать указанное количество битов с первоначально заданным значением <see langword="false" />.
    /// </summary>
    /// <param name="length">
    ///   Число битовых значений в новом массиве <see cref="T:System.Collections.BitArray" />.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="length" /> меньше нуля.
    /// </exception>
    [__DynamicallyInvokable]
    public BitArray(int length)
      : this(length, false)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Collections.BitArray" />, который может содержать указанное количество двоичных значений, для которых установлены заданные начальные значения.
    /// </summary>
    /// <param name="length">
    ///   Число двоичных значений в новом массиве <see cref="T:System.Collections.BitArray" />.
    /// </param>
    /// <param name="defaultValue">
    ///   Логическое значение, присваиваемое каждому биту.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="length" /> меньше нуля.
    /// </exception>
    [__DynamicallyInvokable]
    public BitArray(int length, bool defaultValue)
    {
      if (length < 0)
        throw new ArgumentOutOfRangeException(nameof (length), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      this.m_array = new int[BitArray.GetArrayLength(length, 32)];
      this.m_length = length;
      int num = defaultValue ? -1 : 0;
      for (int index = 0; index < this.m_array.Length; ++index)
        this.m_array[index] = num;
      this._version = 0;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Collections.BitArray" />, который содержит двоичные значения, скопированные из заданного массива байтов.
    /// </summary>
    /// <param name="bytes">
    ///   Массив байтов, содержащий копируемые значения, где каждый байт представляет собой восемь последовательных битов.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="bytes" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Длина <paramref name="bytes" /> больше <see cref="F:System.Int32.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public BitArray(byte[] bytes)
    {
      if (bytes == null)
        throw new ArgumentNullException(nameof (bytes));
      if (bytes.Length > 268435455)
        throw new ArgumentException(Environment.GetResourceString("Argument_ArrayTooLarge", (object) 8), nameof (bytes));
      this.m_array = new int[BitArray.GetArrayLength(bytes.Length, 4)];
      this.m_length = bytes.Length * 8;
      int index1 = 0;
      int index2 = 0;
      while (bytes.Length - index2 >= 4)
      {
        this.m_array[index1++] = (int) bytes[index2] & (int) byte.MaxValue | ((int) bytes[index2 + 1] & (int) byte.MaxValue) << 8 | ((int) bytes[index2 + 2] & (int) byte.MaxValue) << 16 | ((int) bytes[index2 + 3] & (int) byte.MaxValue) << 24;
        index2 += 4;
      }
      switch (bytes.Length - index2)
      {
        case 1:
          this.m_array[index1] |= (int) bytes[index2] & (int) byte.MaxValue;
          break;
        case 2:
          this.m_array[index1] |= ((int) bytes[index2 + 1] & (int) byte.MaxValue) << 8;
          goto case 1;
        case 3:
          this.m_array[index1] = ((int) bytes[index2 + 2] & (int) byte.MaxValue) << 16;
          goto case 2;
      }
      this._version = 0;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Collections.BitArray" />, который содержит двоичные значения, скопированные из заданного массива логических значений.
    /// </summary>
    /// <param name="values">
    ///   Массив логических значений, который нужно скопировать.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="values" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public BitArray(bool[] values)
    {
      if (values == null)
        throw new ArgumentNullException(nameof (values));
      this.m_array = new int[BitArray.GetArrayLength(values.Length, 32)];
      this.m_length = values.Length;
      for (int index = 0; index < values.Length; ++index)
      {
        if (values[index])
          this.m_array[index / 32] |= 1 << index % 32;
      }
      this._version = 0;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Collections.BitArray" />, который содержит двоичные значения, скопированные из указанного массива 32-битных целых чисел.
    /// </summary>
    /// <param name="values">
    ///   Массив целых чисел, содержащий копируемые значения, где каждое целое число представлено 32 последовательными битами.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="values" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Длина <paramref name="values" /> больше <see cref="F:System.Int32.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public BitArray(int[] values)
    {
      if (values == null)
        throw new ArgumentNullException(nameof (values));
      if (values.Length > 67108863)
        throw new ArgumentException(Environment.GetResourceString("Argument_ArrayTooLarge", (object) 32), nameof (values));
      this.m_array = new int[values.Length];
      this.m_length = values.Length * 32;
      Array.Copy((Array) values, (Array) this.m_array, values.Length);
      this._version = 0;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Collections.BitArray" />, содержащий двоичные значения, скопированные из указанного массива <see cref="T:System.Collections.BitArray" />.
    /// </summary>
    /// <param name="bits">
    ///   Копируемый <see cref="T:System.Collections.BitArray" />.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="bits" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public BitArray(BitArray bits)
    {
      if (bits == null)
        throw new ArgumentNullException(nameof (bits));
      int arrayLength = BitArray.GetArrayLength(bits.m_length, 32);
      this.m_array = new int[arrayLength];
      this.m_length = bits.m_length;
      Array.Copy((Array) bits.m_array, (Array) this.m_array, arrayLength);
      this._version = bits._version;
    }

    /// <summary>
    ///   Возвращает или задает значение в указанной позиции в массиве <see cref="T:System.Collections.BitArray" />.
    /// </summary>
    /// <param name="index">
    ///   Отсчитываемый от нуля индекс возвращаемого или задаваемого значения.
    /// </param>
    /// <returns>
    ///   Значение бита в позиции <paramref name="index" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="index" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   Значение параметра <paramref name="index" /> больше или равно значению свойства <see cref="P:System.Collections.BitArray.Count" />.
    /// </exception>
    [__DynamicallyInvokable]
    public bool this[int index]
    {
      [__DynamicallyInvokable] get
      {
        return this.Get(index);
      }
      [__DynamicallyInvokable] set
      {
        this.Set(index, value);
      }
    }

    /// <summary>
    ///   Возвращает значение бита в указанной позиции в массиве <see cref="T:System.Collections.BitArray" />.
    /// </summary>
    /// <param name="index">
    ///   Отсчитываемый от нуля индекс значения, которое нужно получить.
    /// </param>
    /// <returns>
    ///   Значение бита в позиции <paramref name="index" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="index" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   Индекс <paramref name="index" /> больше или равен количеству элементов в массиве <see cref="T:System.Collections.BitArray" />.
    /// </exception>
    [__DynamicallyInvokable]
    public bool Get(int index)
    {
      if (index < 0 || index >= this.Length)
        throw new ArgumentOutOfRangeException(nameof (index), Environment.GetResourceString("ArgumentOutOfRange_Index"));
      return (uint) (this.m_array[index / 32] & 1 << index % 32) > 0U;
    }

    /// <summary>
    ///   Устанавливает заданное значение бита в указанной позиции в массиве <see cref="T:System.Collections.BitArray" />.
    /// </summary>
    /// <param name="index">
    ///   Отсчитываемый от нуля индекс задаваемого бита.
    /// </param>
    /// <param name="value">
    ///   Логическое значение, которое требуется присвоить биту.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="index" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   Индекс <paramref name="index" /> больше или равен количеству элементов в массиве <see cref="T:System.Collections.BitArray" />.
    /// </exception>
    [__DynamicallyInvokable]
    public void Set(int index, bool value)
    {
      if (index < 0 || index >= this.Length)
        throw new ArgumentOutOfRangeException(nameof (index), Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (value)
        this.m_array[index / 32] |= 1 << index % 32;
      else
        this.m_array[index / 32] &= ~(1 << index % 32);
      ++this._version;
    }

    /// <summary>
    ///   Присваивает указанное значение всем битам в массиве <see cref="T:System.Collections.BitArray" />.
    /// </summary>
    /// <param name="value">
    ///   Логическое значение, которое требуется присвоить всем битам.
    /// </param>
    [__DynamicallyInvokable]
    public void SetAll(bool value)
    {
      int num = value ? -1 : 0;
      int arrayLength = BitArray.GetArrayLength(this.m_length, 32);
      for (int index = 0; index < arrayLength; ++index)
        this.m_array[index] = num;
      ++this._version;
    }

    /// <summary>
    ///   Выполняет битовую операцию И между элементами текущего объекта <see cref="T:System.Collections.BitArray" /> с соответствующими элементами указанного массива.
    ///    Текущий <see cref="T:System.Collections.BitArray" /> объект будет изменен для хранения результата битовой операции И.
    /// </summary>
    /// <param name="value">
    ///   Массив, в котором выполняется битовая операция И.
    /// </param>
    /// <returns>
    ///   Массив, содержащий результат битовой операции И, который представляет собой ссылку на текущий объект <see cref="T:System.Collections.BitArray" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Число элементов в <paramref name="value" /> и текущем <see cref="T:System.Collections.BitArray" /> не совпадает.
    /// </exception>
    [__DynamicallyInvokable]
    public BitArray And(BitArray value)
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      if (this.Length != value.Length)
        throw new ArgumentException(Environment.GetResourceString("Arg_ArrayLengthsDiffer"));
      int arrayLength = BitArray.GetArrayLength(this.m_length, 32);
      for (int index = 0; index < arrayLength; ++index)
        this.m_array[index] &= value.m_array[index];
      ++this._version;
      return this;
    }

    /// <summary>
    ///   Выполняет битовую операцию ИЛИ между элементами текущего объекта <see cref="T:System.Collections.BitArray" /> с соответствующими элементами указанного массива.
    ///    Текущий объект <see cref="T:System.Collections.BitArray" /> будет изменен для хранения результата битовой операции ИЛИ.
    /// </summary>
    /// <param name="value">
    ///   Массив, в котором выполняется битовая операция ИЛИ.
    /// </param>
    /// <returns>
    ///   Массив, содержащий результат битовой операции ИЛИ, который представляет собой ссылку на текущий объект <see cref="T:System.Collections.BitArray" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Число элементов в <paramref name="value" /> и текущем <see cref="T:System.Collections.BitArray" /> не совпадает.
    /// </exception>
    [__DynamicallyInvokable]
    public BitArray Or(BitArray value)
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      if (this.Length != value.Length)
        throw new ArgumentException(Environment.GetResourceString("Arg_ArrayLengthsDiffer"));
      int arrayLength = BitArray.GetArrayLength(this.m_length, 32);
      for (int index = 0; index < arrayLength; ++index)
        this.m_array[index] |= value.m_array[index];
      ++this._version;
      return this;
    }

    /// <summary>
    ///   Выполняет битовую операцию исключающего ИЛИ между элементами текущего объекта <see cref="T:System.Collections.BitArray" /> с соответствующими элементами указанного массива.
    ///    Текущий <see cref="T:System.Collections.BitArray" /> объект будет изменен для хранения результата битовой операции исключающего ИЛИ.
    /// </summary>
    /// <param name="value">
    ///   Массив, с которым производится битовая операция исключающего ИЛИ.
    /// </param>
    /// <returns>
    ///   Массив, содержащий результат битовой операции исключающего ИЛИ, который представляет собой ссылку на текущий объект <see cref="T:System.Collections.BitArray" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Число элементов в <paramref name="value" /> и текущем <see cref="T:System.Collections.BitArray" /> не совпадает.
    /// </exception>
    [__DynamicallyInvokable]
    public BitArray Xor(BitArray value)
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      if (this.Length != value.Length)
        throw new ArgumentException(Environment.GetResourceString("Arg_ArrayLengthsDiffer"));
      int arrayLength = BitArray.GetArrayLength(this.m_length, 32);
      for (int index = 0; index < arrayLength; ++index)
        this.m_array[index] ^= value.m_array[index];
      ++this._version;
      return this;
    }

    /// <summary>
    ///   Обращает все двоичные значения в текущем массиве <see cref="T:System.Collections.BitArray" /> таким образом, чтобы каждому элементу со значением <see langword="true" /> было присвоено значение <see langword="false" />, а каждому элементу со значением <see langword="false" /> было присвоено значение <see langword="true" />.
    /// </summary>
    /// <returns>
    ///   Текущие экземпляры с обращенными двоичными значениями.
    /// </returns>
    [__DynamicallyInvokable]
    public BitArray Not()
    {
      int arrayLength = BitArray.GetArrayLength(this.m_length, 32);
      for (int index = 0; index < arrayLength; ++index)
        this.m_array[index] = ~this.m_array[index];
      ++this._version;
      return this;
    }

    /// <summary>
    ///   Получает или задает число элементов в массиве <see cref="T:System.Collections.BitArray" />.
    /// </summary>
    /// <returns>
    ///   Число элементов в массиве <see cref="T:System.Collections.BitArray" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Свойству присвоено значение меньше нуля.
    /// </exception>
    [__DynamicallyInvokable]
    public int Length
    {
      [__DynamicallyInvokable] get
      {
        return this.m_length;
      }
      [__DynamicallyInvokable] set
      {
        if (value < 0)
          throw new ArgumentOutOfRangeException(nameof (value), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
        int arrayLength = BitArray.GetArrayLength(value, 32);
        if (arrayLength > this.m_array.Length || arrayLength + 256 < this.m_array.Length)
        {
          int[] numArray = new int[arrayLength];
          Array.Copy((Array) this.m_array, (Array) numArray, arrayLength > this.m_array.Length ? this.m_array.Length : arrayLength);
          this.m_array = numArray;
        }
        if (value > this.m_length)
        {
          int index = BitArray.GetArrayLength(this.m_length, 32) - 1;
          int num = this.m_length % 32;
          if (num > 0)
            this.m_array[index] &= (1 << num) - 1;
          Array.Clear((Array) this.m_array, index + 1, arrayLength - index - 1);
        }
        this.m_length = value;
        ++this._version;
      }
    }

    /// <summary>
    ///   Копирует целый массив <see cref="T:System.Collections.BitArray" /> в совместимый одномерный массив <see cref="T:System.Array" />, начиная с заданного индекса целевого массива.
    /// </summary>
    /// <param name="array">
    ///   Одномерный массив <see cref="T:System.Array" />, в который копируются элементы из интерфейса <see cref="T:System.Collections.BitArray" />.
    ///    Массив <see cref="T:System.Array" /> должен иметь индексацию, начинающуюся с нуля.
    /// </param>
    /// <param name="index">
    ///   Отсчитываемый от нуля индекс в массиве <paramref name="array" />, указывающий начало копирования.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="array" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="index" /> меньше нуля.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Массив <paramref name="array" /> является многомерным.
    /// 
    ///   -или-
    /// 
    ///   Количество элементов в исходной коллекции <see cref="T:System.Collections.BitArray" /> больше, чем свободное пространство от <paramref name="index" /> до конца массива назначения <paramref name="array" />.
    /// </exception>
    /// <exception cref="T:System.InvalidCastException">
    ///   Тип источника <see cref="T:System.Collections.BitArray" /> не может быть автоматически приведен к типу массива назначения <paramref name="array" />.
    /// </exception>
    public void CopyTo(Array array, int index)
    {
      if (array == null)
        throw new ArgumentNullException(nameof (array));
      if (index < 0)
        throw new ArgumentOutOfRangeException(nameof (index), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (array.Rank != 1)
        throw new ArgumentException(Environment.GetResourceString("Arg_RankMultiDimNotSupported"));
      if (array is int[])
        Array.Copy((Array) this.m_array, 0, array, index, BitArray.GetArrayLength(this.m_length, 32));
      else if (array is byte[])
      {
        int arrayLength = BitArray.GetArrayLength(this.m_length, 8);
        if (array.Length - index < arrayLength)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
        byte[] numArray = (byte[]) array;
        for (int index1 = 0; index1 < arrayLength; ++index1)
          numArray[index + index1] = (byte) (this.m_array[index1 / 4] >> index1 % 4 * 8 & (int) byte.MaxValue);
      }
      else
      {
        if (!(array is bool[]))
          throw new ArgumentException(Environment.GetResourceString("Arg_BitArrayTypeUnsupported"));
        if (array.Length - index < this.m_length)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
        bool[] flagArray = (bool[]) array;
        for (int index1 = 0; index1 < this.m_length; ++index1)
          flagArray[index + index1] = (uint) (this.m_array[index1 / 32] >> index1 % 32 & 1) > 0U;
      }
    }

    /// <summary>
    ///   Получает число элементов, содержащихся в интерфейсе <see cref="T:System.Collections.BitArray" />.
    /// </summary>
    /// <returns>
    ///   Число элементов, содержащихся в коллекции <see cref="T:System.Collections.BitArray" />.
    /// </returns>
    public int Count
    {
      get
      {
        return this.m_length;
      }
    }

    /// <summary>
    ///   Создает неполную копию объекта <see cref="T:System.Collections.BitArray" />.
    /// </summary>
    /// <returns>
    ///   Неполная копия <see cref="T:System.Collections.BitArray" />.
    /// </returns>
    public object Clone()
    {
      return (object) new BitArray(this.m_array)
      {
        _version = this._version,
        m_length = this.m_length
      };
    }

    /// <summary>
    ///   Получает объект, с помощью которого можно синхронизировать доступ к коллекции <see cref="T:System.Collections.BitArray" />.
    /// </summary>
    /// <returns>
    ///   Объект, который может использоваться для синхронизации доступа к <see cref="T:System.Collections.BitArray" />.
    /// </returns>
    public object SyncRoot
    {
      get
      {
        if (this._syncRoot == null)
          Interlocked.CompareExchange<object>(ref this._syncRoot, new object(), (object) null);
        return this._syncRoot;
      }
    }

    /// <summary>
    ///   Получает значение, указывающее, является ли объект <see cref="T:System.Collections.BitArray" /> доступным только для чтения.
    /// </summary>
    /// <returns>
    ///   Данное свойство всегда имеет значение <see langword="false" />.
    /// </returns>
    public bool IsReadOnly
    {
      get
      {
        return false;
      }
    }

    /// <summary>
    ///   Возвращает значение, показывающее, является ли доступ к коллекции <see cref="T:System.Collections.BitArray" /> синхронизированным (потокобезопасным).
    /// </summary>
    /// <returns>
    ///   Данное свойство всегда имеет значение <see langword="false" />.
    /// </returns>
    public bool IsSynchronized
    {
      get
      {
        return false;
      }
    }

    /// <summary>
    ///   Возвращает перечислитель, осуществляющий перебор элементов списка <see cref="T:System.Collections.BitArray" />.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Collections.IEnumerator" /> для всего <see cref="T:System.Collections.BitArray" />.
    /// </returns>
    [__DynamicallyInvokable]
    public IEnumerator GetEnumerator()
    {
      return (IEnumerator) new BitArray.BitArrayEnumeratorSimple(this);
    }

    private static int GetArrayLength(int n, int div)
    {
      if (n <= 0)
        return 0;
      return (n - 1) / div + 1;
    }

    [Serializable]
    private class BitArrayEnumeratorSimple : IEnumerator, ICloneable
    {
      private BitArray bitarray;
      private int index;
      private int version;
      private bool currentElement;

      internal BitArrayEnumeratorSimple(BitArray bitarray)
      {
        this.bitarray = bitarray;
        this.index = -1;
        this.version = bitarray._version;
      }

      public object Clone()
      {
        return this.MemberwiseClone();
      }

      public virtual bool MoveNext()
      {
        if (this.version != this.bitarray._version)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
        if (this.index < this.bitarray.Count - 1)
        {
          ++this.index;
          this.currentElement = this.bitarray.Get(this.index);
          return true;
        }
        this.index = this.bitarray.Count;
        return false;
      }

      public virtual object Current
      {
        get
        {
          if (this.index == -1)
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
          if (this.index >= this.bitarray.Count)
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumEnded"));
          return (object) this.currentElement;
        }
      }

      public void Reset()
      {
        if (this.version != this.bitarray._version)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
        this.index = -1;
      }
    }
  }
}
