// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComTypes.IStream
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices.ComTypes
{
  /// <summary>
  ///   Предоставляет управляемое определение интерфейса <see langword="IStream" /> с функциональными возможностями <see langword="ISequentialStream" />.
  /// </summary>
  [Guid("0000000c-0000-0000-C000-000000000046")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [__DynamicallyInvokable]
  [ComImport]
  public interface IStream
  {
    /// <summary>
    ///   Считывает указанное число байтов из потока объекта в памяти, начиная с текущего указателя поиска.
    /// </summary>
    /// <param name="pv">
    ///   При возвращении данного метода содержит данные, считанные из потока.
    ///    Этот параметр передается неинициализированным.
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
    ///    Если вызывающий объект задает этот указатель <see cref="F:System.IntPtr.Zero" />, этот метод не предоставляет фактическое число записанных байтов.
    /// </param>
    void Write([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] byte[] pv, int cb, IntPtr pcbWritten);

    /// <summary>
    ///   Изменяет указатель поиска в новое расположение относительно начала потока, конца потока или текущего указателя поиска.
    /// </summary>
    /// <param name="dlibMove">
    ///   Смещение, добавляемое к <paramref name="dwOrigin" />.
    /// </param>
    /// <param name="dwOrigin">
    ///   Начальная точка поиска.
    ///    Источник может быть началом файла, текущий поиск указателя или конец файла.
    /// </param>
    /// <param name="plibNewPosition">
    ///   При удачном возвращении содержит смещение указателя поиска от начала потока.
    /// </param>
    void Seek(long dlibMove, int dwOrigin, IntPtr plibNewPosition);

    /// <summary>Изменяет размер объекта потока.</summary>
    /// <param name="libNewSize">Новый размер потока в байтах.</param>
    [__DynamicallyInvokable]
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
    void CopyTo(IStream pstm, long cb, IntPtr pcbRead, IntPtr pcbWritten);

    /// <summary>
    ///   Убедиться, что любые изменения, внесенные в объект потока, открытый в режиме транзакций отражены в родительском хранилище.
    /// </summary>
    /// <param name="grfCommitFlags">
    ///   Значение, которое определяет, как фиксируются изменения для объекта потока.
    /// </param>
    [__DynamicallyInvokable]
    void Commit(int grfCommitFlags);

    /// <summary>
    ///   Отменяет все изменения, внесенные в поток транзакций с момента последнего <see cref="M:System.Runtime.InteropServices.ComTypes.IStream.Commit(System.Int32)" /> вызова.
    /// </summary>
    [__DynamicallyInvokable]
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
    [__DynamicallyInvokable]
    void LockRegion(long libOffset, long cb, int dwLockType);

    /// <summary>
    ///   Удаляет ограничения доступа на диапазон байтов, ранее ограничивался <see cref="M:System.Runtime.InteropServices.ComTypes.IStream.LockRegion(System.Int64,System.Int64,System.Int32)" /> метод.
    /// </summary>
    /// <param name="libOffset">
    ///   Смещение в байтах для начала диапазона.
    /// </param>
    /// <param name="cb">Длина диапазона в байтах.</param>
    /// <param name="dwLockType">
    ///   Диапазон ранее накладываются ограничения доступа.
    /// </param>
    [__DynamicallyInvokable]
    void UnlockRegion(long libOffset, long cb, int dwLockType);

    /// <summary>
    ///   Извлекает <see cref="T:System.Runtime.InteropServices.STATSTG" /> структуры для этого потока.
    /// </summary>
    /// <param name="pstatstg">
    ///   При возвращении данного метода содержит <see langword="STATSTG" /> структуру, описывающую этот объект потока.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    /// <param name="grfStatFlag">
    ///   Элементы в <see langword="STATSTG" /> структуру, этот метод не возвращает, тем самым экономя память операций выделения.
    /// </param>
    [__DynamicallyInvokable]
    void Stat(out STATSTG pstatstg, int grfStatFlag);

    /// <summary>
    ///   Создает новый объект потока с собственным указателем, который ссылается на те же байты, что и исходный поток.
    /// </summary>
    /// <param name="ppstm">
    ///   При возвращении данного метода содержит новый объект потока.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    [__DynamicallyInvokable]
    void Clone(out IStream ppstm);
  }
}
