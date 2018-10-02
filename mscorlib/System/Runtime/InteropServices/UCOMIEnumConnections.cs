// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.UCOMIEnumConnections
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Взамен рекомендуется использовать <see cref="T:System.Runtime.InteropServices.ComTypes.IEnumConnections" />.
  /// </summary>
  [Obsolete("Use System.Runtime.InteropServices.ComTypes.IEnumConnections instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
  [Guid("B196B287-BAB4-101A-B69C-00AA00341D07")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  public interface UCOMIEnumConnections
  {
    /// <summary>
    ///   Возвращает указанное число элементов в последовательности перечисления.
    /// </summary>
    /// <param name="celt">
    ///   Количество <see cref="T:System.Runtime.InteropServices.CONNECTDATA" /> структур для возврата в <paramref name="rgelt" />.
    /// </param>
    /// <param name="rgelt">
    ///   При удачном возвращении ссылку на перечисленные соединения.
    /// </param>
    /// <param name="pceltFetched">
    ///   При удачном возвращении — ссылка на фактическое число подключений, перечисленных в <paramref name="rgelt" />.
    /// </param>
    /// <returns>
    ///   <see langword="S_OK" /> Если <paramref name="pceltFetched" /> параметр равен <paramref name="celt" /> параметр; в противном случае — <see langword="S_FALSE" />.
    /// </returns>
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Next(int celt, [MarshalAs(UnmanagedType.LPArray), Out] CONNECTDATA[] rgelt, out int pceltFetched);

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
    [MethodImpl(MethodImplOptions.PreserveSig)]
    void Reset();

    /// <summary>
    ///   Создает другой перечислитель с тем же состоянием, что и текущий.
    /// </summary>
    /// <param name="ppenum">
    ///   При удачном возвращении ссылку на только что созданный перечислитель.
    /// </param>
    void Clone(out UCOMIEnumConnections ppenum);
  }
}
