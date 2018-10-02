// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.Marshal
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices.ComTypes;
using System.Security;
using System.Threading;

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Предоставляет коллекцию методов для выделения неуправляемой памяти, копирования блоков неуправляемой памяти и преобразования управляемых типов в неуправляемые, а также прочих разнообразных методов, используемых при взаимодействии с неуправляемым кодом.
  /// </summary>
  [__DynamicallyInvokable]
  public static class Marshal
  {
    private static Guid IID_IUnknown = new Guid("00000000-0000-0000-C000-000000000046");
    /// <summary>
    ///   Представляет используемый по умолчанию размер символа в системе. По умолчанию для систем Юникода задается значение 2, а для систем ANSI значение 1.
    ///    Это поле доступно только для чтения.
    /// </summary>
    public static readonly int SystemDefaultCharSize = 2;
    /// <summary>
    ///   Представляет наибольший размер набора двухбайтовых символов (DBCS) в байтах для текущей операционной системы.
    ///    Это поле доступно только для чтения.
    /// </summary>
    public static readonly int SystemMaxDBCSCharSize = Marshal.GetSystemMaxDBCSCharSize();
    internal static readonly Guid ManagedNameGuid = new Guid("{0F21F359-AB84-41E8-9A78-36D110E6D2F9}");
    private const int LMEM_FIXED = 0;
    private const int LMEM_MOVEABLE = 2;
    private const long HIWORDMASK = -65536;
    private const string s_strConvertedTypeInfoAssemblyName = "InteropDynamicTypes";
    private const string s_strConvertedTypeInfoAssemblyTitle = "Interop Dynamic Types";
    private const string s_strConvertedTypeInfoAssemblyDesc = "Type dynamically generated from ITypeInfo's";
    private const string s_strConvertedTypeInfoNameSpace = "InteropDynamicTypes";

    private static bool IsWin32Atom(IntPtr ptr)
    {
      return ((long) ptr & -65536L) == 0L;
    }

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    private static bool IsNotWin32Atom(IntPtr ptr)
    {
      return ((ulong) (long) ptr & 18446744073709486080UL) > 0UL;
    }

    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern int GetSystemMaxDBCSCharSize();

    /// <summary>
    ///   Копирует все символы вплоть до первого пустого из неуправляемой ANSI строка в управляемый <see cref="T:System.String" />, и преобразует каждый символ ANSI в Юникод.
    /// </summary>
    /// <param name="ptr">
    ///   Адрес первого символа в неуправляемой строке.
    /// </param>
    /// <returns>
    ///   Управляемая строка, содержащая копию неуправляемой строки ANSI.
    ///    Если <paramref name="ptr" /> является <see langword="null" />, метод возвращает пустую строку.
    /// </returns>
    [SecurityCritical]
    public static unsafe string PtrToStringAnsi(IntPtr ptr)
    {
      if (IntPtr.Zero == ptr)
        return (string) null;
      if (Marshal.IsWin32Atom(ptr))
        return (string) null;
      if (Win32Native.lstrlenA(ptr) == 0)
        return string.Empty;
      return new string((sbyte*) (void*) ptr);
    }

    /// <summary>
    ///   Выделяет управляемый <see cref="T:System.String" />, копирующую указанное количество символов из неуправляемой строки ANSI и преобразует каждый символ ANSI в Юникод.
    /// </summary>
    /// <param name="ptr">
    ///   Адрес первого символа в неуправляемой строке.
    /// </param>
    /// <param name="len">
    ///   Копируемое количество байтов исходной строки.
    /// </param>
    /// <returns>
    ///   Управляемая строка, содержащая копию собственного ANSI строки, если значение <paramref name="ptr" /> параметр не <see langword="null" />; в противном случае, этот метод возвращает <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Значение параметра <paramref name="len" /> меньше нуля.
    /// </exception>
    [SecurityCritical]
    public static unsafe string PtrToStringAnsi(IntPtr ptr, int len)
    {
      if (ptr == IntPtr.Zero)
        throw new ArgumentNullException(nameof (ptr));
      if (len < 0)
        throw new ArgumentException(nameof (len));
      return new string((sbyte*) (void*) ptr, 0, len);
    }

    /// <summary>
    ///   Выделяет управляемый <see cref="T:System.String" /> и копирует указанное количество символов из неуправляемой строки Юникода в него.
    /// </summary>
    /// <param name="ptr">
    ///   Адрес первого символа в неуправляемой строке.
    /// </param>
    /// <param name="len">Число копируемых символов Юникода.</param>
    /// <returns>
    ///   Управляемая строка, содержащая копию неуправляемой строки, если значение параметра <paramref name="ptr" /> не равно <see langword="null" />. В противном случае этот метод возвращает значение <see langword="null" />.
    /// </returns>
    [SecurityCritical]
    public static unsafe string PtrToStringUni(IntPtr ptr, int len)
    {
      if (ptr == IntPtr.Zero)
        throw new ArgumentNullException(nameof (ptr));
      if (len < 0)
        throw new ArgumentException(nameof (len));
      return new string((char*) (void*) ptr, 0, len);
    }

    /// <summary>
    ///   Выделяет управляемый <see cref="T:System.String" /> и копирует указанное количество символов из строки, хранящейся в неуправляемой памяти в него.
    /// </summary>
    /// <param name="ptr">
    ///   Для платформ Юникода это адрес первого символа Юникода.
    /// 
    ///   -или-
    /// 
    ///   Для платформ ANSI это адрес первого символа ANSI.
    /// </param>
    /// <param name="len">Число символов для копирования.</param>
    /// <returns>
    ///   Управляемая строка, содержащая копию собственной строки, если значение <paramref name="ptr" /> параметр не <see langword="null" />; в противном случае, этот метод возвращает <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Значение параметра <paramref name="len" /> меньше нуля.
    /// </exception>
    [SecurityCritical]
    public static string PtrToStringAuto(IntPtr ptr, int len)
    {
      return Marshal.PtrToStringUni(ptr, len);
    }

    /// <summary>
    ///   Выделяет управляемый <see cref="T:System.String" /> и копирует в него все знаки до первого пустого из неуправляемой строки Юникода.
    /// </summary>
    /// <param name="ptr">
    ///   Адрес первого символа в неуправляемой строке.
    /// </param>
    /// <returns>
    ///   Управляемая строка, содержащая копию неуправляемой строки, если значение параметра <paramref name="ptr" /> не равно <see langword="null" />. В противном случае этот метод возвращает значение <see langword="null" />.
    /// </returns>
    [SecurityCritical]
    public static unsafe string PtrToStringUni(IntPtr ptr)
    {
      if (IntPtr.Zero == ptr)
        return (string) null;
      if (Marshal.IsWin32Atom(ptr))
        return (string) null;
      return new string((char*) (void*) ptr);
    }

    /// <summary>
    ///   Выделяет управляемый <see cref="T:System.String" /> и копирует все символы вплоть до первого пустого из строки, хранящейся в неуправляемой памяти в него.
    /// </summary>
    /// <param name="ptr">
    ///   Для платформ Юникода это адрес первого символа Юникода.
    /// 
    ///   -или-
    /// 
    ///   Для платформ ANSI это адрес первого символа ANSI.
    /// </param>
    /// <returns>
    ///   Управляемая строка, содержащая копию неуправляемой строки, если значение параметра <paramref name="ptr" /> не равно <see langword="null" />. В противном случае этот метод возвращает значение <see langword="null" />.
    /// </returns>
    [SecurityCritical]
    public static string PtrToStringAuto(IntPtr ptr)
    {
      return Marshal.PtrToStringUni(ptr);
    }

    /// <summary>Возвращает неуправляемый размер объекта в байтах.</summary>
    /// <param name="structure">
    ///   Объект, размер которого возвращается.
    /// </param>
    /// <returns>Размер указанного объекта в неуправляемом коде.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="structure" /> имеет значение <see langword="null" />.
    /// </exception>
    [ComVisible(true)]
    public static int SizeOf(object structure)
    {
      if (structure == null)
        throw new ArgumentNullException(nameof (structure));
      return Marshal.SizeOfHelper(structure.GetType(), true);
    }

    /// <summary>
    ///   [Поддерживается в .NET Framework 4.5.1 и более поздних версиях.]
    /// 
    ///   Возвращает неуправляемый размер объекта указанного типа в байтах.
    /// </summary>
    /// <param name="structure">
    ///   Объект, размер которого возвращается.
    /// </param>
    /// <typeparam name="T">
    ///   Тип параметра <paramref name="structure" />.
    /// </typeparam>
    /// <returns>
    ///   Размер в байтах указанного объекта в неуправляемом коде.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="structure" /> имеет значение <see langword="null" />.
    /// </exception>
    public static int SizeOf<T>(T structure)
    {
      return Marshal.SizeOf((object) structure);
    }

    /// <summary>Возвращает размер неуправляемого типа в байтах.</summary>
    /// <param name="t">Тип, размер которого возвращается.</param>
    /// <returns>Размер указанного типа в неуправляемом коде.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="t" /> имеет универсальный тип.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="t" /> имеет значение <see langword="null" />.
    /// </exception>
    public static int SizeOf(Type t)
    {
      if (t == (Type) null)
        throw new ArgumentNullException(nameof (t));
      if ((object) (t as RuntimeType) == null)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"), nameof (t));
      if (t.IsGenericType)
        throw new ArgumentException(Environment.GetResourceString("Argument_NeedNonGenericType"), nameof (t));
      return Marshal.SizeOfHelper(t, true);
    }

    /// <summary>
    ///   [Поддерживается в .NET Framework 4.5.1 и более поздних версиях.]
    /// 
    ///   Возвращает размер неуправляемого типа в байтах.
    /// </summary>
    /// <typeparam name="T">Тип, размер которого возвращается.</typeparam>
    /// <returns>
    ///   Размер в байтах, который задается параметром типа <paramref name="T" /> параметра универсального типа.
    /// </returns>
    public static int SizeOf<T>()
    {
      return Marshal.SizeOf(typeof (T));
    }

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    internal static uint AlignedSizeOf<T>() where T : struct
    {
      uint num = Marshal.SizeOfType(typeof (T));
      switch (num)
      {
        case 1:
        case 2:
          return num;
        default:
          if (IntPtr.Size == 8 && num == 4U)
            return num;
          return Marshal.AlignedSizeOfType(typeof (T));
      }
    }

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern uint SizeOfType(Type type);

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern uint AlignedSizeOfType(Type type);

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern int SizeOfHelper(Type t, bool throwIfNotMarshalable);

    /// <summary>
    ///   Возвращает смещение поля для неуправляемой формы управляемого класса.
    /// </summary>
    /// <param name="t">
    ///   Тип значения или форматированный ссылочный тип, указывающий управляемый класс.
    ///    Необходимо применить <see cref="T:System.Runtime.InteropServices.StructLayoutAttribute" /> к классу.
    /// </param>
    /// <param name="fieldName">
    ///   Поле внутри <paramref name="t" /> параметр.
    /// </param>
    /// <returns>
    ///   Смещение в байтах для <paramref name="fieldName" /> вызова параметра в указанном классе, объявленном вызовом платформы.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Класс не может быть экспортирован как структура или поле не является открытым.
    ///    Начиная с .NET Framework версии 2.0, это поле может быть закрытым.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="t" /> имеет значение <see langword="null" />.
    /// </exception>
    public static IntPtr OffsetOf(Type t, string fieldName)
    {
      if (t == (Type) null)
        throw new ArgumentNullException(nameof (t));
      FieldInfo field = t.GetField(fieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
      if (field == (FieldInfo) null)
        throw new ArgumentException(Environment.GetResourceString("Argument_OffsetOfFieldNotFound", (object) t.FullName), nameof (fieldName));
      RtFieldInfo rtFieldInfo = field as RtFieldInfo;
      if ((FieldInfo) rtFieldInfo == (FieldInfo) null)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeFieldInfo"), nameof (fieldName));
      return Marshal.OffsetOfHelper((IRuntimeFieldInfo) rtFieldInfo);
    }

    /// <summary>
    ///   [Поддерживается в .NET Framework 4.5.1 и более поздних версиях.]
    /// 
    ///   Возвращает смещение поля для неуправляемой формы указанного управляемого класса.
    /// </summary>
    /// <param name="fieldName">
    ///   Имя поля в типе <paramref name="T" />.
    /// </param>
    /// <typeparam name="T">
    ///   Управляемый тип значения или форматированный ссылочный тип.
    ///    Примените атрибут <see cref="T:System.Runtime.InteropServices.StructLayoutAttribute" /> к классу.
    /// </typeparam>
    /// <returns>
    ///   Смещение (в байтах) для параметра <paramref name="fieldName" /> в указанном классе, объявленном вызовом платформы.
    /// </returns>
    public static IntPtr OffsetOf<T>(string fieldName)
    {
      return Marshal.OffsetOf(typeof (T), fieldName);
    }

    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern IntPtr OffsetOfHelper(IRuntimeFieldInfo f);

    /// <summary>
    ///   Возвращает адрес элемента по указанному индексу внутри заданного массива.
    /// </summary>
    /// <param name="arr">Массив, содержащий требуемый элемент.</param>
    /// <param name="index">
    ///   Индекс в <paramref name="arr" /> параметр требуемого элемента.
    /// </param>
    /// <returns>
    ///   Адрес <paramref name="index" /> внутри <paramref name="arr" />.
    /// </returns>
    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern IntPtr UnsafeAddrOfPinnedArrayElement(Array arr, int index);

    /// <summary>
    ///   [Поддерживается в .NET Framework 4.5.1 и более поздних версиях.]
    /// 
    ///   Возвращает адрес элемента по указанному индексу в массив указанного типа.
    /// </summary>
    /// <param name="arr">Массив, содержащий требуемый элемент.</param>
    /// <param name="index">
    ///   Индекс необходимого элемента в <paramref name="arr" /> массива.
    /// </param>
    /// <typeparam name="T">Тип массива.</typeparam>
    /// <returns>
    ///   Адрес <paramref name="index" /> в <paramref name="arr" />.
    /// </returns>
    [SecurityCritical]
    public static IntPtr UnsafeAddrOfPinnedArrayElement<T>(T[] arr, int index)
    {
      return Marshal.UnsafeAddrOfPinnedArrayElement((Array) arr, index);
    }

    /// <summary>
    ///   Копирует данные из одномерного управляемого массива 32-битных целых чисел со знаком в указатель неуправляемой памяти.
    /// </summary>
    /// <param name="source">
    ///   Одномерный массив, из которого выполняется копирование.
    /// </param>
    /// <param name="startIndex">
    ///   Отсчитываемый от нуля индекс в исходном массиве, с которого начинается копирование.
    /// </param>
    /// <param name="destination">
    ///   Указатель памяти, в который выполняется копирование.
    /// </param>
    /// <param name="length">Число копируемых элементов массива.</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="startIndex" /> и <paramref name="length" /> являются недопустимыми.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="startIndex" /> или <paramref name="length" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    public static void Copy(int[] source, int startIndex, IntPtr destination, int length)
    {
      Marshal.CopyToNative((object) source, startIndex, destination, length);
    }

    /// <summary>
    ///   Копирует данные из одномерного управляемого массива символов в неуправляемый указатель памяти.
    /// </summary>
    /// <param name="source">
    ///   Одномерный массив, из которого выполняется копирование.
    /// </param>
    /// <param name="startIndex">
    ///   Отсчитываемый от нуля индекс в исходном массиве, с которого начинается копирование.
    /// </param>
    /// <param name="destination">
    ///   Указатель памяти, в который выполняется копирование.
    /// </param>
    /// <param name="length">Число копируемых элементов массива.</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="startIndex" /> и <paramref name="length" /> являются недопустимыми.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="startIndex" />, <paramref name="destination" /> или <paramref name="length" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    public static void Copy(char[] source, int startIndex, IntPtr destination, int length)
    {
      Marshal.CopyToNative((object) source, startIndex, destination, length);
    }

    /// <summary>
    ///   Копирует данные из одномерного управляемого массива 16-битных целых чисел со знаком в указатель неуправляемой памяти.
    /// </summary>
    /// <param name="source">
    ///   Одномерный массив, из которого выполняется копирование.
    /// </param>
    /// <param name="startIndex">
    ///   Отсчитываемый от нуля индекс в исходном массиве, с которого начинается копирование.
    /// </param>
    /// <param name="destination">
    ///   Указатель памяти, в который выполняется копирование.
    /// </param>
    /// <param name="length">Число копируемых элементов массива.</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="startIndex" /> и <paramref name="length" /> являются недопустимыми.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="source" />, <paramref name="startIndex" />, <paramref name="destination" /> или <paramref name="length" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    public static void Copy(short[] source, int startIndex, IntPtr destination, int length)
    {
      Marshal.CopyToNative((object) source, startIndex, destination, length);
    }

    /// <summary>
    ///   Копирует данные из одномерного управляемого массива 64-битных целых чисел со знаком в указатель неуправляемой памяти.
    /// </summary>
    /// <param name="source">
    ///   Одномерный массив, из которого выполняется копирование.
    /// </param>
    /// <param name="startIndex">
    ///   Отсчитываемый от нуля индекс в исходном массиве, с которого начинается копирование.
    /// </param>
    /// <param name="destination">
    ///   Указатель памяти, в который выполняется копирование.
    /// </param>
    /// <param name="length">Число копируемых элементов массива.</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="startIndex" /> и <paramref name="length" /> являются недопустимыми.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="source" />, <paramref name="startIndex" />, <paramref name="destination" /> или <paramref name="length" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    public static void Copy(long[] source, int startIndex, IntPtr destination, int length)
    {
      Marshal.CopyToNative((object) source, startIndex, destination, length);
    }

    /// <summary>
    ///   Копирует данные из одномерного управляемого массива чисел с плавающей запятой одинарной точности в указатель неуправляемой памяти.
    /// </summary>
    /// <param name="source">
    ///   Одномерный массив, из которого выполняется копирование.
    /// </param>
    /// <param name="startIndex">
    ///   Отсчитываемый от нуля индекс в исходном массиве, с которого начинается копирование.
    /// </param>
    /// <param name="destination">
    ///   Указатель памяти, в который выполняется копирование.
    /// </param>
    /// <param name="length">Число копируемых элементов массива.</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="startIndex" /> и <paramref name="length" /> являются недопустимыми.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="source" />, <paramref name="startIndex" />, <paramref name="destination" /> или <paramref name="length" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    public static void Copy(float[] source, int startIndex, IntPtr destination, int length)
    {
      Marshal.CopyToNative((object) source, startIndex, destination, length);
    }

    /// <summary>
    ///   Копирует данные из одномерного управляемого массива чисел с плавающей запятой двойной точности в указатель неуправляемой памяти.
    /// </summary>
    /// <param name="source">
    ///   Одномерный массив, из которого выполняется копирование.
    /// </param>
    /// <param name="startIndex">
    ///   Отсчитываемый от нуля индекс в исходном массиве, с которого начинается копирование.
    /// </param>
    /// <param name="destination">
    ///   Указатель памяти, в который выполняется копирование.
    /// </param>
    /// <param name="length">Число копируемых элементов массива.</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="startIndex" /> и <paramref name="length" /> являются недопустимыми.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="source" />, <paramref name="startIndex" />, <paramref name="destination" /> или <paramref name="length" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    public static void Copy(double[] source, int startIndex, IntPtr destination, int length)
    {
      Marshal.CopyToNative((object) source, startIndex, destination, length);
    }

    /// <summary>
    ///   Копирует данные из одномерного управляемого массива 8-битных целых чисел без знака в указатель неуправляемой памяти.
    /// </summary>
    /// <param name="source">
    ///   Одномерный массив, из которого выполняется копирование.
    /// </param>
    /// <param name="startIndex">
    ///   Отсчитываемый от нуля индекс в исходном массиве, с которого начинается копирование.
    /// </param>
    /// <param name="destination">
    ///   Указатель памяти, в который выполняется копирование.
    /// </param>
    /// <param name="length">Число копируемых элементов массива.</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="startIndex" /> и <paramref name="length" /> являются недопустимыми.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="source" />, <paramref name="startIndex" />, <paramref name="destination" /> или <paramref name="length" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    public static void Copy(byte[] source, int startIndex, IntPtr destination, int length)
    {
      Marshal.CopyToNative((object) source, startIndex, destination, length);
    }

    /// <summary>
    ///   Копирует данные из одномерного управляемого массива <see cref="T:System.IntPtr" /> в неуправляемый указатель памяти.
    /// </summary>
    /// <param name="source">
    ///   Одномерный массив, из которого выполняется копирование.
    /// </param>
    /// <param name="startIndex">
    ///   Отсчитываемый от нуля индекс в исходном массиве, с которого начинается копирование.
    /// </param>
    /// <param name="destination">
    ///   Указатель памяти, в который выполняется копирование.
    /// </param>
    /// <param name="length">Число копируемых элементов массива.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="source" />, <paramref name="destination" />, <paramref name="startIndex" /> или <paramref name="length" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    public static void Copy(IntPtr[] source, int startIndex, IntPtr destination, int length)
    {
      Marshal.CopyToNative((object) source, startIndex, destination, length);
    }

    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void CopyToNative(object source, int startIndex, IntPtr destination, int length);

    /// <summary>
    ///   Копирует данные из указателя неуправляемой памяти в одномерный управляемый массив 32-битных целых чисел со знаком.
    /// </summary>
    /// <param name="source">
    ///   Указатель памяти, из которого выполняется копирование.
    /// </param>
    /// <param name="destination">Массив для копирования данных.</param>
    /// <param name="startIndex">
    ///   Отсчитываемый от нуля индекс в массиве назначения, с которого начинается копирование.
    /// </param>
    /// <param name="length">Число копируемых элементов массива.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="source" />, <paramref name="destination" />, <paramref name="startIndex" /> или <paramref name="length" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    public static void Copy(IntPtr source, int[] destination, int startIndex, int length)
    {
      Marshal.CopyToManaged(source, (object) destination, startIndex, length);
    }

    /// <summary>
    ///   Копирует данные из указателя неуправляемой памяти в управляемый массив символов.
    /// </summary>
    /// <param name="source">
    ///   Указатель памяти, из которого выполняется копирование.
    /// </param>
    /// <param name="destination">Массив для копирования данных.</param>
    /// <param name="startIndex">
    ///   Отсчитываемый от нуля индекс в массиве назначения, с которого начинается копирование.
    /// </param>
    /// <param name="length">Число копируемых элементов массива.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="source" />, <paramref name="destination" />, <paramref name="startIndex" /> или <paramref name="length" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    public static void Copy(IntPtr source, char[] destination, int startIndex, int length)
    {
      Marshal.CopyToManaged(source, (object) destination, startIndex, length);
    }

    /// <summary>
    ///   Копирует данные из указателя неуправляемой памяти в одномерный управляемый массив 16-битных целых чисел со знаком.
    /// </summary>
    /// <param name="source">
    ///   Указатель памяти, из которого выполняется копирование.
    /// </param>
    /// <param name="destination">Массив для копирования данных.</param>
    /// <param name="startIndex">
    ///   Отсчитываемый от нуля индекс в массиве назначения, с которого начинается копирование.
    /// </param>
    /// <param name="length">Число копируемых элементов массива.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="source" />, <paramref name="destination" />, <paramref name="startIndex" /> или <paramref name="length" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    public static void Copy(IntPtr source, short[] destination, int startIndex, int length)
    {
      Marshal.CopyToManaged(source, (object) destination, startIndex, length);
    }

    /// <summary>
    ///   Копирует данные из указателя неуправляемой памяти в одномерный управляемый массив 64-битных целых чисел со знаком.
    /// </summary>
    /// <param name="source">
    ///   Указатель памяти, из которого выполняется копирование.
    /// </param>
    /// <param name="destination">Массив для копирования данных.</param>
    /// <param name="startIndex">
    ///   Отсчитываемый от нуля индекс в массиве назначения, с которого начинается копирование.
    /// </param>
    /// <param name="length">Число копируемых элементов массива.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="source" />, <paramref name="destination" />, <paramref name="startIndex" /> или <paramref name="length" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    public static void Copy(IntPtr source, long[] destination, int startIndex, int length)
    {
      Marshal.CopyToManaged(source, (object) destination, startIndex, length);
    }

    /// <summary>
    ///   Копирует данные из указателя неуправляемой памяти в управляемый массив чисел с плавающей запятой одиночной точности.
    /// </summary>
    /// <param name="source">
    ///   Указатель памяти, из которого выполняется копирование.
    /// </param>
    /// <param name="destination">Массив для копирования данных.</param>
    /// <param name="startIndex">
    ///   Отсчитываемый от нуля индекс в массиве назначения, с которого начинается копирование.
    /// </param>
    /// <param name="length">Число копируемых элементов массива.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="source" />, <paramref name="destination" />, <paramref name="startIndex" /> или <paramref name="length" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    public static void Copy(IntPtr source, float[] destination, int startIndex, int length)
    {
      Marshal.CopyToManaged(source, (object) destination, startIndex, length);
    }

    /// <summary>
    ///   Копирует данные из неуправляемого указателя памяти в управляемый массив чисел с плавающей запятой двойной точности.
    /// </summary>
    /// <param name="source">
    ///   Указатель памяти, из которого выполняется копирование.
    /// </param>
    /// <param name="destination">Массив для копирования данных.</param>
    /// <param name="startIndex">
    ///   Отсчитываемый от нуля индекс в массиве назначения, с которого начинается копирование.
    /// </param>
    /// <param name="length">Число копируемых элементов массива.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="source" />, <paramref name="destination" />, <paramref name="startIndex" /> или <paramref name="length" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    public static void Copy(IntPtr source, double[] destination, int startIndex, int length)
    {
      Marshal.CopyToManaged(source, (object) destination, startIndex, length);
    }

    /// <summary>
    ///   Копирует данные из указателя неуправляемой памяти в одномерный управляемый массив 8-битных целых чисел без знака.
    /// </summary>
    /// <param name="source">
    ///   Указатель памяти, из которого выполняется копирование.
    /// </param>
    /// <param name="destination">Массив для копирования данных.</param>
    /// <param name="startIndex">
    ///   Отсчитываемый от нуля индекс в массиве назначения, с которого начинается копирование.
    /// </param>
    /// <param name="length">Число копируемых элементов массива.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="source" />, <paramref name="destination" />, <paramref name="startIndex" /> или <paramref name="length" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    public static void Copy(IntPtr source, byte[] destination, int startIndex, int length)
    {
      Marshal.CopyToManaged(source, (object) destination, startIndex, length);
    }

    /// <summary>
    ///   Копирует данные из указателя неуправляемой памяти в управляемый массив <see cref="T:System.IntPtr" />.
    /// </summary>
    /// <param name="source">
    ///   Указатель памяти, из которого выполняется копирование.
    /// </param>
    /// <param name="destination">Массив для копирования данных.</param>
    /// <param name="startIndex">
    ///   Отсчитываемый от нуля индекс в массиве назначения, с которого начинается копирование.
    /// </param>
    /// <param name="length">Число копируемых элементов массива.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="source" />, <paramref name="destination" />, <paramref name="startIndex" /> или <paramref name="length" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    public static void Copy(IntPtr source, IntPtr[] destination, int startIndex, int length)
    {
      Marshal.CopyToManaged(source, (object) destination, startIndex, length);
    }

    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void CopyToManaged(IntPtr source, object destination, int startIndex, int length);

    /// <summary>
    ///   Считывает один байт с указанным смещением (или индексом) из неуправляемой памяти.
    /// </summary>
    /// <param name="ptr">
    ///   Базовый адрес в неуправляемой памяти исходного объекта.
    /// </param>
    /// <param name="ofs">
    ///   Дополнительное смещение в байтах, который добавляется в <paramref name="ptr" /> перед чтением.
    /// </param>
    /// <returns>
    ///   Байт, считываемый из неуправляемой памяти с указанным смещением.
    /// </returns>
    /// <exception cref="T:System.AccessViolationException">
    ///   Базовый адрес (<paramref name="ptr" />), а также смещение байтов (<paramref name="ofs" />) дает значение null или недопустимый адрес.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="ptr" /> является объектом <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" />.
    ///    Этот метод не принимает <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> параметров.
    /// </exception>
    [SuppressUnmanagedCodeSecurity]
    [SecurityCritical]
    [DllImport("mscoree.dll", EntryPoint = "ND_RU1")]
    public static extern byte ReadByte([MarshalAs(UnmanagedType.AsAny), In] object ptr, int ofs);

    /// <summary>
    ///   Считывает один байт с указанным смещением (или индексом) из неуправляемой памяти.
    /// </summary>
    /// <param name="ptr">
    ///   Базовый адрес в неуправляемой памяти, относительно которого выполняется чтение.
    /// </param>
    /// <param name="ofs">
    ///   Дополнительное смещение в байтах, который добавляется в <paramref name="ptr" /> перед чтением.
    /// </param>
    /// <returns>
    ///   Байт, считываемый из неуправляемой памяти с указанным смещением.
    /// </returns>
    /// <exception cref="T:System.AccessViolationException">
    ///   Базовый адрес (<paramref name="ptr" />), а также смещение байтов (<paramref name="ofs" />) дает значение null или недопустимый адрес.
    /// </exception>
    [SecurityCritical]
    public static unsafe byte ReadByte(IntPtr ptr, int ofs)
    {
      try
      {
        return *(byte*) ((IntPtr) (void*) ptr + ofs);
      }
      catch (NullReferenceException ex)
      {
        throw new AccessViolationException();
      }
    }

    /// <summary>Считывает один байт из неуправляемой памяти.</summary>
    /// <param name="ptr">
    ///   Адрес неуправляемой памяти, откуда производится чтение.
    /// </param>
    /// <returns>Байт, считанный из неуправляемой памяти.</returns>
    /// <exception cref="T:System.AccessViolationException">
    ///   <paramref name="ptr" /> не является распознаваемым форматом.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="ptr" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="ptr" /> недопустим.
    /// </exception>
    [SecurityCritical]
    public static byte ReadByte(IntPtr ptr)
    {
      return Marshal.ReadByte(ptr, 0);
    }

    /// <summary>
    ///   Считывает из неуправляемой памяти с указанным смещением 16-битное целое число со знаком.
    /// </summary>
    /// <param name="ptr">
    ///   Базовый адрес в неуправляемой памяти исходного объекта.
    /// </param>
    /// <param name="ofs">
    ///   Дополнительное смещение в байтах, который добавляется в <paramref name="ptr" /> перед чтением.
    /// </param>
    /// <returns>
    ///   16-битное целое число со знаком, считанное из неуправляемой памяти с указанным смещением.
    /// </returns>
    /// <exception cref="T:System.AccessViolationException">
    ///   Базовый адрес (<paramref name="ptr" />), а также смещение байтов (<paramref name="ofs" />) дает значение null или недопустимый адрес.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="ptr" /> является объектом <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" />.
    ///    Этот метод не принимает <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> параметров.
    /// </exception>
    [SuppressUnmanagedCodeSecurity]
    [SecurityCritical]
    [DllImport("mscoree.dll", EntryPoint = "ND_RI2")]
    public static extern short ReadInt16([MarshalAs(UnmanagedType.AsAny), In] object ptr, int ofs);

    /// <summary>
    ///   Считывает из неуправляемой памяти с указанным смещением 16-битное целое число со знаком.
    /// </summary>
    /// <param name="ptr">
    ///   Базовый адрес в неуправляемой памяти, относительно которого выполняется чтение.
    /// </param>
    /// <param name="ofs">
    ///   Дополнительное смещение в байтах, который добавляется в <paramref name="ptr" /> перед чтением.
    /// </param>
    /// <returns>
    ///   16-битное целое число со знаком, считанное из неуправляемой памяти с указанным смещением.
    /// </returns>
    /// <exception cref="T:System.AccessViolationException">
    ///   Базовый адрес (<paramref name="ptr" />), а также смещение байтов (<paramref name="ofs" />) дает значение null или недопустимый адрес.
    /// </exception>
    [SecurityCritical]
    public static unsafe short ReadInt16(IntPtr ptr, int ofs)
    {
      try
      {
        byte* numPtr1 = (byte*) ((IntPtr) (void*) ptr + ofs);
        if (((int) numPtr1 & 1) == 0)
          return *(short*) numPtr1;
        short num;
        byte* numPtr2 = (byte*) &num;
        *numPtr2 = *numPtr1;
        numPtr2[1] = numPtr1[1];
        return num;
      }
      catch (NullReferenceException ex)
      {
        throw new AccessViolationException();
      }
    }

    /// <summary>
    ///   Считывает из неуправляемой памяти 16-битное целое число со знаком.
    /// </summary>
    /// <param name="ptr">
    ///   Адрес неуправляемой памяти, откуда производится чтение.
    /// </param>
    /// <returns>
    ///   16-битное целое число со знаком, считанное из неуправляемой памяти.
    /// </returns>
    /// <exception cref="T:System.AccessViolationException">
    ///   <paramref name="ptr" /> не является распознаваемым форматом.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="ptr" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="ptr" /> недопустим.
    /// </exception>
    [SecurityCritical]
    public static short ReadInt16(IntPtr ptr)
    {
      return Marshal.ReadInt16(ptr, 0);
    }

    /// <summary>
    ///   Считывает из неуправляемой памяти с указанным смещением 32-битное целое число со знаком.
    /// </summary>
    /// <param name="ptr">
    ///   Базовый адрес в неуправляемой памяти исходного объекта.
    /// </param>
    /// <param name="ofs">
    ///   Дополнительное смещение в байтах, который добавляется в <paramref name="ptr" /> перед чтением.
    /// </param>
    /// <returns>
    ///   32-битное целое число со знаком, считанное из неуправляемой памяти с указанным смещением.
    /// </returns>
    /// <exception cref="T:System.AccessViolationException">
    ///   Базовый адрес (<paramref name="ptr" />), а также смещение байтов (<paramref name="ofs" />) дает значение null или недопустимый адрес.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="ptr" /> является объектом <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" />.
    ///    Этот метод не принимает <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> параметров.
    /// </exception>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [SuppressUnmanagedCodeSecurity]
    [SecurityCritical]
    [DllImport("mscoree.dll", EntryPoint = "ND_RI4")]
    public static extern int ReadInt32([MarshalAs(UnmanagedType.AsAny), In] object ptr, int ofs);

    /// <summary>
    ///   Считывает из неуправляемой памяти с указанным смещением 32-битное целое число со знаком.
    /// </summary>
    /// <param name="ptr">
    ///   Базовый адрес в неуправляемой памяти, относительно которого выполняется чтение.
    /// </param>
    /// <param name="ofs">
    ///   Дополнительное смещение в байтах, который добавляется в <paramref name="ptr" /> перед чтением.
    /// </param>
    /// <returns>
    ///   32-битное целое число со знаком, считанное из неуправляемой памяти.
    /// </returns>
    /// <exception cref="T:System.AccessViolationException">
    ///   Базовый адрес (<paramref name="ptr" />), а также смещение байтов (<paramref name="ofs" />) дает значение null или недопустимый адрес.
    /// </exception>
    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    public static unsafe int ReadInt32(IntPtr ptr, int ofs)
    {
      try
      {
        byte* numPtr1 = (byte*) ((IntPtr) (void*) ptr + ofs);
        if (((int) numPtr1 & 3) == 0)
          return *(int*) numPtr1;
        int num;
        byte* numPtr2 = (byte*) &num;
        *numPtr2 = *numPtr1;
        numPtr2[1] = numPtr1[1];
        numPtr2[2] = numPtr1[2];
        numPtr2[3] = numPtr1[3];
        return num;
      }
      catch (NullReferenceException ex)
      {
        throw new AccessViolationException();
      }
    }

    /// <summary>
    ///   Считывает из неуправляемой памяти 32-битное целое число со знаком.
    /// </summary>
    /// <param name="ptr">
    ///   Адрес неуправляемой памяти, откуда производится чтение.
    /// </param>
    /// <returns>
    ///   32-битное целое число со знаком, считанное из неуправляемой памяти.
    /// </returns>
    /// <exception cref="T:System.AccessViolationException">
    ///   <paramref name="ptr" /> не является распознаваемым форматом.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="ptr" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="ptr" /> недопустим.
    /// </exception>
    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    public static int ReadInt32(IntPtr ptr)
    {
      return Marshal.ReadInt32(ptr, 0);
    }

    /// <summary>
    ///   Считывает из неуправляемой памяти целое число, разрядность которого соответствует собственной разрядности процессора.
    /// </summary>
    /// <param name="ptr">
    ///   Базовый адрес в неуправляемой памяти исходного объекта.
    /// </param>
    /// <param name="ofs">
    ///   Дополнительное смещение в байтах, который добавляется в <paramref name="ptr" /> перед чтением.
    /// </param>
    /// <returns>
    ///   Целое число, считываемое из неуправляемой памяти с указанным смещением.
    /// </returns>
    /// <exception cref="T:System.AccessViolationException">
    ///   Базовый адрес (<paramref name="ptr" />), а также смещение байтов (<paramref name="ofs" />) дает значение null или недопустимый адрес.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="ptr" /> является объектом <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" />.
    ///    Этот метод не принимает <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> параметров.
    /// </exception>
    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    public static IntPtr ReadIntPtr([MarshalAs(UnmanagedType.AsAny), In] object ptr, int ofs)
    {
      return (IntPtr) Marshal.ReadInt32(ptr, ofs);
    }

    /// <summary>
    ///   Считывает из неуправляемой памяти с указанным смещением знаковое целое число, разрядность которого соответствует собственной разрядности процессора.
    /// </summary>
    /// <param name="ptr">
    ///   Базовый адрес в неуправляемой памяти, относительно которого выполняется чтение.
    /// </param>
    /// <param name="ofs">
    ///   Дополнительное смещение в байтах, который добавляется в <paramref name="ptr" /> перед чтением.
    /// </param>
    /// <returns>
    ///   Целое число, считываемое из неуправляемой памяти с указанным смещением.
    /// </returns>
    /// <exception cref="T:System.AccessViolationException">
    ///   Базовый адрес (<paramref name="ptr" />), а также смещение байтов (<paramref name="ofs" />) дает значение null или недопустимый адрес.
    /// </exception>
    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    public static IntPtr ReadIntPtr(IntPtr ptr, int ofs)
    {
      return (IntPtr) Marshal.ReadInt32(ptr, ofs);
    }

    /// <summary>
    ///   Считывает из неуправляемой памяти целое число, разрядность которого соответствует собственной разрядности процессора.
    /// </summary>
    /// <param name="ptr">
    ///   Адрес неуправляемой памяти, откуда производится чтение.
    /// </param>
    /// <returns>
    ///   Целое число, считанное из неуправляемой памяти.
    ///    На 32-разрядных компьютерах возвращается 32-битное целое число, а на 64-разрядных компьютерах — 64-битное.
    /// </returns>
    /// <exception cref="T:System.AccessViolationException">
    ///   <paramref name="ptr" /> не является распознаваемым форматом.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="ptr" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="ptr" /> недопустим.
    /// </exception>
    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    public static IntPtr ReadIntPtr(IntPtr ptr)
    {
      return (IntPtr) Marshal.ReadInt32(ptr, 0);
    }

    /// <summary>
    ///   Считывает из неуправляемой памяти с указанным смещением 64-битное целое число со знаком.
    /// </summary>
    /// <param name="ptr">
    ///   Базовый адрес в неуправляемой памяти исходного объекта.
    /// </param>
    /// <param name="ofs">
    ///   Дополнительное смещение в байтах, который добавляется в <paramref name="ptr" /> перед чтением.
    /// </param>
    /// <returns>
    ///   64-битное целое число со знаком, считанное из неуправляемой памяти с указанным смещением.
    /// </returns>
    /// <exception cref="T:System.AccessViolationException">
    ///   Базовый адрес (<paramref name="ptr" />), а также смещение байтов (<paramref name="ofs" />) дает значение null или недопустимый адрес.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="ptr" /> является объектом <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" />.
    ///    Этот метод не принимает <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> параметров.
    /// </exception>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [SuppressUnmanagedCodeSecurity]
    [SecurityCritical]
    [DllImport("mscoree.dll", EntryPoint = "ND_RI8")]
    public static extern long ReadInt64([MarshalAs(UnmanagedType.AsAny), In] object ptr, int ofs);

    /// <summary>
    ///   Считывает из неуправляемой памяти с указанным смещением 64-битное целое число со знаком.
    /// </summary>
    /// <param name="ptr">
    ///   Базовый адрес в неуправляемой памяти, относительно которого выполняется чтение.
    /// </param>
    /// <param name="ofs">
    ///   Дополнительное смещение в байтах, который добавляется в <paramref name="ptr" /> перед чтением.
    /// </param>
    /// <returns>
    ///   64-битное целое число со знаком, считанное из неуправляемой памяти с указанным смещением.
    /// </returns>
    /// <exception cref="T:System.AccessViolationException">
    ///   Базовый адрес (<paramref name="ptr" />), а также смещение байтов (<paramref name="ofs" />) дает значение null или недопустимый адрес.
    /// </exception>
    [SecurityCritical]
    public static unsafe long ReadInt64(IntPtr ptr, int ofs)
    {
      try
      {
        byte* numPtr1 = (byte*) ((IntPtr) (void*) ptr + ofs);
        if (((int) numPtr1 & 7) == 0)
          return *(long*) numPtr1;
        long num;
        byte* numPtr2 = (byte*) &num;
        *numPtr2 = *numPtr1;
        numPtr2[1] = numPtr1[1];
        numPtr2[2] = numPtr1[2];
        numPtr2[3] = numPtr1[3];
        numPtr2[4] = numPtr1[4];
        numPtr2[5] = numPtr1[5];
        numPtr2[6] = numPtr1[6];
        numPtr2[7] = numPtr1[7];
        return num;
      }
      catch (NullReferenceException ex)
      {
        throw new AccessViolationException();
      }
    }

    /// <summary>
    ///   Считывает из неуправляемой памяти 64-битное целое число со знаком.
    /// </summary>
    /// <param name="ptr">
    ///   Адрес неуправляемой памяти, откуда производится чтение.
    /// </param>
    /// <returns>
    ///   64-битное целое число со знаком, считанное из неуправляемой памяти.
    /// </returns>
    /// <exception cref="T:System.AccessViolationException">
    ///   <paramref name="ptr" /> не является распознаваемым форматом.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="ptr" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="ptr" /> недопустим.
    /// </exception>
    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    public static long ReadInt64(IntPtr ptr)
    {
      return Marshal.ReadInt64(ptr, 0);
    }

    /// <summary>
    ///   Записывает однобайтовое значение в неуправляемую память с указанным смещением.
    /// </summary>
    /// <param name="ptr">
    ///   Базовый адрес для записи в неуправляемой памяти.
    /// </param>
    /// <param name="ofs">
    ///   Дополнительное смещение байтов, добавляемое к <paramref name="ptr" /> перед записью.
    /// </param>
    /// <param name="val">Значение для записи.</param>
    /// <exception cref="T:System.AccessViolationException">
    ///   Базовый адрес (<paramref name="ptr" />), а также смещение байтов (<paramref name="ofs" />) дает значение null или недопустимый адрес.
    /// </exception>
    [SecurityCritical]
    public static unsafe void WriteByte(IntPtr ptr, int ofs, byte val)
    {
      try
      {
        *(sbyte*) ((IntPtr) (void*) ptr + ofs) = (sbyte) val;
      }
      catch (NullReferenceException ex)
      {
        throw new AccessViolationException();
      }
    }

    /// <summary>
    ///   Записывает однобайтовое значение в неуправляемую память с указанным смещением.
    /// </summary>
    /// <param name="ptr">
    ///   Базовый адрес конечного объекта в неуправляемой памяти.
    /// </param>
    /// <param name="ofs">
    ///   Дополнительное смещение в байтах, который добавляется в <paramref name="ptr" /> перед записью.
    /// </param>
    /// <param name="val">Значение для записи.</param>
    /// <exception cref="T:System.AccessViolationException">
    ///   Базовый адрес (<paramref name="ptr" />), а также смещение байтов (<paramref name="ofs" />) дает значение null или недопустимый адрес.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="ptr" /> является объектом <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" />.
    ///    Этот метод не принимает <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> параметров.
    /// </exception>
    [SuppressUnmanagedCodeSecurity]
    [SecurityCritical]
    [DllImport("mscoree.dll", EntryPoint = "ND_WU1")]
    public static extern void WriteByte([MarshalAs(UnmanagedType.AsAny), In, Out] object ptr, int ofs, byte val);

    /// <summary>
    ///   Записывает однобайтовое значение в неуправляемую память.
    /// </summary>
    /// <param name="ptr">
    ///   Адрес в неуправляемой памяти, по которому производится запись.
    /// </param>
    /// <param name="val">Значение для записи.</param>
    /// <exception cref="T:System.AccessViolationException">
    ///   <paramref name="ptr" /> не является распознаваемым форматом.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="ptr" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="ptr" /> недопустим.
    /// </exception>
    [SecurityCritical]
    public static void WriteByte(IntPtr ptr, byte val)
    {
      Marshal.WriteByte(ptr, 0, val);
    }

    /// <summary>
    ///   Записывает 16-битное целое число со знаком в неуправляемую память с указанным смещением.
    /// </summary>
    /// <param name="ptr">
    ///   Базовый адрес для записи в неуправляемой памяти.
    /// </param>
    /// <param name="ofs">
    ///   Дополнительное смещение в байтах, который добавляется в <paramref name="ptr" /> перед записью.
    /// </param>
    /// <param name="val">Значение для записи.</param>
    /// <exception cref="T:System.AccessViolationException">
    ///   Базовый адрес (<paramref name="ptr" />), а также смещение байтов (<paramref name="ofs" />) дает значение null или недопустимый адрес.
    /// </exception>
    [SecurityCritical]
    public static unsafe void WriteInt16(IntPtr ptr, int ofs, short val)
    {
      try
      {
        byte* numPtr1 = (byte*) ((IntPtr) (void*) ptr + ofs);
        if (((int) numPtr1 & 1) == 0)
        {
          *(short*) numPtr1 = val;
        }
        else
        {
          byte* numPtr2 = (byte*) &val;
          *numPtr1 = *numPtr2;
          numPtr1[1] = numPtr2[1];
        }
      }
      catch (NullReferenceException ex)
      {
        throw new AccessViolationException();
      }
    }

    /// <summary>
    ///   Записывает 16-битное целое число со знаком в неуправляемую память с указанным смещением.
    /// </summary>
    /// <param name="ptr">
    ///   Базовый адрес конечного объекта в неуправляемой памяти.
    /// </param>
    /// <param name="ofs">
    ///   Дополнительное смещение в байтах, который добавляется в <paramref name="ptr" /> перед записью.
    /// </param>
    /// <param name="val">Значение для записи.</param>
    /// <exception cref="T:System.AccessViolationException">
    ///   Базовый адрес (<paramref name="ptr" />), а также смещение байтов (<paramref name="ofs" />) дает значение null или недопустимый адрес.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="ptr" /> является объектом <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" />.
    ///    Этот метод не принимает <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> параметров.
    /// </exception>
    [SuppressUnmanagedCodeSecurity]
    [SecurityCritical]
    [DllImport("mscoree.dll", EntryPoint = "ND_WI2")]
    public static extern void WriteInt16([MarshalAs(UnmanagedType.AsAny), In, Out] object ptr, int ofs, short val);

    /// <summary>
    ///   Записывает в неуправляемую память 16-битное целое число.
    /// </summary>
    /// <param name="ptr">
    ///   Адрес в неуправляемой памяти, по которому производится запись.
    /// </param>
    /// <param name="val">Значение для записи.</param>
    /// <exception cref="T:System.AccessViolationException">
    ///   <paramref name="ptr" /> не является распознаваемым форматом.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="ptr" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="ptr" /> недопустим.
    /// </exception>
    [SecurityCritical]
    public static void WriteInt16(IntPtr ptr, short val)
    {
      Marshal.WriteInt16(ptr, 0, val);
    }

    /// <summary>
    ///   Записывает 16-битное целое число со знаком в неуправляемую память с указанным смещением.
    /// </summary>
    /// <param name="ptr">
    ///   Базовый адрес для записи в собственной куче.
    /// </param>
    /// <param name="ofs">
    ///   Дополнительное смещение в байтах, который добавляется в <paramref name="ptr" /> перед записью.
    /// </param>
    /// <param name="val">Значение для записи.</param>
    /// <exception cref="T:System.AccessViolationException">
    ///   Базовый адрес (<paramref name="ptr" />), а также смещение байтов (<paramref name="ofs" />) дает значение null или недопустимый адрес.
    /// </exception>
    [SecurityCritical]
    public static void WriteInt16(IntPtr ptr, int ofs, char val)
    {
      Marshal.WriteInt16(ptr, ofs, (short) val);
    }

    /// <summary>
    ///   Записывает 16-битное целое число со знаком в неуправляемую память с указанным смещением.
    /// </summary>
    /// <param name="ptr">
    ///   Базовый адрес конечного объекта в неуправляемой памяти.
    /// </param>
    /// <param name="ofs">
    ///   Дополнительное смещение в байтах, который добавляется в <paramref name="ptr" /> перед записью.
    /// </param>
    /// <param name="val">Значение для записи.</param>
    /// <exception cref="T:System.AccessViolationException">
    ///   Базовый адрес (<paramref name="ptr" />), а также смещение байтов (<paramref name="ofs" />) дает значение null или недопустимый адрес.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="ptr" /> является объектом <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" />.
    ///    Этот метод не принимает <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> параметров.
    /// </exception>
    [SecurityCritical]
    public static void WriteInt16([In, Out] object ptr, int ofs, char val)
    {
      Marshal.WriteInt16(ptr, ofs, (short) val);
    }

    /// <summary>
    ///   Записывает в неуправляемую память символ в виде 16-битного целого числа.
    /// </summary>
    /// <param name="ptr">
    ///   Адрес в неуправляемой памяти, по которому производится запись.
    /// </param>
    /// <param name="val">Значение для записи.</param>
    /// <exception cref="T:System.AccessViolationException">
    ///   <paramref name="ptr" /> не является распознаваемым форматом.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="ptr" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="ptr" /> недопустим.
    /// </exception>
    [SecurityCritical]
    public static void WriteInt16(IntPtr ptr, char val)
    {
      Marshal.WriteInt16(ptr, 0, (short) val);
    }

    /// <summary>
    ///   Записывает 32-битное целое число со знаком в неуправляемую память с указанным смещением.
    /// </summary>
    /// <param name="ptr">
    ///   Базовый адрес для записи в неуправляемой памяти.
    /// </param>
    /// <param name="ofs">
    ///   Дополнительное смещение в байтах, который добавляется в <paramref name="ptr" /> перед записью.
    /// </param>
    /// <param name="val">Значение для записи.</param>
    /// <exception cref="T:System.AccessViolationException">
    ///   Базовый адрес (<paramref name="ptr" />), а также смещение байтов (<paramref name="ofs" />) дает значение null или недопустимый адрес.
    /// </exception>
    [SecurityCritical]
    public static unsafe void WriteInt32(IntPtr ptr, int ofs, int val)
    {
      try
      {
        byte* numPtr1 = (byte*) ((IntPtr) (void*) ptr + ofs);
        if (((int) numPtr1 & 3) == 0)
        {
          *(int*) numPtr1 = val;
        }
        else
        {
          byte* numPtr2 = (byte*) &val;
          *numPtr1 = *numPtr2;
          numPtr1[1] = numPtr2[1];
          numPtr1[2] = numPtr2[2];
          numPtr1[3] = numPtr2[3];
        }
      }
      catch (NullReferenceException ex)
      {
        throw new AccessViolationException();
      }
    }

    /// <summary>
    ///   Записывает 32-битное целое число со знаком в неуправляемую память с указанным смещением.
    /// </summary>
    /// <param name="ptr">
    ///   Базовый адрес конечного объекта в неуправляемой памяти.
    /// </param>
    /// <param name="ofs">
    ///   Дополнительное смещение в байтах, который добавляется в <paramref name="ptr" /> перед записью.
    /// </param>
    /// <param name="val">Значение для записи.</param>
    /// <exception cref="T:System.AccessViolationException">
    ///   Базовый адрес (<paramref name="ptr" />), а также смещение байтов (<paramref name="ofs" />) дает значение null или недопустимый адрес.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="ptr" /> является объектом <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" />.
    ///    Этот метод не принимает <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> параметров.
    /// </exception>
    [SuppressUnmanagedCodeSecurity]
    [SecurityCritical]
    [DllImport("mscoree.dll", EntryPoint = "ND_WI4")]
    public static extern void WriteInt32([MarshalAs(UnmanagedType.AsAny), In, Out] object ptr, int ofs, int val);

    /// <summary>
    ///   Записывает в неуправляемую память 32-битное целое число со знаком.
    /// </summary>
    /// <param name="ptr">
    ///   Адрес в неуправляемой памяти, по которому производится запись.
    /// </param>
    /// <param name="val">Значение для записи.</param>
    /// <exception cref="T:System.AccessViolationException">
    ///   <paramref name="ptr" /> не является распознаваемым форматом.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="ptr" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="ptr" /> недопустим.
    /// </exception>
    [SecurityCritical]
    public static void WriteInt32(IntPtr ptr, int val)
    {
      Marshal.WriteInt32(ptr, 0, val);
    }

    /// <summary>
    ///   Записывает в неуправляемую память с указанным смещением целое число, разрядность которого соответствует собственной разрядности процессора.
    /// </summary>
    /// <param name="ptr">
    ///   Базовый адрес для записи в неуправляемой памяти.
    /// </param>
    /// <param name="ofs">
    ///   Дополнительное смещение в байтах, который добавляется в <paramref name="ptr" /> перед записью.
    /// </param>
    /// <param name="val">Значение для записи.</param>
    /// <exception cref="T:System.AccessViolationException">
    ///   Базовый адрес (<paramref name="ptr" />), а также смещение байтов (<paramref name="ofs" />) дает значение null или недопустимый адрес.
    /// </exception>
    [SecurityCritical]
    public static void WriteIntPtr(IntPtr ptr, int ofs, IntPtr val)
    {
      Marshal.WriteInt32(ptr, ofs, (int) val);
    }

    /// <summary>
    ///   Записывает в неуправляемую память целое число, разрядность которого соответствует собственной разрядности процессора.
    /// </summary>
    /// <param name="ptr">
    ///   Базовый адрес конечного объекта в неуправляемой памяти.
    /// </param>
    /// <param name="ofs">
    ///   Дополнительное смещение в байтах, который добавляется в <paramref name="ptr" /> перед записью.
    /// </param>
    /// <param name="val">Значение для записи.</param>
    /// <exception cref="T:System.AccessViolationException">
    ///   Базовый адрес (<paramref name="ptr" />), а также смещение байтов (<paramref name="ofs" />) дает значение null или недопустимый адрес.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="ptr" /> является объектом <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" />.
    ///    Этот метод не принимает <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> параметров.
    /// </exception>
    [SecurityCritical]
    public static void WriteIntPtr([MarshalAs(UnmanagedType.AsAny), In, Out] object ptr, int ofs, IntPtr val)
    {
      Marshal.WriteInt32(ptr, ofs, (int) val);
    }

    /// <summary>
    ///   Записывает в неуправляемую память целое число, разрядность которого соответствует собственной разрядности процессора.
    /// </summary>
    /// <param name="ptr">
    ///   Адрес в неуправляемой памяти, по которому производится запись.
    /// </param>
    /// <param name="val">Значение для записи.</param>
    /// <exception cref="T:System.AccessViolationException">
    ///   <paramref name="ptr" /> не является распознаваемым форматом.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="ptr" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="ptr" /> недопустим.
    /// </exception>
    [SecurityCritical]
    public static void WriteIntPtr(IntPtr ptr, IntPtr val)
    {
      Marshal.WriteInt32(ptr, 0, (int) val);
    }

    /// <summary>
    ///   Записывает 64-битное целое число со знаком в неуправляемую память с указанным смещением.
    /// </summary>
    /// <param name="ptr">
    ///   Базовый адрес для записи в неуправляемой памяти.
    /// </param>
    /// <param name="ofs">
    ///   Дополнительное смещение в байтах, который добавляется в <paramref name="ptr" /> перед записью.
    /// </param>
    /// <param name="val">Значение для записи.</param>
    /// <exception cref="T:System.AccessViolationException">
    ///   Базовый адрес (<paramref name="ptr" />), а также смещение байтов (<paramref name="ofs" />) дает значение null или недопустимый адрес.
    /// </exception>
    [SecurityCritical]
    public static unsafe void WriteInt64(IntPtr ptr, int ofs, long val)
    {
      try
      {
        byte* numPtr1 = (byte*) ((IntPtr) (void*) ptr + ofs);
        if (((int) numPtr1 & 7) == 0)
        {
          *(long*) numPtr1 = val;
        }
        else
        {
          byte* numPtr2 = (byte*) &val;
          *numPtr1 = *numPtr2;
          numPtr1[1] = numPtr2[1];
          numPtr1[2] = numPtr2[2];
          numPtr1[3] = numPtr2[3];
          numPtr1[4] = numPtr2[4];
          numPtr1[5] = numPtr2[5];
          numPtr1[6] = numPtr2[6];
          numPtr1[7] = numPtr2[7];
        }
      }
      catch (NullReferenceException ex)
      {
        throw new AccessViolationException();
      }
    }

    /// <summary>
    ///   Записывает 64-битное целое число со знаком в неуправляемую память с указанным смещением.
    /// </summary>
    /// <param name="ptr">
    ///   Базовый адрес конечного объекта в неуправляемой памяти.
    /// </param>
    /// <param name="ofs">
    ///   Дополнительное смещение в байтах, который добавляется в <paramref name="ptr" /> перед записью.
    /// </param>
    /// <param name="val">Значение для записи.</param>
    /// <exception cref="T:System.AccessViolationException">
    ///   Базовый адрес (<paramref name="ptr" />), а также смещение байтов (<paramref name="ofs" />) дает значение null или недопустимый адрес.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="ptr" /> является объектом <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" />.
    ///    Этот метод не принимает <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> параметров.
    /// </exception>
    [SuppressUnmanagedCodeSecurity]
    [SecurityCritical]
    [DllImport("mscoree.dll", EntryPoint = "ND_WI8")]
    public static extern void WriteInt64([MarshalAs(UnmanagedType.AsAny), In, Out] object ptr, int ofs, long val);

    /// <summary>
    ///   Записывает в неуправляемую память 64-битное целое число со знаком.
    /// </summary>
    /// <param name="ptr">
    ///   Адрес в неуправляемой памяти, по которому производится запись.
    /// </param>
    /// <param name="val">Значение для записи.</param>
    /// <exception cref="T:System.AccessViolationException">
    ///   <paramref name="ptr" /> не является распознаваемым форматом.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="ptr" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="ptr" /> недопустим.
    /// </exception>
    [SecurityCritical]
    public static void WriteInt64(IntPtr ptr, long val)
    {
      Marshal.WriteInt64(ptr, 0, val);
    }

    /// <summary>
    ///   Возвращает код ошибки, возвращенный последней неуправляемой функцией, который был вызван с помощью платформы вызова с <see cref="F:System.Runtime.InteropServices.DllImportAttribute.SetLastError" /> значение флага.
    /// </summary>
    /// <returns>
    ///   Код последней ошибки, заданный вызовом функции Win32 SetLastError функции.
    /// </returns>
    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern int GetLastWin32Error();

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void SetLastWin32Error(int error);

    /// <summary>
    ///   Возвращает значение HRESULT, соответствующее последней ошибке, возникшей в коде Win32, выполняемом с помощью <see cref="T:System.Runtime.InteropServices.Marshal" />.
    /// </summary>
    /// <returns>
    ///   Значение HRESULT, соответствующее последнему коду ошибки Win32.
    /// </returns>
    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    public static int GetHRForLastWin32Error()
    {
      int lastWin32Error = Marshal.GetLastWin32Error();
      if (((long) lastWin32Error & 2147483648L) == 2147483648L)
        return lastWin32Error;
      return lastWin32Error & (int) ushort.MaxValue | -2147024896;
    }

    /// <summary>
    ///   Выполняет задачи настройки метода за один раз, не вызывая метод.
    /// </summary>
    /// <param name="m">Проверяемый метод.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="m" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="m" /> Параметр не <see cref="T:System.Reflection.MethodInfo" /> объекта.
    /// </exception>
    [SecurityCritical]
    public static void Prelink(MethodInfo m)
    {
      if (m == (MethodInfo) null)
        throw new ArgumentNullException(nameof (m));
      RuntimeMethodInfo runtimeMethodInfo = m as RuntimeMethodInfo;
      if ((MethodInfo) runtimeMethodInfo == (MethodInfo) null)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeMethodInfo"));
      Marshal.InternalPrelink((IRuntimeMethodInfo) runtimeMethodInfo);
    }

    [SuppressUnmanagedCodeSecurity]
    [SecurityCritical]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void InternalPrelink(IRuntimeMethodInfo m);

    /// <summary>
    ///   Выполняет проверку перед связыванием для всех методов класса.
    /// </summary>
    /// <param name="c">
    ///   Класс, методы которого требуется проверить.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="c" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    public static void PrelinkAll(Type c)
    {
      if (c == (Type) null)
        throw new ArgumentNullException(nameof (c));
      MethodInfo[] methods = c.GetMethods();
      if (methods == null)
        return;
      for (int index = 0; index < methods.Length; ++index)
        Marshal.Prelink(methods[index]);
    }

    /// <summary>
    ///   Вычисляет число байтов в неуправляемой памяти, необходимых для хранения параметров указанного метода.
    /// </summary>
    /// <param name="m">Проверяемый метод.</param>
    /// <returns>
    ///   Число байтов, необходимых для представления параметров метода в неуправляемой памяти.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="m" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="m" /> Параметр не <see cref="T:System.Reflection.MethodInfo" /> объекта.
    /// </exception>
    [SecurityCritical]
    public static int NumParamBytes(MethodInfo m)
    {
      if (m == (MethodInfo) null)
        throw new ArgumentNullException(nameof (m));
      RuntimeMethodInfo runtimeMethodInfo = m as RuntimeMethodInfo;
      if ((MethodInfo) runtimeMethodInfo == (MethodInfo) null)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeMethodInfo"));
      return Marshal.InternalNumParamBytes((IRuntimeMethodInfo) runtimeMethodInfo);
    }

    [SuppressUnmanagedCodeSecurity]
    [SecurityCritical]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern int InternalNumParamBytes(IRuntimeMethodInfo m);

    /// <summary>
    ///   Извлекает не зависящее от компьютера описание исключения и сведения о состоянии потоков в момент возникновения исключения.
    /// </summary>
    /// <returns>Указатель на EXCEPTION_POINTERS структуры.</returns>
    [SecurityCritical]
    [ComVisible(true)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern IntPtr GetExceptionPointers();

    /// <summary>
    ///   Извлекает код, определяющий тип возникшего исключения.
    /// </summary>
    /// <returns>Тип исключения.</returns>
    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern int GetExceptionCode();

    /// <summary>
    ///   Маршалирует данные из управляемого объекта в неуправляемый блок памяти.
    /// </summary>
    /// <param name="structure">
    ///   Управляемый объект, содержащий данные для маршалинга.
    ///    Этот объект должен представлять собой структуру или экземпляр форматированного класса.
    /// </param>
    /// <param name="ptr">
    ///   Указатель на неуправляемый блок памяти, который должен быть выделен перед вызовом метода.
    /// </param>
    /// <param name="fDeleteOld">
    ///   Значение <see langword="true" /> для вызова метода <see cref="M:System.Runtime.InteropServices.Marshal.DestroyStructure(System.IntPtr,System.Type)" /> в параметре <paramref name="ptr" /> перед тем, как этот метод скопирует данные.
    ///    Блок должен содержать допустимые данные.
    ///    Обратите внимание, что передача значения <see langword="false" />, когда блок памяти уже содержит данные, может привести к утечке памяти.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="structure" /> — это ссылочный тип, который не является форматированным классом.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="structure" /> — это универсальный тип.
    /// </exception>
    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [ComVisible(true)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern void StructureToPtr(object structure, IntPtr ptr, bool fDeleteOld);

    /// <summary>
    ///   [Поддерживается в .NET Framework 4.5.1 и более поздних версиях.]
    /// 
    ///   Маршалирует данные из управляемого объекта указанного типа в неуправляемый блок памяти.
    /// </summary>
    /// <param name="structure">
    ///   Управляемый объект, содержащий данные для маршалинга.
    ///    Объект должен представлять собой структуру или экземпляр форматированного класса.
    /// </param>
    /// <param name="ptr">
    ///   Указатель на неуправляемый блок памяти, который должен быть выделен перед вызовом метода.
    /// </param>
    /// <param name="fDeleteOld">
    ///   <see langword="true" /> для вызова <see cref="M:System.Runtime.InteropServices.Marshal.DestroyStructure``1(System.IntPtr)" /> метод <paramref name="ptr" /> перед тем, как этот метод копирует данные.
    ///    Блок должен содержать допустимые данные.
    ///    Обратите внимание, что передача <see langword="false" /> Когда блок памяти уже содержит данные, может привести к утечке памяти.
    /// </param>
    /// <typeparam name="T">Тип управляемого объекта.</typeparam>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="structure" /> является ссылочным типом, не форматированный класс.
    /// </exception>
    [SecurityCritical]
    public static void StructureToPtr<T>(T structure, IntPtr ptr, bool fDeleteOld)
    {
      Marshal.StructureToPtr((object) structure, ptr, fDeleteOld);
    }

    /// <summary>
    ///   Маршалирует данные из неуправляемого блока памяти в управляемый объект.
    /// </summary>
    /// <param name="ptr">Указатель на неуправляемый блок памяти.</param>
    /// <param name="structure">
    ///   Объект, в который копируются данные.
    ///    Он должен представлять собой экземпляр форматированного класса.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Макет структуры не является последовательным или явным.
    /// 
    ///   -или-
    /// 
    ///   Структура — это упакованный тип значений.
    /// </exception>
    [SecurityCritical]
    [ComVisible(true)]
    public static void PtrToStructure(IntPtr ptr, object structure)
    {
      Marshal.PtrToStructureHelper(ptr, structure, false);
    }

    /// <summary>
    ///   [Поддерживается в .NET Framework 4.5.1 и более поздних версиях.]
    /// 
    ///   Маршалирует данные из неуправляемого блока памяти в управляемый объект указанного типа.
    /// </summary>
    /// <param name="ptr">Указатель на неуправляемый блок памяти.</param>
    /// <param name="structure">
    ///   Объект, в который копируются данные.
    /// </param>
    /// <typeparam name="T">
    ///   Тип параметра <paramref name="structure" />.
    ///    Этот должен быть форматированный класс.
    /// </typeparam>
    /// <exception cref="T:System.ArgumentException">
    ///   Макет структуры не является последовательным или явным.
    /// </exception>
    [SecurityCritical]
    public static void PtrToStructure<T>(IntPtr ptr, T structure)
    {
      Marshal.PtrToStructure(ptr, (object) structure);
    }

    /// <summary>
    ///   Маршалирует данные из неуправляемого блока памяти во вновь выделенный управляемый объект указанного типа.
    /// </summary>
    /// <param name="ptr">Указатель на неуправляемый блок памяти.</param>
    /// <param name="structureType">
    ///   Тип создаваемого объекта.
    ///    Этот объект должен представлять форматированный класс или структуру.
    /// </param>
    /// <returns>
    ///   Управляемый объект, содержащий данные, на который указывает <paramref name="ptr" /> параметр.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="structureType" /> Макет параметров не является последовательным или явным.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="structureType" /> имеет универсальный тип.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="structureType" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.MissingMethodException">
    ///   Класс, указанный <paramref name="structureType" /> не имеет доступный конструктор по умолчанию.
    /// </exception>
    [SecurityCritical]
    [ComVisible(true)]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static object PtrToStructure(IntPtr ptr, Type structureType)
    {
      if (ptr == IntPtr.Zero)
        return (object) null;
      if (structureType == (Type) null)
        throw new ArgumentNullException(nameof (structureType));
      if (structureType.IsGenericType)
        throw new ArgumentException(Environment.GetResourceString("Argument_NeedNonGenericType"), nameof (structureType));
      RuntimeType underlyingSystemType = structureType.UnderlyingSystemType as RuntimeType;
      if (underlyingSystemType == (RuntimeType) null)
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), "type");
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      object instanceDefaultCtor = underlyingSystemType.CreateInstanceDefaultCtor(false, false, false, ref stackMark);
      Marshal.PtrToStructureHelper(ptr, instanceDefaultCtor, true);
      return instanceDefaultCtor;
    }

    /// <summary>
    ///   [Поддерживается в .NET Framework 4.5.1 и более поздних версиях.]
    /// 
    ///   Маршалирует данные из неуправляемого блока памяти вновь выделенный управляемый объект типа, указанного в параметре универсального типа.
    /// </summary>
    /// <param name="ptr">Указатель на неуправляемый блок памяти.</param>
    /// <typeparam name="T">
    ///   Тип объекта, в который копируются данные.
    ///    Это должен быть форматированный класс или структура.
    /// </typeparam>
    /// <returns>
    ///   Управляемый объект, содержащий данные, <paramref name="ptr" /> указывает параметр.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Макет <paramref name="T" /> не является последовательным или явным.
    /// </exception>
    /// <exception cref="T:System.MissingMethodException">
    ///   Класс, указанный <paramref name="T" /> не имеет доступный конструктор по умолчанию.
    /// </exception>
    [SecurityCritical]
    public static T PtrToStructure<T>(IntPtr ptr)
    {
      return (T) Marshal.PtrToStructure(ptr, typeof (T));
    }

    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void PtrToStructureHelper(IntPtr ptr, object structure, bool allowValueClasses);

    /// <summary>
    ///   Освобождает все вложенные структуры, на которые указывает заданный блок неуправляемой памяти.
    /// </summary>
    /// <param name="ptr">Указатель на неуправляемый блок памяти.</param>
    /// <param name="structuretype">
    ///   Тип отформатированного класса.
    ///    Это предоставляет сведения о макете, необходимые для удаления буфера в <paramref name="ptr" /> параметр.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="structureType" /> имеет автоматический макет.
    ///    Вместо этого используйте последовательным или явным.
    /// </exception>
    [SecurityCritical]
    [ComVisible(true)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern void DestroyStructure(IntPtr ptr, Type structuretype);

    /// <summary>
    ///   [Поддерживается в .NET Framework 4.5.1 и более поздних версиях.]
    /// 
    ///   Освобождает все вложенные структуры указанного типа, который указывает заданный блок неуправляемой памяти.
    /// </summary>
    /// <param name="ptr">Указатель на неуправляемый блок памяти.</param>
    /// <typeparam name="T">
    ///   Тип отформатированной структуры.
    ///    Это предоставляет сведения о макете, необходимые для удаления буфера в <paramref name="ptr" /> параметр.
    /// </typeparam>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="T" /> имеет автоматический макет.
    ///    Вместо этого используйте последовательным или явным.
    /// </exception>
    [SecurityCritical]
    public static void DestroyStructure<T>(IntPtr ptr)
    {
      Marshal.DestroyStructure(ptr, typeof (T));
    }

    /// <summary>
    ///   Возвращает дескриптор экземпляра (HINSTANCE) для указанного модуля.
    /// </summary>
    /// <param name="m">
    ///   Модуль, дескриптор экземпляра (HINSTANCE) которого необходимо получить.
    /// </param>
    /// <returns>
    ///   Значение HINSTANCE для <paramref name="m" />; или значение -1, если модуля отсутствует HINSTANCE.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="m" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    public static IntPtr GetHINSTANCE(Module m)
    {
      if (m == (Module) null)
        throw new ArgumentNullException(nameof (m));
      RuntimeModule runtimeModule = m as RuntimeModule;
      if ((Module) runtimeModule == (Module) null)
      {
        ModuleBuilder moduleBuilder = m as ModuleBuilder;
        if ((Module) moduleBuilder != (Module) null)
          runtimeModule = (RuntimeModule) moduleBuilder.InternalModule;
      }
      if ((Module) runtimeModule == (Module) null)
        throw new ArgumentNullException(Environment.GetResourceString("Argument_MustBeRuntimeModule"));
      return Marshal.GetHINSTANCE(runtimeModule.GetNativeHandle());
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern IntPtr GetHINSTANCE(RuntimeModule m);

    /// <summary>
    ///   Создает исключение с определенным значением ошибки HRESULT.
    /// </summary>
    /// <param name="errorCode">
    ///   Значение HRESULT, соответствующее нужному исключению.
    /// </param>
    [SecurityCritical]
    public static void ThrowExceptionForHR(int errorCode)
    {
      if (errorCode >= 0)
        return;
      Marshal.ThrowExceptionForHRInternal(errorCode, IntPtr.Zero);
    }

    /// <summary>
    ///   Создает исключение с определенным значением ошибки HRESULT, основываясь на указанном IErrorInfo интерфейса.
    /// </summary>
    /// <param name="errorCode">
    ///   Значение HRESULT, соответствующее нужному исключению.
    /// </param>
    /// <param name="errorInfo">
    ///   Указатель на IErrorInfo интерфейс, предоставляющий дополнительные сведения об ошибке.
    ///    Можно указать IntPtr(0) для использования текущих IErrorInfo интерфейс, или IntPtr(-1) игнорировать текущий IErrorInfo интерфейса и создать исключение только из кода ошибки.
    /// </param>
    [SecurityCritical]
    public static void ThrowExceptionForHR(int errorCode, IntPtr errorInfo)
    {
      if (errorCode >= 0)
        return;
      Marshal.ThrowExceptionForHRInternal(errorCode, errorInfo);
    }

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void ThrowExceptionForHRInternal(int errorCode, IntPtr errorInfo);

    /// <summary>
    ///   Преобразует заданный код ошибки HRESULT в соответствующий объект <see cref="T:System.Exception" />.
    /// </summary>
    /// <param name="errorCode">Преобразуемое значение HRESULT.</param>
    /// <returns>
    ///   Объект, представляющий преобразованное значение HRESULT.
    /// </returns>
    [SecurityCritical]
    public static Exception GetExceptionForHR(int errorCode)
    {
      if (errorCode < 0)
        return Marshal.GetExceptionForHRInternal(errorCode, IntPtr.Zero);
      return (Exception) null;
    }

    /// <summary>
    ///   Преобразует код ошибки HRESULT в соответствующий объект <see cref="T:System.Exception" /> с дополнительными сведениями об ошибке, передаваемыми в интерфейсе IErrorInfo для объекта исключения.
    /// </summary>
    /// <param name="errorCode">Преобразуемое значение HRESULT.</param>
    /// <param name="errorInfo">
    ///   Указатель на интерфейс <see langword="IErrorInfo" />, предоставляющий дополнительные сведения об ошибке.
    ///    Можно задать IntPtr(0), чтобы использовать текущий интерфейс <see langword="IErrorInfo" />, или IntPtr(-1), чтобы игнорировать текущий интерфейс <see langword="IErrorInfo" /> и сформировать сообщение об исключении только из кода ошибки.
    /// </param>
    /// <returns>
    ///   Объект, представляющий преобразованное значение HRESULT и сведения, полученные из параметра <paramref name="errorInfo" />.
    /// </returns>
    [SecurityCritical]
    public static Exception GetExceptionForHR(int errorCode, IntPtr errorInfo)
    {
      if (errorCode < 0)
        return Marshal.GetExceptionForHRInternal(errorCode, errorInfo);
      return (Exception) null;
    }

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern Exception GetExceptionForHRInternal(int errorCode, IntPtr errorInfo);

    /// <summary>
    ///   Преобразует указанное исключение в значение HRESULT.
    /// </summary>
    /// <param name="e">
    ///   Исключение, преобразуемое в значение HRESULT.
    /// </param>
    /// <returns>
    ///   Значение HRESULT, сопоставленное с заданным исключением.
    /// </returns>
    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern int GetHRForException(Exception e);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern int GetHRForException_WinRT(Exception e);

    /// <summary>
    ///   Возвращает указатель на функцию, генерируемую в среде выполнения, которая маршалирует вызов из неуправляемого кода в управляемый.
    /// </summary>
    /// <param name="pfnMethodToWrap">
    ///   Указатель на метод, маршалирование которого требуется выполнить.
    /// </param>
    /// <param name="pbSignature">Указатель на сигнатуру метода.</param>
    /// <param name="cbSignature">
    ///   Число байтов в <paramref name="pbSignature" />.
    /// </param>
    /// <returns>
    ///   Указатель на функцию, маршалирующую вызов из <paramref name="pfnMethodToWrap" /> в управляемый код.
    /// </returns>
    [SecurityCritical]
    [Obsolete("The GetUnmanagedThunkForManagedMethodPtr method has been deprecated and will be removed in a future release.", false)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern IntPtr GetUnmanagedThunkForManagedMethodPtr(IntPtr pfnMethodToWrap, IntPtr pbSignature, int cbSignature);

    /// <summary>
    ///   Возвращает указатель на функцию, генерируемую в среде выполнения, которая маршалирует вызов из управляемого кода в неуправляемый.
    /// </summary>
    /// <param name="pfnMethodToWrap">
    ///   Указатель на метод, маршалирование которого требуется выполнить.
    /// </param>
    /// <param name="pbSignature">Указатель на сигнатуру метода.</param>
    /// <param name="cbSignature">
    ///   Число байтов в <paramref name="pbSignature" />.
    /// </param>
    /// <returns>
    ///   Указатель на функцию, маршалирующую вызов из <paramref name="pfnMethodToWrap" /> в неуправляемый код.
    /// </returns>
    [SecurityCritical]
    [Obsolete("The GetManagedThunkForUnmanagedMethodPtr method has been deprecated and will be removed in a future release.", false)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern IntPtr GetManagedThunkForUnmanagedMethodPtr(IntPtr pfnMethodToWrap, IntPtr pbSignature, int cbSignature);

    /// <summary>
    ///   Преобразует Многослойный файл cookie в соответствующий <see cref="T:System.Threading.Thread" /> экземпляра.
    /// </summary>
    /// <param name="cookie">
    ///   Целое число, представляющее многослойный файл cookie.
    /// </param>
    /// <returns>
    ///   Поток, который соответствует <paramref name="cookie" /> параметр.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="cookie" /> Параметр равен 0.
    /// </exception>
    [SecurityCritical]
    [Obsolete("The GetThreadFromFiberCookie method has been deprecated.  Use the hosting API to perform this operation.", false)]
    public static Thread GetThreadFromFiberCookie(int cookie)
    {
      if (cookie == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_ArgumentZero"), nameof (cookie));
      return Marshal.InternalGetThreadFromFiberCookie(cookie);
    }

    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern Thread InternalGetThreadFromFiberCookie(int cookie);

    /// <summary>
    ///   Выделяет память из неуправляемой памяти процесса, используя указатель на заданное количество байтов.
    /// </summary>
    /// <param name="cb">Требуемое количество байтов памяти.</param>
    /// <returns>
    ///   Указатель на только что выделенную память.
    ///    Эта память должна освобождаться с помощью <see cref="M:System.Runtime.InteropServices.Marshal.FreeHGlobal(System.IntPtr)" /> метод.
    /// </returns>
    /// <exception cref="T:System.OutOfMemoryException">
    ///   Недостаточно памяти для выполнения запроса.
    /// </exception>
    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    public static IntPtr AllocHGlobal(IntPtr cb)
    {
      IntPtr num = Win32Native.LocalAlloc_NoSafeHandle(0, new UIntPtr((uint) cb.ToInt32()));
      if (num == IntPtr.Zero)
        throw new OutOfMemoryException();
      return num;
    }

    /// <summary>
    ///   Выделяет память из неуправляемой памяти процесса, используя заданное количество байтов.
    /// </summary>
    /// <param name="cb">Требуемое количество байтов памяти.</param>
    /// <returns>
    ///   Указатель на только что выделенную память.
    ///    Эта память должна освобождаться с помощью <see cref="M:System.Runtime.InteropServices.Marshal.FreeHGlobal(System.IntPtr)" /> метод.
    /// </returns>
    /// <exception cref="T:System.OutOfMemoryException">
    ///   Недостаточно памяти для выполнения запроса.
    /// </exception>
    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    public static IntPtr AllocHGlobal(int cb)
    {
      return Marshal.AllocHGlobal((IntPtr) cb);
    }

    /// <summary>
    ///   Освобождает память, выделенную ранее из неуправляемой памяти процесса.
    /// </summary>
    /// <param name="hglobal">
    ///   Дескриптор, возвращенный исходным сопоставимым вызовом <see cref="M:System.Runtime.InteropServices.Marshal.AllocHGlobal(System.IntPtr)" />.
    /// </param>
    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    public static void FreeHGlobal(IntPtr hglobal)
    {
      if (!Marshal.IsNotWin32Atom(hglobal) || !(IntPtr.Zero != Win32Native.LocalFree(hglobal)))
        return;
      Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
    }

    /// <summary>
    ///   Изменяет размер блока памяти, предварительно выделенной с использованием <see cref="M:System.Runtime.InteropServices.Marshal.AllocHGlobal(System.IntPtr)" />.
    /// </summary>
    /// <param name="pv">
    ///   Указатель на память, выделенную с использованием <see cref="M:System.Runtime.InteropServices.Marshal.AllocHGlobal(System.IntPtr)" />.
    /// </param>
    /// <param name="cb">
    ///   Новый размер выделенного блока.
    ///    Это не указатель; это запрашиваемое число байтов, приведенное к типу <see cref="T:System.IntPtr" />.
    ///    Если передается указатель, он рассматривается как размер.
    /// </param>
    /// <returns>
    ///   Указатель на повторно выделенную память.
    ///    Эта память должна освобождаться с помощью <see cref="M:System.Runtime.InteropServices.Marshal.FreeHGlobal(System.IntPtr)" />.
    /// </returns>
    /// <exception cref="T:System.OutOfMemoryException">
    ///   Недостаточно памяти для выполнения запроса.
    /// </exception>
    [SecurityCritical]
    public static IntPtr ReAllocHGlobal(IntPtr pv, IntPtr cb)
    {
      IntPtr num = Win32Native.LocalReAlloc(pv, cb, 2);
      if (num == IntPtr.Zero)
        throw new OutOfMemoryException();
      return num;
    }

    /// <summary>
    ///   Копирует содержимое управляемого объекта <see cref="T:System.String" /> в неуправляемую память, преобразуя в формат ANSI, во время копирования.
    /// </summary>
    /// <param name="s">Копируемая управляемая строка.</param>
    /// <returns>
    ///   Адрес в неуправляемой памяти, куда <paramref name="s" /> был скопированных или ноль, если <paramref name="s" /> является <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.OutOfMemoryException">
    ///   Не хватает памяти.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="s" /> Параметра превышает максимальную длину, допускаемый операционной системы.
    /// </exception>
    [SecurityCritical]
    public static unsafe IntPtr StringToHGlobalAnsi(string s)
    {
      if (s == null)
        return IntPtr.Zero;
      int cbNativeBuffer = (s.Length + 1) * Marshal.SystemMaxDBCSCharSize;
      if (cbNativeBuffer < s.Length)
        throw new ArgumentOutOfRangeException(nameof (s));
      IntPtr num = Win32Native.LocalAlloc_NoSafeHandle(0, new UIntPtr((uint) cbNativeBuffer));
      if (num == IntPtr.Zero)
        throw new OutOfMemoryException();
      s.ConvertToAnsi((byte*) (void*) num, cbNativeBuffer, false, false);
      return num;
    }

    /// <summary>
    ///   Копирует содержимое управляемого объекта <see cref="T:System.String" /> в неуправляемую память.
    /// </summary>
    /// <param name="s">Копируемая управляемая строка.</param>
    /// <returns>
    ///   Адрес в неуправляемой памяти, куда <paramref name="s" /> был скопированных или ноль, если <paramref name="s" /> является <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.OutOfMemoryException">
    ///   Метод не удалось выделить достаточно памяти в собственной куче.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="s" /> Параметра превышает максимальную длину, допускаемый операционной системы.
    /// </exception>
    [SecurityCritical]
    public static unsafe IntPtr StringToHGlobalUni(string s)
    {
      if (s == null)
        return IntPtr.Zero;
      int num1 = (s.Length + 1) * 2;
      if (num1 < s.Length)
        throw new ArgumentOutOfRangeException(nameof (s));
      IntPtr num2 = Win32Native.LocalAlloc_NoSafeHandle(0, new UIntPtr((uint) num1));
      if (num2 == IntPtr.Zero)
        throw new OutOfMemoryException();
      string str = s;
      char* smem = (char*) str;
      if ((IntPtr) smem != IntPtr.Zero)
        smem += RuntimeHelpers.OffsetToStringData;
      string.wstrcpy((char*) (void*) num2, smem, s.Length + 1);
      str = (string) null;
      return num2;
    }

    /// <summary>
    ///   Копирует содержимое управляемого объекта <see cref="T:System.String" /> в неуправляемую память, преобразуя в формат ANSI при необходимости.
    /// </summary>
    /// <param name="s">Копируемая управляемая строка.</param>
    /// <returns>
    ///   Адрес в неуправляемой памяти, куда скопирована строка, или 0, если <paramref name="s" /> является <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.OutOfMemoryException">
    ///   Не хватает памяти.
    /// </exception>
    [SecurityCritical]
    public static IntPtr StringToHGlobalAuto(string s)
    {
      return Marshal.StringToHGlobalUni(s);
    }

    /// <summary>Извлекает имя библиотеки типов.</summary>
    /// <param name="pTLB">
    ///   Библиотека типов, имя которой требуется извлечь.
    /// </param>
    /// <returns>
    ///   Имя библиотеки типов, <paramref name="pTLB" /> указывает параметр.
    /// </returns>
    [SecurityCritical]
    [Obsolete("Use System.Runtime.InteropServices.Marshal.GetTypeLibName(ITypeLib pTLB) instead. http://go.microsoft.com/fwlink/?linkid=14202&ID=0000011.", false)]
    public static string GetTypeLibName(UCOMITypeLib pTLB)
    {
      return Marshal.GetTypeLibName((ITypeLib) pTLB);
    }

    /// <summary>Извлекает имя библиотеки типов.</summary>
    /// <param name="typelib">
    ///   Библиотека типов, имя которой требуется извлечь.
    /// </param>
    /// <returns>
    ///   Имя библиотеки типов, <paramref name="typelib" /> указывает параметр.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="typelib" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    public static string GetTypeLibName(ITypeLib typelib)
    {
      if (typelib == null)
        throw new ArgumentNullException(nameof (typelib));
      string strName = (string) null;
      string strDocString = (string) null;
      int dwHelpContext = 0;
      string strHelpFile = (string) null;
      typelib.GetDocumentation(-1, out strName, out strDocString, out dwHelpContext, out strHelpFile);
      return strName;
    }

    [SecurityCritical]
    internal static string GetTypeLibNameInternal(ITypeLib typelib)
    {
      if (typelib == null)
        throw new ArgumentNullException(nameof (typelib));
      ITypeLib2 typeLib2 = typelib as ITypeLib2;
      if (typeLib2 != null)
      {
        Guid managedNameGuid = Marshal.ManagedNameGuid;
        object pVarVal;
        try
        {
          typeLib2.GetCustData(ref managedNameGuid, out pVarVal);
        }
        catch (Exception ex)
        {
          pVarVal = (object) null;
        }
        if (pVarVal != null && pVarVal.GetType() == typeof (string))
        {
          string str = ((string) pVarVal).Trim();
          if (str.EndsWith(".DLL", StringComparison.OrdinalIgnoreCase))
            str = str.Substring(0, str.Length - 4);
          else if (str.EndsWith(".EXE", StringComparison.OrdinalIgnoreCase))
            str = str.Substring(0, str.Length - 4);
          return str;
        }
      }
      return Marshal.GetTypeLibName(typelib);
    }

    /// <summary>Извлекает идентификатор LIBID библиотеки типов.</summary>
    /// <param name="pTLB">
    ///   Библиотека типов, идентификатор LIBID которой требуется извлечь.
    /// </param>
    /// <returns>
    ///   Идентификатор LIBID библиотеки типов, <paramref name="pTLB" /> указывает параметр.
    /// </returns>
    [SecurityCritical]
    [Obsolete("Use System.Runtime.InteropServices.Marshal.GetTypeLibGuid(ITypeLib pTLB) instead. http://go.microsoft.com/fwlink/?linkid=14202&ID=0000011.", false)]
    public static Guid GetTypeLibGuid(UCOMITypeLib pTLB)
    {
      return Marshal.GetTypeLibGuid((ITypeLib) pTLB);
    }

    /// <summary>Извлекает идентификатор LIBID библиотеки типов.</summary>
    /// <param name="typelib">
    ///   Библиотека типов, идентификатор LIBID которой требуется извлечь.
    /// </param>
    /// <returns>Идентификатор LIBID указанной библиотеки типов.</returns>
    [SecurityCritical]
    public static Guid GetTypeLibGuid(ITypeLib typelib)
    {
      Guid result = new Guid();
      Marshal.FCallGetTypeLibGuid(ref result, typelib);
      return result;
    }

    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void FCallGetTypeLibGuid(ref Guid result, ITypeLib pTLB);

    /// <summary>Извлекает идентификатор LCID библиотеки типов.</summary>
    /// <param name="pTLB">
    ///   Библиотека типов, идентификатор LCID которой требуется извлечь.
    /// </param>
    /// <returns>
    ///   Идентификатор LCID библиотеки типов, <paramref name="pTLB" /> указывает параметр.
    /// </returns>
    [SecurityCritical]
    [Obsolete("Use System.Runtime.InteropServices.Marshal.GetTypeLibLcid(ITypeLib pTLB) instead. http://go.microsoft.com/fwlink/?linkid=14202&ID=0000011.", false)]
    public static int GetTypeLibLcid(UCOMITypeLib pTLB)
    {
      return Marshal.GetTypeLibLcid((ITypeLib) pTLB);
    }

    /// <summary>Извлекает идентификатор LCID библиотеки типов.</summary>
    /// <param name="typelib">
    ///   Библиотека типов, идентификатор LCID которой требуется извлечь.
    /// </param>
    /// <returns>
    ///   Идентификатор LCID библиотеки типов, <paramref name="typelib" /> указывает параметр.
    /// </returns>
    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern int GetTypeLibLcid(ITypeLib typelib);

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void GetTypeLibVersion(ITypeLib typeLibrary, out int major, out int minor);

    [SecurityCritical]
    internal static Guid GetTypeInfoGuid(ITypeInfo typeInfo)
    {
      Guid result = new Guid();
      Marshal.FCallGetTypeInfoGuid(ref result, typeInfo);
      return result;
    }

    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void FCallGetTypeInfoGuid(ref Guid result, ITypeInfo typeInfo);

    /// <summary>
    ///   Извлекает идентификатор библиотеки LIBID, присвоенный библиотеке типов при экспортировании из указанной сборки.
    /// </summary>
    /// <param name="asm">
    ///   Сборка, из которой экспортирована библиотека типов.
    /// </param>
    /// <returns>
    ///   Идентификатор LIBID, присвоенный библиотеке типов при ее экспорте из указанной сборки.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="asm" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    public static Guid GetTypeLibGuidForAssembly(Assembly asm)
    {
      if (asm == (Assembly) null)
        throw new ArgumentNullException(nameof (asm));
      RuntimeAssembly asm1 = asm as RuntimeAssembly;
      if ((Assembly) asm1 == (Assembly) null)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeAssembly"), nameof (asm));
      Guid result = new Guid();
      Marshal.FCallGetTypeLibGuidForAssembly(ref result, asm1);
      return result;
    }

    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void FCallGetTypeLibGuidForAssembly(ref Guid result, RuntimeAssembly asm);

    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void _GetTypeLibVersionForAssembly(RuntimeAssembly inputAssembly, out int majorVersion, out int minorVersion);

    /// <summary>
    ///   Извлекает номер версии типа библиотеки, экспортируемой из указанной сборки.
    /// </summary>
    /// <param name="inputAssembly">Управляемая сборка.</param>
    /// <param name="majorVersion">Основной номер версии.</param>
    /// <param name="minorVersion">Дополнительный номер версии.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="inputAssembly" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    public static void GetTypeLibVersionForAssembly(Assembly inputAssembly, out int majorVersion, out int minorVersion)
    {
      if (inputAssembly == (Assembly) null)
        throw new ArgumentNullException(nameof (inputAssembly));
      RuntimeAssembly inputAssembly1 = inputAssembly as RuntimeAssembly;
      if ((Assembly) inputAssembly1 == (Assembly) null)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeAssembly"), nameof (inputAssembly));
      Marshal._GetTypeLibVersionForAssembly(inputAssembly1, out majorVersion, out minorVersion);
    }

    /// <summary>
    ///   Получает имя типа, представленного ITypeInfo объекта.
    /// </summary>
    /// <param name="pTI">
    ///   Объект, представляющий <see langword="ITypeInfo" /> указателя.
    /// </param>
    /// <returns>
    ///   Имя типа, <paramref name="pTI" /> указывает параметр.
    /// </returns>
    [SecurityCritical]
    [Obsolete("Use System.Runtime.InteropServices.Marshal.GetTypeInfoName(ITypeInfo pTLB) instead. http://go.microsoft.com/fwlink/?linkid=14202&ID=0000011.", false)]
    public static string GetTypeInfoName(UCOMITypeInfo pTI)
    {
      return Marshal.GetTypeInfoName((ITypeInfo) pTI);
    }

    /// <summary>
    ///   Получает имя типа, представленного ITypeInfo объекта.
    /// </summary>
    /// <param name="typeInfo">
    ///   Объект, представляющий <see langword="ITypeInfo" /> указателя.
    /// </param>
    /// <returns>
    ///   Имя типа, <paramref name="typeInfo" /> указывает параметр.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="typeInfo" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    public static string GetTypeInfoName(ITypeInfo typeInfo)
    {
      if (typeInfo == null)
        throw new ArgumentNullException(nameof (typeInfo));
      string strName = (string) null;
      string strDocString = (string) null;
      int dwHelpContext = 0;
      string strHelpFile = (string) null;
      typeInfo.GetDocumentation(-1, out strName, out strDocString, out dwHelpContext, out strHelpFile);
      return strName;
    }

    [SecurityCritical]
    internal static string GetTypeInfoNameInternal(ITypeInfo typeInfo, out bool hasManagedName)
    {
      if (typeInfo == null)
        throw new ArgumentNullException(nameof (typeInfo));
      ITypeInfo2 typeInfo2 = typeInfo as ITypeInfo2;
      if (typeInfo2 != null)
      {
        Guid managedNameGuid = Marshal.ManagedNameGuid;
        object pVarVal;
        try
        {
          typeInfo2.GetCustData(ref managedNameGuid, out pVarVal);
        }
        catch (Exception ex)
        {
          pVarVal = (object) null;
        }
        if (pVarVal != null && pVarVal.GetType() == typeof (string))
        {
          hasManagedName = true;
          return (string) pVarVal;
        }
      }
      hasManagedName = false;
      return Marshal.GetTypeInfoName(typeInfo);
    }

    [SecurityCritical]
    internal static string GetManagedTypeInfoNameInternal(ITypeLib typeLib, ITypeInfo typeInfo)
    {
      bool hasManagedName;
      string infoNameInternal = Marshal.GetTypeInfoNameInternal(typeInfo, out hasManagedName);
      if (hasManagedName)
        return infoNameInternal;
      return Marshal.GetTypeLibNameInternal(typeLib) + "." + infoNameInternal;
    }

    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern Type GetLoadedTypeForGUID(ref Guid guid);

    /// <summary>
    ///   Преобразует неуправляемый ITypeInfo в управляемый объект <see cref="T:System.Type" /> объект.
    /// </summary>
    /// <param name="piTypeInfo">
    ///   <see langword="ITypeInfo" /> Интерфейс для маршалинга.
    /// </param>
    /// <returns>
    ///   Управляемый тип, представляющий неуправляемый <see langword="ITypeInfo" /> объекта.
    /// </returns>
    [SecurityCritical]
    public static Type GetTypeForITypeInfo(IntPtr piTypeInfo)
    {
      ITypeLib ppTLB = (ITypeLib) null;
      Assembly assembly = (Assembly) null;
      int pIndex = 0;
      if (piTypeInfo == IntPtr.Zero)
        return (Type) null;
      ITypeInfo objectForIunknown = (ITypeInfo) Marshal.GetObjectForIUnknown(piTypeInfo);
      Guid typeInfoGuid = Marshal.GetTypeInfoGuid(objectForIunknown);
      Type loadedTypeForGuid = Marshal.GetLoadedTypeForGUID(ref typeInfoGuid);
      if (loadedTypeForGuid != (Type) null)
        return loadedTypeForGuid;
      try
      {
        objectForIunknown.GetContainingTypeLib(out ppTLB, out pIndex);
      }
      catch (COMException ex)
      {
        ppTLB = (ITypeLib) null;
      }
      Type type;
      if (ppTLB != null)
      {
        string fullName = TypeLibConverter.GetAssemblyNameFromTypelib((object) ppTLB, (string) null, (byte[]) null, (StrongNameKeyPair) null, (Version) null, AssemblyNameFlags.None).FullName;
        Assembly[] assemblies = Thread.GetDomain().GetAssemblies();
        int length = assemblies.Length;
        for (int index = 0; index < length; ++index)
        {
          if (string.Compare(assemblies[index].FullName, fullName, StringComparison.Ordinal) == 0)
            assembly = assemblies[index];
        }
        if (assembly == (Assembly) null)
          assembly = (Assembly) new TypeLibConverter().ConvertTypeLibToAssembly((object) ppTLB, Marshal.GetTypeLibName(ppTLB) + ".dll", TypeLibImporterFlags.None, (ITypeLibImporterNotifySink) new ImporterCallback(), (byte[]) null, (StrongNameKeyPair) null, (string) null, (Version) null);
        type = assembly.GetType(Marshal.GetManagedTypeInfoNameInternal(ppTLB, objectForIunknown), true, false);
        if (type != (Type) null && !type.IsVisible)
          type = (Type) null;
      }
      else
        type = typeof (object);
      return type;
    }

    /// <summary>
    ///   Возвращает тип, связанный с заданным идентификатором класса (CLSID).
    /// </summary>
    /// <param name="clsid">CLSID возвращаемый тип.</param>
    /// <returns>
    ///   <see langword="System.__ComObject" /> вне зависимости от того, допустим ли код CLSID.
    /// </returns>
    [SecuritySafeCritical]
    public static Type GetTypeFromCLSID(Guid clsid)
    {
      return RuntimeType.GetTypeFromCLSIDImpl(clsid, (string) null, false);
    }

    /// <summary>
    ///   Возвращает интерфейс <see cref="T:System.Runtime.InteropServices.ComTypes.ITypeInfo" /> из управляемого типа.
    /// </summary>
    /// <param name="t">
    ///   Тип, интерфейс <see langword="ITypeInfo" /> которого запрашивается.
    /// </param>
    /// <returns>
    ///   Указатель на интерфейс <see langword="ITypeInfo" /> для параметра <paramref name="t" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="t" /> не является типом, видимым для модели COM.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="t" /> относится к типу Среда выполнения Windows.
    /// </exception>
    /// <exception cref="T:System.Runtime.InteropServices.COMException">
    ///   Библиотека типов зарегистрирована для сборки, содержащей тип, но определение типа не найдено.
    /// </exception>
    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern IntPtr GetITypeInfoForType(Type t);

    /// <summary>
    ///   Возвращает IUnknown интерфейс из управляемого объекта.
    /// </summary>
    /// <param name="o">
    ///   Объект, <see langword="IUnknown" /> запрошенный интерфейс.
    /// </param>
    /// <returns>
    ///   <see langword="IUnknown" /> Указатель для <paramref name="o" /> параметр.
    /// </returns>
    [SecurityCritical]
    [__DynamicallyInvokable]
    public static IntPtr GetIUnknownForObject(object o)
    {
      return Marshal.GetIUnknownForObjectNative(o, false);
    }

    /// <summary>
    ///   Возвращает IUnknown интерфейс из управляемого объекта, если вызывающий объект находится в том же контексте, что этот объект.
    /// </summary>
    /// <param name="o">
    ///   Объект, <see langword="IUnknown" /> запрошенный интерфейс.
    /// </param>
    /// <returns>
    ///   <see langword="IUnknown" /> Указателя для указанного объекта или <see langword="null" /> Если вызывающий объект не находится в том же контексте, что и заданный объект.
    /// </returns>
    [SecurityCritical]
    public static IntPtr GetIUnknownForObjectInContext(object o)
    {
      return Marshal.GetIUnknownForObjectNative(o, true);
    }

    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern IntPtr GetIUnknownForObjectNative(object o, bool onlyInContext);

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern IntPtr GetRawIUnknownForComObjectNoAddRef(object o);

    /// <summary>
    ///   Возвращает IDispatch интерфейс из управляемого объекта.
    /// </summary>
    /// <param name="o">
    ///   Объект, <see langword="IDispatch" /> запрошенный интерфейс.
    /// </param>
    /// <returns>
    ///   <see langword="IDispatch" /> Указатель для <paramref name="o" /> параметр.
    /// </returns>
    /// <exception cref="T:System.InvalidCastException">
    ///   <paramref name="o" /> не поддерживает запрошенный интерфейс.
    /// </exception>
    [SecurityCritical]
    [__DynamicallyInvokable]
    public static IntPtr GetIDispatchForObject(object o)
    {
      return Marshal.GetIDispatchForObjectNative(o, false);
    }

    /// <summary>
    ///   Возвращает IDispatch интерфейс указатель из управляемого объекта, если вызывающий объект находится в том же контексте, что этот объект.
    /// </summary>
    /// <param name="o">
    ///   Объект, <see langword="IDispatch" /> запрошенный интерфейс.
    /// </param>
    /// <returns>
    ///   <see langword="IDispatch" /> Указатель на интерфейс для указанного объекта или <see langword="null" /> Если вызывающий объект не находится в том же контексте, что и заданный объект.
    /// </returns>
    /// <exception cref="T:System.InvalidCastException">
    ///   <paramref name="o" /> не поддерживает запрошенный интерфейс.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="o" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    public static IntPtr GetIDispatchForObjectInContext(object o)
    {
      return Marshal.GetIDispatchForObjectNative(o, true);
    }

    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern IntPtr GetIDispatchForObjectNative(object o, bool onlyInContext);

    /// <summary>
    ///   Возвращает указатель на IUnknown представляющий указанный интерфейс для указанного объекта.
    ///    Доступ к настраиваемому интерфейсу запросов включен по умолчанию.
    /// </summary>
    /// <param name="o">Объект, предоставляющий интерфейс.</param>
    /// <param name="T">Тип запрашиваемого интерфейса.</param>
    /// <returns>
    ///   Указатель интерфейса, представляющий заданный интерфейс для объекта.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="T" /> Параметр не является интерфейсом.
    /// 
    ///   -или-
    /// 
    ///   Тип не является видимым для COM.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="T" /> Является параметром универсального типа.
    /// </exception>
    /// <exception cref="T:System.InvalidCastException">
    ///   <paramref name="o" /> Параметр не поддерживает запрошенный интерфейс.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="o" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="T" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    public static IntPtr GetComInterfaceForObject(object o, Type T)
    {
      return Marshal.GetComInterfaceForObjectNative(o, T, false, true);
    }

    /// <summary>
    ///   [Поддерживается в .NET Framework 4.5.1 и более поздних версиях.]
    /// 
    ///   Возвращает указатель на IUnknown интерфейс, представляющий заданный интерфейс для объекта указанного типа.
    ///    Доступ к настраиваемому интерфейсу запросов включен по умолчанию.
    /// </summary>
    /// <param name="o">Объект, предоставляющий интерфейс.</param>
    /// <typeparam name="T">
    ///   Тип параметра <paramref name="o" />.
    /// </typeparam>
    /// <typeparam name="TInterface">
    ///   Тип возвращаемого интерфейса.
    /// </typeparam>
    /// <returns>
    ///   Указатель интерфейса, представляющий <paramref name="TInterface" /> интерфейса.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="TInterface" /> Параметр не является интерфейсом.
    /// 
    ///   -или-
    /// 
    ///   Тип не является видимым для COM.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="T" /> Параметр является открытым универсальным типом.
    /// </exception>
    /// <exception cref="T:System.InvalidCastException">
    ///   <paramref name="o" /> Не поддерживает параметр <paramref name="TInterface" /> интерфейса.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="o" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    public static IntPtr GetComInterfaceForObject<T, TInterface>(T o)
    {
      return Marshal.GetComInterfaceForObject((object) o, typeof (TInterface));
    }

    /// <summary>
    ///   Возвращает указатель на IUnknown представляющий указанный интерфейс для указанного объекта.
    ///    Доступ к настраиваемому интерфейсу запросов контролируется указанным режимом настройки.
    /// </summary>
    /// <param name="o">Объект, предоставляющий интерфейс.</param>
    /// <param name="T">Тип запрашиваемого интерфейса.</param>
    /// <param name="mode">
    ///   Одно из значений перечисления, указывающее, следует ли применить <see langword="IUnknown::QueryInterface" /> настройки, предоставляемые <see cref="T:System.Runtime.InteropServices.ICustomQueryInterface" />.
    /// </param>
    /// <returns>
    ///   Указатель интерфейса, представляющий интерфейс для объекта.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="T" /> Параметр не является интерфейсом.
    /// 
    ///   -или-
    /// 
    ///   Тип не является видимым для COM.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="T" /> имеет универсальный тип.
    /// </exception>
    /// <exception cref="T:System.InvalidCastException">
    ///   Объект <paramref name="o" /> не поддерживает запрошенный интерфейс.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="o" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="T" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    public static IntPtr GetComInterfaceForObject(object o, Type T, CustomQueryInterfaceMode mode)
    {
      bool fEnalbeCustomizedQueryInterface = mode == CustomQueryInterfaceMode.Allow;
      return Marshal.GetComInterfaceForObjectNative(o, T, false, fEnalbeCustomizedQueryInterface);
    }

    /// <summary>
    ///   Возвращает указатель интерфейса, представляющий заданный интерфейс для объекта, если вызывающий объект находится в том же контексте, что и данный объект.
    /// </summary>
    /// <param name="o">Объект, предоставляющий интерфейс.</param>
    /// <param name="t">Тип запрашиваемого интерфейса.</param>
    /// <returns>
    ///   Указатель интерфейса, заданный по <paramref name="t" /> представляющий интерфейс для указанного объекта или <see langword="null" /> Если вызывающий объект не находится в том же контексте, что и объект.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="t" /> не является интерфейсом.
    /// 
    ///   -или-
    /// 
    ///   Тип не является видимым для COM.
    /// </exception>
    /// <exception cref="T:System.InvalidCastException">
    ///   <paramref name="o" /> не поддерживает запрошенный интерфейс.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="o" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="t" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    public static IntPtr GetComInterfaceForObjectInContext(object o, Type t)
    {
      return Marshal.GetComInterfaceForObjectNative(o, t, true, true);
    }

    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern IntPtr GetComInterfaceForObjectNative(object o, Type t, bool onlyInContext, bool fEnalbeCustomizedQueryInterface);

    /// <summary>
    ///   Возвращает экземпляр типа, представляющий COM-объект, указатель на его IUnknown интерфейса.
    /// </summary>
    /// <param name="pUnk">
    ///   Указатель на <see langword="IUnknown" /> интерфейса.
    /// </param>
    /// <returns>
    ///   Объект, представляющий указанный неуправляемый COM-объект.
    /// </returns>
    [SecurityCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern object GetObjectForIUnknown(IntPtr pUnk);

    /// <summary>
    ///   Создает уникальный Runtime Callable Wrapper объект (времени выполнения RCW) для заданного IUnknown интерфейса.
    /// </summary>
    /// <param name="unknown">
    ///   Управляемый указатель <see langword="IUnknown" /> интерфейса.
    /// </param>
    /// <returns>
    ///   Уникальная оболочка RCW для указанного <see langword="IUnknown" /> интерфейса.
    /// </returns>
    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern object GetUniqueObjectForIUnknown(IntPtr unknown);

    /// <summary>
    ///   Возвращает управляемый объект указанного типа, представляющий COM-объект.
    /// </summary>
    /// <param name="pUnk">
    ///   Указатель на интерфейс <see langword="IUnknown" /> неуправляемого объекта.
    /// </param>
    /// <param name="t">Тип запрашиваемого управляемого класса.</param>
    /// <returns>
    ///   Экземпляр класса, который соответствует объекту <see cref="T:System.Type" />, представляющему запрошенный неуправляемый COM-объект.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="t" /> не отмечается атрибутом <see cref="T:System.Runtime.InteropServices.ComImportAttribute" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="t" /> относится к типу Среда выполнения Windows.
    /// </exception>
    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern object GetTypedObjectForIUnknown(IntPtr pUnk, Type t);

    /// <summary>
    ///   Объединяет управляемый объект с заданным COM-объектом.
    /// </summary>
    /// <param name="pOuter">
    ///   Внешний указатель <see langword="IUnknown" />.
    /// </param>
    /// <param name="o">Объект для объединения.</param>
    /// <returns>
    ///   Внутренний указатель <see langword="IUnknown" /> управляемого объекта.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="o" /> является объектом Среда выполнения Windows.
    /// </exception>
    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern IntPtr CreateAggregatedObject(IntPtr pOuter, object o);

    /// <summary>
    ///   [Поддерживается в .NET Framework 4.5.1 и более поздних версиях.]
    /// 
    ///   Объединяет управляемый объект указанного типа с заданным COM-объектом.
    /// </summary>
    /// <param name="pOuter">Внешний IUnknown указателя.</param>
    /// <param name="o">Управляемый объект для объединения.</param>
    /// <typeparam name="T">
    ///   Тип управляемого объекта для статистической обработки.
    /// </typeparam>
    /// <returns>Внутренний IUnknown указателя управляемого объекта.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="o" /> является объектом Среда выполнения Windows.
    /// </exception>
    [SecurityCritical]
    public static IntPtr CreateAggregatedObject<T>(IntPtr pOuter, T o)
    {
      return Marshal.CreateAggregatedObject(pOuter, (object) o);
    }

    /// <summary>
    ///   Предписывает среде выполнения очистить все вызываемые оболочки времени выполнения (RCW), выделенных в текущем контексте.
    /// </summary>
    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern void CleanupUnusedObjectsInCurrentContext();

    /// <summary>
    ///   Указывает, доступны ли для очистки вызываемые оболочки времени выполнения (RCW) из какого-либо контекста.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> При наличии все вызываемые оболочки времени выполнения для очистки; в противном случае — <see langword="false" />.
    /// </returns>
    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern bool AreComObjectsAvailableForCleanup();

    /// <summary>
    ///   Показывает, представляет ли указанный объект COM-объект.
    /// </summary>
    /// <param name="o">Объект для проверки.</param>
    /// <returns>
    ///   Значение <see langword="true" />, если параметр <paramref name="o" /> является COM-типом. В противном случае значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="o" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern bool IsComObject(object o);

    /// <summary>
    ///   Выделяет блок памяти указанного размера из механизма распределения памяти для задач COM.
    /// </summary>
    /// <param name="cb">Размер выделяемого блока памяти.</param>
    /// <returns>
    ///   Целое число, представляющее адрес выделенного блока памяти.
    ///    Эта память освобождается с <see cref="M:System.Runtime.InteropServices.Marshal.FreeCoTaskMem(System.IntPtr)" />.
    /// </returns>
    /// <exception cref="T:System.OutOfMemoryException">
    ///   Недостаточно памяти для выполнения запроса.
    /// </exception>
    [SecurityCritical]
    public static IntPtr AllocCoTaskMem(int cb)
    {
      IntPtr num = Win32Native.CoTaskMemAlloc(new UIntPtr((uint) cb));
      if (num == IntPtr.Zero)
        throw new OutOfMemoryException();
      return num;
    }

    /// <summary>
    ///   Копирует содержимое управляемого объекта <see cref="T:System.String" /> блок памяти, выделенный из неуправляемого распределителя COM задач.
    /// </summary>
    /// <param name="s">Копируемая управляемая строка.</param>
    /// <returns>
    ///   Целое число, представляющее указатель на блок памяти, выделенный для строки, или 0, если s — <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="s" /> Параметра превышает максимальную длину, допускаемый операционной системы.
    /// </exception>
    /// <exception cref="T:System.OutOfMemoryException">
    ///   Не хватает памяти.
    /// </exception>
    [SecurityCritical]
    public static unsafe IntPtr StringToCoTaskMemUni(string s)
    {
      if (s == null)
        return IntPtr.Zero;
      int num1 = (s.Length + 1) * 2;
      if (num1 < s.Length)
        throw new ArgumentOutOfRangeException(nameof (s));
      IntPtr num2 = Win32Native.CoTaskMemAlloc(new UIntPtr((uint) num1));
      if (num2 == IntPtr.Zero)
        throw new OutOfMemoryException();
      string str = s;
      char* smem = (char*) str;
      if ((IntPtr) smem != IntPtr.Zero)
        smem += RuntimeHelpers.OffsetToStringData;
      string.wstrcpy((char*) (void*) num2, smem, s.Length + 1);
      str = (string) null;
      return num2;
    }

    /// <summary>
    ///   Копирует содержимое управляемого объекта <see cref="T:System.String" /> блок памяти, выделенный из неуправляемого распределителя COM задач.
    /// </summary>
    /// <param name="s">Копируемая управляемая строка.</param>
    /// <returns>
    ///   Выделенный блок памяти или 0, если <paramref name="s" /> является <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.OutOfMemoryException">
    ///   Не хватает памяти.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Длина параметра <paramref name="s" /> выходит за пределы диапазона.
    /// </exception>
    [SecurityCritical]
    public static IntPtr StringToCoTaskMemAuto(string s)
    {
      return Marshal.StringToCoTaskMemUni(s);
    }

    /// <summary>
    ///   Копирует содержимое управляемого объекта <see cref="T:System.String" /> блок памяти, выделенный из неуправляемого распределителя COM задач.
    /// </summary>
    /// <param name="s">Копируемая управляемая строка.</param>
    /// <returns>
    ///   Целое число, представляющее указатель на блок памяти, выделенный для строки, или 0, если <paramref name="s" /> является <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.OutOfMemoryException">
    ///   Не хватает памяти.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="s" /> Параметра превышает максимальную длину, допускаемый операционной системы.
    /// </exception>
    [SecurityCritical]
    public static unsafe IntPtr StringToCoTaskMemAnsi(string s)
    {
      if (s == null)
        return IntPtr.Zero;
      int cbNativeBuffer = (s.Length + 1) * Marshal.SystemMaxDBCSCharSize;
      if (cbNativeBuffer < s.Length)
        throw new ArgumentOutOfRangeException(nameof (s));
      IntPtr num = Win32Native.CoTaskMemAlloc(new UIntPtr((uint) cbNativeBuffer));
      if (num == IntPtr.Zero)
        throw new OutOfMemoryException();
      s.ConvertToAnsi((byte*) (void*) num, cbNativeBuffer, false, false);
      return num;
    }

    /// <summary>
    ///   Освобождает блок памяти, выделенный неуправляемым механизмом распределения памяти для задач COM.
    /// </summary>
    /// <param name="ptr">Адрес освобождаемой памяти.</param>
    [SecurityCritical]
    public static void FreeCoTaskMem(IntPtr ptr)
    {
      if (!Marshal.IsNotWin32Atom(ptr))
        return;
      Win32Native.CoTaskMemFree(ptr);
    }

    /// <summary>
    ///   Изменяет размер блока памяти, предварительно выделенного <see cref="M:System.Runtime.InteropServices.Marshal.AllocCoTaskMem(System.Int32)" />.
    /// </summary>
    /// <param name="pv">
    ///   Указатель на память, выделенную с <see cref="M:System.Runtime.InteropServices.Marshal.AllocCoTaskMem(System.Int32)" />.
    /// </param>
    /// <param name="cb">Новый размер выделенного блока.</param>
    /// <returns>
    ///   Целое число, представляющее адрес повторно выделенного блока памяти.
    ///    Эта память освобождается с <see cref="M:System.Runtime.InteropServices.Marshal.FreeCoTaskMem(System.IntPtr)" />.
    /// </returns>
    /// <exception cref="T:System.OutOfMemoryException">
    ///   Недостаточно памяти для выполнения запроса.
    /// </exception>
    [SecurityCritical]
    public static IntPtr ReAllocCoTaskMem(IntPtr pv, int cb)
    {
      IntPtr num = Win32Native.CoTaskMemRealloc(pv, new UIntPtr((uint) cb));
      if (num == IntPtr.Zero && cb != 0)
        throw new OutOfMemoryException();
      return num;
    }

    /// <summary>
    ///   Уменьшает счетчик ссылок оболочки Runtime Callable Wrapper (RCW), связанной с указанным COM-объектом.
    /// </summary>
    /// <param name="o">Освобождаемый COM-объект.</param>
    /// <returns>
    ///   Новое значение счетчика ссылок оболочки RCW, связанной с <paramref name="o" />.
    ///    Это значение обычно равно нулю, поскольку оболочка RCW хранит только одну ссылку на COM-объект в оболочке вне зависимости от количества управляемых клиентов, которые ее вызывают.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="o" /> не является допустимым COM-объектом.
    /// </exception>
    /// <exception cref="T:System.NullReferenceException">
    ///   Свойство <paramref name="o" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    public static int ReleaseComObject(object o)
    {
      __ComObject comObject;
      try
      {
        comObject = (__ComObject) o;
      }
      catch (InvalidCastException ex)
      {
        throw new ArgumentException(Environment.GetResourceString("Argument_ObjNotComObject"), nameof (o));
      }
      return comObject.ReleaseSelf();
    }

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern int InternalReleaseComObject(object o);

    /// <summary>
    ///   Освобождает все ссылки на Runtime Callable Wrapper (времени выполнения RCW), установив число ссылок равным 0.
    /// </summary>
    /// <param name="o">Освобождаемая оболочка CLR.</param>
    /// <returns>
    ///   Новое значение счетчика ссылок оболочки RCW, связанной с <paramref name="o" />параметра, равное 0 (нуль), если освобождение прошло успешно.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="o" /> не является COM-объектом.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="o" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    public static int FinalReleaseComObject(object o)
    {
      if (o == null)
        throw new ArgumentNullException(nameof (o));
      __ComObject comObject;
      try
      {
        comObject = (__ComObject) o;
      }
      catch (InvalidCastException ex)
      {
        throw new ArgumentException(Environment.GetResourceString("Argument_ObjNotComObject"), nameof (o));
      }
      comObject.FinalReleaseSelf();
      return 0;
    }

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void InternalFinalReleaseComObject(object o);

    /// <summary>
    ///   Извлекает данные, на которые ссылается заданный ключ из указанного COM-объекта.
    /// </summary>
    /// <param name="obj">COM-объект, содержащий требуемые данные.</param>
    /// <param name="key">
    ///   Ключ во внутренней хэш-таблице <paramref name="obj" /> для извлечения данных.
    /// </param>
    /// <returns>
    ///   Данные, представленные параметром <paramref name="key" /> в параметре <paramref name="obj" /> внутренней хэш-таблице.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="obj" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="key" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="obj" /> не является COM-объектом.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="obj" /> является объектом Среда выполнения Windows.
    /// </exception>
    [SecurityCritical]
    public static object GetComObjectData(object obj, object key)
    {
      if (obj == null)
        throw new ArgumentNullException(nameof (obj));
      if (key == null)
        throw new ArgumentNullException(nameof (key));
      __ComObject comObject;
      try
      {
        comObject = (__ComObject) obj;
      }
      catch (InvalidCastException ex)
      {
        throw new ArgumentException(Environment.GetResourceString("Argument_ObjNotComObject"), nameof (obj));
      }
      if (obj.GetType().IsWindowsRuntimeObject)
        throw new ArgumentException(Environment.GetResourceString("Argument_ObjIsWinRTObject"), nameof (obj));
      return comObject.GetData(key);
    }

    /// <summary>
    ///   Определяет данные, на которые ссылается заданный ключ в указанном COM-объекте.
    /// </summary>
    /// <param name="obj">COM-объект для хранения данных.</param>
    /// <param name="key">
    ///   Ключ во внутренней хэш-таблице COM-объекта, в котором хранятся данные.
    /// </param>
    /// <param name="data">Задаваемые данные.</param>
    /// <returns>
    ///   Значение <see langword="true" />, если данные заданы успешно; в противном случае значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="obj" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="key" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="obj" /> не является COM-объектом.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="obj" /> является объектом Среда выполнения Windows.
    /// </exception>
    [SecurityCritical]
    public static bool SetComObjectData(object obj, object key, object data)
    {
      if (obj == null)
        throw new ArgumentNullException(nameof (obj));
      if (key == null)
        throw new ArgumentNullException(nameof (key));
      __ComObject comObject;
      try
      {
        comObject = (__ComObject) obj;
      }
      catch (InvalidCastException ex)
      {
        throw new ArgumentException(Environment.GetResourceString("Argument_ObjNotComObject"), nameof (obj));
      }
      if (obj.GetType().IsWindowsRuntimeObject)
        throw new ArgumentException(Environment.GetResourceString("Argument_ObjIsWinRTObject"), nameof (obj));
      return comObject.SetData(key, data);
    }

    /// <summary>
    ///   Инкапсулирует указанный COM-объект в объекте заданного типа.
    /// </summary>
    /// <param name="o">Объект, который следует упаковать.</param>
    /// <param name="t">Тип создаваемой оболочки.</param>
    /// <returns>
    ///   Новый инкапсулированный объект, являющийся экземпляром нужного типа.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="t" /> должен быть производным от <see langword="__ComObject" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="t" /> относится к типу Среда выполнения Windows.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="t" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidCastException">
    ///   Параметр <paramref name="o" /> нельзя преобразовать в результирующий тип, так как он не поддерживает все требуемые интерфейсы.
    /// </exception>
    [SecurityCritical]
    public static object CreateWrapperOfType(object o, Type t)
    {
      if (t == (Type) null)
        throw new ArgumentNullException(nameof (t));
      if (!t.IsCOMObject)
        throw new ArgumentException(Environment.GetResourceString("Argument_TypeNotComObject"), nameof (t));
      if (t.IsGenericType)
        throw new ArgumentException(Environment.GetResourceString("Argument_NeedNonGenericType"), nameof (t));
      if (t.IsWindowsRuntimeObject)
        throw new ArgumentException(Environment.GetResourceString("Argument_TypeIsWinRTType"), nameof (t));
      if (o == null)
        return (object) null;
      if (!o.GetType().IsCOMObject)
        throw new ArgumentException(Environment.GetResourceString("Argument_ObjNotComObject"), nameof (o));
      if (o.GetType().IsWindowsRuntimeObject)
        throw new ArgumentException(Environment.GetResourceString("Argument_ObjIsWinRTObject"), nameof (o));
      if (o.GetType() == t)
        return o;
      object data = Marshal.GetComObjectData(o, (object) t);
      if (data == null)
      {
        data = Marshal.InternalCreateWrapperOfType(o, t);
        if (!Marshal.SetComObjectData(o, (object) t, data))
          data = Marshal.GetComObjectData(o, (object) t);
      }
      return data;
    }

    /// <summary>
    ///   [Поддерживается в .NET Framework 4.5.1 и более поздних версиях.]
    /// 
    ///   Инкапсулирует указанный COM-объект в объекте заданного типа.
    /// </summary>
    /// <param name="o">Инкапсулируемый объект.</param>
    /// <typeparam name="T">
    ///   Тип объекта, для которого создается оболочка.
    /// </typeparam>
    /// <typeparam name="TWrapper">Тип возвращаемого объекта.</typeparam>
    /// <returns>Преобразованный объект.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="T" /> должен быть производным от <see langword="__ComObject" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="T" /> является Среда выполнения Windows типа.
    /// </exception>
    /// <exception cref="T:System.InvalidCastException">
    ///   <paramref name="o" /> не удается преобразовать <paramref name="TWrapper" /> так как он не поддерживает все необходимые интерфейсы.
    /// </exception>
    [SecurityCritical]
    public static TWrapper CreateWrapperOfType<T, TWrapper>(T o)
    {
      return (TWrapper) Marshal.CreateWrapperOfType((object) o, typeof (TWrapper));
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern object InternalCreateWrapperOfType(object o, Type t);

    /// <summary>Освобождает кэш потока.</summary>
    [SecurityCritical]
    [Obsolete("This API did not perform any operation and will be removed in future versions of the CLR.", false)]
    public static void ReleaseThreadCache()
    {
    }

    /// <summary>
    ///   Показывает, является ли тип видимым для клиентов COM.
    /// </summary>
    /// <param name="t">
    ///   Тип, для которого требуется проверить видимость для COM.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если тип видим для COM; в противном случае — <see langword="false" />.
    /// </returns>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern bool IsTypeVisibleFromCom(Type t);

    /// <summary>
    ///   Запрашивает указатель на заданный интерфейс из COM-объекта.
    /// </summary>
    /// <param name="pUnk">Запрашиваемый интерфейс.</param>
    /// <param name="iid">
    ///   Идентификатор IID запрошенного интерфейса.
    /// </param>
    /// <param name="ppv">
    ///   Когда этот метод возвращает результаты, в них содержится ссылка на возвращенный интерфейс.
    /// </param>
    /// <returns>
    ///   Значение HRESULT, показывающее, успешно ли выполнен вызов.
    /// </returns>
    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern int QueryInterface(IntPtr pUnk, ref Guid iid, out IntPtr ppv);

    /// <summary>
    ///   Увеличивает счетчик ссылок для указанного интерфейса.
    /// </summary>
    /// <param name="pUnk">
    ///   Увеличиваемый счетчик ссылок интерфейса.
    /// </param>
    /// <returns>
    ///   Ссылки на новое значение счетчика для <paramref name="pUnk" /> параметр.
    /// </returns>
    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern int AddRef(IntPtr pUnk);

    /// <summary>Уменьшает счетчик ссылок для указанного интерфейса.</summary>
    /// <param name="pUnk">Освобождаемый интерфейс.</param>
    /// <returns>
    ///   Новое значение ссылки счетчика для интерфейса, указанного параметром <paramref name="pUnk" /> параметр.
    /// </returns>
    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern int Release(IntPtr pUnk);

    /// <summary>
    ///   Освобождает <see langword="BSTR" /> с помощью функции COM SysFreeString функции.
    /// </summary>
    /// <param name="ptr">Адрес освобождаемой строки BSTR.</param>
    [SecurityCritical]
    [__DynamicallyInvokable]
    public static void FreeBSTR(IntPtr ptr)
    {
      if (!Marshal.IsNotWin32Atom(ptr))
        return;
      Win32Native.SysFreeString(ptr);
    }

    /// <summary>
    ///   Выделяет BSTR и копирует содержимое управляемого объекта <see cref="T:System.String" /> в него.
    /// </summary>
    /// <param name="s">Копируемая управляемая строка.</param>
    /// <returns>
    ///   Неуправляемый указатель на строку <see langword="BSTR" /> или значение 0, если параметр <paramref name="s" /> имеет значение null.
    /// </returns>
    /// <exception cref="T:System.OutOfMemoryException">
    ///   Не хватает памяти.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Длина параметра <paramref name="s" /> выходит за пределы диапазона.
    /// </exception>
    [SecurityCritical]
    [__DynamicallyInvokable]
    public static IntPtr StringToBSTR(string s)
    {
      if (s == null)
        return IntPtr.Zero;
      if (s.Length + 1 < s.Length)
        throw new ArgumentOutOfRangeException(nameof (s));
      IntPtr num = Win32Native.SysAllocStringLen(s, s.Length);
      if (num == IntPtr.Zero)
        throw new OutOfMemoryException();
      return num;
    }

    /// <summary>
    ///   Выделяет управляемый <see cref="T:System.String" /> и копирует binary string (BSTR) хранящуюся в неуправляемой памяти в него.
    /// </summary>
    /// <param name="ptr">
    ///   Адрес первого символа в неуправляемой строке.
    /// </param>
    /// <returns>
    ///   Управляемая строка, содержащая копию неуправляемой строки.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="ptr" /> равняется <see cref="F:System.IntPtr.Zero" />.
    /// </exception>
    [SecurityCritical]
    [__DynamicallyInvokable]
    public static string PtrToStringBSTR(IntPtr ptr)
    {
      return Marshal.PtrToStringUni(ptr, (int) Win32Native.SysStringLen(ptr));
    }

    /// <summary>Преобразует объект в COM VARIANT.</summary>
    /// <param name="obj">
    ///   Объект, для которого нужно получить COM VARIANT.
    /// </param>
    /// <param name="pDstNativeVariant">
    ///   Указатель, получающий тип VARIANT, соответствующий параметру <paramref name="obj" />.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="obj" /> имеет универсальный тип.
    /// </exception>
    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern void GetNativeVariantForObject(object obj, IntPtr pDstNativeVariant);

    /// <summary>
    ///   [Поддерживается в .NET Framework 4.5.1 и более поздних версиях.]
    /// 
    ///   Преобразует объект заданного типа в COM VARIANT.
    /// </summary>
    /// <param name="obj">
    ///   Объект, для которого нужно получить COM VARIANT.
    /// </param>
    /// <param name="pDstNativeVariant">
    ///   Указатель, получающий тип VARIANT, соответствующий параметру <paramref name="obj" />.
    /// </param>
    /// <typeparam name="T">Тип объекта для преобразования.</typeparam>
    [SecurityCritical]
    public static void GetNativeVariantForObject<T>(T obj, IntPtr pDstNativeVariant)
    {
      Marshal.GetNativeVariantForObject((object) obj, pDstNativeVariant);
    }

    /// <summary>Преобразует COM VARIANT в объект.</summary>
    /// <param name="pSrcNativeVariant">Указатель на COM VARIANT.</param>
    /// <returns>
    ///   Объект, который соответствует <paramref name="pSrcNativeVariant" /> параметр.
    /// </returns>
    /// <exception cref="T:System.Runtime.InteropServices.InvalidOleVariantTypeException">
    ///   <paramref name="pSrcNativeVariant" /> не является допустимым типом VARIANT.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <paramref name="pSrcNativeVariant" /> имеет неподдерживаемый тип.
    /// </exception>
    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern object GetObjectForNativeVariant(IntPtr pSrcNativeVariant);

    /// <summary>
    ///   [Поддерживается в .NET Framework 4.5.1 и более поздних версиях.]
    /// 
    ///   Преобразует COM VARIANT в объект заданного типа.
    /// </summary>
    /// <param name="pSrcNativeVariant">Указатель на COM VARIANT.</param>
    /// <typeparam name="T">
    ///   Тип, в который требуется преобразовать COM VARIANT.
    /// </typeparam>
    /// <returns>
    ///   Объект типа, который соответствует <paramref name="pSrcNativeVariant" /> параметр.
    /// </returns>
    /// <exception cref="T:System.Runtime.InteropServices.InvalidOleVariantTypeException">
    ///   <paramref name="pSrcNativeVariant" /> не является допустимым типом VARIANT.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <paramref name="pSrcNativeVariant" /> имеет неподдерживаемый тип.
    /// </exception>
    [SecurityCritical]
    public static T GetObjectForNativeVariant<T>(IntPtr pSrcNativeVariant)
    {
      return (T) Marshal.GetObjectForNativeVariant(pSrcNativeVariant);
    }

    /// <summary>
    ///   Преобразует массив типа COM VARIANTs в массив объектов.
    /// </summary>
    /// <param name="aSrcNativeVariant">
    ///   Указатель на первый элемент массива типа COM VARIANT.
    /// </param>
    /// <param name="cVars">
    ///   Число типа COM VARIANT в <paramref name="aSrcNativeVariant" />.
    /// </param>
    /// <returns>
    ///   Массив объектов, которые соответствуют <paramref name="aSrcNativeVariant" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="cVars" /> является отрицательным числом.
    /// </exception>
    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern object[] GetObjectsForNativeVariants(IntPtr aSrcNativeVariant, int cVars);

    /// <summary>
    ///   [Поддерживается в .NET Framework 4.5.1 и более поздних версиях.]
    /// 
    ///   Преобразует массив типа COM VARIANT в массив указанного типа.
    /// </summary>
    /// <param name="aSrcNativeVariant">
    ///   Указатель на первый элемент массива типа COM VARIANT.
    /// </param>
    /// <param name="cVars">
    ///   Число типа COM VARIANT в <paramref name="aSrcNativeVariant" />.
    /// </param>
    /// <typeparam name="T">Тип возвращаемого массива.</typeparam>
    /// <returns>
    ///   Массив <paramref name="T" /> объекты, которые соответствуют <paramref name="aSrcNativeVariant" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="cVars" /> является отрицательным числом.
    /// </exception>
    [SecurityCritical]
    public static T[] GetObjectsForNativeVariants<T>(IntPtr aSrcNativeVariant, int cVars)
    {
      object[] forNativeVariants = Marshal.GetObjectsForNativeVariants(aSrcNativeVariant, cVars);
      T[] objArray = (T[]) null;
      if (forNativeVariants != null)
      {
        objArray = new T[forNativeVariants.Length];
        Array.Copy((Array) forNativeVariants, (Array) objArray, forNativeVariants.Length);
      }
      return objArray;
    }

    /// <summary>
    ///   Возвращает первую ячейку в таблице виртуальных функций (VTBL), которая содержит методы, определенные пользователем.
    /// </summary>
    /// <param name="t">Тип, представляющий интерфейс.</param>
    /// <returns>
    ///   Первая ячейка таблицы виртуальных функций (VTBL), содержащая методы, определенные пользователем.
    ///    Первая ячейка содержит 3, если интерфейс основан на IUnknown, и 7, если интерфейс основан на IDispatch.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="t" /> не является видимым для COM.
    /// </exception>
    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern int GetStartComSlot(Type t);

    /// <summary>
    ///   Извлекает последнюю ячейку таблицы виртуальных функций (VTBL) для типа, если он предоставлен модели COM.
    /// </summary>
    /// <param name="t">Тип, представляющий интерфейс или класс.</param>
    /// <returns>
    ///   Последняя ячейка таблицы виртуальных функций (VTBL) интерфейса, если он предоставлен модели COM.
    ///    Если <paramref name="t" /> параметр — это класс, возвращаемый ячейка таблицы VTBL будет последней ячейкой для интерфейса, созданного из класса.
    /// </returns>
    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern int GetEndComSlot(Type t);

    /// <summary>
    ///   Извлекает <see cref="T:System.Reflection.MemberInfo" /> объекта для области данных таблицы (таблица v или VTBL) указанной виртуальной функции.
    /// </summary>
    /// <param name="t">
    ///   Тип, для которого <see cref="T:System.Reflection.MemberInfo" /> требуется получить.
    /// </param>
    /// <param name="slot">Ячейка таблицы VTBL.</param>
    /// <param name="memberType">
    ///   Для успешного возврата значения содержит одно из значений перечисления, указывающее тип члена.
    /// </param>
    /// <returns>
    ///   Объект, представляющий элемент из указанной ячейки таблицы виртуальных функций (VTBL).
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="t" /> не является видимым для COM.
    /// </exception>
    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern MemberInfo GetMethodInfoForComSlot(Type t, int slot, ref ComMemberType memberType);

    /// <summary>
    ///   Извлекает ячейку таблицы (таблица v или VTBL) виртуальную функцию для указанного <see cref="T:System.Reflection.MemberInfo" /> Введите при предоставлении типа COM.
    /// </summary>
    /// <param name="m">Объект, представляющий метод интерфейса.</param>
    /// <returns>
    ///   Ячейка таблицы VTBL <paramref name="m" /> идентификатор, если он предоставлен модели COM.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="m" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="m" /> Параметр не <see cref="T:System.Reflection.MemberInfo" /> объекта.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="m" /> Параметр не является методом интерфейса.
    /// </exception>
    [SecurityCritical]
    public static int GetComSlotForMethodInfo(MemberInfo m)
    {
      if (m == (MemberInfo) null)
        throw new ArgumentNullException(nameof (m));
      if (!(m is RuntimeMethodInfo))
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeMethodInfo"), nameof (m));
      if (!m.DeclaringType.IsInterface)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeInterfaceMethod"), nameof (m));
      if (m.DeclaringType.IsGenericType)
        throw new ArgumentException(Environment.GetResourceString("Argument_NeedNonGenericType"), nameof (m));
      return Marshal.InternalGetComSlotForMethodInfo((IRuntimeMethodInfo) m);
    }

    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern int InternalGetComSlotForMethodInfo(IRuntimeMethodInfo m);

    /// <summary>
    ///   Возвращает уникальный глобальный идентификатор GUID для указанного типа или создает идентификатор GUID при помощи алгоритма, используемого средством экспортирования библиотек типов Tlbexp.exe.
    /// </summary>
    /// <param name="type">Тип, создается идентификатор GUID.</param>
    /// <returns>Идентификатор для указанного типа.</returns>
    [SecurityCritical]
    public static Guid GenerateGuidForType(Type type)
    {
      Guid result = new Guid();
      Marshal.FCallGenerateGuidForType(ref result, type);
      return result;
    }

    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void FCallGenerateGuidForType(ref Guid result, Type type);

    /// <summary>
    ///   Возвращает программный идентификатор ProgID для указанного типа.
    /// </summary>
    /// <param name="type">
    ///   Тип, чтобы получить идентификатор ProgID.
    /// </param>
    /// <returns>Идентификатор ProgID для указанного типа.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="type" /> Параметр не является класс, который может быть создан с помощью COM.
    ///    Класс должен быть открытым, иметь открытый конструктор по умолчанию и быть видимыми для COM.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="type" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    public static string GenerateProgIdForType(Type type)
    {
      if (type == (Type) null)
        throw new ArgumentNullException(nameof (type));
      if (type.IsImport)
        throw new ArgumentException(Environment.GetResourceString("Argument_TypeMustNotBeComImport"), nameof (type));
      if (type.IsGenericType)
        throw new ArgumentException(Environment.GetResourceString("Argument_NeedNonGenericType"), nameof (type));
      if (!RegistrationServices.TypeRequiresRegistrationHelper(type))
        throw new ArgumentException(Environment.GetResourceString("Argument_TypeMustBeComCreatable"), nameof (type));
      IList<CustomAttributeData> customAttributes = CustomAttributeData.GetCustomAttributes((MemberInfo) type);
      for (int index = 0; index < customAttributes.Count; ++index)
      {
        if (customAttributes[index].Constructor.DeclaringType == typeof (ProgIdAttribute))
          return (string) customAttributes[index].ConstructorArguments[0].Value ?? string.Empty;
      }
      return type.FullName;
    }

    /// <summary>
    ///   Возвращает указатель интерфейса, определенный указанным моникером.
    /// </summary>
    /// <param name="monikerName">
    ///   Моникер, соответствующий указателю нужного интерфейса.
    /// </param>
    /// <returns>
    ///   Объект, содержащий ссылку на указатель интерфейса, идентифицируемый параметром <paramref name="monikerName" />.
    ///    Моникер — это имя, в данном случае определенное интерфейсом.
    /// </returns>
    /// <exception cref="T:System.Runtime.InteropServices.COMException">
    ///   Нераспознанное значение HRESULT возвращено неуправляемым методом <see langword="BindToMoniker" />.
    /// </exception>
    [SecurityCritical]
    public static object BindToMoniker(string monikerName)
    {
      object ppvResult = (object) null;
      IBindCtx ppbc = (IBindCtx) null;
      Marshal.CreateBindCtx(0U, out ppbc);
      IMoniker ppmk = (IMoniker) null;
      uint pchEaten;
      Marshal.MkParseDisplayName(ppbc, monikerName, out pchEaten, out ppmk);
      Marshal.BindMoniker(ppmk, 0U, ref Marshal.IID_IUnknown, out ppvResult);
      return ppvResult;
    }

    /// <summary>
    ///   Получает выполняющийся экземпляр указанного объекта из таблицы запущенных объектов (ROT).
    /// </summary>
    /// <param name="progID">
    ///   Программный идентификатор (ProgID) запрошенного объекта.
    /// </param>
    /// <returns>
    ///   Запрошенный объект; в противном случае — значение <see langword="null" />.
    ///    Этот объект может быть приведен к любому интерфейсу COM, поддерживаемому этим объектом.
    /// </returns>
    /// <exception cref="T:System.Runtime.InteropServices.COMException">
    ///   Объект не найден.
    /// </exception>
    [SecurityCritical]
    public static object GetActiveObject(string progID)
    {
      object ppunk = (object) null;
      Guid clsid;
      try
      {
        Marshal.CLSIDFromProgIDEx(progID, out clsid);
      }
      catch (Exception ex)
      {
        Marshal.CLSIDFromProgID(progID, out clsid);
      }
      Marshal.GetActiveObject(ref clsid, IntPtr.Zero, out ppunk);
      return ppunk;
    }

    [SuppressUnmanagedCodeSecurity]
    [SecurityCritical]
    [DllImport("ole32.dll", PreserveSig = false)]
    private static extern void CLSIDFromProgIDEx([MarshalAs(UnmanagedType.LPWStr)] string progId, out Guid clsid);

    [SuppressUnmanagedCodeSecurity]
    [SecurityCritical]
    [DllImport("ole32.dll", PreserveSig = false)]
    private static extern void CLSIDFromProgID([MarshalAs(UnmanagedType.LPWStr)] string progId, out Guid clsid);

    [SuppressUnmanagedCodeSecurity]
    [SecurityCritical]
    [DllImport("ole32.dll", PreserveSig = false)]
    private static extern void CreateBindCtx(uint reserved, out IBindCtx ppbc);

    [SuppressUnmanagedCodeSecurity]
    [SecurityCritical]
    [DllImport("ole32.dll", PreserveSig = false)]
    private static extern void MkParseDisplayName(IBindCtx pbc, [MarshalAs(UnmanagedType.LPWStr)] string szUserName, out uint pchEaten, out IMoniker ppmk);

    [SuppressUnmanagedCodeSecurity]
    [SecurityCritical]
    [DllImport("ole32.dll", PreserveSig = false)]
    private static extern void BindMoniker(IMoniker pmk, uint grfOpt, ref Guid iidResult, [MarshalAs(UnmanagedType.Interface)] out object ppvResult);

    [SuppressUnmanagedCodeSecurity]
    [SecurityCritical]
    [DllImport("oleaut32.dll", PreserveSig = false)]
    private static extern void GetActiveObject(ref Guid rclsid, IntPtr reserved, [MarshalAs(UnmanagedType.Interface)] out object ppunk);

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool InternalSwitchCCW(object oldtp, object newtp);

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern object InternalWrapIUnknownWithComObject(IntPtr i);

    [SecurityCritical]
    private static IntPtr LoadLicenseManager()
    {
      Type type = Assembly.Load("System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089").GetType("System.ComponentModel.LicenseManager");
      if (type == (Type) null || !type.IsVisible)
        return IntPtr.Zero;
      return type.TypeHandle.Value;
    }

    /// <summary>
    ///   Изменяет строгость объекта COM Callable Wrapper дескриптор (CCW).
    /// </summary>
    /// <param name="otp">
    ///   Объект, CCW-оболочка которого содержит дескриптор счетчика ссылок.
    ///    Этот дескриптор является строгим, если значение счетчика CCW-оболочки больше нуля; в противном случае он является слабым.
    /// </param>
    /// <param name="fIsWeak">
    ///   <see langword="true" /> изменение стойкости дескриптора на <paramref name="otp" /> параметра слабого независимо от счетчика ссылок; <see langword="false" /> Чтобы сбросить значение строгости дескриптора на <paramref name="otp" /> быть счетчик ссылок.
    /// </param>
    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern void ChangeWrapperHandleStrength(object otp, bool fIsWeak);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void InitializeWrapperForWinRT(object o, ref IntPtr pUnk);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void InitializeManagedWinRTFactoryObject(object o, RuntimeType runtimeClassType);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern object GetNativeActivationFactory(Type type);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void _GetInspectableIids(ObjectHandleOnStack obj, ObjectHandleOnStack guids);

    [SecurityCritical]
    internal static Guid[] GetInspectableIids(object obj)
    {
      Guid[] o1 = (Guid[]) null;
      __ComObject o2 = obj as __ComObject;
      if (o2 != null)
        Marshal._GetInspectableIids(JitHelpers.GetObjectHandleOnStack<__ComObject>(ref o2), JitHelpers.GetObjectHandleOnStack<Guid[]>(ref o1));
      return o1;
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void _GetCachedWinRTTypeByIid(ObjectHandleOnStack appDomainObj, Guid iid, out IntPtr rthHandle);

    [SecurityCritical]
    internal static Type GetCachedWinRTTypeByIid(AppDomain ad, Guid iid)
    {
      IntPtr rthHandle;
      Marshal._GetCachedWinRTTypeByIid(JitHelpers.GetObjectHandleOnStack<AppDomain>(ref ad), iid, out rthHandle);
      return (Type) Type.GetTypeFromHandleUnsafe(rthHandle);
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void _GetCachedWinRTTypes(ObjectHandleOnStack appDomainObj, ref int epoch, ObjectHandleOnStack winrtTypes);

    [SecurityCritical]
    internal static Type[] GetCachedWinRTTypes(AppDomain ad, ref int epoch)
    {
      IntPtr[] o = (IntPtr[]) null;
      Marshal._GetCachedWinRTTypes(JitHelpers.GetObjectHandleOnStack<AppDomain>(ref ad), ref epoch, JitHelpers.GetObjectHandleOnStack<IntPtr[]>(ref o));
      Type[] typeArray = new Type[o.Length];
      for (int index = 0; index < o.Length; ++index)
        typeArray[index] = (Type) Type.GetTypeFromHandleUnsafe(o[index]);
      return typeArray;
    }

    [SecurityCritical]
    internal static Type[] GetCachedWinRTTypes(AppDomain ad)
    {
      int epoch = 0;
      return Marshal.GetCachedWinRTTypes(ad, ref epoch);
    }

    /// <summary>
    ///   Преобразует указатель на неуправляемую функцию в делегат.
    /// </summary>
    /// <param name="ptr">
    ///   Указатель на неуправляемую функцию, который требуется преобразовать.
    /// </param>
    /// <param name="t">Тип возвращаемого делегата.</param>
    /// <returns>
    ///   Экземпляр делегата, который может быть приведен к соответствующему типу делегата.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="t" /> Параметр не является делегатом или является универсальным.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="ptr" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="t" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    public static Delegate GetDelegateForFunctionPointer(IntPtr ptr, Type t)
    {
      if (ptr == IntPtr.Zero)
        throw new ArgumentNullException(nameof (ptr));
      if (t == (Type) null)
        throw new ArgumentNullException(nameof (t));
      if (t as RuntimeType == (RuntimeType) null)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"), nameof (t));
      if (t.IsGenericType)
        throw new ArgumentException(Environment.GetResourceString("Argument_NeedNonGenericType"), nameof (t));
      Type baseType = t.BaseType;
      if (baseType == (Type) null || baseType != typeof (Delegate) && baseType != typeof (MulticastDelegate))
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeDelegate"), nameof (t));
      return Marshal.GetDelegateForFunctionPointerInternal(ptr, t);
    }

    /// <summary>
    ///   [Поддерживается в .NET Framework 4.5.1 и более поздних версиях.]
    /// 
    ///   Преобразует указатель на неуправляемую функцию в делегат указанного типа.
    /// </summary>
    /// <param name="ptr">
    ///   Указатель на неуправляемую функцию для преобразования.
    /// </param>
    /// <typeparam name="TDelegate">
    ///   Тип возвращаемого делегата.
    /// </typeparam>
    /// <returns>Экземпляр делегата указанного типа.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="TDelegate" /> Универсальный параметр не является делегатом, или он является открытым универсальным типом.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="ptr" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    public static TDelegate GetDelegateForFunctionPointer<TDelegate>(IntPtr ptr)
    {
      return (TDelegate) Marshal.GetDelegateForFunctionPointer(ptr, typeof (TDelegate));
    }

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern Delegate GetDelegateForFunctionPointerInternal(IntPtr ptr, Type t);

    /// <summary>
    ///   Преобразует делегат в указатель на функцию, вызываемый из неуправляемого кода.
    /// </summary>
    /// <param name="d">Делегат, передаваемый в неуправляемый код.</param>
    /// <returns>
    ///   Значение, который может быть передан в неуправляемый код, который, в свою очередь, может использовать его для вызова базового управляемого делегата.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="d" /> имеет универсальный тип.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="d" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    public static IntPtr GetFunctionPointerForDelegate(Delegate d)
    {
      if ((object) d == null)
        throw new ArgumentNullException(nameof (d));
      return Marshal.GetFunctionPointerForDelegateInternal(d);
    }

    /// <summary>
    ///   [Поддерживается в .NET Framework 4.5.1 и более поздних версиях.]
    /// 
    ///   Преобразует делегат указанного типа в указатель на функцию, вызываемый из неуправляемого кода.
    /// </summary>
    /// <param name="d">Делегат, передаваемый в неуправляемый код.</param>
    /// <typeparam name="TDelegate">
    ///   Тип делегата для преобразования.
    /// </typeparam>
    /// <returns>
    ///   Значение, который может быть передан в неуправляемый код, который, в свою очередь, может использовать его для вызова базового управляемого делегата.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="d" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    public static IntPtr GetFunctionPointerForDelegate<TDelegate>(TDelegate d)
    {
      return Marshal.GetFunctionPointerForDelegate((Delegate) (object) d);
    }

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern IntPtr GetFunctionPointerForDelegateInternal(Delegate d);

    /// <summary>
    ///   Выделяет неуправляемый binary string (BSTR) и копирует содержимое управляемого объекта <see cref="T:System.Security.SecureString" /> объект в его.
    /// </summary>
    /// <param name="s">
    ///   Управляемый объект, подлежащий копированию.
    /// </param>
    /// <returns>
    ///   Адрес в неуправляемой памяти, куда скопирован параметр <paramref name="s" />, или 0, если передан пустой объект.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="s" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Текущий компьютер не работает под управлением Windows 2000 с пакетом обновления 3 (SP3) или более поздней версии.
    /// </exception>
    /// <exception cref="T:System.OutOfMemoryException">
    ///   Не хватает памяти.
    /// </exception>
    [SecurityCritical]
    public static IntPtr SecureStringToBSTR(SecureString s)
    {
      if (s == null)
        throw new ArgumentNullException(nameof (s));
      return s.ToBSTR();
    }

    /// <summary>
    ///   Копирует содержимое управляемого объекта <see cref="T:System.Security.SecureString" /> объекта на блок памяти, выделенный из неуправляемого распределителя COM задач.
    /// </summary>
    /// <param name="s">
    ///   Управляемый объект, подлежащий копированию.
    /// </param>
    /// <returns>
    ///   Адрес в неуправляемой памяти, куда скопирован параметр <paramref name="s" />, или 0, если передан пустой объект.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="s" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Текущий компьютер не работает под управлением Windows 2000 с пакетом обновления 3 (SP3) или более поздней версии.
    /// </exception>
    /// <exception cref="T:System.OutOfMemoryException">
    ///   Не хватает памяти.
    /// </exception>
    [SecurityCritical]
    public static IntPtr SecureStringToCoTaskMemAnsi(SecureString s)
    {
      if (s == null)
        throw new ArgumentNullException(nameof (s));
      return s.ToAnsiStr(false);
    }

    /// <summary>
    ///   Копирует содержимое управляемого объекта <see cref="T:System.Security.SecureString" /> в блок памяти, выделенный из неуправляемого распределителя COM-задач.
    /// </summary>
    /// <param name="s">
    ///   Управляемый объект, подлежащий копированию.
    /// </param>
    /// <returns>
    ///   Адрес в неуправляемой памяти, куда скопирован параметр <paramref name="s" />, или 0, если передан пустой объект.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="s" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Текущий компьютер не работает под управлением Windows 2000 с пакетом обновления 3 (SP3) или более поздней версии.
    /// </exception>
    /// <exception cref="T:System.OutOfMemoryException">
    ///   Не хватает памяти.
    /// </exception>
    [SecurityCritical]
    public static IntPtr SecureStringToCoTaskMemUnicode(SecureString s)
    {
      if (s == null)
        throw new ArgumentNullException(nameof (s));
      return s.ToUniStr(false);
    }

    /// <summary>
    ///   Освобождает BSTR указатель, который был выделен с помощью <see cref="M:System.Runtime.InteropServices.Marshal.SecureStringToBSTR(System.Security.SecureString)" /> метод.
    /// </summary>
    /// <param name="s">
    ///   Адрес <see langword="BSTR" /> для освобождения.
    /// </param>
    [SecurityCritical]
    public static void ZeroFreeBSTR(IntPtr s)
    {
      Win32Native.ZeroMemory(s, (UIntPtr) (Win32Native.SysStringLen(s) * 2U));
      Marshal.FreeBSTR(s);
    }

    /// <summary>
    ///   Освобождает указатель на неуправляемую строку, которая была выделена с помощью <see cref="M:System.Runtime.InteropServices.Marshal.SecureStringToCoTaskMemAnsi(System.Security.SecureString)" /> метод.
    /// </summary>
    /// <param name="s">Адрес освобождаемой неуправляемой строки.</param>
    [SecurityCritical]
    public static void ZeroFreeCoTaskMemAnsi(IntPtr s)
    {
      Win32Native.ZeroMemory(s, (UIntPtr) ((ulong) Win32Native.lstrlenA(s)));
      Marshal.FreeCoTaskMem(s);
    }

    /// <summary>
    ///   Освобождает указатель на неуправляемую строку, которая была выделена с помощью <see cref="M:System.Runtime.InteropServices.Marshal.SecureStringToCoTaskMemUnicode(System.Security.SecureString)" /> метод.
    /// </summary>
    /// <param name="s">Адрес освобождаемой неуправляемой строки.</param>
    [SecurityCritical]
    public static void ZeroFreeCoTaskMemUnicode(IntPtr s)
    {
      Win32Native.ZeroMemory(s, (UIntPtr) ((ulong) (Win32Native.lstrlenW(s) * 2)));
      Marshal.FreeCoTaskMem(s);
    }

    /// <summary>
    ///   Копирует содержимое управляемого объекта <see cref="T:System.Security.SecureString" /> в неуправляемую память, преобразуя в формат ANSI, во время копирования.
    /// </summary>
    /// <param name="s">
    ///   Управляемый объект, подлежащий копированию.
    /// </param>
    /// <returns>
    ///   Адрес в неуправляемой памяти, куда <paramref name="s" /> скопирован параметр, или 0, если передан пустой объект.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="s" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Текущий компьютер не работает под управлением Windows 2000 с пакетом обновления 3 (SP3) или более поздней версии.
    /// </exception>
    /// <exception cref="T:System.OutOfMemoryException">
    ///   Не хватает памяти.
    /// </exception>
    [SecurityCritical]
    public static IntPtr SecureStringToGlobalAllocAnsi(SecureString s)
    {
      if (s == null)
        throw new ArgumentNullException(nameof (s));
      return s.ToAnsiStr(true);
    }

    /// <summary>
    ///   Копирует содержимое управляемого объекта <see cref="T:System.Security.SecureString" /> объекта в неуправляемой памяти.
    /// </summary>
    /// <param name="s">
    ///   Управляемый объект, подлежащий копированию.
    /// </param>
    /// <returns>
    ///   Адрес в неуправляемой памяти, где <paramref name="s" /> был скопированных или ноль, если <paramref name="s" /> является <see cref="T:System.Security.SecureString" /> объект, длина которого равна 0.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="s" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Текущий компьютер не работает под управлением Windows 2000 с пакетом обновления 3 (SP3) или более поздней версии.
    /// </exception>
    /// <exception cref="T:System.OutOfMemoryException">
    ///   Не хватает памяти.
    /// </exception>
    [SecurityCritical]
    public static IntPtr SecureStringToGlobalAllocUnicode(SecureString s)
    {
      if (s == null)
        throw new ArgumentNullException(nameof (s));
      return s.ToUniStr(true);
    }

    /// <summary>
    ///   Освобождает указатель на неуправляемую строку, которая была выделена с помощью <see cref="M:System.Runtime.InteropServices.Marshal.SecureStringToGlobalAllocAnsi(System.Security.SecureString)" /> метод.
    /// </summary>
    /// <param name="s">Адрес освобождаемой неуправляемой строки.</param>
    [SecurityCritical]
    public static void ZeroFreeGlobalAllocAnsi(IntPtr s)
    {
      Win32Native.ZeroMemory(s, (UIntPtr) ((ulong) Win32Native.lstrlenA(s)));
      Marshal.FreeHGlobal(s);
    }

    /// <summary>
    ///   Освобождает указатель на неуправляемую строку, которая была выделена с помощью <see cref="M:System.Runtime.InteropServices.Marshal.SecureStringToGlobalAllocUnicode(System.Security.SecureString)" /> метод.
    /// </summary>
    /// <param name="s">Адрес освобождаемой неуправляемой строки.</param>
    [SecurityCritical]
    public static void ZeroFreeGlobalAllocUnicode(IntPtr s)
    {
      Win32Native.ZeroMemory(s, (UIntPtr) ((ulong) (Win32Native.lstrlenW(s) * 2)));
      Marshal.FreeHGlobal(s);
    }
  }
}
