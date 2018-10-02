// Decompiled with JetBrains decompiler
// Type: System.String
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading;

namespace System
{
  /// <summary>
  ///   Представляет текст как последовательность из частей кода UTF-16.
  /// 
  ///   Исходный код .NET Framework для этого типа см. в этом справочнике.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class String : IComparable, ICloneable, IConvertible, IEnumerable, IComparable<string>, IEnumerable<char>, IEquatable<string>
  {
    [NonSerialized]
    private int m_stringLength;
    [NonSerialized]
    private char m_firstChar;
    private const int TrimHead = 0;
    private const int TrimTail = 1;
    private const int TrimBoth = 2;
    /// <summary>
    ///   Представляет пустую строку.
    ///    Это поле доступно только для чтения.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly string Empty;
    private const int charPtrAlignConst = 1;
    private const int alignConst = 3;

    /// <summary>
    ///   Сцепляет все элементы массива строк, помещая между ними заданный разделитель.
    /// </summary>
    /// <param name="separator">
    ///   Строка для использования в качестве разделителя.
    ///   <paramref name="separator" /> включается в возвращаемую строку, только если в <paramref name="value" /> более одного элемента.
    /// </param>
    /// <param name="value">
    ///   Массив, содержащий элементы, которые требуется сцепить.
    /// </param>
    /// <returns>
    ///   Строка, состоящая из элементов <paramref name="value" />, разделяемых строками <paramref name="separator" />.
    ///    Если <paramref name="value" /> равен пустому массиву, метод возвращает значение <see cref="F:System.String.Empty" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static string Join(string separator, params string[] value)
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      return string.Join(separator, value, 0, value.Length);
    }

    /// <summary>
    ///   Сцепляет элементы массива объектов, помещая между ними заданный разделитель.
    /// </summary>
    /// <param name="separator">
    ///   Строка для использования в качестве разделителя.
    ///   <paramref name="separator" /> включается в возвращаемую строку, только если в <paramref name="values" /> более одного элемента.
    /// </param>
    /// <param name="values">
    ///   Массив, содержащий элементы, которые требуется сцепить.
    /// </param>
    /// <returns>
    ///   Строка, состоящая из элементов <paramref name="values" />, разделяемых строками <paramref name="separator" />.
    ///    Если <paramref name="values" /> равен пустому массиву, метод возвращает значение <see cref="F:System.String.Empty" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="values" /> имеет значение <see langword="null" />.
    /// </exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public static string Join(string separator, params object[] values)
    {
      if (values == null)
        throw new ArgumentNullException(nameof (values));
      if (values.Length == 0 || values[0] == null)
        return string.Empty;
      if (separator == null)
        separator = string.Empty;
      StringBuilder sb = StringBuilderCache.Acquire(16);
      string str1 = values[0].ToString();
      if (str1 != null)
        sb.Append(str1);
      for (int index = 1; index < values.Length; ++index)
      {
        sb.Append(separator);
        if (values[index] != null)
        {
          string str2 = values[index].ToString();
          if (str2 != null)
            sb.Append(str2);
        }
      }
      return StringBuilderCache.GetStringAndRelease(sb);
    }

    /// <summary>
    ///   Сцепляет элементы созданной коллекции, помещая между ними заданный разделитель.
    /// </summary>
    /// <param name="separator">
    ///   Строка, которую необходимо использовать в качестве разделителя. <paramref name="separator" /> включается в возвращаемую строку, только если в <paramref name="values" /> более одного элемента.
    /// </param>
    /// <param name="values">
    ///   Коллекция, содержащая сцепляемые объекты.
    /// </param>
    /// <typeparam name="T">
    ///   Тип элементов параметра <paramref name="values" />.
    /// </typeparam>
    /// <returns>
    ///   Строка, состоящая из элементов <paramref name="values" />, разделяемых строками <paramref name="separator" />.
    ///    Если <paramref name="values" /> не содержит членов, метод возвращает <see cref="F:System.String.Empty" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="values" /> имеет значение <see langword="null" />.
    /// </exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public static string Join<T>(string separator, IEnumerable<T> values)
    {
      if (values == null)
        throw new ArgumentNullException(nameof (values));
      if (separator == null)
        separator = string.Empty;
      using (IEnumerator<T> enumerator = values.GetEnumerator())
      {
        if (!enumerator.MoveNext())
          return string.Empty;
        StringBuilder sb = StringBuilderCache.Acquire(16);
        if ((object) enumerator.Current != null)
        {
          string str = enumerator.Current.ToString();
          if (str != null)
            sb.Append(str);
        }
        while (enumerator.MoveNext())
        {
          sb.Append(separator);
          if ((object) enumerator.Current != null)
          {
            string str = enumerator.Current.ToString();
            if (str != null)
              sb.Append(str);
          }
        }
        return StringBuilderCache.GetStringAndRelease(sb);
      }
    }

    /// <summary>
    ///   Сцепляет элементы созданной коллекции <see cref="T:System.Collections.Generic.IEnumerable`1" /> типа <see cref="T:System.String" />, помещая между ними заданный разделитель.
    /// </summary>
    /// <param name="separator">
    ///   Строка, которую необходимо использовать в качестве разделителя. <paramref name="separator" /> включается в возвращаемую строку, только если в <paramref name="values" /> более одного элемента.
    /// </param>
    /// <param name="values">
    ///   Коллекция, содержащая сцепляемые строки.
    /// </param>
    /// <returns>
    ///   Строка, состоящая из элементов <paramref name="values" />, разделяемых строками <paramref name="separator" />.
    ///    Если <paramref name="values" /> не содержит членов, метод возвращает <see cref="F:System.String.Empty" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="values" /> имеет значение <see langword="null" />.
    /// </exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public static string Join(string separator, IEnumerable<string> values)
    {
      if (values == null)
        throw new ArgumentNullException(nameof (values));
      if (separator == null)
        separator = string.Empty;
      using (IEnumerator<string> enumerator = values.GetEnumerator())
      {
        if (!enumerator.MoveNext())
          return string.Empty;
        StringBuilder sb = StringBuilderCache.Acquire(16);
        if (enumerator.Current != null)
          sb.Append(enumerator.Current);
        while (enumerator.MoveNext())
        {
          sb.Append(separator);
          if (enumerator.Current != null)
            sb.Append(enumerator.Current);
        }
        return StringBuilderCache.GetStringAndRelease(sb);
      }
    }

    internal char FirstChar
    {
      get
      {
        return this.m_firstChar;
      }
    }

    /// <summary>
    ///   Сцепляет указанные элементы массива строк, помещая между ними заданный разделитель.
    /// </summary>
    /// <param name="separator">
    ///   Строка для использования в качестве разделителя.
    ///   <paramref name="separator" /> включается в возвращаемую строку, только если в <paramref name="value" /> более одного элемента.
    /// </param>
    /// <param name="value">
    ///   Массив, содержащий элементы, которые требуется сцепить.
    /// </param>
    /// <param name="startIndex">
    ///   Первый используемый элемент массива <paramref name="value" />.
    /// </param>
    /// <param name="count">
    ///   Число используемых элементов массива <paramref name="value" />.
    /// </param>
    /// <returns>
    ///   Строка, состоящая из строк из параметра <paramref name="value" />, разделяемых строками <paramref name="separator" />.
    /// 
    ///   -или-
    /// 
    ///   <see cref="F:System.String.Empty" />, если параметр <paramref name="count" /> равен нулю, параметр <paramref name="value" /> не содержит элементов или параметр <paramref name="separator" /> и все элементы параметра <paramref name="value" /> равны <see cref="F:System.String.Empty" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="startIndex" /> или <paramref name="count" /> меньше 0.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="startIndex" /> плюс <paramref name="count" /> больше, чем число элементов в <paramref name="value" />.
    /// </exception>
    /// <exception cref="T:System.OutOfMemoryException">
    ///   Недостаточно памяти.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static unsafe string Join(string separator, string[] value, int startIndex, int count)
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      if (startIndex < 0)
        throw new ArgumentOutOfRangeException(nameof (startIndex), Environment.GetResourceString("ArgumentOutOfRange_StartIndex"));
      if (count < 0)
        throw new ArgumentOutOfRangeException(nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NegativeCount"));
      if (startIndex > value.Length - count)
        throw new ArgumentOutOfRangeException(nameof (startIndex), Environment.GetResourceString("ArgumentOutOfRange_IndexCountBuffer"));
      if (separator == null)
        separator = string.Empty;
      if (count == 0)
        return string.Empty;
      int num1 = 0;
      int num2 = startIndex + count - 1;
      for (int index = startIndex; index <= num2; ++index)
      {
        if (value[index] != null)
          num1 += value[index].Length;
      }
      int num3 = num1 + (count - 1) * separator.Length;
      if (num3 < 0 || num3 + 1 < 0)
        throw new OutOfMemoryException();
      if (num3 == 0)
        return string.Empty;
      string str = string.FastAllocateString(num3);
      fixed (char* buffer = &str.m_firstChar)
      {
        UnSafeCharBuffer unSafeCharBuffer = new UnSafeCharBuffer(buffer, num3);
        unSafeCharBuffer.AppendString(value[startIndex]);
        for (int index = startIndex + 1; index <= num2; ++index)
        {
          unSafeCharBuffer.AppendString(separator);
          unSafeCharBuffer.AppendString(value[index]);
        }
      }
      return str;
    }

    [SecuritySafeCritical]
    private static unsafe int CompareOrdinalIgnoreCaseHelper(string strA, string strB)
    {
      int num1 = Math.Min(strA.Length, strB.Length);
      fixed (char* chPtr1 = &strA.m_firstChar)
        fixed (char* chPtr2 = &strB.m_firstChar)
        {
          char* chPtr3 = chPtr1;
          char* chPtr4 = chPtr2;
          for (; num1 != 0; --num1)
          {
            int num2 = (int) *chPtr3;
            int num3 = (int) *chPtr4;
            if ((uint) (num2 - 97) <= 25U)
              num2 -= 32;
            if ((uint) (num3 - 97) <= 25U)
              num3 -= 32;
            if (num2 != num3)
              return num2 - num3;
            chPtr3 += 2;
            chPtr4 += 2;
          }
          return strA.Length - strB.Length;
        }
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern int nativeCompareOrdinalEx(string strA, int indexA, string strB, int indexB, int count);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern unsafe int nativeCompareOrdinalIgnoreCaseWC(string strA, sbyte* strBBytes);

    [SecuritySafeCritical]
    internal static unsafe string SmallCharToUpper(string strIn)
    {
      int length = strIn.Length;
      string str = string.FastAllocateString(length);
      fixed (char* chPtr1 = &strIn.m_firstChar)
        fixed (char* chPtr2 = &str.m_firstChar)
        {
          for (int index = 0; index < length; ++index)
          {
            int num = (int) chPtr1[index];
            if ((uint) (num - 97) <= 25U)
              num -= 32;
            chPtr2[index] = (char) num;
          }
        }
      return str;
    }

    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    private static unsafe bool EqualsHelper(string strA, string strB)
    {
      int length = strA.Length;
      fixed (char* chPtr1 = &strA.m_firstChar)
        fixed (char* chPtr2 = &strB.m_firstChar)
        {
          char* chPtr3 = chPtr1;
          char* chPtr4 = chPtr2;
          while (length >= 10)
          {
            if (*(int*) chPtr3 != *(int*) chPtr4 || *(int*) (chPtr3 + 2) != *(int*) (chPtr4 + 2) || (*(int*) (chPtr3 + 4) != *(int*) (chPtr4 + 4) || *(int*) (chPtr3 + 6) != *(int*) (chPtr4 + 6)) || *(int*) (chPtr3 + 8) != *(int*) (chPtr4 + 8))
              return false;
            chPtr3 += 10;
            chPtr4 += 10;
            length -= 10;
          }
          while (length > 0 && *(int*) chPtr3 == *(int*) chPtr4)
          {
            chPtr3 += 2;
            chPtr4 += 2;
            length -= 2;
          }
          return length <= 0;
        }
    }

    [SecuritySafeCritical]
    private static unsafe bool EqualsIgnoreCaseAsciiHelper(string strA, string strB)
    {
      int length = strA.Length;
      fixed (char* chPtr1 = &strA.m_firstChar)
        fixed (char* chPtr2 = &strB.m_firstChar)
        {
          char* chPtr3 = chPtr1;
          char* chPtr4 = chPtr2;
          for (; length != 0; --length)
          {
            int num1 = (int) *chPtr3;
            int num2 = (int) *chPtr4;
            if (num1 != num2 && ((num1 | 32) != (num2 | 32) || (uint) ((num1 | 32) - 97) > 25U))
              return false;
            chPtr3 += 2;
            chPtr4 += 2;
          }
          return true;
        }
    }

    [SecuritySafeCritical]
    private static unsafe int CompareOrdinalHelper(string strA, string strB)
    {
      int num1 = Math.Min(strA.Length, strB.Length);
      int num2 = -1;
      fixed (char* chPtr1 = &strA.m_firstChar)
        fixed (char* chPtr2 = &strB.m_firstChar)
        {
          char* chPtr3 = chPtr1;
          char* chPtr4 = chPtr2;
          while (num1 >= 10)
          {
            if (*(int*) chPtr3 != *(int*) chPtr4)
            {
              num2 = 0;
              break;
            }
            if (*(int*) (chPtr3 + 2) != *(int*) (chPtr4 + 2))
            {
              num2 = 2;
              break;
            }
            if (*(int*) (chPtr3 + 4) != *(int*) (chPtr4 + 4))
            {
              num2 = 4;
              break;
            }
            if (*(int*) (chPtr3 + 6) != *(int*) (chPtr4 + 6))
            {
              num2 = 6;
              break;
            }
            if (*(int*) (chPtr3 + 8) != *(int*) (chPtr4 + 8))
            {
              num2 = 8;
              break;
            }
            chPtr3 += 10;
            chPtr4 += 10;
            num1 -= 10;
          }
          if (num2 != -1)
          {
            char* chPtr5 = chPtr3 + num2;
            char* chPtr6 = chPtr4 + num2;
            int num3;
            if ((num3 = (int) *chPtr5 - (int) *chPtr6) != 0)
              return num3;
            return (int) *(ushort*) ((IntPtr) chPtr5 + 2) - (int) *(ushort*) ((IntPtr) chPtr6 + 2);
          }
          while (num1 > 0 && *(int*) chPtr3 == *(int*) chPtr4)
          {
            chPtr3 += 2;
            chPtr4 += 2;
            num1 -= 2;
          }
          if (num1 <= 0)
            return strA.Length - strB.Length;
          int num4;
          if ((num4 = (int) *chPtr3 - (int) *chPtr4) != 0)
            return num4;
          return (int) *(ushort*) ((IntPtr) chPtr3 + 2) - (int) *(ushort*) ((IntPtr) chPtr4 + 2);
        }
    }

    /// <summary>
    ///   Определяет, равны ли значения этого экземпляра и указанного объекта, который также должен быть объектом <see cref="T:System.String" />.
    /// </summary>
    /// <param name="obj">
    ///   Строка для сравнения с данным экземпляром.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если объект <paramref name="obj" /> имеет тип <see cref="T:System.String" /> и его значение совпадает со значением данного экземпляра; в противном случае — <see langword="false" />.
    ///     Если значением параметра <paramref name="obj" /> является <see langword="null" />, метод возвращает <see langword="false" />.
    /// </returns>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public override bool Equals(object obj)
    {
      if (this == null)
        throw new NullReferenceException();
      string strB = obj as string;
      if (strB == null)
        return false;
      if ((object) this == obj)
        return true;
      if (this.Length != strB.Length)
        return false;
      return string.EqualsHelper(this, strB);
    }

    /// <summary>
    ///   Определяет, равны ли значения этого экземпляра и указанного объекта <see cref="T:System.String" />.
    /// </summary>
    /// <param name="value">
    ///   Строка для сравнения с данным экземпляром.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если значение параметра <paramref name="value" /> совпадает со значением данного экземпляра; в противном случае — <see langword="false" />.
    ///    Если значением параметра <paramref name="value" /> является <see langword="null" />, метод возвращает <see langword="false" />.
    /// </returns>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public bool Equals(string value)
    {
      if (this == null)
        throw new NullReferenceException();
      if (value == null)
        return false;
      if ((object) this == (object) value)
        return true;
      if (this.Length != value.Length)
        return false;
      return string.EqualsHelper(this, value);
    }

    /// <summary>
    ///   Определяет, равны ли значения этой строки и указанного объекта <see cref="T:System.String" />.
    ///    Параметр определяет язык и региональные параметры, учет регистра и правила сортировки, используемые при сравнении.
    /// </summary>
    /// <param name="value">
    ///   Строка для сравнения с данным экземпляром.
    /// </param>
    /// <param name="comparisonType">
    ///   Одно из значений перечисления, задающее способ сравнения строк.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если значение параметра <paramref name="value" /> совпадает со значением данной строки; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="comparisonType" /> не является значением <see cref="T:System.StringComparison" />.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public bool Equals(string value, StringComparison comparisonType)
    {
      switch (comparisonType)
      {
        case StringComparison.CurrentCulture:
        case StringComparison.CurrentCultureIgnoreCase:
        case StringComparison.InvariantCulture:
        case StringComparison.InvariantCultureIgnoreCase:
        case StringComparison.Ordinal:
        case StringComparison.OrdinalIgnoreCase:
          if ((object) this == (object) value)
            return true;
          if (value == null)
            return false;
          switch (comparisonType)
          {
            case StringComparison.CurrentCulture:
              return CultureInfo.CurrentCulture.CompareInfo.Compare(this, value, CompareOptions.None) == 0;
            case StringComparison.CurrentCultureIgnoreCase:
              return CultureInfo.CurrentCulture.CompareInfo.Compare(this, value, CompareOptions.IgnoreCase) == 0;
            case StringComparison.InvariantCulture:
              return CultureInfo.InvariantCulture.CompareInfo.Compare(this, value, CompareOptions.None) == 0;
            case StringComparison.InvariantCultureIgnoreCase:
              return CultureInfo.InvariantCulture.CompareInfo.Compare(this, value, CompareOptions.IgnoreCase) == 0;
            case StringComparison.Ordinal:
              if (this.Length != value.Length)
                return false;
              return string.EqualsHelper(this, value);
            case StringComparison.OrdinalIgnoreCase:
              if (this.Length != value.Length)
                return false;
              if (this.IsAscii() && value.IsAscii())
                return string.EqualsIgnoreCaseAsciiHelper(this, value);
              return TextInfo.CompareOrdinalIgnoreCase(this, value) == 0;
            default:
              throw new ArgumentException(Environment.GetResourceString("NotSupported_StringComparison"), nameof (comparisonType));
          }
        default:
          throw new ArgumentException(Environment.GetResourceString("NotSupported_StringComparison"), nameof (comparisonType));
      }
    }

    /// <summary>
    ///   Определяет, совпадают ли значения двух указанных объектов <see cref="T:System.String" />.
    /// </summary>
    /// <param name="a">
    ///   Первая сравниваемая строка или значение <see langword="null" />.
    /// </param>
    /// <param name="b">
    ///   Вторая сравниваемая строка или значение <see langword="null" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если значение параметра <paramref name="a" /> совпадает со значением <paramref name="b" />; в противном случае — значение <see langword="false" />.
    ///    Если оба параметра <paramref name="a" /> и <paramref name="b" /> имеют значение <see langword="null" />, метод возвращает значение <see langword="true" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool Equals(string a, string b)
    {
      if ((object) a == (object) b)
        return true;
      if (a == null || b == null || a.Length != b.Length)
        return false;
      return string.EqualsHelper(a, b);
    }

    /// <summary>
    ///   Определяет, совпадают ли значения двух указанных объектов <see cref="T:System.String" />.
    ///    Параметр определяет язык и региональные параметры, учет регистра и правила сортировки, используемые при сравнении.
    /// </summary>
    /// <param name="a">
    ///   Первая сравниваемая строка или значение <see langword="null" />.
    /// </param>
    /// <param name="b">
    ///   Вторая сравниваемая строка или значение <see langword="null" />.
    /// </param>
    /// <param name="comparisonType">
    ///   Одно из значений перечисления, определяющее правила сравнения.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если значения параметров <paramref name="a" /> и <paramref name="b" /> совпадают; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="comparisonType" /> не является значением <see cref="T:System.StringComparison" />.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static bool Equals(string a, string b, StringComparison comparisonType)
    {
      switch (comparisonType)
      {
        case StringComparison.CurrentCulture:
        case StringComparison.CurrentCultureIgnoreCase:
        case StringComparison.InvariantCulture:
        case StringComparison.InvariantCultureIgnoreCase:
        case StringComparison.Ordinal:
        case StringComparison.OrdinalIgnoreCase:
          if ((object) a == (object) b)
            return true;
          if (a == null || b == null)
            return false;
          switch (comparisonType)
          {
            case StringComparison.CurrentCulture:
              return CultureInfo.CurrentCulture.CompareInfo.Compare(a, b, CompareOptions.None) == 0;
            case StringComparison.CurrentCultureIgnoreCase:
              return CultureInfo.CurrentCulture.CompareInfo.Compare(a, b, CompareOptions.IgnoreCase) == 0;
            case StringComparison.InvariantCulture:
              return CultureInfo.InvariantCulture.CompareInfo.Compare(a, b, CompareOptions.None) == 0;
            case StringComparison.InvariantCultureIgnoreCase:
              return CultureInfo.InvariantCulture.CompareInfo.Compare(a, b, CompareOptions.IgnoreCase) == 0;
            case StringComparison.Ordinal:
              if (a.Length != b.Length)
                return false;
              return string.EqualsHelper(a, b);
            case StringComparison.OrdinalIgnoreCase:
              if (a.Length != b.Length)
                return false;
              if (a.IsAscii() && b.IsAscii())
                return string.EqualsIgnoreCaseAsciiHelper(a, b);
              return TextInfo.CompareOrdinalIgnoreCase(a, b) == 0;
            default:
              throw new ArgumentException(Environment.GetResourceString("NotSupported_StringComparison"), nameof (comparisonType));
          }
        default:
          throw new ArgumentException(Environment.GetResourceString("NotSupported_StringComparison"), nameof (comparisonType));
      }
    }

    /// <summary>Определяет, равны ли значения двух указанных строк.</summary>
    /// <param name="a">
    ///   Первая сравниваемая строка или значение <see langword="null" />.
    /// </param>
    /// <param name="b">
    ///   Вторая сравниваемая строка или значение <see langword="null" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если значение параметра <paramref name="a" /> совпадает со значением <paramref name="b" />; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool operator ==(string a, string b)
    {
      return string.Equals(a, b);
    }

    /// <summary>
    ///   Определяет, различаются ли значения двух указанных строк.
    /// </summary>
    /// <param name="a">
    ///   Первая сравниваемая строка или значение <see langword="null" />.
    /// </param>
    /// <param name="b">
    ///   Вторая сравниваемая строка или значение <see langword="null" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если значение <paramref name="a" /> отличается от значения <paramref name="b" />; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool operator !=(string a, string b)
    {
      return !string.Equals(a, b);
    }

    /// <summary>
    ///   Возвращает объект <see cref="T:System.Char" /> в указанной позиции в текущем объекте <see cref="T:System.String" />.
    /// </summary>
    /// <param name="index">Позиция в текущей строке.</param>
    /// <returns>
    ///   Объект в позиции <paramref name="index" />.
    /// </returns>
    /// <exception cref="T:System.IndexOutOfRangeException">
    ///   Значение <paramref name="index" /> больше или равно длине данного объекта или меньше нуля.
    /// </exception>
    [__DynamicallyInvokable]
    [IndexerName("Chars")]
    public extern char this[int index] { [SecuritySafeCritical, __DynamicallyInvokable, MethodImpl(MethodImplOptions.InternalCall)] get; }

    /// <summary>
    ///   Копирует заданное число знаков, начиная с указанной позиции в этом экземпляре до указанной позиции в массиве знаков Юникода.
    /// </summary>
    /// <param name="sourceIndex">
    ///   Индекс первого символа в этом экземпляре, который необходимо скопировать.
    /// </param>
    /// <param name="destination">
    ///   Массив символов Юникода, в который копируются символы в этом экземпляре.
    /// </param>
    /// <param name="destinationIndex">
    ///   Индекс в массиве <paramref name="destination" />, с которого начинается копирование.
    /// </param>
    /// <param name="count">
    ///   Число знаков в этом экземпляре, копируемых в <paramref name="destination" />.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="destination" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="sourceIndex" />, <paramref name="destinationIndex" /> или <paramref name="count" /> является отрицательным значением.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="sourceIndex" /> не определяет позицию в текущем экземпляре.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="destinationIndex" /> не определяет допустимый индекс в массиве <paramref name="destination" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="count" /> больше, чем длина подстроки от <paramref name="startIndex" /> до конца данного экземпляра.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="count" /> больше, чем длина подмассива от <paramref name="destinationIndex" /> до конца массива <paramref name="destination" />.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public unsafe void CopyTo(int sourceIndex, char[] destination, int destinationIndex, int count)
    {
      if (destination == null)
        throw new ArgumentNullException(nameof (destination));
      if (count < 0)
        throw new ArgumentOutOfRangeException(nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NegativeCount"));
      if (sourceIndex < 0)
        throw new ArgumentOutOfRangeException(nameof (sourceIndex), Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (count > this.Length - sourceIndex)
        throw new ArgumentOutOfRangeException(nameof (sourceIndex), Environment.GetResourceString("ArgumentOutOfRange_IndexCount"));
      if (destinationIndex > destination.Length - count || destinationIndex < 0)
        throw new ArgumentOutOfRangeException(nameof (destinationIndex), Environment.GetResourceString("ArgumentOutOfRange_IndexCount"));
      if (count <= 0)
        return;
      fixed (char* chPtr1 = &this.m_firstChar)
        fixed (char* chPtr2 = destination)
          string.wstrcpy(chPtr2 + destinationIndex, chPtr1 + sourceIndex, count);
    }

    /// <summary>
    ///   Копирует знаки данного экземпляра в массив знаков Юникода.
    /// </summary>
    /// <returns>
    ///   Массив знаков Юникода, элементами которого являются отдельные знаки из данного экземпляра.
    ///    Если этот экземпляр является пустой строкой, то возвращаемый массив пуст и его длина равна нулю.
    /// </returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public unsafe char[] ToCharArray()
    {
      int length = this.Length;
      char[] chArray = new char[length];
      if (length > 0)
      {
        fixed (char* smem = &this.m_firstChar)
          fixed (char* dmem = chArray)
            string.wstrcpy(dmem, smem, length);
      }
      return chArray;
    }

    /// <summary>
    ///   Копирует знаки из указанной подстроки данного экземпляра в массив знаков Юникода.
    /// </summary>
    /// <param name="startIndex">
    ///   Начальная позиция подстроки в данном экземпляре.
    /// </param>
    /// <param name="length">Длина подстроки в данном экземпляре.</param>
    /// <returns>
    ///   Массив знаков Юникода, элементами которого являются <paramref name="length" /> знаков данного экземпляра начиная с позиции <paramref name="startIndex" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение <paramref name="startIndex" /> или <paramref name="length" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="startIndex" /> + <paramref name="length" /> больше длины этого экземпляра.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public unsafe char[] ToCharArray(int startIndex, int length)
    {
      if (startIndex < 0 || startIndex > this.Length || startIndex > this.Length - length)
        throw new ArgumentOutOfRangeException(nameof (startIndex), Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (length < 0)
        throw new ArgumentOutOfRangeException(nameof (length), Environment.GetResourceString("ArgumentOutOfRange_Index"));
      char[] chArray = new char[length];
      if (length > 0)
      {
        fixed (char* chPtr = &this.m_firstChar)
          fixed (char* dmem = chArray)
            string.wstrcpy(dmem, chPtr + startIndex, length);
      }
      return chArray;
    }

    /// <summary>
    ///   Указывает, является ли указанная строка строкой <see langword="null" /> или <see cref="F:System.String.Empty" />.
    /// </summary>
    /// <param name="value">Строка для проверки.</param>
    /// <returns>
    ///   Значение <see langword="true" />, если параметр <paramref name="value" /> равен <see langword="null" /> или пустой строке (""); в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool IsNullOrEmpty(string value)
    {
      if (value != null)
        return value.Length == 0;
      return true;
    }

    /// <summary>
    ///   Указывает, имеет ли указанная строка значение <see langword="null" />, является ли она пустой строкой или строкой, состоящей только из символов-разделителей.
    /// </summary>
    /// <param name="value">Строка для проверки.</param>
    /// <returns>
    ///   Значение <see langword="true" />, если параметр <paramref name="value" /> имеет значение <see langword="null" /> или <see cref="F:System.String.Empty" />, либо если параметр <paramref name="value" /> содержит только символы-разделители.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool IsNullOrWhiteSpace(string value)
    {
      if (value == null)
        return true;
      for (int index = 0; index < value.Length; ++index)
      {
        if (!char.IsWhiteSpace(value[index]))
          return false;
      }
      return true;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern int InternalMarvin32HashString(string s, int strLen, long additionalEntropy);

    [SecuritySafeCritical]
    internal static bool UseRandomizedHashing()
    {
      return string.InternalUseRandomizedHashing();
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern bool InternalUseRandomizedHashing();

    /// <summary>Возвращает хэш-код для этой строки.</summary>
    /// <returns>
    ///   Хэш-код в виде 32-разрядного целого числа со знаком.
    /// </returns>
    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public override unsafe int GetHashCode()
    {
      if (HashHelpers.s_UseRandomizedStringHashing)
        return string.InternalMarvin32HashString(this, this.Length, 0L);
      string str = this;
      char* chPtr = (char*) str;
      if ((IntPtr) chPtr != IntPtr.Zero)
        chPtr += RuntimeHelpers.OffsetToStringData;
      int num1 = 352654597;
      int num2 = num1;
      int* numPtr = (int*) chPtr;
      int length = this.Length;
      while (length > 2)
      {
        num1 = (num1 << 5) + num1 + (num1 >> 27) ^ *numPtr;
        num2 = (num2 << 5) + num2 + (num2 >> 27) ^ *(int*) ((IntPtr) numPtr + 4);
        numPtr += 2;
        length -= 4;
      }
      if (length > 0)
        num1 = (num1 << 5) + num1 + (num1 >> 27) ^ *numPtr;
      return num1 + num2 * 1566083941;
    }

    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    internal unsafe int GetLegacyNonRandomizedHashCode()
    {
      string str = this;
      char* chPtr = (char*) str;
      if ((IntPtr) chPtr != IntPtr.Zero)
        chPtr += RuntimeHelpers.OffsetToStringData;
      int num1 = 352654597;
      int num2 = num1;
      int* numPtr = (int*) chPtr;
      int length = this.Length;
      while (length > 2)
      {
        num1 = (num1 << 5) + num1 + (num1 >> 27) ^ *numPtr;
        num2 = (num2 << 5) + num2 + (num2 >> 27) ^ *(int*) ((IntPtr) numPtr + 4);
        numPtr += 2;
        length -= 4;
      }
      if (length > 0)
        num1 = (num1 << 5) + num1 + (num1 >> 27) ^ *numPtr;
      return num1 + num2 * 1566083941;
    }

    /// <summary>
    ///   Возвращает число знаков в текущем объекте <see cref="T:System.String" />.
    /// </summary>
    /// <returns>Количество знаков в текущей строке.</returns>
    [__DynamicallyInvokable]
    public extern int Length { [SecuritySafeCritical, __DynamicallyInvokable, MethodImpl(MethodImplOptions.InternalCall)] get; }

    /// <summary>
    ///   Разбивает строку на подстроки в зависимости от символов в массиве.
    /// </summary>
    /// <param name="separator">
    ///   Массив символов, разделяющий подстроки в данной строке, пустой массив, не содержащий разделителей, или <see langword="null" />.
    /// </param>
    /// <returns>
    ///   Массив, элементы которого содержат подстроки из этого экземпляра, разделенные символами из <paramref name="separator" />.
    ///    Дополнительные сведения см. в разделе "Примечания".
    /// </returns>
    [__DynamicallyInvokable]
    public string[] Split(params char[] separator)
    {
      return this.SplitInternal(separator, int.MaxValue, StringSplitOptions.None);
    }

    /// <summary>
    ///   Разбивает строку на максимальное число подстрок в зависимости от символов в массиве.
    ///    Можно также указать максимальное число возвращаемых подстрок.
    /// </summary>
    /// <param name="separator">
    ///   Массив символов, разделяющий подстроки в данной строке, пустой массив, не содержащий разделителей, или <see langword="null" />.
    /// </param>
    /// <param name="count">
    ///   Максимальное число возвращаемых подстрок.
    /// </param>
    /// <returns>
    ///   Массив, элементы которого содержат подстроки данного экземпляра, разделенные одним или более знаками из <paramref name="separator" />.
    ///    Дополнительные сведения см. в разделе "Примечания".
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="count" /> является отрицательным значением.
    /// </exception>
    [__DynamicallyInvokable]
    public string[] Split(char[] separator, int count)
    {
      return this.SplitInternal(separator, count, StringSplitOptions.None);
    }

    /// <summary>
    ///   Разбивает строку на подстроки в зависимости от символов в массиве.
    ///    Можно указать, включают ли подстроки пустые элементы массива.
    /// </summary>
    /// <param name="separator">
    ///   Массив символов, разделяющий подстроки в данной строке, пустой массив, не содержащий разделителей, или <see langword="null" />.
    /// </param>
    /// <param name="options">
    ///   <see cref="F:System.StringSplitOptions.RemoveEmptyEntries" />, чтобы исключить пустые элементы из возвращаемого массива; или <see cref="F:System.StringSplitOptions.None" /> для включения пустых элементов в возвращаемый массив.
    /// </param>
    /// <returns>
    ///   Массив, элементы которого содержат подстроки данной строки, разделенные одним или более знаками из <paramref name="separator" />.
    ///    Дополнительные сведения см. в разделе "Примечания".
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="options" /> не является одним из значений <see cref="T:System.StringSplitOptions" />.
    /// </exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public string[] Split(char[] separator, StringSplitOptions options)
    {
      return this.SplitInternal(separator, int.MaxValue, options);
    }

    /// <summary>
    ///   Разбивает строку на максимальное число подстрок в зависимости от символов в массиве.
    /// </summary>
    /// <param name="separator">
    ///   Массив символов, разделяющий подстроки в данной строке, пустой массив, не содержащий разделителей, или <see langword="null" />.
    /// </param>
    /// <param name="count">
    ///   Максимальное число возвращаемых подстрок.
    /// </param>
    /// <param name="options">
    ///   <see cref="F:System.StringSplitOptions.RemoveEmptyEntries" />, чтобы исключить пустые элементы из возвращаемого массива; или <see cref="F:System.StringSplitOptions.None" /> для включения пустых элементов в возвращаемый массив.
    /// </param>
    /// <returns>
    ///   Массив, элементы которого содержат подстроки данной строки, разделенные одним или более знаками из <paramref name="separator" />.
    ///    Дополнительные сведения см. в разделе "Примечания".
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="count" /> является отрицательным значением.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="options" /> не является одним из значений <see cref="T:System.StringSplitOptions" />.
    /// </exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public string[] Split(char[] separator, int count, StringSplitOptions options)
    {
      return this.SplitInternal(separator, count, options);
    }

    [ComVisible(false)]
    internal string[] SplitInternal(char[] separator, int count, StringSplitOptions options)
    {
      if (count < 0)
        throw new ArgumentOutOfRangeException(nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NegativeCount"));
      switch (options)
      {
        case StringSplitOptions.None:
        case StringSplitOptions.RemoveEmptyEntries:
          bool flag = options == StringSplitOptions.RemoveEmptyEntries;
          if (count == 0 || flag && this.Length == 0)
            return new string[0];
          int[] sepList = new int[this.Length];
          int numReplaces = this.MakeSeparatorList(separator, ref sepList);
          if (numReplaces == 0 || count == 1)
            return new string[1]{ this };
          if (flag)
            return this.InternalSplitOmitEmptyEntries(sepList, (int[]) null, numReplaces, count);
          return this.InternalSplitKeepEmptyEntries(sepList, (int[]) null, numReplaces, count);
        default:
          throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", (object) options));
      }
    }

    /// <summary>
    ///   Разбивает строку на подстроки в зависимости от строк в массиве.
    ///    Можно указать, включают ли подстроки пустые элементы массива.
    /// </summary>
    /// <param name="separator">
    ///   Массив строк, разделяющих подстроки в данной строке, пустой массив, не содержащий разделителей, или <see langword="null" />.
    /// </param>
    /// <param name="options">
    ///   <see cref="F:System.StringSplitOptions.RemoveEmptyEntries" />, чтобы исключить пустые элементы из возвращаемого массива; или <see cref="F:System.StringSplitOptions.None" /> для включения пустых элементов в возвращаемый массив.
    /// </param>
    /// <returns>
    ///   Массив, элементы которого содержат подстроки данной строки, разделенные одной или более строками из <paramref name="separator" />.
    ///    Дополнительные сведения см. в разделе "Примечания".
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="options" /> не является одним из значений <see cref="T:System.StringSplitOptions" />.
    /// </exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public string[] Split(string[] separator, StringSplitOptions options)
    {
      return this.Split(separator, int.MaxValue, options);
    }

    /// <summary>
    ///   Разбивает строку на максимальное число подстрок в зависимости от строк в массиве.
    ///    Можно указать, включают ли подстроки пустые элементы массива.
    /// </summary>
    /// <param name="separator">
    ///   Массив строк, разделяющих подстроки в данной строке, пустой массив, не содержащий разделителей, или <see langword="null" />.
    /// </param>
    /// <param name="count">
    ///   Максимальное число возвращаемых подстрок.
    /// </param>
    /// <param name="options">
    ///   <see cref="F:System.StringSplitOptions.RemoveEmptyEntries" />, чтобы исключить пустые элементы из возвращаемого массива; или <see cref="F:System.StringSplitOptions.None" /> для включения пустых элементов в возвращаемый массив.
    /// </param>
    /// <returns>
    ///   Массив, элементы которого содержат подстроки данной строки, разделенные одной или более строками из <paramref name="separator" />.
    ///    Дополнительные сведения см. в разделе "Примечания".
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="count" /> является отрицательным значением.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="options" /> не является одним из значений <see cref="T:System.StringSplitOptions" />.
    /// </exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public string[] Split(string[] separator, int count, StringSplitOptions options)
    {
      if (count < 0)
        throw new ArgumentOutOfRangeException(nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NegativeCount"));
      switch (options)
      {
        case StringSplitOptions.None:
        case StringSplitOptions.RemoveEmptyEntries:
          bool flag = options == StringSplitOptions.RemoveEmptyEntries;
          if (separator == null || separator.Length == 0)
            return this.SplitInternal((char[]) null, count, options);
          if (count == 0 || flag && this.Length == 0)
            return new string[0];
          int[] sepList = new int[this.Length];
          int[] lengthList = new int[this.Length];
          int numReplaces = this.MakeSeparatorList(separator, ref sepList, ref lengthList);
          if (numReplaces == 0 || count == 1)
            return new string[1]{ this };
          if (flag)
            return this.InternalSplitOmitEmptyEntries(sepList, lengthList, numReplaces, count);
          return this.InternalSplitKeepEmptyEntries(sepList, lengthList, numReplaces, count);
        default:
          throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", (object) options));
      }
    }

    private string[] InternalSplitKeepEmptyEntries(int[] sepList, int[] lengthList, int numReplaces, int count)
    {
      int startIndex = 0;
      int index1 = 0;
      --count;
      int num = numReplaces < count ? numReplaces : count;
      string[] strArray = new string[num + 1];
      for (int index2 = 0; index2 < num && startIndex < this.Length; ++index2)
      {
        strArray[index1++] = this.Substring(startIndex, sepList[index2] - startIndex);
        startIndex = sepList[index2] + (lengthList == null ? 1 : lengthList[index2]);
      }
      if (startIndex < this.Length && num >= 0)
        strArray[index1] = this.Substring(startIndex);
      else if (index1 == num)
        strArray[index1] = string.Empty;
      return strArray;
    }

    private string[] InternalSplitOmitEmptyEntries(int[] sepList, int[] lengthList, int numReplaces, int count)
    {
      int length1 = numReplaces < count ? numReplaces + 1 : count;
      string[] strArray1 = new string[length1];
      int startIndex = 0;
      int length2 = 0;
      for (int index = 0; index < numReplaces && startIndex < this.Length; ++index)
      {
        if (sepList[index] - startIndex > 0)
          strArray1[length2++] = this.Substring(startIndex, sepList[index] - startIndex);
        startIndex = sepList[index] + (lengthList == null ? 1 : lengthList[index]);
        if (length2 == count - 1)
        {
          while (index < numReplaces - 1 && startIndex == sepList[++index])
            startIndex += lengthList == null ? 1 : lengthList[index];
          break;
        }
      }
      if (startIndex < this.Length)
        strArray1[length2++] = this.Substring(startIndex);
      string[] strArray2 = strArray1;
      if (length2 != length1)
      {
        strArray2 = new string[length2];
        for (int index = 0; index < length2; ++index)
          strArray2[index] = strArray1[index];
      }
      return strArray2;
    }

    [SecuritySafeCritical]
    private unsafe int MakeSeparatorList(char[] separator, ref int[] sepList)
    {
      int num1 = 0;
      if (separator == null || separator.Length == 0)
      {
        fixed (char* chPtr = &this.m_firstChar)
        {
          for (int index = 0; index < this.Length && num1 < sepList.Length; ++index)
          {
            if (char.IsWhiteSpace(chPtr[index]))
              sepList[num1++] = index;
          }
        }
      }
      else
      {
        int length1 = sepList.Length;
        int length2 = separator.Length;
        fixed (char* chPtr1 = &this.m_firstChar)
          fixed (char* chPtr2 = separator)
          {
            for (int index = 0; index < this.Length && num1 < length1; ++index)
            {
              char* chPtr3 = chPtr2;
              int num2 = 0;
              while (num2 < length2)
              {
                if ((int) chPtr1[index] == (int) *chPtr3)
                {
                  sepList[num1++] = index;
                  break;
                }
                ++num2;
                chPtr3 += 2;
              }
            }
          }
      }
      return num1;
    }

    [SecuritySafeCritical]
    private unsafe int MakeSeparatorList(string[] separators, ref int[] sepList, ref int[] lengthList)
    {
      int index1 = 0;
      int length1 = sepList.Length;
      int length2 = separators.Length;
      fixed (char* chPtr = &this.m_firstChar)
      {
        for (int indexA = 0; indexA < this.Length && index1 < length1; ++indexA)
        {
          for (int index2 = 0; index2 < separators.Length; ++index2)
          {
            string separator = separators[index2];
            if (!string.IsNullOrEmpty(separator))
            {
              int length3 = separator.Length;
              if ((int) chPtr[indexA] == (int) separator[0] && length3 <= this.Length - indexA && (length3 == 1 || string.CompareOrdinal(this, indexA, separator, 0, length3) == 0))
              {
                sepList[index1] = indexA;
                lengthList[index1] = length3;
                ++index1;
                indexA += length3 - 1;
                break;
              }
            }
          }
        }
      }
      return index1;
    }

    /// <summary>
    ///   Извлекает подстроку из данного экземпляра.
    ///    Подстрока начинается в указанном положении символов и продолжается до конца строки.
    /// </summary>
    /// <param name="startIndex">
    ///   Отсчитываемая от нуля позиция первого знака подстроки в данном экземпляре.
    /// </param>
    /// <returns>
    ///   Строка, эквивалентная подстроке, которая начинается с <paramref name="startIndex" /> в данном экземпляре, или <see cref="F:System.String.Empty" />, если значение <paramref name="startIndex" /> равно длине данного экземпляра.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="startIndex" /> имеет значение меньше нуля или больше длины этого экземпляра.
    /// </exception>
    [__DynamicallyInvokable]
    public string Substring(int startIndex)
    {
      return this.Substring(startIndex, this.Length - startIndex);
    }

    /// <summary>
    ///   Извлекает подстроку из данного экземпляра.
    ///    Подстрока начинается с указанной позиции знака и имеет указанную длину.
    /// </summary>
    /// <param name="startIndex">
    ///   Отсчитываемая от нуля позиция первого знака подстроки в данном экземпляре.
    /// </param>
    /// <param name="length">Число символов в подстроке.</param>
    /// <returns>
    ///   Строка, эквивалентная подстроке длиной <paramref name="length" />, которая начинается с <paramref name="startIndex" /> в данном экземпляре, или <see cref="F:System.String.Empty" />, если значение <paramref name="startIndex" /> равно длине данного экземпляра, а значение <paramref name="length" /> равно нулю.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="startIndex" /> плюс <paramref name="length" /> указывает на позицию за пределами данного экземпляра.
    /// 
    ///   -или-
    /// 
    ///   Значение параметра <paramref name="startIndex" /> или <paramref name="length" /> меньше нуля.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public string Substring(int startIndex, int length)
    {
      if (startIndex < 0)
        throw new ArgumentOutOfRangeException(nameof (startIndex), Environment.GetResourceString("ArgumentOutOfRange_StartIndex"));
      if (startIndex > this.Length)
        throw new ArgumentOutOfRangeException(nameof (startIndex), Environment.GetResourceString("ArgumentOutOfRange_StartIndexLargerThanLength"));
      if (length < 0)
        throw new ArgumentOutOfRangeException(nameof (length), Environment.GetResourceString("ArgumentOutOfRange_NegativeLength"));
      if (startIndex > this.Length - length)
        throw new ArgumentOutOfRangeException(nameof (length), Environment.GetResourceString("ArgumentOutOfRange_IndexLength"));
      if (length == 0)
        return string.Empty;
      if (startIndex == 0 && length == this.Length)
        return this;
      return this.InternalSubString(startIndex, length);
    }

    [SecurityCritical]
    private unsafe string InternalSubString(int startIndex, int length)
    {
      string str = string.FastAllocateString(length);
      fixed (char* dmem = &str.m_firstChar)
        fixed (char* chPtr = &this.m_firstChar)
          string.wstrcpy(dmem, chPtr + startIndex, length);
      return str;
    }

    /// <summary>
    ///   Удаляет все начальные и конечные вхождения набора знаков, заданного в виде массива, из текущего объекта <see cref="T:System.String" />.
    /// </summary>
    /// <param name="trimChars">
    ///   Массив удаляемых знаков Юникода или <see langword="null" />.
    /// </param>
    /// <returns>
    ///   Строка, оставшаяся после удаления всех вхождений символов, заданных в параметре <paramref name="trimChars" />, из начала и конца текущей строки.
    ///    Если значением параметра <paramref name="trimChars" /> является <see langword="null" /> или пустой массив, удаляются символы-разделители.
    ///    Если в текущем экземпляре невозможно усечь символы, метод возвращает текущий экземпляр без изменений.
    /// </returns>
    [__DynamicallyInvokable]
    public string Trim(params char[] trimChars)
    {
      if (trimChars == null || trimChars.Length == 0)
        return this.TrimHelper(2);
      return this.TrimHelper(trimChars, 2);
    }

    /// <summary>
    ///   Удаляет все начальные вхождения набора знаков, заданного в виде массива, из текущего объекта <see cref="T:System.String" />.
    /// </summary>
    /// <param name="trimChars">
    ///   Массив удаляемых знаков Юникода или <see langword="null" />.
    /// </param>
    /// <returns>
    ///   Строка, оставшаяся после удаления всех вхождений символов, заданных в параметре <paramref name="trimChars" />, из начала текущей строки.
    ///    Если значением параметра <paramref name="trimChars" /> является <see langword="null" /> или пустой массив, удаляются символы-разделители.
    /// </returns>
    [__DynamicallyInvokable]
    public string TrimStart(params char[] trimChars)
    {
      if (trimChars == null || trimChars.Length == 0)
        return this.TrimHelper(0);
      return this.TrimHelper(trimChars, 0);
    }

    /// <summary>
    ///   Удаляет все конечные вхождения набора знаков, заданного в виде массива, из текущего объекта <see cref="T:System.String" />.
    /// </summary>
    /// <param name="trimChars">
    ///   Массив удаляемых знаков Юникода или <see langword="null" />.
    /// </param>
    /// <returns>
    ///   Строка, оставшаяся после удаления всех вхождений символов, заданных в параметре <paramref name="trimChars" />, из конца текущей строки.
    ///    Если значением параметра <paramref name="trimChars" /> является <see langword="null" /> или пустой массив, удаляются символы-разделители в Юникоде.
    ///    Если в текущем экземпляре невозможно усечь символы, метод возвращает текущий экземпляр без изменений.
    /// </returns>
    [__DynamicallyInvokable]
    public string TrimEnd(params char[] trimChars)
    {
      if (trimChars == null || trimChars.Length == 0)
        return this.TrimHelper(1);
      return this.TrimHelper(trimChars, 1);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.String" /> значением, определенным указателем на массив знаков Юникода.
    /// </summary>
    /// <param name="value">
    ///   Указатель на строку знаков в кодировке Юникод, завершающуюся нулевым значением.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Текущий процесс не имеет доступа на чтение ко всем рассматриваемым символам.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="value" /> указывает массив, содержащий недопустимый символ Юникода, или <paramref name="value" /> определяет адрес меньше 64 000.
    /// </exception>
    [SecurityCritical]
    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public extern unsafe String(char* value);

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.String" /> значением, которое определяется заданным указателем на массив знаков Юникода, начальной позицией знака в этом массиве и длиной.
    /// </summary>
    /// <param name="value">Указатель на массив знаков Юникода.</param>
    /// <param name="startIndex">
    ///   Начальная позиция в <paramref name="value" />.
    /// </param>
    /// <param name="length">
    ///   Используемое количество знаков в <paramref name="value" />.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="startIndex" /> или <paramref name="length" /> меньше нуля, <paramref name="value" /> + <paramref name="startIndex" /> приводит к переполнению указателя, или текущий процесс не имеет доступа на чтение ко всем рассматриваемым символам.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="value" /> указывает массив, содержащий недопустимый символ Юникода, или <paramref name="value" /> + <paramref name="startIndex" /> определяет адрес меньше 64000.
    /// </exception>
    [SecurityCritical]
    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public extern unsafe String(char* value, int startIndex, int length);

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.String" /> значением, определенным указателем на массив 8-разрядных целых чисел со знаком.
    /// </summary>
    /// <param name="value">
    ///   Указатель на массив 8-разрядных целых чисел со знаком, завершающийся нулевым значением.
    ///    Целые числа интерпретируются с использованием текущей системы кодировки страницы системным кодом (то есть, кодировки, заданной <see cref="P:System.Text.Encoding.Default" />).
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Не удалось инициализировать новый экземпляр <see cref="T:System.String" /> с помощью <paramref name="value" />, если предполагается, что <paramref name="value" /> представлен в кодировке ANSI.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Длина новой инициализируемой строки, определяемая символом, завершающимся нулевым значением <paramref name="value" />, слишком велика для выделения.
    /// </exception>
    /// <exception cref="T:System.AccessViolationException">
    ///   <paramref name="value" /> указывает недопустимый адрес.
    /// </exception>
    [SecurityCritical]
    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public extern unsafe String(sbyte* value);

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.String" /> значением, определяемым заданным указателем на массив 8-разрядных целых чисел со знаком, начальной позицией в пределах массива и длиной.
    /// </summary>
    /// <param name="value">
    ///   Указатель на массив 8-разрядных целых чисел со знаком.
    ///    Целые числа интерпретируются с использованием текущей системы кодировки страницы системным кодом (то есть, кодировки, заданной <see cref="P:System.Text.Encoding.Default" />).
    /// </param>
    /// <param name="startIndex">
    ///   Начальная позиция в <paramref name="value" />.
    /// </param>
    /// <param name="length">
    ///   Используемое количество знаков в <paramref name="value" />.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="startIndex" /> или <paramref name="length" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   Адрес, указанный значениями <paramref name="value" /> + <paramref name="startIndex" />, слишком велик для текущей платформы, то есть при вычислении адреса произошло переполнение.
    /// 
    ///   -или-
    /// 
    ///   Длина новой инициализируемой строки слишком велика для выделения.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Адрес, указанный значениями <paramref name="value" /> + <paramref name="startIndex" />, меньше 64 КБ.
    /// 
    ///   -или-
    /// 
    ///   Не удалось инициализировать новый экземпляр <see cref="T:System.String" /> с помощью <paramref name="value" />, исходя из предположения, что значение <paramref name="value" /> представлено в кодировке ANSI.
    /// </exception>
    /// <exception cref="T:System.AccessViolationException">
    ///   <paramref name="value" />, <paramref name="startIndex" /> и <paramref name="length" /> совместно определяют недопустимый адрес.
    /// </exception>
    [SecurityCritical]
    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public extern unsafe String(sbyte* value, int startIndex, int length);

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.String" /> значением, определенным заданным указателем на массив 8-разрядных целых чисел со знаком, начальной позицией в пределах этого массива, длиной и объектом <see cref="T:System.Text.Encoding" />.
    /// </summary>
    /// <param name="value">
    ///   Указатель на массив 8-разрядных целых чисел со знаком.
    /// </param>
    /// <param name="startIndex">
    ///   Начальная позиция в <paramref name="value" />.
    /// </param>
    /// <param name="length">
    ///   Используемое количество знаков в <paramref name="value" />.
    /// </param>
    /// <param name="enc">
    ///   Объект, определяющий способ кодировки массива, на который указывает параметр <paramref name="value" />.
    ///    Если значением параметра <paramref name="enc" /> является <see langword="null" />, предполагается кодировка ANSI.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="startIndex" /> или <paramref name="length" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   Адрес, указанный значениями <paramref name="value" /> + <paramref name="startIndex" />, слишком велик для текущей платформы, то есть при вычислении адреса произошло переполнение.
    /// 
    ///   -или-
    /// 
    ///   Длина новой инициализируемой строки слишком велика для выделения.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Адрес, указанный значениями <paramref name="value" /> + <paramref name="startIndex" />, меньше 64 КБ.
    /// 
    ///   -или-
    /// 
    ///   Не удалось инициализировать новый экземпляр <see cref="T:System.String" /> с помощью <paramref name="value" />, если предполагается, что <paramref name="value" /> представлен в кодировке <paramref name="enc" />.
    /// </exception>
    /// <exception cref="T:System.AccessViolationException">
    ///   <paramref name="value" />, <paramref name="startIndex" /> и <paramref name="length" /> совместно определяют недопустимый адрес.
    /// </exception>
    [SecurityCritical]
    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public extern unsafe String(sbyte* value, int startIndex, int length, Encoding enc);

    [SecurityCritical]
    private static unsafe string CreateString(sbyte* value, int startIndex, int length, Encoding enc)
    {
      if (enc == null)
        return new string(value, startIndex, length);
      if (length < 0)
        throw new ArgumentOutOfRangeException(nameof (length), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (startIndex < 0)
        throw new ArgumentOutOfRangeException(nameof (startIndex), Environment.GetResourceString("ArgumentOutOfRange_StartIndex"));
      if (value + startIndex < value)
        throw new ArgumentOutOfRangeException(nameof (startIndex), Environment.GetResourceString("ArgumentOutOfRange_PartialWCHAR"));
      byte[] numArray = new byte[length];
      try
      {
        Buffer.Memcpy(numArray, 0, (byte*) value, startIndex, length);
      }
      catch (NullReferenceException ex)
      {
        throw new ArgumentOutOfRangeException(nameof (value), Environment.GetResourceString("ArgumentOutOfRange_PartialWCHAR"));
      }
      return enc.GetString(numArray);
    }

    [SecurityCritical]
    internal static unsafe string CreateStringFromEncoding(byte* bytes, int byteLength, Encoding encoding)
    {
      int charCount = encoding.GetCharCount(bytes, byteLength, (DecoderNLS) null);
      if (charCount == 0)
        return string.Empty;
      string str = string.FastAllocateString(charCount);
      fixed (char* chars = &str.m_firstChar)
        encoding.GetChars(bytes, byteLength, chars, charCount, (DecoderNLS) null);
      return str;
    }

    [SecuritySafeCritical]
    internal unsafe int GetBytesFromEncoding(byte* pbNativeBuffer, int cbNativeBuffer, Encoding encoding)
    {
      fixed (char* chars = &this.m_firstChar)
        return encoding.GetBytes(chars, this.m_stringLength, pbNativeBuffer, cbNativeBuffer);
    }

    [SecuritySafeCritical]
    internal unsafe int ConvertToAnsi(byte* pbNativeBuffer, int cbNativeBuffer, bool fBestFit, bool fThrowOnUnmappableChar)
    {
      uint flags = fBestFit ? 0U : 1024U;
      uint num = 0;
      int multiByte;
      fixed (char* pwzSource = &this.m_firstChar)
        multiByte = Win32Native.WideCharToMultiByte(0U, flags, pwzSource, this.Length, pbNativeBuffer, cbNativeBuffer, IntPtr.Zero, fThrowOnUnmappableChar ? new IntPtr((void*) &num) : IntPtr.Zero);
      if (num != 0U)
        throw new ArgumentException(Environment.GetResourceString("Interop_Marshal_Unmappable_Char"));
      pbNativeBuffer[multiByte] = (byte) 0;
      return multiByte;
    }

    /// <summary>
    ///   Указывает, находится ли данная строка в форме нормализации Юникода C.
    /// </summary>
    /// <returns>
    ///   <see langword="true" />, если данная строка находится в форме нормализации Юникода C; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Текущий экземпляр содержит недопустимые символы Юникода.
    /// </exception>
    public bool IsNormalized()
    {
      return this.IsNormalized(NormalizationForm.FormC);
    }

    /// <summary>
    ///   Указывает, находится ли данная строка в заданной форме нормализации Юникода.
    /// </summary>
    /// <param name="normalizationForm">
    ///   Форма нормализации Юникода.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если строка находится в форме нормализации, указанной в параметре <paramref name="normalizationForm" />; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Текущий экземпляр содержит недопустимые символы Юникода.
    /// </exception>
    [SecuritySafeCritical]
    public bool IsNormalized(NormalizationForm normalizationForm)
    {
      if (this.IsFastSort() && (normalizationForm == NormalizationForm.FormC || normalizationForm == NormalizationForm.FormKC || (normalizationForm == NormalizationForm.FormD || normalizationForm == NormalizationForm.FormKD)))
        return true;
      return Normalization.IsNormalized(this, normalizationForm);
    }

    /// <summary>
    ///   Возвращает новую строку, текстовое значение которой совпадает с данной строкой, а двоичное представление находится в нормализованной форме C Юникода.
    /// </summary>
    /// <returns>
    ///   Новая нормализованная строка, текстовое значение которой совпадает с данной строкой, а двоичное представление находится в нормализованной форме C Юникода.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Текущий экземпляр содержит недопустимые символы Юникода.
    /// </exception>
    public string Normalize()
    {
      return this.Normalize(NormalizationForm.FormC);
    }

    /// <summary>
    ///   Возвращает новую строку, текстовое значение которой совпадает с данной строкой, а двоичное представление находится в заданной нормализованной форме Юникода.
    /// </summary>
    /// <param name="normalizationForm">
    ///   Форма нормализации Юникода.
    /// </param>
    /// <returns>
    ///   Новая строка, текстовое значение которой совпадает с данной строкой, а двоичное представление находится в форме нормализации, заданной в параметре <paramref name="normalizationForm" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Текущий экземпляр содержит недопустимые символы Юникода.
    /// </exception>
    [SecuritySafeCritical]
    public string Normalize(NormalizationForm normalizationForm)
    {
      if (this.IsAscii() && (normalizationForm == NormalizationForm.FormC || normalizationForm == NormalizationForm.FormKC || (normalizationForm == NormalizationForm.FormD || normalizationForm == NormalizationForm.FormKD)))
        return this;
      return Normalization.Normalize(this, normalizationForm);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern string FastAllocateString(int length);

    [SecuritySafeCritical]
    private static unsafe void FillStringChecked(string dest, int destPos, string src)
    {
      if (src.Length > dest.Length - destPos)
        throw new IndexOutOfRangeException();
      fixed (char* chPtr = &dest.m_firstChar)
        fixed (char* smem = &src.m_firstChar)
          string.wstrcpy(chPtr + destPos, smem, src.Length);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.String" /> значением, заданным массивом знаков Юникода, начальной позицией знака в пределах данного массива и длиной.
    /// </summary>
    /// <param name="value">Массив знаков Юникода.</param>
    /// <param name="startIndex">
    ///   Начальная позиция в <paramref name="value" />.
    /// </param>
    /// <param name="length">
    ///   Используемое количество знаков в <paramref name="value" />.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="startIndex" /> или <paramref name="length" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   Сумма <paramref name="startIndex" /> и <paramref name="length" /> больше, чем число элементов в <paramref name="value" />.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public extern String(char[] value, int startIndex, int length);

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.String" /> значением, заданным в виде массива знаков Юникода.
    /// </summary>
    /// <param name="value">Массив знаков Юникода.</param>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public extern String(char[] value);

    [SecurityCritical]
    internal static unsafe void wstrcpy(char* dmem, char* smem, int charCount)
    {
      Buffer.Memcpy((byte*) dmem, (byte*) smem, charCount * 2);
    }

    [SecuritySafeCritical]
    private unsafe string CtorCharArray(char[] value)
    {
      if (value == null || value.Length == 0)
        return string.Empty;
      string str1 = string.FastAllocateString(value.Length);
      string str2 = str1;
      char* dmem = (char*) str2;
      if ((IntPtr) dmem != IntPtr.Zero)
        dmem += RuntimeHelpers.OffsetToStringData;
      fixed (char* smem = value)
      {
        string.wstrcpy(dmem, smem, value.Length);
        str2 = (string) null;
      }
      return str1;
    }

    [SecuritySafeCritical]
    private unsafe string CtorCharArrayStartLength(char[] value, int startIndex, int length)
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      if (startIndex < 0)
        throw new ArgumentOutOfRangeException(nameof (startIndex), Environment.GetResourceString("ArgumentOutOfRange_StartIndex"));
      if (length < 0)
        throw new ArgumentOutOfRangeException(nameof (length), Environment.GetResourceString("ArgumentOutOfRange_NegativeLength"));
      if (startIndex > value.Length - length)
        throw new ArgumentOutOfRangeException(nameof (startIndex), Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (length <= 0)
        return string.Empty;
      string str1 = string.FastAllocateString(length);
      string str2 = str1;
      char* dmem = (char*) str2;
      if ((IntPtr) dmem != IntPtr.Zero)
        dmem += RuntimeHelpers.OffsetToStringData;
      fixed (char* chPtr = value)
      {
        string.wstrcpy(dmem, chPtr + startIndex, length);
        str2 = (string) null;
      }
      return str1;
    }

    [SecuritySafeCritical]
    private unsafe string CtorCharCount(char c, int count)
    {
      if (count > 0)
      {
        string str1 = string.FastAllocateString(count);
        if (c != char.MinValue)
        {
          string str2 = str1;
          char* chPtr1 = (char*) str2;
          if ((IntPtr) chPtr1 != IntPtr.Zero)
            chPtr1 += RuntimeHelpers.OffsetToStringData;
          char* chPtr2;
          for (chPtr2 = chPtr1; ((int) (uint) chPtr2 & 3) != 0 && count > 0; --count)
            *chPtr2++ = c;
          uint num = (uint) c << 16 | (uint) c;
          if (count >= 4)
          {
            count -= 4;
            do
            {
              *(int*) chPtr2 = (int) num;
              *(int*) ((IntPtr) chPtr2 + 4) = (int) num;
              chPtr2 += 4;
              count -= 4;
            }
            while (count >= 0);
          }
          if ((count & 2) != 0)
          {
            *(int*) chPtr2 = (int) num;
            chPtr2 += 2;
          }
          if ((count & 1) != 0)
            *chPtr2 = c;
          str2 = (string) null;
        }
        return str1;
      }
      if (count == 0)
        return string.Empty;
      throw new ArgumentOutOfRangeException(nameof (count), Environment.GetResourceString("ArgumentOutOfRange_MustBeNonNegNum", (object) nameof (count)));
    }

    [SecurityCritical]
    private static unsafe int wcslen(char* ptr)
    {
      char* chPtr = ptr;
      while (((int) (uint) chPtr & 3) != 0 && *chPtr != char.MinValue)
        chPtr += 2;
      if (*chPtr != char.MinValue)
      {
        while (((int) *chPtr & (int) *(ushort*) ((IntPtr) chPtr + 2)) != 0 || *chPtr != char.MinValue && *(ushort*) ((IntPtr) chPtr + 2) != (ushort) 0)
          chPtr += 2;
      }
      while (*chPtr != char.MinValue)
        chPtr += 2;
      return (int) (chPtr - ptr);
    }

    [SecurityCritical]
    private unsafe string CtorCharPtr(char* ptr)
    {
      if ((IntPtr) ptr == IntPtr.Zero)
        return string.Empty;
      if ((UIntPtr) ptr < new UIntPtr(64000))
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeStringPtrNotAtom"));
      try
      {
        int num = string.wcslen(ptr);
        if (num == 0)
          return string.Empty;
        string str1 = string.FastAllocateString(num);
        string str2;
        try
        {
          str2 = str1;
          char* dmem = (char*) str2;
          if ((IntPtr) dmem != IntPtr.Zero)
            dmem += RuntimeHelpers.OffsetToStringData;
          string.wstrcpy(dmem, ptr, num);
        }
        finally
        {
          str2 = (string) null;
        }
        return str1;
      }
      catch (NullReferenceException ex)
      {
        throw new ArgumentOutOfRangeException(nameof (ptr), Environment.GetResourceString("ArgumentOutOfRange_PartialWCHAR"));
      }
    }

    [SecurityCritical]
    private unsafe string CtorCharPtrStartLength(char* ptr, int startIndex, int length)
    {
      if (length < 0)
        throw new ArgumentOutOfRangeException(nameof (length), Environment.GetResourceString("ArgumentOutOfRange_NegativeLength"));
      if (startIndex < 0)
        throw new ArgumentOutOfRangeException(nameof (startIndex), Environment.GetResourceString("ArgumentOutOfRange_StartIndex"));
      char* smem = ptr + startIndex;
      if (smem < ptr)
        throw new ArgumentOutOfRangeException(nameof (startIndex), Environment.GetResourceString("ArgumentOutOfRange_PartialWCHAR"));
      if (length == 0)
        return string.Empty;
      string str1 = string.FastAllocateString(length);
      try
      {
        string str2;
        try
        {
          str2 = str1;
          char* dmem = (char*) str2;
          if ((IntPtr) dmem != IntPtr.Zero)
            dmem += RuntimeHelpers.OffsetToStringData;
          string.wstrcpy(dmem, smem, length);
        }
        finally
        {
          str2 = (string) null;
        }
        return str1;
      }
      catch (NullReferenceException ex)
      {
        throw new ArgumentOutOfRangeException(nameof (ptr), Environment.GetResourceString("ArgumentOutOfRange_PartialWCHAR"));
      }
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.String" /> значением, полученным путем повторения заданного знака Юникода указанное число раз.
    /// </summary>
    /// <param name="c">Знак Юникода.</param>
    /// <param name="count">
    ///   Количество повторов <paramref name="c" />.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="count" /> меньше нуля.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public extern String(char c, int count);

    /// <summary>
    ///   Сравнивает два указанных объекта <see cref="T:System.String" /> и возвращает целое число, которое показывает их относительное положение в порядке сортировки.
    /// </summary>
    /// <param name="strA">Первая сравниваемая строка.</param>
    /// <param name="strB">Вторая сравниваемая строка.</param>
    /// <returns>
    /// 32-битовое целое число со знаком, выражающее лексическое отношение двух сравниваемых значений.
    /// 
    ///         Значение
    /// 
    ///         Условие
    /// 
    ///         Меньше нуля
    /// 
    ///         <paramref name="strA" /> предшествует <paramref name="strB" /> в порядке сортировки.
    /// 
    ///         Нуль
    /// 
    ///         <paramref name="strA" /> занимает ту же позицию в порядке сортировки, что и объект <paramref name="strB" />.
    /// 
    ///         Больше нуля
    /// 
    ///         <paramref name="strA" /> следует за <paramref name="strB" /> в порядке сортировки.
    ///       </returns>
    [__DynamicallyInvokable]
    public static int Compare(string strA, string strB)
    {
      return CultureInfo.CurrentCulture.CompareInfo.Compare(strA, strB, CompareOptions.None);
    }

    /// <summary>
    ///   Сравнивает два указанных объекта <see cref="T:System.String" /> (с учетом или без учета регистра) и возвращает целое число, которое показывает их относительное положение в порядке сортировки.
    /// </summary>
    /// <param name="strA">Первая сравниваемая строка.</param>
    /// <param name="strB">Вторая сравниваемая строка.</param>
    /// <param name="ignoreCase">
    ///   Значение <see langword="true" />, чтобы не учитывать регистр при сравнении; в противном случае — значение <see langword="false" />.
    /// </param>
    /// <returns>
    /// 32-битовое целое число со знаком, выражающее лексическое отношение двух сравниваемых значений.
    /// 
    ///         Значение
    /// 
    ///         Условие
    /// 
    ///         Меньше нуля
    /// 
    ///         <paramref name="strA" /> предшествует <paramref name="strB" /> в порядке сортировки.
    /// 
    ///         Нуль
    /// 
    ///         <paramref name="strA" /> занимает ту же позицию в порядке сортировки, что и объект <paramref name="strB" />.
    /// 
    ///         Больше нуля
    /// 
    ///         <paramref name="strA" /> следует за <paramref name="strB" /> в порядке сортировки.
    ///       </returns>
    [__DynamicallyInvokable]
    public static int Compare(string strA, string strB, bool ignoreCase)
    {
      if (ignoreCase)
        return CultureInfo.CurrentCulture.CompareInfo.Compare(strA, strB, CompareOptions.IgnoreCase);
      return CultureInfo.CurrentCulture.CompareInfo.Compare(strA, strB, CompareOptions.None);
    }

    /// <summary>
    ///   Сравнивает два указанных объекта <see cref="T:System.String" /> с использованием заданных правил и возвращает целое число, которое показывает их относительное положение в порядке сортировки.
    /// </summary>
    /// <param name="strA">Первая сравниваемая строка.</param>
    /// <param name="strB">Вторая сравниваемая строка.</param>
    /// <param name="comparisonType">
    ///   Одно из значений перечисления, определяющее используемые при сравнении правила.
    /// </param>
    /// <returns>
    /// 32-битовое целое число со знаком, выражающее лексическое отношение двух сравниваемых значений.
    /// 
    ///         Значение
    /// 
    ///         Условие
    /// 
    ///         Меньше нуля
    /// 
    ///         <paramref name="strA" /> предшествует <paramref name="strB" /> в порядке сортировки.
    /// 
    ///         Нуль
    /// 
    ///         <paramref name="strA" /> занимает ту же позицию в порядке сортировки, что и объект <paramref name="strB" />.
    /// 
    ///         Больше нуля
    /// 
    ///         <paramref name="strA" /> следует за <paramref name="strB" /> в порядке сортировки.
    ///       </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="comparisonType" /> не является значением <see cref="T:System.StringComparison" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Тип <see cref="T:System.StringComparison" /> не поддерживается.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static int Compare(string strA, string strB, StringComparison comparisonType)
    {
      if ((uint) (comparisonType - 0) > 5U)
        throw new ArgumentException(Environment.GetResourceString("NotSupported_StringComparison"), nameof (comparisonType));
      if ((object) strA == (object) strB)
        return 0;
      if (strA == null)
        return -1;
      if (strB == null)
        return 1;
      switch (comparisonType)
      {
        case StringComparison.CurrentCulture:
          return CultureInfo.CurrentCulture.CompareInfo.Compare(strA, strB, CompareOptions.None);
        case StringComparison.CurrentCultureIgnoreCase:
          return CultureInfo.CurrentCulture.CompareInfo.Compare(strA, strB, CompareOptions.IgnoreCase);
        case StringComparison.InvariantCulture:
          return CultureInfo.InvariantCulture.CompareInfo.Compare(strA, strB, CompareOptions.None);
        case StringComparison.InvariantCultureIgnoreCase:
          return CultureInfo.InvariantCulture.CompareInfo.Compare(strA, strB, CompareOptions.IgnoreCase);
        case StringComparison.Ordinal:
          if ((int) strA.m_firstChar - (int) strB.m_firstChar != 0)
            return (int) strA.m_firstChar - (int) strB.m_firstChar;
          return string.CompareOrdinalHelper(strA, strB);
        case StringComparison.OrdinalIgnoreCase:
          if (strA.IsAscii() && strB.IsAscii())
            return string.CompareOrdinalIgnoreCaseHelper(strA, strB);
          return TextInfo.CompareOrdinalIgnoreCase(strA, strB);
        default:
          throw new NotSupportedException(Environment.GetResourceString("NotSupported_StringComparison"));
      }
    }

    /// <summary>
    ///   Сравнивает два заданных объекта <see cref="T:System.String" />, используя указанные параметры сравнения и сведения о языке и региональных параметрах, которые влияют на сравнение, и возвращает целое число, показывающее связь между двумя строками в порядке сортировки.
    /// </summary>
    /// <param name="strA">Первая сравниваемая строка.</param>
    /// <param name="strB">Вторая сравниваемая строка.</param>
    /// <param name="culture">
    ///   Язык и региональные параметры, которые предоставляют сведения об особенностях сравнения с учетом языка и региональных параметров.
    /// </param>
    /// <param name="options">
    ///   Параметры, которые используются во время сравнения (например, игнорирование регистра или символов).
    /// </param>
    /// <returns>
    /// 32-разрядное целое число со знаком, которое указывает на лексические отношения между <paramref name="strA" /> и <paramref name="strB" />, как показано в следующей таблице.
    /// 
    ///         Значение
    /// 
    ///         Условие
    /// 
    ///         Меньше нуля
    /// 
    ///         <paramref name="strA" /> предшествует <paramref name="strB" /> в порядке сортировки.
    /// 
    ///         Нуль
    /// 
    ///         <paramref name="strA" /> занимает ту же позицию в порядке сортировки, что и объект <paramref name="strB" />.
    /// 
    ///         Больше нуля
    /// 
    ///         <paramref name="strA" /> следует за <paramref name="strB" /> в порядке сортировки.
    ///       </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="options" /> не является значением <see cref="T:System.Globalization.CompareOptions" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="culture" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static int Compare(string strA, string strB, CultureInfo culture, CompareOptions options)
    {
      if (culture == null)
        throw new ArgumentNullException(nameof (culture));
      return culture.CompareInfo.Compare(strA, strB, options);
    }

    /// <summary>
    ///   Сравнивает два указанных объекта <see cref="T:System.String" /> (с учетом или без учета регистра), используя сведения о языке и региональных параметрах, и возвращает целое число, которое показывает их относительное положение в порядке сортировки.
    /// </summary>
    /// <param name="strA">Первая сравниваемая строка.</param>
    /// <param name="strB">Вторая сравниваемая строка.</param>
    /// <param name="ignoreCase">
    ///   Значение <see langword="true" />, чтобы не учитывать регистр при сравнении; в противном случае — значение <see langword="false" />.
    /// </param>
    /// <param name="culture">
    ///   Объект, предоставляющий сведения об особенностях сравнения, определяемых языком и региональными параметрами.
    /// </param>
    /// <returns>
    /// 32-битовое целое число со знаком, выражающее лексическое отношение двух сравниваемых значений.
    /// 
    ///         Значение
    /// 
    ///         Условие
    /// 
    ///         Меньше нуля
    /// 
    ///         <paramref name="strA" /> предшествует <paramref name="strB" /> в порядке сортировки.
    /// 
    ///         Нуль
    /// 
    ///         <paramref name="strA" /> занимает ту же позицию в порядке сортировки, что и объект <paramref name="strB" />.
    /// 
    ///         Больше нуля
    /// 
    ///         <paramref name="strA" /> следует за <paramref name="strB" /> в порядке сортировки.
    ///       </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="culture" /> имеет значение <see langword="null" />.
    /// </exception>
    public static int Compare(string strA, string strB, bool ignoreCase, CultureInfo culture)
    {
      if (culture == null)
        throw new ArgumentNullException(nameof (culture));
      if (ignoreCase)
        return culture.CompareInfo.Compare(strA, strB, CompareOptions.IgnoreCase);
      return culture.CompareInfo.Compare(strA, strB, CompareOptions.None);
    }

    /// <summary>
    ///   Сравнивает подстроки двух указанных объектов <see cref="T:System.String" /> и возвращает целое число, которое показывает их относительное положение в порядке сортировки.
    /// </summary>
    /// <param name="strA">Первая из сравниваемых строк.</param>
    /// <param name="indexA">
    ///   Позиция подстроки в <paramref name="strA" />.
    /// </param>
    /// <param name="strB">Вторая из сравниваемых строк.</param>
    /// <param name="indexB">
    ///   Позиция подстроки в <paramref name="strB" />.
    /// </param>
    /// <param name="length">
    ///   Максимальное число сравниваемых знаков в подстроках.
    /// </param>
    /// <returns>
    /// 32-разрядное целое число со знаком, выражающее лексическое соотношение двух сравниваемых значений.
    /// 
    ///         Значение
    /// 
    ///         Условие
    /// 
    ///         Меньше нуля
    /// 
    ///         Подстрока в <paramref name="strA" /> предшествует подстроке в <paramref name="strB" /> в порядке сортировки.
    /// 
    ///         Нуль
    /// 
    ///         Подстроки появляются в той же позиции в порядке сортировки, или параметр <paramref name="length" /> равен нулю.
    /// 
    ///         Больше нуля
    /// 
    ///         Подстрока в <paramref name="strA" /> следует за подстрокой в <paramref name="strB" /> в порядке сортировки.
    ///       </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение <paramref name="indexA" /> больше <paramref name="strA" />.<see cref="P:System.String.Length" />
    /// 
    ///   -или-
    /// 
    ///   Значение <paramref name="indexB" /> больше значения <paramref name="strB" /><see cref="P:System.String.Length" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="indexA" />, <paramref name="indexB" /> или <paramref name="length" /> является отрицательным значением.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="indexA" /> или <paramref name="indexB" /> равно <see langword="null" />, а <paramref name="length" /> больше нуля.
    /// </exception>
    [__DynamicallyInvokable]
    public static int Compare(string strA, int indexA, string strB, int indexB, int length)
    {
      int length1 = length;
      int length2 = length;
      if (strA != null && strA.Length - indexA < length1)
        length1 = strA.Length - indexA;
      if (strB != null && strB.Length - indexB < length2)
        length2 = strB.Length - indexB;
      return CultureInfo.CurrentCulture.CompareInfo.Compare(strA, indexA, length1, strB, indexB, length2, CompareOptions.None);
    }

    /// <summary>
    ///   Сравнивает подстроки двух заданных объектов <see cref="T:System.String" /> (с учетом или без учета регистра) и возвращает целое число, которое показывает их относительное положение в порядке сортировки.
    /// </summary>
    /// <param name="strA">Первая из сравниваемых строк.</param>
    /// <param name="indexA">
    ///   Позиция подстроки в <paramref name="strA" />.
    /// </param>
    /// <param name="strB">Вторая из сравниваемых строк.</param>
    /// <param name="indexB">
    ///   Позиция подстроки в <paramref name="strB" />.
    /// </param>
    /// <param name="length">
    ///   Максимальное число сравниваемых знаков в подстроках.
    /// </param>
    /// <param name="ignoreCase">
    ///   Значение <see langword="true" />, чтобы не учитывать регистр при сравнении; в противном случае — значение <see langword="false" />.
    /// </param>
    /// <returns>
    /// 32-битовое целое число со знаком, выражающее лексическое отношение двух сравниваемых значений.
    /// 
    ///         Значение
    /// 
    ///         Условие
    /// 
    ///         Меньше нуля
    /// 
    ///         Подстрока в <paramref name="strA" /> предшествует подстроке в <paramref name="strB" /> в порядке сортировки.
    /// 
    ///         Нуль
    /// 
    ///         Подстроки появляются в той же позиции в порядке сортировки, или параметр <paramref name="length" /> равен нулю.
    /// 
    ///         Больше нуля
    /// 
    ///         Подстрока в <paramref name="strA" /> следует за подстрокой в <paramref name="strB" /> в порядке сортировки.
    ///       </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение <paramref name="indexA" /> больше <paramref name="strA" />.<see cref="P:System.String.Length" />
    /// 
    ///   -или-
    /// 
    ///   Значение <paramref name="indexB" /> больше значения <paramref name="strB" /><see cref="P:System.String.Length" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="indexA" />, <paramref name="indexB" /> или <paramref name="length" /> является отрицательным значением.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="indexA" /> или <paramref name="indexB" /> равно <see langword="null" />, а <paramref name="length" /> больше нуля.
    /// </exception>
    public static int Compare(string strA, int indexA, string strB, int indexB, int length, bool ignoreCase)
    {
      int length1 = length;
      int length2 = length;
      if (strA != null && strA.Length - indexA < length1)
        length1 = strA.Length - indexA;
      if (strB != null && strB.Length - indexB < length2)
        length2 = strB.Length - indexB;
      if (ignoreCase)
        return CultureInfo.CurrentCulture.CompareInfo.Compare(strA, indexA, length1, strB, indexB, length2, CompareOptions.IgnoreCase);
      return CultureInfo.CurrentCulture.CompareInfo.Compare(strA, indexA, length1, strB, indexB, length2, CompareOptions.None);
    }

    /// <summary>
    ///   Сравнивает подстроки двух заданных объектов <see cref="T:System.String" /> (с учетом или без учета регистра), используя сведения о языке и региональных параметрах, и возвращает целое число, которое показывает их относительное положение в порядке сортировки.
    /// </summary>
    /// <param name="strA">Первая из сравниваемых строк.</param>
    /// <param name="indexA">
    ///   Позиция подстроки в <paramref name="strA" />.
    /// </param>
    /// <param name="strB">Вторая из сравниваемых строк.</param>
    /// <param name="indexB">
    ///   Позиция подстроки в <paramref name="strB" />.
    /// </param>
    /// <param name="length">
    ///   Максимальное число сравниваемых знаков в подстроках.
    /// </param>
    /// <param name="ignoreCase">
    ///   Значение <see langword="true" />, чтобы не учитывать регистр при сравнении; в противном случае — значение <see langword="false" />.
    /// </param>
    /// <param name="culture">
    ///   Объект, предоставляющий сведения об особенностях сравнения, определяемых языком и региональными параметрами.
    /// </param>
    /// <returns>
    /// Целое число, выражающее лексическое соотношение двух сравниваемых значений.
    /// 
    ///         Значение
    /// 
    ///         Условие
    /// 
    ///         Меньше нуля
    /// 
    ///         Подстрока в <paramref name="strA" /> предшествует подстроке в <paramref name="strB" /> в порядке сортировки.
    /// 
    ///         Нуль
    /// 
    ///         Подстроки появляются в той же позиции в порядке сортировки, или параметр <paramref name="length" /> равен нулю.
    /// 
    ///         Больше нуля
    /// 
    ///         Подстрока в <paramref name="strA" /> следует за подстрокой в <paramref name="strB" /> в порядке сортировки.
    ///       </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение <paramref name="indexA" /> больше <paramref name="strA" />.<see cref="P:System.String.Length" />
    /// 
    ///   -или-
    /// 
    ///   Значение <paramref name="indexB" /> больше значения <paramref name="strB" /><see cref="P:System.String.Length" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="indexA" />, <paramref name="indexB" /> или <paramref name="length" /> является отрицательным значением.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="strA" /> или <paramref name="strB" /> равно <see langword="null" />, а <paramref name="length" /> больше нуля.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="culture" /> имеет значение <see langword="null" />.
    /// </exception>
    public static int Compare(string strA, int indexA, string strB, int indexB, int length, bool ignoreCase, CultureInfo culture)
    {
      if (culture == null)
        throw new ArgumentNullException(nameof (culture));
      int length1 = length;
      int length2 = length;
      if (strA != null && strA.Length - indexA < length1)
        length1 = strA.Length - indexA;
      if (strB != null && strB.Length - indexB < length2)
        length2 = strB.Length - indexB;
      if (ignoreCase)
        return culture.CompareInfo.Compare(strA, indexA, length1, strB, indexB, length2, CompareOptions.IgnoreCase);
      return culture.CompareInfo.Compare(strA, indexA, length1, strB, indexB, length2, CompareOptions.None);
    }

    /// <summary>
    ///   Сравнивает подстроки двух заданных объектов <see cref="T:System.String" />, используя указанные параметры сравнения и сведения о языке и региональных параметрах, которые влияют на сравнение, и возвращает целое число, показывающее связь между двумя подстроками в порядке сортировки.
    /// </summary>
    /// <param name="strA">Первая из сравниваемых строк.</param>
    /// <param name="indexA">
    ///   Начальная позиция подстроки в пределах <paramref name="strA" />.
    /// </param>
    /// <param name="strB">Вторая из сравниваемых строк.</param>
    /// <param name="indexB">
    ///   Начальная позиция подстроки в пределах <paramref name="strB" />.
    /// </param>
    /// <param name="length">
    ///   Максимальное число сравниваемых знаков в подстроках.
    /// </param>
    /// <param name="culture">
    ///   Объект, предоставляющий сведения об особенностях сравнения, определяемых языком и региональными параметрами.
    /// </param>
    /// <param name="options">
    ///   Параметры, которые используются во время сравнения (например, игнорирование регистра или символов).
    /// </param>
    /// <returns>
    /// Целое число, которое указывает на лексические отношения между двумя подстроками, как показано в следующей таблице.
    /// 
    ///         Значение
    /// 
    ///         Условие
    /// 
    ///         Меньше нуля
    /// 
    ///         Подстрока в <paramref name="strA" /> предшествует подстроке в <paramref name="strB" /> в порядке сортировки.
    /// 
    ///         Нуль
    /// 
    ///         Подстроки появляются в той же позиции в порядке сортировки, или параметр <paramref name="length" /> равен нулю.
    /// 
    ///         Больше нуля
    /// 
    ///         Подстрока в <paramref name="strA" /> следует за подстрокой в <paramref name="strB" /> в порядке сортировки.
    ///       </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="options" /> не является значением <see cref="T:System.Globalization.CompareOptions" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение <paramref name="indexA" /> больше значения <paramref name="strA" /><see langword=".Length" />.
    /// 
    ///   -или-
    /// 
    ///   Значение <paramref name="indexB" /> больше значения <paramref name="strB" /><see langword=".Length" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="indexA" />, <paramref name="indexB" /> или <paramref name="length" /> является отрицательным значением.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="strA" /> или <paramref name="strB" /> равно <see langword="null" />, а <paramref name="length" /> больше нуля.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="culture" /> имеет значение <see langword="null" />.
    /// </exception>
    public static int Compare(string strA, int indexA, string strB, int indexB, int length, CultureInfo culture, CompareOptions options)
    {
      if (culture == null)
        throw new ArgumentNullException(nameof (culture));
      int length1 = length;
      int length2 = length;
      if (strA != null && strA.Length - indexA < length1)
        length1 = strA.Length - indexA;
      if (strB != null && strB.Length - indexB < length2)
        length2 = strB.Length - indexB;
      return culture.CompareInfo.Compare(strA, indexA, length1, strB, indexB, length2, options);
    }

    /// <summary>
    ///   Сравнивает подстроки двух указанных объектов <see cref="T:System.String" /> с использованием заданных правил и возвращает целое число, которое показывает их относительное положение в порядке сортировки.
    /// </summary>
    /// <param name="strA">Первая из сравниваемых строк.</param>
    /// <param name="indexA">
    ///   Позиция подстроки в <paramref name="strA" />.
    /// </param>
    /// <param name="strB">Вторая из сравниваемых строк.</param>
    /// <param name="indexB">
    ///   Позиция подстроки в <paramref name="strB" />.
    /// </param>
    /// <param name="length">
    ///   Максимальное число сравниваемых знаков в подстроках.
    /// </param>
    /// <param name="comparisonType">
    ///   Одно из значений перечисления, определяющее используемые при сравнении правила.
    /// </param>
    /// <returns>
    /// 32-битовое целое число со знаком, выражающее лексическое отношение двух сравниваемых значений.
    /// 
    ///         Значение
    /// 
    ///         Условие
    /// 
    ///         Меньше нуля
    /// 
    ///         Подстрока в <paramref name="strA" /> предшествует подстроке в <paramref name="strB" /> в порядке сортировки.
    /// 
    ///         Нуль
    /// 
    ///         Подстроки происходят в той же позиции в порядке сортировки, или <paramref name="length" /> параметра равно нулю.
    /// 
    ///         Больше нуля
    /// 
    ///         Подстрока в <paramref name="strA" /> follllows подстроки в <paramref name="strB" /> в порядке сортировки.
    ///       </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение <paramref name="indexA" /> больше значения <paramref name="strA" /><see cref="P:System.String.Length" />.
    /// 
    ///   -или-
    /// 
    ///   Значение <paramref name="indexB" /> больше значения <paramref name="strB" /><see cref="P:System.String.Length" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="indexA" />, <paramref name="indexB" /> или <paramref name="length" /> является отрицательным значением.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="indexA" /> или <paramref name="indexB" /> равен <see langword="null" />, а <paramref name="length" /> больше нуля.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="comparisonType" /> не является значением <see cref="T:System.StringComparison" />.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static int Compare(string strA, int indexA, string strB, int indexB, int length, StringComparison comparisonType)
    {
      switch (comparisonType)
      {
        case StringComparison.CurrentCulture:
        case StringComparison.CurrentCultureIgnoreCase:
        case StringComparison.InvariantCulture:
        case StringComparison.InvariantCultureIgnoreCase:
        case StringComparison.Ordinal:
        case StringComparison.OrdinalIgnoreCase:
          if (strA == null || strB == null)
          {
            if ((object) strA == (object) strB)
              return 0;
            return strA != null ? 1 : -1;
          }
          if (length < 0)
            throw new ArgumentOutOfRangeException(nameof (length), Environment.GetResourceString("ArgumentOutOfRange_NegativeLength"));
          if (indexA < 0)
            throw new ArgumentOutOfRangeException(nameof (indexA), Environment.GetResourceString("ArgumentOutOfRange_Index"));
          if (indexB < 0)
            throw new ArgumentOutOfRangeException(nameof (indexB), Environment.GetResourceString("ArgumentOutOfRange_Index"));
          if (strA.Length - indexA < 0)
            throw new ArgumentOutOfRangeException(nameof (indexA), Environment.GetResourceString("ArgumentOutOfRange_Index"));
          if (strB.Length - indexB < 0)
            throw new ArgumentOutOfRangeException(nameof (indexB), Environment.GetResourceString("ArgumentOutOfRange_Index"));
          if (length == 0 || strA == strB && indexA == indexB)
            return 0;
          int num1 = length;
          int num2 = length;
          if (strA != null && strA.Length - indexA < num1)
            num1 = strA.Length - indexA;
          if (strB != null && strB.Length - indexB < num2)
            num2 = strB.Length - indexB;
          switch (comparisonType)
          {
            case StringComparison.CurrentCulture:
              return CultureInfo.CurrentCulture.CompareInfo.Compare(strA, indexA, num1, strB, indexB, num2, CompareOptions.None);
            case StringComparison.CurrentCultureIgnoreCase:
              return CultureInfo.CurrentCulture.CompareInfo.Compare(strA, indexA, num1, strB, indexB, num2, CompareOptions.IgnoreCase);
            case StringComparison.InvariantCulture:
              return CultureInfo.InvariantCulture.CompareInfo.Compare(strA, indexA, num1, strB, indexB, num2, CompareOptions.None);
            case StringComparison.InvariantCultureIgnoreCase:
              return CultureInfo.InvariantCulture.CompareInfo.Compare(strA, indexA, num1, strB, indexB, num2, CompareOptions.IgnoreCase);
            case StringComparison.Ordinal:
              return string.nativeCompareOrdinalEx(strA, indexA, strB, indexB, length);
            case StringComparison.OrdinalIgnoreCase:
              return TextInfo.CompareOrdinalIgnoreCaseEx(strA, indexA, strB, indexB, num1, num2);
            default:
              throw new ArgumentException(Environment.GetResourceString("NotSupported_StringComparison"));
          }
        default:
          throw new ArgumentException(Environment.GetResourceString("NotSupported_StringComparison"), nameof (comparisonType));
      }
    }

    /// <summary>
    ///   Сравнивает данный экземпляр с указанным объектом <see cref="T:System.Object" /> и показывает, расположен ли экземпляр перед, после или в той же позиции в порядке сортировки, что и заданный объект <see cref="T:System.Object" />.
    /// </summary>
    /// <param name="value">
    ///   Объект, имеющий значение <see cref="T:System.String" />.
    /// </param>
    /// <returns>
    /// 32-битовое целое число со знаком, которое показывает, расположен ли данный экземпляр перед, после или на той же позиции в порядке сортировки, что и параметр <paramref name="value" />.
    /// 
    ///         Значение
    /// 
    ///         Условие
    /// 
    ///         Меньше нуля
    /// 
    ///         Данный экземпляр предшествует параметру <paramref name="value" />.
    /// 
    ///         Нуль
    /// 
    ///         Данный экземпляр имеет ту же позицию в порядке сортировки, что и <paramref name="value" />.
    /// 
    ///         Больше нуля
    /// 
    ///         Данный экземпляр стоит после параметра <paramref name="value" />.
    /// 
    ///         -или-
    /// 
    ///         Свойство <paramref name="value" /> имеет значение <see langword="null" />.
    ///       </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="value" /> не является объектом <see cref="T:System.String" />.
    /// </exception>
    public int CompareTo(object value)
    {
      if (value == null)
        return 1;
      if (!(value is string))
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeString"));
      return string.Compare(this, (string) value, StringComparison.CurrentCulture);
    }

    /// <summary>
    ///   Сравнивает данный экземпляр с заданным объектом <see cref="T:System.String" /> и показывает, расположен ли данный экземпляр перед, после или на той же позиции в порядке сортировки, что и заданная строка.
    /// </summary>
    /// <param name="strB">
    ///   Строка, сравниваемая с данным экземпляром.
    /// </param>
    /// <returns>
    /// 32-битовое целое число со знаком, которое показывает, расположен ли данный экземпляр перед, после или на той же позиции в порядке сортировки, что и параметр <paramref name="strB" />.
    /// 
    ///         Значение
    /// 
    ///         Условие
    /// 
    ///         Меньше нуля
    /// 
    ///         Данный экземпляр предшествует параметру <paramref name="strB" />.
    /// 
    ///         Нуль
    /// 
    ///         Данный экземпляр имеет ту же позицию в порядке сортировки, что и <paramref name="strB" />.
    /// 
    ///         Больше нуля
    /// 
    ///         Данный экземпляр стоит после параметра <paramref name="strB" />.
    /// 
    ///         -или-
    /// 
    ///         Свойство <paramref name="strB" /> имеет значение <see langword="null" />.
    ///       </returns>
    [__DynamicallyInvokable]
    public int CompareTo(string strB)
    {
      if (strB == null)
        return 1;
      return CultureInfo.CurrentCulture.CompareInfo.Compare(this, strB, CompareOptions.None);
    }

    /// <summary>
    ///   Сравнивает два указанных объекта <see cref="T:System.String" />, оценивая числовые значения соответствующих объектов <see cref="T:System.Char" /> в каждой строке.
    /// </summary>
    /// <param name="strA">Первая сравниваемая строка.</param>
    /// <param name="strB">Вторая сравниваемая строка.</param>
    /// <returns>
    /// Целое число, выражающее лексическое соотношение двух сравниваемых значений.
    /// 
    ///         Значение
    /// 
    ///         Условие
    /// 
    ///         Меньше нуля
    /// 
    ///         Значение <paramref name="strA" /> меньше <paramref name="strB" />.
    /// 
    ///         Нуль
    /// 
    ///         Значения параметров <paramref name="strA" /> и <paramref name="strB" /> равны.
    /// 
    ///         Больше нуля
    /// 
    ///         Значение <paramref name="strA" /> больше значения <paramref name="strB" />.
    ///       </returns>
    [__DynamicallyInvokable]
    public static int CompareOrdinal(string strA, string strB)
    {
      if ((object) strA == (object) strB)
        return 0;
      if (strA == null)
        return -1;
      if (strB == null)
        return 1;
      if ((int) strA.m_firstChar - (int) strB.m_firstChar != 0)
        return (int) strA.m_firstChar - (int) strB.m_firstChar;
      return string.CompareOrdinalHelper(strA, strB);
    }

    /// <summary>
    ///   Сравнивает подстроки двух указанных объектов <see cref="T:System.String" />, вычисляя числовые значения соответствующих объектов <see cref="T:System.Char" /> в каждой подстроке.
    /// </summary>
    /// <param name="strA">Первая из сравниваемых строк.</param>
    /// <param name="indexA">
    ///   Начальный индекс подстроки в <paramref name="strA" />.
    /// </param>
    /// <param name="strB">Вторая из сравниваемых строк.</param>
    /// <param name="indexB">
    ///   Начальный индекс подстроки в <paramref name="strB" />.
    /// </param>
    /// <param name="length">
    ///   Максимальное число сравниваемых знаков в подстроках.
    /// </param>
    /// <returns>
    /// 32-битовое целое число со знаком, выражающее лексическое отношение двух сравниваемых значений.
    /// 
    ///         Значение
    /// 
    ///         Условие
    /// 
    ///         Меньше нуля
    /// 
    ///         Подстрока в <paramref name="strA" /> меньше, чем подстрока в <paramref name="strB" />.
    /// 
    ///         Нуль
    /// 
    ///         Подстроки равны, или значение параметра <paramref name="length" /> равно нулю.
    /// 
    ///         Больше нуля
    /// 
    ///         Подстрока в <paramref name="strA" /> больше, чем подстрока в <paramref name="strB" />.
    ///       </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="strA" /> не имеет значение <see langword="null" />, и значение <paramref name="indexA" /> больше <paramref name="strA" />,<see cref="P:System.String.Length" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="strB" /> не имеет значение <see langword="null" />, и значение <paramref name="indexB" /> больше <paramref name="strB" />,<see cref="P:System.String.Length" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="indexA" />, <paramref name="indexB" /> или <paramref name="length" /> является отрицательным значением.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static int CompareOrdinal(string strA, int indexA, string strB, int indexB, int length)
    {
      if (strA != null && strB != null)
        return string.nativeCompareOrdinalEx(strA, indexA, strB, indexB, length);
      if ((object) strA == (object) strB)
        return 0;
      return strA != null ? 1 : -1;
    }

    /// <summary>
    ///   Возвращает значение, указывающее, встречается ли указанная подстрока внутри этой строки.
    /// </summary>
    /// <param name="value">Строка для поиска.</param>
    /// <returns>
    ///   <see langword="true" />, если параметр <paramref name="value" /> встречается в строке или <paramref name="value" /> является пустой строкой (""); в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public bool Contains(string value)
    {
      return this.IndexOf(value, StringComparison.Ordinal) >= 0;
    }

    /// <summary>
    ///   Определяет, совпадает ли конец данного экземпляра строки с указанной строкой.
    /// </summary>
    /// <param name="value">
    ///   Строка, которую необходимо сравнить с подстрокой, расположенной в конце этого экземпляра.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если конец этого экземпляра совпадает с <paramref name="value" />; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public bool EndsWith(string value)
    {
      return this.EndsWith(value, StringComparison.CurrentCulture);
    }

    /// <summary>
    ///   Определяет, совпадает ли конец экземпляра строки с заданной строкой при сравнении с учетом заданного параметра сравнения.
    /// </summary>
    /// <param name="value">
    ///   Строка, которую необходимо сравнить с подстрокой, расположенной в конце этого экземпляра.
    /// </param>
    /// <param name="comparisonType">
    ///   Одно из значений перечисления, определяющее способ сравнения данной строки со значением <paramref name="value" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если параметр <paramref name="value" /> соответствует концу данной строки; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="comparisonType" /> не является значением <see cref="T:System.StringComparison" />.
    /// </exception>
    [SecuritySafeCritical]
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public bool EndsWith(string value, StringComparison comparisonType)
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      switch (comparisonType)
      {
        case StringComparison.CurrentCulture:
        case StringComparison.CurrentCultureIgnoreCase:
        case StringComparison.InvariantCulture:
        case StringComparison.InvariantCultureIgnoreCase:
        case StringComparison.Ordinal:
        case StringComparison.OrdinalIgnoreCase:
          if ((object) this == (object) value || value.Length == 0)
            return true;
          switch (comparisonType)
          {
            case StringComparison.CurrentCulture:
              return CultureInfo.CurrentCulture.CompareInfo.IsSuffix(this, value, CompareOptions.None);
            case StringComparison.CurrentCultureIgnoreCase:
              return CultureInfo.CurrentCulture.CompareInfo.IsSuffix(this, value, CompareOptions.IgnoreCase);
            case StringComparison.InvariantCulture:
              return CultureInfo.InvariantCulture.CompareInfo.IsSuffix(this, value, CompareOptions.None);
            case StringComparison.InvariantCultureIgnoreCase:
              return CultureInfo.InvariantCulture.CompareInfo.IsSuffix(this, value, CompareOptions.IgnoreCase);
            case StringComparison.Ordinal:
              if (this.Length >= value.Length)
                return string.nativeCompareOrdinalEx(this, this.Length - value.Length, value, 0, value.Length) == 0;
              return false;
            case StringComparison.OrdinalIgnoreCase:
              if (this.Length >= value.Length)
                return TextInfo.CompareOrdinalIgnoreCaseEx(this, this.Length - value.Length, value, 0, value.Length, value.Length) == 0;
              return false;
            default:
              throw new ArgumentException(Environment.GetResourceString("NotSupported_StringComparison"), nameof (comparisonType));
          }
        default:
          throw new ArgumentException(Environment.GetResourceString("NotSupported_StringComparison"), nameof (comparisonType));
      }
    }

    /// <summary>
    ///   Определяет, совпадает ли конец данного экземпляра строки с заданной строкой при сравнении с учетом заданного языка и региональных параметров.
    /// </summary>
    /// <param name="value">
    ///   Строка, которую необходимо сравнить с подстрокой, расположенной в конце этого экземпляра.
    /// </param>
    /// <param name="ignoreCase">
    ///   Значение <see langword="true" />, чтобы не учитывать регистр при сравнении; в противном случае — значение <see langword="false" />.
    /// </param>
    /// <param name="culture">
    ///   Связанные с языком и региональными параметрами сведения, определяющие, как выполняется сравнение этого экземпляра и <paramref name="value" />.
    ///    Если значением параметра <paramref name="culture" /> является <see langword="null" />, используется текущий язык и региональные параметры.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если параметр <paramref name="value" /> соответствует концу данной строки; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    public bool EndsWith(string value, bool ignoreCase, CultureInfo culture)
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      if ((object) this == (object) value)
        return true;
      return (culture != null ? culture : CultureInfo.CurrentCulture).CompareInfo.IsSuffix(this, value, ignoreCase ? CompareOptions.IgnoreCase : CompareOptions.None);
    }

    internal bool EndsWith(char value)
    {
      int length = this.Length;
      return length != 0 && (int) this[length - 1] == (int) value;
    }

    /// <summary>
    ///   Возвращает индекс с отсчетом от нуля первого вхождения указанного символа Юникода в данной строке.
    /// </summary>
    /// <param name="value">Искомый знак Юникода.</param>
    /// <returns>
    ///   Отсчитываемое от нуля значение индекса параметра <paramref name="value" />, если этот знак найден; в противном случае — значение -1.
    /// </returns>
    [__DynamicallyInvokable]
    public int IndexOf(char value)
    {
      return this.IndexOf(value, 0, this.Length);
    }

    /// <summary>
    ///   Возвращает индекс с отсчетом от нуля первого вхождения указанного символа Юникода в данной строке.
    ///    Поиск начинается с указанной позиции знака.
    /// </summary>
    /// <param name="value">Искомый знак Юникода.</param>
    /// <param name="startIndex">
    ///   Позиция, с которой начинается поиск.
    /// </param>
    /// <returns>
    ///   Отсчитываемая от нуля позиция индекса параметра <paramref name="value" /> с начала строки, если символ найден. Значение –1, если символ не найден.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="startIndex" /> имеет значение меньше нуля или больше длины строки.
    /// </exception>
    [__DynamicallyInvokable]
    public int IndexOf(char value, int startIndex)
    {
      return this.IndexOf(value, startIndex, this.Length - startIndex);
    }

    /// <summary>
    ///   Возвращает индекс с отсчетом от нуля первого вхождения указанного символа в данном экземпляре.
    ///    Поиск начинается с указанной позиции знака; проверяется заданное количество позиций.
    /// </summary>
    /// <param name="value">Искомый знак Юникода.</param>
    /// <param name="startIndex">
    ///   Позиция, с которой начинается поиск.
    /// </param>
    /// <param name="count">
    ///   Количество позиций знаков для проверки.
    /// </param>
    /// <returns>
    ///   Отсчитываемая от нуля позиция индекса параметра <paramref name="value" /> с начала строки, если символ найден. Значение –1, если символ не найден.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="count" /> или <paramref name="startIndex" /> является отрицательным значением.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="startIndex" /> больше длины этой строки.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="count" /> больше, чем длина этой строки минус <paramref name="startIndex" />.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public extern int IndexOf(char value, int startIndex, int count);

    /// <summary>
    ///   Возвращает индекс с отсчетом от нуля первого обнаруженного в данном экземпляре символа из указанного массива символов Юникода.
    /// </summary>
    /// <param name="anyOf">
    ///   Массив знаков Юникода, содержащий один или несколько искомых знаков.
    /// </param>
    /// <returns>
    ///   Начинающееся с нуля значение индекса первого вхождения какого-либо знака из <paramref name="anyOf" /> в данном экземпляре; -1, если не найден ни один знак из <paramref name="anyOf" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="anyOf" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public int IndexOfAny(char[] anyOf)
    {
      return this.IndexOfAny(anyOf, 0, this.Length);
    }

    /// <summary>
    ///   Возвращает индекс с отсчетом от нуля первого обнаруженного в данном экземпляре символа из указанного массива символов Юникода.
    ///    Поиск начинается с указанной позиции знака.
    /// </summary>
    /// <param name="anyOf">
    ///   Массив знаков Юникода, содержащий один или несколько искомых знаков.
    /// </param>
    /// <param name="startIndex">
    ///   Позиция, с которой начинается поиск.
    /// </param>
    /// <returns>
    ///   Начинающееся с нуля значение индекса первого вхождения какого-либо знака из <paramref name="anyOf" /> в данном экземпляре; -1, если не найден ни один знак из <paramref name="anyOf" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="anyOf" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="startIndex" /> является отрицательным значением.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="startIndex" /> больше, чем количество символов в данном экземпляре.
    /// </exception>
    [__DynamicallyInvokable]
    public int IndexOfAny(char[] anyOf, int startIndex)
    {
      return this.IndexOfAny(anyOf, startIndex, this.Length - startIndex);
    }

    /// <summary>
    ///   Возвращает индекс с отсчетом от нуля первого обнаруженного в данном экземпляре символа из указанного массива символов Юникода.
    ///    Поиск начинается с указанной позиции знака; проверяется заданное количество позиций.
    /// </summary>
    /// <param name="anyOf">
    ///   Массив знаков Юникода, содержащий один или несколько искомых знаков.
    /// </param>
    /// <param name="startIndex">
    ///   Позиция, с которой начинается поиск.
    /// </param>
    /// <param name="count">
    ///   Количество позиций знаков для проверки.
    /// </param>
    /// <returns>
    ///   Начинающееся с нуля значение индекса первого вхождения какого-либо знака из <paramref name="anyOf" /> в данном экземпляре; -1, если не найден ни один знак из <paramref name="anyOf" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="anyOf" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="count" /> или <paramref name="startIndex" /> является отрицательным значением.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="count" /> + <paramref name="startIndex" /> больше, чем количество символов в данном экземпляре.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public extern int IndexOfAny(char[] anyOf, int startIndex, int count);

    /// <summary>
    ///   Возвращает индекс с отсчетом от нуля первого вхождения значения указанной строки в данном экземпляре.
    /// </summary>
    /// <param name="value">Строка для поиска.</param>
    /// <returns>
    ///   Положение в индексе (начиная с нуля) параметра <paramref name="value" />, если эта строка найдена, или значение -1, если она не найдена.
    ///    Если значение <paramref name="value" /> равно <see cref="F:System.String.Empty" />, то возвращаемое значение равно 0.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public int IndexOf(string value)
    {
      return this.IndexOf(value, StringComparison.CurrentCulture);
    }

    /// <summary>
    ///   Возвращает индекс с отсчетом от нуля первого вхождения значения указанной строки в данном экземпляре.
    ///    Поиск начинается с указанной позиции знака.
    /// </summary>
    /// <param name="value">Строка для поиска.</param>
    /// <param name="startIndex">
    ///   Позиция, с которой начинается поиск.
    /// </param>
    /// <returns>
    ///   Отсчитываемая от нуля позиция индекса параметра <paramref name="value" /> с начала текущего экземпляра, если строка найдена. Значение –1, если строка не найдена.
    ///    Если значение параметра <paramref name="value" /> равно <see cref="F:System.String.Empty" />, возвращаемым значением является <paramref name="startIndex" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="startIndex" /> имеет значение меньше нуля или больше длины этой строки.
    /// </exception>
    [__DynamicallyInvokable]
    public int IndexOf(string value, int startIndex)
    {
      return this.IndexOf(value, startIndex, StringComparison.CurrentCulture);
    }

    /// <summary>
    ///   Возвращает индекс с отсчетом от нуля первого вхождения значения указанной строки в данном экземпляре.
    ///    Поиск начинается с указанной позиции знака; проверяется заданное количество позиций.
    /// </summary>
    /// <param name="value">Строка для поиска.</param>
    /// <param name="startIndex">
    ///   Позиция, с которой начинается поиск.
    /// </param>
    /// <param name="count">
    ///   Количество позиций знаков для проверки.
    /// </param>
    /// <returns>
    ///   Отсчитываемая от нуля позиция индекса параметра <paramref name="value" /> с начала текущего экземпляра, если строка найдена. Значение –1, если строка не найдена.
    ///    Если значение параметра <paramref name="value" /> равно <see cref="F:System.String.Empty" />, возвращаемым значением является <paramref name="startIndex" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="count" /> или <paramref name="startIndex" /> является отрицательным значением.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="startIndex" /> больше длины этой строки.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="count" /> больше, чем длина этой строки минус <paramref name="startIndex" />.
    /// </exception>
    [__DynamicallyInvokable]
    public int IndexOf(string value, int startIndex, int count)
    {
      if (startIndex < 0 || startIndex > this.Length)
        throw new ArgumentOutOfRangeException(nameof (startIndex), Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (count < 0 || count > this.Length - startIndex)
        throw new ArgumentOutOfRangeException(nameof (count), Environment.GetResourceString("ArgumentOutOfRange_Count"));
      return this.IndexOf(value, startIndex, count, StringComparison.CurrentCulture);
    }

    /// <summary>
    ///   Возвращает индекс с отсчетом от нуля первого вхождения указанной строки в текущем объекте <see cref="T:System.String" />.
    ///    Параметр определяет тип поиска заданной строки.
    /// </summary>
    /// <param name="value">Строка для поиска.</param>
    /// <param name="comparisonType">
    ///   Одно из значений перечисления, определяющее правила поиска.
    /// </param>
    /// <returns>
    ///   Индекс позиции строки, заданной в параметре <paramref name="value" />, если она найдена, и -1, если нет.
    ///    Если значение <paramref name="value" /> равно <see cref="F:System.String.Empty" />, то возвращаемое значение равно 0.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="comparisonType" /> не является допустимым значением <see cref="T:System.StringComparison" />.
    /// </exception>
    [__DynamicallyInvokable]
    public int IndexOf(string value, StringComparison comparisonType)
    {
      return this.IndexOf(value, 0, this.Length, comparisonType);
    }

    /// <summary>
    ///   Возвращает индекс с отсчетом от нуля первого вхождения указанной строки в текущем объекте <see cref="T:System.String" />.
    ///    Параметры задают начальную позицию поиска в текущей строке и тип поиска.
    /// </summary>
    /// <param name="value">Строка для поиска.</param>
    /// <param name="startIndex">
    ///   Позиция, с которой начинается поиск.
    /// </param>
    /// <param name="comparisonType">
    ///   Одно из значений перечисления, определяющее правила поиска.
    /// </param>
    /// <returns>
    ///   Отсчитываемая от нуля позиция параметра <paramref name="value" /> с начала текущего экземпляра, если строка найдена, или -1, если нет.
    ///    Если значение параметра <paramref name="value" /> равно <see cref="F:System.String.Empty" />, возвращаемым значением является <paramref name="startIndex" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="startIndex" /> имеет значение меньше нуля или больше длины этой строки.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="comparisonType" /> не является допустимым значением <see cref="T:System.StringComparison" />.
    /// </exception>
    [__DynamicallyInvokable]
    public int IndexOf(string value, int startIndex, StringComparison comparisonType)
    {
      return this.IndexOf(value, startIndex, this.Length - startIndex, comparisonType);
    }

    /// <summary>
    ///   Возвращает индекс с отсчетом от нуля первого вхождения указанной строки в текущем объекте <see cref="T:System.String" />.
    ///    Параметры задают начальную позицию поиска в текущей строке, количество проверяемых знаков текущей строки и тип поиска.
    /// </summary>
    /// <param name="value">Строка для поиска.</param>
    /// <param name="startIndex">
    ///   Позиция, с которой начинается поиск.
    /// </param>
    /// <param name="count">
    ///   Количество позиций знаков для проверки.
    /// </param>
    /// <param name="comparisonType">
    ///   Одно из значений перечисления, определяющее правила поиска.
    /// </param>
    /// <returns>
    ///   Отсчитываемая от нуля позиция параметра <paramref name="value" /> с начала текущего экземпляра, если строка найдена, или -1, если нет.
    ///    Если значение параметра <paramref name="value" /> равно <see cref="F:System.String.Empty" />, возвращаемым значением является <paramref name="startIndex" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="count" /> или <paramref name="startIndex" /> является отрицательным значением.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="startIndex" /> больше длины этого экземпляра.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="count" /> больше, чем длина этой строки минус <paramref name="startIndex" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="comparisonType" /> не является допустимым значением <see cref="T:System.StringComparison" />.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public int IndexOf(string value, int startIndex, int count, StringComparison comparisonType)
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      if (startIndex < 0 || startIndex > this.Length)
        throw new ArgumentOutOfRangeException(nameof (startIndex), Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (count < 0 || startIndex > this.Length - count)
        throw new ArgumentOutOfRangeException(nameof (count), Environment.GetResourceString("ArgumentOutOfRange_Count"));
      switch (comparisonType)
      {
        case StringComparison.CurrentCulture:
          return CultureInfo.CurrentCulture.CompareInfo.IndexOf(this, value, startIndex, count, CompareOptions.None);
        case StringComparison.CurrentCultureIgnoreCase:
          return CultureInfo.CurrentCulture.CompareInfo.IndexOf(this, value, startIndex, count, CompareOptions.IgnoreCase);
        case StringComparison.InvariantCulture:
          return CultureInfo.InvariantCulture.CompareInfo.IndexOf(this, value, startIndex, count, CompareOptions.None);
        case StringComparison.InvariantCultureIgnoreCase:
          return CultureInfo.InvariantCulture.CompareInfo.IndexOf(this, value, startIndex, count, CompareOptions.IgnoreCase);
        case StringComparison.Ordinal:
          return CultureInfo.InvariantCulture.CompareInfo.IndexOf(this, value, startIndex, count, CompareOptions.Ordinal);
        case StringComparison.OrdinalIgnoreCase:
          if (value.IsAscii() && this.IsAscii())
            return CultureInfo.InvariantCulture.CompareInfo.IndexOf(this, value, startIndex, count, CompareOptions.IgnoreCase);
          return TextInfo.IndexOfStringOrdinalIgnoreCase(this, value, startIndex, count);
        default:
          throw new ArgumentException(Environment.GetResourceString("NotSupported_StringComparison"), nameof (comparisonType));
      }
    }

    /// <summary>
    ///   Возвращает позицию индекса с отсчетом от нуля последнего вхождения указанного символа Юникода в пределах данного экземпляра.
    /// </summary>
    /// <param name="value">
    ///   Знак Юникода, который необходимо найти.
    /// </param>
    /// <returns>
    ///   Отсчитываемое от нуля значение индекса параметра <paramref name="value" />, если этот знак найден; в противном случае — значение -1.
    /// </returns>
    [__DynamicallyInvokable]
    public int LastIndexOf(char value)
    {
      return this.LastIndexOf(value, this.Length - 1, this.Length);
    }

    /// <summary>
    ///   Возвращает позицию индекса с отсчетом от нуля последнего вхождения указанного символа Юникода в пределах данного экземпляра.
    ///    Поиск начинается с указанной позиции символа и выполняется в обратном направлении до начала строки.
    /// </summary>
    /// <param name="value">
    ///   Знак Юникода, который необходимо найти.
    /// </param>
    /// <param name="startIndex">
    ///   Начальное положение поиска.
    ///    Поиск выполняется от индекса, заданного параметром <paramref name="startIndex" />, до начала данного экземпляра.
    /// </param>
    /// <returns>
    ///   Отсчитываемая от нуля позиция параметра <paramref name="value" /> в индексе, если этот символ найден, или значение -1, если он не найден или текущий экземпляр равен <see cref="F:System.String.Empty" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Текущий экземпляр не равен <see cref="F:System.String.Empty" />, а <paramref name="startIndex" /> меньше нуля либо равно или превышает длину этого экземпляра.
    /// </exception>
    [__DynamicallyInvokable]
    public int LastIndexOf(char value, int startIndex)
    {
      return this.LastIndexOf(value, startIndex, startIndex + 1);
    }

    /// <summary>
    ///   Возвращает позицию индекса с отсчетом от нуля последнего вхождения указанного символа Юникода в подстроке в пределах данного экземпляра.
    ///    Поиск начинается с указанной позиции символа и выполняется в обратном направлении до начала строки для заданного числа позиций символов.
    /// </summary>
    /// <param name="value">
    ///   Знак Юникода, который необходимо найти.
    /// </param>
    /// <param name="startIndex">
    ///   Начальное положение поиска.
    ///    Поиск выполняется от индекса, заданного параметром <paramref name="startIndex" />, до начала данного экземпляра.
    /// </param>
    /// <param name="count">
    ///   Количество позиций знаков для проверки.
    /// </param>
    /// <returns>
    ///   Отсчитываемая от нуля позиция параметра <paramref name="value" /> в индексе, если этот символ найден, или значение -1, если он не найден или текущий экземпляр равен <see cref="F:System.String.Empty" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Текущий экземпляр не равен <see cref="F:System.String.Empty" />, а <paramref name="startIndex" /> меньше нуля либо равно или превышает длину этого экземпляра.
    /// 
    ///   -или-
    /// 
    ///   Текущий экземпляр не равен <see cref="F:System.String.Empty" />, а <paramref name="startIndex" /> – <paramref name="count" /> + 1 является отрицательным.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public extern int LastIndexOf(char value, int startIndex, int count);

    /// <summary>
    ///   Возвращает позицию индекса с отсчетом от нуля последнего вхождения в данном экземпляре какого-либо одного или нескольких символов, указанных в массиве символов Юникода.
    /// </summary>
    /// <param name="anyOf">
    ///   Массив знаков Юникода, содержащий один или несколько искомых знаков.
    /// </param>
    /// <returns>
    ///   Значение индекса последнего вхождения какого-либо знака из <paramref name="anyOf" /> в данном экземпляре; -1, если ни один знак из <paramref name="anyOf" /> не был найден.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="anyOf" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public int LastIndexOfAny(char[] anyOf)
    {
      return this.LastIndexOfAny(anyOf, this.Length - 1, this.Length);
    }

    /// <summary>
    ///   Возвращает позицию индекса с отсчетом от нуля последнего вхождения в данном экземпляре какого-либо одного или нескольких символов, указанных в массиве символов Юникода.
    ///    Поиск начинается с указанной позиции символа и выполняется в обратном направлении до начала строки.
    /// </summary>
    /// <param name="anyOf">
    ///   Массив знаков Юникода, содержащий один или несколько искомых знаков.
    /// </param>
    /// <param name="startIndex">
    ///   Позиция, с которой начинается поиск.
    ///    Поиск выполняется от индекса, заданного параметром <paramref name="startIndex" />, до начала данного экземпляра.
    /// </param>
    /// <returns>
    ///   Значение индекса последнего вхождения какого-либо знака из <paramref name="anyOf" /> в данном экземпляре; -1, если ни один знак из <paramref name="anyOf" /> не был найден или текущий экземпляр равен <see cref="F:System.String.Empty" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="anyOf" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Текущий экземпляр не равен <see cref="F:System.String.Empty" />, а <paramref name="startIndex" /> указывает позицию вне пределов данного экземпляра.
    /// </exception>
    [__DynamicallyInvokable]
    public int LastIndexOfAny(char[] anyOf, int startIndex)
    {
      return this.LastIndexOfAny(anyOf, startIndex, startIndex + 1);
    }

    /// <summary>
    ///   Возвращает позицию индекса с отсчетом от нуля последнего вхождения в данном экземпляре какого-либо одного или нескольких символов, указанных в массиве символов Юникода.
    ///    Поиск начинается с указанной позиции символа и выполняется в обратном направлении до начала строки для заданного числа позиций символов.
    /// </summary>
    /// <param name="anyOf">
    ///   Массив знаков Юникода, содержащий один или несколько искомых знаков.
    /// </param>
    /// <param name="startIndex">
    ///   Позиция, с которой начинается поиск.
    ///    Поиск выполняется от индекса, заданного параметром <paramref name="startIndex" />, до начала данного экземпляра.
    /// </param>
    /// <param name="count">
    ///   Количество позиций знаков для проверки.
    /// </param>
    /// <returns>
    ///   Значение индекса последнего вхождения какого-либо знака из <paramref name="anyOf" /> в данном экземпляре; -1, если ни один знак из <paramref name="anyOf" /> не был найден или текущий экземпляр равен <see cref="F:System.String.Empty" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="anyOf" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Текущий экземпляр не равен <see cref="F:System.String.Empty" />, а <paramref name="count" /> или <paramref name="startIndex" /> является отрицательным числом.
    /// 
    ///   -или-
    /// 
    ///   Текущий экземпляр не равен <see cref="F:System.String.Empty" />, а разность <paramref name="startIndex" /> и <paramref name="count" /> + 1 меньше нуля.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public extern int LastIndexOfAny(char[] anyOf, int startIndex, int count);

    /// <summary>
    ///   Возвращает позицию индекса с отсчетом от нуля последнего вхождения указанной строки в данном экземпляре.
    /// </summary>
    /// <param name="value">Строка для поиска.</param>
    /// <returns>
    ///   Положение в отсчитываемом от нуля индексе параметра <paramref name="value" />, если эта строка найдена, или значение -1, если она не найдена.
    ///    Если параметр <paramref name="value" /> равен <see cref="F:System.String.Empty" />, возвращаемым значением является последняя позиция в индексе данного экземпляра.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public int LastIndexOf(string value)
    {
      return this.LastIndexOf(value, this.Length - 1, this.Length, StringComparison.CurrentCulture);
    }

    /// <summary>
    ///   Возвращает позицию индекса с отсчетом от нуля последнего вхождения указанной строки в данном экземпляре.
    ///    Поиск начинается с указанной позиции символа и выполняется в обратном направлении до начала строки.
    /// </summary>
    /// <param name="value">Строка для поиска.</param>
    /// <param name="startIndex">
    ///   Позиция, с которой начинается поиск.
    ///    Поиск выполняется от индекса, заданного параметром <paramref name="startIndex" />, до начала данного экземпляра.
    /// </param>
    /// <returns>
    ///   Отсчитываемая от нуля начальная позиция индекса параметра <paramref name="value" />, если строка найдена; значение -1, если строка не найдена или значение текущего экземпляра равно <see cref="F:System.String.Empty" />.
    ///    Если значение параметра <paramref name="value" /> равно <see cref="F:System.String.Empty" />, возвращаемым значением является наименьшее значение из <paramref name="startIndex" /> и последнего значения индекса в данном экземпляре.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Текущий экземпляр не равен <see cref="F:System.String.Empty" />, а <paramref name="startIndex" /> меньше нуля или больше длины текущего экземпляра.
    /// 
    ///   -или-
    /// 
    ///   Текущий экземпляр равен <see cref="F:System.String.Empty" />, а <paramref name="startIndex" /> меньше –1 или больше нуля.
    /// </exception>
    [__DynamicallyInvokable]
    public int LastIndexOf(string value, int startIndex)
    {
      return this.LastIndexOf(value, startIndex, startIndex + 1, StringComparison.CurrentCulture);
    }

    /// <summary>
    ///   Возвращает позицию индекса с отсчетом от нуля последнего вхождения указанной строки в данном экземпляре.
    ///    Поиск начинается с указанной позиции символа и выполняется в обратном направлении до начала строки для заданного числа позиций символов.
    /// </summary>
    /// <param name="value">Строка для поиска.</param>
    /// <param name="startIndex">
    ///   Позиция, с которой начинается поиск.
    ///    Поиск выполняется от индекса, заданного параметром <paramref name="startIndex" />, до начала данного экземпляра.
    /// </param>
    /// <param name="count">
    ///   Количество позиций знаков для проверки.
    /// </param>
    /// <returns>
    ///   Отсчитываемая от нуля начальная позиция индекса параметра <paramref name="value" />, если строка найдена; значение -1, если строка не найдена или значение текущего экземпляра равно <see cref="F:System.String.Empty" />.
    ///    Если значение параметра <paramref name="value" /> равно <see cref="F:System.String.Empty" />, возвращаемым значением является наименьшее значение из <paramref name="startIndex" /> и последнего значения индекса в данном экземпляре.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="count" /> является отрицательным значением.
    /// 
    ///   -или-
    /// 
    ///   Текущий экземпляр не равен <see cref="F:System.String.Empty" />, а <paramref name="startIndex" /> является отрицательным.
    /// 
    ///   -или-
    /// 
    ///   Текущий экземпляр не равен <see cref="F:System.String.Empty" />, а <paramref name="startIndex" /> превышает длину этого экземпляра.
    /// 
    ///   -или-
    /// 
    ///   Текущий экземпляр не равен <see cref="F:System.String.Empty" />, а <paramref name="startIndex" /> – <paramref name="count" /> + 1 указывает позицию вне пределов данного экземпляра.
    /// 
    ///   -или-
    /// 
    ///   Текущий экземпляр равен <see cref="F:System.String.Empty" />, а <paramref name="start" /> меньше -1 или больше нуля.
    /// 
    ///   -или-
    /// 
    ///   Текущий экземпляр равен <see cref="F:System.String.Empty" />, а <paramref name="count" /> больше 1.
    /// </exception>
    [__DynamicallyInvokable]
    public int LastIndexOf(string value, int startIndex, int count)
    {
      if (count < 0)
        throw new ArgumentOutOfRangeException(nameof (count), Environment.GetResourceString("ArgumentOutOfRange_Count"));
      return this.LastIndexOf(value, startIndex, count, StringComparison.CurrentCulture);
    }

    /// <summary>
    ///   Возвращает индекс с отсчетом от нуля последнего вхождения указанной строки в текущем объекте <see cref="T:System.String" />.
    ///    Параметр определяет тип поиска заданной строки.
    /// </summary>
    /// <param name="value">Строка для поиска.</param>
    /// <param name="comparisonType">
    ///   Одно из значений перечисления, определяющее правила поиска.
    /// </param>
    /// <returns>
    ///   Начальное положение в отсчитываемом от нуля индексе параметра <paramref name="value" />, если эта строка найдена, или значение -1, если она не найдена.
    ///    Если параметр <paramref name="value" /> равен <see cref="F:System.String.Empty" />, возвращаемым значением является последняя позиция в индексе данного экземпляра.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="comparisonType" /> не является допустимым значением <see cref="T:System.StringComparison" />.
    /// </exception>
    [__DynamicallyInvokable]
    public int LastIndexOf(string value, StringComparison comparisonType)
    {
      return this.LastIndexOf(value, this.Length - 1, this.Length, comparisonType);
    }

    /// <summary>
    ///   Возвращает индекс с отсчетом от нуля последнего вхождения указанной строки в текущем объекте <see cref="T:System.String" />.
    ///    Поиск начинается с указанной позиции символа и выполняется в обратном направлении до начала строки.
    ///    Параметр определяет тип сравнения для выполнения во время поиска заданной строки.
    /// </summary>
    /// <param name="value">Строка для поиска.</param>
    /// <param name="startIndex">
    ///   Позиция, с которой начинается поиск.
    ///    Поиск выполняется от индекса, заданного параметром <paramref name="startIndex" />, до начала данного экземпляра.
    /// </param>
    /// <param name="comparisonType">
    ///   Одно из значений перечисления, определяющее правила поиска.
    /// </param>
    /// <returns>
    ///   Отсчитываемая от нуля начальная позиция индекса параметра <paramref name="value" />, если эта строка найдена, или значение -1, если строка не найдена или текущий экземпляр равен <see cref="F:System.String.Empty" />.
    ///    Если значение параметра <paramref name="value" /> равно <see cref="F:System.String.Empty" />, возвращаемым значением является наименьшее значение из <paramref name="startIndex" /> и последнего значения индекса в данном экземпляре.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Текущий экземпляр не равен <see cref="F:System.String.Empty" />, а <paramref name="startIndex" /> меньше нуля или больше длины текущего экземпляра.
    /// 
    ///   -или-
    /// 
    ///   Текущий экземпляр равен <see cref="F:System.String.Empty" />, а <paramref name="startIndex" /> меньше -1 или больше нуля.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="comparisonType" /> не является допустимым значением <see cref="T:System.StringComparison" />.
    /// </exception>
    [__DynamicallyInvokable]
    public int LastIndexOf(string value, int startIndex, StringComparison comparisonType)
    {
      return this.LastIndexOf(value, startIndex, startIndex + 1, comparisonType);
    }

    /// <summary>
    ///   Возвращает позицию индекса с отсчетом от нуля последнего вхождения указанной строки в данном экземпляре.
    ///    Поиск начинается с указанной позиции символа и выполняется в обратном направлении до начала строки для заданного числа позиций символов.
    ///    Параметр определяет тип сравнения для выполнения во время поиска заданной строки.
    /// </summary>
    /// <param name="value">Строка для поиска.</param>
    /// <param name="startIndex">
    ///   Позиция, с которой начинается поиск.
    ///    Поиск выполняется от индекса, заданного параметром <paramref name="startIndex" />, до начала данного экземпляра.
    /// </param>
    /// <param name="count">
    ///   Количество позиций знаков для проверки.
    /// </param>
    /// <param name="comparisonType">
    ///   Одно из значений перечисления, определяющее правила поиска.
    /// </param>
    /// <returns>
    ///   Отсчитываемая от нуля начальная позиция индекса параметра <paramref name="value" />, если эта строка найдена, или значение -1, если строка не найдена или текущий экземпляр равен <see cref="F:System.String.Empty" />.
    ///    Если значение параметра <paramref name="value" /> равно <see cref="F:System.String.Empty" />, возвращаемым значением является наименьшее значение из <paramref name="startIndex" /> и последнего значения индекса в данном экземпляре.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="count" /> является отрицательным значением.
    /// 
    ///   -или-
    /// 
    ///   Текущий экземпляр не равен <see cref="F:System.String.Empty" />, а <paramref name="startIndex" /> является отрицательным.
    /// 
    ///   -или-
    /// 
    ///   Текущий экземпляр не равен <see cref="F:System.String.Empty" />, а <paramref name="startIndex" /> превышает длину этого экземпляра.
    /// 
    ///   -или-
    /// 
    ///   Текущий экземпляр не равен <see cref="F:System.String.Empty" />, а <paramref name="startIndex" /> – <paramref name="count" /> + 1 указывает позицию вне пределов данного экземпляра.
    /// 
    ///   -или-
    /// 
    ///   Текущий экземпляр равен <see cref="F:System.String.Empty" />, а <paramref name="start" /> меньше -1 или больше нуля.
    /// 
    ///   -или-
    /// 
    ///   Текущий экземпляр равен <see cref="F:System.String.Empty" />, а <paramref name="count" /> больше 1.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="comparisonType" /> не является допустимым значением <see cref="T:System.StringComparison" />.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public int LastIndexOf(string value, int startIndex, int count, StringComparison comparisonType)
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      if (this.Length == 0 && (startIndex == -1 || startIndex == 0))
        return value.Length != 0 ? -1 : 0;
      if (startIndex < 0 || startIndex > this.Length)
        throw new ArgumentOutOfRangeException(nameof (startIndex), Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (startIndex == this.Length)
      {
        --startIndex;
        if (count > 0)
          --count;
        if (value.Length == 0 && count >= 0 && startIndex - count + 1 >= 0)
          return startIndex;
      }
      if (count < 0 || startIndex - count + 1 < 0)
        throw new ArgumentOutOfRangeException(nameof (count), Environment.GetResourceString("ArgumentOutOfRange_Count"));
      switch (comparisonType)
      {
        case StringComparison.CurrentCulture:
          return CultureInfo.CurrentCulture.CompareInfo.LastIndexOf(this, value, startIndex, count, CompareOptions.None);
        case StringComparison.CurrentCultureIgnoreCase:
          return CultureInfo.CurrentCulture.CompareInfo.LastIndexOf(this, value, startIndex, count, CompareOptions.IgnoreCase);
        case StringComparison.InvariantCulture:
          return CultureInfo.InvariantCulture.CompareInfo.LastIndexOf(this, value, startIndex, count, CompareOptions.None);
        case StringComparison.InvariantCultureIgnoreCase:
          return CultureInfo.InvariantCulture.CompareInfo.LastIndexOf(this, value, startIndex, count, CompareOptions.IgnoreCase);
        case StringComparison.Ordinal:
          return CultureInfo.InvariantCulture.CompareInfo.LastIndexOf(this, value, startIndex, count, CompareOptions.Ordinal);
        case StringComparison.OrdinalIgnoreCase:
          if (value.IsAscii() && this.IsAscii())
            return CultureInfo.InvariantCulture.CompareInfo.LastIndexOf(this, value, startIndex, count, CompareOptions.IgnoreCase);
          return TextInfo.LastIndexOfStringOrdinalIgnoreCase(this, value, startIndex, count);
        default:
          throw new ArgumentException(Environment.GetResourceString("NotSupported_StringComparison"), nameof (comparisonType));
      }
    }

    /// <summary>
    ///   Возвращает новую строку, в которой знаки данного экземпляра выровнены по правому краю путем добавления слева символов-разделителей до указанной общей длины.
    /// </summary>
    /// <param name="totalWidth">
    ///   Количество знаков в полученной строке, равное числу исходных знаков плюс некоторое количество добавленных для заполнения знаков.
    /// </param>
    /// <returns>
    ///   Новая строка, эквивалентная данному экземпляру, но с выравниванием по правому краю и с добавленными слева пробелами, необходимыми для достижения длины <paramref name="totalWidth" />.
    ///    Однако если значение параметра <paramref name="totalWidth" /> меньше длины данного экземпляра, метод возвращает ссылку на имеющийся экземпляр.
    ///    Если значение параметра <paramref name="totalWidth" /> равно длине данного экземпляра, метод возвращает новую строку, идентичную данному экземпляру.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="totalWidth" /> меньше нуля.
    /// </exception>
    [__DynamicallyInvokable]
    public string PadLeft(int totalWidth)
    {
      return this.PadHelper(totalWidth, ' ', false);
    }

    /// <summary>
    ///   Возвращает новую строку, в которой знаки данного экземпляра выровнены по правому краю путем добавления слева пробелов или указанного знака Юникода до указанной общей длины.
    /// </summary>
    /// <param name="totalWidth">
    ///   Количество знаков в полученной строке, равное числу исходных знаков плюс некоторое количество добавленных для заполнения знаков.
    /// </param>
    /// <param name="paddingChar">
    ///   Добавляемый в качестве заполнителя знак Юникода.
    /// </param>
    /// <returns>
    ///   Новая строка, эквивалентная данному экземпляру, но с выравниванием по правому краю и с добавленными слева знаками <paramref name="paddingChar" />, необходимыми для достижения длины <paramref name="totalWidth" />.
    ///    Однако если значение параметра <paramref name="totalWidth" /> меньше длины данного экземпляра, метод возвращает ссылку на имеющийся экземпляр.
    ///    Если значение параметра <paramref name="totalWidth" /> равно длине данного экземпляра, метод возвращает новую строку, идентичную данному экземпляру.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="totalWidth" /> меньше нуля.
    /// </exception>
    [__DynamicallyInvokable]
    public string PadLeft(int totalWidth, char paddingChar)
    {
      return this.PadHelper(totalWidth, paddingChar, false);
    }

    /// <summary>
    ///   Возвращает новую строку, в которой знаки данной строки выровнены по левому краю путем добавления справа символов-разделителей до указанной общей длины.
    /// </summary>
    /// <param name="totalWidth">
    ///   Количество знаков в полученной строке, равное числу исходных знаков плюс некоторое количество добавленных для заполнения знаков.
    /// </param>
    /// <returns>
    ///   Новая строка, эквивалентная данному экземпляру, но с выравниванием по левому краю и с добавленными справа пробелами, необходимыми для достижения длины <paramref name="totalWidth" />.
    ///    Однако если значение параметра <paramref name="totalWidth" /> меньше длины данного экземпляра, метод возвращает ссылку на имеющийся экземпляр.
    ///    Если значение параметра <paramref name="totalWidth" /> равно длине данного экземпляра, метод возвращает новую строку, идентичную данному экземпляру.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="totalWidth" /> меньше нуля.
    /// </exception>
    [__DynamicallyInvokable]
    public string PadRight(int totalWidth)
    {
      return this.PadHelper(totalWidth, ' ', true);
    }

    /// <summary>
    ///   Возвращает новую строку, в которой знаки данной строки выровнены по левому краю путем добавления справа пробелов или указанного знака Юникода до указанной общей длины.
    /// </summary>
    /// <param name="totalWidth">
    ///   Количество знаков в полученной строке, равное числу исходных знаков плюс некоторое количество добавленных для заполнения знаков.
    /// </param>
    /// <param name="paddingChar">
    ///   Добавляемый в качестве заполнителя знак Юникода.
    /// </param>
    /// <returns>
    ///   Новая строка, эквивалентная данному экземпляру, но с выравниванием по левому краю и с добавленными справа знаками <paramref name="paddingChar" />, необходимыми для достижения длины <paramref name="totalWidth" />.
    ///    Однако если значение параметра <paramref name="totalWidth" /> меньше длины данного экземпляра, метод возвращает ссылку на имеющийся экземпляр.
    ///    Если значение параметра <paramref name="totalWidth" /> равно длине данного экземпляра, метод возвращает новую строку, идентичную данному экземпляру.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="totalWidth" /> меньше нуля.
    /// </exception>
    [__DynamicallyInvokable]
    public string PadRight(int totalWidth, char paddingChar)
    {
      return this.PadHelper(totalWidth, paddingChar, true);
    }

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private extern string PadHelper(int totalWidth, char paddingChar, bool isRightPadded);

    /// <summary>
    ///   Определяет, совпадает ли начало данного экземпляра строки с указанной строкой.
    /// </summary>
    /// <param name="value">Строка, подлежащая сравнению.</param>
    /// <returns>
    ///   Значение <see langword="true" />, если параметр <paramref name="value" /> соответствует началу данной строки; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public bool StartsWith(string value)
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      return this.StartsWith(value, StringComparison.CurrentCulture);
    }

    /// <summary>
    ///   Определяет, совпадает ли начало этого экземпляра строки с заданной строкой при сравнении с учетом заданного параметра сравнения.
    /// </summary>
    /// <param name="value">Строка, подлежащая сравнению.</param>
    /// <param name="comparisonType">
    ///   Одно из значений перечисления, определяющее способ сравнения данной строки со значением <paramref name="value" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если данный экземпляр начинается со значения <paramref name="value" />; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="comparisonType" /> не является значением <see cref="T:System.StringComparison" />.
    /// </exception>
    [SecuritySafeCritical]
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public bool StartsWith(string value, StringComparison comparisonType)
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      switch (comparisonType)
      {
        case StringComparison.CurrentCulture:
        case StringComparison.CurrentCultureIgnoreCase:
        case StringComparison.InvariantCulture:
        case StringComparison.InvariantCultureIgnoreCase:
        case StringComparison.Ordinal:
        case StringComparison.OrdinalIgnoreCase:
          if ((object) this == (object) value || value.Length == 0)
            return true;
          switch (comparisonType)
          {
            case StringComparison.CurrentCulture:
              return CultureInfo.CurrentCulture.CompareInfo.IsPrefix(this, value, CompareOptions.None);
            case StringComparison.CurrentCultureIgnoreCase:
              return CultureInfo.CurrentCulture.CompareInfo.IsPrefix(this, value, CompareOptions.IgnoreCase);
            case StringComparison.InvariantCulture:
              return CultureInfo.InvariantCulture.CompareInfo.IsPrefix(this, value, CompareOptions.None);
            case StringComparison.InvariantCultureIgnoreCase:
              return CultureInfo.InvariantCulture.CompareInfo.IsPrefix(this, value, CompareOptions.IgnoreCase);
            case StringComparison.Ordinal:
              if (this.Length < value.Length)
                return false;
              return string.nativeCompareOrdinalEx(this, 0, value, 0, value.Length) == 0;
            case StringComparison.OrdinalIgnoreCase:
              if (this.Length < value.Length)
                return false;
              return TextInfo.CompareOrdinalIgnoreCaseEx(this, 0, value, 0, value.Length, value.Length) == 0;
            default:
              throw new ArgumentException(Environment.GetResourceString("NotSupported_StringComparison"), nameof (comparisonType));
          }
        default:
          throw new ArgumentException(Environment.GetResourceString("NotSupported_StringComparison"), nameof (comparisonType));
      }
    }

    /// <summary>
    ///   Определяет, совпадает ли начало экземпляра строки с заданной строкой при сравнении с учетом заданного языка и региональных параметров.
    /// </summary>
    /// <param name="value">Строка, подлежащая сравнению.</param>
    /// <param name="ignoreCase">
    ///   Значение <see langword="true" />, чтобы не учитывать регистр при сравнении; в противном случае — значение <see langword="false" />.
    /// </param>
    /// <param name="culture">
    ///   Связанные с языком и региональными параметрами сведения, определяющие, как выполняется сравнение этой строки и <paramref name="value" />.
    ///    Если значением параметра <paramref name="culture" /> является <see langword="null" />, используется текущий язык и региональные параметры.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если параметр <paramref name="value" /> соответствует началу данной строки; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    public bool StartsWith(string value, bool ignoreCase, CultureInfo culture)
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      if ((object) this == (object) value)
        return true;
      return (culture != null ? culture : CultureInfo.CurrentCulture).CompareInfo.IsPrefix(this, value, ignoreCase ? CompareOptions.IgnoreCase : CompareOptions.None);
    }

    /// <summary>
    ///   Возвращает копию этой строки, переведенную в нижний регистр.
    /// </summary>
    /// <returns>Строка в нижнем регистре.</returns>
    [__DynamicallyInvokable]
    public string ToLower()
    {
      return this.ToLower(CultureInfo.CurrentCulture);
    }

    /// <summary>
    ///   Возвращает копию этой строки, переведенную в нижний регистр, используя правила определения регистра заданного языка и региональных параметров.
    /// </summary>
    /// <param name="culture">
    ///   Объект, задающий правила определения регистра для языка и региональных параметров.
    /// </param>
    /// <returns>Эквивалент текущей строки в нижнем регистре.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="culture" /> имеет значение <see langword="null" />.
    /// </exception>
    public string ToLower(CultureInfo culture)
    {
      if (culture == null)
        throw new ArgumentNullException(nameof (culture));
      return culture.TextInfo.ToLower(this);
    }

    /// <summary>
    ///   Возвращает копию этого объекта <see cref="T:System.String" />, переведенную в нижний регистр, используя правила учета регистра инвариантного языка и региональных параметров.
    /// </summary>
    /// <returns>Эквивалент текущей строки в нижнем регистре.</returns>
    [__DynamicallyInvokable]
    public string ToLowerInvariant()
    {
      return this.ToLower(CultureInfo.InvariantCulture);
    }

    /// <summary>
    ///   Возвращает копию этой строки, переведенную в верхний регистр.
    /// </summary>
    /// <returns>Эквивалент текущей строки в верхнем регистре.</returns>
    [__DynamicallyInvokable]
    public string ToUpper()
    {
      return this.ToUpper(CultureInfo.CurrentCulture);
    }

    /// <summary>
    ///   Возвращает копию этой строки, переведенную в верхний регистр, используя правила определения регистра заданного языка и региональных параметров.
    /// </summary>
    /// <param name="culture">
    ///   Объект, задающий правила определения регистра для языка и региональных параметров.
    /// </param>
    /// <returns>Эквивалент текущей строки в верхнем регистре.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="culture" /> имеет значение <see langword="null" />.
    /// </exception>
    public string ToUpper(CultureInfo culture)
    {
      if (culture == null)
        throw new ArgumentNullException(nameof (culture));
      return culture.TextInfo.ToUpper(this);
    }

    /// <summary>
    ///   Возвращает копию этого объекта <see cref="T:System.String" />, переведенную в верхний регистр, используя правила учета регистра инвариантного языка и региональных параметров.
    /// </summary>
    /// <returns>Эквивалент текущей строки в верхнем регистре.</returns>
    [__DynamicallyInvokable]
    public string ToUpperInvariant()
    {
      return this.ToUpper(CultureInfo.InvariantCulture);
    }

    /// <summary>
    ///   Возвращает этот экземпляр <see cref="T:System.String" />; реальное преобразование не осуществляется.
    /// </summary>
    /// <returns>Текущая строка.</returns>
    [__DynamicallyInvokable]
    public override string ToString()
    {
      return this;
    }

    /// <summary>
    ///   Возвращает этот экземпляр <see cref="T:System.String" />; реальное преобразование не осуществляется.
    /// </summary>
    /// <param name="provider">
    ///   (Зарезервирован.) Объект, предоставляющий сведения о форматировании, связанные с определенным языком и региональными параметрами.
    /// </param>
    /// <returns>Текущая строка.</returns>
    public string ToString(IFormatProvider provider)
    {
      return this;
    }

    /// <summary>
    ///   Возвращает ссылку на данный экземпляр класса <see cref="T:System.String" />.
    /// </summary>
    /// <returns>
    ///   Этот экземпляр <see cref="T:System.String" />.
    /// </returns>
    public object Clone()
    {
      return (object) this;
    }

    private static bool IsBOMWhitespace(char c)
    {
      return false;
    }

    /// <summary>
    ///   Удаляет все начальные и конечные символы-разделители из текущего объекта <see cref="T:System.String" />.
    /// </summary>
    /// <returns>
    ///   Строка, оставшаяся после удаления всех символов-разделителей из начала и конца текущей строки.
    ///    Если в текущем экземпляре невозможно усечь символы, метод возвращает текущий экземпляр без изменений.
    /// </returns>
    [__DynamicallyInvokable]
    public string Trim()
    {
      return this.TrimHelper(2);
    }

    [SecuritySafeCritical]
    private string TrimHelper(int trimType)
    {
      int end = this.Length - 1;
      int start = 0;
      if (trimType != 1)
      {
        start = 0;
        while (start < this.Length && (char.IsWhiteSpace(this[start]) || string.IsBOMWhitespace(this[start])))
          ++start;
      }
      if (trimType != 0)
      {
        end = this.Length - 1;
        while (end >= start && (char.IsWhiteSpace(this[end]) || string.IsBOMWhitespace(this[start])))
          --end;
      }
      return this.CreateTrimmedString(start, end);
    }

    [SecuritySafeCritical]
    private string TrimHelper(char[] trimChars, int trimType)
    {
      int end = this.Length - 1;
      int start = 0;
      if (trimType != 1)
      {
        for (start = 0; start < this.Length; ++start)
        {
          char ch = this[start];
          int index = 0;
          while (index < trimChars.Length && (int) trimChars[index] != (int) ch)
            ++index;
          if (index == trimChars.Length)
            break;
        }
      }
      if (trimType != 0)
      {
        for (end = this.Length - 1; end >= start; --end)
        {
          char ch = this[end];
          int index = 0;
          while (index < trimChars.Length && (int) trimChars[index] != (int) ch)
            ++index;
          if (index == trimChars.Length)
            break;
        }
      }
      return this.CreateTrimmedString(start, end);
    }

    [SecurityCritical]
    private string CreateTrimmedString(int start, int end)
    {
      int length = end - start + 1;
      if (length == this.Length)
        return this;
      if (length == 0)
        return string.Empty;
      return this.InternalSubString(start, length);
    }

    /// <summary>
    ///   Возвращает новую строку, в которой указанная строка вставляется в указанной позиции индекса в данном экземпляре.
    /// </summary>
    /// <param name="startIndex">
    ///   Положение отсчитываемого от нуля индекса вставки.
    /// </param>
    /// <param name="value">Строка, которую требуется вставить.</param>
    /// <returns>
    ///   Новая строка, эквивалентная данному экземпляру, но с тем отличием, что в положение <paramref name="value" /> помещено значение <paramref name="startIndex" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="startIndex" /> имеет отрицательное значение или больше длины этого экземпляра.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public unsafe string Insert(int startIndex, string value)
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      if (startIndex < 0 || startIndex > this.Length)
        throw new ArgumentOutOfRangeException(nameof (startIndex));
      int length1 = this.Length;
      int length2 = value.Length;
      int length3 = length1 + length2;
      if (length3 == 0)
        return string.Empty;
      string str = string.FastAllocateString(length3);
      fixed (char* smem1 = &this.m_firstChar)
        fixed (char* smem2 = &value.m_firstChar)
          fixed (char* dmem = &str.m_firstChar)
          {
            string.wstrcpy(dmem, smem1, startIndex);
            string.wstrcpy(dmem + startIndex, smem2, length2);
            string.wstrcpy(dmem + startIndex + length2, smem1 + startIndex, length1 - startIndex);
          }
      return str;
    }

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private extern string ReplaceInternal(char oldChar, char newChar);

    /// <summary>
    ///   Возвращает новую строку, в которой все вхождения заданного знака Юникода в текущем экземпляре заменены другим заданным знаком Юникода.
    /// </summary>
    /// <param name="oldChar">Заменяемый знак Юникода.</param>
    /// <param name="newChar">
    ///   Знак Юникода для замены всех обнаруженных вхождений <paramref name="oldChar" />.
    /// </param>
    /// <returns>
    ///   Строка, эквивалентная данному экземпляру, но с тем отличием, что все вхождения <paramref name="oldChar" /> заменены на <paramref name="newChar" />.
    ///    Если <paramref name="oldChar" /> не обнаружен в текущем экземпляре метод возвращает текущий экземпляр без изменений.
    /// </returns>
    [__DynamicallyInvokable]
    public string Replace(char oldChar, char newChar)
    {
      return this.ReplaceInternal(oldChar, newChar);
    }

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private extern string ReplaceInternal(string oldValue, string newValue);

    /// <summary>
    ///   Возвращает новую строку, в которой все вхождения заданной строки в текущем экземпляре заменены другой заданной строкой.
    /// </summary>
    /// <param name="oldValue">Строка, которую требуется заменить.</param>
    /// <param name="newValue">
    ///   Строка для замены всех вхождений <paramref name="oldValue" />.
    /// </param>
    /// <returns>
    ///   Строка, эквивалентная текущей строке, но с тем отличием, что все вхождения <paramref name="oldValue" /> заменены на <paramref name="newValue" />.
    ///    Если <paramref name="oldValue" /> не обнаружен в текущем экземпляре метод возвращает текущий экземпляр без изменений.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="oldValue" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="oldValue" /> является пустой строкой ("").
    /// </exception>
    [__DynamicallyInvokable]
    public string Replace(string oldValue, string newValue)
    {
      if (oldValue == null)
        throw new ArgumentNullException(nameof (oldValue));
      return this.ReplaceInternal(oldValue, newValue);
    }

    /// <summary>
    ///   Возвращает новую строку, в которой было удалено указанное число символов в указанной позиции.
    /// </summary>
    /// <param name="startIndex">
    ///   Отсчитываемая от нуля позиция, с которой начинается удаление знаков.
    /// </param>
    /// <param name="count">Число символов для удаления.</param>
    /// <returns>
    ///   Новая строка, эквивалентная данному экземпляру за минусом удаленных знаков.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="startIndex" /> или <paramref name="count" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="startIndex" /> плюс <paramref name="count" /> указывает позицию за пределами этого экземпляра.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public unsafe string Remove(int startIndex, int count)
    {
      if (startIndex < 0)
        throw new ArgumentOutOfRangeException(nameof (startIndex), Environment.GetResourceString("ArgumentOutOfRange_StartIndex"));
      if (count < 0)
        throw new ArgumentOutOfRangeException(nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NegativeCount"));
      if (count > this.Length - startIndex)
        throw new ArgumentOutOfRangeException(nameof (count), Environment.GetResourceString("ArgumentOutOfRange_IndexCount"));
      int length = this.Length - count;
      if (length == 0)
        return string.Empty;
      string str = string.FastAllocateString(length);
      fixed (char* smem = &this.m_firstChar)
        fixed (char* dmem = &str.m_firstChar)
        {
          string.wstrcpy(dmem, smem, startIndex);
          string.wstrcpy(dmem + startIndex, smem + startIndex + count, length - startIndex);
        }
      return str;
    }

    /// <summary>
    ///   Возвращает новую строку, в которой были удалены все символы, начиная с указанной позиции и до конца в текущем экземпляре.
    /// </summary>
    /// <param name="startIndex">
    ///   Отсчитываемая от нуля позиция, с которой начинается удаление знаков.
    /// </param>
    /// <returns>
    ///   Новая строка, эквивалентная данной строке за минусом удаленных знаков.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="startIndex" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="startIndex" /> указывает положение, которое находится за пределами этой строки.
    /// </exception>
    [__DynamicallyInvokable]
    public string Remove(int startIndex)
    {
      if (startIndex < 0)
        throw new ArgumentOutOfRangeException(nameof (startIndex), Environment.GetResourceString("ArgumentOutOfRange_StartIndex"));
      if (startIndex >= this.Length)
        throw new ArgumentOutOfRangeException(nameof (startIndex), Environment.GetResourceString("ArgumentOutOfRange_StartIndexLessThanLength"));
      return this.Substring(0, startIndex);
    }

    /// <summary>
    ///   Заменяет один или более элементов формата в указанной строке строковым представлением указанного объекта.
    /// </summary>
    /// <param name="format">Объект Строка составного формата.</param>
    /// <param name="arg0">Объект для форматирования.</param>
    /// <returns>
    ///   Копия <paramref name="format" />, в которой все элементы формата заменены строковыми представлениями <paramref name="arg0" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="format" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   Недопустимый элемент формата в <paramref name="format" />.
    /// 
    ///   -или-
    /// 
    ///   Индекс элемента формата не равен нулю.
    /// </exception>
    [__DynamicallyInvokable]
    public static string Format(string format, object arg0)
    {
      return string.FormatHelper((IFormatProvider) null, format, new ParamsArray(arg0));
    }

    /// <summary>
    ///   Заменяет элементы формата в указанной строке строковым представлением двух указанных объектов.
    /// </summary>
    /// <param name="format">Объект Строка составного формата.</param>
    /// <param name="arg0">Первый объект для форматирования.</param>
    /// <param name="arg1">Второй объект для форматирования.</param>
    /// <returns>
    ///   Копия <paramref name="format" />, в которой все элементы формата заменены строковыми представлениями <paramref name="arg0" /> и <paramref name="arg1" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="format" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="format" /> недопустим.
    /// 
    ///   -или-
    /// 
    ///   Индекс элемента формата не равен нулю или единице.
    /// </exception>
    [__DynamicallyInvokable]
    public static string Format(string format, object arg0, object arg1)
    {
      return string.FormatHelper((IFormatProvider) null, format, new ParamsArray(arg0, arg1));
    }

    /// <summary>
    ///   Заменяет элементы формата в указанной строке строковым представлением трех указанных объектов.
    /// </summary>
    /// <param name="format">Объект Строка составного формата.</param>
    /// <param name="arg0">Первый объект для форматирования.</param>
    /// <param name="arg1">Второй объект для форматирования.</param>
    /// <param name="arg2">Третий объект для форматирования.</param>
    /// <returns>
    ///   Копия <paramref name="format" />, в которой все элементы формата заменены строковыми представлениями <paramref name="arg0" />, <paramref name="arg1" /> и <paramref name="arg2" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="format" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="format" /> недопустим.
    /// 
    ///   -или-
    /// 
    ///   Индекс элемента формата меньше нуля или больше двух.
    /// </exception>
    [__DynamicallyInvokable]
    public static string Format(string format, object arg0, object arg1, object arg2)
    {
      return string.FormatHelper((IFormatProvider) null, format, new ParamsArray(arg0, arg1, arg2));
    }

    /// <summary>
    ///   Заменяет элемент формата в указанной строке строковым представлением соответствующего объекта в указанном массиве.
    /// </summary>
    /// <param name="format">Объект Строка составного формата.</param>
    /// <param name="args">
    ///   Массив объектов, содержащий нуль или более форматируемых объектов.
    /// </param>
    /// <returns>
    ///   Копия <paramref name="format" />, в которой элементы формата заменены строковыми представления соответствующих объектов в <paramref name="args" />.
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
    [__DynamicallyInvokable]
    public static string Format(string format, params object[] args)
    {
      if (args == null)
        throw new ArgumentNullException(format == null ? nameof (format) : nameof (args));
      return string.FormatHelper((IFormatProvider) null, format, new ParamsArray(args));
    }

    /// <summary>
    ///   Заменяет элементы формата в указанной строке строковым представлением соответствующего объекта.
    ///    Параметр предоставляет сведения об особенностях форматирования, связанных с языком и региональными параметрами.
    /// </summary>
    /// <param name="provider">
    ///   Объект, предоставляющий сведения о форматировании для определенного языка и региональных параметров.
    /// </param>
    /// <param name="format">Объект Строка составного формата.</param>
    /// <param name="arg0">Объект для форматирования.</param>
    /// <returns>
    ///   Копия <paramref name="format" />, в которой элементы форматирования были заменены строковым представлением <paramref name="arg0" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="format" /> или <paramref name="arg0" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="format" /> недопустим.
    /// 
    ///   -или-
    /// 
    ///   Индекс элемента формата меньше нуля либо больше или равен единице.
    /// </exception>
    [__DynamicallyInvokable]
    public static string Format(IFormatProvider provider, string format, object arg0)
    {
      return string.FormatHelper(provider, format, new ParamsArray(arg0));
    }

    /// <summary>
    ///   Заменяет элементы формата в указанной строке строковым представлением двух указанных объектов.
    ///    Параметр предоставляет сведения об особенностях форматирования, связанных с языком и региональными параметрами.
    /// </summary>
    /// <param name="provider">
    ///   Объект, предоставляющий сведения о форматировании для определенного языка и региональных параметров.
    /// </param>
    /// <param name="format">Объект Строка составного формата.</param>
    /// <param name="arg0">Первый объект для форматирования.</param>
    /// <param name="arg1">Второй объект для форматирования.</param>
    /// <returns>
    ///   Копия <paramref name="format" />, в которой все элементы формата заменены строковыми представлениями <paramref name="arg0" /> и <paramref name="arg1" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="format" />, <paramref name="arg0" /> или <paramref name="arg1" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="format" /> недопустим.
    /// 
    ///   -или-
    /// 
    ///   Индекс элемента формата меньше нуля либо больше или равен двум.
    /// </exception>
    [__DynamicallyInvokable]
    public static string Format(IFormatProvider provider, string format, object arg0, object arg1)
    {
      return string.FormatHelper(provider, format, new ParamsArray(arg0, arg1));
    }

    /// <summary>
    ///   Заменяет элементы формата в указанной строке строковым представлением трех указанных объектов.
    ///    Параметр предоставляет сведения об особенностях форматирования, связанных с языком и региональными параметрами.
    /// </summary>
    /// <param name="provider">
    ///   Объект, предоставляющий сведения о форматировании для определенного языка и региональных параметров.
    /// </param>
    /// <param name="format">Объект Строка составного формата.</param>
    /// <param name="arg0">Первый объект для форматирования.</param>
    /// <param name="arg1">Второй объект для форматирования.</param>
    /// <param name="arg2">Третий объект для форматирования.</param>
    /// <returns>
    ///   Копия <paramref name="format" />, в которой все элементы формата заменены строковыми представлениями <paramref name="arg0" />, <paramref name="arg1" /> и <paramref name="arg2" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Значение параметра <paramref name="format" />, <paramref name="arg0" />, <paramref name="arg1" /> или <paramref name="arg2" /> — <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="format" /> недопустим.
    /// 
    ///   -или-
    /// 
    ///   Индекс элемента формата меньше нуля либо больше или равен трем.
    /// </exception>
    [__DynamicallyInvokable]
    public static string Format(IFormatProvider provider, string format, object arg0, object arg1, object arg2)
    {
      return string.FormatHelper(provider, format, new ParamsArray(arg0, arg1, arg2));
    }

    /// <summary>
    ///   Заменяет элементы формата в указанной строке строковым представлениями соответствующих объектов в указанном массиве.
    ///    Параметр предоставляет сведения об особенностях форматирования, связанных с языком и региональными параметрами.
    /// </summary>
    /// <param name="provider">
    ///   Объект, предоставляющий сведения о форматировании для определенного языка и региональных параметров.
    /// </param>
    /// <param name="format">Строка составного форматирования.</param>
    /// <param name="args">
    ///   Массив объектов, содержащий нуль или более форматируемых объектов.
    /// </param>
    /// <returns>
    ///   Копия <paramref name="format" />, в которой элементы формата заменены строковыми представления соответствующих объектов в <paramref name="args" />.
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
    [__DynamicallyInvokable]
    public static string Format(IFormatProvider provider, string format, params object[] args)
    {
      if (args == null)
        throw new ArgumentNullException(format == null ? nameof (format) : nameof (args));
      return string.FormatHelper(provider, format, new ParamsArray(args));
    }

    private static string FormatHelper(IFormatProvider provider, string format, ParamsArray args)
    {
      if (format == null)
        throw new ArgumentNullException(nameof (format));
      return StringBuilderCache.GetStringAndRelease(StringBuilderCache.Acquire(format.Length + args.Length * 8).AppendFormatHelper(provider, format, args));
    }

    /// <summary>
    ///   Создает экземпляр <see cref="T:System.String" />, имеющий то же значение, что и указанный экземпляр <see cref="T:System.String" />.
    /// </summary>
    /// <param name="str">Строка для копирования.</param>
    /// <returns>
    ///   Новая строка с тем же значением, что и <paramref name="str" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="str" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    public static unsafe string Copy(string str)
    {
      if (str == null)
        throw new ArgumentNullException(nameof (str));
      int length = str.Length;
      string str1 = string.FastAllocateString(length);
      fixed (char* dmem = &str1.m_firstChar)
        fixed (char* smem = &str.m_firstChar)
          string.wstrcpy(dmem, smem, length);
      return str1;
    }

    /// <summary>Создает строковое представление указанного объекта.</summary>
    /// <param name="arg0">
    ///   Объект для представления или значение <see langword="null" />.
    /// </param>
    /// <returns>
    ///   Строковое представление значения параметра <paramref name="arg0" /> или <see cref="F:System.String.Empty" />, если значение параметра <paramref name="arg0" /> равно <see langword="null" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static string Concat(object arg0)
    {
      if (arg0 == null)
        return string.Empty;
      return arg0.ToString();
    }

    /// <summary>
    ///   Сцепляет строковые представления двух указанных объектов.
    /// </summary>
    /// <param name="arg0">Первый из сцепляемых объектов.</param>
    /// <param name="arg1">Второй из сцепляемых объектов.</param>
    /// <returns>
    ///   Сцепленные строковые представления значений <paramref name="arg0" /> и <paramref name="arg1" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static string Concat(object arg0, object arg1)
    {
      if (arg0 == null)
        arg0 = (object) string.Empty;
      if (arg1 == null)
        arg1 = (object) string.Empty;
      return arg0.ToString() + arg1.ToString();
    }

    /// <summary>
    ///   Сцепляет строковые представления трех указанных объектов.
    /// </summary>
    /// <param name="arg0">Первый из сцепляемых объектов.</param>
    /// <param name="arg1">Второй из сцепляемых объектов.</param>
    /// <param name="arg2">Третий из сцепляемых объектов.</param>
    /// <returns>
    ///   Сцепленные строковые представления значений <paramref name="arg0" />, <paramref name="arg1" /> и <paramref name="arg2" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static string Concat(object arg0, object arg1, object arg2)
    {
      if (arg0 == null)
        arg0 = (object) string.Empty;
      if (arg1 == null)
        arg1 = (object) string.Empty;
      if (arg2 == null)
        arg2 = (object) string.Empty;
      return arg0.ToString() + arg1.ToString() + arg2.ToString();
    }

    [CLSCompliant(false)]
    public static string Concat(object arg0, object arg1, object arg2, object arg3, __arglist)
    {
      ArgIterator argIterator = new ArgIterator(__arglist);
      int length = argIterator.GetRemainingCount() + 4;
      object[] objArray = new object[length];
      objArray[0] = arg0;
      objArray[1] = arg1;
      objArray[2] = arg2;
      objArray[3] = arg3;
      for (int index = 4; index < length; ++index)
        objArray[index] = TypedReference.ToObject(argIterator.GetNextArg());
      return string.Concat(objArray);
    }

    /// <summary>
    ///   Сцепляет строковые представления элементов указанного массива <see cref="T:System.Object" />.
    /// </summary>
    /// <param name="args">
    ///   Массив объектов, содержащий элементы, которые требуется сцепить.
    /// </param>
    /// <returns>
    ///   Сцепленные строковые представления значений элементов параметра <paramref name="args" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="args" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.OutOfMemoryException">
    ///   Недостаточно памяти.
    /// </exception>
    [__DynamicallyInvokable]
    public static string Concat(params object[] args)
    {
      if (args == null)
        throw new ArgumentNullException(nameof (args));
      string[] values = new string[args.Length];
      int totalLength = 0;
      for (int index = 0; index < args.Length; ++index)
      {
        object obj = args[index];
        values[index] = obj == null ? string.Empty : obj.ToString();
        if (values[index] == null)
          values[index] = string.Empty;
        totalLength += values[index].Length;
        if (totalLength < 0)
          throw new OutOfMemoryException();
      }
      return string.ConcatArray(values, totalLength);
    }

    /// <summary>
    ///   Сцепляет элементы реализации <see cref="T:System.Collections.Generic.IEnumerable`1" />.
    /// </summary>
    /// <param name="values">
    ///   Объект коллекции, реализующий интерфейс <see cref="T:System.Collections.Generic.IEnumerable`1" />.
    /// </param>
    /// <typeparam name="T">
    ///   Тип элементов параметра <paramref name="values" />.
    /// </typeparam>
    /// <returns>
    ///   Сцепленные элементы в параметре <paramref name="values" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="values" /> имеет значение <see langword="null" />.
    /// </exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public static string Concat<T>(IEnumerable<T> values)
    {
      if (values == null)
        throw new ArgumentNullException(nameof (values));
      StringBuilder sb = StringBuilderCache.Acquire(16);
      using (IEnumerator<T> enumerator = values.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          if ((object) enumerator.Current != null)
          {
            string str = enumerator.Current.ToString();
            if (str != null)
              sb.Append(str);
          }
        }
      }
      return StringBuilderCache.GetStringAndRelease(sb);
    }

    /// <summary>
    ///   Сцепляет элементы созданной коллекции <see cref="T:System.Collections.Generic.IEnumerable`1" /> типа <see cref="T:System.String" />.
    /// </summary>
    /// <param name="values">
    ///   Объект коллекции, реализующий интерфейс <see cref="T:System.Collections.Generic.IEnumerable`1" /> и имеющий аргумент универсального типа <see cref="T:System.String" />.
    /// </param>
    /// <returns>
    ///   Сцепленные строки в параметре <paramref name="values" />. Или <see cref="F:System.String.Empty" />, если <paramref name="values" /> — это пустой элемент <see langword="IEnumerable(Of String)" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="values" /> имеет значение <see langword="null" />.
    /// </exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public static string Concat(IEnumerable<string> values)
    {
      if (values == null)
        throw new ArgumentNullException(nameof (values));
      StringBuilder sb = StringBuilderCache.Acquire(16);
      using (IEnumerator<string> enumerator = values.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          if (enumerator.Current != null)
            sb.Append(enumerator.Current);
        }
      }
      return StringBuilderCache.GetStringAndRelease(sb);
    }

    /// <summary>
    ///   Сцепляет два указанных экземпляра <see cref="T:System.String" />.
    /// </summary>
    /// <param name="str0">Первая из сцепляемых строк.</param>
    /// <param name="str1">Вторая из сцепляемых строк.</param>
    /// <returns>
    ///   Сцепление <paramref name="str0" /> и <paramref name="str1" />.
    /// </returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static string Concat(string str0, string str1)
    {
      if (string.IsNullOrEmpty(str0))
      {
        if (string.IsNullOrEmpty(str1))
          return string.Empty;
        return str1;
      }
      if (string.IsNullOrEmpty(str1))
        return str0;
      int length = str0.Length;
      string dest = string.FastAllocateString(length + str1.Length);
      string.FillStringChecked(dest, 0, str0);
      string.FillStringChecked(dest, length, str1);
      return dest;
    }

    /// <summary>
    ///   Сцепляет три указанных экземпляра <see cref="T:System.String" />.
    /// </summary>
    /// <param name="str0">Первая из сцепляемых строк.</param>
    /// <param name="str1">Вторая из сцепляемых строк.</param>
    /// <param name="str2">Третья из сцепляемых строк.</param>
    /// <returns>
    ///   Сцепление <paramref name="str0" />, <paramref name="str1" /> и <paramref name="str2" />..
    /// </returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static string Concat(string str0, string str1, string str2)
    {
      if (str0 == null && str1 == null && str2 == null)
        return string.Empty;
      if (str0 == null)
        str0 = string.Empty;
      if (str1 == null)
        str1 = string.Empty;
      if (str2 == null)
        str2 = string.Empty;
      string dest = string.FastAllocateString(str0.Length + str1.Length + str2.Length);
      string.FillStringChecked(dest, 0, str0);
      string.FillStringChecked(dest, str0.Length, str1);
      string.FillStringChecked(dest, str0.Length + str1.Length, str2);
      return dest;
    }

    /// <summary>
    ///   Сцепляет четыре указанных экземпляра <see cref="T:System.String" />.
    /// </summary>
    /// <param name="str0">Первая из сцепляемых строк.</param>
    /// <param name="str1">Вторая из сцепляемых строк.</param>
    /// <param name="str2">Третья из сцепляемых строк.</param>
    /// <param name="str3">Четвертая из сцепляемых строк.</param>
    /// <returns>
    ///   Сцепление <paramref name="str0" />, <paramref name="str1" />, <paramref name="str2" /> и <paramref name="str3" />.
    /// </returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static string Concat(string str0, string str1, string str2, string str3)
    {
      if (str0 == null && str1 == null && (str2 == null && str3 == null))
        return string.Empty;
      if (str0 == null)
        str0 = string.Empty;
      if (str1 == null)
        str1 = string.Empty;
      if (str2 == null)
        str2 = string.Empty;
      if (str3 == null)
        str3 = string.Empty;
      string dest = string.FastAllocateString(str0.Length + str1.Length + str2.Length + str3.Length);
      string.FillStringChecked(dest, 0, str0);
      string.FillStringChecked(dest, str0.Length, str1);
      string.FillStringChecked(dest, str0.Length + str1.Length, str2);
      string.FillStringChecked(dest, str0.Length + str1.Length + str2.Length, str3);
      return dest;
    }

    [SecuritySafeCritical]
    private static string ConcatArray(string[] values, int totalLength)
    {
      string dest = string.FastAllocateString(totalLength);
      int destPos = 0;
      for (int index = 0; index < values.Length; ++index)
      {
        string.FillStringChecked(dest, destPos, values[index]);
        destPos += values[index].Length;
      }
      return dest;
    }

    /// <summary>
    ///   Сцепляет элементы указанного массива <see cref="T:System.String" />.
    /// </summary>
    /// <param name="values">Массив строк.</param>
    /// <returns>
    ///   Сцепленные элементы <paramref name="values" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="values" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.OutOfMemoryException">
    ///   Недостаточно памяти.
    /// </exception>
    [__DynamicallyInvokable]
    public static string Concat(params string[] values)
    {
      if (values == null)
        throw new ArgumentNullException(nameof (values));
      int totalLength = 0;
      string[] values1 = new string[values.Length];
      for (int index = 0; index < values.Length; ++index)
      {
        string str = values[index];
        values1[index] = str == null ? string.Empty : str;
        totalLength += values1[index].Length;
        if (totalLength < 0)
          throw new OutOfMemoryException();
      }
      return string.ConcatArray(values1, totalLength);
    }

    /// <summary>
    ///   Извлекает системную ссылку на указанный объект <see cref="T:System.String" />.
    /// </summary>
    /// <param name="str">Строка для поиска в пуле интернирования.</param>
    /// <returns>
    ///   Системная ссылка на значение <paramref name="str" />, если оно уже интернировано; в противном случае возвращается новая ссылка на строку со значением <paramref name="str" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="str" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    public static string Intern(string str)
    {
      if (str == null)
        throw new ArgumentNullException(nameof (str));
      return Thread.GetDomain().GetOrInternString(str);
    }

    /// <summary>
    ///   Извлекает ссылку на указанный объект <see cref="T:System.String" />.
    /// </summary>
    /// <param name="str">Строка для поиска в пуле интернирования.</param>
    /// <returns>
    ///   Ссылка на значение <paramref name="str" />, если оно находится в пуле интернирования среды CLR; в противном случае — значение <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="str" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    public static string IsInterned(string str)
    {
      if (str == null)
        throw new ArgumentNullException(nameof (str));
      return Thread.GetDomain().IsStringInterned(str);
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.TypeCode" /> для класса <see cref="T:System.String" />.
    /// </summary>
    /// <returns>
    ///   Константа перечислимого типа, <see cref="F:System.TypeCode.String" />.
    /// </returns>
    public TypeCode GetTypeCode()
    {
      return TypeCode.String;
    }

    [__DynamicallyInvokable]
    bool IConvertible.ToBoolean(IFormatProvider provider)
    {
      return Convert.ToBoolean(this, provider);
    }

    [__DynamicallyInvokable]
    char IConvertible.ToChar(IFormatProvider provider)
    {
      return Convert.ToChar(this, provider);
    }

    [__DynamicallyInvokable]
    sbyte IConvertible.ToSByte(IFormatProvider provider)
    {
      return Convert.ToSByte(this, provider);
    }

    [__DynamicallyInvokable]
    byte IConvertible.ToByte(IFormatProvider provider)
    {
      return Convert.ToByte(this, provider);
    }

    [__DynamicallyInvokable]
    short IConvertible.ToInt16(IFormatProvider provider)
    {
      return Convert.ToInt16(this, provider);
    }

    [__DynamicallyInvokable]
    ushort IConvertible.ToUInt16(IFormatProvider provider)
    {
      return Convert.ToUInt16(this, provider);
    }

    [__DynamicallyInvokable]
    int IConvertible.ToInt32(IFormatProvider provider)
    {
      return Convert.ToInt32(this, provider);
    }

    [__DynamicallyInvokable]
    uint IConvertible.ToUInt32(IFormatProvider provider)
    {
      return Convert.ToUInt32(this, provider);
    }

    [__DynamicallyInvokable]
    long IConvertible.ToInt64(IFormatProvider provider)
    {
      return Convert.ToInt64(this, provider);
    }

    [__DynamicallyInvokable]
    ulong IConvertible.ToUInt64(IFormatProvider provider)
    {
      return Convert.ToUInt64(this, provider);
    }

    [__DynamicallyInvokable]
    float IConvertible.ToSingle(IFormatProvider provider)
    {
      return Convert.ToSingle(this, provider);
    }

    [__DynamicallyInvokable]
    double IConvertible.ToDouble(IFormatProvider provider)
    {
      return Convert.ToDouble(this, provider);
    }

    [__DynamicallyInvokable]
    Decimal IConvertible.ToDecimal(IFormatProvider provider)
    {
      return Convert.ToDecimal(this, provider);
    }

    [__DynamicallyInvokable]
    DateTime IConvertible.ToDateTime(IFormatProvider provider)
    {
      return Convert.ToDateTime(this, provider);
    }

    [__DynamicallyInvokable]
    object IConvertible.ToType(Type type, IFormatProvider provider)
    {
      return Convert.DefaultToType((IConvertible) this, type, provider);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal extern bool IsFastSort();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal extern bool IsAscii();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal extern void SetTrailByte(byte data);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal extern bool TryGetTrailByte(out byte data);

    /// <summary>
    ///   Извлекает объект, который может выполнять итерацию отдельных знаков данной строки.
    /// </summary>
    /// <returns>Объект перечислителя.</returns>
    public CharEnumerator GetEnumerator()
    {
      return new CharEnumerator(this);
    }

    [__DynamicallyInvokable]
    IEnumerator<char> IEnumerable<char>.GetEnumerator()
    {
      return (IEnumerator<char>) new CharEnumerator(this);
    }

    [__DynamicallyInvokable]
    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) new CharEnumerator(this);
    }

    [SecurityCritical]
    internal static unsafe void InternalCopy(string src, IntPtr dest, int len)
    {
      if (len == 0)
        return;
      fixed (char* chPtr = &src.m_firstChar)
        Buffer.Memcpy((byte*) (void*) dest, (byte*) chPtr, len);
    }
  }
}
