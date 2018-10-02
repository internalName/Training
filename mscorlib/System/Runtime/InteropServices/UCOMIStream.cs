// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.UCOMIStream
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Взамен рекомендуется использовать <see cref="T:System.Runtime.InteropServices.ComTypes.IStream" />.
  /// </summary>
  [Obsolete("Use System.Runtime.InteropServices.ComTypes.IStream instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
  [Guid("0000000c-0000-0000-C000-000000000046")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  public interface UCOMIStream
  {
    /// <summary>
    ///   Считывает указанное число байтов из потока объекта в памяти, начиная с текущего указателя поиска.
    /// </summary>
    /// <param name="pv">
    ///   При удачном возвращении содержит данные, считанные из потока.
    /// </param>
    /// <param name="cb">
    ///   Число байтов, считываемых из объекта потока.
    /// </param>
    /// <param name="pcbRead">
    ///   Указатель на <see langword="ULONG" /> переменной, которая получает фактическое число байтов, считанных из объекта потока.
    /// </param>
    void Read([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1), Out] byte[] pv, int cb, IntPtr pcbRead);

    /// <summary>
    ///   Записывает заданное число байтов в объект потока, начиная с текущего указателя поиска.
    /// </summary>
    /// <param name="pv">Буфер для записи потока.</param>
    /// <param name="cb">Число байтов, записываемое в поток.</param>
    /// <param name="pcbWritten">
    ///   При удачном возвращении содержит фактическое число байтов, записанных в объект потока.
    ///    Вызывающий объект может установить указатель на <see langword="null" />, в этом случае этот метод не предоставляет фактическое число записанных байтов.
    /// </param>
    void Write([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] byte[] pv, int cb, IntPtr pcbWritten);

    /// <summary>
    ///   Изменяет указатель поиска в новое расположение относительно начала потока, конца потока или текущего указателя поиска.
    /// </summary>
    /// <param name="dlibMove">
    ///   Смещение, добавляемое к <paramref name="dwOrigin" />.
    /// </param>
    /// <param name="dwOrigin">
    ///   Задает исходное положение поиска.
    ///    Источник может быть началом файла, текущий поиск указателя или конец файла.
    /// </param>
    /// <param name="plibNewPosition">
    ///   При удачном возвращении содержит смещение указателя поиска от начала потока.
    /// </param>
    void Seek(long dlibMove, int dwOrigin, IntPtr plibNewPosition);

    /// <summary>Изменяет размер объекта потока.</summary>
    /// <param name="libNewSize">
    ///   Задает новый размер потока в байтах.
    /// </param>
    void SetSize(long libNewSize);

    /// <summary>
    ///   Копирует указанное число байтов из текущего указателя поиска в потоке до текущего указателя поиска в другой поток.
    /// </summary>
    /// <param name="pstm">Ссылка на поток назначения.</param>
    /// <param name="cb">
    ///   Число байт для копирования из исходного потока.
    /// </param>
    /// <param name="pcbRead">
    ///   При удачном возвращении содержит фактическое число байтов, считанных из источника.
    /// </param>
    /// <param name="pcbWritten">
    ///   При удачном возвращении содержит фактическое число байтов, записанных в назначение.
    /// </param>
    void CopyTo(UCOMIStream pstm, long cb, IntPtr pcbRead, IntPtr pcbWritten);

    /// <summary>
    ///   Убедиться, что любые изменения, внесенные в объект потока, открытый в режиме транзакций, отражены в родительском хранилище.
    /// </summary>
    /// <param name="grfCommitFlags">
    ///   Определяет, как фиксируются изменения для объекта потока.
    /// </param>
    void Commit(int grfCommitFlags);

    /// <summary>
    ///   Отменяет все изменения, внесенные в поток транзакций с момента последнего <see cref="M:System.Runtime.InteropServices.UCOMIStream.Commit(System.Int32)" /> вызова.
    /// </summary>
    void Revert();

    /// <summary>
    ///   Ограничивает доступ к указанному диапазону байтов в потоке.
    /// </summary>
    /// <param name="libOffset">
    ///   Смещение в байтах для начала диапазона.
    /// </param>
    /// <param name="cb">Длина диапазона в байтах.</param>
    /// <param name="dwLockType">
    ///   Запрошенные ограничения для доступа к диапазону.
    /// </param>
    void LockRegion(long libOffset, long cb, int dwLockType);

    /// <summary>
    ///   Удаляет ограничения доступа на диапазон байтов, ранее ограничивавшие с <see cref="M:System.Runtime.InteropServices.UCOMIStream.LockRegion(System.Int64,System.Int64,System.Int32)" />.
    /// </summary>
    /// <param name="libOffset">
    ///   Смещение в байтах для начала диапазона.
    /// </param>
    /// <param name="cb">Длина диапазона в байтах.</param>
    /// <param name="dwLockType">
    ///   Диапазон ранее накладываются ограничения доступа.
    /// </param>
    void UnlockRegion(long libOffset, long cb, int dwLockType);

    /// <summary>
    ///   Извлекает <see cref="T:System.Runtime.InteropServices.STATSTG" /> структуры для этого потока.
    /// </summary>
    /// <param name="pstatstg">
    ///   При удачном возвращении содержит <see langword="STATSTG" /> структуры, который описывает этот объект потока.
    /// </param>
    /// <param name="grfStatFlag">
    ///   Задает некоторые члены в <see langword="STATSTG" /> структуру, этот метод не возвращает, тем самым экономя память операций выделения.
    /// </param>
    void Stat(out STATSTG pstatstg, int grfStatFlag);

    /// <summary>
    ///   Создает новый объект потока с собственным указателем, который ссылается на те же байты, что и исходный поток.
    /// </summary>
    /// <param name="ppstm">
    ///   При удачном возвращении содержит новый объект потока.
    /// </param>
    void Clone(out UCOMIStream ppstm);
  }
}
