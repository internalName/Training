// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.UnmanagedMarshal
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Reflection.Emit
{
  /// <summary>
  ///   Представляет класс, описывающий способ маршалинга поля из управляемого в неуправляемый код.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  [Obsolete("An alternate API is available: Emit the MarshalAs custom attribute instead. http://go.microsoft.com/fwlink/?linkid=14202")]
  [Serializable]
  [HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
  public sealed class UnmanagedMarshal
  {
    internal UnmanagedType m_unmanagedType;
    internal Guid m_guid;
    internal int m_numElem;
    internal UnmanagedType m_baseType;

    /// <summary>
    ///   Указывает заданный тип, которую следует маршалировать в неуправляемый код.
    /// </summary>
    /// <param name="unmanagedType">
    ///   Неуправляемый тип, в который тип — для маршалинга.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.Emit.UnmanagedMarshal" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Аргумент не является простой собственный тип.
    /// </exception>
    public static UnmanagedMarshal DefineUnmanagedMarshal(UnmanagedType unmanagedType)
    {
      if (unmanagedType == UnmanagedType.ByValTStr || unmanagedType == UnmanagedType.SafeArray || (unmanagedType == UnmanagedType.CustomMarshaler || unmanagedType == UnmanagedType.ByValArray) || unmanagedType == UnmanagedType.LPArray)
        throw new ArgumentException(Environment.GetResourceString("Argument_NotASimpleNativeType"));
      return new UnmanagedMarshal(unmanagedType, Guid.Empty, 0, (UnmanagedType) 0);
    }

    /// <summary>
    ///   Задает строку в фиксированном буфере массива (ByValTStr) для маршалинга в неуправляемый код.
    /// </summary>
    /// <param name="elemCount">
    ///   Число элементов в фиксированном буфере массива.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.Emit.UnmanagedMarshal" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Аргумент не является простой собственный тип.
    /// </exception>
    public static UnmanagedMarshal DefineByValTStr(int elemCount)
    {
      return new UnmanagedMarshal(UnmanagedType.ByValTStr, Guid.Empty, elemCount, (UnmanagedType) 0);
    }

    /// <summary>
    ///   Указывает <see langword="SafeArray" /> для маршалинга в неуправляемый код.
    /// </summary>
    /// <param name="elemType">
    ///   Базовый тип или <see langword="UnmanagedType" /> каждого элемента массива.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.Emit.UnmanagedMarshal" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Аргумент не является простой собственный тип.
    /// </exception>
    public static UnmanagedMarshal DefineSafeArray(UnmanagedType elemType)
    {
      return new UnmanagedMarshal(UnmanagedType.SafeArray, Guid.Empty, 0, elemType);
    }

    /// <summary>
    ///   Указывает массив фиксированной длины (ByValArray) для маршалинга в неуправляемый код.
    /// </summary>
    /// <param name="elemCount">
    ///   Число элементов в массиве фиксированной длины.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.Emit.UnmanagedMarshal" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Аргумент не является простой собственный тип.
    /// </exception>
    public static UnmanagedMarshal DefineByValArray(int elemCount)
    {
      return new UnmanagedMarshal(UnmanagedType.ByValArray, Guid.Empty, elemCount, (UnmanagedType) 0);
    }

    /// <summary>
    ///   Указывает <see langword="LPArray" /> для маршалинга в неуправляемый код.
    ///    Длина <see langword="LPArray" /> определяется размер массива фактическое маршалируется во время выполнения.
    /// </summary>
    /// <param name="elemType">
    ///   Неуправляемый тип для маршалинга массива.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.Emit.UnmanagedMarshal" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Аргумент не является простой собственный тип.
    /// </exception>
    public static UnmanagedMarshal DefineLPArray(UnmanagedType elemType)
    {
      return new UnmanagedMarshal(UnmanagedType.LPArray, Guid.Empty, 0, elemType);
    }

    /// <summary>
    ///   Указывает неуправляемый тип.
    ///    Это свойство доступно только для чтения.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Runtime.InteropServices.UnmanagedType" />.
    /// </returns>
    public UnmanagedType GetUnmanagedType
    {
      get
      {
        return this.m_unmanagedType;
      }
    }

    /// <summary>
    ///   Возвращает идентификатор GUID.
    ///    Это свойство доступно только для чтения.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Guid" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Аргумент не является пользовательским упаковщиком.
    /// </exception>
    public Guid IIDGuid
    {
      get
      {
        if (this.m_unmanagedType == UnmanagedType.CustomMarshaler)
          return this.m_guid;
        throw new ArgumentException(Environment.GetResourceString("Argument_NotACustomMarshaler"));
      }
    }

    /// <summary>
    ///   Получает номер элемента.
    ///    Это свойство доступно только для чтения.
    /// </summary>
    /// <returns>Целое число, представляющее количество элементов.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Аргумент не является неуправляемым счетчиком элементов.
    /// </exception>
    public int ElementCount
    {
      get
      {
        if (this.m_unmanagedType != UnmanagedType.ByValArray && this.m_unmanagedType != UnmanagedType.ByValTStr)
          throw new ArgumentException(Environment.GetResourceString("Argument_NoUnmanagedElementCount"));
        return this.m_numElem;
      }
    }

    /// <summary>
    ///   Получает неуправляемый базовый тип.
    ///    Это свойство доступно только для чтения.
    /// </summary>
    /// <returns>
    ///   Объект <see langword="UnmanagedType" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Неуправляемый тип не <see langword="LPArray" /> или <see langword="SafeArray" />.
    /// </exception>
    public UnmanagedType BaseType
    {
      get
      {
        if (this.m_unmanagedType != UnmanagedType.LPArray && this.m_unmanagedType != UnmanagedType.SafeArray)
          throw new ArgumentException(Environment.GetResourceString("Argument_NoNestedMarshal"));
        return this.m_baseType;
      }
    }

    private UnmanagedMarshal(UnmanagedType unmanagedType, Guid guid, int numElem, UnmanagedType type)
    {
      this.m_unmanagedType = unmanagedType;
      this.m_guid = guid;
      this.m_numElem = numElem;
      this.m_baseType = type;
    }

    internal byte[] InternalGetBytes()
    {
      if (this.m_unmanagedType == UnmanagedType.SafeArray || this.m_unmanagedType == UnmanagedType.LPArray)
        return new byte[2]
        {
          (byte) this.m_unmanagedType,
          (byte) this.m_baseType
        };
      if (this.m_unmanagedType == UnmanagedType.ByValArray || this.m_unmanagedType == UnmanagedType.ByValTStr)
      {
        int num1 = 0;
        byte[] numArray1 = new byte[(this.m_numElem > (int) sbyte.MaxValue ? (this.m_numElem > 16383 ? 4 : 2) : 1) + 1];
        byte[] numArray2 = numArray1;
        int index1 = num1;
        int num2 = index1 + 1;
        int unmanagedType = (int) (byte) this.m_unmanagedType;
        numArray2[index1] = (byte) unmanagedType;
        int num3;
        if (this.m_numElem <= (int) sbyte.MaxValue)
        {
          byte[] numArray3 = numArray1;
          int index2 = num2;
          num3 = index2 + 1;
          int num4 = (int) (byte) (this.m_numElem & (int) byte.MaxValue);
          numArray3[index2] = (byte) num4;
        }
        else if (this.m_numElem <= 16383)
        {
          byte[] numArray3 = numArray1;
          int index2 = num2;
          int num4 = index2 + 1;
          int num5 = (int) (byte) (this.m_numElem >> 8 | 128);
          numArray3[index2] = (byte) num5;
          byte[] numArray4 = numArray1;
          int index3 = num4;
          num3 = index3 + 1;
          int num6 = (int) (byte) (this.m_numElem & (int) byte.MaxValue);
          numArray4[index3] = (byte) num6;
        }
        else if (this.m_numElem <= 536870911)
        {
          byte[] numArray3 = numArray1;
          int index2 = num2;
          int num4 = index2 + 1;
          int num5 = (int) (byte) (this.m_numElem >> 24 | 192);
          numArray3[index2] = (byte) num5;
          byte[] numArray4 = numArray1;
          int index3 = num4;
          int num6 = index3 + 1;
          int num7 = (int) (byte) (this.m_numElem >> 16 & (int) byte.MaxValue);
          numArray4[index3] = (byte) num7;
          byte[] numArray5 = numArray1;
          int index4 = num6;
          int num8 = index4 + 1;
          int num9 = (int) (byte) (this.m_numElem >> 8 & (int) byte.MaxValue);
          numArray5[index4] = (byte) num9;
          byte[] numArray6 = numArray1;
          int index5 = num8;
          num3 = index5 + 1;
          int num10 = (int) (byte) (this.m_numElem & (int) byte.MaxValue);
          numArray6[index5] = (byte) num10;
        }
        return numArray1;
      }
      return new byte[1]{ (byte) this.m_unmanagedType };
    }
  }
}
