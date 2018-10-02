// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.UCOMIEnumString
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Взамен рекомендуется использовать <see cref="T:System.Runtime.InteropServices.ComTypes.IEnumString" />.
  /// </summary>
  [Obsolete("Use System.Runtime.InteropServices.ComTypes.IEnumString instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
  [Guid("00000101-0000-0000-C000-000000000046")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  public interface UCOMIEnumString
  {
    /// <summary>
    ///   Возвращает указанное число элементов в последовательности перечисления.
    /// </summary>
    /// <param name="celt">
    ///   Число строк для возвращения в <paramref name="rgelt" />.
    /// </param>
    /// <param name="rgelt">
    ///   При удачном возвращении ссылку на перечисленные строки.
    /// </param>
    /// <param name="pceltFetched">
    ///   При удачном возвращении — ссылка на фактическое число строк, перечисленных в <paramref name="rgelt" />.
    /// </param>
    /// <returns>
    ///   <see langword="S_OK" /> Если <paramref name="pceltFetched" /> параметр равен <paramref name="celt" /> параметр; в противном случае — <see langword="S_FALSE" />.
    /// </returns>
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Next(int celt, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr), Out] string[] rgelt, out int pceltFetched);

    /// <summary>
    ///   Пропускает указанное число элементов в последовательности перечисления.
    /// </summary>
    /// <param name="celt">
    ///   Число элементов, пропускаемых в перечислении.
    /// </param>
    /// <returns>
    ///   <see langword="S_OK" /> Если число пропущенных элементов равно <paramref name="celt" /> параметр; в противном случае — <see langword="S_FALSE" />.
    /// </returns>
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Skip(int celt);

    /// <summary>
    ///   Сбрасывает последовательность перечисления в начало.
    /// </summary>
    /// <returns>
    ///   Значение HRESULT со значением <see langword="S_OK" />.
    /// </returns>
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Reset();

    /// <summary>
    ///   Создает другой перечислитель с тем же состоянием, что и текущий.
    /// </summary>
    /// <param name="ppenum">
    ///   При удачном возвращении ссылку на только что созданный перечислитель.
    /// </param>
    void Clone(out UCOMIEnumString ppenum);
  }
}
