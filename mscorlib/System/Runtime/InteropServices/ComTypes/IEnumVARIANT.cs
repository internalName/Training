// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComTypes.IEnumVARIANT
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices.ComTypes
{
  /// <summary>
  ///   Управляет определением <see langword="IEnumVARIANT" /> интерфейса.
  /// </summary>
  [Guid("00020404-0000-0000-C000-000000000046")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [__DynamicallyInvokable]
  [ComImport]
  public interface IEnumVARIANT
  {
    /// <summary>
    ///   Возвращает указанное число элементов в последовательности перечисления.
    /// </summary>
    /// <param name="celt">
    ///   Число элементов для возвращения в <paramref name="rgelt" />.
    /// </param>
    /// <param name="rgVar">
    ///   При возвращении данного метода содержит ссылку на перечисленные элементы.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    /// <param name="pceltFetched">
    ///   При возвращении данного метода содержит ссылку на фактическое число элементов, перечисленных в <paramref name="rgelt" />.
    /// </param>
    /// <returns>
    ///   <see langword="S_OK" /> Если <paramref name="pceltFetched" /> параметр равен <paramref name="celt" /> параметр; в противном случае — <see langword="S_FALSE" />.
    /// </returns>
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Next(int celt, [MarshalAs(UnmanagedType.LPArray), Out] object[] rgVar, IntPtr pceltFetched);

    /// <summary>
    ///   Пропускает указанное число элементов в последовательности перечисления.
    /// </summary>
    /// <param name="celt">
    ///   Число элементов, пропускаемых в перечислении.
    /// </param>
    /// <returns>
    ///   <see langword="S_OK" /> Если число пропущенных элементов равно <paramref name="celt" /> параметр; в противном случае — <see langword="S_FALSE" />.
    /// </returns>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Skip(int celt);

    /// <summary>
    ///   Сбрасывает последовательность перечисления в начало.
    /// </summary>
    /// <returns>
    ///   Значение HRESULT со значением <see langword="S_OK" />.
    /// </returns>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Reset();

    /// <summary>
    ///   Создает новый перечислитель с тем же состоянием перечисления, что и текущий.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Runtime.InteropServices.ComTypes.IEnumVARIANT" />  Ссылку на только что созданный перечислитель.
    /// </returns>
    [__DynamicallyInvokable]
    IEnumVARIANT Clone();
  }
}
