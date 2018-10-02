// Decompiled with JetBrains decompiler
// Type: System.Text.StringBuilder
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Text
{
  /// <summary>
  ///   Предоставляет изменяемую строку символов.
  ///    Этот класс не наследуется.
  /// 
  ///   Просмотреть исходный код .NET Framework для этого типа Reference Source.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class StringBuilder : ISerializable
  {
    internal char[] m_ChunkChars;
    internal StringBuilder m_ChunkPrevious;
    internal int m_ChunkLength;
    internal int m_ChunkOffset;
    internal int m_MaxCapacity;
    internal const int DefaultCapacity = 16;
    private const string CapacityField = "Capacity";
    private const string MaxCapacityField = "m_MaxCapacity";
    private const string StringValueField = "m_StringValue";
    private const string ThreadIDField = "m_currentThread";
    internal const int MaxChunkSize = 8000;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Text.StringBuilder" />.
    /// </summary>
    [__DynamicallyInvokable]
    public StringBuilder()
      : this(16)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Text.StringBuilder" />, используя указанную емкость.
    /// </summary>
    /// <param name="capacity">
    ///   Предлагаемый начальный размер этого экземпляра.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="capacity" /> меньше нуля.
    /// </exception>
    [__DynamicallyInvokable]
    public StringBuilder(int capacity)
      : this(string.Empty, capacity)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Text.StringBuilder" />, используя указанную строку.
    /// </summary>
    /// <param name="value">
    ///   Строка, используемая для инициализации значения экземпляра.
    ///    Если <paramref name="value" /> равно <see langword="null" />, то новый <see cref="T:System.Text.StringBuilder" /> будет содержать пустую строку (то есть, он содержит <see cref="F:System.String.Empty" />).
    /// </param>
    [__DynamicallyInvokable]
    public StringBuilder(string value)
      : this(value, 16)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Text.StringBuilder" />, используя указанную строку и емкость.
    /// </summary>
    /// <param name="value">
    ///   Строка, используемая для инициализации значения экземпляра.
    ///    Если <paramref name="value" /> равно <see langword="null" />, то новый <see cref="T:System.Text.StringBuilder" /> будет содержать пустую строку (то есть, он содержит <see cref="F:System.String.Empty" />).
    /// </param>
    /// <param name="capacity">
    ///   Предлагаемый начальный размер <see cref="T:System.Text.StringBuilder" />.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="capacity" /> меньше нуля.
    /// </exception>
    [__DynamicallyInvokable]
    public StringBuilder(string value, int capacity)
      : this(value, 0, value != null ? value.Length : 0, capacity)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Text.StringBuilder" /> из указанной подстроки и емкости.
    /// </summary>
    /// <param name="value">
    ///   Строка, содержащая подстроку, применяемую для инициализации значения этого экземпляра.
    ///    Если <paramref name="value" /> равно <see langword="null" />, то новый <see cref="T:System.Text.StringBuilder" /> будет содержать пустую строку (то есть, он содержит <see cref="F:System.String.Empty" />).
    /// </param>
    /// <param name="startIndex">
    ///   Позиция в пределах <paramref name="value" />, с которой начинается подстрока.
    /// </param>
    /// <param name="length">Число символов в подстроке.</param>
    /// <param name="capacity">
    ///   Предлагаемый начальный размер <see cref="T:System.Text.StringBuilder" />.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="capacity" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="startIndex" /> плюс <paramref name="length" /> не является позицией в <paramref name="value" />.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public unsafe StringBuilder(string value, int startIndex, int length, int capacity)
    {
      if (capacity < 0)
        throw new ArgumentOutOfRangeException(nameof (capacity), Environment.GetResourceString("ArgumentOutOfRange_MustBePositive", (object) nameof (capacity)));
      if (length < 0)
        throw new ArgumentOutOfRangeException(nameof (length), Environment.GetResourceString("ArgumentOutOfRange_MustBeNonNegNum", (object) nameof (length)));
      if (startIndex < 0)
        throw new ArgumentOutOfRangeException(nameof (startIndex), Environment.GetResourceString("ArgumentOutOfRange_StartIndex"));
      if (value == null)
        value = string.Empty;
      if (startIndex > value.Length - length)
        throw new ArgumentOutOfRangeException(nameof (length), Environment.GetResourceString("ArgumentOutOfRange_IndexLength"));
      this.m_MaxCapacity = int.MaxValue;
      if (capacity == 0)
        capacity = 16;
      if (capacity < length)
        capacity = length;
      this.m_ChunkChars = new char[capacity];
      this.m_ChunkLength = length;
      string str = value;
      char* chPtr = (char*) str;
      if ((IntPtr) chPtr != IntPtr.Zero)
        chPtr += RuntimeHelpers.OffsetToStringData;
      StringBuilder.ThreadSafeCopy(chPtr + startIndex, this.m_ChunkChars, 0, length);
      str = (string) null;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Text.StringBuilder" />, который начинается с указанной емкости и может увеличиваться до указанного максимального значения.
    /// </summary>
    /// <param name="capacity">
    ///   Предлагаемый начальный размер <see cref="T:System.Text.StringBuilder" />.
    /// </param>
    /// <param name="maxCapacity">
    ///   Наибольшее допустимое количество знаков в текущей строке.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="maxCapacity" /> меньше единицы, <paramref name="capacity" /> меньше нуля, или <paramref name="capacity" /> больше <paramref name="maxCapacity" />.
    /// </exception>
    [__DynamicallyInvokable]
    public StringBuilder(int capacity, int maxCapacity)
    {
      if (capacity > maxCapacity)
        throw new ArgumentOutOfRangeException(nameof (capacity), Environment.GetResourceString("ArgumentOutOfRange_Capacity"));
      if (maxCapacity < 1)
        throw new ArgumentOutOfRangeException(nameof (maxCapacity), Environment.GetResourceString("ArgumentOutOfRange_SmallMaxCapacity"));
      if (capacity < 0)
        throw new ArgumentOutOfRangeException(nameof (capacity), Environment.GetResourceString("ArgumentOutOfRange_MustBePositive", (object) nameof (capacity)));
      if (capacity == 0)
        capacity = Math.Min(16, maxCapacity);
      this.m_MaxCapacity = maxCapacity;
      this.m_ChunkChars = new char[capacity];
    }

    [SecurityCritical]
    private StringBuilder(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException(nameof (info));
      int length = 0;
      string str = (string) null;
      int num = int.MaxValue;
      bool flag = false;
      SerializationInfoEnumerator enumerator = info.GetEnumerator();
      while (enumerator.MoveNext())
      {
        string name = enumerator.Name;
        if (!(name == nameof (m_MaxCapacity)))
        {
          if (!(name == "m_StringValue"))
          {
            if (name == nameof (Capacity))
            {
              length = info.GetInt32(nameof (Capacity));
              flag = true;
            }
          }
          else
            str = info.GetString("m_StringValue");
        }
        else
          num = info.GetInt32(nameof (m_MaxCapacity));
      }
      if (str == null)
        str = string.Empty;
      if (num < 1 || str.Length > num)
        throw new SerializationException(Environment.GetResourceString("Serialization_StringBuilderMaxCapacity"));
      if (!flag)
      {
        length = 16;
        if (length < str.Length)
          length = str.Length;
        if (length > num)
          length = num;
      }
      if (length < 0 || length < str.Length || length > num)
        throw new SerializationException(Environment.GetResourceString("Serialization_StringBuilderCapacity"));
      this.m_MaxCapacity = num;
      this.m_ChunkChars = new char[length];
      str.CopyTo(0, this.m_ChunkChars, 0, str.Length);
      this.m_ChunkLength = str.Length;
      this.m_ChunkPrevious = (StringBuilder) null;
    }

    [SecurityCritical]
    void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException(nameof (info));
      info.AddValue("m_MaxCapacity", this.m_MaxCapacity);
      info.AddValue("Capacity", this.Capacity);
      info.AddValue("m_StringValue", (object) this.ToString());
      info.AddValue("m_currentThread", 0);
    }

    [Conditional("_DEBUG")]
    private void VerifyClassInvariant()
    {
      StringBuilder stringBuilder = this;
      int maxCapacity = this.m_MaxCapacity;
      while (true)
      {
        StringBuilder chunkPrevious = stringBuilder.m_ChunkPrevious;
        if (chunkPrevious != null)
          stringBuilder = chunkPrevious;
        else
          break;
      }
    }

    /// <summary>
    ///   Возвращает или задает максимальное число знаков, которое может содержаться в памяти, назначенной текущим экземпляром.
    /// </summary>
    /// <returns>
    ///   Максимальное число символов, которое может содержаться в памяти, назначенной текущим экземпляром.
    ///    Это значение может меняться в диапазоне от <see cref="P:System.Text.StringBuilder.Length" /> до <see cref="P:System.Text.StringBuilder.MaxCapacity" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение, указанное для операции задания, меньше текущей длины данного экземпляра.
    /// 
    ///   -или-
    /// 
    ///   Значение, заданное для операции задания, больше максимальной емкости.
    /// </exception>
    [__DynamicallyInvokable]
    public int Capacity
    {
      [__DynamicallyInvokable] get
      {
        return this.m_ChunkChars.Length + this.m_ChunkOffset;
      }
      [__DynamicallyInvokable] set
      {
        if (value < 0)
          throw new ArgumentOutOfRangeException(nameof (value), Environment.GetResourceString("ArgumentOutOfRange_NegativeCapacity"));
        if (value > this.MaxCapacity)
          throw new ArgumentOutOfRangeException(nameof (value), Environment.GetResourceString("ArgumentOutOfRange_Capacity"));
        if (value < this.Length)
          throw new ArgumentOutOfRangeException(nameof (value), Environment.GetResourceString("ArgumentOutOfRange_SmallCapacity"));
        if (this.Capacity == value)
          return;
        char[] chArray = new char[value - this.m_ChunkOffset];
        Array.Copy((Array) this.m_ChunkChars, (Array) chArray, this.m_ChunkLength);
        this.m_ChunkChars = chArray;
      }
    }

    /// <summary>Возвращает максимальную емкость данного экземпляра.</summary>
    /// <returns>
    ///   Наибольшее количество символов, которое может содержать данный экземпляр.
    /// </returns>
    [__DynamicallyInvokable]
    public int MaxCapacity
    {
      [__DynamicallyInvokable] get
      {
        return this.m_MaxCapacity;
      }
    }

    /// <summary>
    ///   Гарантирует, что емкость данного экземпляра <see cref="T:System.Text.StringBuilder" /> не меньше указанного значения.
    /// </summary>
    /// <param name="capacity">Минимальная емкость для проверки.</param>
    /// <returns>Новая емкость этого экземпляра.</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="capacity" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   Увеличение значения этого экземпляра может привести к превышению <see cref="P:System.Text.StringBuilder.MaxCapacity" />.
    /// </exception>
    [__DynamicallyInvokable]
    public int EnsureCapacity(int capacity)
    {
      if (capacity < 0)
        throw new ArgumentOutOfRangeException(nameof (capacity), Environment.GetResourceString("ArgumentOutOfRange_NegativeCapacity"));
      if (this.Capacity < capacity)
        this.Capacity = capacity;
      return this.Capacity;
    }

    /// <summary>
    ///   Преобразует значение данного экземпляра в <see cref="T:System.String" />.
    /// </summary>
    /// <returns>
    ///   Строка, чье значение совпадает с данным экземпляром.
    /// </returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public override unsafe string ToString()
    {
      if (this.Length == 0)
        return string.Empty;
      string str1 = string.FastAllocateString(this.Length);
      StringBuilder stringBuilder = this;
      string str2 = str1;
      char* chPtr = (char*) str2;
      if ((IntPtr) chPtr != IntPtr.Zero)
        chPtr += RuntimeHelpers.OffsetToStringData;
      do
      {
        if (stringBuilder.m_ChunkLength > 0)
        {
          char[] chunkChars = stringBuilder.m_ChunkChars;
          int chunkOffset = stringBuilder.m_ChunkOffset;
          int chunkLength = stringBuilder.m_ChunkLength;
          if ((long) (uint) (chunkLength + chunkOffset) > (long) str1.Length || (uint) chunkLength > (uint) chunkChars.Length)
            throw new ArgumentOutOfRangeException("chunkLength", Environment.GetResourceString("ArgumentOutOfRange_Index"));
          fixed (char* smem = chunkChars)
            string.wstrcpy(chPtr + chunkOffset, smem, chunkLength);
        }
        stringBuilder = stringBuilder.m_ChunkPrevious;
      }
      while (stringBuilder != null);
      str2 = (string) null;
      return str1;
    }

    /// <summary>
    ///   Преобразует значение подстроки этого экземпляра в <see cref="T:System.String" />.
    /// </summary>
    /// <param name="startIndex">
    ///   Начальная позиция подстроки в данном экземпляре.
    /// </param>
    /// <param name="length">Длина подстроки.</param>
    /// <returns>
    ///   Строка, чье значение совпадает с указанной подстрокой данного экземпляра.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="startIndex" /> или <paramref name="length" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   Сума значений <paramref name="startIndex" /> и <paramref name="length" /> превышает длину текущего экземпляра.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public unsafe string ToString(int startIndex, int length)
    {
      int length1 = this.Length;
      if (startIndex < 0)
        throw new ArgumentOutOfRangeException(nameof (startIndex), Environment.GetResourceString("ArgumentOutOfRange_StartIndex"));
      if (startIndex > length1)
        throw new ArgumentOutOfRangeException(nameof (startIndex), Environment.GetResourceString("ArgumentOutOfRange_StartIndexLargerThanLength"));
      if (length < 0)
        throw new ArgumentOutOfRangeException(nameof (length), Environment.GetResourceString("ArgumentOutOfRange_NegativeLength"));
      if (startIndex > length1 - length)
        throw new ArgumentOutOfRangeException(nameof (length), Environment.GetResourceString("ArgumentOutOfRange_IndexLength"));
      StringBuilder stringBuilder = this;
      int num1 = startIndex + length;
      string str1 = string.FastAllocateString(length);
      int num2 = length;
      string str2 = str1;
      char* chPtr = (char*) str2;
      if ((IntPtr) chPtr != IntPtr.Zero)
        chPtr += RuntimeHelpers.OffsetToStringData;
      while (num2 > 0)
      {
        int num3 = num1 - stringBuilder.m_ChunkOffset;
        if (num3 >= 0)
        {
          if (num3 > stringBuilder.m_ChunkLength)
            num3 = stringBuilder.m_ChunkLength;
          int num4 = num2;
          int charCount = num4;
          int index = num3 - num4;
          if (index < 0)
          {
            charCount += index;
            index = 0;
          }
          num2 -= charCount;
          if (charCount > 0)
          {
            char[] chunkChars = stringBuilder.m_ChunkChars;
            if ((long) (uint) (charCount + num2) > (long) length || (uint) (charCount + index) > (uint) chunkChars.Length)
              throw new ArgumentOutOfRangeException("chunkCount", Environment.GetResourceString("ArgumentOutOfRange_Index"));
            fixed (char* smem = &chunkChars[index])
              string.wstrcpy(chPtr + num2, smem, charCount);
          }
        }
        stringBuilder = stringBuilder.m_ChunkPrevious;
      }
      str2 = (string) null;
      return str1;
    }

    /// <summary>
    ///   Удаляет все символы из текущего экземпляра <see cref="T:System.Text.StringBuilder" />.
    /// </summary>
    /// <returns>
    ///   Объект, длина <see cref="P:System.Text.StringBuilder.Length" /> которого равна 0 (нулю).
    /// </returns>
    [__DynamicallyInvokable]
    public StringBuilder Clear()
    {
      this.Length = 0;
      return this;
    }

    /// <summary>
    ///   Возвращает или задает длину текущего объекта <see cref="T:System.Text.StringBuilder" />.
    /// </summary>
    /// <returns>Длина этого экземпляра.</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение, заданное для операции задания, меньше нуля или больше <see cref="P:System.Text.StringBuilder.MaxCapacity" />.
    /// </exception>
    [__DynamicallyInvokable]
    public int Length
    {
      [__DynamicallyInvokable] get
      {
        return this.m_ChunkOffset + this.m_ChunkLength;
      }
      [__DynamicallyInvokable] set
      {
        if (value < 0)
          throw new ArgumentOutOfRangeException(nameof (value), Environment.GetResourceString("ArgumentOutOfRange_NegativeLength"));
        if (value > this.MaxCapacity)
          throw new ArgumentOutOfRangeException(nameof (value), Environment.GetResourceString("ArgumentOutOfRange_SmallCapacity"));
        int capacity = this.Capacity;
        if (value == 0 && this.m_ChunkPrevious == null)
        {
          this.m_ChunkLength = 0;
          this.m_ChunkOffset = 0;
        }
        else
        {
          int repeatCount = value - this.Length;
          if (repeatCount > 0)
          {
            this.Append(char.MinValue, repeatCount);
          }
          else
          {
            StringBuilder chunkForIndex = this.FindChunkForIndex(value);
            if (chunkForIndex != this)
            {
              char[] chArray = new char[capacity - chunkForIndex.m_ChunkOffset];
              Array.Copy((Array) chunkForIndex.m_ChunkChars, (Array) chArray, chunkForIndex.m_ChunkLength);
              this.m_ChunkChars = chArray;
              this.m_ChunkPrevious = chunkForIndex.m_ChunkPrevious;
              this.m_ChunkOffset = chunkForIndex.m_ChunkOffset;
            }
            this.m_ChunkLength = value - chunkForIndex.m_ChunkOffset;
          }
        }
      }
    }

    /// <summary>
    ///   Возвращает или задает символ на указанной позиции в данном экземпляре.
    /// </summary>
    /// <param name="index">Позиция символа.</param>
    /// <returns>
    ///   Символ Юникода в позиции <paramref name="index" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   При задании символа <paramref name="index" /> находится за пределами данного экземпляра.
    /// </exception>
    /// <exception cref="T:System.IndexOutOfRangeException">
    ///   При получении символа <paramref name="index" /> находится за пределами данного экземпляра.
    /// </exception>
    [__DynamicallyInvokable]
    [IndexerName("Chars")]
    public char this[int index]
    {
      [__DynamicallyInvokable] get
      {
        StringBuilder stringBuilder = this;
        do
        {
          int index1 = index - stringBuilder.m_ChunkOffset;
          if (index1 >= 0)
          {
            if (index1 >= stringBuilder.m_ChunkLength)
              throw new IndexOutOfRangeException();
            return stringBuilder.m_ChunkChars[index1];
          }
          stringBuilder = stringBuilder.m_ChunkPrevious;
        }
        while (stringBuilder != null);
        throw new IndexOutOfRangeException();
      }
      [__DynamicallyInvokable] set
      {
        StringBuilder stringBuilder = this;
        do
        {
          int index1 = index - stringBuilder.m_ChunkOffset;
          if (index1 >= 0)
          {
            if (index1 >= stringBuilder.m_ChunkLength)
              throw new ArgumentOutOfRangeException(nameof (index), Environment.GetResourceString("ArgumentOutOfRange_Index"));
            stringBuilder.m_ChunkChars[index1] = value;
            return;
          }
          stringBuilder = stringBuilder.m_ChunkPrevious;
        }
        while (stringBuilder != null);
        throw new ArgumentOutOfRangeException(nameof (index), Environment.GetResourceString("ArgumentOutOfRange_Index"));
      }
    }

    /// <summary>
    ///   Добавляет указанное число копий строкового представления символа Юникода к данному экземпляру.
    /// </summary>
    /// <param name="value">Добавляемый символ.</param>
    /// <param name="repeatCount">
    ///   Сколько раз следует добавить <paramref name="value" />.
    /// </param>
    /// <returns>
    ///   Ссылка на этот экземпляр после завершения операции добавления.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="repeatCount" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   Увеличение значения этого экземпляра может привести к превышению <see cref="P:System.Text.StringBuilder.MaxCapacity" />.
    /// </exception>
    /// <exception cref="T:System.OutOfMemoryException">
    ///   Недостаточно памяти.
    /// </exception>
    [__DynamicallyInvokable]
    public StringBuilder Append(char value, int repeatCount)
    {
      if (repeatCount < 0)
        throw new ArgumentOutOfRangeException(nameof (repeatCount), Environment.GetResourceString("ArgumentOutOfRange_NegativeCount"));
      if (repeatCount == 0)
        return this;
      int num = this.m_ChunkLength;
      while (repeatCount > 0)
      {
        if (num < this.m_ChunkChars.Length)
        {
          this.m_ChunkChars[num++] = value;
          --repeatCount;
        }
        else
        {
          this.m_ChunkLength = num;
          this.ExpandByABlock(repeatCount);
          num = 0;
        }
      }
      this.m_ChunkLength = num;
      return this;
    }

    /// <summary>
    ///   Добавляет строковое представление указанного дочернего массива символов Юникода к данному экземпляру.
    /// </summary>
    /// <param name="value">Массив символов.</param>
    /// <param name="startIndex">
    ///   Начальная позиция в массиве <paramref name="value" />.
    /// </param>
    /// <param name="charCount">Количество добавляемых знаков.</param>
    /// <returns>
    ///   Ссылка на этот экземпляр после завершения операции добавления.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="value" /> имеет значение <see langword="null" />, а <paramref name="startIndex" /> и <paramref name="charCount" /> не равны нулю.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="charCount" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   Значение параметра <paramref name="startIndex" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   Сумма значений <paramref name="startIndex" /> и <paramref name="charCount" /> превышает длину <paramref name="value" />.
    /// 
    ///   -или-
    /// 
    ///   Увеличение значения этого экземпляра может привести к превышению <see cref="P:System.Text.StringBuilder.MaxCapacity" />.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public unsafe StringBuilder Append(char[] value, int startIndex, int charCount)
    {
      if (startIndex < 0)
        throw new ArgumentOutOfRangeException(nameof (startIndex), Environment.GetResourceString("ArgumentOutOfRange_GenericPositive"));
      if (charCount < 0)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_GenericPositive"));
      if (CompatibilitySwitches.IsAppEarlierThanWindowsPhone8 && charCount == 0)
        return this;
      if (value == null)
      {
        if (startIndex == 0 && charCount == 0)
          return this;
        throw new ArgumentNullException(nameof (value));
      }
      if (charCount > value.Length - startIndex)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (charCount == 0)
        return this;
      fixed (char* chPtr = &value[startIndex])
        this.Append(chPtr, charCount);
      return this;
    }

    /// <summary>
    ///   Добавляет копию указанной строки к данному экземпляру.
    /// </summary>
    /// <param name="value">Добавляемая строка.</param>
    /// <returns>
    ///   Ссылка на этот экземпляр после завершения операции добавления.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Увеличение значения этого экземпляра может привести к превышению <see cref="P:System.Text.StringBuilder.MaxCapacity" />.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public unsafe StringBuilder Append(string value)
    {
      if (value != null)
      {
        char[] chunkChars = this.m_ChunkChars;
        int chunkLength = this.m_ChunkLength;
        int length = value.Length;
        int num = chunkLength + length;
        if (num < chunkChars.Length)
        {
          if (length <= 2)
          {
            if (length > 0)
              chunkChars[chunkLength] = value[0];
            if (length > 1)
              chunkChars[chunkLength + 1] = value[1];
          }
          else
          {
            string str = value;
            char* smem = (char*) str;
            if ((IntPtr) smem != IntPtr.Zero)
              smem += RuntimeHelpers.OffsetToStringData;
            fixed (char* dmem = &chunkChars[chunkLength])
              string.wstrcpy(dmem, smem, length);
            str = (string) null;
          }
          this.m_ChunkLength = num;
        }
        else
          this.AppendHelper(value);
      }
      return this;
    }

    [SecuritySafeCritical]
    private unsafe void AppendHelper(string value)
    {
      string str = value;
      char* chPtr = (char*) str;
      if ((IntPtr) chPtr != IntPtr.Zero)
        chPtr += RuntimeHelpers.OffsetToStringData;
      this.Append(chPtr, value.Length);
      str = (string) null;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal extern unsafe void ReplaceBufferInternal(char* newBuffer, int newLength);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal extern unsafe void ReplaceBufferAnsiInternal(sbyte* newBuffer, int newLength);

    /// <summary>
    ///   Добавляет копию указанной подстроки к данному экземпляру.
    /// </summary>
    /// <param name="value">
    ///   Строка, содержащая добавляемую подстроку.
    /// </param>
    /// <param name="startIndex">
    ///   Начальная позиция подстроки в пределах <paramref name="value" />.
    /// </param>
    /// <param name="count">
    ///   Число знаков в подстроке <paramref name="value" /> для добавления.
    /// </param>
    /// <returns>
    ///   Ссылка на этот экземпляр после завершения операции добавления.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="value" /> имеет значение <see langword="null" />, а <paramref name="startIndex" /> и <paramref name="count" /> не равны нулю.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="count" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="startIndex" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   Значение <paramref name="startIndex" /> + <paramref name="count" /> превышает длину <paramref name="value" />.
    /// 
    ///   -или-
    /// 
    ///   Увеличение значения этого экземпляра может привести к превышению <see cref="P:System.Text.StringBuilder.MaxCapacity" />.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public unsafe StringBuilder Append(string value, int startIndex, int count)
    {
      if (startIndex < 0)
        throw new ArgumentOutOfRangeException(nameof (startIndex), Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (count < 0)
        throw new ArgumentOutOfRangeException(nameof (count), Environment.GetResourceString("ArgumentOutOfRange_GenericPositive"));
      if (CompatibilitySwitches.IsAppEarlierThanWindowsPhone8 && count == 0)
        return this;
      if (value == null)
      {
        if (startIndex == 0 && count == 0)
          return this;
        throw new ArgumentNullException(nameof (value));
      }
      if (count == 0)
        return this;
      if (startIndex > value.Length - count)
        throw new ArgumentOutOfRangeException(nameof (startIndex), Environment.GetResourceString("ArgumentOutOfRange_Index"));
      string str = value;
      char* chPtr = (char*) str;
      if ((IntPtr) chPtr != IntPtr.Zero)
        chPtr += RuntimeHelpers.OffsetToStringData;
      this.Append(chPtr + startIndex, count);
      str = (string) null;
      return this;
    }

    /// <summary>
    ///   Добавляет знак завершения строки по умолчанию в конец текущего объекта <see cref="T:System.Text.StringBuilder" />.
    /// </summary>
    /// <returns>
    ///   Ссылка на этот экземпляр после завершения операции добавления.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Увеличение значения этого экземпляра может привести к превышению <see cref="P:System.Text.StringBuilder.MaxCapacity" />.
    /// </exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public StringBuilder AppendLine()
    {
      return this.Append(Environment.NewLine);
    }

    /// <summary>
    ///   Добавляет копию указанной строки и знак завершения строки по умолчанию в конец текущего объекта <see cref="T:System.Text.StringBuilder" />.
    /// </summary>
    /// <param name="value">Добавляемая строка.</param>
    /// <returns>
    ///   Ссылка на этот экземпляр после завершения операции добавления.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Увеличение значения этого экземпляра может привести к превышению <see cref="P:System.Text.StringBuilder.MaxCapacity" />.
    /// </exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public StringBuilder AppendLine(string value)
    {
      this.Append(value);
      return this.Append(Environment.NewLine);
    }

    /// <summary>
    ///   Копирует символы из указанного сегмента этого экземпляра в указанный массив <see cref="T:System.Char" />.
    /// </summary>
    /// <param name="sourceIndex">
    ///   Начальная позиция в этом экземпляре, откуда будут скопированы символы.
    ///    Индексация начинается с нуля.
    /// </param>
    /// <param name="destination">
    ///   Массив, в который копируются символы.
    /// </param>
    /// <param name="destinationIndex">
    ///   Начальная позиция в массиве <paramref name="destination" />, в которую копируются символы.
    ///    Индексация начинается с нуля.
    /// </param>
    /// <param name="count">Число копируемых знаков.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="destination" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение <paramref name="sourceIndex" />, <paramref name="destinationIndex" /> или <paramref name="count" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="sourceIndex" /> больше длины этого экземпляра.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="sourceIndex" /> + <paramref name="count" /> больше длины этого экземпляра.
    /// 
    ///   -или-
    /// 
    ///   Значение <paramref name="destinationIndex" /> + <paramref name="count" /> превышает длину <paramref name="destination" />.
    /// </exception>
    [ComVisible(false)]
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public void CopyTo(int sourceIndex, char[] destination, int destinationIndex, int count)
    {
      if (destination == null)
        throw new ArgumentNullException(nameof (destination));
      if (count < 0)
        throw new ArgumentOutOfRangeException(nameof (count), Environment.GetResourceString("Arg_NegativeArgCount"));
      if (destinationIndex < 0)
        throw new ArgumentOutOfRangeException(nameof (destinationIndex), Environment.GetResourceString("ArgumentOutOfRange_MustBeNonNegNum", (object) nameof (destinationIndex)));
      if (destinationIndex > destination.Length - count)
        throw new ArgumentException(Environment.GetResourceString("ArgumentOutOfRange_OffsetOut"));
      if ((uint) sourceIndex > (uint) this.Length)
        throw new ArgumentOutOfRangeException(nameof (sourceIndex), Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (sourceIndex > this.Length - count)
        throw new ArgumentException(Environment.GetResourceString("Arg_LongerThanSrcString"));
      StringBuilder stringBuilder = this;
      int num1 = sourceIndex + count;
      int destinationIndex1 = destinationIndex + count;
      while (count > 0)
      {
        int num2 = num1 - stringBuilder.m_ChunkOffset;
        if (num2 >= 0)
        {
          if (num2 > stringBuilder.m_ChunkLength)
            num2 = stringBuilder.m_ChunkLength;
          int count1 = count;
          int sourceIndex1 = num2 - count;
          if (sourceIndex1 < 0)
          {
            count1 += sourceIndex1;
            sourceIndex1 = 0;
          }
          destinationIndex1 -= count1;
          count -= count1;
          StringBuilder.ThreadSafeCopy(stringBuilder.m_ChunkChars, sourceIndex1, destination, destinationIndex1, count1);
        }
        stringBuilder = stringBuilder.m_ChunkPrevious;
      }
    }

    /// <summary>
    ///   Вставляет одну или более копий указанной строки в данный экземпляр на указанную позицию символа.
    /// </summary>
    /// <param name="index">
    ///   Позиция в данном экземпляре, с которой начинается вставка.
    /// </param>
    /// <param name="value">Строка, которую требуется вставить.</param>
    /// <param name="count">
    ///   Сколько раз следует вставить <paramref name="value" />.
    /// </param>
    /// <returns>Ссылка на этот экземпляр после завершения вставки.</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="index" /> имеет значение меньше нуля или больше текущей длины этого экземпляра.
    /// 
    ///   -или-
    /// 
    ///   Значение параметра <paramref name="count" /> меньше нуля.
    /// </exception>
    /// <exception cref="T:System.OutOfMemoryException">
    ///   Текущая длина этого объекта <see cref="T:System.Text.StringBuilder" /> плюс длина <paramref name="value" /><paramref name="count" /> раз превышает <see cref="P:System.Text.StringBuilder.MaxCapacity" />.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public unsafe StringBuilder Insert(int index, string value, int count)
    {
      if (count < 0)
        throw new ArgumentOutOfRangeException(nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      int length = this.Length;
      if ((uint) index > (uint) length)
        throw new ArgumentOutOfRangeException(nameof (index), Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (value == null || value.Length == 0 || count == 0)
        return this;
      long num = (long) value.Length * (long) count;
      if (num > (long) (this.MaxCapacity - this.Length))
        throw new OutOfMemoryException();
      StringBuilder chunk;
      int indexInChunk;
      this.MakeRoom(index, (int) num, out chunk, out indexInChunk, false);
      string str = value;
      char* chPtr = (char*) str;
      if ((IntPtr) chPtr != IntPtr.Zero)
        chPtr += RuntimeHelpers.OffsetToStringData;
      for (; count > 0; --count)
        this.ReplaceInPlaceAtChunk(ref chunk, ref indexInChunk, chPtr, value.Length);
      str = (string) null;
      return this;
    }

    /// <summary>
    ///   Удаляет указанный диапазон символов из данного экземпляра.
    /// </summary>
    /// <param name="startIndex">
    ///   Отсчитываемая с нуля позиция в данном экземпляре, с которой начинается удаление.
    /// </param>
    /// <param name="length">Число знаков для удаления.</param>
    /// <returns>
    ///   Ссылка на этот экземпляр после завершения операции удаления.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Если <paramref name="startIndex" /> или <paramref name="length" /> меньше нуля или <paramref name="startIndex" /> + <paramref name="length" /> больше длины этого экземпляра.
    /// </exception>
    [__DynamicallyInvokable]
    public StringBuilder Remove(int startIndex, int length)
    {
      if (length < 0)
        throw new ArgumentOutOfRangeException(nameof (length), Environment.GetResourceString("ArgumentOutOfRange_NegativeLength"));
      if (startIndex < 0)
        throw new ArgumentOutOfRangeException(nameof (startIndex), Environment.GetResourceString("ArgumentOutOfRange_StartIndex"));
      if (length > this.Length - startIndex)
        throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (this.Length == length && startIndex == 0)
      {
        this.Length = 0;
        return this;
      }
      if (length > 0)
      {
        StringBuilder chunk;
        int indexInChunk;
        this.Remove(startIndex, length, out chunk, out indexInChunk);
      }
      return this;
    }

    /// <summary>
    ///   Добавляет строковое представление указанного логического значения к данному экземпляру.
    /// </summary>
    /// <param name="value">Добавляемое логическое значение.</param>
    /// <returns>
    ///   Ссылка на этот экземпляр после завершения операции добавления.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Увеличение значения этого экземпляра может привести к превышению <see cref="P:System.Text.StringBuilder.MaxCapacity" />.
    /// </exception>
    [__DynamicallyInvokable]
    public StringBuilder Append(bool value)
    {
      return this.Append(value.ToString());
    }

    /// <summary>
    ///   Добавляет строковое представление указанного 8-разрядного целого числа со знаком к данному экземпляру.
    /// </summary>
    /// <param name="value">Добавляемое значение.</param>
    /// <returns>
    ///   Ссылка на этот экземпляр после завершения операции добавления.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Увеличение значения этого экземпляра может привести к превышению <see cref="P:System.Text.StringBuilder.MaxCapacity" />.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public StringBuilder Append(sbyte value)
    {
      return this.Append(value.ToString((IFormatProvider) CultureInfo.CurrentCulture));
    }

    /// <summary>
    ///   Добавляет строковое представление указанного 8-разрядного целого числа без знака к данному экземпляру.
    /// </summary>
    /// <param name="value">Добавляемое значение.</param>
    /// <returns>
    ///   Ссылка на этот экземпляр после завершения операции добавления.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Увеличение значения этого экземпляра может привести к превышению <see cref="P:System.Text.StringBuilder.MaxCapacity" />.
    /// </exception>
    [__DynamicallyInvokable]
    public StringBuilder Append(byte value)
    {
      return this.Append(value.ToString((IFormatProvider) CultureInfo.CurrentCulture));
    }

    /// <summary>
    ///   Добавляет строковое представление указанного объекта <see cref="T:System.Char" /> в данный экземпляр.
    /// </summary>
    /// <param name="value">
    ///   Единица кода в кодировке UTF-16 для добавления.
    /// </param>
    /// <returns>
    ///   Ссылка на этот экземпляр после завершения операции добавления.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Увеличение значения этого экземпляра может привести к превышению <see cref="P:System.Text.StringBuilder.MaxCapacity" />.
    /// </exception>
    [__DynamicallyInvokable]
    public StringBuilder Append(char value)
    {
      if (this.m_ChunkLength < this.m_ChunkChars.Length)
        this.m_ChunkChars[this.m_ChunkLength++] = value;
      else
        this.Append(value, 1);
      return this;
    }

    /// <summary>
    ///   Добавляет строковое представление указанного 16-разрядного целого числа со знаком к данному экземпляру.
    /// </summary>
    /// <param name="value">Добавляемое значение.</param>
    /// <returns>
    ///   Ссылка на этот экземпляр после завершения операции добавления.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Увеличение значения этого экземпляра может привести к превышению <see cref="P:System.Text.StringBuilder.MaxCapacity" />.
    /// </exception>
    [__DynamicallyInvokable]
    public StringBuilder Append(short value)
    {
      return this.Append(value.ToString((IFormatProvider) CultureInfo.CurrentCulture));
    }

    /// <summary>
    ///   Добавляет строковое представление указанного 32-разрядного целого числа со знаком к данному экземпляру.
    /// </summary>
    /// <param name="value">Добавляемое значение.</param>
    /// <returns>
    ///   Ссылка на этот экземпляр после завершения операции добавления.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Увеличение значения этого экземпляра может привести к превышению <see cref="P:System.Text.StringBuilder.MaxCapacity" />.
    /// </exception>
    [__DynamicallyInvokable]
    public StringBuilder Append(int value)
    {
      return this.Append(value.ToString((IFormatProvider) CultureInfo.CurrentCulture));
    }

    /// <summary>
    ///   Добавляет строковое представление указанного 64-разрядного целого числа со знаком к данному экземпляру.
    /// </summary>
    /// <param name="value">Добавляемое значение.</param>
    /// <returns>
    ///   Ссылка на этот экземпляр после завершения операции добавления.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Увеличение значения этого экземпляра может привести к превышению <see cref="P:System.Text.StringBuilder.MaxCapacity" />.
    /// </exception>
    [__DynamicallyInvokable]
    public StringBuilder Append(long value)
    {
      return this.Append(value.ToString((IFormatProvider) CultureInfo.CurrentCulture));
    }

    /// <summary>
    ///   Добавляет строковое представление указанного числа с плавающей запятой с обычной точностью к данному экземпляру.
    /// </summary>
    /// <param name="value">Добавляемое значение.</param>
    /// <returns>
    ///   Ссылка на этот экземпляр после завершения операции добавления.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Увеличение значения этого экземпляра может привести к превышению <see cref="P:System.Text.StringBuilder.MaxCapacity" />.
    /// </exception>
    [__DynamicallyInvokable]
    public StringBuilder Append(float value)
    {
      return this.Append(value.ToString((IFormatProvider) CultureInfo.CurrentCulture));
    }

    /// <summary>
    ///   Добавляет строковое представление указанного числа с плавающей запятой с удвоенной точностью к данному экземпляру.
    /// </summary>
    /// <param name="value">Добавляемое значение.</param>
    /// <returns>
    ///   Ссылка на этот экземпляр после завершения операции добавления.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Увеличение значения этого экземпляра может привести к превышению <see cref="P:System.Text.StringBuilder.MaxCapacity" />.
    /// </exception>
    [__DynamicallyInvokable]
    public StringBuilder Append(double value)
    {
      return this.Append(value.ToString((IFormatProvider) CultureInfo.CurrentCulture));
    }

    /// <summary>
    ///   Добавляет строковое представление указанного десятичного числа к данному экземпляру.
    /// </summary>
    /// <param name="value">Добавляемое значение.</param>
    /// <returns>
    ///   Ссылка на этот экземпляр после завершения операции добавления.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Увеличение значения этого экземпляра может привести к превышению <see cref="P:System.Text.StringBuilder.MaxCapacity" />.
    /// </exception>
    [__DynamicallyInvokable]
    public StringBuilder Append(Decimal value)
    {
      return this.Append(value.ToString((IFormatProvider) CultureInfo.CurrentCulture));
    }

    /// <summary>
    ///   Добавляет строковое представление указанного 16-разрядного целого числа без знака к данному экземпляру.
    /// </summary>
    /// <param name="value">Добавляемое значение.</param>
    /// <returns>
    ///   Ссылка на этот экземпляр после завершения операции добавления.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Увеличение значения этого экземпляра может привести к превышению <see cref="P:System.Text.StringBuilder.MaxCapacity" />.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public StringBuilder Append(ushort value)
    {
      return this.Append(value.ToString((IFormatProvider) CultureInfo.CurrentCulture));
    }

    /// <summary>
    ///   Добавляет строковое представление указанного 32-разрядного целого числа без знака к данному экземпляру.
    /// </summary>
    /// <param name="value">Добавляемое значение.</param>
    /// <returns>
    ///   Ссылка на этот экземпляр после завершения операции добавления.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Увеличение значения этого экземпляра может привести к превышению <see cref="P:System.Text.StringBuilder.MaxCapacity" />.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public StringBuilder Append(uint value)
    {
      return this.Append(value.ToString((IFormatProvider) CultureInfo.CurrentCulture));
    }

    /// <summary>
    ///   Добавляет строковое представление указанного 64-разрядного целого числа без знака к данному экземпляру.
    /// </summary>
    /// <param name="value">Добавляемое значение.</param>
    /// <returns>
    ///   Ссылка на этот экземпляр после завершения операции добавления.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Увеличение значения этого экземпляра может привести к превышению <see cref="P:System.Text.StringBuilder.MaxCapacity" />.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public StringBuilder Append(ulong value)
    {
      return this.Append(value.ToString((IFormatProvider) CultureInfo.CurrentCulture));
    }

    /// <summary>
    ///   Добавляет строковое представление указанного объекта к данному экземпляру.
    /// </summary>
    /// <param name="value">Добавляемый объект.</param>
    /// <returns>
    ///   Ссылка на этот экземпляр после завершения операции добавления.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Увеличение значения этого экземпляра может привести к превышению <see cref="P:System.Text.StringBuilder.MaxCapacity" />.
    /// </exception>
    [__DynamicallyInvokable]
    public StringBuilder Append(object value)
    {
      if (value == null)
        return this;
      return this.Append(value.ToString());
    }

    /// <summary>
    ///   Добавляет строковое представление символа Юникода в указанном массиве к данному экземпляру.
    /// </summary>
    /// <param name="value">Массив символов для добавления.</param>
    /// <returns>
    ///   Ссылка на этот экземпляр после завершения операции добавления.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Увеличение значения этого экземпляра может привести к превышению <see cref="P:System.Text.StringBuilder.MaxCapacity" />.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public unsafe StringBuilder Append(char[] value)
    {
      if (value != null && value.Length != 0)
      {
        fixed (char* chPtr = &value[0])
          this.Append(chPtr, value.Length);
      }
      return this;
    }

    /// <summary>
    ///   Вставляет строку в данный экземпляр на указанную позицию символа.
    /// </summary>
    /// <param name="index">
    ///   Позиция в данном экземпляре, с которой начинается вставка.
    /// </param>
    /// <param name="value">Строка, которую требуется вставить.</param>
    /// <returns>
    ///   Ссылка на этот экземпляр после завершения операции вставки.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="index" /> имеет значение меньше нуля или больше текущей длины этого экземпляра.
    /// 
    ///   -или-
    /// 
    ///   Текущая длина этого объекта <see cref="T:System.Text.StringBuilder" /> плюс длина <paramref name="value" /> превышает <see cref="P:System.Text.StringBuilder.MaxCapacity" />.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public unsafe StringBuilder Insert(int index, string value)
    {
      if ((uint) index > (uint) this.Length)
        throw new ArgumentOutOfRangeException(nameof (index), Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (value != null)
      {
        string str = value;
        char* chPtr = (char*) str;
        if ((IntPtr) chPtr != IntPtr.Zero)
          chPtr += RuntimeHelpers.OffsetToStringData;
        this.Insert(index, chPtr, value.Length);
        str = (string) null;
      }
      return this;
    }

    /// <summary>
    ///   Вставляет строковое представление логического значения в данный экземпляр на указанную позицию символа.
    /// </summary>
    /// <param name="index">
    ///   Позиция в данном экземпляре, с которой начинается вставка.
    /// </param>
    /// <param name="value">Значение, которое следует вставить.</param>
    /// <returns>
    ///   Ссылка на этот экземпляр после завершения операции вставки.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="index" /> имеет значение меньше нуля или больше длины этого экземпляра.
    /// </exception>
    /// <exception cref="T:System.OutOfMemoryException">
    ///   Увеличение значения этого экземпляра может привести к превышению <see cref="P:System.Text.StringBuilder.MaxCapacity" />.
    /// </exception>
    [__DynamicallyInvokable]
    public StringBuilder Insert(int index, bool value)
    {
      return this.Insert(index, value.ToString(), 1);
    }

    /// <summary>
    ///   Вставляет строковое представление указанного 8-разрядного знакового целого числа в данный экземпляр на указанную позицию символа.
    /// </summary>
    /// <param name="index">
    ///   Позиция в данном экземпляре, с которой начинается вставка.
    /// </param>
    /// <param name="value">Значение, которое следует вставить.</param>
    /// <returns>
    ///   Ссылка на этот экземпляр после завершения операции вставки.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="index" /> имеет значение меньше нуля или больше длины этого экземпляра.
    /// </exception>
    /// <exception cref="T:System.OutOfMemoryException">
    ///   Увеличение значения этого экземпляра может привести к превышению <see cref="P:System.Text.StringBuilder.MaxCapacity" />.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public StringBuilder Insert(int index, sbyte value)
    {
      return this.Insert(index, value.ToString((IFormatProvider) CultureInfo.CurrentCulture), 1);
    }

    /// <summary>
    ///   Вставляет строковое представление указанного 8-разрядного целого числа без знака в данный экземпляр на указанную позицию символа.
    /// </summary>
    /// <param name="index">
    ///   Позиция в данном экземпляре, с которой начинается вставка.
    /// </param>
    /// <param name="value">Значение, которое следует вставить.</param>
    /// <returns>
    ///   Ссылка на этот экземпляр после завершения операции вставки.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="index" /> имеет значение меньше нуля или больше длины этого экземпляра.
    /// </exception>
    /// <exception cref="T:System.OutOfMemoryException">
    ///   Увеличение значения этого экземпляра может привести к превышению <see cref="P:System.Text.StringBuilder.MaxCapacity" />.
    /// </exception>
    [__DynamicallyInvokable]
    public StringBuilder Insert(int index, byte value)
    {
      return this.Insert(index, value.ToString((IFormatProvider) CultureInfo.CurrentCulture), 1);
    }

    /// <summary>
    ///   Вставляет строковое представление указанного 16-разрядного знакового целого числа в данный экземпляр на указанную позицию символа.
    /// </summary>
    /// <param name="index">
    ///   Позиция в данном экземпляре, с которой начинается вставка.
    /// </param>
    /// <param name="value">Значение, которое следует вставить.</param>
    /// <returns>
    ///   Ссылка на этот экземпляр после завершения операции вставки.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="index" /> имеет значение меньше нуля или больше длины этого экземпляра.
    /// </exception>
    /// <exception cref="T:System.OutOfMemoryException">
    ///   Увеличение значения этого экземпляра может привести к превышению <see cref="P:System.Text.StringBuilder.MaxCapacity" />.
    /// </exception>
    [__DynamicallyInvokable]
    public StringBuilder Insert(int index, short value)
    {
      return this.Insert(index, value.ToString((IFormatProvider) CultureInfo.CurrentCulture), 1);
    }

    /// <summary>
    ///   Вставляет строковое представление указанного символа Юникода в данный экземпляр на указанную позицию символа.
    /// </summary>
    /// <param name="index">
    ///   Позиция в данном экземпляре, с которой начинается вставка.
    /// </param>
    /// <param name="value">Значение, которое следует вставить.</param>
    /// <returns>
    ///   Ссылка на этот экземпляр после завершения операции вставки.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="index" /> имеет значение меньше нуля или больше длины этого экземпляра.
    /// 
    ///   -или-
    /// 
    ///   Увеличение значения этого экземпляра может привести к превышению <see cref="P:System.Text.StringBuilder.MaxCapacity" />.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public unsafe StringBuilder Insert(int index, char value)
    {
      this.Insert(index, &value, 1);
      return this;
    }

    /// <summary>
    ///   Вставляет строковое представление указанного массива символов Юникода в данный экземпляр на указанную позицию символа.
    /// </summary>
    /// <param name="index">
    ///   Позиция в данном экземпляре, с которой начинается вставка.
    /// </param>
    /// <param name="value">Массив символов для вставки.</param>
    /// <returns>
    ///   Ссылка на этот экземпляр после завершения операции вставки.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="index" /> имеет значение меньше нуля или больше длины этого экземпляра.
    /// 
    ///   -или-
    /// 
    ///   Увеличение значения этого экземпляра может привести к превышению <see cref="P:System.Text.StringBuilder.MaxCapacity" />.
    /// </exception>
    [__DynamicallyInvokable]
    public StringBuilder Insert(int index, char[] value)
    {
      if ((uint) index > (uint) this.Length)
        throw new ArgumentOutOfRangeException(nameof (index), Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (value != null)
        this.Insert(index, value, 0, value.Length);
      return this;
    }

    /// <summary>
    ///   Вставляет строковое представление указанного подмассива символов Юникода в данный экземпляр на указанную позицию символа.
    /// </summary>
    /// <param name="index">
    ///   Позиция в данном экземпляре, с которой начинается вставка.
    /// </param>
    /// <param name="value">Массив символов.</param>
    /// <param name="startIndex">
    ///   Начальный индекс внутри <paramref name="value" />.
    /// </param>
    /// <param name="charCount">Число символов для вставки.</param>
    /// <returns>
    ///   Ссылка на этот экземпляр после завершения операции вставки.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="value" /> имеет значение <see langword="null" />, а <paramref name="startIndex" /> и <paramref name="charCount" /> не равны нулю.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение <paramref name="index" />, <paramref name="startIndex" /> или <paramref name="charCount" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="index" /> больше длины этого экземпляра.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="startIndex" /> плюс <paramref name="charCount" /> не является позицией в <paramref name="value" />.
    /// 
    ///   -или-
    /// 
    ///   Увеличение значения этого экземпляра может привести к превышению <see cref="P:System.Text.StringBuilder.MaxCapacity" />.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public unsafe StringBuilder Insert(int index, char[] value, int startIndex, int charCount)
    {
      int length = this.Length;
      if ((uint) index > (uint) length)
        throw new ArgumentOutOfRangeException(nameof (index), Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (value == null)
      {
        if (startIndex == 0 && charCount == 0)
          return this;
        throw new ArgumentNullException(Environment.GetResourceString("ArgumentNull_String"));
      }
      if (startIndex < 0)
        throw new ArgumentOutOfRangeException(nameof (startIndex), Environment.GetResourceString("ArgumentOutOfRange_StartIndex"));
      if (charCount < 0)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_GenericPositive"));
      if (startIndex > value.Length - charCount)
        throw new ArgumentOutOfRangeException(nameof (startIndex), Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (charCount > 0)
      {
        fixed (char* chPtr = &value[startIndex])
          this.Insert(index, chPtr, charCount);
      }
      return this;
    }

    /// <summary>
    ///   Вставляет строковое представление указанного 32-разрядного знакового целого числа в данный экземпляр на указанную позицию символа.
    /// </summary>
    /// <param name="index">
    ///   Позиция в данном экземпляре, с которой начинается вставка.
    /// </param>
    /// <param name="value">Значение, которое следует вставить.</param>
    /// <returns>
    ///   Ссылка на этот экземпляр после завершения операции вставки.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="index" /> имеет значение меньше нуля или больше длины этого экземпляра.
    /// </exception>
    /// <exception cref="T:System.OutOfMemoryException">
    ///   Увеличение значения этого экземпляра может привести к превышению <see cref="P:System.Text.StringBuilder.MaxCapacity" />.
    /// </exception>
    [__DynamicallyInvokable]
    public StringBuilder Insert(int index, int value)
    {
      return this.Insert(index, value.ToString((IFormatProvider) CultureInfo.CurrentCulture), 1);
    }

    /// <summary>
    ///   Вставляет строковое представление 64-разрядного знакового целого числа в данный экземпляр на указанную позицию символа.
    /// </summary>
    /// <param name="index">
    ///   Позиция в данном экземпляре, с которой начинается вставка.
    /// </param>
    /// <param name="value">Значение, которое следует вставить.</param>
    /// <returns>
    ///   Ссылка на этот экземпляр после завершения операции вставки.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="index" /> имеет значение меньше нуля или больше длины этого экземпляра.
    /// </exception>
    /// <exception cref="T:System.OutOfMemoryException">
    ///   Увеличение значения этого экземпляра может привести к превышению <see cref="P:System.Text.StringBuilder.MaxCapacity" />.
    /// </exception>
    [__DynamicallyInvokable]
    public StringBuilder Insert(int index, long value)
    {
      return this.Insert(index, value.ToString((IFormatProvider) CultureInfo.CurrentCulture), 1);
    }

    /// <summary>
    ///   Вставляет строковое представление числа одинарной точности с плавающей запятой с обычной точностью в данный экземпляр на указанную позицию символа.
    /// </summary>
    /// <param name="index">
    ///   Позиция в данном экземпляре, с которой начинается вставка.
    /// </param>
    /// <param name="value">Значение, которое следует вставить.</param>
    /// <returns>
    ///   Ссылка на этот экземпляр после завершения операции вставки.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="index" /> имеет значение меньше нуля или больше длины этого экземпляра.
    /// </exception>
    /// <exception cref="T:System.OutOfMemoryException">
    ///   Увеличение значения этого экземпляра может привести к превышению <see cref="P:System.Text.StringBuilder.MaxCapacity" />.
    /// </exception>
    [__DynamicallyInvokable]
    public StringBuilder Insert(int index, float value)
    {
      return this.Insert(index, value.ToString((IFormatProvider) CultureInfo.CurrentCulture), 1);
    }

    /// <summary>
    ///   Вставляет строковое представление числа с плавающей запятой с удвоенной точностью в данный экземпляр на указанную позицию символа.
    /// </summary>
    /// <param name="index">
    ///   Позиция в данном экземпляре, с которой начинается вставка.
    /// </param>
    /// <param name="value">Значение, которое следует вставить.</param>
    /// <returns>
    ///   Ссылка на этот экземпляр после завершения операции вставки.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="index" /> имеет значение меньше нуля или больше длины этого экземпляра.
    /// </exception>
    /// <exception cref="T:System.OutOfMemoryException">
    ///   Увеличение значения этого экземпляра может привести к превышению <see cref="P:System.Text.StringBuilder.MaxCapacity" />.
    /// </exception>
    [__DynamicallyInvokable]
    public StringBuilder Insert(int index, double value)
    {
      return this.Insert(index, value.ToString((IFormatProvider) CultureInfo.CurrentCulture), 1);
    }

    /// <summary>
    ///   Вставляет строковое представление десятичного числа в данный экземпляр на указанную позицию символа.
    /// </summary>
    /// <param name="index">
    ///   Позиция в данном экземпляре, с которой начинается вставка.
    /// </param>
    /// <param name="value">Значение, которое следует вставить.</param>
    /// <returns>
    ///   Ссылка на этот экземпляр после завершения операции вставки.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="index" /> имеет значение меньше нуля или больше длины этого экземпляра.
    /// </exception>
    /// <exception cref="T:System.OutOfMemoryException">
    ///   Увеличение значения этого экземпляра может привести к превышению <see cref="P:System.Text.StringBuilder.MaxCapacity" />.
    /// </exception>
    [__DynamicallyInvokable]
    public StringBuilder Insert(int index, Decimal value)
    {
      return this.Insert(index, value.ToString((IFormatProvider) CultureInfo.CurrentCulture), 1);
    }

    /// <summary>
    ///   Вставляет строковое представление 16-разрядного целого числа без знака в данный экземпляр на указанную позицию символа.
    /// </summary>
    /// <param name="index">
    ///   Позиция в данном экземпляре, с которой начинается вставка.
    /// </param>
    /// <param name="value">Значение, которое следует вставить.</param>
    /// <returns>
    ///   Ссылка на этот экземпляр после завершения операции вставки.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="index" /> имеет значение меньше нуля или больше длины этого экземпляра.
    /// </exception>
    /// <exception cref="T:System.OutOfMemoryException">
    ///   Увеличение значения этого экземпляра может привести к превышению <see cref="P:System.Text.StringBuilder.MaxCapacity" />.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public StringBuilder Insert(int index, ushort value)
    {
      return this.Insert(index, value.ToString((IFormatProvider) CultureInfo.CurrentCulture), 1);
    }

    /// <summary>
    ///   Вставляет строковое представление 32-разрядного целого числа без знака в данный экземпляр на указанную позицию символа.
    /// </summary>
    /// <param name="index">
    ///   Позиция в данном экземпляре, с которой начинается вставка.
    /// </param>
    /// <param name="value">Значение, которое следует вставить.</param>
    /// <returns>
    ///   Ссылка на этот экземпляр после завершения операции вставки.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="index" /> имеет значение меньше нуля или больше длины этого экземпляра.
    /// </exception>
    /// <exception cref="T:System.OutOfMemoryException">
    ///   Увеличение значения этого экземпляра может привести к превышению <see cref="P:System.Text.StringBuilder.MaxCapacity" />.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public StringBuilder Insert(int index, uint value)
    {
      return this.Insert(index, value.ToString((IFormatProvider) CultureInfo.CurrentCulture), 1);
    }

    /// <summary>
    ///   Вставляет строковое представление 64-разрядного целого числа без знака в данный экземпляр на указанную позицию символа.
    /// </summary>
    /// <param name="index">
    ///   Позиция в данном экземпляре, с которой начинается вставка.
    /// </param>
    /// <param name="value">Значение, которое следует вставить.</param>
    /// <returns>
    ///   Ссылка на этот экземпляр после завершения операции вставки.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="index" /> имеет значение меньше нуля или больше длины этого экземпляра.
    /// </exception>
    /// <exception cref="T:System.OutOfMemoryException">
    ///   Увеличение значения этого экземпляра может привести к превышению <see cref="P:System.Text.StringBuilder.MaxCapacity" />.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public StringBuilder Insert(int index, ulong value)
    {
      return this.Insert(index, value.ToString((IFormatProvider) CultureInfo.CurrentCulture), 1);
    }

    /// <summary>
    ///   Вставляет строковое представление объекта в данный экземпляр на указанную позицию символа.
    /// </summary>
    /// <param name="index">
    ///   Позиция в данном экземпляре, с которой начинается вставка.
    /// </param>
    /// <param name="value">
    ///   Объект для вставки или значение <see langword="null" />.
    /// </param>
    /// <returns>
    ///   Ссылка на этот экземпляр после завершения операции вставки.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="index" /> имеет значение меньше нуля или больше длины этого экземпляра.
    /// </exception>
    /// <exception cref="T:System.OutOfMemoryException">
    ///   Увеличение значения этого экземпляра может привести к превышению <see cref="P:System.Text.StringBuilder.MaxCapacity" />.
    /// </exception>
    [__DynamicallyInvokable]
    public StringBuilder Insert(int index, object value)
    {
      if (value == null)
        return this;
      return this.Insert(index, value.ToString(), 1);
    }

    /// <summary>
    ///   Добавляет к данному экземпляру строку, возвращаемую в результате обработки строки составного формата, содержащей ноль или более элементов формата.
    ///    Каждый элемент формата заменяется строковым представлением одного аргумента.
    /// </summary>
    /// <param name="format">
    ///   Строка составного формата (см. примечания).
    /// </param>
    /// <param name="arg0">Объект для форматирования.</param>
    /// <returns>
    ///   Ссылка на данный экземпляр с добавленным <paramref name="format" />.
    ///    Каждый элемент формата в параметре <paramref name="format" /> заменяется строковым представлением параметра <paramref name="arg0" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="format" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="format" /> недопустим.
    /// 
    ///   -или-
    /// 
    ///   Индекс элемента формата меньше нуля или не меньше 1.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Длина развернутой строки превысила бы <see cref="P:System.Text.StringBuilder.MaxCapacity" />.
    /// </exception>
    [__DynamicallyInvokable]
    public StringBuilder AppendFormat(string format, object arg0)
    {
      return this.AppendFormatHelper((IFormatProvider) null, format, new ParamsArray(arg0));
    }

    /// <summary>
    ///   Добавляет к данному экземпляру строку, возвращаемую в результате обработки строки составного формата, содержащей ноль или более элементов формата.
    ///    Каждый элемент формата заменяется строковым представлением любого из двух аргументов.
    /// </summary>
    /// <param name="format">
    ///   Строка составного формата (см. примечания).
    /// </param>
    /// <param name="arg0">Первый объект для форматирования.</param>
    /// <param name="arg1">Второй объект для форматирования.</param>
    /// <returns>
    ///   Ссылка на данный экземпляр с добавленным <paramref name="format" />.
    ///    Каждый элемент формата в параметре <paramref name="format" /> заменяется строковым представлением соответствующего аргумента объекта.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="format" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="format" /> недопустим.
    /// 
    ///   -или-
    /// 
    ///   Индекс элемента формата меньше нуля или больше либо равен двум.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Длина развернутой строки превысила бы <see cref="P:System.Text.StringBuilder.MaxCapacity" />.
    /// </exception>
    [__DynamicallyInvokable]
    public StringBuilder AppendFormat(string format, object arg0, object arg1)
    {
      return this.AppendFormatHelper((IFormatProvider) null, format, new ParamsArray(arg0, arg1));
    }

    /// <summary>
    ///   Добавляет к данному экземпляру строку, возвращаемую в результате обработки строки составного формата, содержащей ноль или более элементов формата.
    ///    Каждый элемент формата заменяется строковым представлением любого из трех аргументов.
    /// </summary>
    /// <param name="format">
    ///   Строка составного формата (см. примечания).
    /// </param>
    /// <param name="arg0">Первый объект для форматирования.</param>
    /// <param name="arg1">Второй объект для форматирования.</param>
    /// <param name="arg2">Третий объект для форматирования.</param>
    /// <returns>
    ///   Ссылка на данный экземпляр с добавленным <paramref name="format" />.
    ///    Каждый элемент формата в параметре <paramref name="format" /> заменяется строковым представлением соответствующего аргумента объекта.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="format" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="format" /> недопустим.
    /// 
    ///   -или-
    /// 
    ///   Индекс элемента формата меньше нуля либо больше или равен 3.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Длина развернутой строки превысила бы <see cref="P:System.Text.StringBuilder.MaxCapacity" />.
    /// </exception>
    [__DynamicallyInvokable]
    public StringBuilder AppendFormat(string format, object arg0, object arg1, object arg2)
    {
      return this.AppendFormatHelper((IFormatProvider) null, format, new ParamsArray(arg0, arg1, arg2));
    }

    /// <summary>
    ///   Добавляет к данному экземпляру строку, возвращаемую в результате обработки строки составного формата, содержащей ноль или более элементов формата.
    ///    Каждый элемент формата заменяется строковым представлением соответствующего аргумента в массиве параметров.
    /// </summary>
    /// <param name="format">
    ///   Строка составного формата (см. примечания).
    /// </param>
    /// <param name="args">Массив объектов для форматирования.</param>
    /// <returns>
    ///   Ссылка на данный экземпляр с добавленным <paramref name="format" />.
    ///    Каждый элемент формата в параметре <paramref name="format" /> заменяется строковым представлением соответствующего аргумента объекта.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="format" /> или <paramref name="args" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="format" /> недопустим.
    /// 
    ///   -или-
    /// 
    ///   Индекс элемента формата меньше нуля или больше либо равен длине массива <paramref name="args" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Длина развернутой строки превысила бы <see cref="P:System.Text.StringBuilder.MaxCapacity" />.
    /// </exception>
    [__DynamicallyInvokable]
    public StringBuilder AppendFormat(string format, params object[] args)
    {
      if (args == null)
        throw new ArgumentNullException(format == null ? nameof (format) : nameof (args));
      return this.AppendFormatHelper((IFormatProvider) null, format, new ParamsArray(args));
    }

    /// <summary>
    ///   Добавляет к данному экземпляру строку, возвращаемую в результате обработки строки составного формата, содержащей ноль или более элементов формата.
    ///    Каждый элемент формата заменяется строковым представлением одного аргумента с использованием указанного поставщика формата.
    /// </summary>
    /// <param name="provider">
    ///   Объект, предоставляющий сведения о форматировании для определенного языка и региональных параметров.
    /// </param>
    /// <param name="format">
    ///   Строка составного формата (см. примечания).
    /// </param>
    /// <param name="arg0">Объект для форматирования.</param>
    /// <returns>
    ///   Ссылка на этот экземпляр после завершения операции добавления.
    ///    После операции добавления этот экземпляр содержит все данные, существовавшие до операции, к которым добавляется копия параметра <paramref name="format" />, где все спецификации форматирования заменяются представлением <paramref name="arg0" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="format" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="format" /> недопустим.
    /// 
    ///   -или-
    /// 
    ///   Индекс элемента формата меньше нуля или больше либо равен единице.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Длина развернутой строки превысила бы <see cref="P:System.Text.StringBuilder.MaxCapacity" />.
    /// </exception>
    [__DynamicallyInvokable]
    public StringBuilder AppendFormat(IFormatProvider provider, string format, object arg0)
    {
      return this.AppendFormatHelper(provider, format, new ParamsArray(arg0));
    }

    /// <summary>
    ///   Добавляет к данному экземпляру строку, возвращаемую в результате обработки строки составного формата, содержащей ноль или более элементов формата.
    ///    Каждый элемент формата заменяется строковым представлением любого из двух аргументов с помощью указанного поставщика формата.
    /// </summary>
    /// <param name="provider">
    ///   Объект, предоставляющий сведения о форматировании для определенного языка и региональных параметров.
    /// </param>
    /// <param name="format">
    ///   Строка составного формата (см. примечания).
    /// </param>
    /// <param name="arg0">Первый объект для форматирования.</param>
    /// <param name="arg1">Второй объект для форматирования.</param>
    /// <returns>
    ///   Ссылка на этот экземпляр после завершения операции добавления.
    ///    После операции добавления этот экземпляр содержит все данные, существовавшие до операции, к которым добавляется копия параметра <paramref name="format" />, где все спецификации форматирования заменяются представлением строки соответствующего аргумента объекта.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="format" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="format" /> недопустим.
    /// 
    ///   -или-
    /// 
    ///   Индекс элемента формата меньше нуля или больше либо равен двум.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Длина развернутой строки превысит <see cref="P:System.Text.StringBuilder.MaxCapacity" />.
    /// </exception>
    [__DynamicallyInvokable]
    public StringBuilder AppendFormat(IFormatProvider provider, string format, object arg0, object arg1)
    {
      return this.AppendFormatHelper(provider, format, new ParamsArray(arg0, arg1));
    }

    /// <summary>
    ///   Добавляет к данному экземпляру строку, возвращаемую в результате обработки строки составного формата, содержащей ноль или более элементов формата.
    ///    Каждый элемент формата заменяется строковым представлением любого из трех аргументов с помощью указанного поставщика формата.
    /// </summary>
    /// <param name="provider">
    ///   Объект, предоставляющий сведения о форматировании для определенного языка и региональных параметров.
    /// </param>
    /// <param name="format">
    ///   Строка составного формата (см. примечания).
    /// </param>
    /// <param name="arg0">Первый объект для форматирования.</param>
    /// <param name="arg1">Второй объект для форматирования.</param>
    /// <param name="arg2">Третий объект для форматирования.</param>
    /// <returns>
    ///   Ссылка на этот экземпляр после завершения операции добавления.
    ///    После операции добавления этот экземпляр содержит все данные, существовавшие до операции, к которым добавляется копия параметра <paramref name="format" />, где все спецификации форматирования заменяются представлением строки соответствующего аргумента объекта.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="format" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="format" /> недопустим.
    /// 
    ///   -или-
    /// 
    ///   Индекс элемента формата меньше нуля или не меньше трех.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Длина развернутой строки превысила бы <see cref="P:System.Text.StringBuilder.MaxCapacity" />.
    /// </exception>
    [__DynamicallyInvokable]
    public StringBuilder AppendFormat(IFormatProvider provider, string format, object arg0, object arg1, object arg2)
    {
      return this.AppendFormatHelper(provider, format, new ParamsArray(arg0, arg1, arg2));
    }

    /// <summary>
    ///   Добавляет к данному экземпляру строку, возвращаемую в результате обработки строки составного формата, содержащей ноль или более элементов формата.
    ///    Каждый элемент формата заменяется строковым представлением соответствующего аргумента в массиве параметров с помощью указанного поставщика формата.
    /// </summary>
    /// <param name="provider">
    ///   Объект, предоставляющий сведения о форматировании для определенного языка и региональных параметров.
    /// </param>
    /// <param name="format">
    ///   Строка составного формата (см. примечания).
    /// </param>
    /// <param name="args">Массив объектов для форматирования.</param>
    /// <returns>
    ///   Ссылка на этот экземпляр после завершения операции добавления.
    ///    После операции добавления этот экземпляр содержит все данные, существовавшие до операции, к которым добавляется копия параметра <paramref name="format" />, где все спецификации форматирования заменяются представлением строки соответствующего аргумента объекта.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="format" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="format" /> недопустим.
    /// 
    ///   -или-
    /// 
    ///   Индекс элемента формата меньше нуля или больше либо равен длине массива <paramref name="args" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Длина развернутой строки превысит <see cref="P:System.Text.StringBuilder.MaxCapacity" />.
    /// </exception>
    [__DynamicallyInvokable]
    public StringBuilder AppendFormat(IFormatProvider provider, string format, params object[] args)
    {
      if (args == null)
        throw new ArgumentNullException(format == null ? nameof (format) : nameof (args));
      return this.AppendFormatHelper(provider, format, new ParamsArray(args));
    }

    private static void FormatError()
    {
      throw new FormatException(Environment.GetResourceString("Format_InvalidString"));
    }

    internal StringBuilder AppendFormatHelper(IFormatProvider provider, string format, ParamsArray args)
    {
      if (format == null)
        throw new ArgumentNullException(nameof (format));
      int index1 = 0;
      int length = format.Length;
      char minValue = char.MinValue;
      ICustomFormatter customFormatter = (ICustomFormatter) null;
      if (provider != null)
        customFormatter = (ICustomFormatter) provider.GetFormat(typeof (ICustomFormatter));
      while (true)
      {
        bool flag;
        int repeatCount;
        do
        {
          int num1 = index1;
          int num2 = index1;
          while (index1 < length)
          {
            minValue = format[index1];
            ++index1;
            if (minValue == '}')
            {
              if (index1 < length && format[index1] == '}')
                ++index1;
              else
                StringBuilder.FormatError();
            }
            if (minValue == '{')
            {
              if (index1 < length && format[index1] == '{')
              {
                ++index1;
              }
              else
              {
                --index1;
                break;
              }
            }
            this.Append(minValue);
          }
          if (index1 != length)
          {
            int index2 = index1 + 1;
            if (index2 == length || (minValue = format[index2]) < '0' || minValue > '9')
              StringBuilder.FormatError();
            int index3 = 0;
            do
            {
              index3 = index3 * 10 + (int) minValue - 48;
              ++index2;
              if (index2 == length)
                StringBuilder.FormatError();
              minValue = format[index2];
            }
            while (minValue >= '0' && minValue <= '9' && index3 < 1000000);
            if (index3 >= args.Length)
              throw new FormatException(Environment.GetResourceString("Format_IndexOutOfRange"));
            while (index2 < length && (minValue = format[index2]) == ' ')
              ++index2;
            flag = false;
            int num3 = 0;
            if (minValue == ',')
            {
              ++index2;
              while (index2 < length && format[index2] == ' ')
                ++index2;
              if (index2 == length)
                StringBuilder.FormatError();
              minValue = format[index2];
              if (minValue == '-')
              {
                flag = true;
                ++index2;
                if (index2 == length)
                  StringBuilder.FormatError();
                minValue = format[index2];
              }
              if (minValue < '0' || minValue > '9')
                StringBuilder.FormatError();
              do
              {
                num3 = num3 * 10 + (int) minValue - 48;
                ++index2;
                if (index2 == length)
                  StringBuilder.FormatError();
                minValue = format[index2];
              }
              while (minValue >= '0' && minValue <= '9' && num3 < 1000000);
            }
            while (index2 < length && (minValue = format[index2]) == ' ')
              ++index2;
            object obj = args[index3];
            StringBuilder stringBuilder = (StringBuilder) null;
            if (minValue == ':')
            {
              int index4 = index2 + 1;
              num1 = index4;
              num2 = index4;
              while (true)
              {
                if (index4 == length)
                  StringBuilder.FormatError();
                minValue = format[index4];
                ++index4;
                switch (minValue)
                {
                  case '{':
                    if (index4 < length && format[index4] == '{')
                    {
                      ++index4;
                      break;
                    }
                    StringBuilder.FormatError();
                    break;
                  case '}':
                    if (index4 < length && format[index4] == '}')
                    {
                      ++index4;
                      break;
                    }
                    goto label_53;
                }
                if (stringBuilder == null)
                  stringBuilder = new StringBuilder();
                stringBuilder.Append(minValue);
              }
label_53:
              index2 = index4 - 1;
            }
            if (minValue != '}')
              StringBuilder.FormatError();
            index1 = index2 + 1;
            string format1 = (string) null;
            string str = (string) null;
            if (customFormatter != null)
            {
              if (stringBuilder != null)
                format1 = stringBuilder.ToString();
              str = customFormatter.Format(format1, obj, provider);
            }
            if (str == null)
            {
              IFormattable formattable = obj as IFormattable;
              if (formattable != null)
              {
                if (format1 == null && stringBuilder != null)
                  format1 = stringBuilder.ToString();
                str = formattable.ToString(format1, provider);
              }
              else if (obj != null)
                str = obj.ToString();
            }
            if (str == null)
              str = string.Empty;
            repeatCount = num3 - str.Length;
            if (!flag && repeatCount > 0)
              this.Append(' ', repeatCount);
            this.Append(str);
          }
          else
            goto label_76;
        }
        while (!flag || repeatCount <= 0);
        this.Append(' ', repeatCount);
      }
label_76:
      return this;
    }

    /// <summary>
    ///   Замещает все вхождения указанной строки в данном экземпляре на другую указанную строку.
    /// </summary>
    /// <param name="oldValue">Замещаемая строка.</param>
    /// <param name="newValue">
    ///   Строка, замещающая <paramref name="oldValue" />, или <see langword="null" />.
    /// </param>
    /// <returns>
    ///   Ссылка на данный экземпляр со всеми экземплярами <paramref name="oldValue" /> заменяется на <paramref name="newValue" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="oldValue" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Длина параметра <paramref name="oldValue" /> равна нулю.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Увеличение значения этого экземпляра может привести к превышению <see cref="P:System.Text.StringBuilder.MaxCapacity" />.
    /// </exception>
    [__DynamicallyInvokable]
    public StringBuilder Replace(string oldValue, string newValue)
    {
      return this.Replace(oldValue, newValue, 0, this.Length);
    }

    /// <summary>
    ///   Возвращает значение, показывающее, равен ли данный экземпляр заданному объекту.
    /// </summary>
    /// <param name="sb">
    ///   Объект, сравниваемый с этим экземпляром, или значение <see langword="null" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если данный экземпляр и <paramref name="sb" /> имеют одинаковую строку, <see cref="P:System.Text.StringBuilder.Capacity" /> и значения <see cref="P:System.Text.StringBuilder.MaxCapacity" />; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool Equals(StringBuilder sb)
    {
      if (sb == null || this.Capacity != sb.Capacity || (this.MaxCapacity != sb.MaxCapacity || this.Length != sb.Length))
        return false;
      if (sb == this)
        return true;
      StringBuilder stringBuilder1 = this;
      int index1 = stringBuilder1.m_ChunkLength;
      StringBuilder stringBuilder2 = sb;
      int index2 = stringBuilder2.m_ChunkLength;
      do
      {
        --index1;
        --index2;
        for (; index1 < 0; index1 = stringBuilder1.m_ChunkLength + index1)
        {
          stringBuilder1 = stringBuilder1.m_ChunkPrevious;
          if (stringBuilder1 == null)
            break;
        }
        for (; index2 < 0; index2 = stringBuilder2.m_ChunkLength + index2)
        {
          stringBuilder2 = stringBuilder2.m_ChunkPrevious;
          if (stringBuilder2 == null)
            break;
        }
        if (index1 < 0)
          return index2 < 0;
      }
      while (index2 >= 0 && (int) stringBuilder1.m_ChunkChars[index1] == (int) stringBuilder2.m_ChunkChars[index2]);
      return false;
    }

    /// <summary>
    ///   Замещает все вхождения указанной строки в подстроке данного экземпляра на другую указанную строку.
    /// </summary>
    /// <param name="oldValue">Замещаемая строка.</param>
    /// <param name="newValue">
    ///   Строка, замещающая <paramref name="oldValue" />, или <see langword="null" />.
    /// </param>
    /// <param name="startIndex">
    ///   Позиция в данном экземпляре, с которой начинается подстрока.
    /// </param>
    /// <param name="count">Длина подстроки.</param>
    /// <returns>
    ///   Ссылка на данный экземпляр со всеми экземплярами <paramref name="oldValue" /> заменяется на <paramref name="newValue" /> в диапазоне от <paramref name="startIndex" /> до <paramref name="startIndex" /> + <paramref name="count" /> – 1.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="oldValue" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Длина параметра <paramref name="oldValue" /> равна нулю.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="startIndex" /> или <paramref name="count" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="startIndex" /> плюс <paramref name="count" /> указывает на позицию символа за пределами данного экземпляра.
    /// 
    ///   -или-
    /// 
    ///   Увеличение значения этого экземпляра может привести к превышению <see cref="P:System.Text.StringBuilder.MaxCapacity" />.
    /// </exception>
    [__DynamicallyInvokable]
    public StringBuilder Replace(string oldValue, string newValue, int startIndex, int count)
    {
      int length = this.Length;
      if ((uint) startIndex > (uint) length)
        throw new ArgumentOutOfRangeException(nameof (startIndex), Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (count < 0 || startIndex > length - count)
        throw new ArgumentOutOfRangeException(nameof (count), Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (oldValue == null)
        throw new ArgumentNullException(nameof (oldValue));
      if (oldValue.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), nameof (oldValue));
      if (newValue == null)
        newValue = "";
      int num1 = newValue.Length - oldValue.Length;
      int[] replacements = (int[]) null;
      int replacementsCount = 0;
      StringBuilder chunkForIndex = this.FindChunkForIndex(startIndex);
      int indexInChunk = startIndex - chunkForIndex.m_ChunkOffset;
      while (count > 0)
      {
        if (this.StartsWith(chunkForIndex, indexInChunk, count, oldValue))
        {
          if (replacements == null)
            replacements = new int[5];
          else if (replacementsCount >= replacements.Length)
          {
            int[] numArray = new int[replacements.Length * 3 / 2 + 4];
            Array.Copy((Array) replacements, (Array) numArray, replacements.Length);
            replacements = numArray;
          }
          replacements[replacementsCount++] = indexInChunk;
          indexInChunk += oldValue.Length;
          count -= oldValue.Length;
        }
        else
        {
          ++indexInChunk;
          --count;
        }
        if (indexInChunk >= chunkForIndex.m_ChunkLength || count == 0)
        {
          int num2 = indexInChunk + chunkForIndex.m_ChunkOffset;
          this.ReplaceAllInChunk(replacements, replacementsCount, chunkForIndex, oldValue.Length, newValue);
          int index = num2 + (newValue.Length - oldValue.Length) * replacementsCount;
          replacementsCount = 0;
          chunkForIndex = this.FindChunkForIndex(index);
          indexInChunk = index - chunkForIndex.m_ChunkOffset;
        }
      }
      return this;
    }

    /// <summary>
    ///   Замещает все вхождения указанного символа в данном экземпляре на другой указанный знак.
    /// </summary>
    /// <param name="oldChar">Замещаемый символ.</param>
    /// <param name="newChar">
    ///   Символ, замещающий <paramref name="oldChar" />.
    /// </param>
    /// <returns>
    ///   Ссылка на данный экземпляр с помощью <paramref name="oldChar" /> заменяется на <paramref name="newChar" />.
    /// </returns>
    [__DynamicallyInvokable]
    public StringBuilder Replace(char oldChar, char newChar)
    {
      return this.Replace(oldChar, newChar, 0, this.Length);
    }

    /// <summary>
    ///   Замещает все вхождения указанного символа в подстроке данного экземпляра на другой указанный символ.
    /// </summary>
    /// <param name="oldChar">Замещаемый символ.</param>
    /// <param name="newChar">
    ///   Символ, замещающий <paramref name="oldChar" />.
    /// </param>
    /// <param name="startIndex">
    ///   Позиция в данном экземпляре, с которой начинается подстрока.
    /// </param>
    /// <param name="count">Длина подстроки.</param>
    /// <returns>
    ///   Ссылка на данный экземпляр с помощью <paramref name="oldChar" /> заменяется на <paramref name="newChar" /> в диапазоне от <paramref name="startIndex" /> до <paramref name="startIndex" /> + <paramref name="count" /> – 1.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Сумма <paramref name="startIndex" /> и <paramref name="count" /> больше длины значения этого экземпляра.
    /// 
    ///   -или-
    /// 
    ///   Значение параметра <paramref name="startIndex" /> или <paramref name="count" /> меньше нуля.
    /// </exception>
    [__DynamicallyInvokable]
    public StringBuilder Replace(char oldChar, char newChar, int startIndex, int count)
    {
      int length = this.Length;
      if ((uint) startIndex > (uint) length)
        throw new ArgumentOutOfRangeException(nameof (startIndex), Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (count < 0 || startIndex > length - count)
        throw new ArgumentOutOfRangeException(nameof (count), Environment.GetResourceString("ArgumentOutOfRange_Index"));
      int num = startIndex + count;
      StringBuilder stringBuilder = this;
      while (true)
      {
        int val2 = num - stringBuilder.m_ChunkOffset;
        int val1 = startIndex - stringBuilder.m_ChunkOffset;
        if (val2 >= 0)
        {
          int index1 = Math.Max(val1, 0);
          for (int index2 = Math.Min(stringBuilder.m_ChunkLength, val2); index1 < index2; ++index1)
          {
            if ((int) stringBuilder.m_ChunkChars[index1] == (int) oldChar)
              stringBuilder.m_ChunkChars[index1] = newChar;
          }
        }
        if (val1 < 0)
          stringBuilder = stringBuilder.m_ChunkPrevious;
        else
          break;
      }
      return this;
    }

    /// <summary>
    ///   Добавляет к данному экземпляру массив символов Юникода начиная с указанного адреса.
    /// </summary>
    /// <param name="value">Указатель на массив символов.</param>
    /// <param name="valueCount">Количество символов в массиве.</param>
    /// <returns>
    ///   Ссылка на этот экземпляр после завершения операции добавления.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="valueCount" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   Увеличение значения этого экземпляра может привести к превышению <see cref="P:System.Text.StringBuilder.MaxCapacity" />.
    /// </exception>
    /// <exception cref="T:System.NullReferenceException">
    ///   <paramref name="value" /> является пустым указателем
    /// </exception>
    [SecurityCritical]
    [CLSCompliant(false)]
    public unsafe StringBuilder Append(char* value, int valueCount)
    {
      if (valueCount < 0)
        throw new ArgumentOutOfRangeException(nameof (valueCount), Environment.GetResourceString("ArgumentOutOfRange_NegativeCount"));
      int num1 = valueCount + this.m_ChunkLength;
      if (num1 <= this.m_ChunkChars.Length)
      {
        StringBuilder.ThreadSafeCopy(value, this.m_ChunkChars, this.m_ChunkLength, valueCount);
        this.m_ChunkLength = num1;
      }
      else
      {
        int count = this.m_ChunkChars.Length - this.m_ChunkLength;
        if (count > 0)
        {
          StringBuilder.ThreadSafeCopy(value, this.m_ChunkChars, this.m_ChunkLength, count);
          this.m_ChunkLength = this.m_ChunkChars.Length;
        }
        int num2 = valueCount - count;
        this.ExpandByABlock(num2);
        StringBuilder.ThreadSafeCopy(value + count, this.m_ChunkChars, 0, num2);
        this.m_ChunkLength = num2;
      }
      return this;
    }

    [SecurityCritical]
    private unsafe void Insert(int index, char* value, int valueCount)
    {
      if ((uint) index > (uint) this.Length)
        throw new ArgumentOutOfRangeException(nameof (index), Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (valueCount <= 0)
        return;
      StringBuilder chunk;
      int indexInChunk;
      this.MakeRoom(index, valueCount, out chunk, out indexInChunk, false);
      this.ReplaceInPlaceAtChunk(ref chunk, ref indexInChunk, value, valueCount);
    }

    [SecuritySafeCritical]
    private unsafe void ReplaceAllInChunk(int[] replacements, int replacementsCount, StringBuilder sourceChunk, int removeCount, string value)
    {
      if (replacementsCount <= 0)
        return;
      string str = value;
      char* chPtr1 = (char*) str;
      if ((IntPtr) chPtr1 != IntPtr.Zero)
        chPtr1 += RuntimeHelpers.OffsetToStringData;
      int count = (value.Length - removeCount) * replacementsCount;
      StringBuilder chunk = sourceChunk;
      int indexInChunk = replacements[0];
      if (count > 0)
        this.MakeRoom(chunk.m_ChunkOffset + indexInChunk, count, out chunk, out indexInChunk, true);
      int index1 = 0;
      while (true)
      {
        this.ReplaceInPlaceAtChunk(ref chunk, ref indexInChunk, chPtr1, value.Length);
        int index2 = replacements[index1] + removeCount;
        ++index1;
        if (index1 < replacementsCount)
        {
          int replacement = replacements[index1];
          if (count != 0)
          {
            fixed (char* chPtr2 = &sourceChunk.m_ChunkChars[index2])
              this.ReplaceInPlaceAtChunk(ref chunk, ref indexInChunk, chPtr2, replacement - index2);
          }
          else
            indexInChunk += replacement - index2;
        }
        else
          break;
      }
      if (count < 0)
        this.Remove(chunk.m_ChunkOffset + indexInChunk, -count, out chunk, out indexInChunk);
      str = (string) null;
    }

    private bool StartsWith(StringBuilder chunk, int indexInChunk, int count, string value)
    {
      for (int index = 0; index < value.Length; ++index)
      {
        if (count == 0)
          return false;
        if (indexInChunk >= chunk.m_ChunkLength)
        {
          chunk = this.Next(chunk);
          if (chunk == null)
            return false;
          indexInChunk = 0;
        }
        if ((int) value[index] != (int) chunk.m_ChunkChars[indexInChunk])
          return false;
        ++indexInChunk;
        --count;
      }
      return true;
    }

    [SecurityCritical]
    private unsafe void ReplaceInPlaceAtChunk(ref StringBuilder chunk, ref int indexInChunk, char* value, int count)
    {
      if (count == 0)
        return;
      while (true)
      {
        int count1 = Math.Min(chunk.m_ChunkLength - indexInChunk, count);
        StringBuilder.ThreadSafeCopy(value, chunk.m_ChunkChars, indexInChunk, count1);
        indexInChunk += count1;
        if (indexInChunk >= chunk.m_ChunkLength)
        {
          chunk = this.Next(chunk);
          indexInChunk = 0;
        }
        count -= count1;
        if (count != 0)
          value += count1;
        else
          break;
      }
    }

    [SecurityCritical]
    private static unsafe void ThreadSafeCopy(char* sourcePtr, char[] destination, int destinationIndex, int count)
    {
      if (count <= 0)
        return;
      if ((uint) destinationIndex > (uint) destination.Length || destinationIndex + count > destination.Length)
        throw new ArgumentOutOfRangeException(nameof (destinationIndex), Environment.GetResourceString("ArgumentOutOfRange_Index"));
      fixed (char* dmem = &destination[destinationIndex])
        string.wstrcpy(dmem, sourcePtr, count);
    }

    [SecurityCritical]
    private static unsafe void ThreadSafeCopy(char[] source, int sourceIndex, char[] destination, int destinationIndex, int count)
    {
      if (count <= 0)
        return;
      if ((uint) sourceIndex > (uint) source.Length || sourceIndex + count > source.Length)
        throw new ArgumentOutOfRangeException(nameof (sourceIndex), Environment.GetResourceString("ArgumentOutOfRange_Index"));
      fixed (char* sourcePtr = &source[sourceIndex])
        StringBuilder.ThreadSafeCopy(sourcePtr, destination, destinationIndex, count);
    }

    [SecurityCritical]
    internal unsafe void InternalCopy(IntPtr dest, int len)
    {
      if (len == 0)
        return;
      bool flag = true;
      byte* pointer = (byte*) dest.ToPointer();
      StringBuilder stringBuilder = this.FindChunkForByte(len);
      do
      {
        int num = stringBuilder.m_ChunkOffset * 2;
        int len1 = stringBuilder.m_ChunkLength * 2;
        fixed (char* chPtr = &stringBuilder.m_ChunkChars[0])
        {
          if (flag)
          {
            flag = false;
            Buffer.Memcpy(pointer + num, (byte*) chPtr, len - num);
          }
          else
            Buffer.Memcpy(pointer + num, (byte*) chPtr, len1);
        }
        stringBuilder = stringBuilder.m_ChunkPrevious;
      }
      while (stringBuilder != null);
    }

    private StringBuilder FindChunkForIndex(int index)
    {
      StringBuilder stringBuilder = this;
      while (stringBuilder.m_ChunkOffset > index)
        stringBuilder = stringBuilder.m_ChunkPrevious;
      return stringBuilder;
    }

    private StringBuilder FindChunkForByte(int byteIndex)
    {
      StringBuilder stringBuilder = this;
      while (stringBuilder.m_ChunkOffset * 2 > byteIndex)
        stringBuilder = stringBuilder.m_ChunkPrevious;
      return stringBuilder;
    }

    private StringBuilder Next(StringBuilder chunk)
    {
      if (chunk == this)
        return (StringBuilder) null;
      return this.FindChunkForIndex(chunk.m_ChunkOffset + chunk.m_ChunkLength);
    }

    private void ExpandByABlock(int minBlockCharCount)
    {
      if (minBlockCharCount + this.Length < minBlockCharCount || minBlockCharCount + this.Length > this.m_MaxCapacity)
        throw new ArgumentOutOfRangeException("requiredLength", Environment.GetResourceString("ArgumentOutOfRange_SmallCapacity"));
      int length = Math.Max(minBlockCharCount, Math.Min(this.Length, 8000));
      this.m_ChunkPrevious = new StringBuilder(this);
      this.m_ChunkOffset += this.m_ChunkLength;
      this.m_ChunkLength = 0;
      if (this.m_ChunkOffset + length < length)
      {
        this.m_ChunkChars = (char[]) null;
        throw new OutOfMemoryException();
      }
      this.m_ChunkChars = new char[length];
    }

    private StringBuilder(StringBuilder from)
    {
      this.m_ChunkLength = from.m_ChunkLength;
      this.m_ChunkOffset = from.m_ChunkOffset;
      this.m_ChunkChars = from.m_ChunkChars;
      this.m_ChunkPrevious = from.m_ChunkPrevious;
      this.m_MaxCapacity = from.m_MaxCapacity;
    }

    [SecuritySafeCritical]
    private unsafe void MakeRoom(int index, int count, out StringBuilder chunk, out int indexInChunk, bool doneMoveFollowingChars)
    {
      if (count + this.Length < count || count + this.Length > this.m_MaxCapacity)
        throw new ArgumentOutOfRangeException("requiredLength", Environment.GetResourceString("ArgumentOutOfRange_SmallCapacity"));
      chunk = this;
      while (chunk.m_ChunkOffset > index)
      {
        chunk.m_ChunkOffset += count;
        chunk = chunk.m_ChunkPrevious;
      }
      indexInChunk = index - chunk.m_ChunkOffset;
      if (!doneMoveFollowingChars && chunk.m_ChunkLength <= 32 && chunk.m_ChunkChars.Length - chunk.m_ChunkLength >= count)
      {
        int chunkLength = chunk.m_ChunkLength;
        while (chunkLength > indexInChunk)
        {
          --chunkLength;
          chunk.m_ChunkChars[chunkLength + count] = chunk.m_ChunkChars[chunkLength];
        }
        chunk.m_ChunkLength += count;
      }
      else
      {
        StringBuilder stringBuilder = new StringBuilder(Math.Max(count, 16), chunk.m_MaxCapacity, chunk.m_ChunkPrevious);
        stringBuilder.m_ChunkLength = count;
        int count1 = Math.Min(count, indexInChunk);
        if (count1 > 0)
        {
          fixed (char* sourcePtr = chunk.m_ChunkChars)
          {
            StringBuilder.ThreadSafeCopy(sourcePtr, stringBuilder.m_ChunkChars, 0, count1);
            int count2 = indexInChunk - count1;
            if (count2 >= 0)
            {
              StringBuilder.ThreadSafeCopy(sourcePtr + count1, chunk.m_ChunkChars, 0, count2);
              indexInChunk = count2;
            }
          }
        }
        chunk.m_ChunkPrevious = stringBuilder;
        chunk.m_ChunkOffset += count;
        if (count1 >= count)
          return;
        chunk = stringBuilder;
        indexInChunk = count1;
      }
    }

    private StringBuilder(int size, int maxCapacity, StringBuilder previousBlock)
    {
      this.m_ChunkChars = new char[size];
      this.m_MaxCapacity = maxCapacity;
      this.m_ChunkPrevious = previousBlock;
      if (previousBlock == null)
        return;
      this.m_ChunkOffset = previousBlock.m_ChunkOffset + previousBlock.m_ChunkLength;
    }

    [SecuritySafeCritical]
    private void Remove(int startIndex, int count, out StringBuilder chunk, out int indexInChunk)
    {
      int num = startIndex + count;
      chunk = this;
      StringBuilder stringBuilder = (StringBuilder) null;
      int sourceIndex = 0;
      while (true)
      {
        if (num - chunk.m_ChunkOffset >= 0)
        {
          if (stringBuilder == null)
          {
            stringBuilder = chunk;
            sourceIndex = num - stringBuilder.m_ChunkOffset;
          }
          if (startIndex - chunk.m_ChunkOffset >= 0)
            break;
        }
        else
          chunk.m_ChunkOffset -= count;
        chunk = chunk.m_ChunkPrevious;
      }
      indexInChunk = startIndex - chunk.m_ChunkOffset;
      int destinationIndex = indexInChunk;
      int count1 = stringBuilder.m_ChunkLength - sourceIndex;
      if (stringBuilder != chunk)
      {
        destinationIndex = 0;
        chunk.m_ChunkLength = indexInChunk;
        stringBuilder.m_ChunkPrevious = chunk;
        stringBuilder.m_ChunkOffset = chunk.m_ChunkOffset + chunk.m_ChunkLength;
        if (indexInChunk == 0)
        {
          stringBuilder.m_ChunkPrevious = chunk.m_ChunkPrevious;
          chunk = stringBuilder;
        }
      }
      stringBuilder.m_ChunkLength -= sourceIndex - destinationIndex;
      if (destinationIndex == sourceIndex)
        return;
      StringBuilder.ThreadSafeCopy(stringBuilder.m_ChunkChars, sourceIndex, stringBuilder.m_ChunkChars, destinationIndex, count1);
    }
  }
}
